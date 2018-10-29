using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.CommandPattern
{
    public class TransactionManager
    {
        private readonly List<ITransaction> _transactions = new List<ITransaction>();

        public bool HasPendingTransactions
        {
            get
            {
                return _transactions.Any(x =>
                    x.Status == Enum.CommandState.Unprocessed ||
                    x.Status == Enum.CommandState.ExecuteFailed ||
                    x.Status == Enum.CommandState.UndoFailed);
            }
        }

        public void AddTransaction(ITransaction transaction)
        {
            _transactions.Add(transaction);
        }

        public void ProcessPendingTransactions()
        {
            // Execute transactions that are unpocessed, or had a previous Execute fail
            foreach (ITransaction transaction in _transactions.Where(x =>
                x.Status == Enum.CommandState.Unprocessed ||
                x.Status == Enum.CommandState.ExecuteFailed))
            {
                transaction.Execute();
            }

            // Retry the Undo, for transactions that had a previous Undo fail
            foreach (ITransaction transaction in _transactions.Where(x =>
                x.Status == Enum.CommandState.UndoFailed))
            {
                transaction.Undo();
            }
        }

        public void UndoTransactionNumber(int id)
        {
            // Get the Command object that has the passes ID
            // If it does not exist in _transactions, the result will be null
            ITransaction transaction = _transactions.FirstOrDefault(x => x.ID == id);

            if (transaction == null)
                throw new ArgumentException("Can only undo transaction that have been successfully executed.");

            // We have a valid transaction, so perform the Undo
            transaction.Undo();

            // Remove the transaction, if it was successfully completed
            if (transaction.Status == Enum.CommandState.UndoSucceeded)
                _transactions.Remove(transaction);
        }

        public void RemoveOldTransactions()
        {
            // Remove transactions that have been Executed, and are more than 15 days old
            _transactions.RemoveAll(x =>
                x.Status == Enum.CommandState.ExecuteSucceeded &&
                (DateTime.UtcNow - x.CreatedOn).Days > 15);
        }
    }
}