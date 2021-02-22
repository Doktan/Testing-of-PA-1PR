using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingPr2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Insert X");
            int X;
            if (!Int32.TryParse(Console.ReadLine(), out X))
            {
                Console.WriteLine("Input Error");
            }
            else
            {
                if (X > 0 && X <= 6)
                {
                    Console.WriteLine(5 * X - 1);
                }
                else
                {
                    if (X >= 10 && X < 12)
                    {
                        Console.WriteLine(2 * X * X + X + 1);
                    }
                    else
                    {
                        Console.WriteLine("Input Error");
                    }
                }
            }
            Console.ReadKey();
        }
    }
}
