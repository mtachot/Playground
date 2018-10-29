using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Playground.FactoryPattern;
using Playground.WrapperPattern;

[assembly: CLSCompliant(true)]
namespace Playground
{
    class Program
    {
        static void Main()
        {
            string separator = "**********************************************************************";
            string title = String.Empty;

            #region Lazy objects testing

            title = "Lazy objects testing";
            Console.WriteLine(separator);
            Console.WriteLine(String.Format(CultureInfo.InvariantCulture, "\t{0}", title));
            Console.WriteLine(separator);
            Console.WriteLine();

            LazyCustomer customer = new LazyCustomer(1);
            Console.WriteLine("LazyCustomer customer = new LazyCustomer(1);");
            Console.WriteLine(String.Format(CultureInfo.InvariantCulture, "Are orders created ? {0}", customer.Orders.IsValueCreated ? "Yes" : "No"));
            Console.ReadLine();

            List<Order> orders = customer.Orders.Value;
            Console.WriteLine("List<Order> orders = customer.Orders.Value;");
            Console.WriteLine(String.Format(CultureInfo.InvariantCulture, "Are orders created ? {0}", customer.Orders.IsValueCreated ? "Yes" : "No"));
            Console.ReadLine();

            #endregion

            #region Factory testing

            Console.Clear();
            title = "Factory testing";
            Console.WriteLine(separator);
            Console.WriteLine(String.Format(CultureInfo.InvariantCulture, "\t{0}", title));
            Console.WriteLine(separator);
            Console.WriteLine();

            Console.WriteLine("IMonster monster = MonsterFactory.CreateRandom();");
            for (int i = 0; i < 10; i++)
            {
                IMonster monster = MonsterFactory.CreateRandom();
                Console.WriteLine(monster.Attack());
            }
            Console.ReadLine();

            #endregion

            #region Reflection

            Console.Clear();
            title = "Reflection";
            Console.WriteLine(separator);
            Console.WriteLine(String.Format(CultureInfo.InvariantCulture, "\t{0}", title));
            Console.WriteLine(separator);
            Console.WriteLine();

            Assembly assembly = Assembly.LoadFrom(@"C:\Projects\Git\Playground\Playground\bin\Debug\Playground.exe");
            List<Type> listClass = assembly.GetTypes().Where(x => typeof(IMonster).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).ToList();
            Console.WriteLine("Liste des classes qui implémentent l'interface IMonster : ");
            Console.WriteLine();
            List<IMonster> listMonsters = new List<IMonster>();
            foreach (Type item in listClass)
            {
                var inst = (IMonster)Activator.CreateInstance(item);
                listMonsters.Add(inst);
            }
            Console.WriteLine("Instanciation dynamique des classes : ");
            foreach (IMonster monster in listMonsters)
            {
                Console.WriteLine(monster.ToString());
            }
            Console.ReadLine();

            #endregion

            #region WrapperPattern

            Console.Clear();
            title = "Wrapper Pattern";
            Console.WriteLine(separator);
            Console.WriteLine(String.Format(CultureInfo.InvariantCulture, "\t{0}", title));
            Console.WriteLine(separator);
            Console.WriteLine();

            EmailCreator.CreateEmailFrom("from.test@toto.com")
                .To("to.test@toto.com", "to.test2@toto.com")
                .CC("cc.test@toto.com")
                .BCC("bcc.test@toto.com", "bcc.test2@toto.com", "bcc.test3@toto.com", "bcc.test4@toto.com")
                .WithSubject("Email subject here")
                .WithBody("Email body here").Send();

            #endregion
        }
    }
}