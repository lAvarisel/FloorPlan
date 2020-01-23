using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.ComponentModel;
using System.IO;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        public static bool flag_base_model = false;
        public static bool flag_custom_model = false;
        public static bool flag_doors_windows = false;    
        public static bool flagCount = false;
        private FloorplanModel InputData;
        public SaveInfo InfoSave;

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void Rec1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        void MinButtonClick(object sender, RoutedEventArgs e)
        {
            Window window = ((FrameworkElement)sender).TemplatedParent as Window;
            if (window != null) window.WindowState = WindowState.Minimized;
        }
        private void LoadBase_Click(object sender, RoutedEventArgs e)
        {
            flag_base_model = true;
            double H_floor = Convert.ToDouble(h_level_floor.Text);
            FloorplanModel m1 = new FloorplanModel();
            m1.Apartments = Model_Floor_1.Apartsments();
            m1.Points = Model_Floor_1.Points();
            m1.Walls = Model_Floor_1.Walls();
            double Door_w = Convert.ToDouble(weightDoor.Text);
            double Window_w = Convert.ToDouble(weightWindow.Text);
            double Door_h = Convert.ToDouble(Door_he.Text);
            double Window_h = Convert.ToDouble(h_Window.Text);
            double Window_l = Convert.ToDouble(lev_Window.Text);
            double Location = Convert.ToDouble(Location_O.Text);
            int Col_Level = Convert.ToInt32(Col_level.Text);
            InfoSave = FileUtils.CreateData(
                m1, 
                H_floor, 
                Door_w, 
                Window_w, 
                Door_h, 
                Window_h, 
                Window_l, 
                Location, 
                Col_Level);
            RenderLines(m1);
            flagCount = true;
        }
        private void Load_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            // Set filter options and filter index.
            dlg.FileName = "documetn";
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text documents (.txt)|*.txt";

            // Call the ShowDialog method to show the dialog box.
            bool? userClickedOK = dlg.ShowDialog();

            // Process input if the user clicked OK.
            if (userClickedOK == true)
            {
                // Open the selected file to read.               
                // Read the first line from the file and write it the textbox.
                string filename = dlg.FileName;
                FloorplanModel modelfloor = FileUtils.File_Read(filename);
                InputData = modelfloor;
                double H_floor = Convert.ToDouble(h_level_floor.Text);
                double Door_w = Convert.ToDouble(weightDoor.Text);
                double Window_w = Convert.ToDouble(weightWindow.Text);
                double Door_h = Convert.ToDouble(Door_he.Text);
                double Window_h = Convert.ToDouble(h_Window.Text);
                double Window_l = Convert.ToDouble(lev_Window.Text);
                double Location = Convert.ToDouble(Location_O.Text);
                int Col_Level = Convert.ToInt32(Col_level.Text);
                flag_custom_model = true;
                InfoSave = FileUtils.CreateData(
                    InputData, 
                    H_floor, 
                    Door_w, 
                    Window_w, 
                    Door_h, 
                    Window_h, 
                    Window_l, 
                    Location, 
                    Col_Level);
                RenderLines(InputData);
                MessageBox.Show(filename + "Модель успешно загружена");
                flagCount = true;
            }
        }
        private void GenerOpening_Click(object sender, RoutedEventArgs e)
        {
            double H_floor = Convert.ToDouble(h_level_floor.Text);
            double Door_w = Convert.ToDouble(weightDoor.Text);
            double Window_w = Convert.ToDouble(weightWindow.Text);
            double Door_h = Convert.ToDouble(Door_he.Text);
            double Window_h = Convert.ToDouble(h_Window.Text);
            double Window_l = Convert.ToDouble(lev_Window.Text);
            double Location = Convert.ToDouble(Location_O.Text);
            int Col_Level = Convert.ToInt32(Col_level.Text);
            if (flag_base_model == true)
            {
                flag_doors_windows = true;
                FloorplanModel m1 = new FloorplanModel();
                m1.Apartments = Model_Floor_1.Apartsments();
                m1.Points = Model_Floor_1.Points();
                m1.Walls = Model_Floor_1.Walls();
                InfoSave = FileUtils.CreateData(
                    m1, 
                    H_floor, 
                    Door_w, 
                    Window_w, 
                    Door_h, 
                    Window_h, 
                    Window_l, 
                    Location, 
                    Col_Level);
                RenderOpening(InfoSave);
            }
            else if (flag_custom_model == true && InputData != null)
            {
                InfoSave = FileUtils.CreateData(
                    InputData, 
                    H_floor, 
                    Door_w, 
                    Window_w, 
                    Door_h, 
                    Window_h, 
                    Window_l, 
                    Location, 
                    Col_Level);
                flag_doors_windows = true;
                RenderOpening(InfoSave);
                //CreateOpening(modelfloor);
            }
        }
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Canva1.Children.Clear();
            flag_base_model = false;
            flag_custom_model = false;
            flag_doors_windows = false;
            InfoSave = null;
            InputData = null;
            flagCount = false;
        }
        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveInfo InfoS = new SaveInfo();
            InfoS = InfoSave;

            progbar.Maximum = InfoS.MyMesh.Positions.Count + InfoS.MyMesh.TextureCoordinates.Count + InfoS.myWalls.Count;
            progbar.Value = 0;

            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "FloorModel";
            dlg.DefaultExt = ".obj";
            dlg.Filter = "3D model format (.obj)|*.obj";

            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;
                if (flag_base_model == true)
                {
                    FloorplanModel m1 = new FloorplanModel();
                    m1.Apartments = Model_Floor_1.Apartsments();
                    m1.Points = Model_Floor_1.Points();
                    m1.Walls = Model_Floor_1.Walls();

                    var progress = new Progress<double>(s => progbar.Value = s);

                    await FileUtils.CreateObj(filename, InfoS, progress);

                    //await Task.Factory.StartNew(
                    //                           () => FileUtils.CreateObj(filename, InfoS, progress),
                    //                           TaskCreationOptions.AttachedToParent);

                    
                }
                else if (flag_custom_model == true)
                {
                    var progress = new Progress<double>(s => progbar.Value = s);

                    await Task.Factory.StartNew(
                                               () => FileUtils.CreateObj(filename, InfoS, progress),
                                               TaskCreationOptions.AttachedToParent);
                }
                MessageBox.Show(filename + "Модель успешно сохранена");
            }
        }
        private void RenderLines(FloorplanModel model)
        {
            List<Point> FloorPoints = model.Points;
            List<Wall> FloorWalls = model.Walls;
            for (int i = 0; i < FloorWalls.Count; i++)
            {
                double K = 15;
                Line myLine = new Line();
                myLine.Stroke = System.Windows.Media.Brushes.Black;
                myLine.X1 = FloorPoints[FloorWalls[i].Start.Item1].X * K;
                myLine.X2 = FloorPoints[FloorWalls[i].End.Item1].X * K;
                myLine.Y1 = FloorPoints[FloorWalls[i].Start.Item1].Y * K;
                myLine.Y2 = FloorPoints[FloorWalls[i].End.Item1].Y * K;
                string p1 = FloorWalls[i].Start.Item1.ToString();
                string p2 = FloorWalls[i].End.Item1.ToString();
                Text(FloorPoints[FloorWalls[i].Start.Item1].X * K, FloorPoints[FloorWalls[i].Start.Item1].Y * K, p1);
                Text(FloorPoints[FloorWalls[i].End.Item1].X * K, FloorPoints[FloorWalls[i].End.Item1].Y * K, p2);
                Canva1.Children.Add(myLine);
            }
        }
        private void RenderOpening(SaveInfo InfoSave)
        {
            List<MyLine> myRenderLines = InfoSave.myLines;

            for (int i = 0; i < myRenderLines.Count; i++)
            {
                Line myLine = new Line();

                if (InfoSave.myLines[i].Type == OpeningType.Door)
                {
                    myLine.Stroke = System.Windows.Media.Brushes.Brown;
                }
                else if (InfoSave.myLines[i].Type == OpeningType.Window)
                {
                    myLine.Stroke = System.Windows.Media.Brushes.Blue;
                }
                else if (InfoSave.myLines[i].Type == OpeningType.Entrance)
                {
                    myLine.Stroke = System.Windows.Media.Brushes.White;
                }
                else
                {
                    myLine.Stroke = System.Windows.Media.Brushes.Black;
                }

                myLine.StrokeThickness = 3;
                myLine.X1 = InfoSave.Points[myRenderLines[i].Start].X * 15;
                myLine.X2 = InfoSave.Points[myRenderLines[i].End].X * 15;
                myLine.Y1 = InfoSave.Points[myRenderLines[i].Start].Y * 15;
                myLine.Y2 = InfoSave.Points[myRenderLines[i].End].Y * 15;
                Canva1.Children.Add(myLine);
            }
        }
        private void Text(double x, double y, string text)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = text;
            textBlock.Foreground = new SolidColorBrush(Colors.Black);
            textBlock.FontSize = 8;
            Canvas.SetLeft(textBlock, x);
            Canvas.SetTop(textBlock, y);
            Canva1.Children.Add(textBlock);
        }
        private void b1_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;                   
        }
        private void MainWindow_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.WindowState.Maximized)
            {
                this.WindowState = System.Windows.WindowState.Normal;
                this.Left = 0;
                this.Top = 0;
                this.Height = SystemParameters.WorkArea.Height;
                this.Width = SystemParameters.WorkArea.Width;
            }

        }


    }

}
