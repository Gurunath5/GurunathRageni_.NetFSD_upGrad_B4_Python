
/*

Level - 1 Problem 2: Stack - Based Undo System
Scenario:
Design a simple text editor undo feature using Stack (LIFO principle).
Requirements:
-Implement stack using arrays.
-Support push(add action) and pop(undo action).
-Display current state after each operation.
Technical Constraints:
-Only array - based stack implementation.
-Must follow LIFO order strictly.
- Handle empty stack condition.
Sample Input:
Actions: Type A, Type B, Type C, Undo, Undo
Sample Output:
Current State After Operations: Type A
Expectations:
-Correct LIFO implementation.
-Proper error handling.
-Clear logic structure.


Learning Outcome:
-Understand stack operations.
-Learn LIFO principle application.
- Implement stack using arrays.*/

using System;
using System.Collections.Generic;
using System.Text;

namespace P2_Stack_based_undo
{
    internal class Class1
    {
        using System;

namespace StackUndoSystem
    {
        class Program
        {
            static void Main(string[] args)
            {
                string[] stack = new string[10]; // Array stack
                int top = -1; // Stack pointer

                // Push function
                void Push(string action)
                {
                    if (top == stack.Length - 1)
                    {
                        Console.WriteLine("Stack Overflow");
                        return;
                    }

                    top++;
                    stack[top] = action;
                    Console.WriteLine("Action Performed: " + action);
                }

                // Pop function (Undo)
                void Pop()
                {
                    if (top == -1)
                    {
                        Console.WriteLine("Nothing to Undo (Stack Empty)");
                        return;
                    }
                    Console.WriteLine("Undo: " + stack[top]);
                    top--;
                }
                // Display current state
                void Display()
                {
                    if (top == -1)
                    {
                        Console.WriteLine("Editor Empty");
                        return;
                    }
                    Console.Write("Current State: ");
                    for (int i = 0; i <= top; i++)
                    {
                        Console.Write(stack[i] + " ");
                    }
                    Console.WriteLine();
                }

                // Sample operations
                Push("Type A");
                Push("Type B");
                Push("Type C");

                Pop(); // Undo
                Pop(); // Undo

                Display();
            }
        }
    }
}
}
