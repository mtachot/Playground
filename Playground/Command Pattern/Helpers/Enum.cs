using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.CommandPattern
{
    public class Enum
    {
        public enum CommandState
        {
            Unprocessed,
            ExecuteFailed,
            ExecuteSucceeded,
            UndoFailed,
            UndoSucceeded
        }
    }
}