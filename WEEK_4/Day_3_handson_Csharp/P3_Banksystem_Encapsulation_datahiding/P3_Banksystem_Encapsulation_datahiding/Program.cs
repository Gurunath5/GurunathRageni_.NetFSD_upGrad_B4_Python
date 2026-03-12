using System;

namespace BankApp
{
    class BankAccount
    {
        // Private fields (data hiding)
        private int accountNumber;
        private double balance;

        // Property for Account Number
        public int AccountNumber
        {
            get { return accountNumber; }
            set { accountNumber = value; }
        }

        // Property for Balance (read-only outside)
        public double Balance
        {
            get { return balance; }
        }

        // Deposit Method
        public void Deposit(double amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Invalid deposit amount");
            }
            else
            {
                balance += amount;
                Console.WriteLine("Amount Deposited: " + amount);
                Console.WriteLine("Current Balance = " + balance);
            }
        }

        // Withdraw Method
        public void Withdraw(double amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Invalid withdrawal amount");
            }
            else if (amount > balance)
            {
                Console.WriteLine("Insufficient Balance");
            }
            else
            {
                balance -= amount;
                Console.WriteLine("Amount Withdrawn: " + amount);
                Console.WriteLine("Current Balance = " + balance);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            BankAccount account = new BankAccount();

            account.AccountNumber = 101;

            Console.Write("Enter Deposit Amount: ");
            double deposit = Convert.ToDouble(Console.ReadLine());
            account.Deposit(deposit);

            Console.Write("Enter Withdraw Amount: ");
            double withdraw = Convert.ToDouble(Console.ReadLine());
            account.Withdraw(withdraw);

            Console.WriteLine("Final Balance = " + account.Balance);
        }
    }
}