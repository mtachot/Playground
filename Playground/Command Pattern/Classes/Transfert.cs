using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.CommandPattern
{
    public class Transfert : ITransaction
    {
        private readonly decimal _amount;
        private readonly Account _fromAccount;
        private readonly Account _toAccount;

        public int ID { get; set; }
        public DateTime CreatedOn { get; set; }
        public Enum.CommandState Status { get; set; }
        public bool IsCompleted { get; set; }

        public Transfert(int id, Account fromAccount, Account toAccount, decimal amount)
        {
            ID = id;
            CreatedOn = DateTime.Now;

            _fromAccount = fromAccount;
            _toAccount = toAccount;
            _amount = amount;

            Status = Enum.CommandState.Unprocessed;
        }

        public void Execute()
        {
            if (_fromAccount.Balance >= _amount)
            {
                _fromAccount.Balance -= _amount;
                _toAccount.Balance += _amount;

                Status = Enum.CommandState.ExecuteSucceeded;
            }
            else
                Status = Enum.CommandState.ExecuteFailed;
        }

        public void Undo()
        {
            // Remove the money from the original "to" account
            // and give it back to the original "from" account
            if (_toAccount.Balance >= _amount)
            {
                _toAccount.Balance -= _amount;
                _fromAccount.Balance += _amount;

                Status = Enum.CommandState.UndoSucceeded;
            }
            else
                Status = Enum.CommandState.UndoFailed;
        }
    }
}