namespace PokerHandsEvaluatorConsoleApplication.Constants
{
    public static class PokerConstants
    {
        public const string RoyalFlush = "Royal Flush";
        public const string HighCard = "High Card";
        public const string Pair = "Pair";
        public const string TwoPair = "Two pair";
        public const string ThreeOfaKind = "Three of a kind";
        public const string Straight = "Straight ";
        public const string Flush = "Flush ";
        public const string FullHouse = "Full house";
        public const string FourOfaKind = "Four of a kind";
        public const string StraightFlush = "Straight flush";
        #region Error Messages
        public const string InvalidInputMessage = "Invalid Input";
        public const string InvalidInputLengthMessage = "Invalid Input Length";
        public const string InvalidInputSuitsMessage = "Invalid Input Suits";
        public const string InvalidInputDuplicatesMessage = "Duplicate pair of cards present in the input";
        public const string InvalidInputRanksMessage = "Invalid Input Ranks";
        public const string InvalidInputCardsPairMessage = "Number of cards in a pair is more than two";
        #endregion
    }
}
