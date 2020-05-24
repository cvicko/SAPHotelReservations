using SAPhotelReservationsLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPhotelReservationsLogic.Services
{
    public interface IReservationService
    {
        bool Reserve(Hotel hotel, Reservation reservation);
    }
    public class ReservationService : IReservationService
    {
        public bool Reserve(Hotel hotel, Reservation reservation)
        {
            if (reservation.Start >= 0 && reservation.End < 365)
            {
                var rooms = hotel.Rooms.Where(x => x.RoomAvailability.Where(q => q.Key >= reservation.Start && q.Key <= reservation.End && q.Value == false).Any() == false).ToArray();
                if (rooms.Count() == 1)
                    PopulateRoom(rooms.FirstOrDefault(), reservation);
                else if (rooms.Count() > 1)
                    PopulateRoom(GetMostSuitableRoom(rooms, reservation), reservation);
            }
            return reservation.ReservationStatus;
        }
        private Room GetMostSuitableRoom(Room[] rooms, Reservation reservation)
        {
            Room room = null;
            if (reservation.Start > 0 && reservation.End < 364)
                room = rooms.Where(x => x.RoomAvailability[reservation.Start - 1] == false ||
                          x.RoomAvailability[reservation.End + 1] == false).FirstOrDefault();
            if (room == null)
                room = rooms.FirstOrDefault();
            return room;
        }
        private void PopulateRoom(Room room, Reservation reservation)
        {
            room.RoomAvailability.Where(x => x.Key >= reservation.Start && x.Key <= reservation.End).ToList()
                .ForEach(x => room.RoomAvailability[x.Key] = false);
            reservation.ReservationStatus = true;
        }
    }
}
