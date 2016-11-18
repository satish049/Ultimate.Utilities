using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Ultimate.Utilities
{

    /// <summary>
    /// Utility methods for String operations.
    /// All methods in the StringUtils are Null safe.
    /// </summary>
    public static class StringUtils
    {
        /// <summary>
        /// Space Character " "
        /// </summary>
        public static readonly string Space = " ";
        /// <summary>
        /// Empty string ""
        /// </summary>
        public static readonly string Empty = "";
        /// <summary>
        /// Line Feed Character '\n'
        /// </summary>
        public static readonly char Lf = '\n';
        /// <summary>
        /// Carriage Return Character '\r'
        /// </summary>
        public static readonly char Cr = '\r';
        /// <summary>
        /// Index Not Found (-1)
        /// </summary>
        public static readonly int IndexNotFound = -1;
        /// <summary>
        /// Pad Limit (8192)
        /// </summary>
        private const int PadLimit = 8192;

        #region Empty, Blank & Trim

        /// <summary>
        /// Checks If a String is Empty ("") or Null
        /// </summary>
        /// See below examples
        /// <para/> StringUtils.IsEmpty(null)      = true
        /// <para/> StringUtils.IsEmpty("")        = true
        /// <para/> StringUtils.IsEmpty(" ")       = false
        /// <para/> StringUtils.IsEmpty("abc")     = false
        /// <para/> StringUtils.IsEmpty("  abc  ") = false
        /// <param name="str">input string</param>
        /// <returns>true or false</returns>
        public static bool IsEmpty(string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// Checks If a String is Not Empty ("") and Not Null
        /// </summary>
        /// <para>
        /// See below examples
        /// <para/> StringUtils.IsNotEmpty(null)      = false
        /// <para/> StringUtils.IsNotEmpty("")        = false
        /// <para/> StringUtils.IsNotEmpty(" ")       = true
        /// <para/> StringUtils.IsNotEmpty("abc")     = true
        /// <para/> StringUtils.IsNotEmpty("  abc  ") = true
        /// </para>
        /// <param name="str">input string</param>
        /// <returns>true or false</returns>
        public static bool IsNotEmpty(string str)
        {
            return !IsEmpty(str);
        }

        /// <summary>
        /// Checks if any of the Strings are Empty ("") or Null
        /// </summary>
        /// <para>
        /// See below examples
        /// <para/> StringUtils.IsAnyEmpty(null)             = true
        /// <para/> StringUtils.IsAnyEmpty(null, "abc")      = true
        /// <para/> StringUtils.IsAnyEmpty("", "xyz")        = true
        /// <para/> StringUtils.IsAnyEmpty("abc", "")        = true
        /// <para/> StringUtils.IsAnyEmpty("  abc  ", null)  = true
        /// <para/> StringUtils.IsAnyEmpty(" ", "xyz")       = false
        /// <para/> StringUtils.IsAnyEmpty("abc", "xyz")     = false
        /// </para>
        /// <param name="strArgs">input strings array</param>
        /// <returns>true or false</returns>
        public static bool IsAnyEmpty(params string[] strArgs)
        {
            return CollectionUtils.IsEmpty(strArgs) || strArgs.Any(IsEmpty);
        }

        /// <summary>
        /// Checks if None of the Strings are Empty ("") and Null
        /// </summary>
        /// <para>
        /// See below examples
        /// <para/> StringUtils.IsNoneEmpty(null)             = false
        /// <para/> StringUtils.IsNoneEmpty(null, "abc")      = false
        /// <para/> StringUtils.IsNoneEmpty("", "xyz")        = false
        /// <para/> StringUtils.IsNoneEmpty("abc", "")        = false
        /// <para/> StringUtils.IsNoneEmpty("  abc  ", null)  = false
        /// <para/> StringUtils.IsNoneEmpty(" ", "xyz")       = true
        /// <para/> StringUtils.IsNoneEmpty("abc", "xyz")     = true
        /// </para>
        /// <param name="strArgs">input strings array</param>
        /// <returns>true or false</returns>
        public static bool IsNoneEmpty(params string[] strArgs)
        {
            return !IsAnyEmpty(strArgs);
        }

        /// <summary>
        /// Checks if a String is Empty (""),Null or Whitespace (" ")
        /// </summary>
        /// <para>
        /// See below examples
        /// <para/> StringUtils.IsBlank(null)      = true
        /// <para/> StringUtils.IsBlank("")        = true
        /// <para/> StringUtils.IsBlank(" ")       = true
        /// <para/> StringUtils.IsBlank("abc")     = false
        /// <para/> StringUtils.IsBlank("  abc  ") = false
        /// </para>
        /// <param name="str">input string</param>
        /// <returns>true or false</returns>
        public static bool IsBlank(string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// Checks if a String is Not Empty (""),Not Null and Not Whitespace (" ")
        /// </summary>
        /// <para>
        /// See below examples
        /// <para/> StringUtils.IsNotBlank(null)      = false
        /// <para/> StringUtils.IsNotBlank("")        = false
        /// <para/> StringUtils.IsNotBlank(" ")       = false
        /// <para/> StringUtils.IsNotBlank("abc")     = true
        /// <para/> StringUtils.IsNotBlank("  abc  ") = true
        /// </para>
        /// <param name="str">input string</param>
        /// <returns>true or false</returns>
        public static bool IsNotBlank(string str)
        {
            return !IsBlank(str);
        }

        /// <summary>
        /// Checks if any of the Strings are Empty (""),Null or Whitespace (" ")
        /// </summary>
        /// <para>
        /// See below examples
        /// <para/> StringUtils.IsAnyBlank(null)             = true
        /// <para/> StringUtils.IsAnyBlank(null, "abc")      = true
        /// <para/> StringUtils.IsAnyBlank(null, null)       = true
        /// <para/> StringUtils.IsAnyBlank("", "xyz")        = true
        /// <para/> StringUtils.IsAnyBlank("abc", "")        = true
        /// <para/> StringUtils.IsAnyBlank("  abc  ", null)  = true
        /// <para/> StringUtils.IsAnyBlank(" ", "xyz")       = true
        /// <para/> StringUtils.IsAnyBlank("abc", "xyz")     = false     
        /// </para>        
        /// <param name="strArgs">input strings array</param>
        /// <returns>true or false</returns>
        public static bool IsAnyBlank(params string[] strArgs)
        {
            return CollectionUtils.IsEmpty(strArgs) || strArgs.Any(IsBlank);
        }

        ///<summary>
        /// Checks if none of the Strings are Empty (""), Null and Whitespace (" ")
        /// </summary>
        /// <para>
        /// See below examples
        /// <para/> StringUtils.IsNoneBlank(null)             = false
        /// <para/> StringUtils.IsNoneBlank(null, "abc")      = false
        /// <para/> StringUtils.IsNoneBlank(null, null)       = false
        /// <para/> StringUtils.IsNoneBlank("", "xyz")        = false
        /// <para/> StringUtils.IsNoneBlank("abc", "")        = false
        /// <para/> StringUtils.IsNoneBlank("  abc  ", null)  = false
        /// <para/> StringUtils.IsNoneBlank(" ", "xyz")       = false
        /// <para/> StringUtils.IsNoneBlank("abc", "xyz")     = true 
        /// </para>
        /// <param name="strArgs">input strings array</param>
        /// <returns>true or false</returns>
        public static bool IsNoneBlank(params string[] strArgs)
        {
            return !IsAnyBlank(strArgs);
        }

        /// <summary>
        /// Removes control characters from both ends of the String.
        /// Handles null by returning null.
        /// </summary>
        /// <para>
        /// See below examples
        /// <para/> StringUtils.Trim(null)          = null
        /// <para/> StringUtils.Trim("")            = ""
        /// <para/> StringUtils.Trim("     ")       = ""
        /// <para/> StringUtils.Trim("abc")         = "abc"
        /// <para/> StringUtils.Trim("    abc    ") = "abc"
        /// </para>
        /// <param name="str">input string</param>
        /// <returns>Trimmed string</returns>
        public static string Trim(string str)
        {
            return str == null ? null : str.Trim();
        }

        /// <summary>
        /// Removes control characters from both ends of the String by returning 
        /// null if the String is Empty ("") after the trim or null
        /// </summary>
        /// <para>
        /// See below examples
        /// <para/> StringUtils.TrimToNull(null)          = null
        /// <para/> StringUtils.TrimToNull("")            = null
        /// <para/> StringUtils.TrimToNull("     ")       = null
        /// <para/> StringUtils.TrimToNull("abc")         = "abc"
        /// <para/> StringUtils.TrimToNull("    abc    ") = "abc"
        /// </para>
        /// <param name="str">input string</param>
        /// <returns>Trimmed string</returns>
        public static string TrimToNull(string str)
        {
            return IsBlank(str) ? null : Trim(str);
        }

        /// <summary>
        /// Removes control characters from both ends of the String by returning 
        /// Empty String ("") if the String is Empty ("") after the trim or null
        /// </summary>
        /// <para>
        /// See below examples
        /// <para/> StringUtils.TrimToEmpty(null)          = ""
        /// <para/> StringUtils.TrimToEmpty("")            = ""
        /// <para/> StringUtils.TrimToEmpty("     ")       = ""
        /// <para/> StringUtils.TrimToEmpty("abc")         = "abc"
        /// <para/> StringUtils.TrimToEmpty("    abc    ") = "abc"
        /// </para>
        /// <param name="str">input string</param>
        /// <returns>Trimmed string</returns>
        public static string TrimToEmpty(string str)
        {
            return IsBlank(str) ? Empty : Trim(str);
        }

        #endregion

        #region Strip

        /// <summary>
        /// Strips whitespace or the given set of chars at the starting of a String
        /// </summary>
        /// <para>
        /// See below examples
        /// <para/> StringUtils.StripStart(null, *)          = null
        /// <para/> StringUtils.StripStart("", *)            = ""
        /// <para/> StringUtils.StripStart("abc", "")        = "abc"
        /// <para/> StringUtils.StripStart("abc", null)      = "abc"
        /// <para/> StringUtils.StripStart("  abc", null)    = "abc"
        /// <para/> StringUtils.StripStart("abc  ", null)    = "abc  "
        /// <para/> StringUtils.StripStart(" abc ", null)    = "abc "
        /// <para/> StringUtils.StripStart("yxabc  ", "xyz") = "abc  "
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="stripChars">Chars to be stripped</param>
        /// <returns>String</returns>
        public static string StripStart(string str, string stripChars = null)
        {
            int strLen;
            if (str == null || (strLen = str.Length) == 0)
            {
                return str;
            }
            var start = 0;
            if (stripChars == null)
            {
                while (start != strLen && char.IsWhiteSpace(str[start]))
                {
                    start++;
                }
            }
            else if (IsEmpty(stripChars))
            {
                return str;
            }
            else
            {
                while (start != strLen && stripChars.IndexOf(str[start]) != IndexNotFound)
                {
                    start++;
                }
            }
            return str.Substring(start);
        }

        /// <summary>
        /// Strips whitespace or the given set of chars at the ending of a String
        /// </summary>
        /// <para>
        /// See below examples
        /// <para/> StringUtils.StripEnd(null, *)          = null
        /// <para/> StringUtils.StripEnd("", *)            = ""
        /// <para/> StringUtils.StripEnd("abc", "")        = "abc"
        /// <para/> StringUtils.StripEnd("abc", null)      = "abc"
        /// <para/> StringUtils.StripEnd("  abc", null)    = "  abc"
        /// <para/> StringUtils.StripEnd("abc  ", null)    = "abc"
        /// <para/> StringUtils.StripEnd(" abc ", null)    = " abc"
        /// <para/> StringUtils.StripEnd("  abcyx", "xyz") = "  abc"
        /// <para/> StringUtils.StripEnd("199.00", ".0")   = "199"
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="stripChars">Chars to be stripped</param>
        /// <returns>String</returns>
        public static string StripEnd(string str, string stripChars = null)
        {
            int end;
            if (str == null || (end = str.Length) == 0)
            {
                return str;
            }

            if (stripChars == null)
            {
                while (end != 0 && char.IsWhiteSpace(str[end - 1]))
                {
                    end--;
                }
            }
            else if (IsEmpty(stripChars))
            {
                return str;
            }
            else
            {
                while (end != 0 && stripChars.IndexOf(str[end - 1]) != IndexNotFound)
                {
                    end--;
                }
            }
            return str.Substring(0, end);
        }

        /// <summary>
        /// Strips whitespace or the given set of chars from the start and end of a String
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Strip(null)     = null
        /// <para/> StringUtils.Strip("")       = ""
        /// <para/> StringUtils.Strip("   ")    = ""
        /// <para/> StringUtils.Strip("abc")    = "abc"
        /// <para/> StringUtils.Strip("  abc")  = "abc"
        /// <para/> StringUtils.Strip("abc  ")  = "abc"
        /// <para/> StringUtils.Strip(" abc ")  = "abc"
        /// <para/> StringUtils.Strip(" ab c ") = "ab c"
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="stripChars">Chars to be stripped</param>
        /// <returns>string</returns>
        public static string Strip(string str, string stripChars = null)
        {
            return IsEmpty(str) ? str : StripEnd(StripStart(str, stripChars), stripChars);
        }

        /// <summary>
        /// Strips whitespace from the start and end of a String and returns Null if the result is Empty ("").
        /// </summary>
        /// <para>
        /// <para/> StringUtils.StripToNull(null)     = null
        /// <para/> StringUtils.StripToNull("")       = null
        /// <para/> StringUtils.StripToNull("   ")    = null
        /// <para/> StringUtils.StripToNull("abc")    = "abc"
        /// <para/> StringUtils.StripToNull("  abc")  = "abc"
        /// <para/> StringUtils.StripToNull("abc  ")  = "abc"
        /// <para/> StringUtils.StripToNull(" abc ")  = "abc"
        /// <para/> StringUtils.StripToNull(" ab c ") = "ab c"
        /// </para>
        /// <param name="str">input string</param>
        /// <returns>string</returns>
        public static string StripToNull(string str)
        {
            return IsBlank(str) ? null : Strip(str);
        }

        /// <summary>
        /// Strips whitespace from the start and end of a String. Returns Empty ("") if the input is Null.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.StripToEmpty(null)     = ""
        /// <para/> StringUtils.StripToEmpty("")       = ""
        /// <para/> StringUtils.StripToEmpty("   ")    = ""
        /// <para/> StringUtils.StripToEmpty("abc")    = "abc"
        /// <para/> StringUtils.StripToEmpty("  abc")  = "abc"
        /// <para/> StringUtils.StripToEmpty("abc  ")  = "abc"
        /// <para/> StringUtils.StripToEmpty(" abc ")  = "abc"
        /// <para/> StringUtils.StripToEmpty(" ab c ") = "ab c"
        /// </para>
        /// <param name="str">input string</param>
        /// <returns>string</returns>
        public static string StripToEmpty(string str)
        {
            return IsBlank(str) ? Empty : Strip(str);
        }

        /// <summary>
        /// Strips whitespace from the start and end of every String in an array.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.StripAll(null)             = null
        /// <para/> StringUtils.StripAll([])               = []
        /// <para/> StringUtils.StripAll("abc", "  abc") = ["abc", "abc"]
        /// <para/> StringUtils.StripAll("abc  ", null)  = ["abc", null]
        /// <para/> StringUtils.StripAll(["xyz", "  abc"]) = ["xyz", "abc"]
        /// </para>
        /// <param name="strs">input strings</param>
        /// <returns>result strings array</returns>
        public static string[] StripAll(params string[] strs)
        {
            return StripAll(strs, null);
        }

        /// <summary>
        /// Strips the given set of Chars from the start and end of every string in an array.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.StripAll(null, *)                = null
        /// <para/> StringUtils.StripAll([], *)                  = []
        /// <para/> StringUtils.StripAll(["abc", "  abc"], null) = ["abc", "abc"]
        /// <para/> StringUtils.StripAll(["abc  ", null], null)  = ["abc", null]
        /// <para/> StringUtils.StripAll(["abc  ", null], "yz")  = ["abc  ", null]
        /// <para/> StringUtils.StripAll(["yabcz", null], "yz")  = ["abc", null] 
        /// </para>
        /// <param name="strs">input strings</param>
        /// <param name="stripChars">Chars to be stripped</param>
        /// <returns>result strings array</returns>
        public static string[] StripAll(string[] strs, string stripChars = null)
        {
            int strsLen;
            if (strs == null || (strsLen = strs.Length) == 0)
            {
                return strs;
            }
            var newArr = new string[strsLen];
            for (var i = 0; i < strsLen; i++)
            {
                newArr[i] = Strip(strs[i], stripChars);
            }
            return newArr;
        }

        /// <summary>
        /// Removes diacritics (~= accents) from a string. The case will not be altered
        /// </summary>
        /// <para>
        /// <para/> StringUtils.StripAccents(null)                = null
        /// <para/> StringUtils.StripAccents("")                  = ""
        /// <para/> StringUtils.StripAccents("control")           = "control"
        /// <para/> StringUtils.StripAccents("Šatish")            = "Satish"
        /// </para>
        /// <param name="input">input string</param>
        /// <returns>String</returns>
        public static string StripAccents(string input)
        {
            if (IsBlank(input))
                return input;
            var decomposed = input.Normalize(NormalizationForm.FormD);
            var len = decomposed.Length;
            var sb = new StringBuilder();
            for (var i = 0; i < len; i++)
            {
                var uc = CharUnicodeInfo.GetUnicodeCategory(decomposed[i]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(decomposed[i]);
                }
            }
            return (sb.ToString().Normalize(NormalizationForm.FormC));

        }

        #endregion

        #region Comparisons and Indexes

        /// <summary>
        /// Compares two strings and returns true if they represent equal sequence of characters.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Equals(null, null)   = true
        /// <para/> StringUtils.Equals(null, "abc")  = false
        /// <para/> StringUtils.Equals("abc", null)  = false
        /// <para/> StringUtils.Equals("abc", "abc") = true
        /// <para/> StringUtils.Equals("abc", "ABC") = false
        /// </para>
        /// <param name="s1">string1</param>
        /// <param name="s2">string2</param>
        /// <returns>true or false</returns>
        public static bool Equals(string s1, string s2)
        {
            if (s1 == s2)
                return true;
            if (s1 == null || s2 == null)
                return false;
            return s1.Equals(s2);
        }

        /// <summary>
        /// Compares two strings by ignoring case and returns true if they represent equal sequence of characters.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.EqualsIgnoreCase(null, null)   = true
        /// <para/> StringUtils.EqualsIgnoreCase(null, "abc")  = false
        /// <para/> StringUtils.EqualsIgnoreCase("abc", null)  = false
        /// <para/> StringUtils.EqualsIgnoreCase("abc", "abc") = true
        /// <para/> StringUtils.EqualsIgnoreCase("abc", "ABC") = true
        /// </para>
        /// <param name="s1">string1</param>
        /// <param name="s2">string2</param>
        /// <returns>true or false</returns>
        public static bool EqualsIgnoreCase(string s1, string s2)
        {
            if (s1 == null || s2 == null)
                return s1 == s2;
            if (s1 == s2)
                return true;
            return s1.Length == s2.Length && s1.Equals(s2, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// Finds the first index of the given character within a string.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.IndexOf(null, *)         = -1
        /// <para/> StringUtils.IndexOf("", *)           = -1
        /// <para/> StringUtils.IndexOf("aabaabaa", 'a') = 0
        /// <para/> StringUtils.IndexOf("aabaabaa", 'b') = 2
        /// <para/> StringUtils.IndexOf("aabaabaa", 'b', 3)  = 5
        /// </para>
        /// <param name="s1">string</param>
        /// <param name="searchValue">search char</param>
        /// <param name="startPos">starting position</param>
        /// <returns>index of the char if found or -1</returns>
        public static int IndexOf(string s1, char searchValue, int startPos = 0)
        {
            return IndexOf(s1, Convert.ToString(searchValue), startPos);
        }

        /// <summary>
        /// Finds the first index of the given string within a string.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.IndexOf(null, *)          = -1
        /// <para/> StringUtils.IndexOf(*, null)          = -1
        /// <para/> StringUtils.IndexOf("", "")           = 0
        /// <para/> StringUtils.IndexOf("", "a")          = -1 
        /// <para/> StringUtils.IndexOf("aabaabaa", "a")  = 0
        /// <para/> StringUtils.IndexOf("aabaabaa", "b")  = 2
        /// <para/> StringUtils.IndexOf("aabaabaa", "ab") = 1
        /// <para/> StringUtils.IndexOf("aabaabaa", "")   = 0
        /// <para/> StringUtils.IndexOf("aabaabaa", "b", 3)  = 5
        /// <para/> StringUtils.IndexOf("aabaabaa", "b", 9)  = -1
        /// </para>
        /// <param name="s1">string</param>
        /// <param name="searchValue">search string</param>
        /// <param name="startPos">starting position</param>
        /// <returns>index of the string if found or -1</returns>
        public static int IndexOf(string s1, string searchValue, int startPos = 0)
        {
            return null == searchValue || null == s1 || startPos > s1.Length ? IndexNotFound : s1.IndexOf(searchValue, startPos, StringComparison.Ordinal);
        }

        /// <summary>
        /// Finds the first index of the given string within a string by ignoring case.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.IndexOfIgnoreCase(null, *)          = -1
        /// <para/> StringUtils.IndexOfIgnoreCase(*, null)          = -1
        /// <para/> StringUtils.IndexOfIgnoreCase("", "")           = 0
        /// <para/> StringUtils.IndexOfIgnoreCase("aabaabaa", "A")  = 0
        /// <para/> StringUtils.IndexOfIgnoreCase("aabaabaa", "B")  = 2
        /// <para/> StringUtils.IndexOfIgnoreCase("aabaabaa", "ab") = 1
        /// <para/> StringUtils.IndexOfIgnoreCase("aabaabaa", "B", 3)  = 5
        /// <para/> StringUtils.IndexOfIgnoreCase("aabaabaa", "B", 9)  = -1
        /// </para>
        /// <param name="s1">string</param>
        /// <param name="searchValue">search string</param>
        /// <param name="startPos">starting position</param>
        /// <returns>index of the string if found or -1</returns>
        public static int IndexOfIgnoreCase(string s1, string searchValue, int startPos = 0)
        {
            return null == searchValue || null == s1 || startPos > s1.Length ? IndexNotFound : s1.IndexOf(searchValue, startPos, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Finds the first index of the given character within a string by ignoring case.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.IndexOfIgnoreCase(null, *)         = -1
        /// <para/> StringUtils.IndexOfIgnoreCase("", *)           = -1
        /// <para/> StringUtils.IndexOfIgnoreCase("aabaabaa", 'A') = 0
        /// <para/> StringUtils.IndexOfIgnoreCase("aabaabaa", 'b') = 2
        /// <para/> StringUtils.IndexOfIgnoreCase("aabaabaa", 'B', 3)  = 5
        /// </para>
        /// <param name="s1">string</param>
        /// <param name="searchValue">search char</param>
        /// <param name="startPos">starting position</param>
        /// <returns>index of the char if found or -1</returns>
        public static int IndexOfIgnoreCase(string s1, char searchValue, int startPos = 0)
        {
            return IndexOfIgnoreCase(s1, Convert.ToString(searchValue), startPos);
        }

        /// <summary>
        /// Finds the last index of the given string within a string.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.LastIndexOf(null, *)         = -1
        /// <para/> StringUtils.LastIndexOf("", *)           = -1
        /// <para/> StringUtils.LastIndexOf("aabaabaa", "a") = 7
        /// <para/> StringUtils.LastIndexOf("aabaabaa", "ab") = 4
        /// <para/> StringUtils.LastIndexOf("aabaabaa", "b", 4)  = 2
        /// <para/> StringUtils.LastIndexOf("aabaabaa", "b", 7)  = 5
        /// </para>
        /// <param name="s1">string</param>
        /// <param name="searchValue">search string</param>
        /// <param name="startPos">starting position</param>
        /// <returns>index of the string if found or -1</returns>
        public static int LastIndexOf(string s1, string searchValue, int? startPos = null)
        {
            return null == searchValue || null == s1 || (startPos != null && startPos > s1.Length) ? IndexNotFound : s1.LastIndexOf(searchValue, startPos ?? s1.Length - 1, StringComparison.Ordinal);
        }

        /// <summary>
        /// Finds the last index of the given char within a string.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.LastIndexOf(null, *)         = -1
        /// <para/> StringUtils.LastIndexOf("", *)           = -1
        /// <para/> StringUtils.LastIndexOf("aabaabaa", 'a') = 7
        /// <para/> StringUtils.LastIndexOf("aabaabaa", 'b', 0)  = -1
        /// <para/> StringUtils.LastIndexOf("aabaabaa", 'b', 7)  = 5
        /// </para>
        /// <param name="s1">string</param>
        /// <param name="searchValue">search char</param>
        /// <param name="startPos">starting position</param>
        /// <returns>index of the char if found or -1</returns>
        public static int LastIndexOf(string s1, char searchValue, int? startPos = null)
        {
            return LastIndexOf(s1, Convert.ToString(searchValue), startPos);
        }

        /// <summary>
        /// Finds the last index of the given string within a string.,ignoring case.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.LastIndexOfIgnoreCase(null, *)         = -1
        /// <para/> StringUtils.LastIndexOfIgnoreCase("", *)           = -1
        /// <para/> StringUtils.LastIndexOfIgnoreCase("aabaabaa", "A") = 7
        /// <para/> StringUtils.LastIndexOfIgnoreCase("aabaabaa", "ab") = 4
        /// <para/> StringUtils.LastIndexOfIgnoreCase("aabaabaa", "B", 4)  = 2
        /// <para/> StringUtils.LastIndexOfIgnoreCase("aabaabaa", "b", 7)  = 5
        /// <para/> StringUtils.LastIndexOfIgnoreCase("aabaabaa", "b", 9)  = -1
        /// </para>
        /// <param name="s1">string</param>
        /// <param name="searchValue">search string</param>
        /// <param name="startPos">starting position</param>
        /// <returns>index of the string if found or -1</returns>
        public static int LastIndexOfIgnoreCase(string s1, string searchValue, int? startPos = null)
        {
            return null == searchValue || null == s1 || (startPos != null && startPos > s1.Length) ? IndexNotFound : s1.LastIndexOf(searchValue, startPos ?? s1.Length - 1, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Finds the last index of the given char within a string.,ignoring case.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.LastIndexOfIgnoreCase(null, *)         = -1
        /// <para/> StringUtils.LastIndexOfIgnoreCase("", *)           = -1
        /// <para/> StringUtils.LastIndexOfIgnoreCase("aabaabaa", 'A') = 7
        /// <para/> StringUtils.LastIndexOfIgnoreCase("aabaabaa", 'b', 0)  = -1
        /// <para/> StringUtils.LastIndexOfIgnoreCase("aabaabaa", 'B', 7)  = 5
        /// </para>
        /// <param name="s1">string</param>
        /// <param name="searchValue">search char</param>
        /// <param name="startPos">starting position</param>
        /// <returns>index of the char if found or -1</returns>
        public static int LastIndexOfIgnoreCase(string s1, char searchValue, int? startPos = null)
        {
            return LastIndexOfIgnoreCase(s1, Convert.ToString(searchValue), startPos);
        }

        /// <summary>
        /// Finds the n-th index of the given string within a string.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.OrdinalIndexOf(null, *, *)          = -1
        /// <para/> StringUtils.OrdinalIndexOf(*, null, *)          = -1
        /// <para/> StringUtils.OrdinalIndexOf("", "", *)           = 0
        /// <para/> StringUtils.OrdinalIndexOf("aabaabaa", "a", 1)  = 0
        /// <para/> StringUtils.OrdinalIndexOf("aabaabaa", "a", 2)  = 1
        /// <para/> StringUtils.OrdinalIndexOf("aabaabaa", "b", 1)  = 2
        /// <para/> StringUtils.OrdinalIndexOf("aabaabaa", "b", 2)  = 5
        /// <para/> StringUtils.OrdinalIndexOf("aabaabaa", "ab", 1) = 1
        /// <para/> StringUtils.OrdinalIndexOf("aabaabaa", "ab", 2) = 4
        /// <para/> StringUtils.OrdinalIndexOf("aabaabaa", "", 1)   = 0
        /// <para/> StringUtils.OrdinalIndexOf("aabaabaa", "", 2)   = 0
        /// </para>
        /// <param name="str">string</param>
        /// <param name="searchValue">search string</param>
        /// <param name="ordinal">n-th index</param>
        /// <returns>index of the string if found or -1</returns>
        public static int OrdinalIndexOf(string str, string searchValue, int ordinal)
        {
            return OrdinalIndexOf(str, searchValue, ordinal, false);
        }

        /// <summary>
        /// Finds the n-th index of the given char within a string.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.OrdinalIndexOf(null, *, *)          = -1
        /// <para/> StringUtils.OrdinalIndexOf("aabaabaa", 'a', 1)  = 0
        /// <para/> StringUtils.OrdinalIndexOf("aabaabaa", 'a', 2)  = 1
        /// <para/> StringUtils.OrdinalIndexOf("aabaabaa", 'b', 1)  = 2
        /// <para/> StringUtils.OrdinalIndexOf("aabaabaa", 'b', 2)  = 5
        /// </para>
        /// <param name="str">string</param>
        /// <param name="searchValue">search char</param>
        /// <param name="ordinal">n-th index</param>
        /// <returns>index of the char if found or -1</returns>
        public static int OrdinalIndexOf(string str, char searchValue, int ordinal)
        {
            return OrdinalIndexOf(str, Convert.ToString(searchValue), ordinal, false);
        }

        /// <summary>
        /// Finds the n-th Last index of the given string within a string.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.LastOrdinalIndexOf(null, *, *)          = -1
        /// <para/> StringUtils.LastOrdinalIndexOf(*, null, *)          = -1
        /// <para/> StringUtils.LastOrdinalIndexOf("", "", *)           = 0
        /// <para/> StringUtils.LastOrdinalIndexOf("aabaabaa", "a", 1)  = 7
        /// <para/> StringUtils.LastOrdinalIndexOf("aabaabaa", "a", 2)  = 6
        /// <para/> StringUtils.LastOrdinalIndexOf("aabaabaa", "b", 1)  = 5
        /// <para/> StringUtils.LastOrdinalIndexOf("aabaabaa", "b", 2)  = 2
        /// <para/> StringUtils.LastOrdinalIndexOf("aabaabaa", "ab", 1) = 4
        /// <para/> StringUtils.LastOrdinalIndexOf("aabaabaa", "ab", 2) = 1
        /// <para/> StringUtils.LastOrdinalIndexOf("aabaabaa", "", 1)   = 8
        /// <para/> StringUtils.LastOrdinalIndexOf("aabaabaa", "", 2)   = 8
        /// </para>
        /// <param name="str">string</param>
        /// <param name="searchValue">search string</param>
        /// <param name="ordinal">n-th last index</param>
        /// <returns>index of the string if found or -1</returns>
        public static int LastOrdinalIndexOf(string str, string searchValue, int ordinal)
        {
            return OrdinalIndexOf(str, searchValue, ordinal, true);
        }

        /// <summary>
        /// Finds the n-th Last index of the given char within a string.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.LastOrdinalIndexOf(null, *, *)          = -1
        /// <para/> StringUtils.LastOrdinalIndexOf("aabaabaa", 'a', 1)  = 7
        /// <para/> StringUtils.LastOrdinalIndexOf("aabaabaa", 'a', 2)  = 6
        /// <para/> StringUtils.LastOrdinalIndexOf("aabaabaa", 'b', 1)  = 5
        /// <para/> StringUtils.LastOrdinalIndexOf("aabaabaa", 'b', 2)  = 2
        /// </para>
        /// <param name="str">string</param>
        /// <param name="searchValue">search char</param>
        /// <param name="ordinal">n-th last index</param>
        /// <returns>index of the char if found or -1</returns>
        public static int LastOrdinalIndexOf(string str, char searchValue, int ordinal)
        {
            return OrdinalIndexOf(str, Convert.ToString(searchValue), ordinal, true);
        }

        /// <summary>
        /// Checks if a string conatins the given search string.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Contains(null, *)     = false
        /// <para/> StringUtils.Contains(*, null)     = false
        /// <para/> StringUtils.Contains("", "")      = true
        /// <para/> StringUtils.Contains("abc", "")   = true
        /// <para/> StringUtils.Contains("abc", "a")  = true
        /// <para/> StringUtils.Contains("abc", "z")  = false
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="searchValue">search string</param>
        /// <returns>true or false</returns>
        public static bool Contains(string str, string searchValue)
        {
            if (str == null || searchValue == null)
                return false;
            return str.Contains(searchValue);
        }

        /// <summary>
        /// Checks if a string conatins the given search character.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Contains(null, *)    = false
        /// <para/> StringUtils.Contains("", *)      = false
        /// <para/> StringUtils.Contains("abc", 'a') = true
        /// <para/> StringUtils.Contains("abc", 'z') = false
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="searchValue">search char</param>
        /// <returns>true or false</returns>
        public static bool Contains(string str, char searchValue)
        {
            return null != str && str.Contains(searchValue);
        }

        /// <summary>
        /// Checks if a string conatins the given search string.,ignoring case
        /// </summary>
        /// <para>
        /// <para/> StringUtils.ContainsIgnoreCase(null, *)     = false
        /// <para/> StringUtils.ContainsIgnoreCase(*, null)     = false
        /// <para/> StringUtils.ContainsIgnoreCase("", "")      = true
        /// <para/> StringUtils.ContainsIgnoreCase("abc", "")   = true
        /// <para/> StringUtils.ContainsIgnoreCase("abc", "A")  = true
        /// <para/> StringUtils.ContainsIgnoreCase("abc", "z")  = false
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="searchValue">search string</param>
        /// <returns>true or false</returns>
        public static bool ContainsIgnoreCase(string str, string searchValue)
        {
            if (str == null || searchValue == null)
                return false;
            return IndexOfIgnoreCase(str, searchValue) >= 0;
        }

        /// <summary>
        /// Checks if a string conatins the given search character.,ignoring case
        /// </summary>
        /// <para>
        /// <para/> StringUtils.ContainsIgnoreCase(null, *)    = false
        /// <para/> StringUtils.ContainsIgnoreCase("", *)      = false
        /// <para/> StringUtils.ContainsIgnoreCase("abc", 'A') = true
        /// <para/> StringUtils.ContainsIgnoreCase("abc", 'z') = false
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="searchValue">search char</param>
        /// <returns>true or false</returns>
        public static bool ContainsIgnoreCase(string str, char searchValue)
        {
            return null != str && IndexOfIgnoreCase(str, searchValue) >= 0;
        }

        /// <summary>
        /// Checks whether a given string contains any Whitespace characters.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.ContainsWhiteSpace(null)    = false
        /// <para/> StringUtils.ContainsWhiteSpace("")      = false
        /// <para/> StringUtils.ContainsWhiteSpace("abc ")  = true
        /// <para/> StringUtils.ContainsWhiteSpace("a bc")  = true
        /// <para/> StringUtils.ContainsWhiteSpace("abc")   = false
        /// </para>
        /// <param name="str">input string</param>
        /// <returns>true or false</returns>
        public static bool ContainsWhiteSpace(string str)
        {
            return null != str && str.Contains(Space);
        }

        /// <summary>
        /// Search a string to find the first index of any character in the given set of search characters.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.IndexOfAny(null, *)                = -1
        /// <para/> StringUtils.IndexOfAny("", *)                  = -1
        /// <para/> StringUtils.IndexOfAny(*, null)                = -1
        /// <para/> StringUtils.IndexOfAny(*, [])                  = -1
        /// <para/> StringUtils.IndexOfAny("zzabyycdxx",'z','a') = 0
        /// <para/> StringUtils.IndexOfAny("zzabyycdxx",['b','y']) = 3
        /// <para/> StringUtils.IndexOfAny("aba", 'z')           = -1
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="searchChars">search characters</param>
        /// <returns>index if found or -1</returns>
        public static int IndexOfAny(string str, params char[] searchChars)
        {
            if (str == null || CollectionUtils.IsEmpty(searchChars))
                return IndexNotFound;
            var strLen = str.Length;
            var strLast = strLen - 1;
            var searchLen = searchChars.Length;
            var searchLast = searchLen - 1;
            for (var i = 0; i < strLen; i++)
            {
                var ch = str[i];
                for (var j = 0; j < searchLen; j++)
                {
                    if (searchChars[j] != ch) continue;
                    if (i < strLast && j < searchLast && char.IsHighSurrogate(ch))
                    {
                        // ch is a supplementary character
                        if (searchChars[j + 1] == str[i + 1])
                        {
                            return i;
                        }
                    }
                    else
                    {
                        return i;
                    }
                }
            }
            return IndexNotFound;
        }

        /// <summary>
        /// Search a string to find the first index of any character in the given set of search characters.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.IndexOfAny(null, *)                = -1
        /// <para/> StringUtils.IndexOfAny("", *)                  = -1
        /// <para/> StringUtils.IndexOfAny(*, null)                = -1
        /// <para/> StringUtils.IndexOfAny("zzabyycdxx","za") = 0
        /// <para/> StringUtils.IndexOfAny("zzabyycdxx","by") = 3
        /// <para/> StringUtils.IndexOfAny("aba", "z")           = -1
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="searchChars">search characters</param>
        /// <returns>index if found or -1</returns>
        public static int IndexOfAny(string str, string searchChars)
        {
            if (IsEmpty(str) || IsEmpty(searchChars))
                return IndexNotFound;
            return IndexOfAny(str, searchChars.ToCharArray());
        }

        /// <summary>
        /// Checks if the string contains any character from the given set of characters.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.ContainsAny(null, *)                = false
        /// <para/> StringUtils.ContainsAny("", *)                  = false
        /// <para/> StringUtils.ContainsAny(*, null)                = false
        /// <para/> StringUtils.ContainsAny(*, [])                  = false
        /// <para/> StringUtils.ContainsAny("zzabyycdxx",'z','a')   = true
        /// <para/> StringUtils.ContainsAny("zzabyycdxx",'b','y')   = true
        /// <para/> StringUtils.ContainsAny("zzabyycdxx",'z','y') = true
        /// <para/> StringUtils.ContainsAny("aba", 'z')           = false
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="searchChars">search characters</param>
        /// <returns>true or false</returns>
        public static bool ContainsAny(string str, params char[] searchChars)
        {
            if (str == null || CollectionUtils.IsEmpty(searchChars))
                return false;
            return IndexOfAny(str, searchChars) >= 0;
        }

        /// <summary>
        /// Checks if the string contains any character from the given set of characters.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.ContainsAny(null, *)                = false
        /// <para/> StringUtils.ContainsAny("", *)                  = false
        /// <para/> StringUtils.ContainsAny(*, null)                = false
        /// <para/> StringUtils.ContainsAny("zzabyycdxx","za")      = true
        /// <para/> StringUtils.ContainsAny("zzabyycdxx","by")      = true
        /// <para/> StringUtils.ContainsAny("zzabyycdxx","zy"])     = true
        /// <para/> StringUtils.ContainsAny("aba", "z")             = false
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="searchChars">search characters</param>
        /// <returns>true or false</returns>
        public static bool ContainsAny(string str, string searchChars)
        {
            if (str == null || IsEmpty(searchChars))
                return false;
            return IndexOfAny(str, searchChars) >= 0;
        }

        /// <summary>
        /// Checks if the string contains any of the strings in the given array.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.ContainsAny(null, *)            = false
        /// <para/> StringUtils.ContainsAny("", *)              = false
        /// <para/> StringUtils.ContainsAny(*, null)            = false
        /// <para/> StringUtils.ContainsAny(*, [])              = false
        /// <para/> StringUtils.ContainsAny("abcd", "ab", null) = true
        /// <para/> StringUtils.ContainsAny("abcd", "ab", "cd") = true
        /// <para/> StringUtils.ContainsAny("abc", "d", "abc")  = true
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="searchStrings">search strings</param>
        /// <returns>true or false</returns>
        public static bool ContainsAny(string str, params string[] searchStrings)
        {
            if (str == null || CollectionUtils.IsEmpty(searchStrings))
                return false;
            return searchStrings.Any(i => Contains(str, i));

        }

        /// <summary>
        /// Searches a string to find the first index of any character not in the given set of characters.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.IndexOfAnyBut(null, *)                              = -1
        /// <para/> StringUtils.IndexOfAnyBut("", *)                                = -1
        /// <para/> StringUtils.IndexOfAnyBut(*, null)                              = -1
        /// <para/> StringUtils.IndexOfAnyBut(*, [])                                = -1
        /// <para/> StringUtils.IndexOfAnyBut("zzabyycdxx", 'z', 'a' )              = 3
        /// <para/> StringUtils.IndexOfAnyBut("aba", 'z' )                          = 0
        /// <para/> StringUtils.IndexOfAnyBut("aba", new char[] {'a', 'b'} )        = -1
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="searchChars">not in search characters</param>
        /// <returns>true or false</returns>
        public static int IndexOfAnyBut(string str, params char[] searchChars)
        {
            if (str == null || CollectionUtils.IsEmpty(searchChars))
            {
                return IndexNotFound;
            }
            var strLen = str.Length;
            var strLast = strLen - 1;
            var searchLen = searchChars.Length;
            var searchLast = searchLen - 1;
            var i = 0;
        outer:
            while (i < strLen)
            {
                var ch = str[i];
                for (var j = 0; j < searchLen; j++)
                {
                    if (searchChars[j] != ch) continue;
                    if (i < strLast && j < searchLast && char.IsHighSurrogate(ch))
                    {
                        if (searchChars[j + 1] == str[i + 1])
                        {
                            i++;
                            goto outer;
                        }
                    }
                    else
                    {
                        i++;
                        goto outer;
                    }
                }
                return i;
            }
            return IndexNotFound;
        }

        /// <summary>
        /// Searches a string to find the first index of any character not in the given set of characters.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.IndexOfAnyBut(null, *)            = -1
        /// <para/> StringUtils.IndexOfAnyBut("", *)              = -1
        /// <para/> StringUtils.IndexOfAnyBut(*, null)            = -1
        /// <para/> StringUtils.IndexOfAnyBut(*, "")              = -1
        /// <para/> StringUtils.IndexOfAnyBut("zzabyycdxx", "za") = 3
        /// <para/> StringUtils.IndexOfAnyBut("zzabyycdxx", "")   = -1
        /// <para/> StringUtils.IndexOfAnyBut("aba","ab")         = -1
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="searchChars">not in search characters</param>
        /// <returns>true or false</returns>
        public static int IndexOfAnyBut(string str, string searchChars)
        {
            if (str == null || IsEmpty(searchChars))
                return IndexNotFound;

            return IndexOfAnyBut(str, searchChars.ToCharArray());
        }

        /// <summary>
        /// Checks if the string contains only the given set of characters.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.ContainsOnly(null, *)       = false
        /// <para/> StringUtils.ContainsOnly(*, null)       = false
        /// <para/> StringUtils.ContainsOnly("", *)         = true
        /// <para/> StringUtils.ContainsOnly("ab", '')      = false
        /// <para/> StringUtils.ContainsOnly("abab", 'a','b','c') = true
        /// <para/> StringUtils.ContainsOnly("ab1", 'a','b','c')  = false
        /// <para/> StringUtils.ContainsOnly("abz", ['a','b','c'])  = false
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="searchChars">search characters</param>
        /// <returns>true or false</returns>
        public static bool ContainsOnly(string str, params char[] searchChars)
        {
            if (null == str || null == searchChars)
                return false;
            if (str.Length == 0)
                return true;
            if (searchChars.Length == 0)
                return false;
            return IndexOfAnyBut(str, searchChars) == IndexNotFound;
        }

        /// <summary>
        /// Checks if the string contains only the given set of characters.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.ContainsOnly(null, *)       = false
        /// <para/> StringUtils.ContainsOnly(*, null)       = false
        /// <para/> StringUtils.ContainsOnly("", *)         = true
        /// <para/> StringUtils.ContainsOnly("ab", "")      = false
        /// <para/> StringUtils.ContainsOnly("abab", "abc") = true
        /// <para/> StringUtils.ContainsOnly("ab1", "abc")  = false
        /// <para/> StringUtils.ContainsOnly("abz", "abc")  = false
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="searchChars">search characters</param>
        /// <returns>true or false</returns>
        public static bool ContainsOnly(string str, string searchChars)
        {
            if (null == str || null == searchChars)
                return false;
            return ContainsOnly(str, searchChars.ToCharArray());
        }

        /// <summary>
        /// Checks if the string contains none of the given set of characters.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.ContainsNone(null, *)       = true
        /// <para/> StringUtils.ContainsNone(*, null)       = true
        /// <para/> StringUtils.ContainsNone("", *)         = true
        /// <para/> StringUtils.ContainsNone("ab", 'a')      = false
        /// <para/> StringUtils.ContainsNone("abab", 'x','y','z') = true
        /// <para/> StringUtils.ContainsNone("ab1", 'x','y','z')  = true
        /// <para/> StringUtils.ContainsNone("abz", ['x','y','z'])  = false
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="searchChars">search characters</param>
        /// <returns>true or false</returns>
        public static bool ContainsNone(string str, params char[] searchChars)
        {
            if (str == null || searchChars == null)
            {
                return true;
            }
            var strLen = str.Length;
            var strLast = strLen - 1;
            var searchLen = searchChars.Length;
            var searchLast = searchLen - 1;
            for (var i = 0; i < strLen; i++)
            {
                var ch = str[i];
                for (var j = 0; j < searchLen; j++)
                {
                    if (searchChars[j] != ch) continue;
                    if (char.IsHighSurrogate(ch))
                    {
                        if (j == searchLast)
                        {
                            // missing low surrogate, fine, like String.indexOf(String)
                            return false;
                        }
                        if (i < strLast && searchChars[j + 1] == str[i + 1])
                        {
                            return false;
                        }
                    }
                    else
                    {
                        // ch is in the Basic Multilingual Plane
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Checks if the string contains none of the given set of characters.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.ContainsNone(null, *)       = true
        /// <para/> StringUtils.ContainsNone(*, null)       = true
        /// <para/> StringUtils.ContainsNone("", *)         = true
        /// <para/> StringUtils.ContainsNone("ab", "")      = true
        /// <para/> StringUtils.ContainsNone("abab", "xyz") = true
        /// <para/> StringUtils.ContainsNone("ab1", "xyz")  = true
        /// <para/> StringUtils.ContainsNone("abz", "xyz")  = false
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="searchChars">search characters</param>
        /// <returns>true or false</returns>
        public static bool ContainsNone(string str, string searchChars)
        {
            if (null == str || null == searchChars)
                return true;
            return ContainsNone(str, searchChars.ToCharArray());
        }

        /// <summary>
        /// Searches a string to find the first index of any of the given set of substrings.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.IndexOfAny(null, *)                     = -1
        /// <para/> StringUtils.IndexOfAny(*, null)                     = -1
        /// <para/> StringUtils.IndexOfAny(*, [])                       = -1
        /// <para/> StringUtils.IndexOfAny("zzabyycdxx", "ab","cd")     = 2
        /// <para/> StringUtils.IndexOfAny("zzabyycdxx", "cd","ab")     = 2
        /// <para/> StringUtils.IndexOfAny("zzabyycdxx", "mn","op")     = -1
        /// <para/> StringUtils.IndexOfAny("zzabyycdxx", ["zab","aby"]) = 1
        /// <para/> StringUtils.IndexOfAny("zzabyycdxx", [""])          = 0
        /// <para/> StringUtils.IndexOfAny("", [""])                    = 0
        /// <para/> StringUtils.IndexOfAny("", ["a"])                   = -1
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="searchStrs">search strings array</param>
        /// <returns>index if found or -1</returns>
        public static int IndexOfAny(string str, params string[] searchStrs)
        {
            if (str == null || searchStrs == null)
            {
                return IndexNotFound;
            }
            var sz = searchStrs.Length;

            // String's can't have a MAX_VALUEth index.
            var ret = int.MaxValue;

            for (var i = 0; i < sz; i++)
            {
                var search = searchStrs[i];
                if (search == null)
                {
                    continue;
                }
                var tmp = IndexOf(str, search);
                if (tmp == IndexNotFound)
                {
                    continue;
                }

                if (tmp < ret)
                {
                    ret = tmp;
                }
            }

            return ret == int.MaxValue ? IndexNotFound : ret;
        }

        /// <summary>
        /// Searches a string to find the last index of any of the given set of substrings.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.LastIndexOfAny(null, *)                   = -1
        /// <para/> StringUtils.LastIndexOfAny(*, null)                   = -1
        /// <para/> StringUtils.LastIndexOfAny(*, [])                     = -1
        /// <para/> StringUtils.LastIndexOfAny(*, [null])                 = -1
        /// <para/> StringUtils.LastIndexOfAny("zzabyycdxx", "ab","cd")   = 6
        /// <para/> StringUtils.LastIndexOfAny("zzabyycdxx", "cd","ab")   = 6
        /// <para/> StringUtils.LastIndexOfAny("zzabyycdxx", "mn","op")   = -1
        /// <para/> StringUtils.LastIndexOfAny("zzabyycdxx", ["mn","op"]) = -1
        /// <para/> StringUtils.LastIndexOfAny("zzabyycdxx", ["mn",""])   = 9
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="searchStrs">search strings array</param>
        /// <returns>index if found or -1</returns>
        public static int LastIndexOfAny(string str, params string[] searchStrs)
        {
            if (str == null || searchStrs == null)
            {
                return IndexNotFound;
            }
            var sz = searchStrs.Length;
            var ret = IndexNotFound;
            for (var i = 0; i < sz; i++)
            {
                var search = searchStrs[i];
                if (search == null)
                {
                    continue;
                }
                var tmp = LastIndexOf(str, search);
                if (tmp > ret)
                {
                    ret = tmp;
                }
            }
            return ret;
        }

        #endregion

        #region Substring

        /// <summary>
        /// Gets a substring from the given string without raising exceptions.
        ///  </summary>
        /// <para>
        /// A negative start position can be used to start characters from the end of the String.
        /// Null string will return Null and Empty ("") string will return Empty ("") String.
        /// <para/> StringUtils.Substring(null, *)   = null
        /// <para/> StringUtils.Substring("", *)     = ""
        /// <para/> StringUtils.Substring("abc", 0)  = "abc"
        /// <para/> StringUtils.Substring("abc", 2)  = "c"
        /// <para/> StringUtils.Substring("abc", 4)  = ""
        /// <para/> StringUtils.Substring("abc", -2) = "bc"
        /// <para/> StringUtils.Substring("abc", -4) = "abc"
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="start">starting position, negative means count back from the end of the String by this many characters</param>
        /// <returns>string</returns>
        public static string Substring(string str, int start)
        {
            if (str == null)
            {
                return null;
            }

            // handle negatives, which means last n characters
            if (start < 0)
            {
                start = str.Length + start; // remember start is negative
            }

            if (start < 0)
            {
                start = 0;
            }
            return start > str.Length ? Empty : str.Substring(start);
        }

        /// <summary>
        /// Gets a substring from the given string without raising exceptions.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Substring(null, *, *)    = null
        /// <para/> StringUtils.Substring("", * ,  *)    = "";
        /// <para/> StringUtils.Substring("abc", 0, 2)   = "abc"
        /// <para/> StringUtils.Substring("abc", 2, 0)   = ""
        /// <para/> StringUtils.Substring("abc", 2, 4)   = "c"
        /// <para/> StringUtils.Substring("abc", 4, 6)   = ""
        /// <para/> StringUtils.Substring("abc", 2, 2)   = "c"
        /// <para/> StringUtils.Substring("abc", -2, -1) = null
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="start">starting position,should be non negative</param>
        /// <param name="end">ending position,should be non negative</param>
        /// <returns>string</returns>
        public static string Substring(string str, int start, int end)
        {
            if (str == null)
            {
                return null;
            }

            if (start < 0)
                return null;
            if (start == 0 && end < 0)
                return Empty;

            // check length next
            if (end >= str.Length)
            {
                end = str.Length - 1;
            }

            // if start is greater than end, return null
            if (start > end)
            {
                return Empty;
            }

            var length = end - start + 1;

            return str.Substring(start, length);
        }

        /// <summary>
        /// Gets a substring from the given string without raising exceptions.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Substring(null, *, *)    = null
        /// <para/> StringUtils.Substring("", * ,  *)    = "";
        /// <para/> StringUtils.Substring("abc", 0, 2)   = "abc"
        /// <para/> StringUtils.Substring("abc", 2, 0)   = ""
        /// <para/> StringUtils.Substring("abc", 2, 4)   = "c"
        /// <para/> StringUtils.Substring("abc", 4, 6)   = ""
        /// <para/> StringUtils.Substring("abc", 2, 2)   = "c"
        /// <para/> StringUtils.Substring("abc", -2, -1) = "bc"
        /// <para/> StringUtils.Substring("abc", -4, 2)  = "abc"
        /// <para/> StringUtils.Substring("abcde", -2, 4)= "de"
        /// <para/> StringUtils.Substring("abcde", -2, 2)= ""
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="start">starting position, negative means count back from the end of the String by this many characters</param>
        /// <param name="end">ending position, negative means count back from the end of the String by this many characters</param>
        /// <returns>string</returns>
        public static string SubstringWithNegatives(string str, int start, int end)
        {
            if (str == null)
            {
                return null;
            }

            // handle negatives
            if (end < 0)
            {
                end = str.Length + end; // remember end is negative
            }
            if (start < 0)
            {
                start = str.Length + start; // remember start is negative
            }

            // check length next
            if (end >= str.Length)
            {
                end = str.Length - 1;
            }

            // if start is greater than end, return ""
            if (start > end)
            {
                return Empty;
            }

            if (start < 0)
            {
                start = 0;
            }
            if (end < 0)
            {
                end = 0;
            }
            var length = end - start + 1;

            return str.Substring(start, length);
        }

        /// <summary>
        /// Gets the left most Characters of string.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Left(null, *)    = null
        /// <para/> StringUtils.Left(*, -1)      = "" (-ve length returns Empty string)
        /// <para/> StringUtils.Left("", *)      = ""
        /// <para/> StringUtils.Left("abc", 0)   = ""
        /// <para/> StringUtils.Left("abc", 2)   = "ab"
        /// <para/> StringUtils.Left("abc", 4)   = "abc"
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="len">length</param>
        /// <returns>string with left most chars</returns>
        public static string Left(string str, int len)
        {
            if (null == str)
                return null;
            if (len < 0)
                return Empty;
            return len >= str.Length ? str : str.Substring(0, len);
        }

        /// <summary>
        /// Gets the right most Characters of string.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Right(null, *)    = null
        /// <para/> StringUtils.Right(*, -1)      = "" (-ve length returns Empty string)
        /// <para/> StringUtils.Right("", *)      = ""
        /// <para/> StringUtils.Right("abc", 0)   = ""
        /// <para/> StringUtils.Right("abc", 2)   = "bc"
        /// <para/> StringUtils.Right("abc", 4)   = "abc"
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="len">length</param>
        /// <returns>string with right most chars</returns>
        public static string Right(string str, int len)
        {
            if (null == str)
                return null;
            if (len < 0)
                return Empty;
            return len >= str.Length ? str : str.Substring(str.Length - len);
        }

        /// <summary>
        /// Gets characters from the middle of a string.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Mid(null, *, *)    = null
        /// <para/> StringUtils.Mid(*, *, -1)      = "" (-ve length returns Empty string)
        /// <para/> StringUtils.Mid("", 0, *)      = ""
        /// <para/> StringUtils.Mid("abc", 0, 2)   = "ab"
        /// <para/> StringUtils.Mid("abc", 0, 4)   = "abc"
        /// <para/> StringUtils.Mid("abc", 2, 4)   = "c"
        /// <para/> StringUtils.Mid("abc", 4, 2)   = ""
        /// <para/> StringUtils.Mid("abc", -2, 2)  = "ab"
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="pos">starting position</param>
        /// <param name="len">legth of the mid string</param>
        /// <returns>string with midlle chars</returns>
        public static string Mid(string str, int pos, int len)
        {
            if (null == str)
                return null;
            if (len < 0 || pos > str.Length)
                return Empty;
            if (pos < 0)
                pos = 0;
            return str.Length <= pos + len ? str.Substring(pos) : str.Substring(pos, pos + len);
        }

        /// <summary>
        /// Gets the substring before the first occurrence of a separator.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.SubstringBefore(null, *)      = null
        /// <para/> StringUtils.SubstringBefore("", *)        = ""
        /// <para/> StringUtils.SubstringBefore("abc", "a")   = ""
        /// <para/> StringUtils.SubstringBefore("abcba", "b") = "a"
        /// <para/> StringUtils.SubstringBefore("abc", "c")   = "ab"
        /// <para/> StringUtils.SubstringBefore("abc", "d")   = "abc"
        /// <para/> StringUtils.SubstringBefore("abc", "")    = ""
        /// <para/> StringUtils.SubstringBefore("abc", null)  = "abc"
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="separator">seperator</param>
        /// <returns>the substring before the first occurrence of the separator</returns>
        public static string SubstringBefore(string str, string separator)
        {
            if (IsEmpty(str) || separator == null)
            {
                return str;
            }
            if (IsEmpty(separator))
            {
                return Empty;
            }
            var pos = str.IndexOf(separator, StringComparison.Ordinal);
            return pos == IndexNotFound ? str : str.Substring(0, pos);
        }

        /// <summary>
        /// Gets the substring after the first occurrence of a separator.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.SubstringAfter(null, *)      = null
        /// <para/> StringUtils.SubstringAfter("", *)        = ""
        /// <para/> StringUtils.SubstringAfter(*, null)      = ""
        /// <para/> StringUtils.SubstringAfter("abc", "a")   = "bc"
        /// <para/> StringUtils.SubstringAfter("abcba", "b") = "cba"
        /// <para/> StringUtils.SubstringAfter("abc", "c")   = ""
        /// <para/> StringUtils.SubstringAfter("abc", "d")   = ""
        /// <para/> StringUtils.SubstringAfter("abc", "")    = "abc"
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="separator">seperator</param>
        /// <returns>the substring after the first occurrence of the separator</returns>
        public static string SubstringAfter(string str, string separator)
        {
            if (IsEmpty(str))
            {
                return str;
            }
            if (null == separator)
            {
                return Empty;
            }
            var pos = str.IndexOf(separator, StringComparison.Ordinal);
            return pos == IndexNotFound ? Empty : str.Substring(pos + separator.Length);
        }

        /// <summary>
        /// Gets the substring before the last occurrence of a separator.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.SubstringBeforeLast(null, *)      = null
        /// <para/> StringUtils.SubstringBeforeLast("", *)        = ""
        /// <para/> StringUtils.SubstringBeforeLast("abcba", "b") = "abc"
        /// <para/> StringUtils.SubstringBeforeLast("abc", "c")   = "ab"
        /// <para/> StringUtils.SubstringBeforeLast("a", "a")     = ""
        /// <para/> StringUtils.SubstringBeforeLast("a", "z")     = "a"
        /// <para/> StringUtils.SubstringBeforeLast("a", null)    = "a"
        /// <para/> StringUtils.SubstringBeforeLast("a", "")      = "a"
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="separator">seperator</param>
        /// <returns>the substring before the last occurrence of a separator.</returns>
        public static string SubstringBeforeLast(string str, string separator)
        {
            if (IsEmpty(str) || IsEmpty(separator))
            {
                return str;
            }
            var pos = str.LastIndexOf(separator, StringComparison.Ordinal);
            return pos == IndexNotFound ? str : str.Substring(0, pos);
        }

        /// <summary>
        /// Gets the substring after the last occurrence of a separator.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.SubstringAfterLast(null, *)      = null
        /// <para/> StringUtils.SubstringAfterLast("", *)        = ""
        /// <para/> StringUtils.SubstringAfterLast(*, "")        = ""
        /// <para/> StringUtils.SubstringAfterLast(*, null)      = ""
        /// <para/> StringUtils.SubstringAfterLast("abc", "a")   = "bc"
        /// <para/> StringUtils.SubstringAfterLast("abcba", "b") = "a"
        /// <para/> StringUtils.SubstringAfterLast("abc", "c")   = ""
        /// <para/> StringUtils.SubstringAfterLast("a", "a")     = ""
        /// <para/> StringUtils.SubstringAfterLast("a", "z")     = ""
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="separator">seperator</param>
        /// <returns>the substring after the last occurrence of a separator.</returns>
        public static string SubstringAfterLast(string str, string separator)
        {
            if (IsEmpty(str))
            {
                return str;
            }
            if (IsEmpty(separator))
            {
                return Empty;
            }
            var pos = str.LastIndexOf(separator, StringComparison.Ordinal);
            if (pos == IndexNotFound || pos == str.Length - separator.Length)
            {
                return Empty;
            }
            return str.Substring(pos + separator.Length);
        }

        /// <summary>
        /// Gets the String that is nested in between two instances of the same String.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.SubstringBetween(null, *)            = null
        /// <para/> StringUtils.SubstringBetween("", "")             = ""
        /// <para/> StringUtils.SubstringBetween("", "tag")          = null
        /// <para/> StringUtils.SubstringBetween("tagabctag", null)  = null
        /// <para/> StringUtils.SubstringBetween("tagabctag", "")    = ""
        /// <para/> StringUtils.SubstringBetween("tagabctag", "tag") = "abc"
        /// </para>
        /// <param name="str">the String containing the substring</param>
        /// <param name="tag">the String before and after the substring</param>
        /// <returns>the substring or Null if no match found</returns>
        public static string SubstringBetween(string str, string tag)
        {
            return SubstringBetween(str, tag, tag);
        }

        /// <summary>
        /// Gets the String that is nested in between two Strings.
        /// </summary>
        /// <para>
        /// Only the first match is returned.
        /// <para/> StringUtils.SubstringBetween("wx[b]yz", "[", "]") = "b"
        /// <para/> StringUtils.SubstringBetween(null, *, *)          = null
        /// <para/> StringUtils.SubstringBetween(*, null, *)          = null
        /// <para/> StringUtils.SubstringBetween(*, *, null)          = null
        /// <para/> StringUtils.SubstringBetween("", "", "")          = ""
        /// <para/> StringUtils.SubstringBetween("", "", "]")         = null
        /// <para/> StringUtils.SubstringBetween("", "[", "]")        = null
        /// <para/> StringUtils.SubstringBetween("yabcz", "", "")     = ""
        /// <para/> StringUtils.SubstringBetween("yabcz", "y", "z")   = "abc"
        /// <para/> StringUtils.SubstringBetween("yabczyabcz", "y", "z")   = "abc"
        /// </para>
        /// <param name="str">input String containing the substring</param>
        /// <param name="open">the String before the substring</param>
        /// <param name="close">the String after the substring</param>
        /// <returns></returns>
        public static string SubstringBetween(string str, string open, string close)
        {
            if (str == null || open == null || close == null)
            {
                return null;
            }
            var start = str.IndexOf(open, StringComparison.Ordinal);
            if (start == IndexNotFound) return null;
            var end = str.IndexOf(close, start + open.Length, StringComparison.Ordinal);
            return end != IndexNotFound ? Substring(str, start + open.Length, end - 1) : null;
        }

        /// <summary>
        /// Searches a String for substrings delimited by a start and end tag, returning all matching substrings in an array.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.SubstringsBetween("[a][b][c]", "[", "]") = ["a","b","c"]
        /// <para/> StringUtils.SubstringsBetween(null, *, *)            = null
        /// <para/> StringUtils.SubstringsBetween(*, null, *)            = null
        /// <para/> StringUtils.SubstringsBetween(*, *, null)            = null
        /// <para/> StringUtils.SubstringsBetween("", "[", "]")          = []
        /// </para>
        /// <param name="str">the String containing the substrings</param>
        /// <param name="open">the String identifying the start of the substring</param>
        /// <param name="close">the String identifying the end of the substring</param>
        /// <returns>a String Array of substrings or Null if no match found</returns>
        public static string[] SubstringsBetween(string str, string open, string close)
        {
            if (str == null || IsEmpty(open) || IsEmpty(close))
            {
                return null;
            }
            var strLen = str.Length;
            if (strLen == 0)
            {
                return CollectionUtils.EmptyStringArray;
            }
            var closeLen = close.Length;
            var openLen = open.Length;
            var list = new List<string>();
            var pos = 0;
            while (pos < strLen - closeLen)
            {
                var start = str.IndexOf(open, pos, StringComparison.Ordinal);
                if (start < 0)
                {
                    break;
                }
                start += openLen;
                var end = str.IndexOf(close, start, StringComparison.Ordinal);
                if (end <= 0)
                {
                    break;
                }
                list.Add(Substring(str, start, end - 1));
                pos = end + closeLen;
            }
            return CollectionUtils.IsEmpty(list) ? null : list.ToArray();
        }

        #endregion

        #region Split

        /// <summary>
        /// Splits the given string into an array, using whitespace as the separator.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Split(null)       = null
        /// <para/> StringUtils.Split("")         = []
        /// <para/> StringUtils.Split("abc def")  = ["abc", "def"]
        /// <para/> StringUtils.Split("abc  def") = ["abc", "def"]
        /// <para/> StringUtils.Split(" abc ")    = ["abc"] 
        /// </para>
        /// <param name="str">input string</param>
        /// <returns>an array of parsed Strings</returns>
        public static string[] Split(string str)
        {
            return Split(str, null, -1);
        }

        /// <summary>
        /// Splits the given string into an array, using the specified separator.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Split(null, *)         = null
        /// <para/> StringUtils.Split("", *)           = []
        /// <para/> StringUtils.Split("a.b.c", '.')    = ["a", "b", "c"]
        /// <para/> StringUtils.Split("a..b.c", '.')   = ["a", "b", "c"]
        /// <para/> StringUtils.Split("a:b:c", '.')    = ["a:b:c"]
        /// <para/> StringUtils.Split("a b c", ' ')    = ["a", "b", "c"]
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="separatorChar">the character used as the delimiter</param>
        /// <returns>an array of parsed Strings</returns>
        public static string[] Split(string str, char separatorChar)
        {
            return SplitWorker(str, separatorChar, false);
        }

        /// <summary>
        /// Splits the given string into an array, using the specified separators.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Split(null, *)         = null
        /// <para/> StringUtils.Split("", *)           = []
        /// <para/> StringUtils.Split("abc def", null) = ["abc", "def"]
        /// <para/> StringUtils.Split("abc def", " ")  = ["abc", "def"]
        /// <para/> StringUtils.Split("abc  def", " ") = ["abc", "def"]
        /// <para/> StringUtils.Split("ab:cd:ef", ":") = ["ab", "cd", "ef"]
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="separatorChars">the characters used as the delimiter</param>
        /// <returns>an array of parsed Strings</returns>
        public static string[] Split(string str, string separatorChars)
        {
            return SplitWorker(str, separatorChars, -1, false);
        }

        /// <summary>
        /// Splits the given string into an array with max length, using the specified separator string.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Split(null, *, *)            = null
        /// <para/> StringUtils.Split("", *, *)              = []
        /// <para/> StringUtils.Split("ab cd ef", null, 0)   = ["ab", "cd", "ef"]
        /// <para/> StringUtils.Split("ab   cd ef", null, 0) = ["ab", "cd", "ef"]
        /// <para/> StringUtils.Split("ab:cd:ef", ":", 0)    = ["ab", "cd", "ef"]
        /// <para/> StringUtils.Split("ab:cd:ef", ":", 2)    = ["ab", "cd:ef"]
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="separatorChars">the characters used as the delimiter</param>
        /// <param name="max">max number of elements to include in the array.A zero or negative value implies no limit</param>
        /// <returns>an array of parsed Strings</returns>
        public static string[] Split(string str, string separatorChars, int max)
        {
            return SplitWorker(str, separatorChars, max, false);
        }

        /// <summary>
        /// Splits the provided text into an array, using the specified separator string.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.SplitByWholeSeparator(null, *)               = null
        /// <para/> StringUtils.SplitByWholeSeparator("", *)                 = []
        /// <para/> StringUtils.SplitByWholeSeparator("ab de fg", null)      = ["ab", "de", "fg"]
        /// <para/> StringUtils.SplitByWholeSeparator("ab   de fg", null)    = ["ab", "de", "fg"]
        /// <para/> StringUtils.SplitByWholeSeparator("ab:cd:ef", ":")       = ["ab", "cd", "ef"]
        /// <para/> StringUtils.SplitByWholeSeparator("ab-!-cd-!-ef", "-!-") = ["ab", "cd", "ef"]
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="separator">seperator string</param>
        /// <returns>an array of parsed strings</returns>
        public static string[] SplitByWholeSeparator(string str, string separator)
        {
            return SplitByWholeSeparatorWorker(str, separator, -1, false);
        }

        /// <summary>
        /// Splits the provided text into an array with max length, using the specified separator string.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.SplitByWholeSeparator(null, *)               = null
        /// <para/> StringUtils.SplitByWholeSeparator("", *)                 = []
        /// <para/> StringUtils.SplitByWholeSeparator("ab de fg", null)      = ["ab", "de", "fg"]
        /// <para/> StringUtils.SplitByWholeSeparator("ab   de fg", null)    = ["ab", "de", "fg"]
        /// <para/> StringUtils.SplitByWholeSeparator("ab:cd:ef", ":")       = ["ab", "cd", "ef"]
        /// <para/> StringUtils.SplitByWholeSeparator("ab-!-cd-!-ef", "-!-") = ["ab", "cd", "ef"]
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="separator">seperator string</param>
        /// <param name="max">max number of elements to include in the array.A zero or negative value implies no limit</param>
        /// <returns>an array of parsed strings</returns>
        public static string[] SplitByWholeSeparator(string str, string separator, int max)
        {
            return SplitByWholeSeparatorWorker(str, separator, max, false);
        }

        /// <summary>
        /// Splits the provided text into an array, using the specified separator string.Adjacent separators are treated as separators for empty tokens.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.SplitByWholeSeparatorPreserveAllTokens(null, *)               = null
        /// <para/> StringUtils.SplitByWholeSeparatorPreserveAllTokens("", *)                 = []
        /// <para/> StringUtils.SplitByWholeSeparatorPreserveAllTokens("ab de fg", null)      = ["ab", "de", "fg"]
        /// <para/> StringUtils.SplitByWholeSeparatorPreserveAllTokens("ab   de fg", null)    = ["ab", "", "", "de", "fg"]
        /// <para/> StringUtils.SplitByWholeSeparatorPreserveAllTokens("ab:cd:ef", ":")       = ["ab", "cd", "ef"]
        /// <para/> StringUtils.SplitByWholeSeparatorPreserveAllTokens("ab-!-cd-!-ef", "-!-") = ["ab", "cd", "ef"]
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="separator">seperator string</param>
        /// <returns>an array of parsed strings</returns>
        public static string[] SplitByWholeSeparatorPreserveAllTokens(string str, string separator)
        {
            return SplitByWholeSeparatorWorker(str, separator, -1, true);
        }

        /// <summary>
        /// Splits the provided text into an array with max length, using the specified separator string.Adjacent separators are treated as separators for empty tokens.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.SplitByWholeSeparatorPreserveAllTokens(null, *, *)               = null
        /// <para/> StringUtils.SplitByWholeSeparatorPreserveAllTokens("", *, *)                 = []
        /// <para/> StringUtils.SplitByWholeSeparatorPreserveAllTokens("ab de fg", null, 0)      = ["ab", "de", "fg"]
        /// <para/> StringUtils.SplitByWholeSeparatorPreserveAllTokens("ab   de fg", null, 0)    = ["ab", "", "", "de", "fg"]
        /// <para/> StringUtils.SplitByWholeSeparatorPreserveAllTokens("ab:cd:ef", ":", 2)       = ["ab", "cd:ef"]
        /// <para/> StringUtils.SplitByWholeSeparatorPreserveAllTokens("ab-!-cd-!-ef", "-!-", 5) = ["ab", "cd", "ef"]
        /// <para/> StringUtils.SplitByWholeSeparatorPreserveAllTokens("ab-!-cd-!-ef", "-!-", 2) = ["ab", "cd-!-ef"]
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="separator">seperator string</param>
        /// <param name="max">max number of elements to include in the array.A zero or negative value implies no limit</param>k
        /// <returns>an array of parsed strings</returns>
        public static string[] SplitByWholeSeparatorPreserveAllTokens(string str, string separator, int max)
        {
            return SplitByWholeSeparatorWorker(str, separator, max, true);
        }

        /// <summary>
        /// Splits the given string into an array, using whitespace as the separator, preserving all tokens, including empty tokens created by adjacent separators.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.SplitPreserveAllTokens(null)       = null
        /// <para/> StringUtils.SplitPreserveAllTokens("")         = []
        /// <para/> StringUtils.SplitPreserveAllTokens("abc def")  = ["abc", "def"]
        /// <para/> StringUtils.SplitPreserveAllTokens("abc  def") = ["abc", "", "def"]
        /// <para/> StringUtils.SplitPreserveAllTokens(" abc ")    = ["", "abc", ""]
        /// </para>
        /// <param name="str">input string</param>
        /// <returns>an array of parsed strings</returns>
        public static string[] SplitPreserveAllTokens(string str)
        {
            return SplitWorker(str, null, -1, true);
        }

        /// <summary>
        /// Splits the provided text into an array, using the specified separator, preserving all tokens, including empty tokens created by adjacent separators.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.SplitPreserveAllTokens(null, *)         = null
        /// <para/> StringUtils.SplitPreserveAllTokens("", *)           = []
        /// <para/> StringUtils.SplitPreserveAllTokens("a.b.c", '.')    = ["a", "b", "c"]
        /// <para/> StringUtils.SplitPreserveAllTokens("a..b.c", '.')   = ["a", "", "b", "c"]
        /// <para/> StringUtils.SplitPreserveAllTokens("a:b:c", '.')    = ["a:b:c"]
        /// <para/> StringUtils.SplitPreserveAllTokens("a b c", ' ')    = ["a", "b", "c"]
        /// <para/> StringUtils.SplitPreserveAllTokens("a b c ", ' ')   = ["a", "b", "c", ""]
        /// <para/> StringUtils.SplitPreserveAllTokens("a b c  ", ' ')   = ["a", "b", "c", "", ""]
        /// <para/> StringUtils.SplitPreserveAllTokens(" a b c", ' ')   = ["", "a", "b", "c"]
        /// <para/> StringUtils.SplitPreserveAllTokens("  a b c", ' ')  = ["", "", a", "b", "c"]
        /// <para/> StringUtils.SplitPreserveAllTokens(" a b c ", ' ')  = ["", "a", "b", "c", ""]
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="separatorChar">the character used as the delimiter</param>
        /// <returns>an array of parsed strings</returns>
        public static string[] SplitPreserveAllTokens(string str, char separatorChar)
        {
            return SplitWorker(str, separatorChar, true);
        }

        /// <summary>
        /// Splits the provided text into an array, using the specified separator, preserving all tokens, including empty tokens created by adjacent separators.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.SplitPreserveAllTokens(null, *)           = null
        /// <para/> StringUtils.SplitPreserveAllTokens("", *)             = []
        /// <para/> StringUtils.SplitPreserveAllTokens("abc def", null)   = ["abc", "def"]
        /// <para/> StringUtils.SplitPreserveAllTokens("abc def", " ")    = ["abc", "def"]
        /// <para/> StringUtils.SplitPreserveAllTokens("abc  def", " ")   = ["abc", "", "def"]
        /// <para/> StringUtils.SplitPreserveAllTokens("ab:cd:ef", ":")   = ["ab", "cd", "ef"]
        /// <para/> StringUtils.SplitPreserveAllTokens("ab:cd:ef:", ":")  = ["ab", "cd", "ef", ""]
        /// <para/> StringUtils.SplitPreserveAllTokens("ab:cd:ef::", ":") = ["ab", "cd", "ef", "", ""]
        /// <para/> StringUtils.SplitPreserveAllTokens("ab::cd:ef", ":")  = ["ab", "", "cd", "ef"]
        /// <para/> StringUtils.SplitPreserveAllTokens(":cd:ef", ":")     = ["", "cd", "ef"]
        /// <para/> StringUtils.SplitPreserveAllTokens("::cd:ef", ":")    = ["", "", "cd", "ef"]
        /// <para/> StringUtils.SplitPreserveAllTokens(":cd:ef:", ":")    = ["", "cd", "ef", ""]
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="separatorChars">the characters used as the delimiters</param>
        /// <returns>an array of parsed strings</returns>
        public static string[] SplitPreserveAllTokens(string str, string separatorChars)
        {
            return SplitWorker(str, separatorChars, -1, true);
        }

        /// <summary>
        /// Splits the provided text into an array with max length, using the specified separator, preserving all tokens, including empty tokens created by adjacent separators.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.SplitPreserveAllTokens(null, *, *)            = null
        /// <para/> StringUtils.SplitPreserveAllTokens("", *, *)              = []
        /// <para/> StringUtils.SplitPreserveAllTokens("ab de fg", null, 0)   = ["ab", "de", "fg"]
        /// <para/> StringUtils.SplitPreserveAllTokens("ab  de fg", null, 0) = ["ab","", "de", "fg"]
        /// <para/> StringUtils.SplitPreserveAllTokens("ab:cd:ef", ":", 0)    = ["ab", "cd", "ef"]
        /// <para/> StringUtils.SplitPreserveAllTokens("ab:cd:ef", ":", 2)    = ["ab", "cd:ef"]
        /// <para/> StringUtils.SplitPreserveAllTokens("ab   de fg", null, 2) = ["ab", "  de fg"]
        /// <para/> StringUtils.SplitPreserveAllTokens("ab   de fg", null, 3) = ["ab", "", " de fg"]
        /// <para/> StringUtils.SplitPreserveAllTokens("ab   de fg", null, 4) = ["ab", "", "", "de fg"]
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="separatorChars">the characters used as the delimiters</param>
        /// <param name="max">max number of elements to include in the array.A zero or negative value implies no limit</param>k
        /// <returns>an array of parsed strings</returns>
        public static string[] SplitPreserveAllTokens(string str, string separatorChars, int max)
        {
            return SplitWorker(str, separatorChars, max, true);
        }

        /// <summary>
        /// Splits a String by Character type
        /// </summary>
        /// <para>
        /// <para/> StringUtils.SplitByCharacterType(null)         = null
        /// <para/> StringUtils.SplitByCharacterType("")           = []
        /// <para/> StringUtils.SplitByCharacterType("ab de fg")   = ["ab", " ", "de", " ", "fg"]
        /// <para/> StringUtils.SplitByCharacterType("ab   de fg") = ["ab", "   ", "de", " ", "fg"]
        /// <para/> StringUtils.SplitByCharacterType("ab:cd:ef")   = ["ab", ":", "cd", ":", "ef"]
        /// <para/> StringUtils.SplitByCharacterType("number5")    = ["number", "5"]
        /// <para/> StringUtils.SplitByCharacterType("fooBar")     = ["foo", "B", "ar"]
        /// <para/> StringUtils.SplitByCharacterType("foo200Bar")  = ["foo", "200", "B", "ar"]
        /// <para/> StringUtils.SplitByCharacterType("ASFRules")   = ["ASFR", "ules"]
        /// </para>
        /// <param name="str">input string</param>
        /// <returns>an array of parsed strings</returns>
        public static string[] SplitByCharacterType(string str)
        {
            return SplitByCharacterType(str, false);
        }

        /// <summary>
        /// Splits a String by Character type.the character of CamelCase type , if any, immediately preceding a token of type LowerCase will belong to the following token rather than to the preceding.See the examples.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.SplitByCharacterTypeCamelCase(null)         = null
        /// <para/> StringUtils.SplitByCharacterTypeCamelCase("")           = []
        /// <para/> StringUtils.SplitByCharacterTypeCamelCase("ab de fg")   = ["ab", " ", "de", " ", "fg"]
        /// <para/> StringUtils.SplitByCharacterTypeCamelCase("ab   de fg") = ["ab", "   ", "de", " ", "fg"]
        /// <para/> StringUtils.SplitByCharacterTypeCamelCase("ab:cd:ef")   = ["ab", ":", "cd", ":", "ef"]
        /// <para/> StringUtils.SplitByCharacterTypeCamelCase("number5")    = ["number", "5"]
        /// <para/> StringUtils.SplitByCharacterTypeCamelCase("fooBar")     = ["foo", "Bar"]
        /// <para/> StringUtils.SplitByCharacterTypeCamelCase("foo200Bar")  = ["foo", "200", "Bar"]
        /// <para/> StringUtils.SplitByCharacterTypeCamelCase("ASFRules")   = ["ASF", "Rules"] 
        /// </para>
        /// <param name="str">input string</param>
        /// <returns>an array of parsed strings</returns>
        public static string[] SplitByCharacterTypeCamelCase(string str)
        {
            return SplitByCharacterType(str, true);
        }

        #endregion

        #region Join

        /// <summary>
        /// Joins the elements of the given array into a single String containing the given list of elements.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Join(null)            = null
        /// <para/> StringUtils.Join([])              = ""
        /// <para/> StringUtils.Join([null])          = ""
        /// <para/> StringUtils.Join(["a", "b", "c"]) = "abc"
        /// <para/> StringUtils.Join([null, "", "a"]) = "a" 
        /// </para>
        /// <param name="elements">input array of elements</param>
        /// <returns>the joined String</returns>
        public static string Join(params string[] elements)
        {
            return Join(elements, null, 0, elements.GetLength());
        }

        /// <summary>
        /// Joins the elements of the given array into a single String containing the given list of elements using the given seperator.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Join(null, *)               = null
        /// <para/> StringUtils.Join([], *)                 = ""
        /// <para/> StringUtils.Join([null], *)             = ""
        /// <para/> StringUtils.Join(["a", "b", "c"], ";")  = "a;b;c"
        /// <para/> StringUtils.Join(["a", "b", "c"], null) = "abc"
        /// <para/> StringUtils.Join([null, "", "a"], ";")  = "a"
        /// </para>
        /// <param name="array">input array of elements</param>
        /// <param name="separator">seperator character</param>
        /// <returns>the joined String</returns>
        public static string Join(object[] array, string separator = null)
        {
            return Join(array, separator, 0, array.GetLength());
        }

        /// <summary>
        /// Joins the elements of the given array into a single String containing the given list of elements using the given seperator if any.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Join(null, *)               = null
        /// <para/> StringUtils.Join([], *)                 = ""
        /// <para/> StringUtils.Join([null], *)             = ""
        /// <para/> StringUtils.Join([1, 2, 3], ";")        = "1;2;3"
        /// <para/> StringUtils.Join([1, 2, 3], null)       = "123"
        /// </para>
        /// <param name="array">input array of elements</param>
        /// <param name="separator">seperator character(default null)</param>
        /// <returns>the joined String</returns>
        public static string Join(long[] array, string separator = null)
        {
            return Join(array, separator, 0, array != null ? array.Length : -1);
        }

        /// <summary>
        /// Joins the elements of the given array into a single String containing the given list of elements using the given seperator if any.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Join(null, *)               = null
        /// <para/> StringUtils.Join([], *)                 = ""
        /// <para/> StringUtils.Join([1, 2, 3], ";")        = "1;2;3"
        /// <para/> StringUtils.Join([1, 2, 3], null)       = "123"
        /// </para>
        /// <param name="array">input array of elements</param>
        /// <param name="separator">seperator character(default null)</param>
        /// <returns>the joined String</returns>
        public static string Join(int[] array, string separator = null)
        {
            return Join(array, separator, 0, array != null ? array.Length : -1);
        }

        /// <summary>
        /// Joins the elements of the given array into a single String containing the given list of elements using the given seperator if any.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Join(null, *)               = null
        /// <para/> StringUtils.Join([], *)                 = ""
        /// <para/> StringUtils.Join([1, 2, 3], ";")        = "1;2;3"
        /// <para/> StringUtils.Join([1, 2, 3], null)       = "123"
        /// </para>
        /// <param name="array">input array of elements</param>
        /// <param name="separator">seperator character(default null)</param>
        /// <returns>the joined String</returns>
        public static string Join(double[] array, string separator = null)
        {
            return Join(array, separator, 0, array != null ? array.Length : -1);
        }

        /// <summary>
        /// Joins the elements of the given array into a single String containing the given list of elements using the given seperator if any.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Join(null, *)               = null
        /// <para/> StringUtils.Join([], *)                 = ""
        /// <para/> StringUtils.Join([1, 2, 3], ";")        = "1;2;3"
        /// <para/> StringUtils.Join([1, 2, 3], null)       = "123"
        /// </para>
        /// <param name="array">input array of elements</param>
        /// <param name="separator">seperator character(default null)</param>
        /// <returns>the joined String</returns>
        public static string Join(short[] array, string separator = null)
        {
            return Join(array, separator, 0, array != null ? array.Length : -1);
        }

        /// <summary>
        /// Joins the elements of the given array into a single String containing the given list of elements using the given seperator if any.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Join(null, *)               = null
        /// <para/> StringUtils.Join([], *)                 = ""
        /// <para/> StringUtils.Join([1, 2, 3], ";")        = "1;2;3"
        /// <para/> StringUtils.Join([1, 2, 3], null)       = "123"
        /// </para>
        /// <param name="array">input array of elements</param>
        /// <param name="separator">seperator character(default null)</param>
        /// <returns>the joined String</returns>
        public static string Join(float[] array, string separator = null)
        {
            return Join(array, separator, 0, array != null ? array.Length : -1);
        }

        /// <summary>
        /// Joins the elements of the given array into a single String containing the given list of elements using the given seperator if any.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Join(null, "test")                = null
        /// <para/> StringUtils.Join(new byte[] { }, "test")       = ""
        /// <para/> StringUtils.Join(new byte[] { 1, 2, 3 }, ";")  = "1;2;3"
        /// <para/> StringUtils.Join(new byte[] { 1, 2, 3 }, null) = "123" 
        /// </para>
        /// <param name="array">input array of elements</param>
        /// <param name="separator">seperator character(default null)</param>
        /// <returns>the joined String</returns>
        public static string Join(byte[] array, string separator = null)
        {
            return Join(array, separator, 0, array != null ? array.Length : -1);
        }

        /// <summary>
        /// Joins the elements of the given array into a single String containing the given list of elements using the given seperator if any.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Join(null, *)               = null
        /// <para/> StringUtils.Join([], *)                 = ""
        /// <para/> StringUtils.Join([null], *)             = ""
        /// <para/> StringUtils.Join(['a', 'b', 'c'], ";")        = "a;b;c"
        /// <para/> StringUtils.Join(['a', 'b', 'c'], null)       = "abc"
        /// </para>
        /// <param name="array">input array of elements</param>
        /// <param name="separator">seperator character(default null)</param>
        /// <returns>the joined String</returns>
        public static string Join(char[] array, string separator = null)
        {
            return Join(array, separator, 0, array?.Length ?? -1);
        }

        /// <summary>
        /// Joins the elements of the given array into a single String containing the given list of elements using the given seperator if any, and using the start index and no.of items.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Join(null, *,*,*)               = null
        /// <para/> StringUtils.Join([], *,*,*)                 = ""
        /// <para/> StringUtils.Join([null], *,*,*)             = ""
        /// <para/> StringUtils.Join(["a", "b", "c"], ";",0,2)        = "a;b"
        /// <para/> StringUtils.Join(["a", "b", "c"], null,0,2)       = "ab"
        /// </para>
        /// <param name="array">input array of elements</param>
        /// <param name="separator">seperator character</param>
        /// <param name="startIndex">the first index to start joining from.</param>
        /// <param name="noOfItems">no.of items to join</param>
        /// <returns>the joined String</returns>
        public static string Join(object[] array, string separator, int startIndex, int noOfItems)
        {
            if (array==null)
            {
                return null;
            }

            if (!array.Any() || noOfItems <= 0)
            {
                return Empty;
            }
            var endIndex = startIndex + noOfItems;
            var delimmiter = IsNotEmpty(separator) ? separator : Empty;
            var buf = new StringBuilder();
            for (var i = startIndex; i < endIndex; i++)
            {

                if (IsEmpty(Convert.ToString(array[i])))
                    continue;
                if (IsNotEmpty(buf.ToString()))
                {
                    buf.Append(delimmiter);
                }

                if (array[i] != null)
                {
                    buf.Append(array[i]);
                }
            }
            return buf.ToString();
        }

        /// <summary>
        /// Joins the elements of the given array into a single String containing the given list of elements using the given seperator if any, and using the start index and no.of items.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Join(null, *,*,*)                     = null
        /// <para/> StringUtils.Join([], *,*,*)                       = ""
        /// <para/> StringUtils.Join(["1", "2", "3"], ";",0,2)        = "1;2"
        /// <para/> StringUtils.Join(["1", "2", "3"], null,0,2)       = "12"
        /// </para>
        /// <param name="array">input array of elements</param>
        /// <param name="separator">seperator character</param>
        /// <param name="startIndex">the first index to start joining from.</param>
        /// <param name="noOfItems">no.of items to join</param>
        /// <returns>the joined String</returns>
        public static string Join(long[] array, string separator, int startIndex, int noOfItems)
        {
            if (array == null)
            {
                return null;
            }

            if (!array.Any() || noOfItems <= 0)
            {
                return Empty;
            }
            var endIndex = startIndex + noOfItems;
            var delimmiter = IsNotEmpty(separator) ? separator : Empty;
            var buf = new StringBuilder();
            for (var i = startIndex; i < endIndex; i++)
            {

                if (IsNotEmpty(buf.ToString()))
                {
                    buf.Append(delimmiter);
                }

                buf.Append(array[i]);
            }
            return buf.ToString();
        }

        /// <summary>
        /// Joins the elements of the given array into a single String containing the given list of elements using the given seperator if any, and using the start index and no.of items.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Join(null, *,*,*)                     = null
        /// <para/> StringUtils.Join([], *,*,*)                       = ""
        /// <para/> StringUtils.Join(["1", "2", "3"], ";",0,2)        = "1;2"
        /// <para/> StringUtils.Join(["1", "2", "3"], null,0,2)       = "12"
        /// </para>
        /// <param name="array">input array of elements</param>
        /// <param name="separator">seperator character</param>
        /// <param name="startIndex">the first index to start joining from.</param>
        /// <param name="noOfItems">no.of items to join</param>
        /// <returns>the joined String</returns>
        public static string Join(int[] array, string separator, int startIndex, int noOfItems)
        {
            if (array == null)
            {
                return null;
            }

            if (!array.Any() || noOfItems <= 0)
            {
                return Empty;
            }
            var endIndex = startIndex + noOfItems;
            var delimmiter = IsNotEmpty(separator) ? separator : Empty;
            var buf = new StringBuilder();
            for (var i = startIndex; i < endIndex; i++)
            {

                if (IsNotEmpty(buf.ToString()))
                {
                    buf.Append(delimmiter);
                }

                buf.Append(array[i]);
            }
            return buf.ToString();
        }

        /// <summary>
        /// Joins the elements of the given array into a single String containing the given list of elements using the given seperator if any, and using the start index and no.of items.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Join(null, *,*,*)                     = null
        /// <para/> StringUtils.Join([], *,*,*)                       = ""
        /// <para/> StringUtils.Join(["1", "2", "3"], ";",0,2)        = "1;2"
        /// <para/> StringUtils.Join(["1", "2", "3"], null,0,2)       = "12"
        /// </para>
        /// <param name="array">input array of elements</param>
        /// <param name="separator">seperator character</param>
        /// <param name="startIndex">the first index to start joining from.</param>
        /// <param name="noOfItems">no.of items to join</param>
        /// <returns>the joined String</returns>
        public static string Join(short[] array, string separator, int startIndex, int noOfItems)
        {
            if (array == null)
            {
                return null;
            }

            if (!array.Any() || noOfItems <= 0)
            {
                return Empty;
            }
            var endIndex = startIndex + noOfItems;
            var delimmiter = IsNotEmpty(separator) ? separator : Empty;
            var buf = new StringBuilder();
            for (var i = startIndex; i < endIndex; i++)
            {

                if (IsNotEmpty(buf.ToString()))
                {
                    buf.Append(delimmiter);
                }

                buf.Append(array[i]);
            }
            return buf.ToString();
        }

        /// <summary>
        /// Joins the elements of the given array into a single String containing the given list of elements using the given seperator if any, and using the start index and no.of items.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Join(null, *,*,*)                     = null
        /// <para/> StringUtils.Join([], *,*,*)                       = ""
        /// <para/> StringUtils.Join(["1", "2", "3"], ";",0,2)        = "1;2"
        /// <para/> StringUtils.Join(["1", "2", "3"], null,0,2)       = "12"
        /// </para>
        /// <param name="array">input array of elements</param>
        /// <param name="separator">seperator character</param>
        /// <param name="startIndex">the first index to start joining from.</param>
        /// <param name="noOfItems">no.of items to join</param>
        /// <returns>the joined String</returns>
        public static string Join(double[] array, string separator, int startIndex, int noOfItems)
        {
            if (array == null)
            {
                return null;
            }

            if (!array.Any() || noOfItems <= 0)
            {
                return Empty;
            }
            var endIndex = startIndex + noOfItems;
            var delimmiter = IsNotEmpty(separator) ? separator : Empty;
            var buf = new StringBuilder();
            for (var i = startIndex; i < endIndex; i++)
            {

                if (IsNotEmpty(buf.ToString()))
                {
                    buf.Append(delimmiter);
                }

                buf.Append(array[i]);
            }
            return buf.ToString();
        }

        /// <summary>
        /// Joins the elements of the given array into a single String containing the given list of elements using the given seperator if any, and using the start index and no.of items.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Join(null, *,*,*)                     = null
        /// <para/> StringUtils.Join([], *,*,*)                       = ""
        /// <para/> StringUtils.Join(["1", "2", "3"], ";",0,2)        = "1;2"
        /// <para/> StringUtils.Join(["1", "2", "3"], null,0,2)       = "12"
        /// </para>
        /// <param name="array">input array of elements</param>
        /// <param name="separator">seperator character</param>
        /// <param name="startIndex">the first index to start joining from.</param>
        /// <param name="noOfItems">no.of items to join</param>
        /// <returns>the joined String</returns>
        public static string Join(float[] array, string separator, int startIndex, int noOfItems)
        {
            if (array == null)
            {
                return null;
            }

            if (!array.Any() || noOfItems <= 0)
            {
                return Empty;
            }
            var endIndex = startIndex + noOfItems;
            var delimmiter = IsNotEmpty(separator) ? separator : Empty;
            var buf = new StringBuilder();
            for (var i = startIndex; i < endIndex; i++)
            {

                if (IsNotEmpty(buf.ToString()))
                {
                    buf.Append(delimmiter);
                }

                buf.Append(array[i]);
            }
            return buf.ToString();
        }

        /// <summary>
        /// Joins the elements of the given array into a single String containing the given list of elements using the given seperator if any, and using the start index and no.of items.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Join(null, ";", 1, 2)                   = null
        /// <para/> StringUtils.Join(new byte[] { }, ";", 1, 2)         = ""
        /// <para/> StringUtils.Join(new byte[] { 1, 2, 3 }, ";", 0, 2) = "1;2"
        /// <para/> StringUtils.Join(new byte[] { 1, 2, 3 }, null, 0, 2)= "12"
        /// </para>
        /// <param name="array">input array of elements</param>
        /// <param name="separator">seperator character</param>
        /// <param name="startIndex">the first index to start joining from.</param>
        /// <param name="noOfItems">no.of items to join</param>
        /// <returns>the joined String</returns>
        public static string Join(byte[] array, string separator, int startIndex, int noOfItems)
        {
            if (array == null)
            {
                return null;
            }

            if (!array.Any() || noOfItems <= 0)
            {
                return Empty;
            }
            var endIndex = startIndex + noOfItems;
            var delimmiter = IsNotEmpty(separator) ? separator : Empty;
            var buf = new StringBuilder();
            for (var i = startIndex; i < endIndex; i++)
            {

                if (IsNotEmpty(buf.ToString()))
                {
                    buf.Append(delimmiter);
                }

                buf.Append(array[i]);
            }
            return buf.ToString();
        }

        /// <summary>
        /// Joins the elements of the given array into a single String containing the given list of elements using the given seperator if any, and using the start index and no.of items.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Join(null       , *,*,*)               = null
        /// <para/> StringUtils.Join([], *,*,*)                        = ""
        /// <para/> StringUtils.Join(['a', 'b', 'c'], ";",0,2)         = "a;b"
        /// <para/> StringUtils.Join(['a', 'b', 'c'], null,0,2)        = "ab"
        /// </para>
        /// <param name="array">input array of elements</param>
        /// <param name="separator">seperator character</param>
        /// <param name="startIndex">the first index to start joining from.</param>
        /// <param name="noOfItems">no.of items to join</param>
        /// <returns>the joined String</returns>
        public static string Join(char[] array, string separator, int startIndex, int noOfItems)
        {
            if (array == null)
            {
                return null;
            }

            if (!array.Any() || noOfItems <= 0)
            {
                return Empty;
            }
            var endIndex = startIndex + noOfItems;
            var delimmiter = IsNotEmpty(separator) ? separator : Empty;
            var buf = new StringBuilder();
            for (var i = startIndex; i < endIndex; i++)
            {

                if (IsNotEmpty(buf.ToString()))
                {
                    buf.Append(delimmiter);
                }

                buf.Append(array[i]);
            }
            return buf.ToString();
        }

        /// <summary>
        /// Joins the elements of the given args into a single String containing the provided elements.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.JoinWith({"a", "b"})        = "ab"
        /// <para/> StringUtils.JoinWith({"a", "1",""})     = "a1"
        /// <para/> StringUtils.JoinWith({"a", null, "b"})  = "ab"
        /// <para/> StringUtils.JoinWith( {"a", "1"})       = "a1"
        /// </para>
        /// <param name="objects">the args having the values to join together. </param>
        /// <returns>the joined String</returns>
        public static string JoinWith(params object[] objects)
        {
            return Join(objects, null, 0, objects.Length);
        }

        /// <summary>
        /// Joins the elements of the given args into a single String containing the provided elements using the given separator.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.JoinWith(",", {"a", "b"})        = "a,b"
        /// <para/> StringUtils.JoinWith(",", {"a", "1",""})     = "a,1"
        /// <para/> StringUtils.JoinWith(",", {"a", null, "b"})  = "a,b"
        /// <para/> StringUtils.JoinWith(null, {"a", "1"})       = "a1"
        /// </para>
        /// <param name="separator">separator character</param>
        /// <param name="objects">the args having the values to join together. </param>
        /// <returns>the joined String</returns>
        public static string JoinWith(string separator, params object[] objects)
        {
            return Join(objects, separator, 0, objects.Length);
        }

        #endregion

        #region Remove

        /// <summary>
        /// Deletes all whitespaces from a String
        /// </summary>
        /// <para>
        /// <para/> StringUtils.DeleteWhitespace(null)         = null
        /// <para/> StringUtils.DeleteWhitespace("")           = ""
        /// <para/> StringUtils.DeleteWhitespace("abc")        = "abc"
        /// <para/> StringUtils.DeleteWhitespace("   ab  c  ") = "abc" 
        /// </para>
        /// <param name="str">input string</param>
        /// <returns>the string without whitespaces</returns>
        public static string DeleteWhitespace(string str)
        {
            return IsEmpty(str) ? str : str.Replace(" ", "");
        }

        /// <summary>
        /// Removes a substring only if it is at the beginning of the input string, otherwise returns the original string.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.RemoveStart(null, *)      = null
        /// <para/> StringUtils.RemoveStart("", *)        = ""
        /// <para/> StringUtils.RemoveStart(*, null)      = *
        /// <para/> StringUtils.RemoveStart("www.xyz.com", "www.")   = "xyz.com"
        /// <para/> StringUtils.RemoveStart("xyz.com", "www.")       = "xyz.com"
        /// <para/> StringUtils.RemoveStart("www.xyz.com", "xyz") = "www.xyz.com"
        /// <para/> StringUtils.RemoveStart("abc", "")    = "abc"
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="remove">substring to remove</param>
        /// <returns>the substring with the string removed if found</returns>
        public static string RemoveStart(string str, string remove)
        {
            if (IsEmpty(str) || IsEmpty(remove))
                return str;

            return str.StartsWith(remove) ? str.Substring(remove.Length) : str;
        }

        /// <summary>
        /// By ignoring case.,Removes a substring only if it is at the beginning of the input string , otherwise returns the original string.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.RemoveStartIgnoreCase(null, *)      = null
        /// <para/> StringUtils.RemoveStartIgnoreCase("", *)        = ""
        /// <para/> StringUtils.RemoveStartIgnoreCase(*, null)      = *
        /// <para/> StringUtils.RemoveStartIgnoreCase("www.xyz.com", "www.")   = "xyz.com"
        /// <para/> StringUtils.RemoveStartIgnoreCase("www.xyz.com", "WWW.")   = "xyz.com"
        /// <para/> StringUtils.RemoveStartIgnoreCase("xyz.com", "www.")       = "xyz.com"
        /// <para/> StringUtils.RemoveStartIgnoreCase("www.xyz.com", "xyz") = "www.xyz.com"
        /// <para/> StringUtils.RemoveStartIgnoreCase("abc", "")    = "abc"
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="remove">substring to remove</param>
        /// <returns>the substring with the string removed if found</returns>
        public static string RemoveStartIgnoreCase(string str, string remove)
        {
            if (IsEmpty(str) || IsEmpty(remove))
                return str;

            return str.StartsWith(remove, StringComparison.CurrentCultureIgnoreCase) ? str.Substring(remove.Length) : str;
        }

        /// <summary>
        /// Removes a substring only if it is at the end of the input string, otherwise returns the original string.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.RemoveEnd(null, *)      = null
        /// <para/> StringUtils.RemoveEnd("", *)        = ""
        /// <para/> StringUtils.RemoveEnd(*, null)      = *
        /// <para/> StringUtils.RemoveEnd("www.xyz.com", ".com.")  = "www.xyz.com"
        /// <para/> StringUtils.RemoveEnd("www.xyz.com", ".com")   = "www.xyz"
        /// <para/> StringUtils.RemoveEnd("www.xyz.com", "xyz") = "www.xyz.com"
        /// <para/> StringUtils.RemoveEnd("abc", "")    = "abc"
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="remove">substring to remove</param>
        /// <returns>the substring with the string removed if found</returns>
        public static string RemoveEnd(string str, string remove)
        {
            if (IsEmpty(str) || IsEmpty(remove))
                return str;
            return str.EndsWith(remove) ? str.Substring(0, str.Length - remove.Length) : str;
        }

        /// <summary>
        /// By ignoring case.,Removes a substring only if it is at the end of the input string , otherwise returns the original string.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.RemoveEndIgnoreCase(null, *)      = null
        /// <para/> StringUtils.RemoveEndIgnoreCase("", *)        = ""
        /// <para/> StringUtils.RemoveEndIgnoreCase(*, null)      = *
        /// <para/> StringUtils.RemoveEndIgnoreCase("www.xyz.com", ".com.")  = "www.xyz.com"
        /// <para/> StringUtils.RemoveEndIgnoreCase("www.xyz.com", ".com")   = "www.xyz"
        /// <para/> StringUtils.RemoveEndIgnoreCase("www.xyz.com", "xyz") = "www.xyz.com"
        /// <para/> StringUtils.RemoveEndIgnoreCase("abc", "")    = "abc"
        /// <para/> StringUtils.RemoveEndIgnoreCase("www.xyz.com", ".COM") = "www.xyz"
        /// <para/> StringUtils.RemoveEndIgnoreCase("www.xyz.COM", ".com") = "www.xyz"
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="remove">substring to remove</param>
        /// <returns>the substring with the string removed if found</returns>
        public static string RemoveEndIgnoreCase(string str, string remove)
        {
            if (IsEmpty(str) || IsEmpty(remove))
                return str;
            return str.EndsWith(remove, StringComparison.CurrentCultureIgnoreCase) ? str.Substring(0, str.Length - remove.Length) : str;
        }

        /// <summary>
        /// Removes all occurences of the substring from within the input string.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Remove(null, *)        = null
        /// <para/> StringUtils.Remove("", *)          = ""
        /// <para/> StringUtils.Remove(*, null)        = *
        /// <para/> StringUtils.Remove(*, "")          = *
        /// <para/> StringUtils.Remove("queued", "ue") = "qd"
        /// <para/> StringUtils.Remove("queued", "zz") = "queued"
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="remove">substring to remove</param>
        /// <returns>the substring with the string removed if found</returns>
        public static string Remove(string str, string remove)
        {
            if (IsEmpty(str) || IsEmpty(remove))
                return str;
            return Replace(str, remove, Empty, -1);
        }

        /// <summary>
        /// Removes all occurences of a character from within the input string.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Remove(null, *)       = null
        /// <para/> StringUtils.Remove("", *)         = ""
        /// <para/> StringUtils.Remove("queued", 'u') = "qeed"
        /// <para/> StringUtils.Remove("queued", 'z') = "queued"
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="remove">substring to remove</param>
        /// <returns>the substring with the string removed if found</returns>
        public static string Remove(string str, char remove)
        {
            if (IsEmpty(str) || str.IndexOf(remove) == IndexNotFound)
            {
                return str;
            }
            return str.Replace(remove.ToString(), "");
        }

        /// <summary>
        /// Replaces a String with another String inside a larger String, once.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.ReplaceOnce(null, *, *)        = null
        /// <para/> StringUtils.ReplaceOnce("", *, *)          = ""
        /// <para/> StringUtils.ReplaceOnce("any", null, *)    = "any"
        /// <para/> StringUtils.ReplaceOnce("any", *, null)    = "any"
        /// <para/> StringUtils.ReplaceOnce("any", "", *)      = "any"
        /// <para/> StringUtils.ReplaceOnce("aba", "a", null)  = "aba"
        /// <para/> StringUtils.ReplaceOnce("aba", "a", "")    = "ba"
        /// <para/> StringUtils.ReplaceOnce("aba", "a", "z")   = "zba"
        /// </para>
        /// <param name="text">input string</param>
        /// <param name="searchString">string to find</param>
        /// <param name="replacement">string to replace</param>
        /// <returns>the text with replacements if any</returns>
        public static string ReplaceOnce(string text, string searchString, string replacement)
        {
            return Replace(text, searchString, replacement, 1);
        }

        /// <summary>
        /// Replaces All regular expression matches with another String inside a larger String.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.ReplacePattern(null, *, *)        = null
        /// <para/> StringUtils.ReplacePattern("", *, *)          = ""
        /// <para/> StringUtils.ReplacePattern("any", null, *)    = "any"
        /// <para/> StringUtils.ReplacePattern("any", *, null)    = "any"
        /// <para/> StringUtils.ReplacePattern("any", "", *)      = "any"
        /// <para/> StringUtils.ReplacePattern("something/satish049@gmail.com/xyz", @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z","NoEmail") = "something/NoEmail/xyz"
        /// </para>
        /// <param name="source">input string</param>
        /// <param name="regex">regular expression to match</param>
        /// <param name="replacement">string to replace</param>
        /// <param name="ignoreCase">true if the ignore case (default false)</param>
        /// <returns>the text with replacements if any</returns>
        public static string ReplacePattern(string source, string regex, string replacement, bool ignoreCase = false)
        {
            if (source == null) return null;
            if (IsEmpty(regex)) return source;
            return ignoreCase ? Regex.Replace(source, regex, replacement ?? Empty, RegexOptions.Multiline | RegexOptions.IgnoreCase) : Regex.Replace(source, regex, replacement ?? Empty, RegexOptions.Multiline);
        }

        /// <summary>
        /// Removes All regular expression matches inside a larger String.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.RemovePattern(null, *)        = null
        /// <para/> StringUtils.RemovePattern("", *)          = ""
        /// <para/> StringUtils.RemovePattern("any", null)    = "any"
        /// <para/> StringUtils.RemovePattern("any", "")      = "any"
        /// <para/> StringUtils.RemovePattern("something/satish049@gmail.com/xyz", @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z") = "something//xyz"
        /// </para>
        /// <param name="source">input string</param>
        /// <param name="regex">regular expression to match</param>
        /// <returns>the text with patterns removed if any</returns>
        public static string RemovePattern(string source, string regex)
        {
            return ReplacePattern(source, regex, Empty);
        }

        /// <summary>
        /// Replaces all occurrences of a String within another String.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Replace(null, *, *)        = null
        /// <para/> StringUtils.Replace("", *, *)          = ""
        /// <para/> StringUtils.Replace("any", null, *)    = "any"
        /// <para/> StringUtils.Replace("any", *, null)    = "any"
        /// <para/> StringUtils.Replace("any", "", *)      = "any"
        /// <para/> StringUtils.Replace("aba", "a", null)  = "aba"
        /// <para/> StringUtils.Replace("aba", "a", "")    = "b"
        /// <para/> StringUtils.Replace("aba", "a", "z")   = "zbz"
        /// </para>
        /// <param name="text">input text</param>
        /// <param name="searchString">string to find</param>
        /// <param name="replacement">string to replace</param>
        /// <returns>the text with replacements if any</returns>
        public static string Replace(string text, string searchString, string replacement)
        {
            return Replace(text, searchString, replacement, -1);
        }

        /// <summary>
        /// Replaces all occurrences of a String within another String.,for the first max values of the search string.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Replace(null, *, *, *)         = null
        /// <para/> StringUtils.Replace("", *, *, *)           = ""
        /// <para/> StringUtils.Replace("any", null, *, *)     = "any"
        /// <para/> StringUtils.Replace("any", *, null, *)     = "any"
        /// <para/> StringUtils.Replace("any", "", *, *)       = "any"
        /// <para/> StringUtils.Replace("any", *, *, 0)        = "any"
        /// <para/> StringUtils.Replace("abaa", "a", null, -1) = "abaa"
        /// <para/> StringUtils.Replace("abaa", "a", "", -1)   = "b"
        /// <para/> StringUtils.Replace("abaa", "a", "z", 0)   = "abaa"
        /// <para/> StringUtils.Replace("abaa", "a", "z", 1)   = "zbaa"
        /// <para/> StringUtils.Replace("abaa", "a", "z", 2)   = "zbza"
        /// <para/> StringUtils.Replace("abaa", "a", "z", -1)  = "zbzz"
        /// </para>
        /// <param name="text">input text</param>
        /// <param name="searchString">string to find</param>
        /// <param name="replacement">string to replace</param>
        /// <param name="max">maximum number of values to replace</param>
        /// <returns>the text with replacements if any</returns>
        public static string Replace(string text, string searchString, string replacement, int max)
        {
            if (IsEmpty(text) || IsEmpty(searchString) || replacement == null || max == 0)
            {
                return text;
            }
            var start = 0;
            var end = text.IndexOf(searchString, start, StringComparison.Ordinal);
            if (end == IndexNotFound)
            {
                return text;
            }
            var replLength = searchString.Length;
            var buf = new StringBuilder();
            while (end != IndexNotFound)
            {
                buf.Append(Substring(text,start, end-1)).Append(replacement);
                start = end + replLength;
                if (--max == 0)
                {
                    break;
                }
                end = text.IndexOf(searchString, start, StringComparison.Ordinal);
            }
            buf.Append(text.Substring(start));
            return buf.ToString();
        }

        /// <summary>
        /// Replaces all occurrences of Strings within another String.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.ReplaceEach(null, *, *)        = null
        /// <para/> StringUtils.ReplaceEach("", *, *)          = ""
        /// <para/> StringUtils.ReplaceEach("aba", null, null) = "aba"
        /// <para/> StringUtils.ReplaceEach("aba", new String[0], null) = "aba"
        /// <para/> StringUtils.ReplaceEach("aba", null, new String[0]) = "aba"
        /// <para/> StringUtils.ReplaceEach("aba", new String[]{"a"}, null)  = "aba"
        /// <para/> StringUtils.ReplaceEach("aba", new String[]{"a"}, new String[]{""})  = "b"
        /// <para/> StringUtils.ReplaceEach("aba", new String[]{null}, new String[]{"a"})  = "aba"
        /// <para/> StringUtils.ReplaceEach("abcde", new String[]{"ab", "d"}, new String[]{"w", "t"})  = "wcte"
        /// </para>
        /// <param name="text">text to search and replace in, no operation if null</param>
        /// <param name="searchList">the Strings to search for, no operation if null</param>
        /// <param name="replacementList">the Strings to replace them with, no operation if null</param>
        /// <returns>the text with replacements if any</returns>
        public static string ReplaceEach(string text, string[] searchList, string[] replacementList)
        {
            return ReplaceEach(text, searchList, replacementList, false, 0);
        }

        /// <summary>
        /// Replaces all occurrences of Strings within another String in a repeated way.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.ReplaceEachRepeatedly(null, *, *) = null
        /// <para/> StringUtils.ReplaceEachRepeatedly("", *, *) = ""
        /// <para/> StringUtils.ReplaceEachRepeatedly("aba", null, null) = "aba"
        /// <para/> StringUtils.ReplaceEachRepeatedly("aba", new String[0], null) = "aba"
        /// <para/> StringUtils.ReplaceEachRepeatedly("aba", null, new String[0]) = "aba"
        /// <para/> StringUtils.ReplaceEachRepeatedly("aba", new String[]{"a"}, null) = "aba"
        /// <para/> StringUtils.ReplaceEachRepeatedly("aba", new String[]{"a"}, new String[]{""}) = "b"
        /// <para/> StringUtils.ReplaceEachRepeatedly("aba", new String[]{null}, new String[]{"a"}) = "aba"
        /// <para/> StringUtils.ReplaceEachRepeatedly("abcde", new String[]{"ab", "d"}, new String[]{"w", "t"}) = "wcte"
        /// </para>
        /// <param name="text">text to search and replace in, no operation if null</param>
        /// <param name="searchList">the Strings to search for, no operation if null</param>
        /// <param name="replacementList">the Strings to replace them with, no operation if null</param>
        /// <returns>the text with replacements if any</returns>
        public static string ReplaceEachRepeatedly(string text, string[] searchList, string[] replacementList)
        {
            // timeToLive should be 0 if not used or nothing to replace, else it's
            // the length of the replace array
            var timeToLive = searchList == null ? 0 : searchList.Length;
            return ReplaceEach(text, searchList, replacementList, true, timeToLive);
        }

        /// <summary>
        /// Replaces all occurrences of a character within another String 
        /// </summary>
        /// <para>
        /// <para/> StringUtils.ReplaceChars(null, *, *)        = null
        /// <para/> StringUtils.ReplaceChars("", *, *)          = ""
        /// <para/> StringUtils.ReplaceChars("abcba", 'b', 'y') = "aycya"
        /// <para/> StringUtils.ReplaceChars("abcba", 'z', 'y') = "abcba"
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="searchChar">char to search for</param>
        /// <param name="replaceChar">char to replace with</param>
        /// <returns>text with replacements if any</returns>
        public static string ReplaceChars(string str, char searchChar, char replaceChar)
        {
            return str == null ? null : str.Replace(searchChar, replaceChar);
        }

        /// <summary>
        /// Replaces multiple characters of a string within another string
        /// </summary>
        /// <para>
        /// <para/> StringUtils.ReplaceChars(null, *, *)           = null
        /// <para/> StringUtils.ReplaceChars("", *, *)             = ""
        /// <para/> StringUtils.ReplaceChars("abc", null, *)       = "abc"
        /// <para/> StringUtils.ReplaceChars("abc", "", *)         = "abc"
        /// <para/> StringUtils.ReplaceChars("abc", "b", null)     = "ac"
        /// <para/> StringUtils.ReplaceChars("abc", "b", "")       = "ac"
        /// <para/> StringUtils.ReplaceChars("abcba", "bc", "yz")  = "ayzya"
        /// <para/> StringUtils.ReplaceChars("abcba", "bc", "y")   = "ayya"
        /// <para/> StringUtils.ReplaceChars("abcba", "bc", "yzx") = "ayzya"
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="searchChars">chars to search for</param>
        /// <param name="replaceChars">chars to replace with</param>
        /// <returns>text with replacements if any</returns>
        public static string ReplaceChars(string str, string searchChars, string replaceChars)
        {
            if (IsEmpty(str) || IsEmpty(searchChars))
            {
                return str;
            }
            if (replaceChars == null)
            {
                replaceChars = Empty;
            }
            var modified = false;
            var replaceCharsLength = replaceChars.Length;
            var strLength = str.Length;
            var buf = new StringBuilder();
            for (var i = 0; i < strLength; i++)
            {
                var ch = str[i];
                var index = searchChars.IndexOf(ch);
                if (index >= 0)
                {
                    modified = true;
                    if (index < replaceCharsLength)
                    {
                        buf.Append(replaceChars[index]);
                    }
                }
                else
                {
                    buf.Append(ch);
                }
            }
            return modified ? buf.ToString() : str;
        }

        /// <summary>
        /// Overlays part of a String with another String.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Overlay(null, *, *, *)            = null
        /// <para/> StringUtils.Overlay("", "abc", 0, 0)          = "abc"
        /// <para/> StringUtils.Overlay("abcdef", null, 2, 4)     = "abef"
        /// <para/> StringUtils.Overlay("abcdef", "", 2, 4)       = "abef"
        /// <para/> StringUtils.Overlay("abcdef", "", 4, 2)       = "abef"
        /// <para/> StringUtils.Overlay("abcdef", "zzzz", 2, 4)   = "abzzzzef"
        /// <para/> StringUtils.Overlay("abcdef", "zzzz", 4, 2)   = "abzzzzef"
        /// <para/> StringUtils.Overlay("abcdef", "zzzz", -1, 4)  = "zzzzef"
        /// <para/> StringUtils.Overlay("abcdef", "zzzz", 2, 8)   = "abzzzz"
        /// <para/> StringUtils.Overlay("abcdef", "zzzz", -2, -3) = "zzzzabcdef"
        /// <para/> StringUtils.Overlay("abcdef", "zzzz", 8, 10)  = "abcdefzzzz"
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="overlay">string to overlay</param>
        /// <param name="start">the position to start overlaying at</param>
        /// <param name="end">the position to stop overlaying before</param>
        /// <returns>overlayed String</returns>
        public static string Overlay(string str, string overlay, int start, int end)
        {
            if (str == null)
            {
                return null;
            }
            if (overlay == null)
            {
                overlay = Empty;
            }
            var len = str.Length;
            if (start < 0)
            {
                start = 0;
            }
            if (start > len)
            {
                start = len;
            }
            if (end < 0)
            {
                end = 0;
            }
            if (end > len)
            {
                end = len;
            }
            if (start > end)
            {
                var temp = start;
                start = end;
                end = temp;
            }
            return new StringBuilder(len + start - end + overlay.Length + 1)
                .Append(str.Substring(0, start))
                .Append(overlay)
                .Append(str.Substring(end))
                .ToString();
        }

        /// <summary>
        /// Removes one newline from end of a String if it's there,otherwise string is unchanged. 
        /// </summary>
        /// <para>
        /// A new line is \n,\r or \n\r
        /// <para/> StringUtils.Chomp(null)          = null
        /// <para/> StringUtils.Chomp("")            = ""
        /// <para/> StringUtils.Chomp("abc \r")      = "abc "
        /// <para/> StringUtils.Chomp("abc\n")       = "abc"
        /// <para/> StringUtils.Chomp("abc\r\n")     = "abc"
        /// <para/> StringUtils.Chomp("abc\r\n\r\n") = "abc\r\n"
        /// <para/> StringUtils.Chomp("abc\n\r")     = "abc\n"
        /// <para/> StringUtils.Chomp("abc\n\rabc")  = "abc\n\rabc"
        /// <para/> StringUtils.Chomp("\r")          = ""
        /// <para/> StringUtils.Chomp("\n")          = ""
        /// <para/> StringUtils.Chomp("\r\n")        = ""
        /// </para>
        /// <param name="str">input string</param>
        /// <returns>string without new line</returns>
        public static string Chomp(string str)
        {
            if (IsEmpty(str))
            {
                return str;
            }

            if (str.Length == 1)
            {
                var ch = str[0];
                if (ch == Cr || ch == Lf || ch.ToString() == Environment.NewLine)
                {
                    return Empty;
                }
                return str;
            }

            var lastIdx = str.Length - 1;
            var last = str[lastIdx];

            if (last == Lf)
            {
                if (str[lastIdx - 1] == Cr)
                {
                    lastIdx--;
                }
            }
            else if (last != Cr)
            {
                lastIdx++;
            }
            return str.Substring(0, lastIdx);
        }

        /// <summary>
        /// Remove the last character from a String.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Chop(null)          = null
        /// <para/> StringUtils.Chop("")            = ""
        /// <para/> StringUtils.Chop("abc \r")      = "abc "
        /// <para/> StringUtils.Chop("abc\n")       = "abc"
        /// <para/> StringUtils.Chop("abc\r\n")     = "abc"
        /// <para/> StringUtils.Chop("abc")         = "ab"
        /// <para/> StringUtils.Chop("abc\nabc")    = "abc\nab"
        /// <para/> StringUtils.Chop("a")           = ""
        /// <para/> StringUtils.Chop("\r")          = ""
        /// <para/> StringUtils.Chop("\n")          = ""
        /// <para/> StringUtils.Chop("\r\n")        = ""
        /// </para>
        /// <param name="str">input string</param>
        /// <returns>string without last character</returns>
        public static string Chop(string str)
        {
            if (str == null)
            {
                return null;
            }
            var strLen = str.Length;
            if (strLen < 2)
            {
                return Empty;
            }
            var lastIdx = strLen - 1;
            var ret = str.Substring(0, lastIdx);
            var last = str[lastIdx];
            if (last == Lf && ret[lastIdx - 1] == Cr)
            {
                return ret.Substring(0, lastIdx - 1);
            }
            return ret;
        }

        #endregion

        #region Paddings and Conversions

        /// <summary>
        /// Repeat a String n times to form a new String.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Repeat(null, 2) = null
        /// <para/> StringUtils.Repeat("", 0)   = ""
        /// <para/> StringUtils.Repeat("", 2)   = ""
        /// <para/> StringUtils.Repeat("a", 3)  = "aaa"
        /// <para/> StringUtils.Repeat("ab", 2) = "abab"
        /// <para/> StringUtils.Repeat("a", -2) = ""
        /// </para>
        /// <param name="str">string to repeat</param>
        /// <param name="repeat">no.of times to repeat</param>
        /// <returns>a new repeated string</returns>
        public static string Repeat(string str, int repeat)
        {

            if (str == null)
            {
                return null;
            }
            if (repeat <= 0)
            {
                return Empty;
            }
            var inputLength = str.Length;
            if (repeat == 1 || inputLength == 0)
            {
                return str;
            }
     
            int outputLength = inputLength * repeat;
            switch (inputLength)
            {
                case 2:
                    var ch0 = str[0];
                    var ch1 = str[1];
                    var output2 = new char[outputLength];
                    for (var i = repeat * 2 - 2; i >= 0; i--, i--)
                    {
                        output2[i] = ch0;
                        output2[i + 1] = ch1;
                    }
                    return new string(output2);
                default:
                    var buf = new StringBuilder(outputLength);
                    for (int i = 0; i < repeat; i++)
                    {
                        buf.Append(str);
                    }
                    return buf.ToString();
            }
        }

        /// <summary>
        /// Repeat a String n times to form a new String.,with a string separator added in between
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Repeat(null, null, 2) = null
        /// <para/> StringUtils.Repeat(null, "x", 2)  = null
        /// <para/> StringUtils.Repeat("", null, 0)   = ""
        /// <para/> StringUtils.Repeat("", "", 2)     = ""
        /// <para/> StringUtils.Repeat("x", "", 3)    = "xxx"
        /// <para/> StringUtils.Repeat("?", ", ", 3)  = "?, ?, ?"
        /// </para>
        /// <param name="str">string to repeat</param>
        /// <param name="separator">separator to be added</param>
        /// <param name="repeat">no.of times to repeat</param>
        /// <returns>a new repeated string</returns>
        public static string Repeat(string str, string separator, int repeat)
        {
            if (str == null || separator == null)
            {
                return Repeat(str, repeat);
            }
            // given that repeat(String, int) is quite optimized, better to rely on it than try and splice this into it
            var result = Repeat(str + separator, repeat);
            return RemoveEnd(result, separator);
        }

        /// <summary>
        /// Repeat a character n times to form a new String.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Repeat('e', 0)  = ""
        /// <para/> StringUtils.Repeat('e', 3)  = "eee"
        /// <para/> StringUtils.Repeat('e', -2) = ""
        /// </para>
        /// <param name="ch">character to repeat</param>
        /// <param name="repeat">no.of times to repeat</param>
        /// <returns>a new repeated string</returns>
        public static string Repeat(char ch, int repeat)
        {
            if (repeat <= 0)
            {
                return Empty;
            }

            var buf = new char[repeat];
            for (var i = repeat - 1; i >= 0; i--)
            {
                buf[i] = ch;
            }
            return new string(buf);
        }

        /// <summary>
        /// Right pad a String with spaces (' ').
        /// </summary>
        /// <para>
        /// <para/> StringUtils.RightPad(null, *)   = null
        /// <para/> StringUtils.RightPad("", 3)     = "   "
        /// <para/> StringUtils.RightPad("bat", 3)  = "bat"
        /// <para/> StringUtils.RightPad("bat", 5)  = "bat  "
        /// <para/> StringUtils.RightPad("bat", 1)  = "bat"
        /// <para/> StringUtils.RightPad("bat", -1) = "bat"
        /// </para>
        /// <param name="str">the String to pad out</param>
        /// <param name="size">the size to pad to</param>
        /// <returns>right padded String or original String if no padding is necessary</returns>
        public static string RightPad(string str, int size)
        {
            return RightPad(str, size, ' ');
        }

        /// <summary>
        /// Right pad a String with a specified character.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.RightPad(null, *, *)     = null
        /// <para/> StringUtils.RightPad("", 3, 'z')     = "zzz"
        /// <para/> StringUtils.RightPad("bat", 3, 'z')  = "bat"
        /// <para/> StringUtils.RightPad("bat", 5, 'z')  = "batzz"
        /// <para/> StringUtils.RightPad("bat", 1, 'z')  = "bat"
        /// <para/> StringUtils.RightPad("bat", -1, 'z') = "bat"
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="size">the size to pad to</param>
        /// <param name="padChar">the character to pad with</param>
        /// <returns>right padded String or original String if no padding is necessary</returns>
        public static string RightPad(string str, int size, char padChar)
        {
            if (str == null)
            {
                return null;
            }
            var pads = size - str.Length;
            return pads <= 0 ? str : str.PadRight(size, padChar);
        }

        /// <summary>
        /// Right pad a String with a specified String.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.RightPad(null, *, *)      = null
        /// <para/> StringUtils.RightPad("", 3, "z")      = "zzz"
        /// <para/> StringUtils.RightPad("bat", 3, "yz")  = "bat"
        /// <para/> StringUtils.RightPad("bat", 5, "yz")  = "batyz"
        /// <para/> StringUtils.RightPad("bat", 8, "yz")  = "batyzyzy"
        /// <para/> StringUtils.RightPad("bat", 1, "yz")  = "bat"
        /// <para/> StringUtils.RightPad("bat", -1, "yz") = "bat"
        /// <para/> StringUtils.RightPad("bat", 5, null)  = "bat  "
        /// <para/> StringUtils.RightPad("bat", 5, "")    = "bat  "
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="size">the size to pad to</param>
        /// <param name="padStr">the string to pad with</param>
        /// <returns>right padded String or original String if no padding is necessary</returns>
        public static string RightPad(string str, int size, string padStr)
        {
            if (str == null)
            {
                return null;
            }
            if (IsEmpty(padStr))
            {
                padStr = Space;
            }

            var padLen = padStr.Length;
            var strLen = str.Length;
            var pads = size - strLen;
            if (pads <= 0)
            {
                return str; // returns original String when possible
            }
            if (padLen == 1 && pads <= PadLimit)
            {
                return str.PadRight(size, padStr[0]);
            }

            if (pads == padLen)
            {
                return str + padStr;
            }
            if (pads < padLen)
            {
                return str + padStr.Substring(0, pads);
            }
            var padding = new char[pads];
            var padChars = padStr.ToCharArray();
            for (var i = 0; i < pads; i++)
            {
                padding[i] = padChars[i % padLen];
            }
            return str + new string(padding);
        }

        /// <summary>
        /// Left pad a String with spaces (' ').
        /// </summary>
        /// <para>
        /// <para/> StringUtils.LeftPad(null, *)   = null
        /// <para/> StringUtils.LeftPad("", 3)     = "   "
        /// <para/> StringUtils.LeftPad("bat", 3)  = "bat"
        /// <para/> StringUtils.LeftPad("bat", 5)  = "  bat"
        /// <para/> StringUtils.LeftPad("bat", 1)  = "bat"
        /// <para/> StringUtils.LeftPad("bat", -1) = "bat"
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="size">the size to pad to</param>
        /// <returns>left padded String or original String if no padding is necessary</returns>
        public static string LeftPad(string str, int size)
        {
            return LeftPad(str, size, ' ');
        }

        /// <summary>
        /// Left pad a String with a specified character.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.LeftPad(null, *, *)     = null
        /// <para/> StringUtils.LeftPad("", 3, 'z')     = "zzz"
        /// <para/> StringUtils.LeftPad("bat", 3, 'z')  = "bat"
        /// <para/> StringUtils.LeftPad("bat", 5, 'z')  = "zzbat"
        /// <para/> StringUtils.LeftPad("bat", 1, 'z')  = "bat"
        /// <para/> StringUtils.LeftPad("bat", -1, 'z') = "bat"
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="size">the size to pad to</param>
        /// <param name="padChar">the character to pad with</param>
        /// <returns>left padded String or original String if no padding is necessary</returns>
        public static string LeftPad(string str, int size, char padChar)
        {
            if (str == null)
            {
                return null;
            }
            var pads = size - str.Length;
            return pads <= 0 ? str : str.PadLeft(size, padChar);
        }

        /// <summary>
        /// Left pad a String with a specified String.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.LeftPad(null, *, *)      = null
        /// <para/> StringUtils.LeftPad("", 3, "z")      = "zzz"
        /// <para/> StringUtils.LeftPad("bat", 3, "yz")  = "bat"
        /// <para/> StringUtils.LeftPad("bat", 5, "yz")  = "yzbat"
        /// <para/> StringUtils.LeftPad("bat", 8, "yz")  = "yzyzybat"
        /// <para/> StringUtils.LeftPad("bat", 1, "yz")  = "bat"
        /// <para/> StringUtils.LeftPad("bat", -1, "yz") = "bat"
        /// <para/> StringUtils.LeftPad("bat", 5, null)  = "  bat"
        /// <para/> StringUtils.LeftPad("bat", 5, "")    = "  bat"
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="size">the size to pad to</param>
        /// <param name="padStr">the string to pad with</param>
        /// <returns>left padded String or original String if no padding is necessary</returns>
        public static string LeftPad(string str, int size, string padStr)
        {
            if (str == null)
            {
                return null;
            }
            if (IsEmpty(padStr))
            {
                padStr = Space;
            }
            var padLen = padStr.Length;
            var strLen = str.Length;
            var pads = size - strLen;
            if (pads <= 0)
            {
                return str; // returns original String when possible
            }
            if (padLen == 1 && pads <= PadLimit)
            {
                return str.PadLeft(size, padStr[0]);
            }

            if (pads == padLen)
            {
                return padStr + str;
            }
            else if (pads < padLen)
            {
                return padStr.Substring(0, pads) + str;
            }
            else
            {
                var padding = new char[pads];
                var padChars = padStr.ToCharArray();
                for (var i = 0; i < pads; i++)
                {
                    padding[i] = padChars[i % padLen];
                }
                return new string(padding) + str;
            }
        }

        /// <summary>
        /// Gets the length of a string and returns 0 if incase of null input
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Length("bat")   =3
        /// <para/> StringUtils.Length("")      =0
        /// <para/> StringUtils.Length(null)    =0
        /// </para>
        /// <param name="str">input string</param>
        /// <returns>legth of string or 0 if null</returns>
        public static int Length(string str)
        {
            return str == null ? 0 : str.Length;
        }

        /// <summary>
        /// Centers a String in a larger String of size using space (' ')
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Center(null, *)   = null
        /// <para/> StringUtils.Center("", 4)     = "    "
        /// <para/> StringUtils.Center("ab", -1)  = "ab"
        /// <para/> StringUtils.Center("ab", 4)   = " ab "
        /// <para/> StringUtils.Center("abcd", 2) = "abcd"
        /// <para/> StringUtils.Center("a", 4)    = " a  "
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="size">size of new String, negative treated as zero</param>
        /// <returns>centered string</returns>
        public static string Center(string str, int size)
        {
            return Center(str, size, ' ');
        }

        /// <summary>
        /// Centers a String in a larger String of size using the given character.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Center(null, *, *)     = null
        /// <para/> StringUtils.Center("", 4, ' ')     = "    "
        /// <para/> StringUtils.Center("ab", -1, ' ')  = "ab"
        /// <para/> StringUtils.Center("ab", 4, ' ')   = " ab "
        /// <para/> StringUtils.Center("abcd", 2, ' ') = "abcd"
        /// <para/> StringUtils.Center("a", 4, ' ')    = " a  "
        /// <para/> StringUtils.Center("a", 4, 'y')    = "yayy"
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="size">size of new String, negative treated as zero</param>
        /// <param name="padChar">character to pad with</param>
        /// <returns>centered string</returns>
        public static string Center(string str, int size, char padChar)
        {
            if (str == null || size <= 0)
            {
                return str;
            }
            var strLen = str.Length;
            var pads = size - strLen;
            if (pads <= 0)
            {
                return str;
            }
            str = LeftPad(str, strLen + pads / 2, padChar);
            str = RightPad(str, size, padChar);
            return str;
        }

        /// <summary>
        /// Centers a String in a larger String of size using the given string.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Center(null, *, *)     = null
        /// <para/> StringUtils.Center("", 4, " ")     = "    "
        /// <para/> StringUtils.Center("ab", -1, " ")  = "ab"
        /// <para/> StringUtils.Center("ab", 4, " ")   = " ab "
        /// <para/> StringUtils.Center("abcd", 2, " ") = "abcd"
        /// <para/> StringUtils.Center("a", 4, " ")    = " a  "
        /// <para/> StringUtils.Center("a", 4, "yz")   = "yayz"
        /// <para/> StringUtils.Center("abc", 7, null) = "  abc  "
        /// <para/> StringUtils.Center("abc", 7, "")   = "  abc  "
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="size">size of new String, negative treated as zero</param>
        /// <param name="padStr">string to pad with</param>
        /// <returns>centered string</returns>
        public static string Center(string str, int size, string padStr)
        {
            if (str == null || size <= 0)
            {
                return str;
            }
            if (IsEmpty(padStr))
            {
                padStr = Space;
            }
            var strLen = str.Length;
            var pads = size - strLen;
            if (pads <= 0)
            {
                return str;
            }
            str = LeftPad(str, strLen + pads / 2, padStr);
            str = RightPad(str, size, padStr);
            return str;
        }

        #endregion

        #region Casing

        /// <summary>
        /// Converts a string to upper case
        /// </summary>
        /// <para>
        /// <para/> StringUtils.UpperCase(null)  = null
        /// <para/> StringUtils.UpperCase("")    = ""
        /// <para/> StringUtils.UpperCase("abc") = "ABC"
        /// </para>
        /// <param name="str">input string</param>
        /// <returns>uppercased string</returns>
        public static string UpperCase(string str)
        {
            return str == null ? null : str.ToUpperInvariant();
        }

        /// <summary>
        /// Converts a string to lower case
        /// </summary>
        /// <para>
        /// <para/> StringUtils.LowerCase(null)  = null
        /// <para/> StringUtils.LowerCase("")    = ""
        /// <para/> StringUtils.LowerCase("AbC") = "abc"
        /// </para>
        /// <param name="str">input string</param>
        /// <returns>lowercased string</returns>
        public static string LowerCase(string str)
        {
            return str == null ? null : str.ToLowerInvariant();
        }

        /// <summary>
        /// Capitalizes a string by changing the first letter to a title letter
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Capitalize(null)  = null
        /// <para/> StringUtils.Capitalize("")    = ""
        /// <para/> StringUtils.Capitalize("cat") = "Cat"
        /// <para/> StringUtils.Capitalize("cAt") = "CAt"
        /// </para>
        /// <param name="str">input string</param>
        /// <returns>Capitalized string</returns>
        public static string Capitalize(string str)
        {

            if (IsEmpty(str))
            {
                return str;
            }

            var firstChar = str[0];
            var newChar = char.ToUpperInvariant(firstChar);
            if (firstChar == newChar)
            {
                // already capitalized
                return str;
            }

            var newCharArray = str.ToCharArray();
            newCharArray[0] = newChar;
            return new string(newCharArray);

        }

        /// <summary>
        /// Uncapitalizes a String, changing the first letter to lower case
        /// </summary>
        /// <para>
        /// <para/> StringUtils.UnCapitalize(null)  = null
        /// <para/> StringUtils.UnCapitalize("")    = ""
        /// <para/> StringUtils.UnCapitalize("cat") = "cat"
        /// <para/> StringUtils.UnCapitalize("Cat") = "cat"
        /// <para/> StringUtils.UnCapitalize("CAT") = "cAT"
        /// </para>
        /// <param name="str">input string</param>
        /// <returns>Uncapitalized string</returns>
        public static string UnCapitalize(string str)
        {

            if (IsEmpty(str))
            {
                return str;
            }

            var firstChar = str[0];
            var newChar = char.ToLowerInvariant(firstChar);
            if (firstChar == newChar)
            {
                // already uncapitalized
                return str;
            }

            var newChars = str.ToCharArray();
            newChars[0] = newChar;
            return new string(newChars);
        }

        /// <summary>
        /// Swaps the case of a String changing upper and title case to lower case, and lower case to upper case.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.SwapCase(null)                 = null
        /// <para/> StringUtils.SwapCase("")                   = ""
        /// <para/> StringUtils.SwapCase("Good Day")           = "gOOD dAY"
        /// </para>
        /// <param name="str">input string</param>
        /// <returns>Swapcased string</returns>
        public static string SwapCase(string str)
        {
            if (IsEmpty(str))
            {
                return str;
            }

            var buffer = str.ToCharArray();

            for (var i = 0; i < buffer.Length; i++)
            {
                var ch = buffer[i];
                if (char.IsUpper(ch))
                {
                    buffer[i] = char.ToLowerInvariant(ch);
                }
                else if (char.IsLower(ch))
                {
                    buffer[i] = char.ToUpperInvariant(ch);
                }
            }
            return new string(buffer);
        }

        /// <summary>
        /// Counts how many times the substring appears in the larger string.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.CountMatches(null, *)       = 0
        /// <para/> StringUtils.CountMatches("", *)         = 0
        /// <para/> StringUtils.CountMatches("abba", null)  = 0
        /// <para/> StringUtils.CountMatches("abba", "")    = 0
        /// <para/> StringUtils.CountMatches("abba", "a")   = 2
        /// <para/> StringUtils.CountMatches("abba", "ab")  = 1
        /// <para/> StringUtils.CountMatches("abba", "xxx") = 0
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="sub">substring to search for</param>
        /// <returns>the count of substring matches</returns>
        public static int CountMatches(string str, string sub)
        {
            if (IsEmpty(str) || IsEmpty(sub))
            {
                return 0;
            }
            var count = 0;
            var idx = 0;
            while ((idx = IndexOf(str, sub, idx)) != IndexNotFound)
            {
                count++;
                idx += sub.Length;
            }
            return count;
        }

        /// <summary>
        /// Counts how many times a character appears in the larger string.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.CountMatches(null, *)       = 0
        /// <para/> StringUtils.CountMatches("", *)         = 0
        /// <para/> StringUtils.CountMatches("abba", '0')   = 0
        /// <para/> StringUtils.CountMatches("abba", 'a')   = 2
        /// <para/> StringUtils.CountMatches("abba", 'b')   = 2
        /// <para/> StringUtils.CountMatches("abba", 'x')   = 0
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="ch">character to search for</param>
        /// <returns>the count of character matches</returns>
        public static int CountMatches(string str, char ch)
        {
            return IsEmpty(str) ? 0 : str.Count(t => ch == t);
        }

        #endregion

        #region Checkings

        /// <summary>
        /// Checks if the string contains only Unicode letters
        /// </summary>
        /// <para>
        /// <para/> StringUtils.IsAlpha(null)   = false
        /// <para/> StringUtils.IsAlpha("")     = false
        /// <para/> StringUtils.IsAlpha("  ")   = false
        /// <para/> StringUtils.IsAlpha("abc")  = true
        /// <para/> StringUtils.IsAlpha("ab2c") = false
        /// <para/> StringUtils.IsAlpha("ab-c") = false
        /// </para>
        /// <param name="str">input string</param>
        /// <returns>true or false</returns>
        public static bool IsAlpha(string str)
        {
            return !IsEmpty(str) && str.All(char.IsLetter);
        }

        /// <summary>
        /// Checks if the string contains only Unicode letters and space
        /// </summary>
        /// <para>
        /// <para/> StringUtils.IsAlphaSpace(null)   = false
        /// <para/> StringUtils.IsAlphaSpace("")     = true
        /// <para/> StringUtils.IsAlphaSpace("  ")   = true
        /// <para/> StringUtils.IsAlphaSpace("abc")  = true
        /// <para/> StringUtils.IsAlphaSpace("ab c") = true
        /// <para/> StringUtils.IsAlphaSpace("ab2c") = false
        /// <para/> StringUtils.IsAlphaSpace("ab-c") = false
        /// </para>
        /// <param name="str">input string</param>
        /// <returns>true or false</returns>
        public static bool IsAlphaSpace(string str)
        {
            return str != null && str.All(t => char.IsLetter(t) || t == ' ');
        }

        /// <summary>
        /// Checks if the string contains only Unicode letters and numbers
        /// </summary>
        /// <para>
        /// <para/> StringUtils.IsAlphaNumeric(null)   = false
        /// <para/> StringUtils.IsAlphaNumeric("")     = false
        /// <para/> StringUtils.IsAlphaNumeric("  ")   = false
        /// <para/> StringUtils.IsAlphaNumeric("abc")  = true
        /// <para/> StringUtils.IsAlphaNumeric("ab c") = false
        /// <para/> StringUtils.IsAlphaNumeric("ab2c") = true
        /// <para/> StringUtils.IsAlphaNumeric("ab-c") = false
        /// </para>
        /// <param name="str">input string</param>
        /// <returns>true or false</returns>
        public static bool IsAlphaNumeric(string str)
        {
            return IsNotEmpty(str) && str.All(char.IsLetterOrDigit);
        }

        /// <summary>
        /// Checks if the string contains only Unicode letters and numbers
        /// </summary>
        /// <para>
        /// <para/> StringUtils.IsAlphaNumericSpace(null)   = false
        /// <para/> StringUtils.IsAlphaNumericSpace("")     = true
        /// <para/> StringUtils.IsAlphaNumericSpace("  ")   = true
        /// <para/> StringUtils.IsAlphaNumericSpace("abc")  = true
        /// <para/> StringUtils.IsAlphaNumericSpace("ab c") = true
        /// <para/> StringUtils.IsAlphaNumericSpace("ab2c") = true
        /// <para/> StringUtils.IsAlphaNumericSpace("ab-c") = false
        /// </para>
        /// <param name="str">input string</param>
        /// <returns>true or false</returns>
        public static bool IsAlphaNumericSpace(string str)
        {
            return str != null && str.All(t => char.IsLetterOrDigit(t) || t == ' ');
        }

        /// <summary>
        /// Checks if the string contains only ASCII printable characters.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.IsAsciiPrintable(null)     = false
        /// <para/> StringUtils.IsAsciiPrintable("")       = true
        /// <para/> StringUtils.IsAsciiPrintable(" ")      = true
        /// <para/> StringUtils.IsAsciiPrintable("Ceki")   = true
        /// <para/> StringUtils.IsAsciiPrintable("ab2c")   = true
        /// <para/> StringUtils.IsAsciiPrintable("!ab-c~") = true
        /// <para/> StringUtils.IsAsciiPrintable("\u0020") = true
        /// <para/> StringUtils.IsAsciiPrintable("\u0021") = true
        /// <para/> StringUtils.IsAsciiPrintable("\u007e") = true
        /// <para/> StringUtils.IsAsciiPrintable("\u007f") = false
        /// <para/> StringUtils.IsAsciiPrintable("Ceki G\u00fclc\u00fc") = false
        /// </para>
        /// <param name="str">input string</param>
        /// <returns>true or false</returns>
        public static bool IsAsciiPrintable(string str)
        {
            if (str == null)
            {
                return false;
            }
            var sz = str.Length;
            for (var i = 0; i < sz; i++)
            {
                if (char.IsControl(str[i]))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Checks if the string contains only Unicode numbers
        /// </summary>
        /// <para>
        /// <para/> StringUtils.IsNumeric(null)   = false
        /// <para/> StringUtils.IsNumeric("")     = false
        /// <para/> StringUtils.IsNumeric("  ")   = false
        /// <para/> StringUtils.IsNumeric("123")  = true
        /// <para/> StringUtils.IsNumeric("\u0967\u0968\u0969")  = true
        /// <para/> StringUtils.IsNumeric("12 3") = false
        /// <para/> StringUtils.IsNumeric("ab2c") = false
        /// <para/> StringUtils.IsNumeric("12-3") = false
        /// <para/> StringUtils.IsNumeric("12.3") = false
        /// <para/> StringUtils.IsNumeric("-123") = false
        /// <para/> StringUtils.IsNumeric("+123") = false
        /// </para>
        /// <param name="str">input string</param>
        /// <returns>true or false</returns>
        public static bool IsNumeric(string str)
        {
            return !IsEmpty(str) && str.All(char.IsDigit);
        }

        /// <summary>
        /// Checks if the string contains only Unicode numbers or space
        /// </summary>
        /// <para>
        /// <para/> StringUtils.IsNumericSpace(null)   = false
        /// <para/> StringUtils.IsNumericSpace("")     = false
        /// <para/> StringUtils.IsNumericSpace("  ")   = true
        /// <para/> StringUtils.IsNumericSpace("123")  = true
        /// <para/> StringUtils.IsNumericSpace("12 3") = true
        /// <para/> StringUtils.IsNumeric("\u0967\u0968\u0969")  = true
        /// <para/> StringUtils.IsNumeric("\u0967\u0968\u0969")  = fa
        /// <para/> StringUtils.IsNumericSpace("ab2c") = false
        /// <para/> StringUtils.IsNumericSpace("12-3") = false
        /// <para/> StringUtils.IsNumericSpace("12.3") = false
        /// </para>
        /// <param name="str">input string</param>
        /// <returns>true or false</returns>
        public static bool IsNumericSpace(string str)
        {
            return IsNotEmpty(str) && str.All(t => char.IsDigit(t) || t == ' ');
        }

        /// <summary>
        /// Checks if the string contains only whitespace
        /// </summary>
        /// <para>
        /// <para/> StringUtils.IsWhiteSpace(null)   = false
        /// <para/> StringUtils.IsWhiteSpace("")     = true
        /// <para/> StringUtils.IsWhiteSpace("  ")   = true
        /// <para/> StringUtils.IsWhiteSpace("abc")  = false
        /// <para/> StringUtils.IsWhiteSpace("ab2c") = false
        /// <para/> StringUtils.IsWhiteSpace("ab-c") = false
        /// </para>
        /// <param name="str">input string</param>
        /// <returns>true or false</returns>
        public static bool IsWhiteSpace(string str)
        {
            return str != null && str.All(char.IsWhiteSpace);
        }

        /// <summary>
        /// Checks if the string contains only lowercase letters
        /// </summary>
        /// <para>
        /// <para/> StringUtils.IsAllLowerCase(null)   = false
        /// <para/> StringUtils.IsAllLowerCase("")     = false
        /// <para/> StringUtils.IsAllLowerCase("  ")   = false
        /// <para/> StringUtils.IsAllLowerCase("abc")  = true
        /// <para/> StringUtils.IsAllLowerCase("abC")  = false
        /// <para/> StringUtils.IsAllLowerCase("ab c") = false
        /// <para/> StringUtils.IsAllLowerCase("ab1c") = false
        /// <para/> StringUtils.IsAllLowerCase("ab/c") = false
        /// </para>
        /// <param name="str">input string</param>
        /// <returns>true or false</returns>
        public static bool IsAllLowerCase(string str)
        {
            return !IsEmpty(str) && str.All(char.IsLower);
        }

        /// <summary>
        /// Checks if the string contains only uppercase letters
        /// </summary>
        /// <para>
        /// <para/> StringUtils.IsAllUpperCase(null)   = false
        /// <para/> StringUtils.IsAllUpperCase("")     = false
        /// <para/> StringUtils.IsAllUpperCase("  ")   = false
        /// <para/> StringUtils.IsAllUpperCase("ABC")  = true
        /// <para/> StringUtils.IsAllUpperCase("aBC")  = false
        /// <para/> StringUtils.IsAllUpperCase("A C")  = false
        /// <para/> StringUtils.IsAllUpperCase("A1C")  = false
        /// <para/> StringUtils.IsAllUpperCase("A/C")  = false
        /// </para>
        /// <param name="str">input string</param>
        /// <returns>true or false</returns>
        public static bool IsAllUpperCase(string str)
        {
            return !IsEmpty(str) && str.All(char.IsUpper);
        }

        /// <summary>
        /// Returns either the input string or the default string if the input string is null
        /// </summary>
        /// <para>
        /// <para/> StringUtils.DefaultString(null)  = ""
        /// <para/> StringUtils.DefaultString("")    = ""
        /// <para/> StringUtils.DefaultString("bat") = "bat"
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="defaultStr">default string to be returned incase of null ("" will be returned by default if not passed)</param>
        /// <returns>passed string or default string if it was null</returns>
        public static string DefaultString(string str, string defaultStr = "")
        {
            return str ?? defaultStr;
        }

        /// <summary>
        /// Returns either the input string or the default string if the input string is blank
        /// </summary>
        /// <para>
        /// <para/> StringUtils.DefaultIfBlank(null, "abc")  = "abc"
        /// <para/> StringUtils.DefaultIfBlank("")    = ""
        /// <para/> StringUtils.DefaultIfBlank(" ")   = ""
        /// <para/> StringUtils.DefaultIfBlank("bat", "abc") = "bat"
        /// <para/> StringUtils.DefaultIfBlank("", null)      = null
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="defaultStr">default string to be returned incase of blank ("" will be returned by default if not passed)</param>
        /// <returns>passed string or default string if it was blank</returns>
        public static string DefaultIfBlank(string str, string defaultStr = "")
        {
            return IsBlank(str) ? defaultStr : str;
        }

        /// <summary>
        /// Returns either the input string or the default string if the input string is Empty
        /// </summary>
        /// <para>
        /// <para/> StringUtils.DefaultIfEmpty(null, "NULL")  = "NULL"
        /// <para/> StringUtils.DefaultIfEmpty("", "NULL")    = "NULL"
        /// <para/> StringUtils.DefaultIfEmpty(" ", "NULL")   = " "
        /// <para/> StringUtils.DefaultIfEmpty("bat", "NULL") = "bat"
        /// <para/> StringUtils.DefaultIfEmpty("", null)      = null
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="defaultStr">default string to be returned incase of empty</param>
        /// <returns>passed string or default string if it was empty</returns>
        public static string DefaultIfEmpty(string str, string defaultStr)
        {
            return IsEmpty(str) ? defaultStr : str;
        }

        /// <summary>
        /// Rotate a string of characters
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Rotate(null, *)        = null
        /// <para/> StringUtils.Rotate("", *)          = ""
        /// <para/> StringUtils.Rotate("abcdefg", 0)   = "abcdefg"
        /// <para/> StringUtils.Rotate("abcdefg", 2)   = "fgabcde"
        /// <para/> StringUtils.Rotate("abcdefg", -2)  = "cdefgab"
        /// <para/> StringUtils.Rotate("abcdefg", 7)   = "abcdefg"
        /// <para/> StringUtils.Rotate("abcdefg", -7)  = "abcdefg"
        /// <para/> StringUtils.Rotate("abcdefg", 9)   = "fgabcde"
        /// <para/> StringUtils.Rotate("abcdefg", -9)  = "cdefgab"
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="shift">number of times to shift</param>
        /// <returns>Rotated string</returns>
        public static string Rotate(string str, int shift)
        {
            if (str == null)
            {
                return null;
            }

            var strLen = str.Length;
            if (shift == 0 || strLen == 0 || shift % strLen == 0)
            {
                return str;
            }

            var builder = new StringBuilder();
            var offset = -(shift % strLen);
            builder.Append(Substring(str, offset));
            builder.Append(SubstringWithNegatives(str, 0, offset-1));
            return builder.ToString();
        }

        /// <summary>
        /// Reverses a string
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Reverse(null)  = null
        /// <para/> StringUtils.Reverse("")    = ""
        /// <para/> StringUtils.Reverse("bat") = "tab"
        /// </para>
        /// <param name="str">input string</param>
        /// <returns>reversed string</returns>
        public static string Reverse(string str)
        {
            return str == null ? null : new string(str.Reverse().ToArray());
        }

        /// <summary>
        /// Reverses a String that is delimited by a specific character.
        /// </summary>
        ///<para>
        /// <para/> StringUtils.ReverseDelimited(null, *)      = null
        /// <para/> StringUtils.ReverseDelimited("", *)        = ""
        /// <para/> StringUtils.ReverseDelimited("a.b.c", 'x') = "a.b.c"
        /// <para/> StringUtils.ReverseDelimited("a.b.c", '.') = "c.b.a" 
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="separatorChar">the seperator character to use</param>
        /// <returns>reversed string</returns>
        public static string ReverseDelimited(string str, char separatorChar)
        {
            if (str == null)
            {
                return null;
            }

            string[] strs = Split(str, separatorChar);
            Array.Reverse(strs);
            return Join(strs, separatorChar.ToString());
        }

        #endregion

        #region Abbreviate

        /// <summary>
        /// Abbreviates a String using ellipses.For example "Today is a nice day for everyone" will become "Today is a nice..."
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Abbreviate(null, *)      = null
        /// <para/> StringUtils.Abbreviate("", 4)        = ""
        /// <para/> StringUtils.Abbreviate("abcdefg", 6) = "abc..."
        /// <para/> StringUtils.Abbreviate("abcdefg", 7) = "abcdefg"
        /// <para/> StringUtils.Abbreviate("abcdefg", 8) = "abcdefg"
        /// <para/> StringUtils.Abbreviate("abcdefg", 4) = "a..."
        /// <para/> StringUtils.Abbreviate("abcdefg", 3) = ArgumentException
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="maxWidth">maximum length of result String, must be at least 4</param>
        /// <returns>returns abbreviated string or empty if width is less than 4</returns>
        public static string Abbreviate(string str, int maxWidth)
        {
            return Abbreviate(str, 0, maxWidth);
        }

        /// <summary>
        /// Abbreviates a String using ellipses.This allows you to specify offset.For example "Today is a nice day for everyone" will become "...is a nice..."
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Abbreviate(null, *, *)                = null
        /// <para/> StringUtils.Abbreviate("", 0, 4)                  = ""
        /// <para/> StringUtils.Abbreviate("abcdefghijklmno", -1, 10) = "abcdefg..."
        /// <para/> StringUtils.Abbreviate("abcdefghijklmno", 0, 10)  = "abcdefg..."
        /// <para/> StringUtils.Abbreviate("abcdefghijklmno", 1, 10)  = "abcdefg..."
        /// <para/> StringUtils.Abbreviate("abcdefghijklmno", 4, 10)  = "abcdefg..."
        /// <para/> StringUtils.Abbreviate("abcdefghijklmno", 5, 10)  = "...fghi..."
        /// <para/> StringUtils.Abbreviate("abcdefghijklmno", 6, 10)  = "...ghij..."
        /// <para/> StringUtils.Abbreviate("abcdefghijklmno", 8, 10)  = "...ijklmno"
        /// <para/> StringUtils.Abbreviate("abcdefghijklmno", 10, 10) = "...ijklmno"
        /// <para/> StringUtils.Abbreviate("abcdefghijklmno", 12, 10) = "...ijklmno"
        /// <para/> StringUtils.Abbreviate("abcdefghij", 0, 3)        = ArgumentException
        /// <para/> StringUtils.Abbreviate("abcdefghij", 5, 6)        = ArgumentException
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="offset">offset length to be used on the left edge of string</param>
        /// <param name="maxWidth">maximum length of result String, must be at least 4 (should be atleast 7 if offset is used).</param>
        /// <returns>returns abbreviated string. Throws ArgumentException if the width is too small</returns>
        public static string Abbreviate(string str, int offset, int maxWidth)
        {
            if (str == null)
            {
                return null;
            }
            if (maxWidth < 4)
            {
                throw new ArgumentException("Minimum abbreviation width is 4");
            }
            if (str.Length <= maxWidth)
            {
                return str;
            }
            if (offset > str.Length)
            {
                offset = str.Length;
            }
            if (str.Length - offset < maxWidth - 3)
            {
                offset = str.Length - (maxWidth - 3);
            }
            const string abrevMarker = "...";
            if (offset <= 4)
            {
                return str.Substring(0, maxWidth - 3) + abrevMarker;
            }
            if (maxWidth < 7)
            {
                throw new ArgumentException("Minimum abbreviation width with offset is 7");
            }
            if (offset + maxWidth - 3 < str.Length)
            {
                return abrevMarker + Abbreviate(str.Substring(offset), maxWidth - 3);
            }
            return abrevMarker + str.Substring(str.Length - (maxWidth - 3));
        }

        /// <summary>
        /// Abbreviates a String to the length passed, replacing the middle characters with the given replacement String.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.AbbreviateMiddle(null, null, 0)      = null
        /// <para/> StringUtils.AbbreviateMiddle("abc", null, 0)      = "abc"
        /// <para/> StringUtils.AbbreviateMiddle("abc", ".", 0)      = "abc"
        /// <para/> StringUtils.AbbreviateMiddle("abc", ".", 3)      = "abc"
        /// <para/> StringUtils.AbbreviateMiddle("abcdef", ".", 4)     = "ab.f"
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="middle">the String to replace the middle characters with</param>
        /// <param name="length">length to abbreviate</param>
        /// <returns>the abbreviated string or original string if the replacement string is null</returns>
        public static string AbbreviateMiddle(string str, string middle, int length)
        {
            if (IsEmpty(str) || IsEmpty(middle))
            {
                return str;
            }

            if (length >= str.Length || length < middle.Length + 2)
            {
                return str;
            }

            var targetString = length - middle.Length;
            var startOffset = targetString / 2 + targetString % 2;
            var endOffset = str.Length - targetString / 2;

            var builder = new StringBuilder(length);
            builder.Append(str.Substring(0, startOffset));
            builder.Append(middle);
            builder.Append(str.Substring(endOffset));

            return builder.ToString();
        }

        #endregion

        #region Difference

        /// <summary>
        /// Compares two Strings, and returns the part of string where they differ.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Difference(null, null) = null
        /// <para/> StringUtils.Difference("", "") = ""
        /// <para/> StringUtils.Difference("", "abc") = "abc"
        /// <para/> StringUtils.Difference("abc", "") = ""
        /// <para/> StringUtils.Difference("abc", "abc") = ""
        /// <para/> StringUtils.Difference("abc", "ab") = ""
        /// <para/> StringUtils.Difference("ab", "abxyz") = "xyz"
        /// <para/> StringUtils.Difference("abcde", "abxyz") = "xyz"
        /// <para/> StringUtils.Difference("abcde", "xyz") = "xyz"
        /// </para>
        /// <param name="str1">the first string</param>
        /// <param name="str2">the second string</param>
        /// <returns>the portion of str2 where it differs from str1; returns empty String if both are equal</returns>
        public static string Difference(string str1, string str2)
        {
            if (str1 == null)
            {
                return str2;
            }
            if (str2 == null)
            {
                return str1;
            }
            var at = IndexOfDifference(str1, str2);
            return at == IndexNotFound ? Empty : str2.Substring(at);
        }

        /// <summary>
        /// Compares two Strings, and returns the index at which they begin to differ.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.IndexOfDifference(null, null) = -1
        /// <para/> StringUtils.IndexOfDifference("", "") = -1
        /// <para/> StringUtils.IndexOfDifference("", "abc") = 0
        /// <para/> StringUtils.IndexOfDifference("abc", "") = 0
        /// <para/> StringUtils.IndexOfDifference("abc", "abc") = -1
        /// <para/> StringUtils.IndexOfDifference("ab", "abxyz") = 2
        /// <para/> StringUtils.IndexOfDifference("abcde", "abxyz") = 2
        /// <para/> StringUtils.IndexOfDifference("abcde", "xyz") = 0
        /// </para>
        /// <param name="str1">the first string</param>
        /// <param name="str2">the second string</param>
        /// <returns>index at which str1 and str2 begin to differ; returns -1 if both are equal</returns>
        public static int IndexOfDifference(string str1, string str2)
        {
            if (str1 == str2)
            {
                return IndexNotFound;
            }
            if (str1 == null || str2 == null)
            {
                return 0;
            }
            int i;
            for (i = 0; i < str1.Length && i < str2.Length; ++i)
            {
                if (str1[i] != str2[i])
                {
                    break;
                }
            }
            if (i < str2.Length || i < str1.Length)
            {
                return i;
            }
            return IndexNotFound;
        }

        /// <summary>
        /// Compares all strings in an array and returns the index at which the strings begin to differ.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.IndexOfDifference(null) = -1
        /// <para/> StringUtils.IndexOfDifference(new String[] {}) = -1
        /// <para/> StringUtils.IndexOfDifference(new String[] {"abc"}) = -1
        /// <para/> StringUtils.IndexOfDifference(new String[] {null, null}) = -1
        /// <para/> StringUtils.IndexOfDifference(new String[] {"", ""}) = -1
        /// <para/> StringUtils.IndexOfDifference(new String[] {"", null}) = 0
        /// <para/> StringUtils.IndexOfDifference(new String[] {"abc", null, null}) = 0
        /// <para/> StringUtils.IndexOfDifference(new String[] {null, null, "abc"}) = 0
        /// <para/> StringUtils.IndexOfDifference(new String[] {"", "abc"}) = 0
        /// <para/> StringUtils.IndexOfDifference(new String[] {"abc", ""}) = 0
        /// <para/> StringUtils.IndexOfDifference(new String[] {"abc", "abc"}) = -1
        /// <para/> StringUtils.IndexOfDifference(new String[] {"abc", "a"}) = 1
        /// <para/> StringUtils.IndexOfDifference(new String[] {"ab", "abxyz"}) = 2
        /// <para/> StringUtils.IndexOfDifference(new String[] {"abcde", "abxyz"}) = 2
        /// <para/> StringUtils.IndexOfDifference(new String[] {"abcde", "xyz"}) = 0
        /// <para/> StringUtils.IndexOfDifference(new String[] {"xyz", "abcde"}) = 0
        /// <para/> StringUtils.IndexOfDifference(new String[] {"i am a machine", "i am a robot"}) = 7
        /// </para>
        /// <param name="strArray">input array of strings</param>
        /// <returns>the index at which the strings start to differ or -1 if all are equal</returns>
        public static int IndexOfDifference(params string[] strArray)
        {
            if (strArray == null || strArray.Length <= 1)
            {
                return IndexNotFound;
            }
            var anyStringNull = false;
            var allStringsNull = true;
            var arrayLen = strArray.Length;
            var shortestStrLen = int.MaxValue;
            var longestStrLen = 0;

            // find the min and max string lengths; this avoids checking to make
            // sure we are not exceeding the length of the string each time through
            // the bottom loop.
            for (var i = 0; i < arrayLen; i++)
            {
                if (strArray[i] == null)
                {
                    anyStringNull = true;
                    shortestStrLen = 0;
                }
                else
                {
                    allStringsNull = false;
                    shortestStrLen = Math.Min(strArray[i].Length, shortestStrLen);
                    longestStrLen = Math.Max(strArray[i].Length, longestStrLen);
                }
            }

            // handle lists containing all nulls or all empty strings
            if (allStringsNull || longestStrLen == 0 && !anyStringNull)
            {
                return IndexNotFound;
            }

            // handle lists containing some nulls or some empty strings
            if (shortestStrLen == 0)
            {
                return 0;
            }

            // find the position with the first difference across all strings
            var firstDiff = -1;
            for (var stringPos = 0; stringPos < shortestStrLen; stringPos++)
            {
                var comparisonChar = strArray[0][stringPos];
                for (var arrayPos = 1; arrayPos < arrayLen; arrayPos++)
                {
                    if (strArray[arrayPos][stringPos] != comparisonChar)
                    {
                        firstDiff = stringPos;
                        break;
                    }
                }
                if (firstDiff != -1)
                {
                    break;
                }
            }

            if (firstDiff == -1 && shortestStrLen != longestStrLen)
            {
                // we compared all of the characters up to the length of the
                // shortest string and didn't find a match, but the string lengths
                // vary, so return the length of the shortest string.
                return shortestStrLen;
            }
            return firstDiff;
        }

        /// <summary>
        /// Compares all Strings in an array and returns the initial sequence of characters that is common to all of them.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.GetCommonPrefix(null) = ""
        /// <para/> StringUtils.GetCommonPrefix(new String[] {}) = ""
        /// <para/> StringUtils.GetCommonPrefix(new String[] {"abc"}) = "abc"
        /// <para/> StringUtils.GetCommonPrefix(new String[] {null, null}) = ""
        /// <para/> StringUtils.GetCommonPrefix(new String[] {"", ""}) = ""
        /// <para/> StringUtils.GetCommonPrefix(new String[] {"", null}) = ""
        /// <para/> StringUtils.GetCommonPrefix(new String[] {"abc", null, null}) = ""
        /// <para/> StringUtils.GetCommonPrefix(new String[] {null, null, "abc"}) = ""
        /// <para/> StringUtils.GetCommonPrefix(new String[] {"", "abc"}) = ""
        /// <para/> StringUtils.GetCommonPrefix(new String[] {"abc", ""}) = ""
        /// <para/> StringUtils.GetCommonPrefix(new String[] {"abc", "abc"}) = "abc"
        /// <para/> StringUtils.GetCommonPrefix(new String[] {"abc", "a"}) = "a"
        /// <para/> StringUtils.GetCommonPrefix(new String[] {"ab", "abxyz"}) = "ab"
        /// <para/> StringUtils.GetCommonPrefix(new String[] {"abcde", "abxyz"}) = "ab"
        /// <para/> StringUtils.GetCommonPrefix(new String[] {"abcde", "xyz"}) = ""
        /// <para/> StringUtils.GetCommonPrefix(new String[] {"xyz", "abcde"}) = ""
        /// <para/> StringUtils.GetCommonPrefix(new String[] {"i am a machine", "i am a robot"}) = "i am a "
        /// </para>
        /// <param name="strs">inpu array of strings</param>
        /// <returns>the common prefix if any or ""</returns>
        public static string GetCommonPrefix(params string[] strs)
        {
            if (strs == null || strs.Length == 0)
            {
                return Empty;
            }
            var smallestIndexOfDiff = IndexOfDifference(strs);
            if (smallestIndexOfDiff == IndexNotFound)
            {
                // all strings were identical
                return strs[0] ?? Empty;
            }
            return smallestIndexOfDiff == 0 ? Empty : strs[0].Substring(0, smallestIndexOfDiff);
            // we found a common initial character sequence
        }




        #endregion

        #region StartsWith & EndsWith

        /// <summary>
        /// Checks if a string starts with a specified prefix by ignoring case.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.StartsWithIgnoreCase(null, null)      = true
        /// <para/> StringUtils.StartsWithIgnoreCase(null, "abc")     = false
        /// <para/> StringUtils.StartsWithIgnoreCase("abcdef", null)  = false
        /// <para/> StringUtils.StartsWithIgnoreCase("abcdef", "abc") = true
        /// <para/> StringUtils.StartsWithIgnoreCase("ABCDEF", "abc") = true 
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="prefix">the prefix to find</param>
        /// <returns>true or false</returns>
        public static bool StartsWithIgnoreCase(string str, string prefix)
        {
            return StartsWith(str, prefix, true);
        }

        /// <summary>
        /// Checks if a string starts with a specified prefix.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.StartsWith(null, null)      = true 
        /// <para/> StringUtils.StartsWith(null, "abc")     = false
        /// <para/> StringUtils.StartsWith("abcdef", null)  = false
        /// <para/> StringUtils.StartsWith("abcdef", "abc") = true
        /// <para/> StringUtils.StartsWith("ABCDEF", "abc") = false
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="prefix">the prefix to find</param>
        /// <param name="ignoreCase">is case ignored? (default false)</param>
        /// <returns>true or false</returns>
        public static bool StartsWith(string str, string prefix, bool ignoreCase = false)
        {
            if (str == null || prefix == null)
            {
                return str == null && prefix == null;
            }
            if (prefix.Length > str.Length)
            {
                return false;
            }

            return ignoreCase
                ? str.StartsWith(prefix,StringComparison.OrdinalIgnoreCase)
                : str.StartsWith(prefix);
        }

        /// <summary>
        /// Checks if a string starts with any of an array of specified strings.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.StartsWithAny(null, null)      = false
        /// <para/> StringUtils.StartsWithAny(null, new String[] {"abc"})  = false
        /// <para/> StringUtils.StartsWithAny("abcxyz", null)     = false
        /// <para/> StringUtils.StartsWithAny("abcxyz", new String[] {""}) = true
        /// <para/> StringUtils.StartsWithAny("abcxyz", new String[] {"abc"}) = true
        /// <para/> StringUtils.StartsWithAny("abcxyz", new String[] {null, "xyz", "abc"}) = true
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="ignoreCase">is case ignored?</param>
        /// <param name="searchStrings">array of prefix search strings</param>
        /// <returns>true or false</returns>
        public static bool StartsWithAny(string str, bool ignoreCase, params string[] searchStrings)
        {
            if (IsEmpty(str) || CollectionUtils.IsEmpty(searchStrings))
            {
                return false;
            }

            return searchStrings.Any(x => StartsWith(str, x, ignoreCase));
        }

        /// <summary>
        /// Checks if a string ends with a specified suffix by ignoring case.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.EndsWithIgnoreCase(null, null)      = true
        /// <para/> StringUtils.EndsWithIgnoreCase(null, "def")     = false
        /// <para/> StringUtils.EndsWithIgnoreCase("abcdef", null)  = false
        /// <para/> StringUtils.EndsWithIgnoreCase("abcdef", "def") = true
        /// <para/> StringUtils.EndsWithIgnoreCase("ABCDEF", "def") = true
        /// <para/> StringUtils.EndsWithIgnoreCase("ABCDEF", "cde") = false
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="suffix">the suffix to find</param>
        /// <returns>true or false</returns>
        public static bool EndsWithIgnoreCase(string str, string suffix)
        {
            return EndsWith(str, suffix, true);
        }

        /// <summary>
        /// Checks if a string ends with a specified suffix.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.EndsWith(null, null)      = true
        /// <para/> StringUtils.EndsWith(null, "def")     = false
        /// <para/> StringUtils.EndsWith("abcdef", null)  = false
        /// <para/> StringUtils.EndsWith("abcdef", "def") = true
        /// <para/> StringUtils.EndsWith("ABCDEF", "def") = false
        /// <para/> StringUtils.EndsWith("ABCDEF", "cde") = false
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="suffix">the suffix to find</param>
        /// <param name="ignoreCase">is case ignored? (default false)</param>
        /// <returns>true or false</returns>
        public static bool EndsWith(string str, string suffix, bool ignoreCase = false)
        {
            if (str == null || suffix == null)
            {
                return str == null && suffix == null;
            }
            if (suffix.Length > str.Length)
            {
                return false;
            }
            return ignoreCase
              ? str.EndsWith(suffix, StringComparison.CurrentCultureIgnoreCase)
              : str.EndsWith(suffix);
        }

        /// <summary>
        /// Checks if a string ends with any of an array of specified strings.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.EndsWithAny(null, null)      = false
        /// <para/> StringUtils.EndsWithAny(null, new String[] {"abc"})  = false
        /// <para/> StringUtils.EndsWithAny("abcxyz", null)     = false
        /// <para/> StringUtils.EndsWithAny("abcxyz", new String[] {""}) = true
        /// <para/> StringUtils.EndsWithAny("abcxyz", new String[] {"xyz"}) = true
        /// <para/> StringUtils.EndsWithAny("abcxyz", new String[] {null, "xyz", "abc"}) = true
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="ignoreCase">is case ignored? (default false)</param>
        /// <param name="searchStrings">array of suffix search strings</param>
        /// <returns>true or false</returns>
        public static bool EndsWithAny(string str, bool ignoreCase, params string[] searchStrings)
        {
            if (IsEmpty(str) || CollectionUtils.IsEmpty(searchStrings))
            {
                return false;
            }

            return searchStrings.Any(x => EndsWith(str, x, ignoreCase));
        }


        #endregion

        #region Misc


        /// <summary>
        /// Removes leading and trailing whitespaces and replaces sequences of whitespace characters by a single space.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.NormalizeSpace(null)      = null
        /// <para/> StringUtils.NormalizeSpace(" apple ") = "apple"
        /// <para/> StringUtils.NormalizeSpace(" apple  is   good for   health ") = "apple is good for health"
        /// </para>
        /// <param name="str">input string</param>
        /// <returns>normalized string with proper spaces</returns>
        public static string NormalizeSpace(string str)
        {

            if (IsEmpty(str))
            {
                return str;
            }
            var size = str.Length;
            var newChars = new char[size];
            var count = 0;
            var whitespacesCount = 0;
            var startWhitespaces = true;
            for (var i = 0; i < size; i++)
            {
                var actualChar = str[i];
                var isWhitespace = char.IsWhiteSpace(actualChar);
                if (!isWhitespace)
                {
                    startWhitespaces = false;
                    newChars[count++] = (char)(actualChar == 160 ? 32 : actualChar);
                    whitespacesCount = 0;
                }
                else
                {
                    if (whitespacesCount == 0 && !startWhitespaces)
                    {
                        newChars[count++] = Space[0];
                    }
                    whitespacesCount++;
                }
            }
            return startWhitespaces ? Empty : new string(newChars, 0, count - (whitespacesCount > 0 ? 1 : 0));
        }

        #endregion

        #region Append and Prepend

        /// <summary>
        /// Appends the given suffix to the end of the string if the string does not already end with the suffix.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.AppendIfMissing(null, null) = null
        /// <para/> StringUtils.AppendIfMissing("abc", null) = "abc"
        /// <para/> StringUtils.AppendIfMissing("", "xyz") = "xyz"
        /// <para/> StringUtils.AppendIfMissing("abc", "xyz") = "abcxyz"
        /// <para/> StringUtils.AppendIfMissing("abcxyz", "xyz") = "abcxyz"
        /// <para/> StringUtils.AppendIfMissing("abcXYZ", "xyz") = "abcXYZxyz"
        /// <para/> StringUtils.AppendIfMissing(null, null, true) = null
        /// <para/> StringUtils.AppendIfMissing("abc", null, true) = "abc"
        /// <para/> StringUtils.AppendIfMissing("", "xyz", true) = "xyz"
        /// <para/> StringUtils.AppendIfMissing("abc", "xyz",false, {null}) = "abcxyz"
        /// <para/> StringUtils.AppendIfMissing("abcxyz", "xyz",false, {"mno"}) = "abcxyz"
        /// <para/> StringUtils.AppendIfMissing("abcmno", "xyz",false, {"mno"}) = "abcmno"
        /// <para/> StringUtils.AppendIfMissing("abcXYZ", "xyz",false, {"mno"}) = "abcXYZxyz"
        /// <para/> StringUtils.AppendIfMissing("abcMNO", "xyz",false, {"mno"}) = "abcMNOxyz"
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="suffix">suffix to add at the end of the string</param>
        /// <param name="ignoreCase">is case ignored? (default false)</param>
        /// <param name="suffixes">array of suffixes. Once a suffix is found others will be terminated (default null)</param>
        /// <returns>string with suffix added if it doesn't already has it</returns>
        public static string AppendIfMissing(string str, string suffix, bool ignoreCase = false, ICollection<string> suffixes = null)
        {
            if (str == null || IsEmpty(suffix) || EndsWith(str, suffix, ignoreCase))
            {
                return str;
            }
            if (CollectionUtils.IsNotEmpty(suffixes))
            {
                if (suffixes.Any(s => EndsWith(str, s, ignoreCase)))
                {
                    return str;
                }
            }
            return str + suffix;
        }

        /// <summary>
        /// Appends the given suffix to the end of the string if the string does not already end with the suffix. Ignores the casing while searching for the suffix.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.AppendIfMissingIgnoreCase(null, null) = null
        /// <para/> StringUtils.AppendIfMissingIgnoreCase("abc", null) = "abc"
        /// <para/> StringUtils.AppendIfMissingIgnoreCase("", "xyz") = "xyz"
        /// <para/> StringUtils.AppendIfMissingIgnoreCase("abc", "xyz") = "abcxyz"
        /// <para/> StringUtils.AppendIfMissingIgnoreCase("abcxyz", "xyz") = "abcxyz"
        /// <para/> StringUtils.AppendIfMissingIgnoreCase("abcXYZ", "xyz") = "abcXYZ"
        /// <para/> StringUtils.AppendIfMissingIgnoreCase(null, null, null) = null
        /// <para/> StringUtils.AppendIfMissingIgnoreCase("abc", null, null) = "abc"
        /// <para/> StringUtils.AppendIfMissingIgnoreCase("", "xyz", null) = "xyz"
        /// <para/> StringUtils.AppendIfMissingIgnoreCase("abc", "xyz", {null}) = "abcxyz"
        /// <para/> StringUtils.AppendIfMissingIgnoreCase("abc", "xyz", {""}) = "abc"
        /// <para/> StringUtils.AppendIfMissingIgnoreCase("abc", "xyz", {"mno"}) = "abcxyz"
        /// <para/> StringUtils.AppendIfMissingIgnoreCase("abcxyz", "xyz", {"mno"}) = "abcxyz"
        /// <para/> StringUtils.AppendIfMissingIgnoreCase("abcmno", "xyz", {"mno"}) = "abcmno"
        /// <para/> StringUtils.AppendIfMissingIgnoreCase("abcXYZ", "xyz", {"mno"}) = "abcXYZ"
        /// <para/> StringUtils.AppendIfMissingIgnoreCase("abcMNO", "xyz", {"mno"}) = "abcMNO"
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="suffix">suffix to add at the end of the string</param>
        /// <param name="suffixes">array of suffixes. Once a suffix is found others will be terminated (default null)</param>
        /// <returns>string with suffix added if it doesn't already has it</returns>
        public static string AppendIfMissingIgnoreCase(string str, string suffix, string[] suffixes = null)
        {
            return AppendIfMissing(str, suffix, true, suffixes);
        }

        /// <summary>
        /// Prepends the given prefix to the start of the string if the string does not already start with the prefix.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.PrependIfMissing(null, null) = null
        /// <para/> StringUtils.PrependIfMissing("abc", null) = "abc"
        /// <para/> StringUtils.PrependIfMissing("", "xyz") = "xyz"
        /// <para/> StringUtils.PrependIfMissing("abc", "xyz") = "xyzabc"
        /// <para/> StringUtils.PrependIfMissing("xyzabc", "xyz") = "xyzabc"
        /// <para/> StringUtils.PrependIfMissing("XYZabc", "xyz") = "xyzXYZabc"
        /// <para/> StringUtils.PrependIfMissing(null, null, true) = null
        /// <para/> StringUtils.PrependIfMissing("abc", null, false) = "abc"
        /// <para/> StringUtils.PrependIfMissing("", "xyz", false) = "xyz"
        /// <para/> StringUtils.PrependIfMissing("abc", "xyz",false, {null}) = "xyzabc"
        /// <para/> StringUtils.PrependIfMissing("abc", "xyz",false, {""}) = "abc"
        /// <para/> StringUtils.PrependIfMissing("abc", "xyz",false, {"mno"}) = "xyzabc"
        /// <para/> StringUtils.PrependIfMissing("xyzabc", "xyz",false, {"mno"}) = "xyzabc"
        /// <para/> StringUtils.PrependIfMissing("mnoabc", "xyz",false, {"mno"}) = "mnoabc"
        /// <para/> StringUtils.PrependIfMissing("XYZabc", "xyz",false, {"mno"}) = "xyzXYZabc"
        /// <para/> StringUtils.PrependIfMissing("MNOabc", "xyz",false, {"mno"}) = "xyzMNOabc"
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="prefix">prefix to add at the start of the string</param>
        /// <param name="ignoreCase">is case ignored? (default false)</param>
        /// <param name="prefixes">array of prefixes. Once a prefix is found others will be terminated (default null)</param>
        /// <returns>string with prefix added if it doesn't already has it</returns>
        public static string PrependIfMissing(string str, string prefix, bool ignoreCase = false, ICollection<string> prefixes = null)
        {
            if (str == null || IsEmpty(prefix) || StartsWith(str, prefix, ignoreCase))
            {
                return str;
            }
            if (CollectionUtils.IsNotEmpty(prefixes))
            {
                if (prefixes.Any(s => StartsWith(str, s, ignoreCase)))
                {
                    return str;
                }
            }
            return prefix + str;

        }


        /// <summary>
        /// Prepends the given prefix to the start of the string if the string does not already start with the prefix. Ignores the casing while searching for the prefix.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.PrependIfMissingIgnoreCase(null, null) = null
        /// <para/> StringUtils.PrependIfMissingIgnoreCase("abc", null) = "abc"
        /// <para/> StringUtils.PrependIfMissingIgnoreCase("", "xyz") = "xyz"
        /// <para/> StringUtils.PrependIfMissingIgnoreCase("abc", "xyz") = "xyzabc"
        /// <para/> StringUtils.PrependIfMissingIgnoreCase("xyzabc", "xyz") = "xyzabc"
        /// <para/> StringUtils.PrependIfMissingIgnoreCase("XYZabc", "xyz") = "XYZabc"
        /// <para/> StringUtils.PrependIfMissingIgnoreCase(null, null, null) = null
        /// <para/> StringUtils.PrependIfMissingIgnoreCase("abc", null, null) = "abc"
        /// <para/> StringUtils.PrependIfMissingIgnoreCase("", "xyz", null) = "xyz"
        /// <para/> StringUtils.PrependIfMissingIgnoreCase("abc", "xyz", {null}) = "xyzabc"
        /// <para/> StringUtils.PrependIfMissingIgnoreCase("abc", "xyz",{ ""}) = "abc"
        /// <para/> StringUtils.PrependIfMissingIgnoreCase("abc", "xyz", {"mno"}) = "xyzabc"
        /// <para/> StringUtils.PrependIfMissingIgnoreCase("xyzabc", "xyz", {"mno"}) = "xyzabc"
        /// <para/> StringUtils.PrependIfMissingIgnoreCase("mnoabc", "xyz", {"mno"}) = "mnoabc"
        /// <para/> StringUtils.PrependIfMissingIgnoreCase("XYZabc", "xyz", {"mno"}) = "XYZabc"
        /// <para/> StringUtils.PrependIfMissingIgnoreCase("MNOabc", "xyz", {"mno"}) = "MNOabc"
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="prefix">prefix to add at the start of the string</param>
        /// <param name="prefixes">array of prefixes. Once a prefix is found others will be terminated (default null)</param>
        /// <returns>string with prefix added if it doesn't already has it</returns>
        public static string PrependIfMissingIgnoreCase(string str, string prefix, string[] prefixes = null)
        {
            return PrependIfMissing(str, prefix, true, prefixes);
        }

        /// <summary>
        /// Wraps a string with a character.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Wrap(null, *)        = null
        /// <para/> StringUtils.Wrap("", *)          = ""
        /// <para/> StringUtils.Wrap("ab", '\0')     = "ab"
        /// <para/> StringUtils.Wrap("ab", 'x')      = "xabx"
        /// <para/> StringUtils.Wrap("ab", '\'')     = "'ab'"
        /// <para/> StringUtils.Wrap("\"ab\"", '\"') = "\"\"ab\"\""
        /// </para>
        /// <param name="str">input</param>
        /// <param name="wrapWith">character to be wrapped with</param>
        /// <returns>wrapped text</returns>
        public static string Wrap(string str, char wrapWith)
        {

            if (IsEmpty(str) || wrapWith == '\0')
            {
                return str;
            }

            return wrapWith + str + wrapWith;
        }

        /// <summary>
        /// raps a String with the given String.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.Wrap(null, *)         = null
        /// <para/> StringUtils.Wrap("", *)           = ""
        /// <para/> StringUtils.Wrap("ab", null)      = "ab"
        /// <para/> StringUtils.Wrap("ab", "x")       = "xabx"
        /// <para/> StringUtils.Wrap("ab", "\"")      = "\"ab\""
        /// <para/> StringUtils.Wrap("\"ab\"", "\"")  = "\"\"ab\"\""
        /// <para/> StringUtils.Wrap("ab", "'")       = "'ab'"
        /// <para/> StringUtils.Wrap("'abcd'", "'")   = "''abcd''"
        /// <para/> StringUtils.Wrap("\"abcd\"", "'") = "'\"abcd\"'"
        /// <para/> StringUtils.Wrap("'abcd'", "\"")  = "\"'abcd'\""
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="wrapWith">string to be wrapped with</param>
        /// <returns>wrapped text</returns>
        public static string Wrap(string str, string wrapWith)
        {

            if (IsEmpty(str) || IsEmpty(wrapWith))
            {
                return str;
            }

            return wrapWith + str + wrapWith;
        }

        #endregion

        #region Encoding

        /// <summary>
        /// Converts a byte array to a String using the Ascii encoding.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.ToAsciiString(null)                                 = null   
        /// <para/> StringUtils.ToAsciiString(new byte[]{})                         = ""     
        /// <para/> StringUtils.ToAsciiString(new byte[] { 65, 112, 112, 108, 101 })= "Apple"
        /// </para>
        /// <param name="bytes">input byte array</param>
        /// <param name="throwExceptions">do u want to throw exception? (default false). Pass 'true' to allow exceptions </param>
        /// <returns>Converted string or empty if convertion fails</returns>
        public static string ToAsciiString(byte[] bytes, bool throwExceptions = false)
        {
            if (bytes == null) return null;
            try
            {
                return Encoding.ASCII.GetString(bytes);
            }
            catch (Exception ex)
            {
                if (throwExceptions)
                    throw ex;
                return Empty;
            }

        }

        /// <summary>
        /// Converts a byte array to a String using the BigEndianUnicode encoding.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.ToBigEndianUnicodeString(null)                                 = null
        /// <para/> StringUtils.ToBigEndianUnicodeString(new byte[] { })                       = ""
        /// <para/> StringUtils.ToBigEndianUnicodeString(new byte[] { 0, 65, 0, 112, 0, 112, 0, 108, 0, 101 })= "Apple"
        /// </para>
        /// <param name="bytes">input byte array</param>
        /// <param name="throwExceptions">do u want to throw exception? (default false). Pass 'true' to allow exceptions </param>
        /// <returns>Converted string or empty if convertion fails</returns>
        public static string ToBigEndianUnicodeString(byte[] bytes, bool throwExceptions = false)
        {
            if (bytes == null) return null;
            try
            {
                return Encoding.BigEndianUnicode.GetString(bytes);
            }
            catch (Exception ex)
            {
                if (throwExceptions)
                    throw ex;
                return Empty;
            }

        }

        /// <summary>
        /// Converts a byte array to a String using the Unicode encoding.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.ToUnicodeString(null)                                                   = null
        /// <para/> StringUtils.ToUnicodeString(new byte[] { })                                         = ""
        /// <para/> StringUtils.ToUnicodeString(new byte[] { 65, 0, 112, 0, 112, 0, 108, 0, 101, 0 })   = "Apple"
        /// </para>
        /// <param name="bytes">input byte array</param>
        /// <param name="throwExceptions">do u want to throw exception? (default false). Pass 'true' to allow exceptions </param>
        /// <returns>Converted string or empty if convertion fails</returns>
        public static string ToUnicodeString(byte[] bytes, bool throwExceptions = false)
        {
            if (bytes == null) return null;
            try
            {
                return Encoding.Unicode.GetString(bytes);
            }
            catch (Exception ex)
            {
                if (throwExceptions)
                    throw ex;
                return Empty;
            }

        }

        /// <summary>
        /// Converts a byte array to a String using the UTF32 encoding.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.ToUtf32String(null)                                                   = null
        /// <para/> StringUtils.ToUtf32String(new byte[] { })                                         = ""
        /// <para/> StringUtils.ToUtf32String(new byte[] { 65, 0, 0, 0, 112, 0, 0, 0, 112, 0, 0, 0, 108, 0, 0, 0, 101, 0, 0, 0 })   = "Apple"
        /// </para>
        /// <param name="bytes">input byte array</param>
        /// <param name="throwExceptions">do u want to throw exception? (default false). Pass 'true' to allow exceptions </param>
        /// <returns>Converted string or empty if convertion fails</returns>
        public static string ToUtf32String(byte[] bytes, bool throwExceptions = false)
        {
            if (bytes == null) return null;
            try
            {
                return Encoding.UTF32.GetString(bytes);
            }
            catch (Exception ex)
            {
                if (throwExceptions)
                    throw ex;
                return Empty;
            }

        }

        /// <summary>
        /// Converts a byte array to a String using the UTF7 encoding.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.ToUtf7String(null)                                      = null
        /// <para/> StringUtils.ToUtf7String(new byte[] { })                            = ""
        /// <para/> StringUtils.ToUtf7String(new byte[] { 65, 112, 112, 108, 101 })     = "Apple"
        /// </para>
        /// <param name="bytes">input byte array</param>
        /// <param name="throwExceptions">do u want to throw exception? (default false). Pass 'true' to allow exceptions </param>
        /// <returns>Converted string or empty if convertion fails</returns>
        public static string ToUtf7String(byte[] bytes, bool throwExceptions = false)
        {
            if (bytes == null) return null;
            try
            {
                return Encoding.UTF7.GetString(bytes);
            }
            catch (Exception ex)
            {
                if (throwExceptions)
                    throw ex;
                return Empty;
            }

        }

        /// <summary>
        /// Converts a byte array to a String using the UTF8 encoding.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.ToUtf8String(null)                                      = null
        /// <para/> StringUtils.ToUtf8String(new byte[] { })                            = ""
        /// <para/> StringUtils.ToUtf8String(new byte[] { 65, 112, 112, 108, 101 })     = "Apple"
        /// </para>
        /// <param name="bytes">input byte array</param>
        /// <param name="throwExceptions">do u want to throw exception? (default false). Pass 'true' to allow exceptions </param>
        /// <returns>Converted string or empty if convertion fails</returns>
        public static string ToUtf8String(byte[] bytes, bool throwExceptions = false)
        {
            if (bytes == null) return null;
            try
            {
                return Encoding.UTF8.GetString(bytes);
            }
            catch (Exception ex)
            {
                if (throwExceptions)
                    throw ex;
                return Empty;
            }

        }

        /// <summary>
        /// Converts a byte array to a String using the Default encoding.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.ToDefaultString(null)                                      = null
        /// <para/> StringUtils.ToDefaultString(new byte[] { })                            = ""
        /// <para/> StringUtils.ToDefaultString(new byte[] { 65, 112, 112, 108, 101 })     = "Apple"
        /// </para>
        /// <param name="bytes">input byte array</param>
        /// <param name="throwExceptions">do u want to throw exception? (default false). Pass 'true' to allow exceptions </param>
        /// <returns>Converted string or empty if convertion fails</returns>
        public static string ToDefaultString(byte[] bytes, bool throwExceptions = false)
        {
            if (bytes == null) return null;
            try
            {
                return Encoding.GetEncoding(0).GetString(bytes);
            }
            catch (Exception ex)
            {
                if (throwExceptions)
                    throw ex;
                return Empty;
            }

        }

        /// <summary>
        /// Converts the given string to byte array using the Ascii encoding.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.ToAsciiBytes(null)      = null           
        /// <para/> StringUtils.ToAsciiBytes("")        = new byte[]{}   
        /// <para/> StringUtils.ToAsciiBytes("Apple")   = new byte[] { 65, 112, 112, 108, 101 }
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="throwExceptions">do u want to throw exception? (default false). Pass 'true' to allow exceptions </param>
        /// <returns>Converted byte array or null if conversion fails</returns>
        public static byte[] ToAsciiBytes(string str, bool throwExceptions = false)
        {
            if (str==null) return null;
            try
            {
                return Encoding.ASCII.GetBytes(str);
            }
            catch (Exception)
            {
                if (throwExceptions)
                    throw;
                return null;
            }

        }

        /// <summary>
        /// Converts the given string to byte array using the BigEndianUnicode encoding.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.ToBigEndianUnicodeBytes(null)      = null           
        /// <para/> StringUtils.ToBigEndianUnicodeBytes("")        = new byte[]{}   
        /// <para/> StringUtils.ToBigEndianUnicodeBytes("Apple")   = new byte[] { 0, 65, 0, 112, 0, 112, 0, 108, 0, 101 }
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="throwExceptions">do u want to throw exception? (default false). Pass 'true' to allow exceptions </param>
        /// <returns>Converted byte array or null if conversion fails</returns>
        public static byte[] ToBigEndianUnicodeBytes(string str, bool throwExceptions = false)
        {
            if (str==null) return null;
            try
            {
                return Encoding.BigEndianUnicode.GetBytes(str);
            }
            catch (Exception)
            {
                if (throwExceptions)
                    throw;
                return null;
            }

        }

        /// <summary>
        /// Converts the given string to byte array using the Unicode encoding.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.ToUnicodeBytes(null)      = null           
        /// <para/> StringUtils.ToUnicodeBytes("")        = new byte[]{}   
        /// <para/> StringUtils.ToUnicodeBytes("Apple")   =  new byte[] { 65, 0, 112, 0, 112, 0, 108, 0, 101, 0 }
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="throwExceptions">do u want to throw exception? (default false). Pass 'true' to allow exceptions </param>
        /// <returns>Converted byte array or null if conversion fails</returns>
        public static byte[] ToUnicodeBytes(string str, bool throwExceptions = false)
        {
            if (str==null) return null;
            try
            {
                return Encoding.Unicode.GetBytes(str);
            }
            catch (Exception)
            {
                if (throwExceptions)
                    throw;
                return null;
            }

        }

        /// <summary>
        /// Converts the given string to byte array using the UTF32 encoding.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.ToUtf32Bytes(null)      = null           
        /// <para/> StringUtils.ToUtf32Bytes("")        = new byte[]{}   
        /// <para/> StringUtils.ToUtf32Bytes("Apple")   = new byte[] { 65, 0, 0, 0, 112, 0, 0, 0, 112, 0, 0, 0, 108, 0, 0, 0, 101, 0, 0, 0 }
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="throwExceptions">do u want to throw exception? (default false). Pass 'true' to allow exceptions </param>
        /// <returns>Converted byte array or null if conversion fails</returns>
        public static byte[] ToUtf32Bytes(string str, bool throwExceptions = false)
        {
            if (str==null) return null;
            try
            {
                return Encoding.UTF32.GetBytes(str);
            }
            catch (Exception)
            {
                if (throwExceptions)
                    throw;
                return null;
            }

        }

        /// <summary>
        /// Converts the given string to byte array using the UTF7 encoding.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.ToUtf7Bytes(null)      = null           
        /// <para/> StringUtils.ToUtf7Bytes("")        = new byte[]{}   
        /// <para/> StringUtils.ToUtf7Bytes("Apple")   = new byte[] { 65, 112, 112, 108, 101 }
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="throwExceptions">do u want to throw exception? (default false). Pass 'true' to allow exceptions </param>
        /// <returns>Converted byte array or null if conversion fails</returns>
        public static byte[] ToUtf7Bytes(string str, bool throwExceptions = false)
        {
            if (str==null) return null;
            try
            {
                return Encoding.UTF7.GetBytes(str);
            }
            catch (Exception)
            {
                if (throwExceptions)
                    throw;
                return null;
            }

        }

        /// <summary>
        /// Converts the given string to byte array using the UTF8 encoding.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.ToUtf8Bytes(null)      = null           
        /// <para/> StringUtils.ToUtf8Bytes("")        = new byte[]{}   
        /// <para/> StringUtils.ToUtf8Bytes("Apple")   = new byte[] { 65, 112, 112, 108, 101 }
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="throwExceptions">do u want to throw exception? (default false). Pass 'true' to allow exceptions </param>
        /// <returns>Converted byte array or null if conversion fails</returns>
        public static byte[] ToUtf8Bytes(string str, bool throwExceptions = false)
        {
            if (str==null) return null;
            try
            {
                return Encoding.UTF8.GetBytes(str);
            }
            catch (Exception)
            {
                if (throwExceptions)
                    throw;
                return null;
            }

        }

        /// <summary>
        /// Converts the given string to byte array using the Default encoding.
        /// </summary>
        /// <para>
        /// <para/> StringUtils.ToDefaultBytes(null)      = null           
        /// <para/> StringUtils.ToDefaultBytes("")        = new byte[]{}   
        /// <para/> StringUtils.ToDefaultBytes("Apple")   = new byte[] { 65, 112, 112, 108, 101 }
        /// </para>
        /// <param name="str">input string</param>
        /// <param name="throwExceptions">do u want to throw exception? (default false). Pass 'true' to allow exceptions </param>
        /// <returns>Converted byte array or null if conversion fails</returns>
        public static byte[] ToDefaultBytes(string str, bool throwExceptions = false)
        {
            if (str==null) return null;
            try
            {
                return Encoding.GetEncoding(0).GetBytes(str);
            }
            catch (Exception)
            {
                if (throwExceptions)
                    throw;
                return null;
            }

        }


        #endregion

        #region private methods

        private static int OrdinalIndexOf(string s1, string searchValue, int ordinal, bool lastIndex)
        {
            if (null == searchValue || null == s1 || ordinal <= 0)
                return IndexNotFound;
            if (searchValue.Length == 0)
                return lastIndex ? s1.Length : 0;
            var found = 0;
            var index = lastIndex ? s1.Length : IndexNotFound;
            while (found < ordinal)
            {
                if (lastIndex)
                {
                    index = s1.LastIndexOf(searchValue, index - searchValue.Length, StringComparison.Ordinal);
                }
                else
                {
                    index = s1.IndexOf(searchValue, index + searchValue.Length, StringComparison.Ordinal);
                }
                if (index < 0)
                    return index;
                found++;
            }
            return index;
        }

        private static string[] SplitByWholeSeparatorWorker(string str, string separator, int max, bool preserveAllTokens)
        {
            if (str == null)
            {
                return null;
            }

            var len = str.Length;

            if (len == 0)
            {
                return CollectionUtils.EmptyStringArray;
            }

            if (IsEmpty(separator))
            {
                // Split on whitespace.
                return SplitWorker(str, null, max, preserveAllTokens);
            }

            var separatorLength = separator.Length;

            var substrings = new List<string>();
            var numberOfSubstrings = 0;
            var beg = 0;
            var end = 0;
            while (end < len)
            {
                end = str.IndexOf(separator, beg, StringComparison.Ordinal);

                if (end > IndexNotFound)
                {
                    if (end > beg)
                    {
                        numberOfSubstrings += 1;

                        if (numberOfSubstrings == max)
                        {
                            end = len;
                            substrings.Add(str.Substring(beg));
                        }
                        else
                        {
                            substrings.Add(Substring(str, beg, end - 1));


                            beg = end + separatorLength;
                        }
                    }
                    else
                    {
                        if (preserveAllTokens)
                        {
                            numberOfSubstrings += 1;
                            if (numberOfSubstrings == max)
                            {
                                end = len;
                                substrings.Add(str.Substring(beg));
                            }
                            else
                            {
                                substrings.Add(Empty);
                            }
                        }
                        beg = end + separatorLength;
                    }
                }
                else
                {
                    substrings.Add(str.Substring(beg));
                    end = len;
                }
            }

            return substrings.ToArray();
        }

        private static string[] SplitWorker(string str, char separatorChar, bool preserveAllTokens)
        {
            if (str == null)
            {
                return null;
            }
            var len = str.Length;
            if (len == 0)
            {
                return CollectionUtils.EmptyStringArray;
            }
            var list = new List<String>();
            int i = 0, start = 0;
            var match = false;
            var lastMatch = false;
            while (i < len)
            {
                if (str[i] == separatorChar)
                {
                    if (match || preserveAllTokens)
                    {
                        list.Add(Substring(str, start, i - 1));
                        match = false;
                        lastMatch = true;
                    }
                    start = ++i;
                    continue;
                }
                lastMatch = false;
                match = true;
                i++;
            }
            if (match || preserveAllTokens && lastMatch)
            {
                list.Add(Substring(str, start, i - 1));
            }
            return list.ToArray();
        }

        private static string[] SplitWorker(string str, string separatorChars, int max, bool preserveAllTokens)
        {
            if (str == null)
            {
                return null;
            }
            var len = str.Length;
            if (len == 0)
            {
                return CollectionUtils.EmptyStringArray;
            }
            var list = new List<string>();
            var sizePlus1 = 1;
            int i = 0, start = 0;
            var match = false;
            var lastMatch = false;
            if (separatorChars == null)
            {
                // Null separator means use whitespace
                while (i < len)
                {
                    if (char.IsWhiteSpace(str[i]))
                    {
                        if (match || preserveAllTokens)
                        {
                            lastMatch = true;
                            if (sizePlus1++ == max)
                            {
                                i = len;
                                lastMatch = false;
                            }
                            list.Add(Substring(str, start, i - 1));
                            match = false;
                        }
                        start = ++i;
                        continue;
                    }
                    lastMatch = false;
                    match = true;
                    i++;
                }
            }
            else if (separatorChars.Length == 1)
            {
                // Optimise 1 character case
                var sep = separatorChars[0];
                while (i < len)
                {
                    if (str[i] == sep)
                    {
                        if (match || preserveAllTokens)
                        {
                            lastMatch = true;
                            if (sizePlus1++ == max)
                            {
                                i = len;
                                lastMatch = false;
                            }

                            list.Add(Substring(str, start, i - 1));
                            match = false;
                        }
                        start = ++i;
                        continue;
                    }
                    lastMatch = false;
                    match = true;
                    i++;
                }
            }
            else
            {
                // standard case
                while (i < len)
                {
                    if (separatorChars.IndexOf(str[i]) >= 0)
                    {
                        if (match || preserveAllTokens)
                        {
                            lastMatch = true;
                            if (sizePlus1++ == max)
                            {
                                i = len;
                                lastMatch = false;
                            }

                            list.Add(Substring(str, start, i - 1));
                            match = false;
                        }
                        start = ++i;
                        continue;
                    }
                    lastMatch = false;
                    match = true;
                    i++;
                }
            }
            if (match || preserveAllTokens && lastMatch)
            {

                list.Add(Substring(str, start, i - 1));
            }
            return list.ToArray();
        }

        private static string[] SplitByCharacterType(string str, bool camelCase)
        {
            if (str == null)
            {
                return null;
            }
            if (IsEmpty(str))
            {
                return CollectionUtils.EmptyStringArray;
            }
            var c = str.ToCharArray();
            var list = new List<string>();
            var tokenStart = 0;
            var currentType = CharUnicodeInfo.GetUnicodeCategory(c[tokenStart]);
            for (var pos = tokenStart + 1; pos < c.Length; pos++)
            {
                var type = CharUnicodeInfo.GetUnicodeCategory(c[pos]);
                if (type == currentType)
                {
                    continue;
                }
                if (camelCase && type == UnicodeCategory.LowercaseLetter && currentType == UnicodeCategory.UppercaseLetter)
                {
                    var newTokenStart = pos - 1;
                    if (newTokenStart != tokenStart)
                    {
                        list.Add(new string(c, tokenStart, newTokenStart - tokenStart));
                        tokenStart = newTokenStart;
                    }
                }
                else
                {
                    list.Add(new string(c, tokenStart, pos - tokenStart));
                    tokenStart = pos;
                }
                currentType = type;
            }
            list.Add(new string(c, tokenStart, c.Length - tokenStart));
            return list.ToArray();
        }

        private static string ReplaceEach(string text, IList<string> searchList, IList<string> replacementList, bool repeat, int timeToLive)
        {
            if (text == null || IsEmpty(text) || searchList == null || searchList.Count == 0 || replacementList == null || replacementList.Count == 0)
            {
                return text;
            }

            // if recursing, this shouldn't be less than 0
            if (timeToLive < 0)
            {
                throw new InvalidOperationException("Aborting the operation to avoid stack overflow");
            }

            var searchLength = searchList.Count;
            var replacementLength = replacementList.Count;

            // make sure lengths are ok, these need to be equal
            if (searchLength != replacementLength)
            {
                throw new ArgumentException("Search and Replace array lengths don't match: " + searchLength + " vs " + replacementLength);
            }

            // keep track of which still have matches
            var noMoreMatchesForReplIndex = new bool[searchLength];

            // index on index that the match was found
            var textIndex = -1;
            var replaceIndex = -1;
            int tempIndex;

            // index of replace array that will replace the search string found
            // NOTE: logic duplicated below START
            for (var i = 0; i < searchLength; i++)
            {
                if (noMoreMatchesForReplIndex[i] || searchList[i] == null || IsEmpty(searchList[i]) || replacementList[i] == null)
                {
                    continue;
                }
                tempIndex = text.IndexOf(searchList[i], StringComparison.Ordinal);

                // see if we need to keep searching for this
                if (tempIndex == -1)
                {
                    noMoreMatchesForReplIndex[i] = true;
                }
                else
                {
                    if (textIndex == -1 || tempIndex < textIndex)
                    {
                        textIndex = tempIndex;
                        replaceIndex = i;
                    }
                }
            }
            // NOTE: logic mostly below END

            // no search strings found, we are done
            if (textIndex == -1)
            {
                return text;
            }

            var start = 0;

            var buf = new StringBuilder();

            while (textIndex != -1)
            {
                for (var i = start; i < textIndex; i++)
                {
                    buf.Append(text[i]);
                }
                buf.Append(replacementList[replaceIndex]);

                start = textIndex + searchList[replaceIndex].Length;

                textIndex = -1;
                replaceIndex = -1;
                tempIndex = -1;
                // find the next earliest match
                // NOTE: logic mostly duplicated above START
                for (var i = 0; i < searchLength; i++)
                {
                    if (noMoreMatchesForReplIndex[i] || searchList[i] == null || IsEmpty(searchList[i]) || replacementList[i] == null)
                    {
                        continue;
                    }
                    tempIndex = text.IndexOf(searchList[i], start, StringComparison.Ordinal);

                    // see if we need to keep searching for this
                    if (tempIndex == -1)
                    {
                        noMoreMatchesForReplIndex[i] = true;
                    }
                    else
                    {
                        if (textIndex == -1 || tempIndex < textIndex)
                        {
                            textIndex = tempIndex;
                            replaceIndex = i;
                        }
                    }
                }
                // NOTE: logic duplicated above END
            }
            var textLength = text.Length;
            for (var i = start; i < textLength; i++)
            {
                buf.Append(text[i]);
            }
            var result = buf.ToString();
            if (!repeat)
            {
                return result;
            }

            return ReplaceEach(result, searchList, replacementList, repeat, timeToLive - 1);
        }

        private static int CommonPrefixLength(string first, string second)
        {
            var result = GetCommonPrefix(first, second).Length;

            // Limit the result to 4.
            return result > 4 ? 4 : result;
        }

        #endregion
    }
}
