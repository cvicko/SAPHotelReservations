using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SAPhotelReservationsLogic.Models
{
   public class Reservation
    {
        private static int id;
        private string reservationName;
        private int start;
        private int end;
        private bool reservationStatus;
        public Reservation()
        {
            reservationName = "Booking " + ++id;
            start = 0;
            end = 0;
            reservationStatus = false;
        }
        public Reservation(int start, int end)
        {
            reservationName = "Booking " + ++id;
            this.start = start;
            this.end = end;
            reservationStatus = false;
        }
        public static int Id { get => id; set => id = value; }
        public string ReservationName { get => reservationName; set => reservationName = value; }
        public int Start { get => start; set => start = value; }
        public int End { get => end; set => end = value; }
        public bool ReservationStatus { get => reservationStatus; set => reservationStatus = value; }
    }
}
