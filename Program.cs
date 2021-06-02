using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;


namespace iEngine
{
    class Program
    {
        public static string RemoveWhiteSpace(string s)
        {
            List<char> result = s.ToList();
            result.RemoveAll(c => c == ' ');
            s = new string(result.ToArray());
            return s;
        }

        static void Main(string[] args)
        {
            foreach (string arg in args)
            {
                Console.WriteLine(arg);
            }
            Console.ReadLine();

            string path = Directory.GetCurrentDirectory();
            string newPath = Path.GetFullPath(Path.Combine(path, @"..\..\..\tests", args[1]));

            string[] file = System.IO.File.ReadAllLines(String.Format(newPath));
            string tell = file[1];
            string ask = file[3];

            if (args[0] == "TT")
            {
                TT_Check_All TT = new TT_Check_All(ask, tell);
                Console.WriteLine(TT.Execute());
            }
            else if (args[0] == "FC")
            {
                ForwardChaining FC = new ForwardChaining(ask, tell);
                Console.WriteLine(FC.Execute());
            }
            else if (args[0] == "BC")
            {
                BackwardChaining BC = new BackwardChaining(ask, tell);
                Console.WriteLine(BC.Execute());
            }
            else if (args[0] == "h")
            {
                Console.WriteLine("Help:");
                Console.WriteLine("Format: <filename> <Logic Method> ");
                Console.WriteLine("Logic methods are as follows: ");
                Console.WriteLine(" TT: Truth Table");
                Console.WriteLine(" FC: Forward Chaining");
                Console.WriteLine(" TT: Backward Chaining");




            }
            else
            {
                Console.WriteLine("ERROR: PLEASE FORMAT CORRECTLY <filename> <Logic Method>");

            }



        }
    }
}
