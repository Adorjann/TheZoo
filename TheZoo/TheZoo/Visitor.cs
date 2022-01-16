using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheZoo.TicketsService;

namespace TheZoo
{
    public class Visitor
    {
        private int _id;
        private double _cashBalance;
        private int _age;
        private Ticket ticket;

        public Visitor(double cashBalance, int age, int id)
        {
            _cashBalance = cashBalance;
            _age = age;
            _id = id;
        }

        public double CashBalance { get => _cashBalance; }
        public int Age { get => _age; }
        public Ticket Ticket { get => ticket; set => ticket = value; }
        public int Id { get => _id; }

        public override bool Equals(Object obj)
        {
            return obj is Visitor visitor &&
                   _id == visitor._id;
        }

        public double? RemoveFromCashBalance(double amount)
        {
            if (amount <= 0 || amount > CashBalance) { return null; }

            this._cashBalance -= amount;
            return amount;

        }

    }   
}
