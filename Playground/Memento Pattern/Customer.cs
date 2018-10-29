using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.MementoPattern
{
    public class Customer
    {
        // This is the memento object, which holds the original values
        private readonly Customer _customer;

        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }

        public bool IsDirty
        {
            get
            {
                return Name != _customer.Name ||
                       Address != _customer.Address ||
                       City != _customer.City ||
                       PostalCode != _customer.PostalCode;
            }
        }

        public Customer(int id, string name, string address, string city, string postalCode)
        {
            ID = id;
            Address = address;
            Name = name;
            City = city;
            PostalCode = postalCode;

            // Save the originally-passed values to the memento
        }

        public void RevertToOriginalValues()
        {
            Name = _customer.Name;
            Address = _customer.Address;
            City = _customer.City;
            PostalCode = _customer.PostalCode;
        }

        // This is one of the rare cases where you have to declare more than one class in a file
        // The CustomerMemento class will never be used in any place, other than in the Customer class
        // So, you can make it a private class inside the one class where it is used.
        private class CustomerMemento
        {
            public string Name { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string PostalCode { get; set; }
        }
    }
}