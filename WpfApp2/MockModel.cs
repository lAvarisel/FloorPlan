using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;

namespace WpfApp2
{
    public class FloorplanModel
    {
        public List<Apartment> Apartments;
        public List<Wall> Walls;
        public List<Point> Points;
        public FloorplanConstants FloorplanConstants;
    }
    public class Apartment
    {
        public List<Room> Rooms;
    }
    public class Room
    {
        public List<int> Walls;
    }
    public class Wall
    {
        public Tuple<int> Start { get; set; }
        public Tuple<int> End { get; set; }
        public List<Opening> Openings;
    }
    public class Opening
    {
        public double Location;
        public OpeningType Type;
    }
    public enum OpeningType
    {
        Window,
        Door,
        Closed,
        Entrance,
        inapartmentWall
    }
    public class FloorplanConstants
    {
        public double H_Floor;
    }
    public class SaveInfo
    {
        public List<Point> Points;
        public List<MyLine> myLines;
        public MeshGeometry3D MyMesh;
        public List<MeshGeometry3D> listMyMesh;
        public List<Triange> myTriangle;
        public List<Wall_3D> myWalls;

        internal Task CopyToAsync(object destinationStream)
        {
            throw new NotImplementedException();
        }
    }
    public class Triange
    {
        public pointTriangle p1;
        public pointTriangle p2;
        public pointTriangle p3;
    }
    public class MyLine
    {
        public int Start;
        public int End;
        public OpeningType Type;
    }
    public class pointTriangle
    {
        public int indexPoint;
        public int index_vt;
    }
    public class Wall_3D
    {
        public List<Triange> myTriangle;
    }
  
}
