/*Level-2 Problem 2: Bank Account Management System
Scenario:
A bank wants to develop a simple console-based application to manage customer bank accounts. The system should protect account balance information and allow controlled access using properties.
Requirements:
1. Create a BankAccount class with private fields for account number and balance.
2. Use properties to allow controlled access to account number and balance.
3. Implement Deposit and Withdraw methods with proper validation.
4. Prevent withdrawal if balance is insufficient.
Technical Constraints:
• Use private fields with public properties.
• Apply encapsulation and data hiding.
• No direct access to balance field from outside the class.
Expectations:
• Demonstrate correct use of access modifiers.
• Validate negative deposit or withdrawal amounts.
• Display updated balance after each transaction.
Learning Outcome:
• Understand encapsulation using properties.
• Apply data hiding effectively.
• Implement validation logic inside class methods.
Sample Input: 
Deposit = 5000, Withdraw = 2000
Sample Output: 
Current Balance = 3000*/
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