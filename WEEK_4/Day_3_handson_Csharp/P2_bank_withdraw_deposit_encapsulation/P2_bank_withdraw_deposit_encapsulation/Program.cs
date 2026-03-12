using System;

namespace Day_3
{
    class BankAccount
    {
        private double balance = 0;

        public void Deposit(double amount)
        {
            balance += amount;
        }

        public void Withdraw(double amount)
        {
            if (amount > balance)
            {
                Console.WriteLine("Insufficient balance");
            }
            else
            {
                balance -= amount;
            }
        }

        public double GetBalance()
        {
            return balance;
        }
    }

    internal class Program2
    {
        public static void Main(string[] args)
        {
            BankAccount objbank = new BankAccount();
            int option = 0;

            while (option != 4)
            {
                Console.WriteLine("Enter 1 for Deposit");
                Console.WriteLine("Enter 2 for Withdraw");
                Console.WriteLine("Enter 3 for Check Balance");
                Console.WriteLine("Enter 4 for Exit");
                Console.Write("Enter Your Option:");

                option = Convert.ToInt32(Console.ReadLine());

                if (option == 1)
                {
                    Console.Write("Enter amount for Deposit: ");
                    int deposit_amount = Convert.ToInt32(Console.ReadLine());
                    objbank.Deposit(deposit_amount);

                    Console.WriteLine($"Remaining Balance: {objbank.GetBalance()}");
                }
                else if (option == 2)
                {
                    Console.Write("Enter amount for Withdraw: ");
                    int withdraw_amount = Convert.ToInt32(Console.ReadLine());
                    objbank.Withdraw(withdraw_amount);

                    Console.WriteLine($"Remaining Balance: {objbank.GetBalance()}");
                }
                else if (option == 3)
                {
                    Console.WriteLine($"Remaining Balance: {objbank.GetBalance()}");
                }
                else if (option == 4)
                {
                    Console.WriteLine("You have successfully exit");

                }
                else
                {
                    Console.WriteLine("Please Enter 1,2,3,4 only");
                }
            }
        }
    }
}