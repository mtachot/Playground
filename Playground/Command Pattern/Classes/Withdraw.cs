using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.CommandPattern
{
    public class Withdraw : ITransaction
    {
        private readonly Account _account;
        private readonly decimal _amount;

        public int ID { get; set; }
        public DateTime CreatedOn { get; set; }
        public Enum.CommandState Status { get; set; }

        public Withdraw(int ID, Account account, decimal amount)
        {
            ID = ID;
            CreatedOn = DateTime.Now;

            _account = account;
            _amount = amount;

            Status = Enum.CommandState.Unprocessed;
        }

        public void Execute()
        {
            if (_account.Balance >= _amount)
            {
                _account.Balance -= _amount;
                Status = Enum.CommandState.ExecuteSucceeded;
            }
            else
                Status = Enum.CommandState.ExecuteFailed;
        }

        public void Undo()
        {
            _account.Balance += _amount;
            Status = Enum.CommandState.UndoSucceeded;
        }
    }
}