using System;

namespace eigenein.SkypeNinja.Core.Exceptions
{
    [Serializable]
    public class MessageFailedException : Exception
    {
        public MessageFailedException(string message) :
            base(message)
        {
            // Do nothing.
        }
    }
}
