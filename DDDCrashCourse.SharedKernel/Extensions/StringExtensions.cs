using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DDDCrashCourse.SharedKernel.Extensions
{
    public static class StringExtensions
    {
        public static bool IsValidEmail(this string email)
        {
            if (email == null || email == string.Empty) return false;

            return new EmailAddressAttribute().IsValid(email);
        }

        public static bool IsUri(this string uri)
        {
            if (uri == null || uri == string.Empty) return false;
            Uri uriResult;
            return Uri.TryCreate(uri, UriKind.Absolute, out uriResult);
        }
    }
}
