using System;
using System.Collections.Generic;
using System.IO;


namespace RPC
{
    public class FileWork
    {
        public static string[] ReadFile()
        {
            File.WriteAllText("output.txt", string.Empty);
            return File.ReadAllLines("input.txt");
        }
        public static void WriteFile(string RPN,string num)
        {
            List<string> strInOutput = new List<string>(File.ReadAllLines("output.txt"));

            strInOutput.Add( $"| {num} | {RPN} |");

            string str = "";
            for (int i = 0; i < num.Length + RPN.Length + 7; i++)
            {
                str += ("-");
            }
            strInOutput.Add(str);

            File.WriteAllLines("output.txt", strInOutput);
        }
    }
}
