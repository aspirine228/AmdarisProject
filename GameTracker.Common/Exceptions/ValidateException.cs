using System;

namespace GameTracker.Common.Exceptions
{
    public class ValidateException:Exception
    {
        public ValidateException(string message) : base(message)
        {

        }
    }
}
