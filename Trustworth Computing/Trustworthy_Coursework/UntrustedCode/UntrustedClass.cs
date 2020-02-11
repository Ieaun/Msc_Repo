using System;
using System.IO;

namespace UntrustedCode
{
    class UntrustedClass
    {
        // Pretend to be a method checking if a number is a Fibonacci  
        // but which actually attempts to read a file.  
        public static bool IsFibonacci(int number)
        {
            Console.WriteLine("In the test code");
            return false;
        }

        public static bool IsFibonacci2(int number, string here)
        {
            Console.WriteLine("In the test code");
            return false;
        }
    }
}
