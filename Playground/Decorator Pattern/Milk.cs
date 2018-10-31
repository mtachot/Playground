using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Decorator_Pattern
{
    public class Milk : AbstractCoffee
    {
        public Milk(AbstractCoffee c) : base(c) { }
        public override string ShowCoffee()
        {
            if (k != null)
                return k.ShowCoffee() + " with milk";
            else
                return "milk";
        }
    }
}
