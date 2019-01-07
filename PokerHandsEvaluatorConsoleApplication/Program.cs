using System;
using PokerHandsEvaluatorConsoleApplication.PokerHandBusiness;

namespace PokerHandsEvaluatorConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter the Poker cards");
            string pokerHandsData = Convert.ToString(Console.ReadLine());
            try
            {
                string pokerHandType = PokerHands.EvaluatePokerHands(pokerHandsData);
                Console.WriteLine(pokerHandType);
                Console.ReadLine();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Console.ReadLine();
            }
        }
    }
}
