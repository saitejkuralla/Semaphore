using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lock_project
{
    public class BusReservation
    {
        private object lockObject = new object();
        int AvailableTickets = 3;
        static int t1 = 2, t2 = 3, t3 = 1;
        public void BookTicket(string name, int wantedtickets)
        {
            lock (lockObject)
            {
                if (wantedtickets <= AvailableTickets)
                {
                    Console.WriteLine(wantedtickets + " booked to " + name);
                    AvailableTickets = AvailableTickets - wantedtickets;
                }
                else
                {
                    Console.WriteLine("No tickets Available to book");
                }
            }
        }
        public void TicketBooking()
        {
            string name = Thread.CurrentThread.Name;
            if (name.Equals("User1"))
            {
                BookTicket(name, t1);
            }
            else if (name.Equals("User2"))
            {
                BookTicket(name, t2);
            }
            else
            {
                BookTicket(name, t3);
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            BusReservation reservation = new BusReservation();
            Thread user1 = new Thread(reservation.TicketBooking) { Name = "User1" };
            Thread user2 = new Thread(reservation.TicketBooking) { Name = "User2" };
            Thread user3 = new Thread(reservation.TicketBooking) { Name = "User3" };
            user1.Start();
            user2.Start();
            user3.Start();
            Console.ReadLine();
        }
    }
}
