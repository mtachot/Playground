using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Playground.CommandPattern;

namespace PlaygroundTesting
{
    [TestClass]
    public class TestTransactionManager
    {
        [TestMethod]
        public void Test_AllTransactionsSuccessful()
        {
            TransactionManager transactionManager = new TransactionManager();

            Account anniesAccount = new Account("Annie", 0);

            Deposit deposit = new Deposit(1, anniesAccount, 100);
            transactionManager.AddTransaction(deposit);

            // Command has been added to the queue, but not yet executed
            Assert.IsTrue(transactionManager.HasPendingTransactions);
            Assert.AreEqual(anniesAccount.Balance, 0);

            // This executes the commands
            transactionManager.ProcessPendingTransactions();
            Assert.IsFalse(transactionManager.HasPendingTransactions);
            Assert.AreEqual(anniesAccount.Balance, 100);

            // Add a withdrawal, apply it and check the balance changed
            Withdraw withdraw = new Withdraw(2, anniesAccount, 50);
            transactionManager.AddTransaction(withdraw);
            transactionManager.ProcessPendingTransactions();
            Assert.IsFalse(transactionManager.HasPendingTransactions);
            Assert.AreEqual(anniesAccount.Balance, 50);

            // Test the Undo
            transactionManager.UndoTransactionNumber(2);
            Assert.IsFalse(transactionManager.HasPendingTransactions);
            Assert.AreEqual(anniesAccount.Balance, 100);
        }

        [TestMethod]
        public void Test_OverdraftRemainsInPendingtransactions()
        {
            TransactionManager transactionManager = new TransactionManager();

            // Create an account with a balance of 75
            Account bobsAccount = new Account("Bob", 75);

            // The first command is a withdrawal larger than the account's balance
            // It will not be executed, because of the check in Withdraw.Execute
            // The deposit will be successful
            transactionManager.AddTransaction(new Withdraw(1, bobsAccount, 100));
            transactionManager.AddTransaction(new Deposit(2, bobsAccount, 75));
            transactionManager.ProcessPendingTransactions();

            // The withdrawal of 100 was not completed
            // because there was not eough money in the account
            // So, it is still pending
            Assert.IsTrue(transactionManager.HasPendingTransactions);
            Assert.AreEqual(bobsAccount.Balance, 150);

            // The pending transactions (the withdrawal of 100) should execute now
            transactionManager.ProcessPendingTransactions();
            Assert.IsFalse(transactionManager.HasPendingTransactions);
            Assert.AreEqual(bobsAccount.Balance, 50);

            // Test the Undo
            transactionManager.UndoTransactionNumber(2);

            // The Undo failed, because there is not enough money on the account to undo
            Assert.IsTrue(transactionManager.HasPendingTransactions);
            Assert.AreEqual(bobsAccount.Balance, 50);

            transactionManager.UndoTransactionNumber(1);

            // The previous Undo (for transaction ID 2) is still pending
            // But we successfully undid transaction ID 1
            Assert.IsTrue(transactionManager.HasPendingTransactions);
            Assert.AreEqual(bobsAccount.Balance, 150);

            // This should re-do the failed Undo for transaction ID 2
            transactionManager.ProcessPendingTransactions();
            Assert.IsFalse(transactionManager.HasPendingTransactions);
            Assert.AreEqual(bobsAccount.Balance, 75);
        }

        [TestMethod]
        public void Test_Transfert()
        {
            TransactionManager transactionManager = new TransactionManager();

            Account checking = new Account("Charlie", 1000);
            Account savings = new Account("Dimitri", 100);

            transactionManager.AddTransaction(new Transfert(1, checking, savings, 750));
            transactionManager.ProcessPendingTransactions();

            Assert.AreEqual(checking.Balance, 250);
            Assert.AreEqual(savings.Balance, 850);

            // Test the Undo
            transactionManager.UndoTransactionNumber(1);
            Assert.IsFalse(transactionManager.HasPendingTransactions);
            Assert.AreEqual(checking.Balance, 1000);
            Assert.AreEqual(savings.Balance, 100);
        }
    }
}