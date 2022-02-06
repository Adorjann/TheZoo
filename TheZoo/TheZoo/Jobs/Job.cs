using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheZoo.Animals;
using TheZoo.FoodService;

namespace TheZoo
{
    public abstract class Job
    {
        private Type _typeOfWork;

        public Job(Type typeOfWork) {
            this._typeOfWork = typeOfWork;
        }

        public Type TypeOfWork { get => _typeOfWork;  }

        public abstract bool DoTheWork(object theObject,Employee employee);
    }
}
