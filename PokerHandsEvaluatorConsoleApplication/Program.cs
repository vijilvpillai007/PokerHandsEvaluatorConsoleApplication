using PokerHandsEvaluatorConsoleApplication.PokerHandBusiness;
using System;
using PokerHandsEvaluatorConsoleApplication.PokerHandsInterfaces;
using Unity;

namespace PokerHandsEvaluatorConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var unityContainer = new UnityContainer();
            unityContainer.RegisterType<IPokerHands, PokerHands>();
            Console.WriteLine("Please enter the Poker cards");
            string pokerHandsData = Convert.ToString(Console.ReadLine());
            try
            {
                var pokerHandsObject = unityContainer.Resolve<PokerHands>();
                string pokerHandType = pokerHandsObject.EvaluatePokerHands(pokerHandsData);
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
