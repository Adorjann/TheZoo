using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheZoo.Jobs
{
    public class RandomJobAppointer
    {
        private Worker _worker = Worker.GiveMeWorker();
        private Cashier _cashier = Cashier.GiveMeCashier();


        private Random _random = new Random();

        public Worker Worker { get => _worker; }

        public Cashier Cashier { get => _cashier; }

        public bool AppointJobsRandomly(List<Employee> employees)
        {
           if (this.AppointWorkers(employees) &&
                this.AppointCashiers(employees) &&
                this.AppointAdministrators(employees))
           {
                return true;
           }

           return false;

        }

        private bool AppointAdministrators(List<Employee> employees)
        {
            // Appointing one third of all employees to the Administrator job

            List<bool> retVals = new ();

            double oneThirdOfEmployees = employees.Count() / 3;
            oneThirdOfEmployees = Math.Floor(oneThirdOfEmployees);

            while (retVals.Count != oneThirdOfEmployees)
            {
                Employee newWorker = employees[_random.Next(0, employees.Count)];
                if (newWorker.Jobs.Count == 1)
                {
                    if (newWorker.Jobs[0].Equals(this.Worker))
                    {
                        retVals.Add(newWorker.AddJob(this.Cashier));
                    }
                    else
                    {
                        retVals.Add(newWorker.AddJob(this.Worker));
                    }
                }
            }

            return !retVals.Contains(false);
        }

        private bool AppointCashiers(List<Employee> employees)
        {
            // Appointing half of all employees the Cashiers job

            List<bool> retVals = new ();

            employees.ForEach(employee =>
            {
                if (employee.Jobs.Count == 0)
                {
                    retVals.Add(employee.AddJob(this.Cashier));
                }
            });
            return !retVals.Contains(false);
        }

        private bool AppointWorkers(List<Employee> employees)
        {
            // Appointing half of all employees the Workers job

            List<bool> retVals = new ();

            double halfOdEmployees = employees.Count() / 2;
            halfOdEmployees = Math.Floor(halfOdEmployees);

            while (retVals.Count != halfOdEmployees)
            {
                Employee newWorker = employees[_random.Next(0, employees.Count)];
                if (newWorker.Jobs.Count == 0) {
                    retVals.Add(newWorker.AddJob(this.Worker));
                }
            }

            return !retVals.Contains(false);
        }
    }
}
