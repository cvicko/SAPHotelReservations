using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPhotelReservationsLogic.Models
{
    public class Hotel
    {
        private int size;
        private Room[] rooms;
        public Hotel()
        {
            size = 0;
            rooms = new Room[1000];
            CreateRooms();
        }
        public Hotel(int size)
        {
            this.size = size;
            rooms = new Room[size];
            CreateRooms();
        }
        private void CreateRooms()
        {
            for (int i = 0; i < size; i++)
                rooms[i] = new Room();
        }
        public int Size { get => size; set => size = value; }
        internal Room[] Rooms { get => rooms; set => rooms = value; }
    }
}
