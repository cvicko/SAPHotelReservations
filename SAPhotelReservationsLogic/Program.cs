using SAPhotelReservationsLogic.Models;
using SAPhotelReservationsLogic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace SAPhotelReservationsLogic
{
    class Program
    {
        static void Main(string[] args)
        {
            printMenu();
        }
        public static void printMenu()
        {
            IReservationService reservationService = new ReservationService();
            Hotel hotel = null;
            List<Reservation> reservations = new List<Reservation>();
            while (true)
            {
                Console.WriteLine(@"Welcome to SAP Hotel reservations." + Environment.NewLine +
                "Please choose one of the options below to continue:" + Environment.NewLine +
                "1. Create a hotel." + Environment.NewLine +
                "2. Create a reservation." + Environment.NewLine +
                "3. View room availability for current hotel." + Environment.NewLine +
                "4. View your reservations." + Environment.NewLine +
                "5. Exit" + Environment.NewLine);
                bool menuOptionVerification = int.TryParse(Console.ReadLine(), out int menuOption);
                if (menuOptionVerification)
                {
                    switch (menuOption)
                    {
                        case 1:
                            if (hotel == null)
                                hotel = CreateHotel();
                            else
                            {
                                Console.WriteLine("You already created a hotel. Do you want to create it again?");
                                Console.WriteLine("1. Yes.\n2. No." + Environment.NewLine);
                                bool hotelOptionVerification = int.TryParse(Console.ReadLine(), out int hotelOption);
                                if (hotelOptionVerification)
                                {
                                    if (hotelOption == 1)
                                    {
                                        hotel = CreateHotel();
                                        reservations.Clear();
                                    }
                                    else
                                        break;
                                }
                                else
                                {
                                    Console.WriteLine("Please insert correct menu option!");
                                    Thread.Sleep(2000);
                                }
                            }
                            break;
                        case 2:
                            CheckIfHotelExists(ref hotel);
                            Reservation reservation = CreateReservations();
                            reservations.Add(reservation);
                            Console.Write("Your reservation status is: ");
                            if (reservationService.Reserve(hotel, reservation))
                                Console.WriteLine("Accepted");
                            else
                                Console.WriteLine("Declined");
                            Console.Write("Press any key to continue...");
                            Console.ReadKey();
                            break;
                        case 3:
                            CheckIfHotelExists(ref hotel);
                            RoomAvailability(hotel);
                            break;
                        case 4:
                            CheckIfHotelExists(ref hotel);
                            ReservationsTable(reservations);
                            break;
                        case 5:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Wrong operation");
                            Thread.Sleep(2000);
                            break;
                    }

                }
                else
                {
                    Console.WriteLine("Please insert correct menu option!");
                    Thread.Sleep(2000);
                }
                Console.Clear();
            }
        }
        private static Hotel CreateHotel()
        {
            Console.Clear();
            bool sizeValidation = false;
            while (!sizeValidation)
            {
                Console.WriteLine("Enter number of rooms for your hotel: ");
                sizeValidation = int.TryParse(Console.ReadLine(), out int size);
                if (sizeValidation && size <= 1000)
                {
                    Console.Write("You successfully created a hotel. Press any key to continue..!");
                    Console.ReadKey();
                    return new Hotel(size);
                }
                else
                {
                    sizeValidation = false;
                    Console.WriteLine("Please insert correct format!");
                    Thread.Sleep(2000);
                    Console.Clear();
                }
            }
            return null;
        }
        private static void CheckIfHotelExists(ref Hotel hotel)
        {
            if (hotel == null)
            {
                Console.WriteLine("There is no hotel created! You will be redirected to hotel creation page..");
                Thread.Sleep(2000);
                hotel = CreateHotel();
            }
        }
        private static Reservation CreateReservations()
        {
            Console.Clear();
            Console.WriteLine("Enter start date for your reservation:");
            bool startDateValidation = int.TryParse(Console.ReadLine(), out int startDate);
            Console.WriteLine("Enter end date for your reservation:");
            bool endtDateValidation = int.TryParse(Console.ReadLine(), out int endDate);
            if (startDateValidation && endtDateValidation)
                return new Reservation(startDate, endDate);
            else
            {
                Console.WriteLine("Please insert correct format!");
                Thread.Sleep(2000);
            }
            return null;
        }
        private static void RoomAvailability(Hotel hotel)
        {
            Console.Clear();
            Console.Write("Room name \t");
            for (int i = 0; i <= 10; i++)
                Console.Write(i + "\t");
            Console.WriteLine();
            foreach (Room room in hotel.Rooms)
            {
                Console.Write(room.RoomName + "\t\t");
                foreach (KeyValuePair<int, bool> item in room.RoomAvailability)
                    if (item.Key == 11)
                        break;
                    else if (!item.Value)
                        Console.Write("X\t");
                    else
                        Console.Write("-\t");
                Console.WriteLine();
            }
            Console.WriteLine("If you want to see all data in excel, press Y, otherwise press any key to continue...");
            if (Console.ReadKey().Key == ConsoleKey.Y)
                ExportToExcel(hotel);
        }
        public static void ExportToExcel(Hotel hotel)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            Console.Clear();
            Console.WriteLine(@"Craeting excel file at: " + path + "...");
            Application xlApp = new Application();
            Workbook xlWorkBook = xlApp.Workbooks.Add(Type.Missing);
            _Worksheet xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.Cells[1, 1] = "Room name";
            for (int i = 0; i < 365; i++)
                xlWorkSheet.Cells[1, i + 2] = "Day " + i;
            for (int i = 0; i < hotel.Rooms.Length; i++)
            {
                xlWorkSheet.Cells[i + 2, 1] = hotel.Rooms[i].RoomName;
                for (int j = 0; j < hotel.Rooms[i].RoomAvailability.Count; j++)
                    if (hotel.Rooms[i].RoomAvailability[j])
                        xlWorkSheet.Cells[i + 2, j + 2] = "-";
                    else
                        xlWorkSheet.Cells[i + 2, j + 2] = "X";
            }
            xlWorkBook.SaveAs(path + @"\SAPHotelReservations.xls");
            xlApp.Quit();
            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);
            Console.WriteLine("Done.");
            Console.Write("Press any key to continue..");
            Console.ReadKey();
        }
        private static void ReservationsTable(List<Reservation> reservations)
        {
            Console.Clear();
            Console.WriteLine("Reservation name\t  StartDate\t EndDate\tResult:Accept/Decline");
            foreach (Reservation reservation in reservations)
            {
                Console.Write(reservation.ReservationName + "\t\t   " + reservation.Start + "\t\t    " + reservation.End + "\t\t");
                if (reservation.ReservationStatus)
                    Console.WriteLine("Accepted");
                else
                    Console.WriteLine("Declined");
            }
            Console.WriteLine("Press any key to continue..");
            Console.ReadKey();
        }
    }
}
