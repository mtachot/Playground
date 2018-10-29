using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.FactoryPattern
{
    public static class MonsterFactory
    {
        private static Random rand = new Random();

        public static IMonster CreateRandom()
        {
            int id = rand.Next(1, 4);

            switch (id)
            {
                case 1:
                    return new GroundMonster();

                case 2:
                    return new AirMonster();

                case 3:
                    return new MagicMonster();

                default:
                    return new GroundMonster();
            }
        }
    }
}