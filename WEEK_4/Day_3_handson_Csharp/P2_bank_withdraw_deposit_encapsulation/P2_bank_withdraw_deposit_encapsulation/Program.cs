
/*Level-2 Problem 1: Bank Account with Encapsulation
Scenario:
A bank wants to manage customer accounts securely using encapsulation.
Requirements:
1. Create class BankAccount.
2. Private field: balance.
3. Public methods: Deposit(double amount), Withdraw(double amount).
4. Method GetBalance() to return balance.
5. Prevent withdrawal if insufficient balance.
Technical Constraints:
1. Balance must be private.
2. Access balance only through public methods.
3. Use appropriate return types.
Expectations:
Proper use of encapsulation and object-oriented principles.
Learning Outcome:
Understand encapsulation, access modifiers, and secure data handling.
Sample Input: 
Deposit 1000, Withdraw 300
Sample Output: 
Current Balance = 700*/
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