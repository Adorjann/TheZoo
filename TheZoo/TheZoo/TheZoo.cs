using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TheZoo.Animals;
using TheZoo.FoodService;
using TheZoo.Jobs;

namespace TheZoo
{
    public class TheZoo
    {
        private List<Animal> _allAnimals = new ();
        private List<Employee> _allEmployees = new ();
        private FoodStorage _foodStorage;
        private TicketOffice _ticketOffice;

        private List<Visitor> _allVisitors = new ();

        public List<Visitor> AllVisitors { get => _allVisitors; set => _allVisitors = value; }

        public TheZoo(List<Animal> allAnimals, List<Employee> allEmployees,FoodStorage foodStorage, TicketOffice ticketOffice)
        {
            _allAnimals = allAnimals;
            _allEmployees = allEmployees;
            _foodStorage = foodStorage;
            _ticketOffice = ticketOffice;

            RandomJobAppointer jobAppointer = new ();
            jobAppointer.Worker.FoodStorage = this._foodStorage;
            jobAppointer.Cashier.TicketOffice = this._ticketOffice;
            jobAppointer.AppointJobsRandomly(allEmployees);

            this._allAnimals.ForEach(animal => {
                animal.EventHungryAnimal += this.OnEventHungryAnimal;
                animal.IsdailyNeedSatisfyed();
            });

            jobAppointer.Cashier.VisitorEntering += this.OnVisitorEntering;
            jobAppointer.Cashier.CallingAdminToTheTicketOffice += this.ONCallingAdminToTheTicketOffice;
        }

        public void NewVisitorArrived(Visitor visitor)
        {
            Employee cashier = ReturnRandomEmployee(EmployeeRoleEnum.CASHIER);
            cashier.DoYourJob(visitor);

        }

        private void OnVisitorEntering (Object sender, TheZooJobEventArgs args)
        {
            Visitor visitorWithATicket = (Visitor)args.theObject;
            if (visitorWithATicket != null)
            {
                this.AllVisitors.Add(visitorWithATicket);
            }
        }

        private void ONCallingAdminToTheTicketOffice(Object sender,TheZooJobEventArgs args)
        {
            Visitor visitor = (Visitor)args.theObject;

            Employee admin = this.ReturnRandomEmployee(EmployeeRoleEnum.ADMINISTRATOR);
            if (admin.DoYourJob(visitor))
            {
                Console.WriteLine("\n  A new Visitor has Entered the Zoo! Welcome");
            }
        }

        private void OnEventHungryAnimal(object sender, TheZooJobEventArgs e)
        {
            Employee randomWorker = this.ReturnRandomEmployee(EmployeeRoleEnum.WORKER);
            if (randomWorker != null)
            {
                Animal animalToFeed = (Animal)sender;
                if (!randomWorker.DoYourJob(animalToFeed))
                {
                    Console.WriteLine($"\n\n\t...Since there is no more food for the animals, the Zoo will be closed.." +
                        $"\n\t\t.. Good Bye ..");
                    this.ClosingTheZooREport();
                }
                else
                {
                    // calling an administrator to do his part of the job.(25%)
                    Employee administrator = this.ReturnRandomEmployee(EmployeeRoleEnum.ADMINISTRATOR);

                    administrator.DoYourJob(animalToFeed);
                }
            }
        }

        private void ClosingTheZooREport()
        {
            Console.WriteLine($"\nTicket Office Report:");
            Console.WriteLine($"\tCash in Registry: {_ticketOffice.CashInRegistry}");
            Console.WriteLine($"\tVisitors Entered: {_ticketOffice.TicketsSold}");
            Console.WriteLine($"\tVisitors Rejected: {_ticketOffice.TicketsFailedToSell}");
            Console.WriteLine($"\t(Rejected due to lack of funds)");


            Environment.Exit(0);
        }

        private Employee ReturnRandomEmployee(EmployeeRoleEnum role)
        {
            Random random = new ();
            Employee employee = null;

            while (employee == null)
            {
                Employee testEmployee = _allEmployees[random.Next(0, this._allEmployees.Count)];

                if (testEmployee.Role == role)
                {
                    employee = testEmployee;
                }
            }

            return employee;
        }
    }
}
