using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground
{
    public class Order
    {

    }

    public class LazyCustomer
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public Lazy<List<Order>> Orders { get; set; }

        public LazyCustomer(int id)
        {
            Id = id;
            Name = "Toto";

            // Création d'un objet lourd fictif, de type Lazy
            Orders = new Lazy<List<Order>>(() =>
            {
                return Enumerable.Repeat(new Order(), 200).ToList();
            });
        }
    }
}