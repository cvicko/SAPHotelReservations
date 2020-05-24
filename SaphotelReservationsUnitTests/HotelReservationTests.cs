using Microsoft.VisualStudio.TestTools.UnitTesting;
using SAPhotelReservationsLogic.Models;
using SAPhotelReservationsLogic.Services;
namespace SaphotelReservationsUnitTests
{
    [TestClass]
    public class HotelReservationTests
    {
        [TestMethod]
        public void Reserve_RequestOutsideOfPlaningPeriod_Declined()
        {
            //Arange
            Hotel hotel = new Hotel(1);
            IReservationService reservationService = new ReservationService();
            Reservation reservation = new Reservation(-4, 2);

            //Act
            bool reservationState = reservationService.Reserve(hotel, reservation);

            //Asert
            bool expected = false;
            Assert.AreEqual(expected, reservationState);
        }
        [TestMethod]
        public void Reserve_RequestRangeOutsideOfPlaningPeriod_Declined()
        {
            //Arange
            Hotel hotel = new Hotel(1);
            IReservationService reservationService = new ReservationService();
            Reservation reservation = new Reservation(200, 400);

            //Act
            bool reservationState = reservationService.Reserve(hotel, reservation);

            //Asert
            bool expected = false;
            Assert.AreEqual(expected, reservationState);
        }
        [TestMethod]
        public void Reserve_AllRequests_Accepted()
        {
            //Arange
            Hotel hotel = new Hotel(3);
            IReservationService reservationService = new ReservationService();
            Reservation reservation1 = new Reservation(0, 5);
            Reservation reservation2 = new Reservation(7, 13);
            Reservation reservation3 = new Reservation(3, 9);
            Reservation reservation4 = new Reservation(5, 7);
            Reservation reservation5 = new Reservation(6, 6);
            Reservation reservation6 = new Reservation(0, 4);

            //Act
            bool reservationState1 = reservationService.Reserve(hotel, reservation1);
            bool reservationState2 = reservationService.Reserve(hotel, reservation2);
            bool reservationState3 = reservationService.Reserve(hotel, reservation3);
            bool reservationState4 = reservationService.Reserve(hotel, reservation4);
            bool reservationState5 = reservationService.Reserve(hotel, reservation5);
            bool reservationState6 = reservationService.Reserve(hotel, reservation6);

            //Asert
            bool expected = true;
            Assert.AreEqual(expected, reservationState1);
            Assert.AreEqual(expected, reservationState2);
            Assert.AreEqual(expected, reservationState3);
            Assert.AreEqual(expected, reservationState4);
            Assert.AreEqual(expected, reservationState5);
            Assert.AreEqual(expected, reservationState6);
        }
        [TestMethod]
        public void Reserve_RequestCanBeDeclined_Declined()
        {
            //Arange
            Hotel hotel = new Hotel(3);
            IReservationService reservationService = new ReservationService();
            Reservation reservation1 = new Reservation(1, 3);
            Reservation reservation2 = new Reservation(2, 5);
            Reservation reservation3 = new Reservation(1, 9);
            Reservation reservation4 = new Reservation(0, 15);

            //Act
            bool reservationState1 = reservationService.Reserve(hotel, reservation1);
            bool reservationState2 = reservationService.Reserve(hotel, reservation2);
            bool reservationState3 = reservationService.Reserve(hotel, reservation3);
            bool reservationState4 = reservationService.Reserve(hotel, reservation4);

            //Asert
            bool expected = true;
            Assert.AreEqual(expected, reservationState1);
            Assert.AreEqual(expected, reservationState2);
            Assert.AreEqual(expected, reservationState3);
            expected = false;
            Assert.AreEqual(expected, reservationState4);
        }
        [TestMethod]
        public void Reserve_RequestCanBeAcceptedAfterPreviousRequestIsDeclined_Accepted()
        {
            //Arange
            Hotel hotel = new Hotel(3);
            IReservationService reservationService = new ReservationService();
            Reservation reservation1 = new Reservation(1, 3);
            Reservation reservation2 = new Reservation(0, 15);
            Reservation reservation3 = new Reservation(1, 9);
            Reservation reservation4 = new Reservation(2, 5);
            Reservation reservation5 = new Reservation(4, 9);

            //Act
            bool reservationState1 = reservationService.Reserve(hotel, reservation1);
            bool reservationState2 = reservationService.Reserve(hotel, reservation2);
            bool reservationState3 = reservationService.Reserve(hotel, reservation3);
            bool reservationState4 = reservationService.Reserve(hotel, reservation4);
            bool reservationState5 = reservationService.Reserve(hotel, reservation5);

            //Asert
            bool expected = true;
            Assert.AreEqual(expected, reservationState1);
            Assert.AreEqual(expected, reservationState2);
            Assert.AreEqual(expected, reservationState3);
            expected = false;
            Assert.AreEqual(expected, reservationState4);
            expected = true;
            Assert.AreEqual(expected, reservationState5);
        }
        [TestMethod]
        public void Reserve_RequestsCanBeComplex_Accepted()
        {
            //Arange
            Hotel hotel = new Hotel(2);
            IReservationService reservationService = new ReservationService();
            Reservation reservation1 = new Reservation(1, 3);
            Reservation reservation2 = new Reservation(0, 4);
            Reservation reservation3 = new Reservation(2, 3);
            Reservation reservation4 = new Reservation(5, 5);
            Reservation reservation5 = new Reservation(4, 10);
            Reservation reservation6 = new Reservation(10, 10);
            Reservation reservation7 = new Reservation(6, 7);
            Reservation reservation8 = new Reservation(8, 10);
            Reservation reservation9 = new Reservation(8, 9);

            //Act
            bool reservationState1 = reservationService.Reserve(hotel, reservation1);
            bool reservationState2 = reservationService.Reserve(hotel, reservation2);
            bool reservationState3 = reservationService.Reserve(hotel, reservation3);
            bool reservationState4 = reservationService.Reserve(hotel, reservation4);
            bool reservationState5 = reservationService.Reserve(hotel, reservation5);
            bool reservationState6 = reservationService.Reserve(hotel, reservation6);
            bool reservationState7 = reservationService.Reserve(hotel, reservation7);
            bool reservationState8 = reservationService.Reserve(hotel, reservation8);
            bool reservationState9 = reservationService.Reserve(hotel, reservation9);

            //Asert
            bool expected1 = true;
            bool expected2 = false;
            Assert.AreEqual(expected1, reservationState1);
            Assert.AreEqual(expected1, reservationState2);
            Assert.AreEqual(expected2, reservationState3);
            Assert.AreEqual(expected1, reservationState4);
            Assert.AreEqual(expected1, reservationState5);
            Assert.AreEqual(expected1, reservationState6);
            Assert.AreEqual(expected1, reservationState7);
            Assert.AreEqual(expected2, reservationState8);
            Assert.AreEqual(expected1, reservationState9);
        }
    }
}
