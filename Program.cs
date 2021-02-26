using System;
using System.Collections.Generic;

namespace RPC
{
    class Program
    {
        static void Main(string[] args) 
        {
            string[] text = FileWork.ReadFile();

            int minRange = Convert.ToInt32(text[0]);
            int maxRange = Convert.ToInt32(text[1]);
            string example = text[2];

            for (int i = minRange; i <= maxRange; i++)
            {
                RPN.splitExample(example, i);
            }
        }
    }
}
