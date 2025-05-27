using System.Globalization;
using System.Linq.Expressions;
using Microsoft.VisualBasic;

namespace SimpleConsoleCalculator
{
    internal class Program
    {
        static void Main()
        {
            int Mode = 0;
            int[] num;
            while (true)
            {
                Console.WriteLine("Choose Mode: \n0.Exit\n1.Addition\n2.Substraction\n3.Multiplication\n4.Division");
                try{
                    Console.Write("Input mode number : ");
                    Mode = Int32.Parse(Console.ReadLine());
                }
                catch(FormatException){
                    Console.WriteLine("Your Input isn't a number, try again");
                }
                switch (Mode)
                {
                    case 0:
                        Console.WriteLine("Thank you for using this program");
                        return;
                    case 1:
                        num = numbers();
                        Console.WriteLine(Addition(num[0], num[1]));
                        break;
                    case 2:
                        num = numbers();
                        Console.WriteLine(Substraction(num[0], num[1]));
                        break;
                    case 3:
                        num = numbers();
                        Console.WriteLine(Multiplication(num[0], num[1]));
                        break;
                    case 4:
                        num = numbers();
                        Console.WriteLine(Division(num[0], num[1]));
                        break;
                    default:
                        Console.WriteLine("Mode not recognized");
                        break;
                }
            }
        }
        static string Addition(int a, int b) 
        {
            Console.WriteLine($"first number is {a} and second number is {b}");
            int c = a + b;
            return $"Addition between {a} and {b} is {c}\n";
        }
        static string Substraction(int a,int b) 
        {
            Console.WriteLine($"first number is {a} and second number is {b}");
            int c = a - b;
            return $"Substraction between {a} and {b} is {c}\n"; 
        }
        static string Multiplication(int a,int b)
        {
            Console.WriteLine($"first number is {a} and second number is {b}");
            int c = a * b;
            return $"Multipication between {a} and {b} is {c}\n"; 
        }
        static string Division(int a,int b)
        {
            Console.WriteLine($"first number is {a} and second number is {b}");
            decimal c = a / b;
            c = Decimal.Round(c, 2);
            return $"Division between {a} and {b} is {c}\n";
        }
        public static int[] numbers()
        {
            int a = 0, b = 0;
            try
            {
                Console.Write("Input your first number: ");
                a = Int32.Parse(Console.ReadLine());
                Console.Write("Input your first number: ");
                b = Int32.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Your Input isn't a number, try again");
            }
            return [a,b];
        }
    }
}
