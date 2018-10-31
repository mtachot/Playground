using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Decorator_Pattern
{
    public abstract class AbstractCoffee
    {
        protected AbstractCoffee k = null;

        public AbstractCoffee(AbstractCoffee k)
        {
            this.k = k;
        }

        public abstract string ShowCoffee();
    }
}
