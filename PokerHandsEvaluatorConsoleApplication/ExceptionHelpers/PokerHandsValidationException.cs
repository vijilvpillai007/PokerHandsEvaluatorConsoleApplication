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

        public PokerHandsValidationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
