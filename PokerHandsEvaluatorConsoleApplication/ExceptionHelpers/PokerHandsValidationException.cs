using System;

namespace PokerHandsEvaluatorConsoleApplication.ExceptionHelpers
{
    public class PokerHandsValidationException:Exception
    {
        public PokerHandsValidationException()
        {
        }

        public PokerHandsValidationException(string message)
            : base(message)
        {
        }

        public PokerHandsValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
