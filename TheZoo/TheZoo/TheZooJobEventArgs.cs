using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheZoo.Animals;

namespace TheZoo
{
    public class TheZooJobEventArgs : EventArgs
    {
        //Design this class to send any data to the subscriber
        //I just put the sender

        public Object theObject { get; set; } 
    }
}
