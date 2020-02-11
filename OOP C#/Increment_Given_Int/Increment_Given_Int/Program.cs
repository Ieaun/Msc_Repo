using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Increment_Given_Int
{
    class Program
    {
        public static int IncrementResult(int i)
        {
            int Result = i + 1 ;
            return Result;
        }

        public static bool Check_if_Number(string Data)
        {
            if (int.TryParse(Data, out int Value))
                return true;
            else
                return false;
        }

        static int GetNumber()
        {
            string Input;
            bool Is_Number = false;
            int Number = 0 ;


            while (Is_Number == false)
            {
                Console.WriteLine("Please enter a number");
                Input = Console.ReadLine();
                Is_Number = Program.Check_if_Number(Input);

                if (Is_Number)
                    Number = int.Parse(Input);
                else
                    Console.WriteLine("Please enter a number not a letter or illegal character");
            }

            return Number;
                
        }


        static void Main(string[] args)
        {
            int Number = 0;

            Console.WriteLine("This program increments a number given");

            while (true)
            {
                Number = Program.GetNumber();
                Number = Program.IncrementResult(Number);

                Console.WriteLine(string.Format("Incremented result is {0}", Number.ToString()));
                Console.WriteLine(" ");
                Console.WriteLine(" ");
            }

        }
    }
}
