global using global::System;
global using global::System.Collections.Generic;
global using global::System.IO;
global using global::System.Linq;
global using global::System.Text;
global using global::System.Net;
global using global::System.Net.Http;
global using global::System.Net.Sockets;
global using global::System.Threading;
global using global::System.Threading.Tasks;



/// int     |   4 bytes     |   Stores whole numbers from -2,147,483,648 to 2,147,483,647
/// long    |	8 bytes     |	Stores whole numbers from -9,223,372,036,854,775,808 to 9,223,372,036,854,775,807   |   can be defined by putting an 'L'/'l' at the end like: long num = 1000000000L;
/// float 	|   4 bytes 	|   Stores fractional numbers. Sufficient for storing 6 to 7 decimal digits             |   can be defined by putting an 'F'/'f' at the end like: float num = 100.69F;
/// double 	|   8 bytes 	|   Stores fractional numbers. Sufficient for storing 15 decimal digits                 |   can be defined by putting an 'D'/'d' at the end like: double num = 100.69D;
/// converting between number types: https://www.w3schools.com/cs/cs_type_casting.php
/// 
/// Console.WriteLine($"Hello, World!"); // Console.Write() writes the text but without making a new line '\n'
/// 
/// public      |   The code is accessible for all classes
/// private     |   The code is only accessible within the same class
/// protected   |   The code is accessible within the same class, or in a class that is inherited from that class. You will learn more about inheritance in a later chapter
/// internal    |   The code is only accessible within its own assembly, but not from another assembly. You will learn more about this in a later chapter
/// 
/// file read/write: https://www.w3schools.com/cs/cs_files.php


/*
namespace MessingWithCode
{
    class Program
    {
        static void PrintOverwritableMessage(string overwritableMessage = "e")
        {
            foreach (char letter in overwritableMessage)
            {
                overwritableMessage += '\b';
            }
            Console.Write(overwritableMessage);
        }

        private static void Main(string[] args)
        {
            try
            {
                Console.WriteLine($"Hello, World!"); // Console.Write() writes the text but without making a new line '\n'
                PrintOverwritableMessage("longass message made for testing");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.ReadLine();
        }
    }
}
*/
