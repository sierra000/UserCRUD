using System;

namespace UserCRUD.Core.Extensions
{
    public static class StringExtensions
    {
        public static string ToLogMessage(this string message)
        {
            string value = $@"Custom Log: {message}{Environment.NewLine}{Environment.NewLine}";

            return value;
        }
    }
}
