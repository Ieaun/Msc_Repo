using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sum_of_two_numbers
{
    class Program
    {
        static int Sum_of_Two(int A, int B)
        {
            int Sum = A + B;
            return Sum;
        }


        static void Main(string[] args)
        {
            int A, B, Sum;

            Console.WriteLine(@"This program adds 2 values together and gives you the final total");

            Console.WriteLine(@"Enter value for A:");
            A = int.Parse(Console.ReadLine());
            Console.WriteLine(@"Enter value for B:");
            B = int.Parse(Console.ReadLine());

            Sum = Program.Sum_of_Two(A, B);


            Console.WriteLine(string.Format("The sum is :{0}", Sum.ToString()));

            Console.Read();

        }
    }
}
