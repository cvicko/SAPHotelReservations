using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPhotelReservationsLogic.Models
{
    public class Room
    {
        private static int id;
        private string roomName;
        private Dictionary<int, bool> roomAvailability;
        public Room()
        {
            roomName = "Room " + ++id;
            roomAvailability = new Dictionary<int, bool>();
            CreateDaysForRoom();
        }
        private void CreateDaysForRoom()
        {
            for (int i = 0; i < 365; i++)
                roomAvailability.Add(i, true);
        }
        public static int Id { get => id; set => id = value; }
        public string RoomName { get => roomName; set => roomName = value; }
        public Dictionary<int, bool> RoomAvailability { get => roomAvailability; set => roomAvailability = value; }


    }
}
