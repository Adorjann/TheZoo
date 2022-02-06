using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheZoo.FoodService
{
    public abstract class Food
    {
        private double _quantity;

        public Food()
        {

        }

        public Food(double quantity)
        {
            _quantity = quantity;
        }

        public double Quantity { get => _quantity;  }

        public bool addQuantity(double amount)
        {
            if (amount <= 0)
            {
                return false;
            }

            double quantBeforeAdd = this._quantity;
            this._quantity += amount;

            if (quantBeforeAdd + amount == Quantity)
            {
                return true;
            }else
            {
                return false;
            }
        }

        public double RemoveAmount(double amount)
        {
            if (amount <= 0) {return 0; }

            double quantBeforeRemove = this._quantity;
            this._quantity -= amount;
            if (this._quantity < 0)
            {
                this._quantity = quantBeforeRemove;
                return 0;
            }

            if (quantBeforeRemove - amount == Quantity)
            {
                return amount;
            }
            else { return 0; }
        }
    }
}
