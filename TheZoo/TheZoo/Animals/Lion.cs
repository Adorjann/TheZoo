using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheZoo.Animals
{
    public class Lion : WildCat
    {
        private const double foodNeedInKg = 10;

        public Lion() : base(foodNeedInKg)
        {
        }
    }
}
