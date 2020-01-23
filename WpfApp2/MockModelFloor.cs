using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp2
{
    public class Model_Floor_1
    {
        public static List<Point> Points()
        {
            List<Point> points = new List<Point>();
            points = CreatePoint(
         new Tuple<double, double>(0, 0),
         new Tuple<double, double>(3, 0),
         new Tuple<double, double>(3, 3),
         new Tuple<double, double>(6, 3),
         new Tuple<double, double>(6, 5),
         new Tuple<double, double>(4, 5),
         new Tuple<double, double>(4, 7.5),
         new Tuple<double, double>(5.5, 7.5),
         new Tuple<double, double>(6, 7.5),
         new Tuple<double, double>(0, 7.5),
         new Tuple<double, double>(7.2, 7.5),
         new Tuple<double, double>(8, 7.5),
         new Tuple<double, double>(8, 5),
         new Tuple<double, double>(7.2, 5),
         new Tuple<double, double>(7.2, 3),
         new Tuple<double, double>(7.2, 0),
         new Tuple<double, double>(10.5, 0),
         new Tuple<double, double>(10.5, 5),
         new Tuple<double, double>(10.5, 7.5),
         new Tuple<double, double>(13.7, 7.5),
         new Tuple<double, double>(16, 7.5),
         new Tuple<double, double>(16, 6),
         new Tuple<double, double>(16, 3),
         new Tuple<double, double>(16, 0),
         new Tuple<double, double>(21, 0),
         new Tuple<double, double>(21, 3),
         new Tuple<double, double>(21, 6),
         new Tuple<double, double>(21, 10.75),
         new Tuple<double, double>(18, 10.75),
         new Tuple<double, double>(16, 10.75),
         new Tuple<double, double>(13.7, 10.75),
         new Tuple<double, double>(16, 9),
         new Tuple<double, double>(18, 9),
         new Tuple<double, double>(18, 6),
         new Tuple<double, double>(21, 16),
         new Tuple<double, double>(18, 16),
         new Tuple<double, double>(16, 16),
         new Tuple<double, double>(13.7, 16),
         new Tuple<double, double>(11.5, 16),
         new Tuple<double, double>(11.5, 14),
         new Tuple<double, double>(13.7, 14),
         new Tuple<double, double>(16, 14),
         new Tuple<double, double>(18, 14),
         new Tuple<double, double>(21, 19),
         new Tuple<double, double>(21, 21.5),
         new Tuple<double, double>(16, 21.5),
         new Tuple<double, double>(16, 19),
         new Tuple<double, double>(10.5, 21.5),
         new Tuple<double, double>(10.5, 19),
         new Tuple<double, double>(10.5, 16),
         new Tuple<double, double>(9.5, 16),
         new Tuple<double, double>(8, 16),
         new Tuple<double, double>(8, 19),
         new Tuple<double, double>(6, 19),
         new Tuple<double, double>(6, 21.5),
         new Tuple<double, double>(0, 21.5),
         new Tuple<double, double>(0, 17),
         new Tuple<double, double>(4, 17),
         new Tuple<double, double>(4, 19),
         new Tuple<double, double>(5, 17),
         new Tuple<double, double>(7.2, 17),
         new Tuple<double, double>(7.2, 14),
         new Tuple<double, double>(9.5, 14),
         new Tuple<double, double>(5, 14),
         new Tuple<double, double>(4, 14),
         new Tuple<double, double>(0, 14),
         new Tuple<double, double>(0, 11),
         new Tuple<double, double>(4, 11),
         new Tuple<double, double>(5.5, 11),
         new Tuple<double, double>(7.2, 12),
         new Tuple<double, double>(4, 12));

            return points;
        }
        public static List<Wall> Walls()
        {
            List<Wall> walls = new List<Wall>();

            walls = CreateWall(
         new Tuple<int, int>(3, 14),// 0
         new Tuple<int, int>(0, 1),// 1
         new Tuple<int, int>(1, 2),// 2
         new Tuple<int, int>(2, 3),// 3
         new Tuple<int, int>(3, 4),// 4
         new Tuple<int, int>(4, 5),// 5
         new Tuple<int, int>(5, 6),// 6
         new Tuple<int, int>(6, 9),// 7
         new Tuple<int, int>(9, 0),// 8
         new Tuple<int, int>(1, 15),// 9
         new Tuple<int, int>(15, 14),// 10
         new Tuple<int, int>(14, 13),// 11
         new Tuple<int, int>(13, 12),// 12
         new Tuple<int, int>(12, 17),// 13
         new Tuple<int, int>(12, 11),// 14
         new Tuple<int, int>(15, 16),// 15
         new Tuple<int, int>(16, 17),// 16
         new Tuple<int, int>(17, 18),// 17
         new Tuple<int, int>(18, 11),// 18
         new Tuple<int, int>(11, 10),// 19
         new Tuple<int, int>(10, 8),// 20
         new Tuple<int, int>(8, 4),// 21
         new Tuple<int, int>(8, 7),// 22
         new Tuple<int, int>(7, 6),// 23
         new Tuple<int, int>(7, 68),// 24
         new Tuple<int, int>(68, 67),// 25
         new Tuple<int, int>(67, 66),// 26
         new Tuple<int, int>(66, 9),// 27
         new Tuple<int, int>(16, 23),// 28
         new Tuple<int, int>(23, 22),// 29
         new Tuple<int, int>(22, 21),// 30
         new Tuple<int, int>(21, 20),// 31
         new Tuple<int, int>(20, 19),// 32
         new Tuple<int, int>(19, 18),// 33
         new Tuple<int, int>(23, 24),// 34
         new Tuple<int, int>(25, 22),// 35
         new Tuple<int, int>(24, 25),// 36
         new Tuple<int, int>(25, 26),// 37
         new Tuple<int, int>(26, 33),// 38
         new Tuple<int, int>(33, 21),// 39
         new Tuple<int, int>(33, 32),// 40
         new Tuple<int, int>(26, 27),// 41
         new Tuple<int, int>(31, 32),// 42
         new Tuple<int, int>(31, 20),// 43
         new Tuple<int, int>(30, 19),// 44
         new Tuple<int, int>(30, 29),// 45
         new Tuple<int, int>(29, 28),// 46
         new Tuple<int, int>(28, 27),// 47
         new Tuple<int, int>(27, 34),// 48
         new Tuple<int, int>(34, 35),// 49
         new Tuple<int, int>(35, 42),// 50
         new Tuple<int, int>(42, 28),// 51
         new Tuple<int, int>(29, 41),// 52
         new Tuple<int, int>(40, 41),// 53
         new Tuple<int, int>(30, 40),// 54
         new Tuple<int, int>(37, 40),// 55
         new Tuple<int, int>(39, 40),// 56
         new Tuple<int, int>(39, 38),// 57
         new Tuple<int, int>(37, 38),// 58
         new Tuple<int, int>(36, 37),// 59
         new Tuple<int, int>(36, 46),// 60
         new Tuple<int, int>(46, 43),// 61
         new Tuple<int, int>(34, 43),// 62
         new Tuple<int, int>(43, 44),// 63
         new Tuple<int, int>(44, 45),// 64
         new Tuple<int, int>(45, 46),// 65
         new Tuple<int, int>(45, 47),// 66
         new Tuple<int, int>(47, 48),// 67
         new Tuple<int, int>(47, 54),// 68
         new Tuple<int, int>(52, 48),// 69
         new Tuple<int, int>(48, 49),// 70
         new Tuple<int, int>(49, 38),// 71
         new Tuple<int, int>(49, 50),// 72
         new Tuple<int, int>(50, 62),// 73
         new Tuple<int, int>(61, 62),// 74
         new Tuple<int, int>(50, 51),// 75
         new Tuple<int, int>(51, 52),// 76
         new Tuple<int, int>(52, 53),// 77
         new Tuple<int, int>(53, 54),// 78
         new Tuple<int, int>(53, 58),// 79
         new Tuple<int, int>(57, 58),// 80
         new Tuple<int, int>(57, 59),// 81
         new Tuple<int, int>(59, 63),// 82
         new Tuple<int, int>(60, 61),// 83
         new Tuple<int, int>(59, 60),// 84
         new Tuple<int, int>(63, 61),// 85
         new Tuple<int, int>(61, 69),// 86
         new Tuple<int, int>(10, 69),// 87
         new Tuple<int, int>(69, 70),// 88
         new Tuple<int, int>(64, 70),// 89
         new Tuple<int, int>(67, 70),// 90
         new Tuple<int, int>(64, 65),// 91
         new Tuple<int, int>(65, 66),// 92
         new Tuple<int, int>(56, 65),// 93
         new Tuple<int, int>(56, 57),// 94
         new Tuple<int, int>(55, 56),// 95
         new Tuple<int, int>(54, 55),// 96
         new Tuple<int, int>(32, 28),// 97
         new Tuple<int, int>(29, 31),// 98
         new Tuple<int, int>(41, 42),// 99
         new Tuple<int, int>(35, 36),// 100
         new Tuple<int, int>(63, 64));// 101


            return walls;
        }
        public static List<Apartment> Apartsments()
        {

            List<Apartment> apartments = new List<Apartment>();
            Apartment Holl = new Apartment();
            Apartment Apartment_1 = new Apartment();
            Apartment Apartment_2 = new Apartment();
            Apartment Apartment_3 = new Apartment();
            Apartment Apartment_4 = new Apartment();
            Apartment Apartment_5 = new Apartment();
            Holl.Rooms = new List<Room>();
            Apartment_1.Rooms = new List<Room>();
            Apartment_2.Rooms = new List<Room>();
            Apartment_3.Rooms = new List<Room>();
            Apartment_4.Rooms = new List<Room>();
            Apartment_5.Rooms = new List<Room>();


            Room Holl_Room_1 = new Room();
            Room Room_1 = new Room();
            Room Room_2 = new Room();
            Room Room_3 = new Room();
            Room Room_4 = new Room();
            Room Room_5 = new Room();
            Room Room_6 = new Room();
            Room Room_7 = new Room();
            Room Room_8 = new Room();
            Room Room_9 = new Room();
            Room Room_10 = new Room();
            Room Room_11 = new Room();
            Room Room_12 = new Room();
            Room Room_13 = new Room();
            Room Room_14 = new Room();
            Room Room_15 = new Room();
            Room Room_16 = new Room();
            Room Room_17 = new Room();
            Room Room_18 = new Room();
            Room Room_19 = new Room();
            Room Room_20 = new Room();
            Room Room_21 = new Room();
            Room Room_22 = new Room();
            Room Room_23 = new Room();
            Room Room_24 = new Room();
            Room Room_25 = new Room();
            Room Room_26 = new Room();
            Room Room_27 = new Room();
            Room Room_28 = new Room();
            Room Room_29 = new Room();
            Room Room_30 = new Room();
            Room Room_31 = new Room();

            Holl_Room_1.Walls = new List<int> {19, 18, 33, 44, 54, 56, 57, 71, 72, 73, 74, 86, 87 };
            Room_1.Walls = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
            Room_2.Walls = new List<int> { 2, 9, 10, 0, 3 };
            Room_3.Walls = new List<int> { 10, 15, 16, 13, 12, 11 };
            Room_4.Walls = new List<int> { 13, 17, 18, 14 };
            Room_5.Walls = new List<int> { 14, 19, 20, 21, 4, 0, 11, 12 };
            Room_6.Walls = new List<int> { 21, 22, 23, 6, 5 };
            Room_7.Walls = new List<int> { 28, 29, 30, 31, 32, 33, 17, 16 };
            Room_8.Walls = new List<int> { 34, 36, 35, 29 };
            Room_9.Walls = new List<int> { 35, 37, 38, 39, 30 };
            Room_10.Walls = new List<int> { 38, 41, 47, 40, 97 };
            Room_11.Walls = new List<int> { 97, 46, 42, 98 };
            Room_12.Walls = new List<int> { 39, 40, 42, 43, 31 };
            Room_13.Walls = new List<int> { 32, 43, 98, 45, 44 };
            Room_14.Walls = new List<int> { 45, 52, 53, 54 };
            Room_15.Walls = new List<int> { 46, 51, 52, 99 };
            Room_16.Walls = new List<int> { 47, 48, 49, 50, 51 };
            Room_17.Walls = new List<int> { 50, 100, 59, 55, 53, 99 };
            Room_18.Walls = new List<int> { 56, 55, 58, 57 };
            Room_19.Walls = new List<int> { 100, 49, 62, 61, 60 };
            Room_20.Walls = new List<int> { 61, 63, 64, 65 };
            Room_21.Walls = new List<int> { 71, 58, 59, 60, 65, 66, 67, 70 };
            Room_22.Walls = new List<int> { 77, 69, 67, 68, 78 };
            Room_23.Walls = new List<int> { 94, 80, 79, 78, 96, 95 };
            Room_24.Walls = new List<int> { 84, 83, 74, 73, 75, 76, 77, 79, 80 };
            Room_25.Walls = new List<int> { 75, 72, 70, 69, 76 };
            Room_26.Walls = new List<int> { 85, 83, 84, 82 };
            Room_27.Walls = new List<int> { 91, 82, 81, 94, 93 };
            Room_28.Walls = new List<int> { 88, 86, 85, 89 };
            Room_29.Walls = new List<int> { 26, 90, 89, 91, 92 };
            Room_30.Walls = new List<int> { 20, 87, 88, 90, 25, 24, 22 };
            Room_31.Walls = new List<int> { 7, 23, 24, 25, 26, 27 };

            List<Room> roomsHoll = new List<Room>();
            List<Room> roomsApartment1 = new List<Room>();
            List<Room> roomsApartment2 = new List<Room>();
            List<Room> roomsApartment3 = new List<Room>();
            List<Room> roomsApartment4 = new List<Room>();
            List<Room> roomsApartment5 = new List<Room>();

            roomsHoll.Add(Holl_Room_1);

            roomsApartment1.Add(Room_1);
            roomsApartment1.Add(Room_2);
            roomsApartment1.Add(Room_3);
            roomsApartment1.Add(Room_4);
            roomsApartment1.Add(Room_5);
            roomsApartment1.Add(Room_6);

            roomsApartment2.Add(Room_7);
            roomsApartment2.Add(Room_8);
            roomsApartment2.Add(Room_9);
            roomsApartment2.Add(Room_10);
            roomsApartment2.Add(Room_11);
            roomsApartment2.Add(Room_12);
            roomsApartment2.Add(Room_13);

            roomsApartment3.Add(Room_14);
            roomsApartment3.Add(Room_15);
            roomsApartment3.Add(Room_16);
            roomsApartment3.Add(Room_17);
            roomsApartment3.Add(Room_18);
            roomsApartment3.Add(Room_19);
            roomsApartment3.Add(Room_20);
            roomsApartment3.Add(Room_21);

            roomsApartment4.Add(Room_22);
            roomsApartment4.Add(Room_23);
            roomsApartment4.Add(Room_24);
            roomsApartment4.Add(Room_25);
            roomsApartment4.Add(Room_26);
            roomsApartment4.Add(Room_27);

            roomsApartment5.Add(Room_28);
            roomsApartment5.Add(Room_29);
            roomsApartment5.Add(Room_30);
            roomsApartment5.Add(Room_31);

            Holl.Rooms.AddRange(roomsHoll);

            Apartment_1.Rooms.AddRange(roomsApartment1);
            Apartment_2.Rooms.AddRange(roomsApartment2);
            Apartment_3.Rooms.AddRange(roomsApartment3);
            Apartment_4.Rooms.AddRange(roomsApartment4);
            Apartment_5.Rooms.AddRange(roomsApartment5);

            apartments.Add(Holl);
            apartments.Add(Apartment_1);
            apartments.Add(Apartment_2);
            apartments.Add(Apartment_3);
            apartments.Add(Apartment_4);
            apartments.Add(Apartment_5);

            return apartments;
        }

        static List<Point> CreatePoint(params Tuple<double, double>[] wallparams)
        {
            List<Point> points = new List<Point>();
            foreach (var par in wallparams)
            {
                points.Add(new Point
                {
                    X = par.Item1,
                    Y = par.Item2
                });
            }
            return points;
        }
        static List<Wall> CreateWall(params Tuple<int, int>[] wallparams)
        {
            List<Wall> walls = new List<Wall>();
            foreach (var par in wallparams)
            {
                walls.Add(new Wall
                {
                    Start = new Tuple<int>(par.Item1),
                    End = new Tuple<int>(par.Item2)
                });
            }
            return walls;
        }

    }
}
