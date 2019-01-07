using PokerHandsEvaluatorConsoleApplication.Constants;
using PokerHandsEvaluatorConsoleApplication.PokerHandBusiness;
using PokerHandsEvaluatorConsoleApplication.ExceptionHelpers;
using Xunit;

namespace PokerHandEvaluatorTests
{
    public class PokerHandsTest
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void EvaluatePokerHands_CheckNullOrEmptyTest_ReturnsInvalidInputException(string pokerHandsData)
        {
            var actualResultException = Assert.Throws<PokerHandsValidationException>(() => PokerHands.EvaluatePokerHands(pokerHandsData));
            var expectedResult = PokerConstants.InvalidInputMessage;
            Assert.Equal(expectedResult, actualResultException.Message);
        }

        [Theory]
        [InlineData("2c")]
        [InlineData("2c 6d")]
        [InlineData("2c 6d Td")]
        [InlineData("2c 6d Td Tc")]
        [InlineData("2c 6d Td Tc Js 2c")]
        [InlineData("2c 6d Td Tc Js 2c 2s")]
        public void EvaluatePokerHands_CheckPokerHandsCardCountIsValidTest_ReturnsInvalidCardsCountException(string pokerHandsData)
        {
            var actualResultException = Assert.Throws<PokerHandsValidationException>(() => PokerHands.EvaluatePokerHands(pokerHandsData));
            var expectedResult = PokerConstants.InvalidInputLengthMessage;
            Assert.Equal(expectedResult, actualResultException.Message);
        }

        [Theory]
        [InlineData("2c 6z Td Tc Js")]
        [InlineData("2c 6c Td Tc Jr")]
        [InlineData("2c 6c Td Tc Jp")]
        public void EvaluatePokerHands_CheckPokerCardsSuitIsValidTests_ReturnsInvalidCardSuitException(string pokerHandsData)
        {
            var actualResultException = Assert.Throws<PokerHandsValidationException>(() => PokerHands.EvaluatePokerHands(pokerHandsData));
            var expectedResult = PokerConstants.InvalidInputSuitsMessage;
            Assert.Equal(expectedResult, actualResultException.Message);
        }

        [Theory]
        [InlineData("cc 6d td tc js")]
        [InlineData("zc zd xd tc js")]
        [InlineData("zc zd xd rc js")]
        public void EvaluatePokerHands_PokerCardsRanksIsValidTest_ReturnsInvalidCardRankException(string pokerHandsData)
        {
            var actualResultException = Assert.Throws<PokerHandsValidationException>(() => PokerHands.EvaluatePokerHands(pokerHandsData));
            var expectedResult = PokerConstants.InvalidInputRanksMessage;
            Assert.Equal(expectedResult, actualResultException.Message);
        }

        [Theory]
        [InlineData("as ks qs js ts")]
        [InlineData("ac kc qc jc tc")]
        [InlineData("ad kd qd jd td")]
        [InlineData("ah kh qh jh th")]
        public void EvaluatePokerHands_PokerCardsSelecedIsOfRoyalFlushType_ReturnsTrue(string pokerHandsData)
        {
            string actualResult = PokerHands.EvaluatePokerHands(pokerHandsData);
            var expectedResult = PokerConstants.RoyalFlush;
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData("ac ks qs js ts")]
        [InlineData("as kc qc jc tc")]
        [InlineData("ah kd qd jd td")]
        [InlineData("ac kh qh jh th")]
        public void EvaluatePokerHands_PokerCardsSelecedIsOfRoyalFlushType_ReturnsFalse(string pokerHandsData)
        {
            string actualResult = PokerHands.EvaluatePokerHands(pokerHandsData);
            var expectedResult = PokerConstants.RoyalFlush;
            Assert.NotEqual(expectedResult, actualResult);
        }

        [Theory]
        [InlineData("as as as as as")]
        [InlineData("ac kc kc jc tc")]
        [InlineData("ad kd jd jd td")]
        [InlineData("ah ah qh jh th")]
        public void EvaluatePokerHands_PokerCardsHasDuplicateCardsTest_ReturnsException(string pokerHandsData)
        {
            var actualResult = Assert.Throws<PokerHandsValidationException>(() => PokerHands.EvaluatePokerHands(pokerHandsData));
            var expectedResult = PokerConstants.InvalidInputDuplicatesMessage;
            Assert.Equal(expectedResult, actualResult.Message);
        }

        [Theory]
        [InlineData("ask ash as ast asp")]
        [InlineData("ac kch kc jc tc")]
        [InlineData("adc kd qd jd td")]
        [InlineData("ah khc qh jh th")]
        public void EvaluatePokerHands_PokerCardsHasMoreThanTwoCardsTest_ReturnsException(string pokerHandsData)
        {
            var actualResult = Assert.Throws<PokerHandsValidationException>(() => PokerHands.EvaluatePokerHands(pokerHandsData));
            var expectedResult = PokerConstants.InvalidInputCardsPairMessage;
            Assert.Equal(expectedResult, actualResult.Message);
        }
    }
}
