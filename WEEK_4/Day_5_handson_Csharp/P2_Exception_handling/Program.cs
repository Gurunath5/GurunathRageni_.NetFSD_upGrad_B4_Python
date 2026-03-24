/*Level-2 Problem 1: Bank Withdrawal System with Custom Exception
Scenario:
A bank wants to implement a withdrawal system that throws a custom exception when a customer attempts to withdraw more money than available in their account.
Requirements:
1.Create a class BankAccount with a private field balance.
2.Create a method Withdraw(double amount).
3.Create a custom exception class InsufficientBalanceException.
4.Throw the custom exception if the withdrawal amount exceeds the balance.
5.Handle the exception in the main program using try-catch.
6.Ensure proper cleanup using a finally block.
Technical Constraints:
Use custom exception classes by inheriting from Exception.
Implement error propagation by throwing exceptions from methods.
Handle exceptions in the calling method.
Expectations:
Demonstrate creation and usage of custom exceptions.
Implement proper exception handling in calling code.
Display user-friendly error messages.
Learning Outcome:
Learn how to create custom exceptions.
Understand throw and error propagation.
Apply structured exception handling in real-world scenarios.
Sample Input:
Balance = 3000
 Withdraw = 5000
Sample Output:
Error: Withdrawal amount exceeds available balance
*/
using System;

namespace BankSystem
{
    // 1. Custom Exception
    public class InsufficientBalanceException : Exception
    {
        public InsufficientBalanceException(string message) : base(message)
        {
        }
    }

    // 2. BankAccount Class
    class BankAccount
    {
        private double balance;

        public BankAccount(double initialBalance)
        {
            balance = initialBalance;
        }

        public void Withdraw(double amount)
        {
            if (amount > balance)
            {
                // 3. Throw custom exception
                throw new InsufficientBalanceException("Withdrawal amount exceeds available balance");
            }

            balance -= amount;
            Console.WriteLine($"Withdrawal Successful! Remaining Balance: {balance}");
        }
    }

    // 4. Main Program
    class Program
    {
        static void Main()
        {
            Console.Write("Enter Account Balance: ");
            double balance = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter Withdrawal Amount: ");
            double amount = Convert.ToDouble(Console.ReadLine());

            BankAccount account = new BankAccount(balance);

            try
            {
                account.Withdraw(amount);
            }
            catch (InsufficientBalanceException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Transaction process completed.");
            }

            Console.WriteLine("\nProgram continues safely...");
            Console.ReadLine();
        }
    }
}