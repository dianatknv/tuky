using System;

namespace Primability
{
    /// <summary>
    /// Prime.
    /// </summary>
    public static class Prime
    {
        /// <summary>
        /// Chekcking if the number prime or not
        /// </summary>
        /// <param name="x">entered number</param>
        /// <returns>prime number</returns>
        public static bool IsPrime(int x)
        {
            bool res = true;
            if (x == 1)
                res = false;
            if (x == 2)
                res = true;
            for (int i = 2; i <= Math.Sqrt(x); ++i)
            {
                if(x % i == 0)
                {
                    res = false;
                    break;
                }
            }
            return res;
        }
    }

    /// <summary>
    /// Program.
    /// </summary>
    class Program
    {
        /// <summary>
        /// The entry point of  the program, where the program control starts and ends
        /// </summary>
        /// <param name="args">The command-line arguments</param>
    

        static void Main(string[] args)
        {
        /// <summary>
        /// Entering numbers and showing on the console only prime numbers.
        /// </summary>
        /// <param name="str">Entered string of numbers</param>
        /// <param name="words">Numbers splitted with the ' '</param>
        /// <param name= "l">Count of all numbers</param>
        /// <returns>only prime numbers</returns>

            string str = Console.ReadLine();
            string[] values = str.Split(' ');
            int n = values.Length;
            for (int i = 0; i < n; ++i)
            {
                if (Prime.IsPrime(int.Parse(values[i]))) 
                {
                    Console.WriteLine(values[i]);
                }
            }
            Console.ReadKey();
        }
    }
}
