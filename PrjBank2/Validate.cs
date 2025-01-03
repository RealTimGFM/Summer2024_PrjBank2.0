using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PrjBankATM
{
    internal class Validate
    {
        public static Int16 validInt16(string question)
        {
            Int16 resultInt;
            bool isIntValid;
            Console.Write("\n" + question);
            string input = Console.ReadLine();
            isIntValid = Int16.TryParse(input, out resultInt);
            if (isIntValid == false)
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
            return resultInt;
        }                        //IP Man
        public static decimal validDec(string question)
        {
            decimal resultDec;
            bool isDecValid;
            do
            {
                Console.Write(question);
                string input = Console.ReadLine();
                isDecValid = Decimal.TryParse(input, out resultDec);
                if (isDecValid == false)
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
                if (resultDec < 0)
                {
                    Console.WriteLine("Invalid input. Number must be bigger or equal to 0. ");
                }
            } while (isDecValid == false || resultDec < 0);
            return resultDec;
        }                        //IP Man
    }
}
