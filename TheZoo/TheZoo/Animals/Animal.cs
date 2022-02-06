using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using TheZoo.FoodService;

namespace TheZoo.Animals
{
    public abstract class Animal
    {
        private int _id;
        private double _foodNeedInKg;
        private double _foodEatenTodayInKg;
        private Type _foodPreference;

        public event EventHandler<TheZooJobEventArgs> EventHungryAnimal;

        private static Timer _feedingTimer = new();

        public double FoodNeedInKg { get => _foodNeedInKg;}

        public Type FoodPreference { get => _foodPreference;  }

        public int Id { get => _id; set => _id = value; }

        public int LenghtOfTheDay { get; internal set; }

        public Animal(double dailyFoodNeedInKg, Type foodPreference)
        {
            _foodNeedInKg = dailyFoodNeedInKg;
            _foodPreference = foodPreference;

            // creating a full stomach to start the daily timer
            this._foodEatenTodayInKg = dailyFoodNeedInKg;
        }

        public virtual bool Eat(Food food)
        {
            if (food.GetType() == this.FoodPreference)
            {
                if (this.FoodNeedInKg != this._foodEatenTodayInKg)
                {
                    this._foodEatenTodayInKg += food.Quantity;
                }
                else
                {
                    Console.WriteLine("** Man, I had enough food for today,Thank you!");
                    return false;
                }
            }
            else
            {
                Console.WriteLine($"** Man, {this.GetType().Name}'s don't eat {food.GetType().Name}");
                return false;
            }

            this.IsdailyNeedSatisfyed();
            return true;
        }

        public void IsdailyNeedSatisfyed()
        {
            if (this.FoodNeedInKg == this._foodEatenTodayInKg)
            {
                Animal._feedingTimer.Interval= LenghtOfTheDay;
                Animal._feedingTimer.Elapsed += this.OnFeedMePlease;
                Animal._feedingTimer.AutoReset = false;
                Animal._feedingTimer.Enabled = true;
            }
        }

        public void OnFeedMePlease(Object source, ElapsedEventArgs e)
        {

            Animal._feedingTimer.Stop();

            // returning the food eaten today to 0
            this._foodEatenTodayInKg = 0;

            // setting the value for the event
            TheZooJobEventArgs args = new TheZooJobEventArgs();
            args.theObject = this;
            this.OnFeedMePlease(args);
        }

        protected virtual void OnFeedMePlease(TheZooJobEventArgs args)
        {
            EventHandler<TheZooJobEventArgs> handler = this.EventHungryAnimal;
            if (handler != null)
            {
                handler(this, args);
            }
        }

    }
}
