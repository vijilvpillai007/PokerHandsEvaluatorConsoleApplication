using PokerHandsEvaluatorConsoleApplication.Constants;
using PokerHandsEvaluatorConsoleApplication.ExceptionHelpers;
using System;
using System.Linq;
using PokerHandsEvaluatorConsoleApplication.PokerHandsInterfaces;

namespace PokerHandsEvaluatorConsoleApplication.PokerHandBusiness
{
    public class PokerHands:IPokerHands
    {
        public string EvaluatePokerHands(string pokerHandsData)
        {
            if (!CheckForPokerCardHandsDataIsValid(pokerHandsData))
            {
                throw new PokerHandsValidationException(PokerConstants.InvalidInputMessage);
            }
            var pokerHands = pokerHandsData.ToLower().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            if (CheckForPokerCardsIsOfTypeRoyalFlush(pokerHands))
            {
               return PokerConstants.RoyalFlush;
            }
            else
            {
                return  string.Empty;
            }
        }
        private bool CheckForPokerCardHandsDataIsValid(string pokerHandsData)
        {
            if (string.IsNullOrEmpty(pokerHandsData))
            {
                return false;
            }
            var pokerHands = pokerHandsData.ToLower().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            if (pokerHands == null)
            {
                return false;
            }
            bool isPokerHandsInputValid = CheckforPokerHandsCountAndDuplicateCards(pokerHands)
                                          && CheckForPokerCardsRanksIsValid(pokerHands)
                                          && CheckForPokerCardsSuitsIsValid(pokerHands);
            if (!isPokerHandsInputValid)
            {
                throw new PokerHandsValidationException(PokerConstants.InvalidInputLengthMessage);
            }
            return true;
        }

        private bool CheckforPokerHandsCountAndDuplicateCards(string[] pokerHandsData)
        {
            if (pokerHandsData.Length != 5)
            {
                throw new PokerHandsValidationException(PokerConstants.InvalidInputLengthMessage);
            }
            if (pokerHandsData.Distinct().Count() != 5)
            {
                throw new PokerHandsValidationException(PokerConstants.InvalidInputDuplicatesMessage);
            }
            if (pokerHandsData.Where(s => !string.IsNullOrEmpty(s) && s.Length == 2).Count() != 5)
            {
                throw new PokerHandsValidationException(PokerConstants.InvalidInputCardsPairMessage);
            }
            return true;
        }

        private bool CheckForPokerCardsRanksIsValid(string[] pokerHandsData)
        {
            bool isValidPokerCardsRank = pokerHandsData
                .Where(s => !string.IsNullOrEmpty(s)
                && (
                (Char.IsDigit(s[0]) && Convert.ToInt32(s[0].ToString()) > 1 && Convert.ToInt32(s[0].ToString()) < 10)
                || Enum.IsDefined(typeof(ValidPokerCardRanksEnums), s[0].ToString()))).Select(s => s).Count() == 5;
            if (!isValidPokerCardsRank)
            {
                throw new PokerHandsValidationException(PokerConstants.InvalidInputRanksMessage);
            }
            return true;
        }
        private bool CheckForPokerCardsSuitsIsValid(string[] pokerHandsData)
        {
            bool isValidPokerCardsSuit = pokerHandsData
                                             .Where(s => !string.IsNullOrEmpty(s) &&
                                                         Enum.IsDefined(typeof(ValidPokerSuitsEnum), s[1].ToString())).Select(s => s[0]).Count() == 5;
            if (!isValidPokerCardsSuit)
            {
                throw new PokerHandsValidationException(PokerConstants.InvalidInputSuitsMessage);
            }
            return true;
        }

        private bool CheckForPokerCardsIsOfTypeRoyalFlush(string[] pokerHandsData)
        {
            bool royalRank = pokerHandsData
                                 .Where(s => !string.IsNullOrEmpty(s) && Enum.IsDefined(typeof(ValidPokerCardRanksEnums), s[0].ToString())).Count() == 5;
            bool sameSuit = pokerHandsData
                                .Where(s => !string.IsNullOrEmpty(s) &&
                                           Enum.IsDefined(typeof(ValidPokerSuitsEnum), s[1].ToString())).Select(s => s[1]).Distinct().Count() == 1;
            return royalRank && sameSuit;
        }
    }
}
