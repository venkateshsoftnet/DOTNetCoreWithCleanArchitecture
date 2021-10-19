using System;
using System.Globalization;

namespace TFL.Road.Status.Application.Exceptions
{
    public class NoResultsFoundException : Exception
    {
        public NoResultsFoundException() : base() { }

        public NoResultsFoundException(string message) : base(message) { }

        public NoResultsFoundException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
