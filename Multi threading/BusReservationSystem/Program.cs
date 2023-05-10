using System;
using System.Threading;

namespace BusReservationSystem
{
    public class BusReservation
    {
        // Object to synchronize access to shared data
        private object lockObject = new object();
        private int availableTickets = 3;

        // Number of tickets requested by each user
        private static int user1Tickets = 2, user2Tickets = 3, user3Tickets = 1;

        public void BookTicket(string name, int wantedTickets)
        {
            lock (lockObject) // Synchronize access to shared data
            {
                if (wantedTickets <= availableTickets)
                {
                    Console.WriteLine(wantedTickets + " tickets booked to " + name);
                    availableTickets = availableTickets - wantedTickets;
                }
                else
                {
                    Console.WriteLine("No tickets available to book for " + name);
                }
            }
        }

        // Method to book tickets for each user
        public void TicketBooking()
        {
            string name = Thread.CurrentThread.Name; // Get name of current thread
            if (name.Equals("User1"))
            {
                BookTicket(name, user1Tickets);
            }
            else if (name.Equals("User2"))
            {
                BookTicket(name, user2Tickets);
            }
            else
            {
                BookTicket(name, user3Tickets);
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            BusReservation reservation = new BusReservation(); // Create instance of BusReservation class

            // Create threads for each user
            Thread user1 = new Thread(reservation.TicketBooking) { Name = "User1" };
            Thread user2 = new Thread(reservation.TicketBooking) { Name = "User2" };
            Thread user3 = new Thread(reservation.TicketBooking) { Name = "User3" };

            // Start threads
            user1.Start();
            user2.Start();
            user3.Start();

            Console.ReadLine();
        }
    }
}
