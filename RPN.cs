using System;
using System.Collections.Generic;
using System.Text;
using RPC.Logic;    

namespace RPC
{
    class RPN
    {
        public static void splitExample(string example, int num)
        {
            string newExample = example.Replace("x", $"{num}");

            List<string> partsExmple = CreateRPN.MoveInList(newExample);

            if (!CreateRPN.CheckBrackets(partsExmple))
            {
                Console.WriteLine("ошибка");
            }
            if (!CreateRPN.CheckOperation(partsExmple))
            {
                Console.WriteLine("ошибка");
            }

            CreateRPN.FindExmpleInBrackets(partsExmple);

            string RPN = CreateRPN.ParseExpression(newExample);

            FileWork.WriteFile(RPN, Convert.ToString(num));
        }
    }
}
