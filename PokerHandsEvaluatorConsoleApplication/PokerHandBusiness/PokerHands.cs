using PokerHandsEvaluatorConsoleApplication.Constants;
using PokerHandsEvaluatorConsoleApplication.ExceptionHelpers;
using System;
using System.Linq;

namespace PokerHandsEvaluatorConsoleApplication.PokerHandBusiness
{
    public static class PokerHands
    {
        #region Enums
        enum ValidPokerCardRanks
        {
            Ace = 'a',
            Two = '2',
            Three = '3',
            Four = '4',
            Five = '5',
            Six = '6',
            Seven = '7',
            Eight = '8',
            Nine = '9',
            Ten = 't',
            Jack = 'j',
            Queen = 'q',
            King = 'k'
        }

        enum ValidPokerSuits
        {
            Club = 'c',
            Diamond = 'd',
            Hearts = 'h',
            Spades = 's'
        }

        enum ValidRoyalFlushCards
        {
            Ace = 'a',
            Ten = 't',
            Jack = 'j',
            Queen = 'q',
            King = 'k'
        }
        #endregion
        public static string EvaluatePokerHands(string pokerHandsData)
        {
            string pokerHandResult = string.Empty;
            if (!ValidatePokerData(pokerHandsData))
            {
                throw new PokerHandsValidationException(PokerConstants.InvalidInputMessage);
            }
            var pokerHands = pokerHandsData.ToLower().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            if (CheckForPokerCardsIsOfTypeRoyalFlush( pokerHands))
            {
                pokerHandResult = PokerConstants.RoyalFlush;
            }
            return pokerHandResult;
        }
        private static bool ValidatePokerData(string pokerHandsData)
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

        private static bool CheckforPokerHandsCountAndDuplicateCards(string[] pokerHandsData)
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

        private static bool CheckForPokerCardsRanksIsValid(string[] pokerHandsData)
        {
            bool isValidPokerCardsRank= pokerHandsData
                .Where(s => !string.IsNullOrEmpty(s) && Enum.IsDefined(typeof(ValidPokerCardRanks),s[0])).Count()==5;
            if (!isValidPokerCardsRank)
            {
                throw new PokerHandsValidationException(PokerConstants.InvalidInputRanksMessage);
            }
            return true;
        }
        private static bool CheckForPokerCardsSuitsIsValid(string[] pokerHandsData)
        {
            bool isValidPokerCardsSuit = pokerHandsData
                                             .Where(s => !string.IsNullOrEmpty(s) &&
                                                         Enum.IsDefined(typeof(ValidPokerSuits), s[1])).Count() == 5;
            if (!isValidPokerCardsSuit)
            {
                throw new PokerHandsValidationException(PokerConstants.InvalidInputSuitsMessage);
            }
            return true;
        }

        private static bool CheckForPokerCardsIsOfTypeRoyalFlush(string[] pokerHandsData)
        {
            bool royalRank = pokerHandsData
                                 .Where(s => !string.IsNullOrEmpty(s) && Enum.IsDefined(typeof(ValidPokerSuits), s[0])
                                                                      && Enum.IsDefined(typeof(ValidPokerSuits), s[0]))
                                 .Count() == 5;
            bool sameSuit = pokerHandsData
                                .Where(s => !string.IsNullOrEmpty(s) &&
                                            Enum.IsDefined(typeof(ValidPokerSuits), s[1])).Distinct().Count() == 1;
            return royalRank && sameSuit;
        }
    }
}
