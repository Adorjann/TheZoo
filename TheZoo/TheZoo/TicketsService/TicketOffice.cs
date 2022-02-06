using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheZoo.TicketsService;

namespace TheZoo
{
    public class TicketOffice
    {
        private const double _ticketPrice = 800;
        private double _cashInRegistry;
        private int _ticketsSold;
        private int _ticketsFailedToSell;

        public static double TicketPrice => _ticketPrice;

        public double CashInRegistry { get => _cashInRegistry; set => _cashInRegistry = value; }

        public int TicketsSold { get => _ticketsSold; set => _ticketsSold = value; }

        public int TicketsFailedToSell { get => _ticketsFailedToSell; set => _ticketsFailedToSell = value; }

        public Ticket PrintTheTicket(Visitor visitor, double price)
        {
            if (visitor != null && price != 0)
            {
               Ticket ticket = new (price, visitor);
               this.TicketsSold++;
               return ticket;
            }

            return null;
        }
    }
}
