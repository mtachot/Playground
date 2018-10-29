using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.CommandPattern
{
    public interface ITransaction
    {
        int ID { get; set; }
        DateTime CreatedOn { get; set; }
        Enum.CommandState Status { get; set; }
        void Execute();
        void Undo();
    }
}