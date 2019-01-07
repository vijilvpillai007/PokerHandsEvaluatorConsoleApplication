using PokerHandsEvaluatorConsoleApplication.Constants;
using PokerHandsEvaluatorConsoleApplication.ExceptionHelpers;
using System;
using System.Linq;

namespace PokerHandsEvaluatorConsoleApplication.PokerHandBusiness
{
    public class PokerHands
    {
        private char[] _pokerRanks;
        private char[] _pokerSuits;
        public string EvaluatePokerHands(string pokerHandsData)
        {
            string pokerHandResult = string.Empty;
            if (ValidatePokerData(pokerHandsData))
            {
                if (ValidateRoyalFlush())
                {
                    pokerHandResult = PokerConstants.RoyalFlush;
                }
            }
            else
            {
                throw new PokerHandsValidationException(PokerConstants.InvalidInputMessage);
            }
            return pokerHandResult;
        }
        private bool ValidatePokerData(string pokerHandsData)
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
                                          && ValidatePokerInputRanksIsValid(pokerHands)
                                          && ValidatePokerInputSuitsIsValid(pokerHands);
            if (!isPokerHandsInputValid)
            {
                throw new PokerHandsValidationException(PokerConstants.InvalidInputLengthMessage);
            }
            return true;
        }

        private bool CheckforPokerHandsCountAndDuplicateCards(string[] pokerHandsData)
        {
            int pokerCardsCount = pokerHandsData.Length;
            if (pokerCardsCount != 5)
            {
                throw new PokerHandsValidationException(PokerConstants.InvalidInputLengthMessage);
            }
            int duplicateCardCount = pokerHandsData.Distinct().Count();
            if (duplicateCardCount != 5)
            {
                throw new PokerHandsValidationException(PokerConstants.InvalidInputDuplicatesMessage);
            }
            return true;
        }

        private bool ValidatePokerInputRanksIsValid(string[] pokerHandsData)
        {
            _pokerRanks = pokerHandsData
                .Where(s => !string.IsNullOrEmpty(s) && PokerValidationConstants.ValidPokerRanks.Contains(s[0]))
                .Select(s => s[0]).ToArray();
            if (_pokerRanks.Length != 5)
            {
                throw new PokerHandsValidationException(PokerConstants.InvalidInputRanksMessage);
            }
            return true;
        }
        private bool ValidatePokerInputSuitsIsValid(string[] pokerHandsData)
        {
            _pokerSuits = pokerHandsData
                .Where(s => !string.IsNullOrEmpty(s) && PokerValidationConstants.ValidPokerSuits.Contains(s[1]))
                .Select(s => s[1]).ToArray();
            if (_pokerSuits.Length != 5)
            {
                throw new PokerHandsValidationException(PokerConstants.InvalidInputSuitsMessage);
            }
            return true;
        }

        private bool ValidateRoyalFlush()
        {
            bool royalRank = _pokerRanks.Where(s => PokerValidationConstants.ValidRoyalFlush.Contains(s)).Count() == 5 ? true : false;
            bool sameSuit = _pokerSuits.Distinct().Count() == 1 ? true : false;
            return royalRank && sameSuit;
        }
    }
}
