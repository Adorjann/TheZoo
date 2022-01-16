using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheZoo.Animals
{
    public class Tiger : WildCat
    {
        private const double foodNeedInKg = 15;

        public Tiger() : base(foodNeedInKg)
        {
        }
    }
}
