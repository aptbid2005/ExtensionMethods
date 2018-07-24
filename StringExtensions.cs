// ***********************************************************************
// Assembly         : Extensions.Core
// Author           : Jason Nowicki
// Created          : 02-13-2018
//
// Last Modified By : Jason Nowicki
// Last Modified On : 02-28-2018
// ***********************************************************************
// <copyright file="StringExtensions.cs" company="EECPPC KY.gov">
//     Copyright ©  2018
// </copyright>
// ***********************************************************************

using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Extensions.Core
{
    /// <summary>
    /// Class StringExtensions.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class StringExtensions
    {
        /// <summary>
        /// Converts the bool to character.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns>System.String.</returns>
        public static string ConvertBoolToChar(this bool? value)
        {
            if (value.HasValue)
            {
                return value.Value ? "Y" : "N";
            }

            return "N";
        }


        /// <summary>
        /// Converts the bool to character.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns>System.String.</returns>
        public static string ConvertBoolToChar(this bool value)
        {
            return value ? "Y" : "N";
        }


        /// <summary>
        /// Converts the character to bool.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool ConvertCharToBool(this string value)
        {
            switch (value.ToUpper())
            {
                case "1":
                case "Y":
                case "X":
                case "TRUE":
                case "T":
                    return true;
                case "N":
                case "0":
                case "FALSE":
                case "F":
                case "":
                    return false;
                default:
                    return false;
            }
        }


        /// <summary>
        /// Substrings from zero to n
        /// </summary>
        /// <param name="text">String to trim</param>
        /// <param name="allowedLength">Max length allowed</param>
        /// <returns>System.String.</returns>
        public static string SubstringAt(this string text, int allowedLength)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            return text.Length > allowedLength ? text.Substring(0, allowedLength) : text;
        }


        /// <summary>
        /// Determines whether [is valid email address] [the specified s].
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns><c>true</c> if [is valid email address] [the specified s]; otherwise, <c>false</c>.</returns>
        public static bool IsValidEmailAddress(this string s)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(s);
        }


        /// <summary>
        /// Determines whether [is valid URL] [the specified text].
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns><c>true</c> if [is valid URL] [the specified text]; otherwise, <c>false</c>.</returns>
        public static bool IsValidUrl(this string text)
        {
            Regex rx = new Regex(@"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
            return rx.IsMatch(text);
        }

    }
}
