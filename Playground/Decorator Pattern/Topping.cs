using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Decorator_Pattern
{
    public class Topping : AbstractCoffee
    {
        public Topping(AbstractCoffee c) : base(c) { }

        public override string ShowCoffee()
        {
            if (k != null)
                return k.ShowCoffee() + " with topping";
            else
                return "topping";
        }
    }
}
