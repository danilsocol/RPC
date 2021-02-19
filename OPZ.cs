using System;
using System.Collections.Generic;
using System.Text;

namespace ОПЗ
{
    class OPZ
    {
        public static void splitExample(string example, int num)
        {
            string newExample = example.Replace("x", $"{num}");

          //  List<string> partsExmple = MoveInList(newExample);

            //if (!CheckBrackets(partsExmple))
            //{
            //    Console.WriteLine("ошибка");
            //}
            //if (!CheckOperation(partsExmple))
            //{
            //    Console.WriteLine("ошибка");
            //}

           // FindExmpleInBrackets(partsExmple);

           string RPN = ParseExpression(newExample);

            FileWork.WriteFile(RPN,Convert.ToString(num));
        }
        //static List<string> MoveInList(string example)
        //{
        //    List<string> Parts = new List<string>();

        //    string[] partsExample = example.Split(' ');

        //    for(int i = 0; i < partsExample.Length; i++)
        //    {
        //        Parts.Add(partsExample[i]);
        //    }
        //    return Parts;
        //}
        //static void FindExmpleInBrackets(List<string> partsExmple)
        //{
        //    List<string> newExmple = new List<string>();

        //    int openBrackets = 0;
        //    int closeBrackets = -1;

        //    for (int i = 0; i < partsExmple.Count; i++)
        //    {
        //        if (partsExmple[i] == "(")
        //        {
        //            openBrackets = i;
        //        }
        //        if (partsExmple[i] == "(" && closeBrackets == -1)
        //        {
        //            closeBrackets = i;
        //        }
        //    }

        //    for(int i = openBrackets; i< closeBrackets; i++)
        //    {
        //        newExmple.Add(partsExmple[i]);
        //    }
        //}

        //static bool CheckBrackets(List<string> partsExmple)
        //{
        //    int countOpenBrackets = 0;
        //    int countCloseBrackets = 0;

        //    for (int i = 0; i < partsExmple.Count; i++)
        //    {
        //        if (partsExmple[i] == "(")
        //        {
        //            countOpenBrackets++;
        //        }
        //        if (partsExmple[i] == ")")
        //        {
        //            countCloseBrackets++;
        //        }
        //    }

        //    return countOpenBrackets == countCloseBrackets;
        //}

        //static bool CheckOperation(List<string> partsExmple)
        //{
        //    int numOperation =partsExmple.Count - 2;

        //    List<string> accessOperations = new List<string>() { "*", "/", "+", "-" };

        //    while (numOperation != 1)
        //    {
        //        for(int i =0;i<accessOperations.Count;i++)
        //        {
        //            if (partsExmple[numOperation] == accessOperations[i] )
        //            {
        //                break;
        //            }

        //            if (i == accessOperations.Count)
        //            {
        //                return false;
        //            }
        //        }
        //        numOperation -= 2;
        //    }
        //    return true;
        //}
        static string ParseExpression(string example)
        {
            List<string> str = new List<string>(example.Split(new char[] { ' ' }));

            List<string> operationsLight = new List<string>();
            List<string> operationsHard = new List<string>();

            List<string> nums = new List<string>();
            List<string> arrRPN = new List<string>();

            for (int i = 0; i < str.Count; i++)
            {
                if (OperationsHard(str, i))
                {
                    operationsHard.Add(str[i]);
                }
                else if (OperationsLight(str, i))
                {
                    operationsLight.Add(str[i]);
                }

                else
                {
                    nums.Add(str[i]);
                }
            }

            CreatedRPN(arrRPN, nums,  operationsHard,  operationsLight,  str);

            string RPN = "";
            for (int i = 0; i < arrRPN.Count - 1; i++)
            {
                RPN += Convert.ToString($"{arrRPN[i]},");
            }
            RPN += Convert.ToString($"{arrRPN[arrRPN.Count - 1]}");

            return RPN;
        }

        static void CreatedRPN(List<string> arrRPN, List<string> nums,  List<string> operationsHard, List<string> operationsLight, List<string> str)
        {
            int numArrNums = 0;
            int numArrOperationsHard = 0;
            int numArrOperationsLight = 0;

            for (int i = 1; i < nums.Count + operationsLight.Count + operationsHard.Count; i += 2)
            {
                if (arrRPN.Count == 0)
                {
                    arrRPN.Add(nums[numArrNums]);
                    numArrNums++;
                }

                if (arrRPN.Count == nums.Count + operationsLight.Count + operationsHard.Count)
                    break;

                if (OperationsHard(str, i))
                {
                    RPNHardOperation(arrRPN, nums, numArrNums, operationsHard, numArrOperationsHard);
                    numArrNums++;
                    numArrOperationsHard++;
                }
                else
                {
                    arrRPN.Add(nums[numArrNums]);
                    numArrNums++;

                    if (i < nums.Count + operationsLight.Count + operationsHard.Count - 2)
                        if (OperationsHard(str, i + 2))
                        {
                            i += 2;
                            RPNHardAndLightOperation(arrRPN, nums, numArrNums, operationsHard, numArrOperationsHard, str, i, operationsLight);
                            numArrNums++;
                            numArrOperationsHard++;
                        }

                    arrRPN.Add(operationsLight[numArrOperationsLight]);
                    numArrOperationsLight++;
                }
            }
        }
        static bool OperationsHard(List<string> str, int i)
        {
            return str[i] == "*" || str[i] == "/";
        }
        static bool OperationsLight(List<string> str, int i)
        {
            return str[i] == "+" || str[i] == "-";
        }

        static void RPNHardOperation(List<string> arrRPN, List<string> nums, int numArrNums, List<string> operationsHard, int numArrOperationsHard)
        {
            arrRPN.Add(nums[numArrNums]);
            arrRPN.Add(operationsHard[numArrOperationsHard]);
        }

        static void RPNHardAndLightOperation(List<string> arrRPN, List<string> nums, int numArrNums, List<string> operationsHard, int numArrOperationsHard, List<string> str, int i, List<string> operationsLight)
        {
            arrRPN.Add(nums[numArrNums]);
            numArrNums++;

            arrRPN.Add(operationsHard[numArrOperationsHard]);
            numArrOperationsHard++;

            if (i + 2 < nums.Count + operationsLight.Count + operationsHard.Count && OperationsHard(str, i + 2))
            {
                i += 2;
                RPNHardAndLightOperation(arrRPN, nums, numArrNums, operationsHard, numArrOperationsHard, str, i, operationsLight);
            }
        }
    }
}
