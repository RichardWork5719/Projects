global using global::System;
global using global::System.Collections;
global using global::System.Collections.Generic;
global using global::System.Collections.Immutable;
global using global::System.Diagnostics;
global using global::System.Drawing;
global using global::System.Drawing.Imaging;
global using global::System.IO;
global using global::System.Linq;
global using global::System.Net;
global using global::System.Net.Http;
global using global::System.Net.Sockets;
global using global::System.Runtime.CompilerServices;
global using global::System.Runtime.InteropServices;
global using global::System.Text;
global using global::System.Threading;
global using global::System.Threading.Tasks;
global using global::System.Windows;
global using global::System.Windows.Input;
global using global::System.Xml;
global using global::System.Xml.Linq;

using MessingWithCode.Imtiaz;
using MessingWithCode_New_;
using System.Net.Mail;
//using System.Windows.Forms;
//using static System.Net.Mime.MediaTypeNames; this is apperently for emails? HHHMMMMMMMMMMMMMMMMMMMMMMMMMM!!!!!!!! interesting...



/// byte    |   1 byte      |   Represents an 8-bit unsigned integer
/// sbyte	|	1 byte		|	from -128 to 127
/// short   |   2 bytes     |   Represents a 16-bit signed integer
/// int     |   4 bytes     |   Stores whole numbers from -2,147,483,648 to 2,147,483,647
/// long    |	8 bytes     |	Stores whole numbers from -9,223,372,036,854,775,808 to 9,223,372,036,854,775,807   |   can be defined by putting an 'L'/'l' at the end like: long num = 1000000000L;
/// float 	|   4 bytes 	|   Stores fractional numbers. Sufficient for storing 6 to 7 decimal digits             |   can be defined by putting an 'F'/'f' at the end like: float num = 100.69F;
/// double 	|   8 bytes 	|   Stores fractional numbers. Sufficient for storing 15 decimal digits                 |   can be defined by putting an 'D'/'d' at the end like: double num = 100.69D;
/// decimal |   16 bytes    |   Represents a 128-bit precise decimal value, suitable for financial calculations
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
/// 
/// if i ever want to alphabetically sort a List<string>: https://www.webdevtutor.net/blog/c-sharp-sort-list-of-strings-alphabetically



namespace MessingWithCode
{
    class Program
    {
        static void PrintOverwritableMessage(string overwritableMessage = "Penis Wax")
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
            { // i wonder if i could make an app that goes to time.windows.com and gets the time itself without windows doing it automatically
                //Thread.Sleep(5000);         //

                MouseControler.AFKMouse();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            PrintOverwritableMessage("Main thread finished");
            Console.ReadKey();
        }
    }
}