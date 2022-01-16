using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheZoo.TicketsService
{
    public class Ticket
    {
        private double _ticketPrice;
        private Visitor _visitor;

        public Ticket(double ticketPrice, Visitor visitor)
        {
            _ticketPrice = ticketPrice;
            _visitor = visitor;
        }

        public double TicketPrice { get => _ticketPrice; }
        public Visitor Visitor { get => _visitor;  }
    }
}
