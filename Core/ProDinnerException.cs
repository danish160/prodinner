using System;

namespace Omu.ProDinner.Core
{
    [Serializable]
    public class ProDinnerException : Exception
    {
        public ProDinnerException(string message)
            : base(message)
        {
        }
    }
}