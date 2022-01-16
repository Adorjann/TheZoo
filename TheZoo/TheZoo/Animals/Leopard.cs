using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheZoo.Animals
{
    public class Leopard : WildCat
    {
        private const double foodNeedInKg = 5;
        public Leopard() : base(foodNeedInKg)
        {
        }
    }
}
