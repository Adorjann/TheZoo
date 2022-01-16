using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheZoo.Animals;
using TheZoo.Jobs;

namespace TheZoo
{
    public class Employee
    {
        private int _id;

        private List<Job> _jobs = new();
        private EmployeeRoleEnum _role;
        private double _percentageOfWork;


        public EmployeeRoleEnum Role { get => _role; }
        public double PercentageOfWork { get => _percentageOfWork; }
        public List<Job> Jobs { get => _jobs; }
        public int Id { get => _id; set => _id = value; }

        public bool AddJob(Job job)
        {
            if (this.Jobs.Count == 2) { return false; }

            if (!this.Jobs.Contains(job))
            {
                this.Jobs.Add(job);
                if (this.Jobs.Contains(job))
                {
                    RoleSetter();
                    return true;
                }
            }
            return false;
        }
        public bool RemoveJob(Job job)
        {
            return this.Jobs.Remove(job);
        }
        private void RoleSetter()
        {
            if(this.Jobs.Count == 2)
            {
                this._role = EmployeeRoleEnum.ADMINISTRATOR;
                this._percentageOfWork = 25;
            }
            else
            {
                if(this.Jobs[0].GetType().Name == "Cashier")
                {
                    this._role = EmployeeRoleEnum.CASHIER;
                    this._percentageOfWork = 75;
                }
                else if(this.Jobs[0].GetType().Name == "Worker")
                {
                    this._role = EmployeeRoleEnum.WORKER;
                    this._percentageOfWork = 75;
                }
            }

        }

        public bool DoYourJob(Animal animal)
        {

            //because administrators have 2jobs, additional filtering is needed
            if(this.Role == EmployeeRoleEnum.ADMINISTRATOR)
            {

                if (this.Jobs[0].TypeOfWork.Equals(typeof(Worker)))
                {
                    if (!this.Jobs[0].DoTheWork(animal, this))
                    {
                        return false;
                    }
                }
                else
                {
                    if (!this.Jobs[1].DoTheWork(animal, this))
                    {
                        return false;
                    }
                }
            }
            else
            {
                //if this Employee  a Worker
                if(!this.Jobs[0].DoTheWork(animal, this))
                {
                    return false;
                }

            }
            return true;
        }
        public bool DoYourJob(Visitor visitor)
        {

            //because administrators have 2jobs, additional filtering is needed
            if (this.Role == EmployeeRoleEnum.ADMINISTRATOR)
            {

                if (this.Jobs[0].TypeOfWork.Equals(typeof(Cashier)))
                {
                    if (!this.Jobs[0].DoTheWork(visitor, this))
                    {
                        return false;
                    }
                }
                else
                {
                    if (!this.Jobs[1].DoTheWork(visitor, this))
                    {
                        return false;
                    }
                }
            }
            else
            {
                //if this Employee is a Cashier
                if (!this.Jobs[0].DoTheWork(visitor, this))
                {
                    return false;
                }

            }
            return true;
        }
    }
}
