using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;

namespace WpfApp2
{
    public class FileUtils
    {
        
        public static async Task CreateObj(string pathToFile, SaveInfo model, IProgress<double> progress)
        {
            SaveInfo InfoSave = model;


            File.AppendAllText(pathToFile, Environment.NewLine);
            File.AppendAllText(pathToFile, "mtllib chair.mtl");

            int verticesCount = 0;
            int facesCount = 0;
            int normals = 0;
            int uvCount = 0;
            double amout = 0;

            for (int i = 0; i < model.MyMesh.Positions.Count; i++)
            {
                verticesCount++;

                string Point_X = Convert.ToString(model.MyMesh.Positions[i].X).Replace(",", ".");
                string Point_Y = Convert.ToString(model.MyMesh.Positions[i].Y).Replace(",", ".");
                string Point_Z = Convert.ToString(model.MyMesh.Positions[i].Z).Replace(",", ".");

                File.AppendAllText(pathToFile, Environment.NewLine);
                //File.AppendAllText(pathToFile, "v " + Point_X + " " + Point_Y + " " + Point_Z);
                string text = "v " + Point_X + " " + Point_Y + " " + Point_Z;


                using (StreamWriter sourceStream = new StreamWriter(pathToFile, true, System.Text.Encoding.Default))
                {
                    await sourceStream.WriteAsync(text);
                };
                amout++;
                progress.Report(amout);

            }

            File.AppendAllText(pathToFile, Environment.NewLine);
            File.AppendAllText(pathToFile, "# " + verticesCount + " " + "vertites");
            File.AppendAllText(pathToFile, Environment.NewLine);

            for (int i = 0; i < model.MyMesh.Normals.Count; i++)
            {
                normals++;

                string Point_X = Convert.ToString(model.MyMesh.Normals[i].X).Replace(",", ".");
                string Point_Y = Convert.ToString(model.MyMesh.Normals[i].Y).Replace(",", ".");
                string Point_Z = Convert.ToString(model.MyMesh.Normals[i].Z).Replace(",", ".");

                File.AppendAllText(pathToFile, Environment.NewLine);
                //File.AppendAllText(pathToFile, "vn " + Point_X + " " + Point_Y + " " + Point_Z);

                byte[] s = Encoding.Unicode.GetBytes("vn " + Point_X + " " + Point_Y + " " + Point_Z);


        //        using (FileStream sourceStream = new FileStream(pathToFile,
        //FileMode.Append, FileAccess.Write, FileShare.None,
        //bufferSize: 4096, useAsync: true))
        //        {
        //            await sourceStream.WriteAsync(s, 0, s.Length);
        //        };

        //        amout++;
        //        progress.Report(amout);
            }

            File.AppendAllText(pathToFile, Environment.NewLine);
            File.AppendAllText(pathToFile, "# " + normals + " " + "vertex normals");
            File.AppendAllText(pathToFile, Environment.NewLine);

            for (int i = 0; i < model.MyMesh.TextureCoordinates.Count; i++)
            {
                uvCount++;

                string Point_X = Convert.ToString(model.MyMesh.TextureCoordinates[i].X).Replace(",", ".");
                string Point_Y = Convert.ToString(model.MyMesh.TextureCoordinates[i].Y).Replace(",", ".");


                File.AppendAllText(pathToFile, Environment.NewLine);
                //File.AppendAllText(pathToFile, "vt " + Point_X + " " + Point_Y);

        //        byte[] s = Encoding.Unicode.GetBytes("vt " + Point_X + " " + Point_Y);


        //        using (FileStream sourceStream = new FileStream(pathToFile,
        //FileMode.Append, FileAccess.Write, FileShare.None,
        //bufferSize: 4096, useAsync: true))
        //        {
        //            await sourceStream.WriteAsync(s, 0, s.Length);
        //        };

        //        amout++;
        //        progress.Report(amout);
            }

            File.AppendAllText(pathToFile, Environment.NewLine);
            File.AppendAllText(pathToFile, "# " + uvCount + " " + "texture coords");
            File.AppendAllText(pathToFile, Environment.NewLine);

            File.AppendAllText(pathToFile, Environment.NewLine);
            File.AppendAllText(pathToFile, "usemtl chair");
            File.AppendAllText(pathToFile, Environment.NewLine);
            File.AppendAllText(pathToFile, "s off");
            File.AppendAllText(pathToFile, Environment.NewLine);

            for (int i = 0; i < model.myWalls.Count; i++)
            {
                for (int j = 0; j < model.myWalls[i].myTriangle.Count; j++)
                {
                    facesCount++;

                    int p1 = model.myWalls[i].myTriangle[j].p1.indexPoint + 1;
                    int p2 = model.myWalls[i].myTriangle[j].p2.indexPoint + 1;
                    int p3 = model.myWalls[i].myTriangle[j].p3.indexPoint + 1;

                    int t1 = model.myWalls[i].myTriangle[j].p1.index_vt;
                    int t2 = model.myWalls[i].myTriangle[j].p2.index_vt;
                    int t3 = model.myWalls[i].myTriangle[j].p3.index_vt;

                    File.AppendAllText(pathToFile, Environment.NewLine);
                    //File.AppendAllText(pathToFile, "f " + p1 + "/" + t1 + " " + p2 + "/" + t2 + " " + p3 + "/" + t3);

                    byte[] s = Encoding.Unicode.GetBytes("f " + p1 + "/" + t1 + " " + p2 + "/" + t2 + " " + p3 + "/" + t3);


                    //        using (FileStream sourceStream = new FileStream(pathToFile,
                    //FileMode.Append, FileAccess.Write, FileShare.None,
                    //bufferSize: 4096, useAsync: true))
                    //        {
                    //            await sourceStream.WriteAsync(s, 0, s.Length);
                    //        };

                    //        amout++;
                    //        progress.Report(amout);
                }
            }
            File.AppendAllText(pathToFile, Environment.NewLine);
            File.AppendAllText(pathToFile, "# " + facesCount + " " + "faces");
            File.AppendAllText(pathToFile, Environment.NewLine);
        }

        public static FloorplanModel File_Read(string File_name)
        {
            FloorplanModel InputData = new FloorplanModel();
            InputData.Apartments = new List<Apartment>();
            InputData.Points = new List<Point>();
            InputData.Walls = new List<Wall>();

            Apartment Apartment_i = new Apartment();
            Apartment_i.Rooms = new List<Room>();

            FileStream file = new FileStream(File_name, FileMode.Open);
            StreamReader readFile = new StreamReader(file);

            while (!readFile.EndOfStream)
            {
                string s = readFile.ReadLine();
                string[] split = s.Split(' ');
                if (s.StartsWith("p"))
                {
                    double x, y;
                    x = Convert.ToDouble(split[1]);
                    y = Convert.ToDouble(split[2]);
                    InputData.Points.Add(new Point
                    {
                        X = x,
                        Y = y
                    });
                }
                else if (s.StartsWith("w"))
                {
                    int s_wall = Int32.Parse(split[1]);
                    int f_wall = Int32.Parse(split[2]);
                    InputData.Walls.Add(new Wall
                    {
                        Start = new Tuple<int>(s_wall),
                        End = new Tuple<int>(f_wall)
                    });
                }

                else if (s.StartsWith("r"))
                {
                    int roomwall;
                    Room room_i = new Room();
                    room_i.Walls = new List<int>();
                    for (int i = 0; i < s.Count(x => x == ' ') + 1; i++)
                    {
                        if (split[i] != "r")
                        {
                            roomwall = Int32.Parse(split[i]);
                            room_i.Walls.Add(roomwall);
                        }
                    }
                    Apartment_i.Rooms.Add(room_i);
                }
                else if (s.StartsWith("a"))
                {
                    int ap;
                    Apartment apartment_i = new Apartment();
                    apartment_i.Rooms = new List<Room>();
                    for (int i = 0; i < s.Count(x => x == ' ') + 1; i++)
                    {
                        if (split[i] != "a")
                        {
                            ap = Int32.Parse(split[i]);
                            apartment_i.Rooms.Add(Apartment_i.Rooms[ap]);
                        }
                    }
                    InputData.Apartments.Add(apartment_i);
                }
            }
            readFile.Close();
            return InputData;
        }

        public static List<Wall> Type_Walls1(FloorplanModel model, double DoorW)
        {
            List<Apartment> Apartments_Floor_1 = model.Apartments;
            List<Point> FloorPoints = model.Points;

            List<int> External_Wall = new List<int>();
            List<int> Internal_Apartment_Wall = new List<int>();
            List<int> Internal_Room_Wall = new List<int>();

            FloorplanModel Type_Walls = new FloorplanModel();
            Type_Walls.Walls = new List<Wall>();

            List<int> Internal_Wall_lock = new List<int>();// временное локальное хранилище внутренних стен
            List<int> External_Wall_lock = new List<int>();// временное локальное хранилище предполагаемых изначально наружных стен (общий лист стен этажа)

            List<Room> Ap_Wall = new List<Room>();// храним стены по квартирам

            for (int i = 0; i < Apartments_Floor_1.Count; i++)
            {
                Room locklist = new Room();
                locklist.Walls = new List<int>();
                locklist.Walls.Clear();
                for (int j = 0; j < Apartments_Floor_1[i].Rooms.Count; j++)
                {
                    foreach (int value in Apartments_Floor_1[i].Rooms[j].Walls)
                    {
                        if (External_Wall_lock.Contains(value))
                        {
                            Internal_Wall_lock.Add(value);
                        }
                        else
                        {
                            External_Wall_lock.Add(value);
                        }
                        while (locklist.Walls.Contains(value) == false)
                        {
                            locklist.Walls.Add(value);
                        }
                    }
                }
                Ap_Wall.Add(locklist);
            }

            foreach (int value in External_Wall_lock)
            {
                if (Internal_Wall_lock.Contains(value) == false)
                {
                    External_Wall.Add(value);
                }
            }

            List<int> Internal_Wall_lock_1 = new List<int>();
            for (int i = 0; i < Ap_Wall.Count; i++)
            {
                foreach (int value in Ap_Wall[i].Walls)
                {
                    if (Internal_Wall_lock_1.Contains(value))
                    {
                        Internal_Apartment_Wall.Add(value);
                    }
                    else
                    {
                        Internal_Wall_lock_1.Add(value);
                    }
                }
            }

            foreach (int value in Internal_Wall_lock)
            {
                if (Internal_Apartment_Wall.Contains(value) == false)
                {
                    Internal_Room_Wall.Add(value);
                }
            }

            List<Wall> Floor1Wall = model.Walls;
            List<int> Floor_wall_lock = new List<int>();

            for (int i = 0; i < Floor1Wall.Count; i++)
            {
                Floor_wall_lock.Add(i);
            }

            foreach (int value in Floor_wall_lock)
            {
                if (External_Wall.Contains(value))
                {
                    Opening Window = new Opening();
                    Window.Type = OpeningType.Window;
                    Window.Location = 0.5;
                    Floor1Wall[value].Openings = new List<Opening>();
                    Floor1Wall[value].Openings.Add(Window);
                }
                else if (Internal_Room_Wall.Contains(value))
                {
                    Opening Door = new Opening();
                    Door.Type = OpeningType.Door;
                    Door.Location = 0.5;
                    Floor1Wall[value].Openings = new List<Opening>();
                    Floor1Wall[value].Openings.Add(Door);
                }
                else 
                {
                    for (int j = 0; j < Apartments_Floor_1[0].Rooms.Count; j++)
                    {
                        double x1 = FloorPoints[Floor1Wall[value].Start.Item1].X;
                        double y1 = FloorPoints[Floor1Wall[value].Start.Item1].Y;
                        double x2 = FloorPoints[Floor1Wall[value].End.Item1].X;
                        double y2 = FloorPoints[Floor1Wall[value].End.Item1].Y;
                        double L = Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));

                        if (Apartments_Floor_1[0].Rooms[j].Walls.Contains(value) && L > DoorW)
                        {
                            Opening Entrance = new Opening();
                            Entrance.Type = OpeningType.Entrance;
                            Entrance.Location = 0.5;
                            Floor1Wall[value].Openings = new List<Opening>();
                            Floor1Wall[value].Openings.Add(Entrance);
                        }
                        else
                        {
                            Opening Closed = new Opening();
                            Closed.Type = OpeningType.Closed;
                            Closed.Location = 0.0;
                            Floor1Wall[value].Openings = new List<Opening>();
                            Floor1Wall[value].Openings.Add(Closed);
                        }
                    }                  
                }            
            }

            List<int> indexWallOut = new List<int>();
            for (int i = 1; i < Ap_Wall.Count; i++)
            {
                List<int> index = new List<int>();
                for (int j = 0; j < Ap_Wall[i].Walls.Count; j++)
                {
                    if (Floor1Wall[Ap_Wall[i].Walls[j]].Openings[0].Type == OpeningType.Entrance)
                    {
                        index.Add(Ap_Wall[i].Walls[j]);
                    }
                }
                int targetindex = MaxRandom(index);
                indexWallOut.Add(index[targetindex]);
                for (int j = 0; j < index.Count; j++)
                {
                    if (j != targetindex)
                    {
                        Floor1Wall[index[j]].Openings[0].Type = OpeningType.Closed;
                    }
                }
            }

            List<int> inapartmentWal = new List<int>();
            for (int i = 1; i < Apartments_Floor_1.Count; i++)
            {
                int startRoom = 0;

                for (int j = 0; j < Apartments_Floor_1[i].Rooms.Count; j++)
                {
                    foreach (int value in Apartments_Floor_1[i].Rooms[j].Walls)
                    {
                        if (indexWallOut.Contains(value))
                        {
                            startRoom = j;
                        }
                    }
                }

                int ColRooms = Apartments_Floor_1[i].Rooms.Count;
                int[,] graf = new int[ColRooms, ColRooms];

                for (int j = 0; j < ColRooms; j++)
                {
                    foreach (int value in Apartments_Floor_1[i].Rooms[j].Walls)
                    {
                        for (int k = 0; k < ColRooms; k++)
                        {
                            if (Apartments_Floor_1[i].Rooms[k].Walls.Contains(value) && j != k)
                            {
                                graf[j, k] = 1;
                                //graf[k, j] = 1;
                            }
                        }
                    }
                }

                int[] Parent = BFS(graf, ColRooms, startRoom);

                for (int j = 0; j < ColRooms; j++)
                {

                    List<int> Wall = new List<int>();
                    foreach (int value in Apartments_Floor_1[i].Rooms[j].Walls)
                    {
                        if (Apartments_Floor_1[i].Rooms[Parent[j]].Walls.Contains(value))
                        {
                            Wall.Add(value);
                        }
                    }
                    if (Wall.Count != 0)
                    {
                        int index = Wall[MaxRandom(Wall)];
                        if (inapartmentWal.Contains(index) == false)
                        {
                            inapartmentWal.Add(index);
                        }
                    }                  
                }
            }         

            for (int i = 0; i < Floor1Wall.Count; i++)
            {
                if (inapartmentWal.Contains(i) == false && Floor1Wall[i].Openings[0].Type == OpeningType.Door)
                {
                    Floor1Wall[i].Openings[0].Type = OpeningType.inapartmentWall;
                }
                
            }             

            Type_Walls.Walls.AddRange(Floor1Wall);
            return Type_Walls.Walls;
        }      

        public static SaveInfo CreateData(FloorplanModel model, double h_level_floor, double DoorW, double WindowW, double DoorH, double WindowH, double WindowL, double Locat, int ColLevel)
        {
            double H_floor = h_level_floor;
            double Door_w = DoorW;
            double Window_w = WindowW;
            double Door_h = DoorH;
            double Window_h = WindowH;
            double Window_l = WindowL;
            double Loc = Locat;
            int Col_Level = ColLevel;

            SaveInfo InfoSave = new SaveInfo();
            InfoSave.Points = new List<Point>();
            InfoSave.myLines = new List<MyLine>();
            InfoSave.MyMesh = new MeshGeometry3D();
            InfoSave.myTriangle = new List<Triange>();
            InfoSave.myWalls = new List<Wall_3D>();

            if (MainWindow.flag_base_model == true && MainWindow.flagCount == false || MainWindow.flag_custom_model == true && MainWindow.flagCount == false)
            {
                List<Wall> Floor1Wall = FileUtils.Type_Walls1(model, Door_w);
                List<Point> FloorPoints = model.Points;
                for (int i = 0; i < model.Walls.Count; i++)
                {
                    List<Point3D> lockPoint = new List<Point3D>();
                    Point3D p1 = (new Point3D
                    {
                        X = FloorPoints[Floor1Wall[i].Start.Item1].X,
                        Y = FloorPoints[Floor1Wall[i].Start.Item1].Y,
                        Z = 0.0
                    });
                    Point3D p2 = (new Point3D
                    {
                        X = FloorPoints[Floor1Wall[i].End.Item1].X,
                        Y = FloorPoints[Floor1Wall[i].End.Item1].Y,
                        Z = 0.0
                    });
                    Point3D p3 = (new Point3D
                    {
                        X = FloorPoints[Floor1Wall[i].Start.Item1].X,
                        Y = FloorPoints[Floor1Wall[i].Start.Item1].Y,
                        Z = H_floor
                    });
                    Point3D p4 = (new Point3D
                    {
                        X = FloorPoints[Floor1Wall[i].End.Item1].X,
                        Y = FloorPoints[Floor1Wall[i].End.Item1].Y,
                        Z = H_floor
                    });

                    lockPoint.Add(p1);
                    lockPoint.Add(p2);
                    lockPoint.Add(p3);
                    lockPoint.Add(p4);

                    if (InfoSave.MyMesh.Positions.Contains(p1) == false && InfoSave.MyMesh.Positions.Contains(p2))
                    {
                        InfoSave.MyMesh.Positions.Add(p1);
                        InfoSave.MyMesh.Positions.Add(p3);
                    }
                    else if (InfoSave.MyMesh.Positions.Contains(p1) && InfoSave.MyMesh.Positions.Contains(p2) == false)
                    {
                        InfoSave.MyMesh.Positions.Add(p2);
                        InfoSave.MyMesh.Positions.Add(p4);
                    }
                    else if (InfoSave.MyMesh.Positions.Contains(p1) == false && InfoSave.MyMesh.Positions.Contains(p2) == false)
                    {
                        InfoSave.MyMesh.Positions.Add(p1);
                        InfoSave.MyMesh.Positions.Add(p2);
                        InfoSave.MyMesh.Positions.Add(p3);
                        InfoSave.MyMesh.Positions.Add(p4);                      
                    }                                                       
                    InfoSave.myTriangle.AddRange(CreateTriangle(InfoSave, lockPoint, OpeningType.Closed, null));
                }
                if (Col_Level > 1)
                {
                    InfoSave = Create_Level(InfoSave, Col_Level, H_floor);
                }
            }
            else if (MainWindow.flag_doors_windows == true && MainWindow.flagCount == true)
            {
                List<Wall> Floor1Wall = FileUtils.Type_Walls1(model, Door_w);
                List<Point> FloorPoints = model.Points;
                for (int i = 0; i < Floor1Wall.Count; i++)
                {
                    List<Point3D> lockPoint = new List<Point3D>();
                    if (Floor1Wall[i].Openings[0].Type == OpeningType.Door)
                    {
                        double x1 = FloorPoints[Floor1Wall[i].Start.Item1].X;
                        double y1 = FloorPoints[Floor1Wall[i].Start.Item1].Y;
                        double x2 = FloorPoints[Floor1Wall[i].End.Item1].X;
                        double y2 = FloorPoints[Floor1Wall[i].End.Item1].Y;
                        double L = Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));

                        if (L > Door_w)
                        {
                            List<double> point_for_triangle = new List<double>();
                            double K1 = (L * Loc - Door_w / 2) / L;
                            double K2 = (L * Loc + Door_w / 2) / L;
                            double new_Point_x1 = x1 + (x2 - x1) * K1;
                            double new_Point_y1 = y1 + (y2 - y1) * K1;
                            double new_Point_x2 = x1 + (x2 - x1) * K2;
                            double new_Point_y2 = y1 + (y2 - y1) * K2;

                            point_for_triangle.Add(new_Point_x1);
                            point_for_triangle.Add(new_Point_x2);
                            point_for_triangle.Add(new_Point_y1);
                            point_for_triangle.Add(new_Point_y2);
                            point_for_triangle.Add(x1);
                            point_for_triangle.Add(y1);
                            point_for_triangle.Add(L);
                            point_for_triangle.Add(H_floor);
                            point_for_triangle.Add(Door_h);

                            Point3D p1 = (new Point3D { X = x1, Y = y1, Z = 0.0 });                           
                            Point3D p2 = (new Point3D { X = x2, Y = y2, Z = 0.0 });                        
                            Point3D p1door = (new Point3D { X = new_Point_x1, Y = new_Point_y1, Z = 0.0 });                          
                            Point3D p2door = (new Point3D { X = new_Point_x2, Y = new_Point_y2, Z = 0.0 });
                            Point3D p1h_floor = (new Point3D { X = x1, Y = y1, Z = H_floor });
                            Point3D p2h_floor = (new Point3D { X = x2, Y = y2, Z = H_floor });
                            Point3D p1h_door = (new Point3D { X = new_Point_x1, Y = new_Point_y1, Z = Door_h });
                            Point3D p2h_door = (new Point3D { X = new_Point_x2, Y = new_Point_y2, Z = Door_h });

                            Point p1door2D = (new Point { X = new_Point_x1, Y = new_Point_y1 });
                            Point p2door2D = (new Point { X = new_Point_x2, Y = new_Point_y2 });

                            MyLine line = new MyLine();
                            InfoSave.Points.Add(p1door2D);
                            InfoSave.Points.Add(p2door2D);
                            line.Start = InfoSave.Points.IndexOf(p1door2D);
                            line.End = InfoSave.Points.IndexOf(p2door2D);
                            line.Type = OpeningType.Door;
                            InfoSave.myLines.Add(line);

                            lockPoint.Add(p1);
                            lockPoint.Add(p2);
                            lockPoint.Add(p1h_floor);
                            lockPoint.Add(p2h_floor);
                            lockPoint.Add(p1door);
                            lockPoint.Add(p2door);
                            lockPoint.Add(p1h_door);
                            lockPoint.Add(p2h_door);                           

                            if (InfoSave.MyMesh.Positions.Contains(p1) == false && InfoSave.MyMesh.Positions.Contains(p2))
                            {
                                InfoSave.MyMesh.Positions.Add(p1);
                                InfoSave.MyMesh.Positions.Add(p1h_floor);
                                InfoSave.MyMesh.Positions.Add(p1door);
                                InfoSave.MyMesh.Positions.Add(p2door);
                                InfoSave.MyMesh.Positions.Add(p1h_door);
                                InfoSave.MyMesh.Positions.Add(p2h_door);                               
                            }
                            else if (InfoSave.MyMesh.Positions.Contains(p1) && InfoSave.MyMesh.Positions.Contains(p2) == false)
                            {
                                InfoSave.MyMesh.Positions.Add(p2);
                                InfoSave.MyMesh.Positions.Add(p2h_floor);
                                InfoSave.MyMesh.Positions.Add(p1door);
                                InfoSave.MyMesh.Positions.Add(p2door);
                                InfoSave.MyMesh.Positions.Add(p1h_door);
                                InfoSave.MyMesh.Positions.Add(p2h_door);
                            }
                            else if (InfoSave.MyMesh.Positions.Contains(p1) == false && InfoSave.MyMesh.Positions.Contains(p2) == false)
                            {
                                InfoSave.MyMesh.Positions.Add(p1);
                                InfoSave.MyMesh.Positions.Add(p2);
                                InfoSave.MyMesh.Positions.Add(p1h_floor);
                                InfoSave.MyMesh.Positions.Add(p2h_floor);
                                InfoSave.MyMesh.Positions.Add(p1door);
                                InfoSave.MyMesh.Positions.Add(p2door);
                                InfoSave.MyMesh.Positions.Add(p1h_door);
                                InfoSave.MyMesh.Positions.Add(p2h_door);
                            }
                            else
                            {
                                InfoSave.MyMesh.Positions.Add(p1door);
                                InfoSave.MyMesh.Positions.Add(p2door);
                                InfoSave.MyMesh.Positions.Add(p1h_door);
                                InfoSave.MyMesh.Positions.Add(p2h_door);
                            }
                            InfoSave.myTriangle.AddRange(CreateTriangle(InfoSave, lockPoint, OpeningType.Door, point_for_triangle));
                        }

                        else
                        {
                            Point3D p1 = (new Point3D
                            {
                                X = FloorPoints[Floor1Wall[i].Start.Item1].X,
                                Y = FloorPoints[Floor1Wall[i].Start.Item1].Y,
                                Z = 0.0
                            });
                            Point3D p2 = (new Point3D
                            {
                                X = FloorPoints[Floor1Wall[i].End.Item1].X,
                                Y = FloorPoints[Floor1Wall[i].End.Item1].Y,
                                Z = 0.0
                            });
                            Point3D p3 = (new Point3D
                            {
                                X = FloorPoints[Floor1Wall[i].Start.Item1].X,
                                Y = FloorPoints[Floor1Wall[i].Start.Item1].Y,
                                Z = H_floor
                            });
                            Point3D p4 = (new Point3D
                            {
                                X = FloorPoints[Floor1Wall[i].End.Item1].X,
                                Y = FloorPoints[Floor1Wall[i].End.Item1].Y,
                                Z = H_floor
                            });

                            lockPoint.Add(p1);
                            lockPoint.Add(p2);
                            lockPoint.Add(p3);
                            lockPoint.Add(p4);

                            if (InfoSave.MyMesh.Positions.Contains(p1) == false && InfoSave.MyMesh.Positions.Contains(p2))
                            {
                                InfoSave.MyMesh.Positions.Add(p1);
                                InfoSave.MyMesh.Positions.Add(p3);
                            }
                            else if (InfoSave.MyMesh.Positions.Contains(p1) && InfoSave.MyMesh.Positions.Contains(p2) == false)
                            {
                                InfoSave.MyMesh.Positions.Add(p2);
                                InfoSave.MyMesh.Positions.Add(p4);
                            }
                            else if (InfoSave.MyMesh.Positions.Contains(p1) == false && InfoSave.MyMesh.Positions.Contains(p2) == false)
                            {
                                InfoSave.MyMesh.Positions.Add(p1);
                                InfoSave.MyMesh.Positions.Add(p2);
                                InfoSave.MyMesh.Positions.Add(p3);
                                InfoSave.MyMesh.Positions.Add(p4);
                            }                
                            InfoSave.myTriangle.AddRange(CreateTriangle(InfoSave, lockPoint, OpeningType.Closed, null));
                        }
                    }

                    else if (Floor1Wall[i].Openings[0].Type == OpeningType.Window)
                    {
                        double x1 = FloorPoints[Floor1Wall[i].Start.Item1].X;
                        double y1 = FloorPoints[Floor1Wall[i].Start.Item1].Y;
                        double x2 = FloorPoints[Floor1Wall[i].End.Item1].X;
                        double y2 = FloorPoints[Floor1Wall[i].End.Item1].Y;
                        double L = Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
                        if (L > Window_w)
                        {
                            List<double> point_for_triangle = new List<double>();
                            double K1 = (L * Loc - Window_w / 2) / L;
                            double K2 = (L * Loc + Window_w / 2) / L;
                            double new_Point_x1 = x1 + (x2 - x1) * K1;
                            double new_Point_y1 = y1 + (y2 - y1) * K1;
                            double new_Point_x2 = x1 + (x2 - x1) * K2;
                            double new_Point_y2 = y1 + (y2 - y1) * K2;

                            point_for_triangle.Add(new_Point_x1);
                            point_for_triangle.Add(new_Point_x2);
                            point_for_triangle.Add(new_Point_y1);
                            point_for_triangle.Add(new_Point_y2);
                            point_for_triangle.Add(x1);
                            point_for_triangle.Add(y1);
                            point_for_triangle.Add(L);
                            point_for_triangle.Add(H_floor);
                            point_for_triangle.Add(Window_l);
                            point_for_triangle.Add(Window_h);

                            Point3D p1 = (new Point3D { X = x1, Y = y1, Z = 0.0 });
                            Point3D p2 = (new Point3D { X = x2, Y = y2, Z = 0.0 });
                            Point3D p1window = (new Point3D { X = new_Point_x1, Y = new_Point_y1, Z = Window_l });
                            Point3D p2window = (new Point3D { X = new_Point_x2, Y = new_Point_y2, Z = Window_l });
                            Point3D p1h_floor = (new Point3D { X = x1, Y = y1, Z = H_floor });
                            Point3D p2h_floor = (new Point3D { X = x2, Y = y2, Z = H_floor });
                            Point3D p1h_window = (new Point3D { X = new_Point_x1, Y = new_Point_y1, Z = Window_l + Window_h });
                            Point3D p2h_window = (new Point3D { X = new_Point_x2, Y = new_Point_y2, Z = Window_l + Window_h });

                            MyLine line = new MyLine();
                            Point p1window2D = (new Point { X = new_Point_x1, Y = new_Point_y1 });
                            Point p2window2D = (new Point { X = new_Point_x2, Y = new_Point_y2 });                            
                            InfoSave.Points.Add(p1window2D);
                            InfoSave.Points.Add(p2window2D);
                            line.Start = InfoSave.Points.IndexOf(p1window2D);
                            line.End = InfoSave.Points.IndexOf(p2window2D);
                            line.Type = OpeningType.Window;
                            InfoSave.myLines.Add(line);
                            
                            lockPoint.Add(p1);
                            lockPoint.Add(p2);
                            lockPoint.Add(p1h_floor);
                            lockPoint.Add(p2h_floor);
                            lockPoint.Add(p1window);
                            lockPoint.Add(p2window);                          
                            lockPoint.Add(p1h_window);
                            lockPoint.Add(p2h_window);                        

                            if (InfoSave.MyMesh.Positions.Contains(p1) == false && InfoSave.MyMesh.Positions.Contains(p2))
                            {
                                InfoSave.MyMesh.Positions.Add(p1);
                                InfoSave.MyMesh.Positions.Add(p1h_floor);
                                InfoSave.MyMesh.Positions.Add(p1window);
                                InfoSave.MyMesh.Positions.Add(p2window);                               
                                InfoSave.MyMesh.Positions.Add(p1h_window);
                                InfoSave.MyMesh.Positions.Add(p2h_window);
                            }
                            else if (InfoSave.MyMesh.Positions.Contains(p1) && InfoSave.MyMesh.Positions.Contains(p2) == false)
                            {
                                InfoSave.MyMesh.Positions.Add(p2);
                                InfoSave.MyMesh.Positions.Add(p2h_floor);
                                InfoSave.MyMesh.Positions.Add(p1window);
                                InfoSave.MyMesh.Positions.Add(p2window);                             
                                InfoSave.MyMesh.Positions.Add(p1h_window);
                                InfoSave.MyMesh.Positions.Add(p2h_window);
                            }
                            else if (InfoSave.MyMesh.Positions.Contains(p1) == false && InfoSave.MyMesh.Positions.Contains(p2) == false)
                            {
                                InfoSave.MyMesh.Positions.Add(p1);
                                InfoSave.MyMesh.Positions.Add(p2);
                                InfoSave.MyMesh.Positions.Add(p1h_floor);
                                InfoSave.MyMesh.Positions.Add(p2h_floor);
                                InfoSave.MyMesh.Positions.Add(p1window);
                                InfoSave.MyMesh.Positions.Add(p2window);                             
                                InfoSave.MyMesh.Positions.Add(p1h_window);
                                InfoSave.MyMesh.Positions.Add(p2h_window);
                            }
                            else
                            {
                                InfoSave.MyMesh.Positions.Add(p1window);
                                InfoSave.MyMesh.Positions.Add(p2window);
                                InfoSave.MyMesh.Positions.Add(p1h_window);
                                InfoSave.MyMesh.Positions.Add(p2h_window);
                            }
                            InfoSave.myTriangle.AddRange(CreateTriangle(InfoSave, lockPoint, OpeningType.Window, point_for_triangle));
                            
                        }

                        else
                        {
                            Point3D p1 = (new Point3D
                            {
                                X = FloorPoints[Floor1Wall[i].Start.Item1].X,
                                Y = FloorPoints[Floor1Wall[i].Start.Item1].Y,
                                Z = 0.0
                            });
                            Point3D p2 = (new Point3D
                            {
                                X = FloorPoints[Floor1Wall[i].End.Item1].X,
                                Y = FloorPoints[Floor1Wall[i].End.Item1].Y,
                                Z = 0.0
                            });
                            Point3D p3 = (new Point3D
                            {
                                X = FloorPoints[Floor1Wall[i].Start.Item1].X,
                                Y = FloorPoints[Floor1Wall[i].Start.Item1].Y,
                                Z = H_floor
                            });
                            Point3D p4 = (new Point3D
                            {
                                X = FloorPoints[Floor1Wall[i].End.Item1].X,
                                Y = FloorPoints[Floor1Wall[i].End.Item1].Y,
                                Z = H_floor
                            });

                            lockPoint.Add(p1);
                            lockPoint.Add(p2);
                            lockPoint.Add(p3);
                            lockPoint.Add(p4);

                            if (InfoSave.MyMesh.Positions.Contains(p1) == false && InfoSave.MyMesh.Positions.Contains(p2))
                            {
                                InfoSave.MyMesh.Positions.Add(p1);
                                InfoSave.MyMesh.Positions.Add(p3);
                            }
                            else if (InfoSave.MyMesh.Positions.Contains(p1) && InfoSave.MyMesh.Positions.Contains(p2) == false)
                            {
                                InfoSave.MyMesh.Positions.Add(p2);
                                InfoSave.MyMesh.Positions.Add(p4);
                            }
                            else if (InfoSave.MyMesh.Positions.Contains(p1) == false && InfoSave.MyMesh.Positions.Contains(p2) == false)
                            {
                                InfoSave.MyMesh.Positions.Add(p1);
                                InfoSave.MyMesh.Positions.Add(p2);
                                InfoSave.MyMesh.Positions.Add(p3);
                                InfoSave.MyMesh.Positions.Add(p4);
                            }                      
                            InfoSave.myTriangle.AddRange(CreateTriangle(InfoSave, lockPoint, OpeningType.Closed, null));
                        }
                    }

                    else if (Floor1Wall[i].Openings[0].Type == OpeningType.Entrance)
                    {
                        double x1 = FloorPoints[Floor1Wall[i].Start.Item1].X;
                        double y1 = FloorPoints[Floor1Wall[i].Start.Item1].Y;
                        double x2 = FloorPoints[Floor1Wall[i].End.Item1].X;
                        double y2 = FloorPoints[Floor1Wall[i].End.Item1].Y;
                        double L = Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));

                        if (L > Door_w)
                        {
                            List<double> point_for_triangle = new List<double>();
                            double K1 = (L * Loc - Door_w / 2) / L;
                            double K2 = (L * Loc + Door_w / 2) / L;
                            double new_Point_x1 = x1 + (x2 - x1) * K1;
                            double new_Point_y1 = y1 + (y2 - y1) * K1;
                            double new_Point_x2 = x1 + (x2 - x1) * K2;
                            double new_Point_y2 = y1 + (y2 - y1) * K2;

                            point_for_triangle.Add(new_Point_x1);
                            point_for_triangle.Add(new_Point_x2);
                            point_for_triangle.Add(new_Point_y1);
                            point_for_triangle.Add(new_Point_y2);
                            point_for_triangle.Add(x1);
                            point_for_triangle.Add(y1);
                            point_for_triangle.Add(L);
                            point_for_triangle.Add(H_floor);
                            point_for_triangle.Add(Door_h);

                            Point3D p1 = (new Point3D { X = x1, Y = y1, Z = 0.0 });
                            Point3D p2 = (new Point3D { X = x2, Y = y2, Z = 0.0 });
                            Point3D p1door = (new Point3D { X = new_Point_x1, Y = new_Point_y1, Z = 0.0 });
                            Point3D p2door = (new Point3D { X = new_Point_x2, Y = new_Point_y2, Z = 0.0 });
                            Point3D p1h_floor = (new Point3D { X = x1, Y = y1, Z = H_floor });
                            Point3D p2h_floor = (new Point3D { X = x2, Y = y2, Z = H_floor });
                            Point3D p1h_door = (new Point3D { X = new_Point_x1, Y = new_Point_y1, Z = Door_h });
                            Point3D p2h_door = (new Point3D { X = new_Point_x2, Y = new_Point_y2, Z = Door_h });

                            Point p1door2D = (new Point { X = new_Point_x1, Y = new_Point_y1 });
                            Point p2door2D = (new Point { X = new_Point_x2, Y = new_Point_y2 });
                            Point p12D = (new Point { X = x1, Y = y1 });
                            Point p22D = (new Point { X = x2, Y = y2 });

                            MyLine line1 = new MyLine();
                            InfoSave.Points.Add(p12D);
                            InfoSave.Points.Add(p22D);
                            line1.Start = InfoSave.Points.IndexOf(p12D);
                            line1.End = InfoSave.Points.IndexOf(p22D);
                            line1.Type = OpeningType.Closed;
                            InfoSave.myLines.Add(line1);

                            MyLine line = new MyLine();
                            InfoSave.Points.Add(p1door2D);
                            InfoSave.Points.Add(p2door2D);
                            line.Start = InfoSave.Points.IndexOf(p1door2D);
                            line.End = InfoSave.Points.IndexOf(p2door2D);
                            line.Type = OpeningType.Entrance;
                            InfoSave.myLines.Add(line);

                            lockPoint.Add(p1);
                            lockPoint.Add(p2);
                            lockPoint.Add(p1h_floor);
                            lockPoint.Add(p2h_floor);
                            lockPoint.Add(p1door);
                            lockPoint.Add(p2door);
                            lockPoint.Add(p1h_door);
                            lockPoint.Add(p2h_door);

                            if (InfoSave.MyMesh.Positions.Contains(p1) == false && InfoSave.MyMesh.Positions.Contains(p2))
                            {
                                InfoSave.MyMesh.Positions.Add(p1);
                                InfoSave.MyMesh.Positions.Add(p1h_floor);
                                InfoSave.MyMesh.Positions.Add(p1door);
                                InfoSave.MyMesh.Positions.Add(p2door);
                                InfoSave.MyMesh.Positions.Add(p1h_door);
                                InfoSave.MyMesh.Positions.Add(p2h_door);
                            }
                            else if (InfoSave.MyMesh.Positions.Contains(p1) && InfoSave.MyMesh.Positions.Contains(p2) == false)
                            {
                                InfoSave.MyMesh.Positions.Add(p2);
                                InfoSave.MyMesh.Positions.Add(p2h_floor);
                                InfoSave.MyMesh.Positions.Add(p1door);
                                InfoSave.MyMesh.Positions.Add(p2door);
                                InfoSave.MyMesh.Positions.Add(p1h_door);
                                InfoSave.MyMesh.Positions.Add(p2h_door);
                            }
                            else if (InfoSave.MyMesh.Positions.Contains(p1) == false && InfoSave.MyMesh.Positions.Contains(p2) == false)
                            {
                                InfoSave.MyMesh.Positions.Add(p1);
                                InfoSave.MyMesh.Positions.Add(p2);
                                InfoSave.MyMesh.Positions.Add(p1h_floor);
                                InfoSave.MyMesh.Positions.Add(p2h_floor);
                                InfoSave.MyMesh.Positions.Add(p1door);
                                InfoSave.MyMesh.Positions.Add(p2door);
                                InfoSave.MyMesh.Positions.Add(p1h_door);
                                InfoSave.MyMesh.Positions.Add(p2h_door);
                            }
                            else
                            {
                                InfoSave.MyMesh.Positions.Add(p1door);
                                InfoSave.MyMesh.Positions.Add(p2door);
                                InfoSave.MyMesh.Positions.Add(p1h_door);
                                InfoSave.MyMesh.Positions.Add(p2h_door);
                            }
                            InfoSave.myTriangle.AddRange(CreateTriangle(InfoSave, lockPoint, OpeningType.Door, point_for_triangle));
                        }

                        else
                        {
                            Point3D p1 = (new Point3D
                            {
                                X = FloorPoints[Floor1Wall[i].Start.Item1].X,
                                Y = FloorPoints[Floor1Wall[i].Start.Item1].Y,
                                Z = 0.0
                            });
                            Point3D p2 = (new Point3D
                            {
                                X = FloorPoints[Floor1Wall[i].End.Item1].X,
                                Y = FloorPoints[Floor1Wall[i].End.Item1].Y,
                                Z = 0.0
                            });
                            Point3D p3 = (new Point3D
                            {
                                X = FloorPoints[Floor1Wall[i].Start.Item1].X,
                                Y = FloorPoints[Floor1Wall[i].Start.Item1].Y,
                                Z = H_floor
                            });
                            Point3D p4 = (new Point3D
                            {
                                X = FloorPoints[Floor1Wall[i].End.Item1].X,
                                Y = FloorPoints[Floor1Wall[i].End.Item1].Y,
                                Z = H_floor
                            });

                            lockPoint.Add(p1);
                            lockPoint.Add(p2);
                            lockPoint.Add(p3);
                            lockPoint.Add(p4);

                            if (InfoSave.MyMesh.Positions.Contains(p1) == false && InfoSave.MyMesh.Positions.Contains(p2))
                            {
                                InfoSave.MyMesh.Positions.Add(p1);
                                InfoSave.MyMesh.Positions.Add(p3);
                            }
                            else if (InfoSave.MyMesh.Positions.Contains(p1) && InfoSave.MyMesh.Positions.Contains(p2) == false)
                            {
                                InfoSave.MyMesh.Positions.Add(p2);
                                InfoSave.MyMesh.Positions.Add(p4);
                            }
                            else if (InfoSave.MyMesh.Positions.Contains(p1) == false && InfoSave.MyMesh.Positions.Contains(p2) == false)
                            {
                                InfoSave.MyMesh.Positions.Add(p1);
                                InfoSave.MyMesh.Positions.Add(p2);
                                InfoSave.MyMesh.Positions.Add(p3);
                                InfoSave.MyMesh.Positions.Add(p4);
                            }
                            InfoSave.myTriangle.AddRange(CreateTriangle(InfoSave, lockPoint, OpeningType.Closed, null));
                        }
                    }

                    else if (Floor1Wall[i].Openings[0].Type == OpeningType.inapartmentWall)
                    {
                        double x1 = FloorPoints[Floor1Wall[i].Start.Item1].X;
                        double y1 = FloorPoints[Floor1Wall[i].Start.Item1].Y;
                        double x2 = FloorPoints[Floor1Wall[i].End.Item1].X;
                        double y2 = FloorPoints[Floor1Wall[i].End.Item1].Y;
                        Point3D p1 = (new Point3D { X = x1, Y = y1, Z = 0.0 });
                        Point3D p2 = (new Point3D { X = x2, Y = y2, Z = 0.0 });
                        Point3D p3 = (new Point3D { X = x1, Y = y1, Z = H_floor });
                        Point3D p4 = (new Point3D { X = x2, Y = y2, Z = H_floor });                     

                        lockPoint.Add(p1);
                        lockPoint.Add(p2);
                        lockPoint.Add(p3);
                        lockPoint.Add(p4);

                        if (InfoSave.MyMesh.Positions.Contains(p1) == false && InfoSave.MyMesh.Positions.Contains(p2))
                        {
                            InfoSave.MyMesh.Positions.Add(p1);
                            InfoSave.MyMesh.Positions.Add(p3);
                        }
                        else if (InfoSave.MyMesh.Positions.Contains(p1) && InfoSave.MyMesh.Positions.Contains(p2) == false)
                        {
                            InfoSave.MyMesh.Positions.Add(p2);
                            InfoSave.MyMesh.Positions.Add(p4);
                        }
                        else if (InfoSave.MyMesh.Positions.Contains(p1) == false && InfoSave.MyMesh.Positions.Contains(p2) == false)
                        {
                            InfoSave.MyMesh.Positions.Add(p1);
                            InfoSave.MyMesh.Positions.Add(p2);
                            InfoSave.MyMesh.Positions.Add(p3);
                            InfoSave.MyMesh.Positions.Add(p4);
                        }
                        InfoSave.myTriangle.AddRange(CreateTriangle(InfoSave, lockPoint, OpeningType.Closed, null));
                    }

                    else if (Floor1Wall[i].Openings[0].Type == OpeningType.Closed)
                    {
                        double x1 = FloorPoints[Floor1Wall[i].Start.Item1].X;
                        double y1 = FloorPoints[Floor1Wall[i].Start.Item1].Y;
                        double x2 = FloorPoints[Floor1Wall[i].End.Item1].X;
                        double y2 = FloorPoints[Floor1Wall[i].End.Item1].Y;
                        Point3D p1 = (new Point3D { X = x1, Y = y1, Z = 0.0 });
                        Point3D p2 = (new Point3D { X = x2, Y = y2, Z = 0.0 });
                        Point3D p3 = (new Point3D { X = x1, Y = y1, Z = H_floor });
                        Point3D p4 = (new Point3D { X = x2, Y = y2, Z = H_floor });

                        MyLine line = new MyLine();
                        Point p1_2D = (new Point { X = x1, Y = y1 });
                        Point p2_2D = (new Point { X = x2, Y = y2 });
                        InfoSave.Points.Add(p1_2D);
                        InfoSave.Points.Add(p2_2D);
                        line.Start = InfoSave.Points.IndexOf(p1_2D);
                        line.End = InfoSave.Points.IndexOf(p2_2D);
                        line.Type = OpeningType.Closed;
                        InfoSave.myLines.Add(line);

                        lockPoint.Add(p1);
                        lockPoint.Add(p2);
                        lockPoint.Add(p3);
                        lockPoint.Add(p4);

                        if (InfoSave.MyMesh.Positions.Contains(p1) == false && InfoSave.MyMesh.Positions.Contains(p2))
                        {
                            InfoSave.MyMesh.Positions.Add(p1);
                            InfoSave.MyMesh.Positions.Add(p3);
                        }
                        else if (InfoSave.MyMesh.Positions.Contains(p1) && InfoSave.MyMesh.Positions.Contains(p2) == false)
                        {
                            InfoSave.MyMesh.Positions.Add(p2);
                            InfoSave.MyMesh.Positions.Add(p4);
                        }
                        else if (InfoSave.MyMesh.Positions.Contains(p1) == false && InfoSave.MyMesh.Positions.Contains(p2) == false)
                        {
                            InfoSave.MyMesh.Positions.Add(p1);
                            InfoSave.MyMesh.Positions.Add(p2);
                            InfoSave.MyMesh.Positions.Add(p3);
                            InfoSave.MyMesh.Positions.Add(p4);
                        }
                        InfoSave.myTriangle.AddRange(CreateTriangle(InfoSave, lockPoint, OpeningType.Closed, null));
                    }
                }
                if (Col_Level > 1)
                {
                    InfoSave = Create_Level(InfoSave, Col_Level, H_floor);
                }
            }          

            return InfoSave;
        }

        public static List<Triange> CreateTriangle(SaveInfo model, List<Point3D> modelPoint, OpeningType type, List<double> list)
        {
            int quantity = modelPoint.Count;
            List<Triange> myTriangle = new List<Triange>();

            Wall_3D myWall = new Wall_3D();
            myWall.myTriangle = new List<Triange>();
            Triange triange1 = new Triange();
            triange1.p1 = new pointTriangle();
            triange1.p2 = new pointTriangle();
            triange1.p3 = new pointTriangle();

            Triange triange2 = new Triange();
            triange2.p1 = new pointTriangle();
            triange2.p2 = new pointTriangle();
            triange2.p3 = new pointTriangle();

            Triange triange3 = new Triange();
            triange3.p1 = new pointTriangle();
            triange3.p2 = new pointTriangle();
            triange3.p3 = new pointTriangle();

            Triange triange4 = new Triange();
            triange4.p1 = new pointTriangle();
            triange4.p2 = new pointTriangle();
            triange4.p3 = new pointTriangle();

            Triange triange5 = new Triange();
            triange5.p1 = new pointTriangle();
            triange5.p2 = new pointTriangle();
            triange5.p3 = new pointTriangle();

            Triange triange6 = new Triange();
            triange6.p1 = new pointTriangle();
            triange6.p2 = new pointTriangle();
            triange6.p3 = new pointTriangle();

            Triange triange7 = new Triange();
            triange7.p1 = new pointTriangle();
            triange7.p2 = new pointTriangle();
            triange7.p3 = new pointTriangle();

            Triange triange8 = new Triange();
            triange8.p1 = new pointTriangle();
            triange8.p2 = new pointTriangle();
            triange8.p3 = new pointTriangle();

            switch (quantity)
            {
                case 4:
                    triange1.p1.indexPoint = (model.MyMesh.Positions.IndexOf(modelPoint[0]));
                    triange1.p1.index_vt = model.MyMesh.TextureCoordinates.Count + 1;
                    triange1.p2.indexPoint = (model.MyMesh.Positions.IndexOf(modelPoint[1]));
                    triange1.p2.index_vt = model.MyMesh.TextureCoordinates.Count + 2;
                    triange1.p3.indexPoint = (model.MyMesh.Positions.IndexOf(modelPoint[3]));
                    triange1.p3.index_vt = model.MyMesh.TextureCoordinates.Count + 4;

                    triange2.p1.indexPoint = (model.MyMesh.Positions.IndexOf(modelPoint[0]));
                    triange2.p1.index_vt = model.MyMesh.TextureCoordinates.Count + 1;
                    triange2.p2.indexPoint = (model.MyMesh.Positions.IndexOf(modelPoint[3]));
                    triange2.p2.index_vt = model.MyMesh.TextureCoordinates.Count + 4;
                    triange2.p3.indexPoint = (model.MyMesh.Positions.IndexOf(modelPoint[2]));
                    triange2.p3.index_vt = model.MyMesh.TextureCoordinates.Count + 3;

                    myTriangle.Add(triange1);
                    myTriangle.Add(triange2);

                    myWall.myTriangle.Add(triange1);
                    myWall.myTriangle.Add(triange2);

                    model.MyMesh.TextureCoordinates.Add(new Point(0, 0));
                    model.MyMesh.TextureCoordinates.Add(new Point(1, 0));
                    model.MyMesh.TextureCoordinates.Add(new Point(0, 1));
                    model.MyMesh.TextureCoordinates.Add(new Point(1, 1));

                    model.myWalls.Add(myWall);

                    break;

                case 8:
                    while(type == OpeningType.Door)
                    {
                        triange1.p1.indexPoint = (model.MyMesh.Positions.IndexOf(modelPoint[0]));
                        triange1.p1.index_vt = model.MyMesh.TextureCoordinates.Count + 1;
                        triange1.p2.indexPoint = (model.MyMesh.Positions.IndexOf(modelPoint[4]));
                        triange1.p2.index_vt = model.MyMesh.TextureCoordinates.Count + 5;
                        triange1.p3.indexPoint = (model.MyMesh.Positions.IndexOf(modelPoint[6]));
                        triange1.p3.index_vt = model.MyMesh.TextureCoordinates.Count + 7;

                        triange2.p1.indexPoint = (model.MyMesh.Positions.IndexOf(modelPoint[0]));
                        triange2.p1.index_vt = model.MyMesh.TextureCoordinates.Count + 1;
                        triange2.p2.indexPoint = (model.MyMesh.Positions.IndexOf(modelPoint[6]));
                        triange2.p2.index_vt = model.MyMesh.TextureCoordinates.Count + 7;
                        triange2.p3.indexPoint = (model.MyMesh.Positions.IndexOf(modelPoint[2]));
                        triange2.p3.index_vt = model.MyMesh.TextureCoordinates.Count + 3;

                        triange3.p1.indexPoint = (model.MyMesh.Positions.IndexOf(modelPoint[2]));
                        triange3.p1.index_vt = model.MyMesh.TextureCoordinates.Count + 3;
                        triange3.p2.indexPoint = (model.MyMesh.Positions.IndexOf(modelPoint[6]));
                        triange3.p2.index_vt = model.MyMesh.TextureCoordinates.Count + 7;
                        triange3.p3.indexPoint = (model.MyMesh.Positions.IndexOf(modelPoint[3]));
                        triange3.p3.index_vt = model.MyMesh.TextureCoordinates.Count + 4;

                        triange4.p1.indexPoint = (model.MyMesh.Positions.IndexOf(modelPoint[6]));
                        triange4.p1.index_vt = model.MyMesh.TextureCoordinates.Count + 7;
                        triange4.p2.indexPoint = (model.MyMesh.Positions.IndexOf(modelPoint[7]));
                        triange4.p2.index_vt = model.MyMesh.TextureCoordinates.Count + 8;
                        triange4.p3.indexPoint = (model.MyMesh.Positions.IndexOf(modelPoint[3]));
                        triange4.p3.index_vt = model.MyMesh.TextureCoordinates.Count + 4;

                        triange5.p1.indexPoint = (model.MyMesh.Positions.IndexOf(modelPoint[7]));
                        triange5.p1.index_vt = model.MyMesh.TextureCoordinates.Count + 8;
                        triange5.p2.indexPoint = (model.MyMesh.Positions.IndexOf(modelPoint[1]));
                        triange5.p2.index_vt = model.MyMesh.TextureCoordinates.Count + 2;
                        triange5.p3.indexPoint = (model.MyMesh.Positions.IndexOf(modelPoint[3]));
                        triange5.p3.index_vt = model.MyMesh.TextureCoordinates.Count + 4;

                        triange6.p1.indexPoint = (model.MyMesh.Positions.IndexOf(modelPoint[5]));
                        triange6.p1.index_vt = model.MyMesh.TextureCoordinates.Count + 6;
                        triange6.p2.indexPoint = (model.MyMesh.Positions.IndexOf(modelPoint[1]));
                        triange6.p2.index_vt = model.MyMesh.TextureCoordinates.Count + 2;
                        triange6.p3.indexPoint = (model.MyMesh.Positions.IndexOf(modelPoint[7]));
                        triange6.p3.index_vt = model.MyMesh.TextureCoordinates.Count + 8;

                        myTriangle.Add(triange1);
                        myTriangle.Add(triange2);
                        myTriangle.Add(triange3);
                        myTriangle.Add(triange4);
                        myTriangle.Add(triange5);
                        myTriangle.Add(triange6);

                        myWall.myTriangle.Add(triange1);
                        myWall.myTriangle.Add(triange2);
                        myWall.myTriangle.Add(triange3);
                        myWall.myTriangle.Add(triange4);
                        myWall.myTriangle.Add(triange5);
                        myWall.myTriangle.Add(triange6);

                        model.MyMesh.TextureCoordinates.Add(new Point(0, 0));
                        model.MyMesh.TextureCoordinates.Add(new Point(1, 0));
                        model.MyMesh.TextureCoordinates.Add(new Point(0, 1));
                        model.MyMesh.TextureCoordinates.Add(new Point(1, 1));

                        model.MyMesh.TextureCoordinates.Add(new Point(Math.Sqrt(Math.Pow((list[0] - list[4]), 2) + Math.Pow((list[2] - list[5]), 2)) * 1 / list[6], 0));
                        model.MyMesh.TextureCoordinates.Add(new Point(Math.Sqrt(Math.Pow((list[1] - list[4]), 2) + Math.Pow((list[3] - list[5]), 2)) * 1 / list[6], 0));
                        model.MyMesh.TextureCoordinates.Add(new Point(Math.Sqrt(Math.Pow((list[0] - list[4]), 2) + Math.Pow((list[2] - list[5]), 2)) * 1 / list[6],
                            list[8] * 1 / list[7]));
                        model.MyMesh.TextureCoordinates.Add(new Point(Math.Sqrt(Math.Pow((list[1] - list[4]), 2) + Math.Pow((list[3] - list[5]), 2)) * 1 / list[6],
                            list[8] * 1 / list[7]));

                        model.myWalls.Add(myWall);

                        model.MyMesh.TriangleIndices.Add(model.MyMesh.Positions.IndexOf(modelPoint[0]));
                        model.MyMesh.TriangleIndices.Add(model.MyMesh.Positions.IndexOf(modelPoint[4]));
                        model.MyMesh.TriangleIndices.Add(model.MyMesh.Positions.IndexOf(modelPoint[6]));

                        model.MyMesh.TriangleIndices.Add(model.MyMesh.Positions.IndexOf(modelPoint[0]));
                        model.MyMesh.TriangleIndices.Add(model.MyMesh.Positions.IndexOf(modelPoint[6]));
                        model.MyMesh.TriangleIndices.Add(model.MyMesh.Positions.IndexOf(modelPoint[2]));

                        model.MyMesh.TriangleIndices.Add(model.MyMesh.Positions.IndexOf(modelPoint[2]));
                        model.MyMesh.TriangleIndices.Add(model.MyMesh.Positions.IndexOf(modelPoint[6]));
                        model.MyMesh.TriangleIndices.Add(model.MyMesh.Positions.IndexOf(modelPoint[3]));

                        model.MyMesh.TriangleIndices.Add(model.MyMesh.Positions.IndexOf(modelPoint[6]));
                        model.MyMesh.TriangleIndices.Add(model.MyMesh.Positions.IndexOf(modelPoint[7]));
                        model.MyMesh.TriangleIndices.Add(model.MyMesh.Positions.IndexOf(modelPoint[3]));

                        model.MyMesh.TriangleIndices.Add(model.MyMesh.Positions.IndexOf(modelPoint[7]));
                        model.MyMesh.TriangleIndices.Add(model.MyMesh.Positions.IndexOf(modelPoint[1]));
                        model.MyMesh.TriangleIndices.Add(model.MyMesh.Positions.IndexOf(modelPoint[3]));

                        model.MyMesh.TriangleIndices.Add(model.MyMesh.Positions.IndexOf(modelPoint[5]));
                        model.MyMesh.TriangleIndices.Add(model.MyMesh.Positions.IndexOf(modelPoint[1]));
                        model.MyMesh.TriangleIndices.Add(model.MyMesh.Positions.IndexOf(modelPoint[7]));


                        break;
                    }
                    while(type == OpeningType.Window)
                    {
                        triange1.p1.indexPoint = (model.MyMesh.Positions.IndexOf(modelPoint[0]));
                        triange1.p1.index_vt = model.MyMesh.TextureCoordinates.Count + 1;
                        triange1.p2.indexPoint = (model.MyMesh.Positions.IndexOf(modelPoint[4]));
                        triange1.p2.index_vt = model.MyMesh.TextureCoordinates.Count + 5;
                        triange1.p3.indexPoint = (model.MyMesh.Positions.IndexOf(modelPoint[2]));
                        triange1.p3.index_vt = model.MyMesh.TextureCoordinates.Count + 3;

                        triange2.p1.indexPoint = (model.MyMesh.Positions.IndexOf(modelPoint[2]));
                        triange2.p1.index_vt = model.MyMesh.TextureCoordinates.Count + 3;
                        triange2.p2.indexPoint = (model.MyMesh.Positions.IndexOf(modelPoint[4]));
                        triange2.p2.index_vt = model.MyMesh.TextureCoordinates.Count + 5;
                        triange2.p3.indexPoint = (model.MyMesh.Positions.IndexOf(modelPoint[6]));
                        triange2.p3.index_vt = model.MyMesh.TextureCoordinates.Count + 7;

                        triange3.p1.indexPoint = (model.MyMesh.Positions.IndexOf(modelPoint[2]));
                        triange3.p1.index_vt = model.MyMesh.TextureCoordinates.Count + 3;
                        triange3.p2.indexPoint = (model.MyMesh.Positions.IndexOf(modelPoint[6]));
                        triange3.p2.index_vt = model.MyMesh.TextureCoordinates.Count + 7;
                        triange3.p3.indexPoint = (model.MyMesh.Positions.IndexOf(modelPoint[3]));
                        triange3.p3.index_vt = model.MyMesh.TextureCoordinates.Count + 4;

                        triange4.p1.indexPoint = (model.MyMesh.Positions.IndexOf(modelPoint[6]));
                        triange4.p1.index_vt = model.MyMesh.TextureCoordinates.Count + 7;
                        triange4.p2.indexPoint = (model.MyMesh.Positions.IndexOf(modelPoint[7]));
                        triange4.p2.index_vt = model.MyMesh.TextureCoordinates.Count + 8;
                        triange4.p3.indexPoint = (model.MyMesh.Positions.IndexOf(modelPoint[3]));
                        triange4.p3.index_vt = model.MyMesh.TextureCoordinates.Count + 4;

                        triange5.p1.indexPoint = (model.MyMesh.Positions.IndexOf(modelPoint[3]));
                        triange5.p1.index_vt = model.MyMesh.TextureCoordinates.Count + 4;
                        triange5.p2.indexPoint = (model.MyMesh.Positions.IndexOf(modelPoint[7]));
                        triange5.p2.index_vt = model.MyMesh.TextureCoordinates.Count + 8;
                        triange5.p3.indexPoint = (model.MyMesh.Positions.IndexOf(modelPoint[1]));
                        triange5.p3.index_vt = model.MyMesh.TextureCoordinates.Count + 2;

                        triange6.p1.indexPoint = (model.MyMesh.Positions.IndexOf(modelPoint[1]));
                        triange6.p1.index_vt = model.MyMesh.TextureCoordinates.Count + 2;
                        triange6.p2.indexPoint = (model.MyMesh.Positions.IndexOf(modelPoint[7]));
                        triange6.p2.index_vt = model.MyMesh.TextureCoordinates.Count + 8;
                        triange6.p3.indexPoint = (model.MyMesh.Positions.IndexOf(modelPoint[5]));
                        triange6.p3.index_vt = model.MyMesh.TextureCoordinates.Count + 6;

                        triange7.p1.indexPoint = (model.MyMesh.Positions.IndexOf(modelPoint[0]));
                        triange7.p1.index_vt = model.MyMesh.TextureCoordinates.Count + 1;
                        triange7.p2.indexPoint = (model.MyMesh.Positions.IndexOf(modelPoint[1]));
                        triange7.p2.index_vt = model.MyMesh.TextureCoordinates.Count + 2;
                        triange7.p3.indexPoint = (model.MyMesh.Positions.IndexOf(modelPoint[5]));
                        triange7.p3.index_vt = model.MyMesh.TextureCoordinates.Count + 6;

                        triange8.p1.indexPoint = (model.MyMesh.Positions.IndexOf(modelPoint[0]));
                        triange8.p1.index_vt = model.MyMesh.TextureCoordinates.Count + 1;
                        triange8.p2.indexPoint = (model.MyMesh.Positions.IndexOf(modelPoint[5]));
                        triange8.p2.index_vt = model.MyMesh.TextureCoordinates.Count + 6;
                        triange8.p3.indexPoint = (model.MyMesh.Positions.IndexOf(modelPoint[4]));
                        triange8.p3.index_vt = model.MyMesh.TextureCoordinates.Count + 5;

                        myTriangle.Add(triange1);
                        myTriangle.Add(triange2);
                        myTriangle.Add(triange3);
                        myTriangle.Add(triange4);
                        myTriangle.Add(triange5);
                        myTriangle.Add(triange6);
                        myTriangle.Add(triange7);
                        myTriangle.Add(triange8);

                        myWall.myTriangle.Add(triange1);
                        myWall.myTriangle.Add(triange2);
                        myWall.myTriangle.Add(triange3);
                        myWall.myTriangle.Add(triange4);
                        myWall.myTriangle.Add(triange5);
                        myWall.myTriangle.Add(triange6);
                        myWall.myTriangle.Add(triange7);
                        myWall.myTriangle.Add(triange8);

                        model.MyMesh.TextureCoordinates.Add(new Point(0, 0));
                        model.MyMesh.TextureCoordinates.Add(new Point(1, 0));
                        model.MyMesh.TextureCoordinates.Add(new Point(0, 1));
                        model.MyMesh.TextureCoordinates.Add(new Point(1, 1));

                        model.MyMesh.TextureCoordinates.Add(new Point(Math.Sqrt(Math.Pow((list[0] - list[4]), 2) + Math.Pow((list[2] - list[5]), 2)) * 1 / list[6],
                            list[8] * 1 / list[7]));
                        model.MyMesh.TextureCoordinates.Add(new Point(Math.Sqrt(Math.Pow((list[1] - list[4]), 2) + Math.Pow((list[3] - list[5]), 2)) * 1 / list[6],
                            list[8] * 1 / list[7]));
                        model.MyMesh.TextureCoordinates.Add(new Point(Math.Sqrt(Math.Pow((list[0] - list[4]), 2) + Math.Pow((list[2] - list[5]), 2)) * 1 / list[6],
                            (list[8] + list[9]) * 1 / list[7]));
                        model.MyMesh.TextureCoordinates.Add(new Point(Math.Sqrt(Math.Pow((list[1] - list[4]), 2) + Math.Pow((list[3] - list[5]), 2)) * 1 / list[6],
                            (list[8] + list[9]) * 1 / list[7]));

                        model.myWalls.Add(myWall);

                        model.MyMesh.TriangleIndices.Add(model.MyMesh.Positions.IndexOf(modelPoint[0]));
                        model.MyMesh.TriangleIndices.Add(model.MyMesh.Positions.IndexOf(modelPoint[4]));
                        model.MyMesh.TriangleIndices.Add(model.MyMesh.Positions.IndexOf(modelPoint[2]));

                        model.MyMesh.TriangleIndices.Add(model.MyMesh.Positions.IndexOf(modelPoint[2]));
                        model.MyMesh.TriangleIndices.Add(model.MyMesh.Positions.IndexOf(modelPoint[4]));
                        model.MyMesh.TriangleIndices.Add(model.MyMesh.Positions.IndexOf(modelPoint[6]));

                        model.MyMesh.TriangleIndices.Add(model.MyMesh.Positions.IndexOf(modelPoint[2]));
                        model.MyMesh.TriangleIndices.Add(model.MyMesh.Positions.IndexOf(modelPoint[6]));
                        model.MyMesh.TriangleIndices.Add(model.MyMesh.Positions.IndexOf(modelPoint[3]));

                        model.MyMesh.TriangleIndices.Add(model.MyMesh.Positions.IndexOf(modelPoint[6]));
                        model.MyMesh.TriangleIndices.Add(model.MyMesh.Positions.IndexOf(modelPoint[7]));
                        model.MyMesh.TriangleIndices.Add(model.MyMesh.Positions.IndexOf(modelPoint[3]));

                        model.MyMesh.TriangleIndices.Add(model.MyMesh.Positions.IndexOf(modelPoint[3]));
                        model.MyMesh.TriangleIndices.Add(model.MyMesh.Positions.IndexOf(modelPoint[7]));
                        model.MyMesh.TriangleIndices.Add(model.MyMesh.Positions.IndexOf(modelPoint[1]));

                        model.MyMesh.TriangleIndices.Add(model.MyMesh.Positions.IndexOf(modelPoint[1]));
                        model.MyMesh.TriangleIndices.Add(model.MyMesh.Positions.IndexOf(modelPoint[7]));
                        model.MyMesh.TriangleIndices.Add(model.MyMesh.Positions.IndexOf(modelPoint[5]));

                        model.MyMesh.TriangleIndices.Add(model.MyMesh.Positions.IndexOf(modelPoint[0]));
                        model.MyMesh.TriangleIndices.Add(model.MyMesh.Positions.IndexOf(modelPoint[1]));
                        model.MyMesh.TriangleIndices.Add(model.MyMesh.Positions.IndexOf(modelPoint[5]));

                        model.MyMesh.TriangleIndices.Add(model.MyMesh.Positions.IndexOf(modelPoint[0]));
                        model.MyMesh.TriangleIndices.Add(model.MyMesh.Positions.IndexOf(modelPoint[5]));
                        model.MyMesh.TriangleIndices.Add(model.MyMesh.Positions.IndexOf(modelPoint[4]));

                        break;                                    
                    }
                    break;
            }
            return myTriangle;
        }

        public static SaveInfo Create_Level (SaveInfo model, int ColLevel, double h_level_floor)
        {
            SaveInfo InfoSave = model;
            double H_floor = h_level_floor;
            double H_level = 0;

            for (int i = 0; i < ColLevel-1; i++)
            {
                int ColPoint = InfoSave.MyMesh.Positions.Count;
                int ColTextCoord = InfoSave.MyMesh.TextureCoordinates.Count;
                List<Wall_3D> myListNewWall = new List<Wall_3D>();
                for (int j = 0; j < InfoSave.myWalls.Count; j++)
                {
                    foreach (var value in InfoSave.myWalls[j].myTriangle)
                    {
                        Wall_3D newWall = new Wall_3D();
                        newWall.myTriangle = new List<Triange>();
                        
                        if (InfoSave.MyMesh.Positions[value.p1.indexPoint].Z >= H_level)
                        {
                            Triange newtriange = new Triange();
                            newtriange.p1 = new pointTriangle();
                            newtriange.p2 = new pointTriangle();
                            newtriange.p3 = new pointTriangle();
                            
                            Point3D newP1 = (new Point3D
                            {
                                X = InfoSave.MyMesh.Positions[value.p1.indexPoint].X,
                                Y = InfoSave.MyMesh.Positions[value.p1.indexPoint].Y,
                                Z = InfoSave.MyMesh.Positions[value.p1.indexPoint].Z + H_floor
                            });
                            Point3D newP2 = (new Point3D
                            {
                                X = InfoSave.MyMesh.Positions[value.p2.indexPoint].X,
                                Y = InfoSave.MyMesh.Positions[value.p2.indexPoint].Y,
                                Z = InfoSave.MyMesh.Positions[value.p2.indexPoint].Z + H_floor
                            });
                            Point3D newP3 = (new Point3D
                            {
                                X = InfoSave.MyMesh.Positions[value.p3.indexPoint].X,
                                Y = InfoSave.MyMesh.Positions[value.p3.indexPoint].Y,
                                Z = InfoSave.MyMesh.Positions[value.p3.indexPoint].Z + H_floor
                            });

                            newtriange.p1.indexPoint = value.p1.indexPoint + ColPoint;
                            newtriange.p1.index_vt = value.p1.index_vt + ColTextCoord;
                            newtriange.p2.indexPoint = value.p2.indexPoint + ColPoint;
                            newtriange.p2.index_vt = value.p2.index_vt + ColTextCoord;
                            newtriange.p3.indexPoint = value.p3.indexPoint + ColPoint;
                            newtriange.p3.index_vt = value.p3.index_vt + ColTextCoord;

                            newWall.myTriangle.Add(newtriange);

                        }
                        myListNewWall.Add(newWall);                       
                    }
                }

                MeshGeometry3D clon = new MeshGeometry3D();
                foreach (Point value1 in model.MyMesh.TextureCoordinates)
                {
                    clon.TextureCoordinates.Add(value1);
                }

                foreach (Point value1 in clon.TextureCoordinates)
                {
                    model.MyMesh.TextureCoordinates.Add(value1);
                }

                MeshGeometry3D clon1 = new MeshGeometry3D();
                foreach (Point3D value1 in model.MyMesh.Positions)
                {
                    Point3D newPoint = new Point3D(value1.X, value1.Y, value1.Z + H_floor);
                    clon1.Positions.Add(newPoint);
                }

                foreach (Point3D value1 in clon1.Positions)
                {
                    model.MyMesh.Positions.Add(value1);
                }

                H_level = H_level + H_floor;
                InfoSave.myWalls.AddRange(myListNewWall);
            }
            return InfoSave;
        }

        private static Vector3D CreateNormal(Point3D p0, Point3D p1, Point3D p2)
        {
            Vector3D v0 = new Vector3D(p1.X - p0.X, p1.Y - p0.Y, p1.Z - p0.Z);
            Vector3D v1 = new Vector3D(p2.X - p1.X, p2.Y - p1.Y, p2.Z - p1.Z);
            return Vector3D.CrossProduct(v0, v1);
        }

        private static void CreateUV(SaveInfo model, int quantity)
        {
            int quan = quantity;
            switch (quan)
            {
                case 2:
                    model.MyMesh.TextureCoordinates.Add(new Point(0, 0));
                    model.MyMesh.TextureCoordinates.Add(new Point(1, 0));
                    model.MyMesh.TextureCoordinates.Add(new Point(0, 1));
                    model.MyMesh.TextureCoordinates.Add(new Point(1, 1));
                    break;
                case 6:
                    model.MyMesh.TextureCoordinates.Add(new Point(0, 0));
                    model.MyMesh.TextureCoordinates.Add(new Point(1, 0));
                    model.MyMesh.TextureCoordinates.Add(new Point(0, 1));
                    model.MyMesh.TextureCoordinates.Add(new Point(1, 1));
                    break;
                case 8:
                    model.MyMesh.TextureCoordinates.Add(new Point(0, 0));
                    model.MyMesh.TextureCoordinates.Add(new Point(1, 0));
                    model.MyMesh.TextureCoordinates.Add(new Point(0, 1));
                    model.MyMesh.TextureCoordinates.Add(new Point(1, 1));
                    break;
            }

        }

        private static int MaxRandom(List<int> value)
        {
            List<int> MaxRandom = new List<int>();
            for (int i = 0; i < value.Count; i++)
            {
                Random rnd = new Random();
                int value1 = rnd.Next(0, 100);
                MaxRandom.Add(value1);
            }
            int result = MaxRandom.IndexOf(MaxRandom.Max());
            return result;
        }

        private static int[] BFS(int[,] matr_smeznosti, int kol_vershn, int startRoom)
        {
            int[] Mark = new int[kol_vershn]; //массив пометок
            int[] Parent = new int[kol_vershn]; //массив предков
            for (int i = 0; i < kol_vershn; i++)
            {
                Mark[i] = 0;
                Parent[i] = 0;
            }

            Queue<int> Q = new Queue<int>(); //очередь
            int v = startRoom; //начальная вершина
            Mark[v] = 1;
            Q.Enqueue(v);

            while (Q.Count != 0)
            {
                v = Q.Dequeue();
                for (int i = 0; i < kol_vershn; i++)
                {
                    if((matr_smeznosti[v,i] != 0) && (Mark[i] == 0))
                    {
                        Mark[i] = 1;
                        Q.Enqueue(i);
                        Parent[i] = v;
                    }
                }
                Mark[v] = 2;
            }
            return Parent;
        }        
    }
}
