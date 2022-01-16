using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheZoo.TicketsService;

namespace TheZoo.Jobs
{
    public class Cashier : Job
    {
        private TicketOffice _ticketOffice;
        public event EventHandler<TheZooJobEventArgs> VisitorEntering;
        public event EventHandler<TheZooJobEventArgs> CallingAdminToTheTicketOffice;

        public Cashier() : base (typeof(Cashier))
        {

        }

        public TicketOffice TicketOffice { get => _ticketOffice; set => _ticketOffice = value; }

        public override bool DoTheWork(object theObject, Employee employee)
        {
            Visitor visitor = (Visitor)theObject;
            Visitor processedVisitor = ProcessTheVisitor(visitor, employee);



            if(processedVisitor != null && processedVisitor.Ticket != null)
            {
                TheZooJobEventArgs args = new();
                args.theObject = processedVisitor;
                OnVisitorEntering(args);
                return true;
            }
            return false;
        }
        public Visitor ProcessTheVisitor(Visitor visitor, Employee employee)
        {
            double ticketPriceToPay = CalcPriceToPay(visitor);
            if (visitor.CashBalance < ticketPriceToPay && employee.Role == EmployeeRoleEnum.CASHIER)
            {
                TicketOffice.TicketsFailedToSell++;
                return null;
            }
            else
            {
                double employeesPartToCharge = (employee.PercentageOfWork / 100) * ticketPriceToPay;
                double? ticketCharged = visitor.RemoveFromCashBalance(employeesPartToCharge);
                if (ticketCharged == null)
                {
                    TicketOffice.TicketsFailedToSell++;
                    return null;
                }
                else
                {
                    TicketOffice.CashInRegistry += ticketCharged.Value;

                    if(employee.Role == EmployeeRoleEnum.CASHIER)
                    {
                        TheZooJobEventArgs args = new();
                        args.theObject = visitor;
                        OnCallTheAdmin(args);
                        
                    }
                    else if(employee.Role == EmployeeRoleEnum.ADMINISTRATOR)
                    {
                        Ticket ticket = TicketOffice.PrintTheTicket(visitor, ticketPriceToPay);
                        visitor.Ticket = ticket;
                    }

                    return visitor; 
                }
            }


        }

        protected virtual void OnCallTheAdmin(TheZooJobEventArgs args)
        {
            EventHandler<TheZooJobEventArgs> handler = CallingAdminToTheTicketOffice;
            if(handler != null)
            {
                handler(this, args);
            }
        }

        private double CalcPriceToPay(Visitor visitor)
        {

            if (visitor.Age < 7) { return TicketOffice.TicketPrice / 2; }
            else { return TicketOffice.TicketPrice; }
        }

        protected virtual void OnVisitorEntering(TheZooJobEventArgs args)
        {
            EventHandler<TheZooJobEventArgs> handler = VisitorEntering;
            if(handler != null)
            {
                handler(this, args);
            }
        }

    }
}
