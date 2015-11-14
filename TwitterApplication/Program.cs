using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwitterApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                System.Console.WriteLine("Error: Invalid Input... \nPlease enter arguments (1)User file & (2) Twitter file: ");
            }
            else
            {
                Twitter TwitterApp = new Twitter();

                TwitterApp.LoadFiles(args[0], args[1]);
            }
            //Console.Read();
        }
    }
}
