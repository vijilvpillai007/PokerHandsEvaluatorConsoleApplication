using PokerHandsEvaluatorConsoleApplication.Constants;
using PokerHandsEvaluatorConsoleApplication.PokerHandBusiness;
using PokerHandsEvaluatorConsoleApplication.ExceptionHelpers;
using PokerHandsEvaluatorConsoleApplication.PokerHandsInterfaces;
using Unity;
using Xunit;

namespace PokerHandEvaluatorTests
{
    public class PokerHandsTest
    {
        private PokerHands GetPokerHandsObject()
        {
            var unityContainer=new UnityContainer();
            unityContainer.RegisterType<IPokerHands, PokerHands>();
            return unityContainer.Resolve<PokerHands>();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void EvaluatePokerHands_CheckNullOrEmptyTest_ReturnsInvalidInputException(string pokerHandsData)
        {
            var pokerHandsObject = GetPokerHandsObject();
            var actualResultException = Assert.Throws<PokerHandsValidationException>(() => pokerHandsObject.EvaluatePokerHands(pokerHandsData));
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
            var pokerHandsObject = GetPokerHandsObject();
            var actualResultException = Assert.Throws<PokerHandsValidationException>(() => pokerHandsObject.EvaluatePokerHands(pokerHandsData));
            var expectedResult = PokerConstants.InvalidInputLengthMessage;
            Assert.Equal(expectedResult, actualResultException.Message);
        }

        [Theory]
        [InlineData("2c 6z Td Tc Js")]
        [InlineData("2c 6c Td Tc Jr")]
        [InlineData("2c 6c Td Tc Jp")]
        public void EvaluatePokerHands_CheckPokerHandsSuitIsValidTests_ReturnsInvalidCardSuitException(string pokerHandsData)
        {
            var pokerHandsObject = GetPokerHandsObject();
            var actualResultException = Assert.Throws<PokerHandsValidationException>(() => pokerHandsObject.EvaluatePokerHands(pokerHandsData));
            var expectedResult = PokerConstants.InvalidInputSuitsMessage;
            Assert.Equal(expectedResult, actualResultException.Message);
        }

        [Theory]
        [InlineData("cc 6d td tc js")]
        [InlineData("zc zd xd tc js")]
        [InlineData("zc zd xd rc js")]
        public void EvaluatePokerHands_PokerHandsRankIsValidTest_ReturnsInvalidCardRankException(string pokerHandsData)
        {
            var pokerHandsObject = GetPokerHandsObject();
            var actualResultException = Assert.Throws<PokerHandsValidationException>(() => pokerHandsObject.EvaluatePokerHands(pokerHandsData));
            var expectedResult = PokerConstants.InvalidInputRanksMessage;
            Assert.Equal(expectedResult, actualResultException.Message);
        }

        [Theory]
        [InlineData("as ks qs js ts")]
        [InlineData("ac kc qc jc tc")]
        [InlineData("ad kd qd jd td")]
        [InlineData("ah kh qh jh th")]
        public void EvaluatePokerHands_PokerHandsSelecedIsOfRoyalFlushType_ReturnsTrue(string pokerHandsData)
        {
            var pokerHandsObject = GetPokerHandsObject();
            var actualResult = pokerHandsObject.EvaluatePokerHands(pokerHandsData);
            var expectedResult = PokerConstants.RoyalFlush;
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData("ac ks qs js ts")]
        [InlineData("as kc qc jc tc")]
        [InlineData("ah kd qd jd td")]
        [InlineData("ac kh qh jh th")]
        public void EvaluatePokerHands_PokerHandsSelecedIsOfNotRoyalFlushType_ReturnsFalse(string pokerHandsData)
        {
            var pokerHandsObject = GetPokerHandsObject();
            var actualResult = pokerHandsObject.EvaluatePokerHands(pokerHandsData);
            var expectedResult = PokerConstants.RoyalFlush;
            Assert.NotEqual(expectedResult, actualResult);
        }

        [Theory]
        [InlineData("as as as as as")]
        [InlineData("ac kc kc jc tc")]
        [InlineData("ad kd jd jd td")]
        [InlineData("ah ah qh jh th")]
        public void EvaluatePokerHands_PokerHandsHasDuplicateCardsTest_ReturnsException(string pokerHandsData)
        {
            var pokerHandsObject = GetPokerHandsObject();
            var actualResult = Assert.Throws<PokerHandsValidationException>(() => pokerHandsObject.EvaluatePokerHands(pokerHandsData));
            var expectedResult = PokerConstants.InvalidInputDuplicatesMessage;
            Assert.Equal(expectedResult, actualResult.Message);
        }

        [Theory]
        [InlineData("ask ash as ast asp")]
        [InlineData("ac kch kc jc tc")]
        [InlineData("adc kd qd jd td")]
        [InlineData("ah khc qh jh th")]
        public void EvaluatePokerHands_EachPokerHandsHasMoreThanTwoCardsTest_ReturnsException(string pokerHandsData)
        {
            var pokerHandsObject = GetPokerHandsObject();
            var actualResult = Assert.Throws<PokerHandsValidationException>(() => pokerHandsObject.EvaluatePokerHands(pokerHandsData));
            var expectedResult = PokerConstants.InvalidInputCardsPairMessage;
            Assert.Equal(expectedResult, actualResult.Message);
        }
    }
}
