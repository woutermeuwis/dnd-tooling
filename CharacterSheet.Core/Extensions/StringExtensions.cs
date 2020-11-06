using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace CharacterSheet.Core.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string input) => input == null || input == string.Empty;
        public static bool IsNullOrEmptyOrWhitespace(this string input) => (input?.Trim()).IsNullOrEmpty();

        public static bool IsNotNullOrEmpty(this string input) => !input.IsNullOrEmpty();
        public static bool IsNotNullOrEmptyOrWhitespace(this string input) => !input.IsNullOrEmptyOrWhitespace();
    }
}
