using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.FactoryPattern
{
    public class MagicMonster : IMonster
    {
        public string Attack()
        {
            return ("Magic attack");
        }
    }
}