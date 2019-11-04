using System;
using System.Collections.Generic;
using System.Text;

namespace DDDCrashCourse.SharedKernel.Exceptions
{
    public class InvalidIdentifierException : ArgumentException
    {
        /// <summary>
        ///     Creates a new instance of the exception
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="parameter">Parameter</param>
        public InvalidIdentifierException(string message, string parameter) : base(message, parameter)
        {
        }


        /// <summary>
        ///     Formats the exception as a string
        /// </summary>
        /// <returns>A string representation of the excetion object</returns>
        public override string ToString()
        {
            return $"Message: {Message}\n" +
                $"Invalid parameter value: {ParamName}\n" +
                $"{StackTrace}";
        }
    }
}
