using System;
using NUnit.Framework;

namespace Ultimate.Utilities.Tests
{
    [TestFixture]
    public class StringUtilsTests
    {
        private string[] _emptyCollection;
        private readonly string[] _nullStrings = null;
        private readonly char[] _nullChars = null;

        private const string MatchEmailPattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@" + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\." + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|" + @"([a-zA-Z0-9]+[\w-]+\.)+[a-zA-Z]{1}[a-zA-Z0-9-]{1,23})$";

        [OneTimeSetUp]
        protected void OnSetup()
        {
            _emptyCollection = new string[] { };
        }

        #region Empty, Blank & Trim Tests

        [Test]
        public void TestIsEmpty()
        {
            Assert.AreEqual(StringUtils.IsEmpty(null), true);
            Assert.AreEqual(StringUtils.IsEmpty(""), true);
            Assert.AreEqual(StringUtils.IsEmpty(" "), false);
            Assert.AreEqual(StringUtils.IsEmpty("abc"), false);
            Assert.AreEqual(StringUtils.IsEmpty("  abc  "), false);
        }

        [Test]
        public void TestIsNotEmpty()
        {
            Assert.AreEqual(StringUtils.IsNotEmpty(null), false);
            Assert.AreEqual(StringUtils.IsNotEmpty(""), false);
            Assert.AreEqual(StringUtils.IsNotEmpty(" "), true);
            Assert.AreEqual(StringUtils.IsNotEmpty("abc"), true);
            Assert.AreEqual(StringUtils.IsNotEmpty("  abc  "), true);
        }

        [Test]
        public void TestIsAnyEmpty()
        {
            Assert.AreEqual(StringUtils.IsAnyEmpty(null), true);
            Assert.AreEqual(StringUtils.IsAnyEmpty(null, "abc"), true);
            Assert.AreEqual(StringUtils.IsAnyEmpty("", "xyz"), true);
            Assert.AreEqual(StringUtils.IsAnyEmpty("abc", ""), true);
            Assert.AreEqual(StringUtils.IsAnyEmpty("  abc  ", null), true);
            Assert.AreEqual(StringUtils.IsAnyEmpty(" ", "xyz"), false);
            Assert.AreEqual(StringUtils.IsAnyEmpty("abc", "xyz"), false);
        }

        [Test]
        public void TestIsNoneEmpty()
        {
            Assert.AreEqual(StringUtils.IsNoneEmpty(null), false);
            Assert.AreEqual(StringUtils.IsNoneEmpty(null, "abc"), false);
            Assert.AreEqual(StringUtils.IsNoneEmpty("", "xyz"), false);
            Assert.AreEqual(StringUtils.IsNoneEmpty("abc", ""), false);
            Assert.AreEqual(StringUtils.IsNoneEmpty("  abc  ", null), false);
            Assert.AreEqual(StringUtils.IsNoneEmpty(" ", "xyz"), true);
            Assert.AreEqual(StringUtils.IsNoneEmpty("abc", "xyz"), true);
        }

        [Test]
        public void TestIsBlank()
        {
            Assert.AreEqual(StringUtils.IsBlank(null), true);
            Assert.AreEqual(StringUtils.IsBlank(""), true);
            Assert.AreEqual(StringUtils.IsBlank(" "), true);
            Assert.AreEqual(StringUtils.IsBlank("abc"), false);
            Assert.AreEqual(StringUtils.IsBlank("  abc  "), false);
        }

        [Test]
        public void TestIsNotBlank()
        {
            Assert.AreEqual(StringUtils.IsNotBlank(null), false);
            Assert.AreEqual(StringUtils.IsNotBlank(""), false);
            Assert.AreEqual(StringUtils.IsNotBlank(" "), false);
            Assert.AreEqual(StringUtils.IsNotBlank("abc"), true);
            Assert.AreEqual(StringUtils.IsNotBlank("  abc  "), true);
        }

        [Test]
        public void TestIsAnyBlank()
        {
            Assert.AreEqual(StringUtils.IsAnyBlank(null), true);
            Assert.AreEqual(StringUtils.IsAnyBlank(null, "abc"), true);
            Assert.AreEqual(StringUtils.IsAnyBlank(null, null), true);
            Assert.AreEqual(StringUtils.IsAnyBlank("", "xyz"), true);
            Assert.AreEqual(StringUtils.IsAnyBlank("abc", ""), true);
            Assert.AreEqual(StringUtils.IsAnyBlank("  abc  ", null), true);
            Assert.AreEqual(StringUtils.IsAnyBlank(" ", "xyz"), true);
            Assert.AreEqual(StringUtils.IsAnyBlank("abc", "xyz"), false);
        }

        [Test]
        public void TestIsNoneBlank()
        {
            Assert.AreEqual(StringUtils.IsNoneBlank(null), false);
            Assert.AreEqual(StringUtils.IsNoneBlank(null, "abc"), false);
            Assert.AreEqual(StringUtils.IsNoneBlank(null, null), false);
            Assert.AreEqual(StringUtils.IsNoneBlank("", "xyz"), false);
            Assert.AreEqual(StringUtils.IsNoneBlank("abc", ""), false);
            Assert.AreEqual(StringUtils.IsNoneBlank("  abc  ", null), false);
            Assert.AreEqual(StringUtils.IsNoneBlank(" ", "xyz"), false);
            Assert.AreEqual(StringUtils.IsNoneBlank("abc", "xyz"), true);
        }

        [Test]
        public void TestTrim()
        {
            Assert.AreEqual(StringUtils.Trim(null), null);
            Assert.AreEqual(StringUtils.Trim(""), "");
            Assert.AreEqual(StringUtils.Trim("     "), "");
            Assert.AreEqual(StringUtils.Trim("abc"), "abc");
            Assert.AreEqual(StringUtils.Trim("    abc    "), "abc");
        }

        [Test]
        public void TestTrimToNull()
        {
            Assert.AreEqual(StringUtils.TrimToNull(null), null);
            Assert.AreEqual(StringUtils.TrimToNull(""), null);
            Assert.AreEqual(StringUtils.TrimToNull("     "), null);
            Assert.AreEqual(StringUtils.TrimToNull("abc"), "abc");
            Assert.AreEqual(StringUtils.TrimToNull("    abc    "), "abc");
        }

        [Test]
        public void TestTrimToEmpty()
        {
            Assert.AreEqual(StringUtils.TrimToEmpty(null), "");
            Assert.AreEqual(StringUtils.TrimToEmpty(""), "");
            Assert.AreEqual(StringUtils.TrimToEmpty("     "), "");
            Assert.AreEqual(StringUtils.TrimToEmpty("abc"), "abc");
            Assert.AreEqual(StringUtils.TrimToEmpty("    abc    "), "abc");
        }


        #endregion

        #region Strip Tests

        [Test]
        public void TestStripStart()
        {
            Assert.AreEqual(StringUtils.StripStart(null, "test"), null);
            Assert.AreEqual(StringUtils.StripStart("", "test"), "");
            Assert.AreEqual(StringUtils.StripStart("abc", ""), "abc");
            Assert.AreEqual(StringUtils.StripStart("abc"), "abc");
            Assert.AreEqual(StringUtils.StripStart("  abc"), "abc");
            Assert.AreEqual(StringUtils.StripStart("abc  "), "abc  ");
            Assert.AreEqual(StringUtils.StripStart(" abc "), "abc ");
            Assert.AreEqual(StringUtils.StripStart("yxabc  ", "xyz"), "abc  ");
        }

        [Test]
        public void TestStripEnd()
        {
            Assert.AreEqual(StringUtils.StripEnd(null, "test"), null);
            Assert.AreEqual(StringUtils.StripEnd("", "test"), "");
            Assert.AreEqual(StringUtils.StripEnd("abc", ""), "abc");
            Assert.AreEqual(StringUtils.StripEnd("abc"), "abc");
            Assert.AreEqual(StringUtils.StripEnd("  abc"), "  abc");
            Assert.AreEqual(StringUtils.StripEnd("abc  "), "abc");
            Assert.AreEqual(StringUtils.StripEnd(" abc "), " abc");
            Assert.AreEqual(StringUtils.StripEnd("  abcyx", "xyz"), "  abc");
            Assert.AreEqual(StringUtils.StripEnd("199.00", ".0"), "199");
        }

        [Test]
        public void TestStrip()
        {
            Assert.AreEqual(StringUtils.Strip(null), null);
            Assert.AreEqual(StringUtils.Strip(""), "");
            Assert.AreEqual(StringUtils.Strip("   "), "");
            Assert.AreEqual(StringUtils.Strip("abc"), "abc");
            Assert.AreEqual(StringUtils.Strip("  abc"), "abc");
            Assert.AreEqual(StringUtils.Strip("abc  "), "abc");
            Assert.AreEqual(StringUtils.Strip(" abc "), "abc");
            Assert.AreEqual(StringUtils.Strip(" ab c "), "ab c");
        }

        [Test]
        public void TestStripToNull()
        {
            Assert.AreEqual(StringUtils.StripToNull(null), null);
            Assert.AreEqual(StringUtils.StripToNull(""), null);
            Assert.AreEqual(StringUtils.StripToNull("   "), null);
            Assert.AreEqual(StringUtils.StripToNull("abc"), "abc");
            Assert.AreEqual(StringUtils.StripToNull("  abc"), "abc");
            Assert.AreEqual(StringUtils.StripToNull("abc  "), "abc");
            Assert.AreEqual(StringUtils.StripToNull(" abc "), "abc");
            Assert.AreEqual(StringUtils.StripToNull(" ab c "), "ab c");
        }

        [Test]
        public void TestStripToEmpty()
        {
            Assert.AreEqual(StringUtils.StripToEmpty(null), "");
            Assert.AreEqual(StringUtils.StripToEmpty(""), "");
            Assert.AreEqual(StringUtils.StripToEmpty("   "), "");
            Assert.AreEqual(StringUtils.StripToEmpty("abc"), "abc");
            Assert.AreEqual(StringUtils.StripToEmpty("  abc"), "abc");
            Assert.AreEqual(StringUtils.StripToEmpty("abc  "), "abc");
            Assert.AreEqual(StringUtils.StripToEmpty(" abc "), "abc");
            Assert.AreEqual(StringUtils.StripToEmpty(" ab c "), "ab c");
        }

        [Test]
        public void TestStripAll()
        {
            Assert.AreEqual(StringUtils.StripAll(null), null);
            Assert.AreEqual(StringUtils.StripAll(_emptyCollection), _emptyCollection);
            Assert.AreEqual(StringUtils.StripAll("abc", "  abc"), new[] { "abc", "abc" });
            Assert.AreEqual(StringUtils.StripAll("abc  ", null), new[] { "abc", null });
            Assert.AreEqual(StringUtils.StripAll(new[] { "xyz", "  abc" }), new[] { "xyz", "abc" });
        }


        [Test]
        public void TestStripAllWithStripchar()
        {
            Assert.AreEqual(StringUtils.StripAll(null, stripChars: "test"), null);
            Assert.AreEqual(StringUtils.StripAll(_emptyCollection, "test"), _emptyCollection);
            Assert.AreEqual(StringUtils.StripAll(new[] { "abc", "  abc" }, null), new[] { "abc", "abc" });
            Assert.AreEqual(StringUtils.StripAll(new[] { "abc  ", null }, null), new[] { "abc", null });
            Assert.AreEqual(StringUtils.StripAll(new[] { "abc  ", null }, "yz"), new[] { "abc  ", null });
            Assert.AreEqual(StringUtils.StripAll(new[] { "yabcz", null }, "yz"), new[] { "abc", null });
        }

        [Test]
        public void TestStripAccents()
        {
            Assert.AreEqual(StringUtils.StripAccents(null), null);
            Assert.AreEqual(StringUtils.StripAccents(""), "");
            Assert.AreEqual(StringUtils.StripAccents("control"), "control");
            Assert.AreEqual(StringUtils.StripAccents("Šatish"), "Satish");
        }


        #endregion

        #region Comparisons and Indexes Tests

        [Test]
        public void TestEquals()
        {
            Assert.AreEqual(StringUtils.Equals(null, null), true);
            Assert.AreEqual(StringUtils.Equals(null, "abc"), false);
            Assert.AreEqual(StringUtils.Equals("abc", null), false);
            Assert.AreEqual(StringUtils.Equals("abc", "abc"), true);
            Assert.AreEqual(StringUtils.Equals("abc", "ABC"), false);
        }

        [Test]
        public void TestEqualsIgnoreCase()
        {
            Assert.AreEqual(StringUtils.EqualsIgnoreCase(null, null), true);
            Assert.AreEqual(StringUtils.EqualsIgnoreCase(null, "abc"), false);
            Assert.AreEqual(StringUtils.EqualsIgnoreCase("abc", null), false);
            Assert.AreEqual(StringUtils.EqualsIgnoreCase("abc", "abc"), true);
            Assert.AreEqual(StringUtils.EqualsIgnoreCase("abc", "ABC"), true);
        }

        [Test]
        public void TestIndexOf()
        {
            Assert.AreEqual(StringUtils.IndexOf(null, "test"), -1);
            Assert.AreEqual(StringUtils.IndexOf("", "test"), -1);
            Assert.AreEqual(StringUtils.IndexOf("aabaabaa", 'a'), 0);
            Assert.AreEqual(StringUtils.IndexOf("aabaabaa", 'b'), 2);
            Assert.AreEqual(StringUtils.IndexOf("aabaabaa", 'b', 3), 5);
        }

        [Test]
        public void TestIndexOfWithStartPos()
        {
            Assert.AreEqual(StringUtils.IndexOf(null, "test"), -1);
            Assert.AreEqual(StringUtils.IndexOf("test", null), -1);
            Assert.AreEqual(StringUtils.IndexOf("", ""), 0);
            Assert.AreEqual(StringUtils.IndexOf("", "a"), -1);
            Assert.AreEqual(StringUtils.IndexOf("aabaabaa", "a"), 0);
            Assert.AreEqual(StringUtils.IndexOf("aabaabaa", "b"), 2);
            Assert.AreEqual(StringUtils.IndexOf("aabaabaa", "ab"), 1);
            Assert.AreEqual(StringUtils.IndexOf("aabaabaa", ""), 0);
            Assert.AreEqual(StringUtils.IndexOf("aabaabaa", "b", 3), 5);
        }

        [Test]
        public void TestIndexOfIgnoreCase()
        {
            Assert.AreEqual(StringUtils.IndexOfIgnoreCase(null, "test"), -1);
            Assert.AreEqual(StringUtils.IndexOfIgnoreCase("test", null), -1);
            Assert.AreEqual(StringUtils.IndexOfIgnoreCase("", ""), 0);
            Assert.AreEqual(StringUtils.IndexOfIgnoreCase("aabaabaa", "A"), 0);
            Assert.AreEqual(StringUtils.IndexOfIgnoreCase("aabaabaa", "B"), 2);
            Assert.AreEqual(StringUtils.IndexOfIgnoreCase("aabaabaa", "ab"), 1);
            Assert.AreEqual(StringUtils.IndexOfIgnoreCase("aabaabaa", "B", 3), 5);
            Assert.AreEqual(StringUtils.IndexOfIgnoreCase("aabaabaa", "a", 9), -1);
        }

        [Test]
        public void TestIndexOfIgnoreCaseWithChar()
        {
            Assert.AreEqual(StringUtils.IndexOfIgnoreCase(null, 'A'), -1);
            Assert.AreEqual(StringUtils.IndexOfIgnoreCase("", 'A'), -1);
            Assert.AreEqual(StringUtils.IndexOfIgnoreCase("aabaabaa", 'A'), 0);
            Assert.AreEqual(StringUtils.IndexOfIgnoreCase("aabaabaa", 'b'), 2);
            Assert.AreEqual(StringUtils.IndexOfIgnoreCase("aabaabaa", 'B', 3), 5);
        }

        [Test]
        public void TestLastIndexOf()
        {
            Assert.AreEqual(StringUtils.LastIndexOf(null, "test"), -1);
            Assert.AreEqual(StringUtils.LastIndexOf("", "test"), -1);
            Assert.AreEqual(StringUtils.LastIndexOf("aabaabaa", "a"), 7);
            Assert.AreEqual(StringUtils.LastIndexOf("aabaabaa", "ab"), 4);
            Assert.AreEqual(StringUtils.LastIndexOf("aabaabaa", "b", 8), 5);
            Assert.AreEqual(StringUtils.LastIndexOf("aabaabaa", "a", 9), -1);
        }

        [Test]
        public void TestLastIndexOfWithChar()
        {
            Assert.AreEqual(StringUtils.LastIndexOf(null, 'a'), -1);
            Assert.AreEqual(StringUtils.LastIndexOf("", 'a'), -1);
            Assert.AreEqual(StringUtils.LastIndexOf("aabaabaa", 'a'), 7);
            Assert.AreEqual(StringUtils.LastIndexOf("aabaabaa", 'b', 0), -1);
            Assert.AreEqual(StringUtils.LastIndexOf("aabaabaa", 'b', 7), 5);
            Assert.AreEqual(StringUtils.LastIndexOf("aabaabaa", 'b', 9), -1);
        }

        [Test]
        public void TestLastIndexOfIgnoreCase()
        {
            Assert.AreEqual(StringUtils.LastIndexOfIgnoreCase(null, "test"), -1);
            Assert.AreEqual(StringUtils.LastIndexOfIgnoreCase("", "test"), -1);
            Assert.AreEqual(StringUtils.LastIndexOfIgnoreCase("aabaabaa", "A"), 7);
            Assert.AreEqual(StringUtils.LastIndexOfIgnoreCase("aabaabaa", "ab"), 4);
            Assert.AreEqual(StringUtils.LastIndexOfIgnoreCase("aabaabaa", "B", 4), 2);
            Assert.AreEqual(StringUtils.LastIndexOfIgnoreCase("aabaabaa", "b", 7), 5);
            Assert.AreEqual(StringUtils.LastIndexOfIgnoreCase("aabaabaa", "b", 9), -1);
        }

        [Test]
        public void TestLastIndexOfIgnoreCaseWithStartPos()
        {
            Assert.AreEqual(StringUtils.LastIndexOfIgnoreCase(null, 'A'), -1);
            Assert.AreEqual(StringUtils.LastIndexOfIgnoreCase("", 'A'), -1);
            Assert.AreEqual(StringUtils.LastIndexOfIgnoreCase("aabaabaa", 'A'), 7);
            Assert.AreEqual(StringUtils.LastIndexOfIgnoreCase("aabaabaa", 'b', 0), -1);
            Assert.AreEqual(StringUtils.LastIndexOfIgnoreCase("aabaabaa", 'B', 7), 5);
            Assert.AreEqual(StringUtils.LastIndexOfIgnoreCase("aabaabaa", 'B', 9), -1);
        }

        [Test]
        public void TestOrdinalIndexOf()
        {
            Assert.AreEqual(StringUtils.OrdinalIndexOf(null, "test", 1), -1);
            Assert.AreEqual(StringUtils.OrdinalIndexOf("test", null, 1), -1);
            Assert.AreEqual(StringUtils.OrdinalIndexOf("", "", 1), 0);
            Assert.AreEqual(StringUtils.OrdinalIndexOf("aabaabaa", "a", 1), 0);
            Assert.AreEqual(StringUtils.OrdinalIndexOf("aabaabaa", "a", 2), 1);
            Assert.AreEqual(StringUtils.OrdinalIndexOf("aabaabaa", "b", 1), 2);
            Assert.AreEqual(StringUtils.OrdinalIndexOf("aabaabaa", "b", 2), 5);
            Assert.AreEqual(StringUtils.OrdinalIndexOf("aabaabaa", "ab", 1), 1);
            Assert.AreEqual(StringUtils.OrdinalIndexOf("aabaabaa", "ab", 2), 4);
            Assert.AreEqual(StringUtils.OrdinalIndexOf("aabaabaa", "", 1), 0);
            Assert.AreEqual(StringUtils.OrdinalIndexOf("aabaabaa", "", 2), 0);
        }

        [Test]
        public void TestOrdinalIndexOfWithChar()
        {
            Assert.AreEqual(StringUtils.OrdinalIndexOf(null, 'a', 1), -1);
            Assert.AreEqual(StringUtils.OrdinalIndexOf("aabaabaa", 'a', 1), 0);
            Assert.AreEqual(StringUtils.OrdinalIndexOf("aabaabaa", 'a', 2), 1);
            Assert.AreEqual(StringUtils.OrdinalIndexOf("aabaabaa", 'b', 1), 2);
            Assert.AreEqual(StringUtils.OrdinalIndexOf("aabaabaa", 'b', 2), 5);
        }

        [Test]
        public void TestLastOrdinalIndexOf()
        {
            Assert.AreEqual(StringUtils.LastOrdinalIndexOf(null, "test", 1), -1);
            Assert.AreEqual(StringUtils.LastOrdinalIndexOf("test", null, 1), -1);
            Assert.AreEqual(StringUtils.LastOrdinalIndexOf("", "", 1), 0);
            Assert.AreEqual(StringUtils.LastOrdinalIndexOf("aabaabaa", "a", 1), 7);
            Assert.AreEqual(StringUtils.LastOrdinalIndexOf("aabaabaa", "a", 2), 6);
            Assert.AreEqual(StringUtils.LastOrdinalIndexOf("aabaabaa", "b", 1), 5);
            Assert.AreEqual(StringUtils.LastOrdinalIndexOf("aabaabaa", "b", 2), 2);
            Assert.AreEqual(StringUtils.LastOrdinalIndexOf("aabaabaa", "ab", 1), 4);
            Assert.AreEqual(StringUtils.LastOrdinalIndexOf("aabaabaa", "ab", 2), 1);
            Assert.AreEqual(StringUtils.LastOrdinalIndexOf("aabaabaa", "", 1), 8);
            Assert.AreEqual(StringUtils.LastOrdinalIndexOf("aabaabaa", "", 2), 8);
        }

        [Test]
        public void TestLastOrdinalIndexOfWithChar()
        {
            Assert.AreEqual(StringUtils.LastOrdinalIndexOf(null, 'a', 1), -1);
            Assert.AreEqual(StringUtils.LastOrdinalIndexOf("test", null, 1), -1);
            Assert.AreEqual(StringUtils.LastOrdinalIndexOf("aabaabaa", 'a', 1), 7);
            Assert.AreEqual(StringUtils.LastOrdinalIndexOf("aabaabaa", 'a', 2), 6);
            Assert.AreEqual(StringUtils.LastOrdinalIndexOf("aabaabaa", 'b', 1), 5);
            Assert.AreEqual(StringUtils.LastOrdinalIndexOf("aabaabaa", 'b', 2), 2);
        }

        [Test]
        public void TestContains()
        {
            Assert.AreEqual(StringUtils.Contains(null, "test"), false);
            Assert.AreEqual(StringUtils.Contains("test", null), false);
            Assert.AreEqual(StringUtils.Contains("", ""), true);
            Assert.AreEqual(StringUtils.Contains("abc", ""), true);
            Assert.AreEqual(StringUtils.Contains("abc", "a"), true);
            Assert.AreEqual(StringUtils.Contains("abc", "z"), false);
        }

        [Test]
        public void TestContainsChar()
        {
            Assert.AreEqual(StringUtils.Contains(null, "test"), false);
            Assert.AreEqual(StringUtils.Contains("", "test"), false);
            Assert.AreEqual(StringUtils.Contains("abc", 'a'), true);
            Assert.AreEqual(StringUtils.Contains("abc", 'z'), false);
        }

        [Test]
        public void TestContainsIgnoreCase()
        {
            Assert.AreEqual(StringUtils.ContainsIgnoreCase(null, "test"), false);
            Assert.AreEqual(StringUtils.ContainsIgnoreCase("test", null), false);
            Assert.AreEqual(StringUtils.ContainsIgnoreCase("", ""), true);
            Assert.AreEqual(StringUtils.ContainsIgnoreCase("abc", ""), true);
            Assert.AreEqual(StringUtils.ContainsIgnoreCase("abc", "A"), true);
            Assert.AreEqual(StringUtils.ContainsIgnoreCase("abc", "z"), false);
        }

        [Test]
        public void TestContainsIgnoreCaseChar()
        {
            Assert.AreEqual(StringUtils.ContainsIgnoreCase(null, "test"), false);
            Assert.AreEqual(StringUtils.ContainsIgnoreCase("", "test"), false);
            Assert.AreEqual(StringUtils.ContainsIgnoreCase("abc", 'A'), true);
            Assert.AreEqual(StringUtils.ContainsIgnoreCase("abc", 'z'), false);
        }

        [Test]
        public void TestContainsWhiteSpace()
        {
            Assert.AreEqual(StringUtils.ContainsWhiteSpace(null), false);
            Assert.AreEqual(StringUtils.ContainsWhiteSpace(""), false);
            Assert.AreEqual(StringUtils.ContainsWhiteSpace("abc "), true);
            Assert.AreEqual(StringUtils.ContainsWhiteSpace("a bc"), true);
            Assert.AreEqual(StringUtils.ContainsWhiteSpace("abc"), false);
        }

        [Test]
        public void TestIndexOfAny()
        {
            Assert.AreEqual(StringUtils.IndexOfAny(null, "test"), -1);
            Assert.AreEqual(StringUtils.IndexOfAny("", "test"), -1);
            Assert.AreEqual(StringUtils.IndexOfAny("test", _nullChars), -1);
            Assert.AreEqual(StringUtils.IndexOfAny("test", _emptyCollection), -1);
            Assert.AreEqual(StringUtils.IndexOfAny("zzabyycdxx", 'z', 'a'), 0);
            Assert.AreEqual(StringUtils.IndexOfAny("zzabyycdxx", new[] { 'b', 'y' }), 3);
            Assert.AreEqual(StringUtils.IndexOfAny("aba", 'z'), -1);
        }

        [Test]
        public void TestIndexOfAnyWithString()
        {
            Assert.AreEqual(StringUtils.IndexOfAny(null, "test"), -1);
            Assert.AreEqual(StringUtils.IndexOfAny("", "test"), -1);
            Assert.AreEqual(StringUtils.IndexOfAny("test", (string) null), -1);
            Assert.AreEqual(StringUtils.IndexOfAny("zzabyycdxx", "za"), 0);
            Assert.AreEqual(StringUtils.IndexOfAny("zzabyycdxx", "by"), 3);
            Assert.AreEqual(StringUtils.IndexOfAny("aba", "z"), -1);
        }

        [Test]
        public void TestContainsAny()
        {
            Assert.AreEqual(StringUtils.ContainsAny(null, "test"), false);
            Assert.AreEqual(StringUtils.ContainsAny("", "test"), false);
            Assert.AreEqual(StringUtils.ContainsAny("test", _nullChars), false);
            Assert.AreEqual(StringUtils.ContainsAny("test", _emptyCollection), false);
            Assert.AreEqual(StringUtils.ContainsAny("zzabyycdxx", 'z', 'a'), true);
            Assert.AreEqual(StringUtils.ContainsAny("zzabyycdxx", 'b', 'y'), true);
            Assert.AreEqual(StringUtils.ContainsAny("zzabyycdxx", 'z', 'y'), true);
            Assert.AreEqual(StringUtils.ContainsAny("aba", 'z'), false);
        }

        [Test]
        public void TestContainsAnyWithString()
        {

            Assert.AreEqual(StringUtils.ContainsAny(null, "test"), false);
            Assert.AreEqual(StringUtils.ContainsAny("", "test"), false);
            Assert.AreEqual(StringUtils.ContainsAny("test", _nullChars), false);
            Assert.AreEqual(StringUtils.ContainsAny("test", _emptyCollection), false);
            Assert.AreEqual(StringUtils.ContainsAny("abcd", "ab", null), true);
            Assert.AreEqual(StringUtils.ContainsAny("abcd", "ab", "cd"), true);
            Assert.AreEqual(StringUtils.ContainsAny("abc", "d", "abc"), true);
        }

        [Test]
        public void TestIndexOfAnyBut()
        {
            Assert.AreEqual(StringUtils.IndexOfAnyBut(null, "test"), -1);
            Assert.AreEqual(StringUtils.IndexOfAnyBut("", "test"), -1);
            Assert.AreEqual(StringUtils.IndexOfAnyBut("test", _nullChars), -1);
            Assert.AreEqual(StringUtils.IndexOfAnyBut("test", new char[] { }), -1);
            Assert.AreEqual(StringUtils.IndexOfAnyBut("zzabyycdxx", 'z', 'a'), 3);
            Assert.AreEqual(StringUtils.IndexOfAnyBut("aba", 'z'), 0);
            Assert.AreEqual(StringUtils.IndexOfAnyBut("aba", new[] { 'a', 'b' }), -1);
        }

        [Test]
        public void TestIndexOfAnyButWithString()
        {
            Assert.AreEqual(StringUtils.IndexOfAnyBut(null, "test"), -1);
            Assert.AreEqual(StringUtils.IndexOfAnyBut("", "test"), -1);
            Assert.AreEqual(StringUtils.IndexOfAnyBut("test", (string) null), -1);
            Assert.AreEqual(StringUtils.IndexOfAnyBut("test", ""), -1);
            Assert.AreEqual(StringUtils.IndexOfAnyBut("zzabyycdxx", "za"), 3);
            Assert.AreEqual(StringUtils.IndexOfAnyBut("zzabyycdxx", ""), -1);
            Assert.AreEqual(StringUtils.IndexOfAnyBut("aba", "ab"), -1);
        }
        [Test]
        public void TestContainsOnly()
        {
            Assert.AreEqual(StringUtils.ContainsOnly(null, 'a'), false);
            Assert.AreEqual(StringUtils.ContainsOnly("test", _nullChars), false);
            Assert.AreEqual(StringUtils.ContainsOnly("", 'a'), true);
            Assert.AreEqual(StringUtils.ContainsOnly("ab", 'a'), false);
            Assert.AreEqual(StringUtils.ContainsOnly("abab", 'a', 'b', 'c'), true);
            Assert.AreEqual(StringUtils.ContainsOnly("ab1", 'a', 'b', 'c'), false);
            Assert.AreEqual(StringUtils.ContainsOnly("abz", new[] { 'a', 'b', 'c' }), false);
        }

        [Test]
        public void TestContainsOnlyWithString()
        {
            Assert.AreEqual(StringUtils.ContainsOnly(null, 'a'), false);
            Assert.AreEqual(StringUtils.ContainsOnly("test", _nullChars), false);
            Assert.AreEqual(StringUtils.ContainsOnly("", 'a'), true);
            Assert.AreEqual(StringUtils.ContainsOnly("ab", ""), false);
            Assert.AreEqual(StringUtils.ContainsOnly("abab", "abc"), true);
            Assert.AreEqual(StringUtils.ContainsOnly("ab1", "abc"), false);
            Assert.AreEqual(StringUtils.ContainsOnly("abz", "abc"), false);
        }

        [Test]
        public void TestContainsNone()
        {
            Assert.AreEqual(StringUtils.ContainsNone(null, 'a'), true);
            Assert.AreEqual(StringUtils.ContainsNone("test", _nullChars), true);
            Assert.AreEqual(StringUtils.ContainsNone("", 'a'), true);
            Assert.AreEqual(StringUtils.ContainsNone("ab", 'a'), false);
            Assert.AreEqual(StringUtils.ContainsNone("abab", 'x', 'y', 'z'), true);
            Assert.AreEqual(StringUtils.ContainsNone("ab1", 'x', 'y', 'z'), true);
            Assert.AreEqual(StringUtils.ContainsNone("abz", new[] { 'x', 'y', 'z' }), false);
        }

        [Test]
        public void TestContainsNoneWithString()
        {
            Assert.AreEqual(StringUtils.ContainsNone(null, "a"), true);
            Assert.AreEqual(StringUtils.ContainsNone("test", (string) null), true);
            Assert.AreEqual(StringUtils.ContainsNone("", "a"), true);
            Assert.AreEqual(StringUtils.ContainsNone("ab", ""), true);
            Assert.AreEqual(StringUtils.ContainsNone("abab", "xyz"), true);
            Assert.AreEqual(StringUtils.ContainsNone("ab1", "xyz"), true);
            Assert.AreEqual(StringUtils.ContainsNone("abz", "xyz"), false);
        }
        [Test]
        public void TestIndexOfAnyWithStrings()
        {
            Assert.AreEqual(StringUtils.IndexOfAny(null, "a"), -1);
            Assert.AreEqual(StringUtils.IndexOfAny("test", _nullStrings), -1);
            Assert.AreEqual(StringUtils.IndexOfAny("test", _emptyCollection), -1);
            Assert.AreEqual(StringUtils.IndexOfAny("zzabyycdxx", "ab", "cd"), 2);
            Assert.AreEqual(StringUtils.IndexOfAny("zzabyycdxx", "cd", "ab"), 2);
            Assert.AreEqual(StringUtils.IndexOfAny("zzabyycdxx", "mn", "op"), -1);
            Assert.AreEqual(StringUtils.IndexOfAny("zzabyycdxx", "zab", "aby"), 1);
            Assert.AreEqual(StringUtils.IndexOfAny("zzabyycdxx", new[] { "" }), 0);
            Assert.AreEqual(StringUtils.IndexOfAny("", new[] { "" }), 0);
            Assert.AreEqual(StringUtils.IndexOfAny("", "a"), -1);
        }

        [Test]
        public void TestLastIndexOfAnyWithStrings()
        {
            Assert.AreEqual(StringUtils.LastIndexOfAny(null, "a"), -1);
            Assert.AreEqual(StringUtils.LastIndexOfAny("test", null), -1);
            Assert.AreEqual(StringUtils.LastIndexOfAny("test", _emptyCollection), -1);
            Assert.AreEqual(StringUtils.LastIndexOfAny("test", new string[] { null }), -1);
            Assert.AreEqual(StringUtils.LastIndexOfAny("zzabyycdxx", "ab", "cd"), 6);
            Assert.AreEqual(StringUtils.LastIndexOfAny("zzabyycdxx", "cd", "ab"), 6);
            Assert.AreEqual(StringUtils.LastIndexOfAny("zzabyycdxx", "mn", "op"), -1);
            Assert.AreEqual(StringUtils.LastIndexOfAny("zzabyycdxx", new[] { "mn", "op" }), -1);
            Assert.AreEqual(StringUtils.LastIndexOfAny("zzabyycdxx", new[] { "mn", "" }), 9);
            Assert.AreEqual(StringUtils.LastIndexOfAny("zzabyycdxx", new[] { "x", "d" }), 9);
        }



        #endregion

        #region Substring

        [Test]
        public void TestSubstring()
        {
            Assert.AreEqual(StringUtils.Substring(null, 1), null);
            Assert.AreEqual(StringUtils.Substring("", 1), "");
            Assert.AreEqual(StringUtils.Substring("abc", 0), "abc");
            Assert.AreEqual(StringUtils.Substring("abc", 2), "c");
            Assert.AreEqual(StringUtils.Substring("abc", 4), "");
            Assert.AreEqual(StringUtils.Substring("abc", -2), "bc");
            Assert.AreEqual(StringUtils.Substring("abc", -4), "abc");
        }

        //TODO:Verify
        [Test]
        public void TestSubstringWithEnd()
        {
            Assert.AreEqual(StringUtils.Substring(null, 1, 2), null);
            Assert.AreEqual(StringUtils.Substring("", 1, 2), "");
            Assert.AreEqual(StringUtils.Substring("abc", 0, 2), "abc");
            Assert.AreEqual(StringUtils.Substring("abc", 2, 0), "");
            Assert.AreEqual(StringUtils.Substring("abc", 2, 4), "c");
            Assert.AreEqual(StringUtils.Substring("abc", 4, 6), "");
            Assert.AreEqual(StringUtils.Substring("abc", 2, 2), "c");
            Assert.AreEqual(StringUtils.Substring("abc", -4, 2), null);
        }

        //TODO:Verify
        [Test]
        public void TestSubstringWithNegatives()
        {
            Assert.AreEqual(StringUtils.SubstringWithNegatives(null, 1, 2), null);
            Assert.AreEqual(StringUtils.SubstringWithNegatives("", 1, 2), "");
            Assert.AreEqual(StringUtils.SubstringWithNegatives("abc", 0, 2), "abc");
            Assert.AreEqual(StringUtils.SubstringWithNegatives("abc", 2, 0), "");
            Assert.AreEqual(StringUtils.SubstringWithNegatives("abc", 2, 4), "c");
            Assert.AreEqual(StringUtils.SubstringWithNegatives("abc", 4, 6), "");
            Assert.AreEqual(StringUtils.SubstringWithNegatives("abc", 2, 2), "c");
            Assert.AreEqual(StringUtils.SubstringWithNegatives("abc", -2, -1), "bc");
            Assert.AreEqual(StringUtils.SubstringWithNegatives("abc", -4, 2), "abc");
            Assert.AreEqual(StringUtils.SubstringWithNegatives("abcde", -2, 4), "de");
            Assert.AreEqual(StringUtils.SubstringWithNegatives("abcde", -2, 2), "");
        }


        [Test]
        public void TestLeft()
        {
            Assert.AreEqual(StringUtils.Left(null, 1), null);
            Assert.AreEqual(StringUtils.Left("test", -1), "");
            Assert.AreEqual(StringUtils.Left("", 1), "");
            Assert.AreEqual(StringUtils.Left("abc", 0), "");
            Assert.AreEqual(StringUtils.Left("abc", 2), "ab");
            Assert.AreEqual(StringUtils.Left("abc", 4), "abc");
        }

        [Test]
        public void TestRight()
        {
            Assert.AreEqual(StringUtils.Right(null, 1), null);
            Assert.AreEqual(StringUtils.Right("test", -1), "");
            Assert.AreEqual(StringUtils.Right("", 1), "");
            Assert.AreEqual(StringUtils.Right("abc", 0), "");
            Assert.AreEqual(StringUtils.Right("abc", 2), "bc");
            Assert.AreEqual(StringUtils.Right("abc", 4), "abc");
        }

        [Test]
        public void TestMid()
        {
            Assert.AreEqual(StringUtils.Mid(null, 1, 2), null);
            Assert.AreEqual(StringUtils.Mid("test", 0, -1), "");
            Assert.AreEqual(StringUtils.Mid("", 0, 1), "");
            Assert.AreEqual(StringUtils.Mid("abc", 0, 2), "ab");
            Assert.AreEqual(StringUtils.Mid("abc", 0, 4), "abc");
            Assert.AreEqual(StringUtils.Mid("abc", 2, 4), "c");
            Assert.AreEqual(StringUtils.Mid("abc", 4, 2), "");
            Assert.AreEqual(StringUtils.Mid("abc", -2, 2), "ab");
        }

        [Test]
        public void TestSubstringBefore()
        {
            Assert.AreEqual(StringUtils.SubstringBefore(null, "a"), null);
            Assert.AreEqual(StringUtils.SubstringBefore("", "a"), "");
            Assert.AreEqual(StringUtils.SubstringBefore("abc", "a"), "");
            Assert.AreEqual(StringUtils.SubstringBefore("abcba", "b"), "a");
            Assert.AreEqual(StringUtils.SubstringBefore("abc", "c"), "ab");
            Assert.AreEqual(StringUtils.SubstringBefore("abc", "d"), "abc");
            Assert.AreEqual(StringUtils.SubstringBefore("abc", ""), "");
            Assert.AreEqual(StringUtils.SubstringBefore("abc", null), "abc");
        }

        [Test]
        public void TestSubstringAfter()
        {
            Assert.AreEqual(StringUtils.SubstringAfter(null, "a"), null);
            Assert.AreEqual(StringUtils.SubstringAfter("", "a"), "");
            Assert.AreEqual(StringUtils.SubstringAfter("test", null), "");
            Assert.AreEqual(StringUtils.SubstringAfter("abc", "a"), "bc");
            Assert.AreEqual(StringUtils.SubstringAfter("abcba", "b"), "cba");
            Assert.AreEqual(StringUtils.SubstringAfter("abc", "c"), "");
            Assert.AreEqual(StringUtils.SubstringAfter("abc", "d"), "");
            Assert.AreEqual(StringUtils.SubstringAfter("abc", ""), "abc");
        }

        [Test]
        public void TestSubstringBeforeLast()
        {
            Assert.AreEqual(StringUtils.SubstringBeforeLast(null, "test"), null);
            Assert.AreEqual(StringUtils.SubstringBeforeLast("", "test"), "");
            Assert.AreEqual(StringUtils.SubstringBeforeLast("abcba", "b"), "abc");
            Assert.AreEqual(StringUtils.SubstringBeforeLast("abc", "c"), "ab");
            Assert.AreEqual(StringUtils.SubstringBeforeLast("a", "a"), "");
            Assert.AreEqual(StringUtils.SubstringBeforeLast("a", "z"), "a");
            Assert.AreEqual(StringUtils.SubstringBeforeLast("a", null), "a");
            Assert.AreEqual(StringUtils.SubstringBeforeLast("a", ""), "a");
        }

        [Test]
        public void TestSubstringAfterLast()
        {
            Assert.AreEqual(StringUtils.SubstringAfterLast(null, "test"), null);
            Assert.AreEqual(StringUtils.SubstringAfterLast("", "test"), "");
            Assert.AreEqual(StringUtils.SubstringAfterLast("test", ""), "");
            Assert.AreEqual(StringUtils.SubstringAfterLast("test", null), "");
            Assert.AreEqual(StringUtils.SubstringAfterLast("abc", "a"), "bc");
            Assert.AreEqual(StringUtils.SubstringAfterLast("abcba", "b"), "a");
            Assert.AreEqual(StringUtils.SubstringAfterLast("abc", "c"), "");
            Assert.AreEqual(StringUtils.SubstringAfterLast("a", "a"), "");
            Assert.AreEqual(StringUtils.SubstringAfterLast("a", "z"), "");
        }

        [Test]
        public void TestSubstringBetween()
        {
            Assert.AreEqual(StringUtils.SubstringBetween(null, "test"), null);
            Assert.AreEqual(StringUtils.SubstringBetween("", ""), "");
            Assert.AreEqual(StringUtils.SubstringBetween("", "tag"), null);
            Assert.AreEqual(StringUtils.SubstringBetween("tagabctag", null), null);
            Assert.AreEqual(StringUtils.SubstringBetween("tagabctag", ""), "");
            Assert.AreEqual(StringUtils.SubstringBetween("tagabctag", "tag"), "abc");
            Assert.AreEqual(StringUtils.SubstringBetween("tagabctag", "xyz"), null);
        }

        [Test]
        public void TestSubstringBetweenWithCloseTag()
        {
            Assert.AreEqual(StringUtils.SubstringBetween("wx[b]yz", "[", "]"), "b");
            Assert.AreEqual(StringUtils.SubstringBetween(null, "a", "test"), null);
            Assert.AreEqual(StringUtils.SubstringBetween("test", null, "test"), null);
            Assert.AreEqual(StringUtils.SubstringBetween("test", "a", null), null);
            Assert.AreEqual(StringUtils.SubstringBetween("", "", ""), "");
            Assert.AreEqual(StringUtils.SubstringBetween("", "", "]"), null);
            Assert.AreEqual(StringUtils.SubstringBetween("", "[", "]"), null);
            Assert.AreEqual(StringUtils.SubstringBetween("yabcz", "", ""), "");
            Assert.AreEqual(StringUtils.SubstringBetween("yabcz", "y", "z"), "abc");
            Assert.AreEqual(StringUtils.SubstringBetween("yabczyabcz", "y", "z"), "abc");
        }

        [Test]
        public void TestSubstringsBetween()
        {
            Assert.AreEqual(StringUtils.SubstringsBetween("[a][b][c]a", "[", "]"), new[] { "a", "b", "c" });
            Assert.AreEqual(StringUtils.SubstringsBetween(null, "a", "b"), null);
            Assert.AreEqual(StringUtils.SubstringsBetween("test", null, "a"), null);
            Assert.AreEqual(StringUtils.SubstringsBetween("test", "a", null), null);
            Assert.AreEqual(StringUtils.SubstringsBetween("", "[", "]"), _emptyCollection);
        }

        [Test]
        public void TestSplit()
        {
            Assert.AreEqual(StringUtils.Split(null), null);
            Assert.AreEqual(StringUtils.Split(""), _emptyCollection);
            Assert.AreEqual(StringUtils.Split("abc def"), new[] { "abc", "def" });
            Assert.AreEqual(StringUtils.Split("abc  def"), new[] { "abc", "def" });
            Assert.AreEqual(StringUtils.Split(" abc "), new[] { "abc" });
        }

        [Test]
        public void TestSplitWithCharSeperator()
        {
            Assert.AreEqual(StringUtils.Split(null, "test"), null);
            Assert.AreEqual(StringUtils.Split("", "test"), _emptyCollection);
            Assert.AreEqual(StringUtils.Split("a.b.c", '.'), new[] { "a", "b", "c" });
            Assert.AreEqual(StringUtils.Split("a..b.c", '.'), new[] { "a", "b", "c" });
            Assert.AreEqual(StringUtils.Split("a:b:c", '.'), new[] { "a:b:c" });
            Assert.AreEqual(StringUtils.Split("a b c", ' '), new[] { "a", "b", "c" });
        }

        [Test]
        public void TestSplitWithStringSeperators()
        {
            Assert.AreEqual(StringUtils.Split(null, "test"), null);
            Assert.AreEqual(StringUtils.Split("", "test"), _emptyCollection);
            Assert.AreEqual(StringUtils.Split("abc def", null), new[] { "abc", "def" });
            Assert.AreEqual(StringUtils.Split("abc def", " "), new[] { "abc", "def" });
            Assert.AreEqual(StringUtils.Split("abc  def", " "), new[] { "abc", "def" });
            Assert.AreEqual(StringUtils.Split("ab:cd:ef", ":"), new[] { "ab", "cd", "ef" });
        }

        [Test]
        public void TestSplitWithMax()
        {
            Assert.AreEqual(StringUtils.Split(null, "a", 1), null);
            Assert.AreEqual(StringUtils.Split("", "a", 1), _emptyCollection);
            Assert.AreEqual(StringUtils.Split("ab cd ef", null, 0), new[] { "ab", "cd", "ef" });
            Assert.AreEqual(StringUtils.Split("ab   cd ef", null, 0), new[] { "ab", "cd", "ef" });
            Assert.AreEqual(StringUtils.Split("ab:cd:ef", ":", 0), new[] { "ab", "cd", "ef" });
            Assert.AreEqual(StringUtils.Split("ab:cd:ef", ":", 2), new[] { "ab", "cd:ef" });
        }

        [Test]
        public void TestSplitByWholeSeparator()
        {
            Assert.AreEqual(StringUtils.SplitByWholeSeparator(null, "test"), null);
            Assert.AreEqual(StringUtils.SplitByWholeSeparator("", "test"), _emptyCollection);
            Assert.AreEqual(StringUtils.SplitByWholeSeparator("ab de fg", null), new[] { "ab", "de", "fg" });
            Assert.AreEqual(StringUtils.SplitByWholeSeparator("ab   de fg", null), new[] { "ab", "de", "fg" });
            Assert.AreEqual(StringUtils.SplitByWholeSeparator("ab:cd:ef", ":"), new[] { "ab", "cd", "ef" });
            Assert.AreEqual(StringUtils.SplitByWholeSeparator("ab-!-cd-!-ef", "-!-"), new[] { "ab", "cd", "ef" });
        }

        [Test]
        public void TestSplitByWholeSeparatorWithMax()
        {
            Assert.AreEqual(StringUtils.SplitByWholeSeparator(null, "test"), null);
            Assert.AreEqual(StringUtils.SplitByWholeSeparator("", "test"), _emptyCollection);
            Assert.AreEqual(StringUtils.SplitByWholeSeparator("ab de fg", null), new[] { "ab", "de", "fg" });
            Assert.AreEqual(StringUtils.SplitByWholeSeparator("ab   de fg", null), new[] { "ab", "de", "fg" });
            Assert.AreEqual(StringUtils.SplitByWholeSeparator("ab:cd:ef", ":"), new[] { "ab", "cd", "ef" });
            Assert.AreEqual(StringUtils.SplitByWholeSeparator("ab-!-cd-!-ef", "-!-"), new[] { "ab", "cd", "ef" });
        }

        [Test]
        public void TestSplitByWholeSeparatorPreserveAllTokens()
        {
            Assert.AreEqual(StringUtils.SplitByWholeSeparatorPreserveAllTokens(null, "test"), null);
            Assert.AreEqual(StringUtils.SplitByWholeSeparatorPreserveAllTokens("", "test"), _emptyCollection);
            Assert.AreEqual(StringUtils.SplitByWholeSeparatorPreserveAllTokens("ab de fg", null), new[] { "ab", "de", "fg" });
            Assert.AreEqual(StringUtils.SplitByWholeSeparatorPreserveAllTokens("ab   de fg", null), new[] { "ab", "", "", "de", "fg" });
            Assert.AreEqual(StringUtils.SplitByWholeSeparatorPreserveAllTokens("ab:cd:ef", ":"), new[] { "ab", "cd", "ef" });
            Assert.AreEqual(StringUtils.SplitByWholeSeparatorPreserveAllTokens("ab-!-cd-!-ef", "-!-"), new[] { "ab", "cd", "ef" });
        }

        [Test]
        public void TestSplitByWholeSeparatorPreserveAllTokensWithMax()
        {
            Assert.AreEqual(StringUtils.SplitByWholeSeparatorPreserveAllTokens(null, "", 1), null);
            Assert.AreEqual(StringUtils.SplitByWholeSeparatorPreserveAllTokens("", "", 1), _emptyCollection);
            Assert.AreEqual(StringUtils.SplitByWholeSeparatorPreserveAllTokens("ab de fg", null, 0), new[] { "ab", "de", "fg" });
            Assert.AreEqual(StringUtils.SplitByWholeSeparatorPreserveAllTokens("ab   de fg", null, 0), new[] { "ab", "", "", "de", "fg" });
            Assert.AreEqual(StringUtils.SplitByWholeSeparatorPreserveAllTokens("ab:cd:ef", ":", 2), new[] { "ab", "cd:ef" });
            Assert.AreEqual(StringUtils.SplitByWholeSeparatorPreserveAllTokens("ab-!-cd-!-ef", "-!-", 5), new[] { "ab", "cd", "ef" });
            Assert.AreEqual(StringUtils.SplitByWholeSeparatorPreserveAllTokens("ab-!-cd-!-ef", "-!-", 2), new[] { "ab", "cd-!-ef" });
        }

        [Test]
        public void TestSplitPreserveAllTokens()
        {
            Assert.AreEqual(StringUtils.SplitPreserveAllTokens(null), null);
            Assert.AreEqual(StringUtils.SplitPreserveAllTokens(""), _emptyCollection);
            Assert.AreEqual(StringUtils.SplitPreserveAllTokens("abc def"), new[] { "abc", "def" });
            Assert.AreEqual(StringUtils.SplitPreserveAllTokens("abc  def"), new[] { "abc", "", "def" });
            Assert.AreEqual(StringUtils.SplitPreserveAllTokens(" abc "), new[] { "", "abc", "" });
        }

        [Test]
        public void TestSplitPreserveAllTokensByCharSeperator()
        {
            Assert.AreEqual(StringUtils.SplitPreserveAllTokens(null, 'a'), null);
            Assert.AreEqual(StringUtils.SplitPreserveAllTokens("", 'a'), _emptyCollection);
            Assert.AreEqual(StringUtils.SplitPreserveAllTokens("a.b.c", '.'), new[] { "a", "b", "c" });
            Assert.AreEqual(StringUtils.SplitPreserveAllTokens("a..b.c", '.'), new[] { "a", "", "b", "c" });
            Assert.AreEqual(StringUtils.SplitPreserveAllTokens("a:b:c", '.'), new[] { "a:b:c" });
            Assert.AreEqual(StringUtils.SplitPreserveAllTokens("a b c", ' '), new[] { "a", "b", "c" });
            Assert.AreEqual(StringUtils.SplitPreserveAllTokens("a b c ", ' '), new[] { "a", "b", "c", "" });
            Assert.AreEqual(StringUtils.SplitPreserveAllTokens("a b c  ", ' '), new[] { "a", "b", "c", "", "" });
            Assert.AreEqual(StringUtils.SplitPreserveAllTokens(" a b c", ' '), new[] { "", "a", "b", "c" });
            Assert.AreEqual(StringUtils.SplitPreserveAllTokens("  a b c", ' '), new[] { "", "", "a", "b", "c" });
            Assert.AreEqual(StringUtils.SplitPreserveAllTokens(" a b c ", ' '), new[] { "", "a", "b", "c", "" });
        }

        [Test]
        public void TestSplitPreserveAllTokensByStringSeperator()
        {

            Assert.AreEqual(StringUtils.SplitPreserveAllTokens(null, "test"), null);
            Assert.AreEqual(StringUtils.SplitPreserveAllTokens("", "test"), _emptyCollection);
            Assert.AreEqual(StringUtils.SplitPreserveAllTokens("abc def", null), new[] { "abc", "def" });
            Assert.AreEqual(StringUtils.SplitPreserveAllTokens("abc def", " "), new[] { "abc", "def" });
            Assert.AreEqual(StringUtils.SplitPreserveAllTokens("abc  def", " "), new[] { "abc", "", "def" });
            Assert.AreEqual(StringUtils.SplitPreserveAllTokens("ab:cd:ef", ":"), new[] { "ab", "cd", "ef" });
            Assert.AreEqual(StringUtils.SplitPreserveAllTokens("ab:cd:ef:", ":"), new[] { "ab", "cd", "ef", "" });
            Assert.AreEqual(StringUtils.SplitPreserveAllTokens("ab:cd:ef::", ":"), new[] { "ab", "cd", "ef", "", "" });
            Assert.AreEqual(StringUtils.SplitPreserveAllTokens("ab::cd:ef", ":"), new[] { "ab", "", "cd", "ef" });
            Assert.AreEqual(StringUtils.SplitPreserveAllTokens(":cd:ef", ":"), new[] { "", "cd", "ef" });
            Assert.AreEqual(StringUtils.SplitPreserveAllTokens("::cd:ef", ":"), new[] { "", "", "cd", "ef" });
            Assert.AreEqual(StringUtils.SplitPreserveAllTokens(":cd:ef:", ":"), new[] { "", "cd", "ef", "" });

        }

        [Test]
        public void TestSplitPreserveAllTokensByStringSeparatorWithMax()
        {
            Assert.AreEqual(StringUtils.SplitPreserveAllTokens(null, "", 1), null);
            Assert.AreEqual(StringUtils.SplitPreserveAllTokens("", "", 1), _emptyCollection);
            Assert.AreEqual(StringUtils.SplitPreserveAllTokens("ab de fg", null, 0), new[] { "ab", "de", "fg" });
            Assert.AreEqual(StringUtils.SplitPreserveAllTokens("ab  de fg", null, 0), new[] { "ab", "", "de", "fg" });
            Assert.AreEqual(StringUtils.SplitPreserveAllTokens("ab:cd:ef", ":", 0), new[] { "ab", "cd", "ef" });
            Assert.AreEqual(StringUtils.SplitPreserveAllTokens("ab:cd:ef", ":", 2), new[] { "ab", "cd:ef" });
            Assert.AreEqual(StringUtils.SplitPreserveAllTokens("ab   de fg", null, 2), new[] { "ab", "  de fg" });
            Assert.AreEqual(StringUtils.SplitPreserveAllTokens("ab   de fg", null, 3), new[] { "ab", "", " de fg" });
            Assert.AreEqual(StringUtils.SplitPreserveAllTokens("ab   de fg", null, 4), new[] { "ab", "", "", "de fg" });
        }

        [Test]
        public void TestSplitByCharacterType()
        {
            Assert.AreEqual(StringUtils.SplitByCharacterType(null), null);
            Assert.AreEqual(StringUtils.SplitByCharacterType(""), _emptyCollection);
            Assert.AreEqual(StringUtils.SplitByCharacterType("ab de fg"), new[] { "ab", " ", "de", " ", "fg" });
            Assert.AreEqual(StringUtils.SplitByCharacterType("ab   de fg"), new[] { "ab", "   ", "de", " ", "fg" });
            Assert.AreEqual(StringUtils.SplitByCharacterType("ab:cd:ef"), new[] { "ab", ":", "cd", ":", "ef" });
            Assert.AreEqual(StringUtils.SplitByCharacterType("number5"), new[] { "number", "5" });
            Assert.AreEqual(StringUtils.SplitByCharacterType("fooBar"), new[] { "foo", "B", "ar" });
            Assert.AreEqual(StringUtils.SplitByCharacterType("foo200Bar"), new[] { "foo", "200", "B", "ar" });
            Assert.AreEqual(StringUtils.SplitByCharacterType("ASFRules"), new[] { "ASFR", "ules" });
        }

        [Test]
        public void TestSplitByCharacterTypeCamelCase()
        {
            Assert.AreEqual(StringUtils.SplitByCharacterTypeCamelCase(null), null);
            Assert.AreEqual(StringUtils.SplitByCharacterTypeCamelCase(""), _emptyCollection);
            Assert.AreEqual(StringUtils.SplitByCharacterTypeCamelCase("ab de fg"), new[] { "ab", " ", "de", " ", "fg" });
            Assert.AreEqual(StringUtils.SplitByCharacterTypeCamelCase("ab   de fg"), new[] { "ab", "   ", "de", " ", "fg" });
            Assert.AreEqual(StringUtils.SplitByCharacterTypeCamelCase("ab:cd:ef"), new[] { "ab", ":", "cd", ":", "ef" });
            Assert.AreEqual(StringUtils.SplitByCharacterTypeCamelCase("number5"), new[] { "number", "5" });
            Assert.AreEqual(StringUtils.SplitByCharacterTypeCamelCase("fooBar"), new[] { "foo", "Bar" });
            Assert.AreEqual(StringUtils.SplitByCharacterTypeCamelCase("foo200Bar"), new[] { "foo", "200", "Bar" });
            Assert.AreEqual(StringUtils.SplitByCharacterTypeCamelCase("ASFRules"), new[] { "ASF", "Rules" });
        }



        #endregion

        #region Join

        [Test]
        public void TestJoin()
        {
            Assert.AreEqual(StringUtils.Join(null), null);
            Assert.AreEqual(StringUtils.Join(_emptyCollection), "");
            Assert.AreEqual(StringUtils.Join(new string[] { null }), "");
            Assert.AreEqual(StringUtils.Join("a", "b", "c"), "abc");
            Assert.AreEqual(StringUtils.Join(null, "", "a"), "a");
        }

        [Test]
        public void TestJoinBySeparator()
        {
            Assert.AreEqual(StringUtils.Join((object[]) null, "test"), null);
            Assert.AreEqual(StringUtils.Join(new object[] { }, "test"), "");
            Assert.AreEqual(StringUtils.Join(new object[] { null }, "test"), "");
            Assert.AreEqual(StringUtils.Join(new object[] { "a", "b", "c" }, ";"), "a;b;c");
            Assert.AreEqual(StringUtils.Join(new object[] { "a", "b", "c" }), "abc");
            Assert.AreEqual(StringUtils.Join(new object[] { null, "", "a" }, ";"), "a");
        }

        [Test]
        public void TestJoinByLong()
        {
            Assert.AreEqual(StringUtils.Join((long[]) null, ";"), null);
            Assert.AreEqual(StringUtils.Join(new long[] { }, ";"), "");
            Assert.AreEqual(StringUtils.Join(new long[] { 1, 2, 3 }, ";"), "1;2;3");
            Assert.AreEqual(StringUtils.Join(new long[] { 1, 2, 3 }), "123");
        }

        [Test]
        public void TestJoinByInt()
        {
            Assert.AreEqual(StringUtils.Join((int[]) null, "test"), null);
            Assert.AreEqual(StringUtils.Join(new int[] { }, "test"), "");
            Assert.AreEqual(StringUtils.Join(new[] { 1, 2, 3 }, ";"), "1;2;3");
            Assert.AreEqual(StringUtils.Join(new[] { 1, 2, 3 }), "123");
        }

        [Test]
        public void TestJoinByDouble()
        {
            Assert.AreEqual(StringUtils.Join((double[]) null, "test"), null);
            Assert.AreEqual(StringUtils.Join(new double[] { }, "test"), "");
            Assert.AreEqual(StringUtils.Join(new double[] { 1, 2, 3 }, ";"), "1;2;3");
            Assert.AreEqual(StringUtils.Join(new double[] { 1, 2, 3 }), "123");
        }

        [Test]
        public void TestJoinByShort()
        {
            Assert.AreEqual(StringUtils.Join((short[]) null, "test"), null);
            Assert.AreEqual(StringUtils.Join(new short[] { }, "test"), "");
            Assert.AreEqual(StringUtils.Join(new short[] { 1, 2, 3 }, ";"), "1;2;3");
            Assert.AreEqual(StringUtils.Join(new short[] { 1, 2, 3 }), "123");
        }

        [Test]
        public void TestJoinByFloat()
        {
            Assert.AreEqual(StringUtils.Join((float[]) null, "test"), null);
            Assert.AreEqual(StringUtils.Join(new float[] { }, "test"), "");
            Assert.AreEqual(StringUtils.Join(new float[] { 1, 2, 3 }, ";"), "1;2;3");
            Assert.AreEqual(StringUtils.Join(new float[] { 1, 2, 3 }), "123");
        }

        [Test]
        public void TestJoinByByte()
        {
            Assert.AreEqual(StringUtils.Join((byte[]) null, "test"), null);
            Assert.AreEqual(StringUtils.Join(new byte[] { }, "test"), "");
            Assert.AreEqual(StringUtils.Join(new byte[] { 1, 2, 3 }, ";"), "1;2;3");
            Assert.AreEqual(StringUtils.Join(new byte[] { 1, 2, 3 }), "123");
        }

        [Test]
        public void TestJoinByChar()
        {
            Assert.AreEqual(StringUtils.Join((char[]) null, "test"), null);
            Assert.AreEqual(StringUtils.Join(new char[] { }, "test"), "");
            Assert.AreEqual(StringUtils.Join(new[] { 'a', 'b', 'c' }, ";"), "a;b;c");
            Assert.AreEqual(StringUtils.Join(new[] { 'a', 'b', 'c' }), "abc");
        }

        [Test]
        public void TestJoinByObjStartIndex()
        {
            Assert.AreEqual(StringUtils.Join((object[]) null, ";", 0, 2), null);
            Assert.AreEqual(StringUtils.Join(new object[] { }, ";", 0, 2), "");
            Assert.AreEqual(StringUtils.Join(new object[] { null }, ";", 0, 0), "");
            Assert.AreEqual(StringUtils.Join(new object[] { "a", "b", "c" }, ";", 0, 2), "a;b");
            Assert.AreEqual(StringUtils.Join(new object[] { "a", "b", "c" }, null, 0, 2), "ab");

        }

        [Test]
        public void TestJoinByLongStartIndex()
        {
            Assert.AreEqual(StringUtils.Join((long[]) null, ";", 1, 2), null);
            Assert.AreEqual(StringUtils.Join(new long[] { }, ";", 1, 2), "");
            Assert.AreEqual(StringUtils.Join(new long[] { 1, 2, 3 }, ";", 0, 2), "1;2");
            Assert.AreEqual(StringUtils.Join(new long[] { 1, 2, 3 }, null, 0, 2), "12");

        }

        [Test]
        public void TestJoinByIntStartIndex()
        {
            Assert.AreEqual(StringUtils.Join((int[]) null, ";", 1, 2), null);
            Assert.AreEqual(StringUtils.Join(new int[] { }, ";", 1, 2), "");
            Assert.AreEqual(StringUtils.Join(new[] { 1, 2, 3 }, ";", 0, 2), "1;2");
            Assert.AreEqual(StringUtils.Join(new[] { 1, 2, 3 }, null, 0, 2), "12");

        }

        [Test]
        public void TestJoinByShortStartIndex()
        {
            Assert.AreEqual(StringUtils.Join((short[]) null, ";", 1, 2), null);
            Assert.AreEqual(StringUtils.Join(new short[] { }, ";", 1, 2), "");
            Assert.AreEqual(StringUtils.Join(new short[] { 1, 2, 3 }, ";", 0, 2), "1;2");
            Assert.AreEqual(StringUtils.Join(new short[] { 1, 2, 3 }, null, 0, 2), "12");

        }


        [Test]
        public void TestJoinByDoubleStartIndex()
        {
            Assert.AreEqual(StringUtils.Join((double[]) null, ";", 1, 2), null);
            Assert.AreEqual(StringUtils.Join(new double[] { }, ";", 1, 2), "");
            Assert.AreEqual(StringUtils.Join(new double[] { 1, 2, 3 }, ";", 0, 2), "1;2");
            Assert.AreEqual(StringUtils.Join(new double[] { 1, 2, 3 }, null, 0, 2), "12");

        }


        [Test]
        public void TestJoinByFloatStartIndex()
        {
            Assert.AreEqual(StringUtils.Join((float[]) null, ";", 1, 2), null);
            Assert.AreEqual(StringUtils.Join(new float[] { }, ";", 1, 2), "");
            Assert.AreEqual(StringUtils.Join(new float[] { 1, 2, 3 }, ";", 0, 2), "1;2");
            Assert.AreEqual(StringUtils.Join(new float[] { 1, 2, 3 }, null, 0, 2), "12");

        }


        [Test]
        public void TestJoinByByteStartIndex()
        {
            Assert.AreEqual(StringUtils.Join((byte[]) null, ";", 1, 2), null);
            Assert.AreEqual(StringUtils.Join(new byte[] { }, ";", 1, 2), "");
            Assert.AreEqual(StringUtils.Join(new byte[] { 1, 2, 3 }, ";", 0, 2), "1;2");
            Assert.AreEqual(StringUtils.Join(new byte[] { 1, 2, 3 }, null, 0, 2), "12");

        }


        [Test]
        public void TestJoinByCharStartIndex()
        {
            Assert.AreEqual(StringUtils.Join((char[]) null, ";", 1, 2), null);
            Assert.AreEqual(StringUtils.Join(new char[] { }, ";", 1, 2), "");
            Assert.AreEqual(StringUtils.Join(new[] { 'a', 'b', 'c' }, ";", 0, 2), "a;b");
            Assert.AreEqual(StringUtils.Join(new[] { 'a', 'b', 'c' }, null, 0, 2), "ab");

        }

        [Test]
        public void TestJoinWith()
        {
            Assert.AreEqual(StringUtils.JoinWith(new object[] { "a", "b" }), "ab");
            Assert.AreEqual(StringUtils.JoinWith(new object[] { "a", "1", "" }), "a1");
            Assert.AreEqual(StringUtils.JoinWith(new object[] { "a", null, "b" }), "ab");
            Assert.AreEqual(StringUtils.JoinWith(new object[] { "a", "1" }), "a1");
        }

        [Test]
        public void TestJoinWithBySeparator()
        {
            Assert.AreEqual(StringUtils.JoinWith(",", "a", "b"), "a,b");
            Assert.AreEqual(StringUtils.JoinWith(",", "a", "1", ""), "a,1");
            Assert.AreEqual(StringUtils.JoinWith(",", "a", null, "b"), "a,b");
            Assert.AreEqual(StringUtils.JoinWith(null, "a", "1"), "a1");
        }


        #endregion

        #region Remove

        [Test]
        public void TestDeleteWhitespace()
        {
            Assert.AreEqual(StringUtils.DeleteWhitespace(null), null);
            Assert.AreEqual(StringUtils.DeleteWhitespace(""), "");
            Assert.AreEqual(StringUtils.DeleteWhitespace("abc"), "abc");
            Assert.AreEqual(StringUtils.DeleteWhitespace("   ab  c  "), "abc");
        }

        [Test]
        public void TestRemoveStart()
        {
            Assert.AreEqual(StringUtils.RemoveStart(null, "test"), null);
            Assert.AreEqual(StringUtils.RemoveStart("", "test"), "");
            Assert.AreEqual(StringUtils.RemoveStart("test", null), "test");
            Assert.AreEqual(StringUtils.RemoveStart("www.xyz.com", "www."), "xyz.com");
            Assert.AreEqual(StringUtils.RemoveStart("xyz.com", "www."), "xyz.com");
            Assert.AreEqual(StringUtils.RemoveStart("www.xyz.com", "xyz"), "www.xyz.com");
            Assert.AreEqual(StringUtils.RemoveStart("abc", ""), "abc");
        }

        [Test]
        public void TestRemoveStartIgnoreCase()
        {
            Assert.AreEqual(StringUtils.RemoveStartIgnoreCase(null, "test"), null);
            Assert.AreEqual(StringUtils.RemoveStartIgnoreCase("", "test"), "");
            Assert.AreEqual(StringUtils.RemoveStartIgnoreCase("test", null), "test");
            Assert.AreEqual(StringUtils.RemoveStartIgnoreCase("www.xyz.com", "www."), "xyz.com");
            Assert.AreEqual(StringUtils.RemoveStartIgnoreCase("www.xyz.com", "WWW."), "xyz.com");
            Assert.AreEqual(StringUtils.RemoveStartIgnoreCase("xyz.com", "www."), "xyz.com");
            Assert.AreEqual(StringUtils.RemoveStartIgnoreCase("www.xyz.com", "xyz"), "www.xyz.com");
            Assert.AreEqual(StringUtils.RemoveStartIgnoreCase("abc", ""), "abc");
        }

        [Test]
        public void TestRemoveEnd()
        {
            Assert.AreEqual(StringUtils.RemoveEnd(null, "test"), null);
            Assert.AreEqual(StringUtils.RemoveEnd("", "test"), "");
            Assert.AreEqual(StringUtils.RemoveEnd("test", null), "test");
            Assert.AreEqual(StringUtils.RemoveEnd("www.xyz.com", ".com."), "www.xyz.com");
            Assert.AreEqual(StringUtils.RemoveEnd("www.xyz.com", ".com"), "www.xyz");
            Assert.AreEqual(StringUtils.RemoveEnd("www.xyz.com", "xyz"), "www.xyz.com");
            Assert.AreEqual(StringUtils.RemoveEnd("abc", ""), "abc");
        }

        [Test]
        public void TestRemoveEndIgnoreCase()
        {
            Assert.AreEqual(StringUtils.RemoveEndIgnoreCase(null, "test"), null);
            Assert.AreEqual(StringUtils.RemoveEndIgnoreCase("", "test"), "");
            Assert.AreEqual(StringUtils.RemoveEndIgnoreCase("test", null), "test");
            Assert.AreEqual(StringUtils.RemoveEndIgnoreCase("www.xyz.com", ".com."), "www.xyz.com");
            Assert.AreEqual(StringUtils.RemoveEndIgnoreCase("www.xyz.com", ".com"), "www.xyz");
            Assert.AreEqual(StringUtils.RemoveEndIgnoreCase("www.xyz.com", "xyz"), "www.xyz.com");
            Assert.AreEqual(StringUtils.RemoveEndIgnoreCase("abc", ""), "abc");
            Assert.AreEqual(StringUtils.RemoveEndIgnoreCase("www.xyz.com", ".COM"), "www.xyz");
            Assert.AreEqual(StringUtils.RemoveEndIgnoreCase("www.xyz.COM", ".com"), "www.xyz");
        }

        [Test]
        public void TestRemove()
        {
            Assert.AreEqual(StringUtils.Remove(null, "test"), null);
            Assert.AreEqual(StringUtils.Remove("", "test"), "");
            Assert.AreEqual(StringUtils.Remove("test", null), "test");
            Assert.AreEqual(StringUtils.Remove("test", ""), "test");
            Assert.AreEqual(StringUtils.Remove("queued", "ue"), "qd");
            Assert.AreEqual(StringUtils.Remove("queued", "zz"), "queued");
            Assert.AreEqual(StringUtils.Remove("quesaued", "ue"), "qsad");
        }

        [Test]
        public void TestRemoveByChar()
        {
            Assert.AreEqual(StringUtils.Remove(null, "test"), null);
            Assert.AreEqual(StringUtils.Remove("", "test"), "");
            Assert.AreEqual(StringUtils.Remove("queued", 'u'), "qeed");
            Assert.AreEqual(StringUtils.Remove("queued", 'z'), "queued");
        }

        [Test]
        public void TestReplaceOnce()
        {
            Assert.AreEqual(StringUtils.ReplaceOnce(null, "dream", "test"), null);
            Assert.AreEqual(StringUtils.ReplaceOnce("", "dream", "test"), "");
            Assert.AreEqual(StringUtils.ReplaceOnce("any", null, "test"), "any");
            Assert.AreEqual(StringUtils.ReplaceOnce("any", "dream", null), "any");
            Assert.AreEqual(StringUtils.ReplaceOnce("any", "", "test"), "any");
            Assert.AreEqual(StringUtils.ReplaceOnce("aba", "a", null), "aba");
            Assert.AreEqual(StringUtils.ReplaceOnce("aba", "a", ""), "ba");
            Assert.AreEqual(StringUtils.ReplaceOnce("aba", "a", "z"), "zba");
        }

        [Test]
        public void TestReplacePattern()
        {

            Assert.AreEqual(StringUtils.ReplacePattern(null, "dream", "test"), null);
            Assert.AreEqual(StringUtils.ReplacePattern("", "dream", "test"), "");
            Assert.AreEqual(StringUtils.ReplacePattern("any", null, "test"), "any");
            Assert.AreEqual(StringUtils.ReplacePattern("any", "dream", null), "any");
            Assert.AreEqual(StringUtils.ReplacePattern("any", "", "test"), "any");
            Assert.AreEqual(
               StringUtils.ReplacePattern(@"satish@gmail.com",
                  MatchEmailPattern,
                   "NoEmail", true), "NoEmail");
        }

        [Test]
        public void TestRemovePattern()
        {
            Assert.AreEqual(StringUtils.RemovePattern(null, "dream"), null);
            Assert.AreEqual(StringUtils.RemovePattern("", "dream"), "");
            Assert.AreEqual(StringUtils.RemovePattern("any", null), "any");
            Assert.AreEqual(StringUtils.RemovePattern("any", ""), "any");
            Assert.AreEqual(StringUtils.RemovePattern("satish049@gmail.com", @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z"), "");
        }

        [Test]
        public void TestReplace()
        {
            Assert.AreEqual(StringUtils.Replace(null, "dream", "test"), null);
            Assert.AreEqual(StringUtils.Replace("", "dream", "test"), "");
            Assert.AreEqual(StringUtils.Replace("any", null, "test"), "any");
            Assert.AreEqual(StringUtils.Replace("any", "dream", null), "any");
            Assert.AreEqual(StringUtils.Replace("any", "", "test"), "any");
            Assert.AreEqual(StringUtils.Replace("aba", "a", null), "aba");
            Assert.AreEqual(StringUtils.Replace("aba", "a", ""), "b");
            Assert.AreEqual(StringUtils.Replace("aba", "a", "z"), "zbz");
        }

        [Test]
        public void TestReplaceWithMax()
        {
            Assert.AreEqual(StringUtils.Replace(null, "dream", "a", 1), null);
            Assert.AreEqual(StringUtils.Replace("", "dream", "a", 0), "");
            Assert.AreEqual(StringUtils.Replace("any", null, "a", 1), "any");
            Assert.AreEqual(StringUtils.Replace("any", "dream", null, 1), "any");
            Assert.AreEqual(StringUtils.Replace("any", "", "a", 1), "any");
            Assert.AreEqual(StringUtils.Replace("any", "dream", "a", 0), "any");
            Assert.AreEqual(StringUtils.Replace("abaa", "a", null, -1), "abaa");
            Assert.AreEqual(StringUtils.Replace("abaa", "a", "", -1), "b");
            Assert.AreEqual(StringUtils.Replace("abaa", "a", "z", 0), "abaa");
            Assert.AreEqual(StringUtils.Replace("abaa", "a", "z", 1), "zbaa");
            Assert.AreEqual(StringUtils.Replace("abaa", "a", "z", 2), "zbza");
            Assert.AreEqual(StringUtils.Replace("abaa", "a", "z", -1), "zbzz");
        }

        [Test]
        public void TestReplaceEach()
        {
            Assert.AreEqual(StringUtils.ReplaceEach(null, new[] { "test" }, new[] { "test" }), null);
            Assert.AreEqual(StringUtils.ReplaceEach("", new[] { "test" }, new[] { "test" }), "");
            Assert.AreEqual(StringUtils.ReplaceEach("aba", null, null), "aba");
            Assert.AreEqual(StringUtils.ReplaceEach("aba", new String[0], null), "aba");
            Assert.AreEqual(StringUtils.ReplaceEach("aba", null, new String[0]), "aba");
            Assert.AreEqual(StringUtils.ReplaceEach("aba", new[] { "a" }, null), "aba");
            Assert.AreEqual(StringUtils.ReplaceEach("aba", new[] { "a" }, new[] { "" }), "b");
            Assert.AreEqual(StringUtils.ReplaceEach("aba", new String[] { null }, new[] { "a" }), "aba");
            Assert.AreEqual(StringUtils.ReplaceEach("abcde", new[] { "ab", "d" }, new[] { "w", "t" }), "wcte");
        }

        [Test]
        public void TestReplaceEachRepeatedly()
        {
            Assert.AreEqual(StringUtils.ReplaceEachRepeatedly(null, new[] { "test" }, new[] { "test" }), null);
            Assert.AreEqual(StringUtils.ReplaceEachRepeatedly("", new[] { "test" }, new[] { "test" }), "");
            Assert.AreEqual(StringUtils.ReplaceEachRepeatedly("aba", null, null), "aba");
            Assert.AreEqual(StringUtils.ReplaceEachRepeatedly("aba", new String[0], null), "aba");
            Assert.AreEqual(StringUtils.ReplaceEachRepeatedly("aba", null, new String[0]), "aba");
            Assert.AreEqual(StringUtils.ReplaceEachRepeatedly("aba", new[] { "a" }, null), "aba");
            Assert.AreEqual(StringUtils.ReplaceEachRepeatedly("aba", new[] { "a" }, new[] { "" }), "b");
            Assert.AreEqual(StringUtils.ReplaceEachRepeatedly("aba", new String[] { null }, new[] { "a" }), "aba");
            Assert.AreEqual(StringUtils.ReplaceEachRepeatedly("abcde", new[] { "ab", "d" }, new[] { "w", "t" }), "wcte");
        }


        [Test]
        public void TestReplaceChars()
        {
            Assert.AreEqual(StringUtils.ReplaceChars(null, 's', 'a'), null);
            Assert.AreEqual(StringUtils.ReplaceChars("", 's', 'a'), "");
            Assert.AreEqual(StringUtils.ReplaceChars("abcba", 'b', 'y'), "aycya");
            Assert.AreEqual(StringUtils.ReplaceChars("abcba", 'z', 'y'), "abcba");
        }

        [Test]
        public void TestReplaceCharsByStringofChars()
        {
            Assert.AreEqual(StringUtils.ReplaceChars(null, "dream", "test"), null);
            Assert.AreEqual(StringUtils.ReplaceChars("", "dream", "test"), "");
            Assert.AreEqual(StringUtils.ReplaceChars("abc", null, "test"), "abc");
            Assert.AreEqual(StringUtils.ReplaceChars("abc", "", "test"), "abc");
            Assert.AreEqual(StringUtils.ReplaceChars("abc", "b", null), "ac");
            Assert.AreEqual(StringUtils.ReplaceChars("abc", "b", ""), "ac");
            Assert.AreEqual(StringUtils.ReplaceChars("abcba", "bc", "yz"), "ayzya");
            Assert.AreEqual(StringUtils.ReplaceChars("abcba", "bc", "y"), "ayya");
            Assert.AreEqual(StringUtils.ReplaceChars("abcba", "bc", "yzx"), "ayzya");
        }

        [Test]
        public void TestOverlay()
        {
            Assert.AreEqual(StringUtils.Overlay(null, "test", 1, 1), null);
            Assert.AreEqual(StringUtils.Overlay("", "abc", 0, 0), "abc");
            Assert.AreEqual(StringUtils.Overlay("abcdef", null, 2, 4), "abef");
            Assert.AreEqual(StringUtils.Overlay("abcdef", "", 2, 4), "abef");
            Assert.AreEqual(StringUtils.Overlay("abcdef", "", 4, 2), "abef");
            Assert.AreEqual(StringUtils.Overlay("abcdef", "zzzz", 2, 4), "abzzzzef");
            Assert.AreEqual(StringUtils.Overlay("abcdef", "zzzz", 4, 2), "abzzzzef");
            Assert.AreEqual(StringUtils.Overlay("abcdef", "zzzz", -1, 4), "zzzzef");
            Assert.AreEqual(StringUtils.Overlay("abcdef", "zzzz", 2, 8), "abzzzz");
            Assert.AreEqual(StringUtils.Overlay("abcdef", "zzzz", -2, -3), "zzzzabcdef");
            Assert.AreEqual(StringUtils.Overlay("abcdef", "zzzz", 8, 10), "abcdefzzzz");
        }

        [Test]
        public void TestChomp()
        {
            Assert.AreEqual(StringUtils.Chomp(null), null);
            Assert.AreEqual(StringUtils.Chomp(""), "");
            Assert.AreEqual(StringUtils.Chomp("abc \r"), "abc ");
            Assert.AreEqual(StringUtils.Chomp("abc\n"), "abc");
            Assert.AreEqual(StringUtils.Chomp("abc\r\n"), "abc");
            Assert.AreEqual(StringUtils.Chomp("abc\r\n\r\n"), "abc\r\n");
            Assert.AreEqual(StringUtils.Chomp("abc\n\r"), "abc\n");
            Assert.AreEqual(StringUtils.Chomp("abc\n\rabc"), "abc\n\rabc");
            Assert.AreEqual(StringUtils.Chomp("\r"), "");
            Assert.AreEqual(StringUtils.Chomp("\n"), "");
            Assert.AreEqual(StringUtils.Chomp("\r\n"), "");
        }

        [Test]
        public void TestChop()
        {
            Assert.AreEqual(StringUtils.Chop(null), null);
            Assert.AreEqual(StringUtils.Chop(""), "");
            Assert.AreEqual(StringUtils.Chop("abc \r"), "abc ");
            Assert.AreEqual(StringUtils.Chop("abc\n"), "abc");
            Assert.AreEqual(StringUtils.Chop("abc\r\n"), "abc");
            Assert.AreEqual(StringUtils.Chop("abc"), "ab");
            Assert.AreEqual(StringUtils.Chop("abc\nabc"), "abc\nab");
            Assert.AreEqual(StringUtils.Chop("a"), "");
            Assert.AreEqual(StringUtils.Chop("\r"), "");
            Assert.AreEqual(StringUtils.Chop("\n"), "");
            Assert.AreEqual(StringUtils.Chop("\r\n"), "");
        }



        #endregion

        #region Paddings and Conversions

        [Test]
        public void TestRepeat()
        {
            Assert.AreEqual(StringUtils.Repeat(null, 2), null);
            Assert.AreEqual(StringUtils.Repeat("", 0), "");
            Assert.AreEqual(StringUtils.Repeat("", 2), "");
            Assert.AreEqual(StringUtils.Repeat("a", 3), "aaa");
            Assert.AreEqual(StringUtils.Repeat("ab", 2), "abab");
            Assert.AreEqual(StringUtils.Repeat("a", -2), "");
        }

        [Test]
        public void TestRepeatBynTimes()
        {
            Assert.AreEqual(StringUtils.Repeat(null, null, 2), null);
            Assert.AreEqual(StringUtils.Repeat(null, "x", 2), null);
            Assert.AreEqual(StringUtils.Repeat("", null, 0), "");
            Assert.AreEqual(StringUtils.Repeat("", "", 2), "");
            Assert.AreEqual(StringUtils.Repeat("x", "", 3), "xxx");
            Assert.AreEqual(StringUtils.Repeat("?", ", ", 3), "?, ?, ?");
        }

        [Test]
        public void TestRepeatByChar()
        {
            Assert.AreEqual(StringUtils.Repeat('e', 0), "");
            Assert.AreEqual(StringUtils.Repeat('e', 3), "eee");
            Assert.AreEqual(StringUtils.Repeat('e', -2), "");
        }

        [Test]
        public void TestRightPad()
        {
            Assert.AreEqual(StringUtils.RightPad(null, 0), null);
            Assert.AreEqual(StringUtils.RightPad("", 3), "   ");
            Assert.AreEqual(StringUtils.RightPad("bat", 3), "bat");
            Assert.AreEqual(StringUtils.RightPad("bat", 5), "bat  ");
            Assert.AreEqual(StringUtils.RightPad("bat", 1), "bat");
            Assert.AreEqual(StringUtils.RightPad("bat", -1), "bat");
        }

        [Test]
        public void TestRightPadByChar()
        {
            Assert.AreEqual(StringUtils.RightPad(null, 3, 'z'), null);
            Assert.AreEqual(StringUtils.RightPad("", 3, 'z'), "zzz");
            Assert.AreEqual(StringUtils.RightPad("bat", 3, 'z'), "bat");
            Assert.AreEqual(StringUtils.RightPad("bat", 5, 'z'), "batzz");
            Assert.AreEqual(StringUtils.RightPad("bat", 1, 'z'), "bat");
            Assert.AreEqual(StringUtils.RightPad("bat", -1, 'z'), "bat");
        }

        [Test]
        public void TestRightPadByString()
        {
            Assert.AreEqual(StringUtils.RightPad(null, 3, "z"), null);
            Assert.AreEqual(StringUtils.RightPad("", 3, "z"), "zzz");
            Assert.AreEqual(StringUtils.RightPad("bat", 3, "yz"), "bat");
            Assert.AreEqual(StringUtils.RightPad("bat", 5, "yz"), "batyz");
            Assert.AreEqual(StringUtils.RightPad("bat", 8, "yz"), "batyzyzy");
            Assert.AreEqual(StringUtils.RightPad("bat", 1, "yz"), "bat");
            Assert.AreEqual(StringUtils.RightPad("bat", -1, "yz"), "bat");
            Assert.AreEqual(StringUtils.RightPad("bat", 5, null), "bat  ");
            Assert.AreEqual(StringUtils.RightPad("bat", 5, ""), "bat  ");
        }

        [Test]
        public void TestLeftPad()
        {
            Assert.AreEqual(StringUtils.LeftPad(null, 0), null);
            Assert.AreEqual(StringUtils.LeftPad("", 3), "   ");
            Assert.AreEqual(StringUtils.LeftPad("bat", 3), "bat");
            Assert.AreEqual(StringUtils.LeftPad("bat", 5), "  bat");
            Assert.AreEqual(StringUtils.LeftPad("bat", 1), "bat");
            Assert.AreEqual(StringUtils.LeftPad("bat", -1), "bat");
        }

        [Test]
        public void TestLeftPadByString()
        {
            Assert.AreEqual(StringUtils.LeftPad(null, 3, "z"), null);
            Assert.AreEqual(StringUtils.LeftPad("", 3, "z"), "zzz");
            Assert.AreEqual(StringUtils.LeftPad("bat", 3, "yz"), "bat");
            Assert.AreEqual(StringUtils.LeftPad("bat", 5, "yz"), "yzbat");
            Assert.AreEqual(StringUtils.LeftPad("bat", 8, "yz"), "yzyzybat");
            Assert.AreEqual(StringUtils.LeftPad("bat", 1, "yz"), "bat");
            Assert.AreEqual(StringUtils.LeftPad("bat", -1, "yz"), "bat");
            Assert.AreEqual(StringUtils.LeftPad("bat", 5, null), "  bat");
            Assert.AreEqual(StringUtils.LeftPad("bat", 5, ""), "  bat");
        }

        [Test]
        public void TestLeftPadByChar()
        {
            Assert.AreEqual(StringUtils.LeftPad(null, 3, 'z'), null);
            Assert.AreEqual(StringUtils.LeftPad("", 3, 'z'), "zzz");
            Assert.AreEqual(StringUtils.LeftPad("bat", 3, 'z'), "bat");
            Assert.AreEqual(StringUtils.LeftPad("bat", 5, 'z'), "zzbat");
            Assert.AreEqual(StringUtils.LeftPad("bat", 1, 'z'), "bat");
            Assert.AreEqual(StringUtils.LeftPad("bat", -1, 'z'), "bat");
        }

        [Test]
        public void TestLength()
        {
            Assert.AreEqual(StringUtils.Length("bat"), 3);
            Assert.AreEqual(StringUtils.Length(""), 0);
            Assert.AreEqual(StringUtils.Length(null), 0);
        }

        [Test]
        public void TestCenter()
        {
            Assert.AreEqual(StringUtils.Center(null, 0), null);
            Assert.AreEqual(StringUtils.Center("", 4), "    ");
            Assert.AreEqual(StringUtils.Center("ab", -1), "ab");
            Assert.AreEqual(StringUtils.Center("ab", 4), " ab ");
            Assert.AreEqual(StringUtils.Center("abcd", 2), "abcd");
            Assert.AreEqual(StringUtils.Center("a", 4), " a  ");
        }

        [Test]
        public void TestCenterByChar()
        {
            Assert.AreEqual(StringUtils.Center(null, 4, ' '), null);
            Assert.AreEqual(StringUtils.Center("", 4, ' '), "    ");
            Assert.AreEqual(StringUtils.Center("ab", -1, ' '), "ab");
            Assert.AreEqual(StringUtils.Center("ab", 4, ' '), " ab ");
            Assert.AreEqual(StringUtils.Center("abcd", 2, ' '), "abcd");
            Assert.AreEqual(StringUtils.Center("a", 4, ' '), " a  ");
            Assert.AreEqual(StringUtils.Center("a", 4, 'y'), "yayy");
        }

        [Test]
        public void TestCenterByString()
        {
            Assert.AreEqual(StringUtils.Center(null, 4, " "), null);
            Assert.AreEqual(StringUtils.Center("", 4, " "), "    ");
            Assert.AreEqual(StringUtils.Center("ab", -1, " "), "ab");
            Assert.AreEqual(StringUtils.Center("ab", 4, " "), " ab ");
            Assert.AreEqual(StringUtils.Center("abcd", 2, " "), "abcd");
            Assert.AreEqual(StringUtils.Center("a", 4, " "), " a  ");
            Assert.AreEqual(StringUtils.Center("a", 4, "yz"), "yayz");
            Assert.AreEqual(StringUtils.Center("abc", 7, null), "  abc  ");
            Assert.AreEqual(StringUtils.Center("abc", 7, ""), "  abc  ");
        }

        #endregion

        #region Casing

        [Test]
        public void TestUpperCase()
        {
            Assert.AreEqual(StringUtils.UpperCase(null), null);
            Assert.AreEqual(StringUtils.UpperCase(""), "");
            Assert.AreEqual(StringUtils.UpperCase("abc"), "ABC");
        }

        [Test]
        public void TestLowerCase()
        {
            Assert.AreEqual(StringUtils.LowerCase(null), null);
            Assert.AreEqual(StringUtils.LowerCase(""), "");
            Assert.AreEqual(StringUtils.LowerCase("AbC"), "abc");
        }


        [Test]
        public void TestCapitalize()
        {
            Assert.AreEqual(StringUtils.Capitalize(null), null);
            Assert.AreEqual(StringUtils.Capitalize(""), "");
            Assert.AreEqual(StringUtils.Capitalize("cat"), "Cat");
            Assert.AreEqual(StringUtils.Capitalize("cAt"), "CAt");
        }

        [Test]
        public void TestUnCapitalize()
        {
            Assert.AreEqual(StringUtils.UnCapitalize(null), null);
            Assert.AreEqual(StringUtils.UnCapitalize(""), "");
            Assert.AreEqual(StringUtils.UnCapitalize("cat"), "cat");
            Assert.AreEqual(StringUtils.UnCapitalize("Cat"), "cat");
            Assert.AreEqual(StringUtils.UnCapitalize("CAT"), "cAT");
        }

        [Test]
        public void TestSwapCase()
        {
            Assert.AreEqual(StringUtils.SwapCase(null), null);
            Assert.AreEqual(StringUtils.SwapCase(""), "");
            Assert.AreEqual(StringUtils.SwapCase("Good Day"), "gOOD dAY");
        }

        [Test]
        public void TestCountMatches()
        {
            Assert.AreEqual(StringUtils.CountMatches(null, "A"), 0);
            Assert.AreEqual(StringUtils.CountMatches("", "a"), 0);
            Assert.AreEqual(StringUtils.CountMatches("abba", null), 0);
            Assert.AreEqual(StringUtils.CountMatches("abba", ""), 0);
            Assert.AreEqual(StringUtils.CountMatches("abba", "a"), 2);
            Assert.AreEqual(StringUtils.CountMatches("abba", "ab"), 1);
            Assert.AreEqual(StringUtils.CountMatches("abba", "xxx"), 0);
        }

        [Test]
        public void TestCountMatchesByChar()
        {
            Assert.AreEqual(StringUtils.CountMatches(null, '0'), 0);
            Assert.AreEqual(StringUtils.CountMatches("", '0'), 0);
            Assert.AreEqual(StringUtils.CountMatches("abba", '0'), 0);
            Assert.AreEqual(StringUtils.CountMatches("abba", 'a'), 2);
            Assert.AreEqual(StringUtils.CountMatches("abba", 'b'), 2);
            Assert.AreEqual(StringUtils.CountMatches("abba", 'x'), 0);
        }

        #endregion

        #region Checkings

        [Test]
        public void TestIsAlpha()
        {
            Assert.AreEqual(StringUtils.IsAlpha(null), false);
            Assert.AreEqual(StringUtils.IsAlpha(""), false);
            Assert.AreEqual(StringUtils.IsAlpha("  "), false);
            Assert.AreEqual(StringUtils.IsAlpha("abc"), true);
            Assert.AreEqual(StringUtils.IsAlpha("ab2c"), false);
            Assert.AreEqual(StringUtils.IsAlpha("ab-c"), false);
        }

        [Test]
        public void TestIsAlphaSpace()
        {
            Assert.AreEqual(StringUtils.IsAlphaSpace(null), false);
            Assert.AreEqual(StringUtils.IsAlphaSpace(""), true);
            Assert.AreEqual(StringUtils.IsAlphaSpace("  "), true);
            Assert.AreEqual(StringUtils.IsAlphaSpace("abc"), true);
            Assert.AreEqual(StringUtils.IsAlphaSpace("ab c"), true);
            Assert.AreEqual(StringUtils.IsAlphaSpace("ab2c"), false);
            Assert.AreEqual(StringUtils.IsAlphaSpace("ab-c"), false);
        }

        [Test]
        public void TestIsAlphaNumeric()
        {
            Assert.AreEqual(StringUtils.IsAlphaNumeric(null), false);
            Assert.AreEqual(StringUtils.IsAlphaNumeric(""), false);
            Assert.AreEqual(StringUtils.IsAlphaNumeric("  "), false);
            Assert.AreEqual(StringUtils.IsAlphaNumeric("abc"), true);
            Assert.AreEqual(StringUtils.IsAlphaNumeric("ab c"), false);
            Assert.AreEqual(StringUtils.IsAlphaNumeric("ab2c"), true);
            Assert.AreEqual(StringUtils.IsAlphaNumeric("ab-c"), false);
        }

        [Test]
        public void TestIsAlphaNumericSpace()
        {
            Assert.AreEqual(StringUtils.IsAlphaNumericSpace(null), false);
            Assert.AreEqual(StringUtils.IsAlphaNumericSpace(""), true);
            Assert.AreEqual(StringUtils.IsAlphaNumericSpace("  "), true);
            Assert.AreEqual(StringUtils.IsAlphaNumericSpace("abc"), true);
            Assert.AreEqual(StringUtils.IsAlphaNumericSpace("ab c"), true);
            Assert.AreEqual(StringUtils.IsAlphaNumericSpace("ab2c"), true);
            Assert.AreEqual(StringUtils.IsAlphaNumericSpace("ab-c"), false);
        }

        [Test]
        public void TestIsAsciiPrintable()
        {
            Assert.AreEqual(StringUtils.IsAsciiPrintable(null), false);
            Assert.AreEqual(StringUtils.IsAsciiPrintable(""), true);
            Assert.AreEqual(StringUtils.IsAsciiPrintable(" "), true);
            Assert.AreEqual(StringUtils.IsAsciiPrintable("Ceki"), true);
            Assert.AreEqual(StringUtils.IsAsciiPrintable("ab2c"), true);
            Assert.AreEqual(StringUtils.IsAsciiPrintable("!ab-c~"), true);
            Assert.AreEqual(StringUtils.IsAsciiPrintable("\u0020"), true);
            Assert.AreEqual(StringUtils.IsAsciiPrintable("\u0021"), true);
            Assert.AreEqual(StringUtils.IsAsciiPrintable("\u007e"), true);
            Assert.AreEqual(StringUtils.IsAsciiPrintable("\u007f"), false);
            Assert.AreEqual(StringUtils.IsAsciiPrintable("Ceki G\u00fclc\u007f"), false);
        }

        [Test]
        public void TestIsNumericSpace()
        {
            Assert.AreEqual(StringUtils.IsNumericSpace(null), false);
            Assert.AreEqual(StringUtils.IsNumericSpace(""), false);
            Assert.AreEqual(StringUtils.IsNumericSpace("  "), true);
            Assert.AreEqual(StringUtils.IsNumericSpace("123"), true);
            Assert.AreEqual(StringUtils.IsNumericSpace("12 3"), true);
            Assert.AreEqual(StringUtils.IsNumeric("\u0967\u0968\u0969"), true);
            Assert.AreEqual(StringUtils.IsNumeric("\u0967\u0968\u0969"), true);
            Assert.AreEqual(StringUtils.IsNumericSpace("ab2c"), false);
            Assert.AreEqual(StringUtils.IsNumericSpace("12-3"), false);
            Assert.AreEqual(StringUtils.IsNumericSpace("12.3"), false);
        }

        [Test]
        public void TestIsNumeric()
        {
            Assert.AreEqual(StringUtils.IsNumeric(null), false);
            Assert.AreEqual(StringUtils.IsNumeric(""), false);
            Assert.AreEqual(StringUtils.IsNumeric("  "), false);
            Assert.AreEqual(StringUtils.IsNumeric("123"), true);
            Assert.AreEqual(StringUtils.IsNumeric("\u0967\u0968\u0969"), true);
            Assert.AreEqual(StringUtils.IsNumeric("12 3"), false);
            Assert.AreEqual(StringUtils.IsNumeric("ab2c"), false);
            Assert.AreEqual(StringUtils.IsNumeric("12-3"), false);
            Assert.AreEqual(StringUtils.IsNumeric("12.3"), false);
            Assert.AreEqual(StringUtils.IsNumeric("-123"), false);
            Assert.AreEqual(StringUtils.IsNumeric("+123"), false);
        }

        [Test]
        public void TestIsWhiteSpace()
        {
            Assert.AreEqual(StringUtils.IsWhiteSpace(null), false);
            Assert.AreEqual(StringUtils.IsWhiteSpace(""), true);
            Assert.AreEqual(StringUtils.IsWhiteSpace("  "), true);
            Assert.AreEqual(StringUtils.IsWhiteSpace("abc"), false);
            Assert.AreEqual(StringUtils.IsWhiteSpace("ab2c"), false);
            Assert.AreEqual(StringUtils.IsWhiteSpace("ab-c"), false);
        }


        [Test]
        public void TestIsAllLowerCase()
        {
            Assert.AreEqual(StringUtils.IsAllLowerCase(null), false);
            Assert.AreEqual(StringUtils.IsAllLowerCase(""), false);
            Assert.AreEqual(StringUtils.IsAllLowerCase("  "), false);
            Assert.AreEqual(StringUtils.IsAllLowerCase("abc"), true);
            Assert.AreEqual(StringUtils.IsAllLowerCase("abC"), false);
            Assert.AreEqual(StringUtils.IsAllLowerCase("ab c"), false);
            Assert.AreEqual(StringUtils.IsAllLowerCase("ab1c"), false);
            Assert.AreEqual(StringUtils.IsAllLowerCase("ab/c"), false);
        }


        [Test]
        public void TestIsAllUpperCase()
        {
            Assert.AreEqual(StringUtils.IsAllUpperCase(null), false);
            Assert.AreEqual(StringUtils.IsAllUpperCase(""), false);
            Assert.AreEqual(StringUtils.IsAllUpperCase("  "), false);
            Assert.AreEqual(StringUtils.IsAllUpperCase("ABC"), true);
            Assert.AreEqual(StringUtils.IsAllUpperCase("aBC"), false);
            Assert.AreEqual(StringUtils.IsAllUpperCase("A C"), false);
            Assert.AreEqual(StringUtils.IsAllUpperCase("A1C"), false);
            Assert.AreEqual(StringUtils.IsAllUpperCase("A/C"), false);
        }


        [Test]
        public void TestDefaultString()
        {
            Assert.AreEqual(StringUtils.DefaultString(null), "");
            Assert.AreEqual(StringUtils.DefaultString(""), "");
            Assert.AreEqual(StringUtils.DefaultString("bat"), "bat");
        }


        [Test]
        public void TestDefaultIfBlank()
        {
            Assert.AreEqual(StringUtils.DefaultIfBlank(null, "abc"), "abc");
            Assert.AreEqual(StringUtils.DefaultIfBlank(""), "");
            Assert.AreEqual(StringUtils.DefaultIfBlank(" "), "");
            Assert.AreEqual(StringUtils.DefaultIfBlank("bat", "abc"), "bat");
            Assert.AreEqual(StringUtils.DefaultIfBlank("", null), null);
        }


        [Test]
        public void TestDefaultIfEmpty()
        {
            Assert.AreEqual(StringUtils.DefaultIfEmpty(null, "NULL"), "NULL");
            Assert.AreEqual(StringUtils.DefaultIfEmpty("", "NULL"), "NULL");
            Assert.AreEqual(StringUtils.DefaultIfEmpty(" ", "NULL"), " ");
            Assert.AreEqual(StringUtils.DefaultIfEmpty("bat", "NULL"), "bat");
            Assert.AreEqual(StringUtils.DefaultIfEmpty("", null), null);
        }


        [Test]
        public void TestRotate()
        {
            Assert.AreEqual(StringUtils.Rotate(null, 0), null);
            Assert.AreEqual(StringUtils.Rotate("", 0), "");
            Assert.AreEqual(StringUtils.Rotate("abcdefg", 0), "abcdefg");
            Assert.AreEqual(StringUtils.Rotate("abcdefg", 2), "fgabcde");
            Assert.AreEqual(StringUtils.Rotate("abcdefg", -2), "cdefgab");
            Assert.AreEqual(StringUtils.Rotate("abcdefg", 7), "abcdefg");
            Assert.AreEqual(StringUtils.Rotate("abcdefg", -7), "abcdefg");
            Assert.AreEqual(StringUtils.Rotate("abcdefg", 9), "fgabcde");
            Assert.AreEqual(StringUtils.Rotate("abcdefg", -9), "cdefgab");
        }


        [Test]
        public void TestReverse()
        {
            Assert.AreEqual(StringUtils.Reverse(null), null);
            Assert.AreEqual(StringUtils.Reverse(""), "");
            Assert.AreEqual(StringUtils.Reverse("bat"), "tab");
        }


        [Test]
        public void TestReverseDelimited()
        {
            Assert.AreEqual(StringUtils.ReverseDelimited(null, 'x'), null);
            Assert.AreEqual(StringUtils.ReverseDelimited("", 'x'), "");
            Assert.AreEqual(StringUtils.ReverseDelimited("a.b.c", 'x'), "a.b.c");
            Assert.AreEqual(StringUtils.ReverseDelimited("a.b.c", '.'), "c.b.a");
        }

        #endregion

        #region Abbreviate

        [Test]
        public void TestAbbreviate()
        {
            Assert.AreEqual(StringUtils.Abbreviate(null, 0), null);
            Assert.AreEqual(StringUtils.Abbreviate("", 4), "");
            Assert.AreEqual(StringUtils.Abbreviate("abcdefg", 6), "abc...");
            Assert.AreEqual(StringUtils.Abbreviate("abcdefg", 7), "abcdefg");
            Assert.AreEqual(StringUtils.Abbreviate("abcdefg", 8), "abcdefg");
            Assert.AreEqual(StringUtils.Abbreviate("abcdefg", 4), "a...");
            Assert.Throws<ArgumentException>(() => StringUtils.Abbreviate("abcdefg", 3));
        }


        [Test]
        public void TestAbbreviateByMaxWidth()
        {
            Assert.AreEqual(StringUtils.Abbreviate(null, 0, 4), null);
            Assert.AreEqual(StringUtils.Abbreviate("", 0, 4), "");
            Assert.AreEqual(StringUtils.Abbreviate("abcdefghijklmno", -1, 10), "abcdefg...");
            Assert.AreEqual(StringUtils.Abbreviate("abcdefghijklmno", 0, 10), "abcdefg...");
            Assert.AreEqual(StringUtils.Abbreviate("abcdefghijklmno", 1, 10), "abcdefg...");
            Assert.AreEqual(StringUtils.Abbreviate("abcdefghijklmno", 4, 10), "abcdefg...");
            Assert.AreEqual(StringUtils.Abbreviate("abcdefghijklmno", 5, 10), "...fghi...");
            Assert.AreEqual(StringUtils.Abbreviate("abcdefghijklmno", 6, 10), "...ghij...");
            Assert.AreEqual(StringUtils.Abbreviate("abcdefghijklmno", 8, 10), "...ijklmno");
            Assert.AreEqual(StringUtils.Abbreviate("abcdefghijklmno", 10, 10), "...ijklmno");
            Assert.AreEqual(StringUtils.Abbreviate("abcdefghijklmno", 12, 10), "...ijklmno");
            Assert.Throws<ArgumentException>(() => StringUtils.Abbreviate("abcdefghij", 0, 3));
            Assert.Throws<ArgumentException>(() => StringUtils.Abbreviate("abcdefghij", 5, 6));
        }


        [Test]
        public void TestAbbreviateMiddle()
        {
            Assert.AreEqual(StringUtils.AbbreviateMiddle(null, null, 0), null);
            Assert.AreEqual(StringUtils.AbbreviateMiddle("abc", null, 0), "abc");
            Assert.AreEqual(StringUtils.AbbreviateMiddle("abc", ".", 0), "abc");
            Assert.AreEqual(StringUtils.AbbreviateMiddle("abc", ".", 3), "abc");
            Assert.AreEqual(StringUtils.AbbreviateMiddle("abcdef", ".", 4), "ab.f");
        }

        #endregion

        #region Difference



        [Test]
        public void TestDifference()
        {
            Assert.AreEqual(StringUtils.Difference(null, null), null);
            Assert.AreEqual(StringUtils.Difference("", ""), "");
            Assert.AreEqual(StringUtils.Difference("", "abc"), "abc");
            Assert.AreEqual(StringUtils.Difference("abc", ""), "");
            Assert.AreEqual(StringUtils.Difference("abc", "abc"), "");
            Assert.AreEqual(StringUtils.Difference("abc", "ab"), "");
            Assert.AreEqual(StringUtils.Difference("ab", "abxyz"), "xyz");
            Assert.AreEqual(StringUtils.Difference("abcde", "abxyz"), "xyz");
            Assert.AreEqual(StringUtils.Difference("abcde", "xyz"), "xyz");
        }


        [Test]
        public void TestIndexOfDifference()
        {
            Assert.AreEqual(StringUtils.IndexOfDifference(null, null), -1);
            Assert.AreEqual(StringUtils.IndexOfDifference("", ""), -1);
            Assert.AreEqual(StringUtils.IndexOfDifference("", "abc"), 0);
            Assert.AreEqual(StringUtils.IndexOfDifference("abc", ""), 0);
            Assert.AreEqual(StringUtils.IndexOfDifference("abc", "abc"), -1);
            Assert.AreEqual(StringUtils.IndexOfDifference("ab", "abxyz"), 2);
            Assert.AreEqual(StringUtils.IndexOfDifference("abcde", "abxyz"), 2);
            Assert.AreEqual(StringUtils.IndexOfDifference("abcde", "xyz"), 0);
        }


        [Test]
        public void TestIndexOfDifferenceByStringArray()
        {
            Assert.AreEqual(StringUtils.IndexOfDifference(null), -1);
            Assert.AreEqual(StringUtils.IndexOfDifference(new string[] { }), -1);
            Assert.AreEqual(StringUtils.IndexOfDifference(new[] { "abc" }), -1);
            Assert.AreEqual(StringUtils.IndexOfDifference(new string[] { null, null }), -1);
            Assert.AreEqual(StringUtils.IndexOfDifference(new[] { "", "" }), -1);
            Assert.AreEqual(StringUtils.IndexOfDifference(new[] { "", null }), 0);
            Assert.AreEqual(StringUtils.IndexOfDifference(new[] { "abc", null, null }), 0);
            Assert.AreEqual(StringUtils.IndexOfDifference(new[] { null, null, "abc" }), 0);
            Assert.AreEqual(StringUtils.IndexOfDifference(new[] { "", "abc" }), 0);
            Assert.AreEqual(StringUtils.IndexOfDifference(new[] { "abc", "" }), 0);
            Assert.AreEqual(StringUtils.IndexOfDifference(new[] { "abc", "abc" }), -1);
            Assert.AreEqual(StringUtils.IndexOfDifference(new[] { "abc", "a" }), 1);
            Assert.AreEqual(StringUtils.IndexOfDifference(new[] { "ab", "abxyz" }), 2);
            Assert.AreEqual(StringUtils.IndexOfDifference(new[] { "abcde", "abxyz" }), 2);
            Assert.AreEqual(StringUtils.IndexOfDifference(new[] { "abcde", "xyz" }), 0);
            Assert.AreEqual(StringUtils.IndexOfDifference(new[] { "xyz", "abcde" }), 0);
            Assert.AreEqual(StringUtils.IndexOfDifference(new[] { "i am a machine", "i am a robot" }), 7);
        }


        [Test]
        public void TestGetCommonPrefix()
        {
            Assert.AreEqual(StringUtils.GetCommonPrefix(null), "");
            Assert.AreEqual(StringUtils.GetCommonPrefix(new string[] { }), "");
            Assert.AreEqual(StringUtils.GetCommonPrefix(new[] { "abc" }), "abc");
            Assert.AreEqual(StringUtils.GetCommonPrefix(new string[] { null, null }), "");
            Assert.AreEqual(StringUtils.GetCommonPrefix(new[] { "", "" }), "");
            Assert.AreEqual(StringUtils.GetCommonPrefix(new[] { "", null }), "");
            Assert.AreEqual(StringUtils.GetCommonPrefix(new[] { "abc", null, null }), "");
            Assert.AreEqual(StringUtils.GetCommonPrefix(new[] { null, null, "abc" }), "");
            Assert.AreEqual(StringUtils.GetCommonPrefix(new[] { "", "abc" }), "");
            Assert.AreEqual(StringUtils.GetCommonPrefix(new[] { "abc", "" }), "");
            Assert.AreEqual(StringUtils.GetCommonPrefix(new[] { "abc", "abc" }), "abc");
            Assert.AreEqual(StringUtils.GetCommonPrefix(new[] { "abc", "a" }), "a");
            Assert.AreEqual(StringUtils.GetCommonPrefix(new[] { "ab", "abxyz" }), "ab");
            Assert.AreEqual(StringUtils.GetCommonPrefix(new[] { "abcde", "abxyz" }), "ab");
            Assert.AreEqual(StringUtils.GetCommonPrefix(new[] { "abcde", "xyz" }), "");
            Assert.AreEqual(StringUtils.GetCommonPrefix(new[] { "xyz", "abcde" }), "");
            Assert.AreEqual(StringUtils.GetCommonPrefix(new[] { "i am a machine", "i am a robot" }), "i am a ");
        }

        #endregion

        #region StartsWith & EndsWith


        [Test]
        public void TestStartsWithIgnoreCase()
        {
            Assert.AreEqual(StringUtils.StartsWithIgnoreCase(null, null), true);
            Assert.AreEqual(StringUtils.StartsWithIgnoreCase(null, "abc"), false);
            Assert.AreEqual(StringUtils.StartsWithIgnoreCase("abcdef", null), false);
            Assert.AreEqual(StringUtils.StartsWithIgnoreCase("abcdef", "abc"), true);
            Assert.AreEqual(StringUtils.StartsWithIgnoreCase("ABCDEF", "abc"), true);
        }


        [Test]
        public void TestStartsWith()
        {
            Assert.AreEqual(StringUtils.StartsWith(null, null), true);
            Assert.AreEqual(StringUtils.StartsWith(null, "abc"), false);
            Assert.AreEqual(StringUtils.StartsWith("abcdef", null), false);
            Assert.AreEqual(StringUtils.StartsWith("abcdef", "abc"), true);
            Assert.AreEqual(StringUtils.StartsWith("ABCDEF", "abc"), false);
        }


        [Test]
        public void TestStartsWithAny()
        {
            Assert.AreEqual(StringUtils.StartsWithAny(null, false, null), false);
            Assert.AreEqual(StringUtils.StartsWithAny(null, false, new[] { "abc" }), false);
            Assert.AreEqual(StringUtils.StartsWithAny("abcxyz", false, null), false);
            Assert.AreEqual(StringUtils.StartsWithAny("abcxyz", false, new[] { "" }), true);
            Assert.AreEqual(StringUtils.StartsWithAny("abcxyz", false, new[] { "abc" }), true);
            Assert.AreEqual(StringUtils.StartsWithAny("abcxyz", false, new[] { null, "xyz", "abc" }), true);
        }


        [Test]
        public void TestEndsWithIgnoreCase()
        {
            Assert.AreEqual(StringUtils.EndsWithIgnoreCase(null, null), true);
            Assert.AreEqual(StringUtils.EndsWithIgnoreCase(null, "def"), false);
            Assert.AreEqual(StringUtils.EndsWithIgnoreCase("abcdef", null), false);
            Assert.AreEqual(StringUtils.EndsWithIgnoreCase("abcdef", "def"), true);
            Assert.AreEqual(StringUtils.EndsWithIgnoreCase("ABCDEF", "def"), true);
            Assert.AreEqual(StringUtils.EndsWithIgnoreCase("ABCDEF", "cde"), false);
        }


        [Test]
        public void TestEndsWith()
        {
            Assert.AreEqual(StringUtils.EndsWith(null, null), true);
            Assert.AreEqual(StringUtils.EndsWith(null, "def"), false);
            Assert.AreEqual(StringUtils.EndsWith("abcdef", null), false);
            Assert.AreEqual(StringUtils.EndsWith("abcdef", "def"), true);
            Assert.AreEqual(StringUtils.EndsWith("ABCDEF", "def"), false);
            Assert.AreEqual(StringUtils.EndsWith("ABCDEF", "cde"), false);
        }

        [Test]
        public void TestEndsWithAny()
        {
            Assert.AreEqual(StringUtils.EndsWithAny(null, false, null), false);
            Assert.AreEqual(StringUtils.EndsWithAny(null, false, new[] { "abc" }), false);
            Assert.AreEqual(StringUtils.EndsWithAny("abcxyz", false, null), false);
            Assert.AreEqual(StringUtils.EndsWithAny("abcxyz", false, new[] { "" }), true);
            Assert.AreEqual(StringUtils.EndsWithAny("abcxyz", false, new[] { "xyz" }), true);
            Assert.AreEqual(StringUtils.EndsWithAny("abcxyz", false, new[] { null, "xyz", "abc" }), true);
        }

        #endregion

        #region Misc

        [Test]
        public void TestNormalizeSpace()
        {
            Assert.AreEqual(StringUtils.NormalizeSpace(null), null);
            Assert.AreEqual(StringUtils.NormalizeSpace(" apple "), "apple");
            Assert.AreEqual(StringUtils.NormalizeSpace(" apple  is   good for   health "), "apple is good for health");
        }


        #endregion

        #region Append and Prepend

        [Test]
        public void TestAppendIfMissing()
        {
            Assert.AreEqual(StringUtils.AppendIfMissing(null, null), null);
            Assert.AreEqual(StringUtils.AppendIfMissing("abc", null), "abc");
            Assert.AreEqual(StringUtils.AppendIfMissing("", "xyz"), "xyz");
            Assert.AreEqual(StringUtils.AppendIfMissing("abc", "xyz"), "abcxyz");
            Assert.AreEqual(StringUtils.AppendIfMissing("abcxyz", "xyz"), "abcxyz");
            Assert.AreEqual(StringUtils.AppendIfMissing("abcXYZ", "xyz"), "abcXYZxyz");
            Assert.AreEqual(StringUtils.AppendIfMissing(null, null, true), null);
            Assert.AreEqual(StringUtils.AppendIfMissing("abc", null, true), "abc");
            Assert.AreEqual(StringUtils.AppendIfMissing("", "xyz", true), "xyz");
            Assert.AreEqual(StringUtils.AppendIfMissing("abc", "xyz", false, new string[] { null }), "abcxyz");
            Assert.AreEqual(StringUtils.AppendIfMissing("abcxyz", "xyz", false, new[] { "mno" }), "abcxyz");
            Assert.AreEqual(StringUtils.AppendIfMissing("abcmno", "xyz", false, new[] { "mno" }), "abcmno");
            Assert.AreEqual(StringUtils.AppendIfMissing("abcXYZ", "xyz", false, new[] { "mno" }), "abcXYZxyz");
            Assert.AreEqual(StringUtils.AppendIfMissing("abcMNO", "xyz", false, new[] { "mno" }), "abcMNOxyz");
        }

        [Test]
        public void TestAppendIfMissingIgnoreCase()
        {
            Assert.AreEqual(StringUtils.AppendIfMissingIgnoreCase(null, null), null);
            Assert.AreEqual(StringUtils.AppendIfMissingIgnoreCase("abc", null), "abc");
            Assert.AreEqual(StringUtils.AppendIfMissingIgnoreCase("", "xyz"), "xyz");
            Assert.AreEqual(StringUtils.AppendIfMissingIgnoreCase("abc", "xyz"), "abcxyz");
            Assert.AreEqual(StringUtils.AppendIfMissingIgnoreCase("abcxyz", "xyz"), "abcxyz");
            Assert.AreEqual(StringUtils.AppendIfMissingIgnoreCase("abcXYZ", "xyz"), "abcXYZ");
            Assert.AreEqual(StringUtils.AppendIfMissingIgnoreCase(null, null), null);
            Assert.AreEqual(StringUtils.AppendIfMissingIgnoreCase("abc", null), "abc");
            Assert.AreEqual(StringUtils.AppendIfMissingIgnoreCase("", "xyz"), "xyz");
            Assert.AreEqual(StringUtils.AppendIfMissingIgnoreCase("abc", "xyz", new string[] { null }), "abcxyz");
            Assert.AreEqual(StringUtils.AppendIfMissingIgnoreCase("abc", "xyz", new[] { "" }), "abc");
            Assert.AreEqual(StringUtils.AppendIfMissingIgnoreCase("abc", "xyz", new[] { "mno" }), "abcxyz");
            Assert.AreEqual(StringUtils.AppendIfMissingIgnoreCase("abcxyz", "xyz", new[] { "mno" }), "abcxyz");
            Assert.AreEqual(StringUtils.AppendIfMissingIgnoreCase("abcmno", "xyz", new[] { "mno" }), "abcmno");
            Assert.AreEqual(StringUtils.AppendIfMissingIgnoreCase("abcXYZ", "xyz", new[] { "mno" }), "abcXYZ");
            Assert.AreEqual(StringUtils.AppendIfMissingIgnoreCase("abcMNO", "xyz", new[] { "mno" }), "abcMNO");
        }

        [Test]
        public void TestPrependIfMissing()
        {
            Assert.AreEqual(StringUtils.PrependIfMissing(null, null), null);
            Assert.AreEqual(StringUtils.PrependIfMissing("abc", null), "abc");
            Assert.AreEqual(StringUtils.PrependIfMissing("", "xyz"), "xyz");
            Assert.AreEqual(StringUtils.PrependIfMissing("abc", "xyz"), "xyzabc");
            Assert.AreEqual(StringUtils.PrependIfMissing("xyzabc", "xyz"), "xyzabc");
            Assert.AreEqual(StringUtils.PrependIfMissing("XYZabc", "xyz"), "xyzXYZabc");
            Assert.AreEqual(StringUtils.PrependIfMissing(null, null, true), null);
            Assert.AreEqual(StringUtils.PrependIfMissing("abc", null), "abc");
            Assert.AreEqual(StringUtils.PrependIfMissing("", "xyz"), "xyz");
            Assert.AreEqual(StringUtils.PrependIfMissing("abc", "xyz", false, new string[] { null }), "xyzabc");
            Assert.AreEqual(StringUtils.PrependIfMissing("abc", "xyz", false, new[] { "" }), "abc");
            Assert.AreEqual(StringUtils.PrependIfMissing("abc", "xyz", false, new[] { "mno" }), "xyzabc");
            Assert.AreEqual(StringUtils.PrependIfMissing("xyzabc", "xyz", false, new[] { "mno" }), "xyzabc");
            Assert.AreEqual(StringUtils.PrependIfMissing("mnoabc", "xyz", false, new[] { "mno" }), "mnoabc");
            Assert.AreEqual(StringUtils.PrependIfMissing("XYZabc", "xyz", false, new[] { "mno" }), "xyzXYZabc");
            Assert.AreEqual(StringUtils.PrependIfMissing("MNOabc", "xyz", false, new[] { "mno" }), "xyzMNOabc");
        }

        [Test]
        public void TestPrependIfMissingIgnoreCase()
        {
            Assert.AreEqual(StringUtils.PrependIfMissingIgnoreCase(null, null), null);
            Assert.AreEqual(StringUtils.PrependIfMissingIgnoreCase("abc", null), "abc");
            Assert.AreEqual(StringUtils.PrependIfMissingIgnoreCase("", "xyz"), "xyz");
            Assert.AreEqual(StringUtils.PrependIfMissingIgnoreCase("abc", "xyz"), "xyzabc");
            Assert.AreEqual(StringUtils.PrependIfMissingIgnoreCase("xyzabc", "xyz"), "xyzabc");
            Assert.AreEqual(StringUtils.PrependIfMissingIgnoreCase("XYZabc", "xyz"), "XYZabc");
            Assert.AreEqual(StringUtils.PrependIfMissingIgnoreCase(null, null), null);
            Assert.AreEqual(StringUtils.PrependIfMissingIgnoreCase("abc", null), "abc");
            Assert.AreEqual(StringUtils.PrependIfMissingIgnoreCase("", "xyz"), "xyz");
            Assert.AreEqual(StringUtils.PrependIfMissingIgnoreCase("abc", "xyz", new string[] { null }), "xyzabc");
            Assert.AreEqual(StringUtils.PrependIfMissingIgnoreCase("abc", "xyz", new[] { "" }), "abc");
            Assert.AreEqual(StringUtils.PrependIfMissingIgnoreCase("abc", "xyz", new[] { "mno" }), "xyzabc");
            Assert.AreEqual(StringUtils.PrependIfMissingIgnoreCase("xyzabc", "xyz", new[] { "mno" }), "xyzabc");
            Assert.AreEqual(StringUtils.PrependIfMissingIgnoreCase("mnoabc", "xyz", new[] { "mno" }), "mnoabc");
            Assert.AreEqual(StringUtils.PrependIfMissingIgnoreCase("XYZabc", "xyz", new[] { "mno" }), "XYZabc");
            Assert.AreEqual(StringUtils.PrependIfMissingIgnoreCase("MNOabc", "xyz", new[] { "mno" }), "MNOabc");
        }

        [Test]
        public void TestWrap()
        {
            Assert.AreEqual(StringUtils.Wrap(null, 'a'), null);
            Assert.AreEqual(StringUtils.Wrap("", 'a'), "");
            Assert.AreEqual(StringUtils.Wrap("ab", '\0'), "ab");
            Assert.AreEqual(StringUtils.Wrap("ab", 'x'), "xabx");
            Assert.AreEqual(StringUtils.Wrap("ab", '\''), "'ab'");
            Assert.AreEqual(StringUtils.Wrap("\"ab\"", '\"'), "\"\"ab\"\"");
        }

        [Test]
        public void TestWrapByString()
        {
            Assert.AreEqual(StringUtils.Wrap(null, "a"), null);
            Assert.AreEqual(StringUtils.Wrap("", "a"), "");
            Assert.AreEqual(StringUtils.Wrap("ab", null), "ab");
            Assert.AreEqual(StringUtils.Wrap("ab", "x"), "xabx");
            Assert.AreEqual(StringUtils.Wrap("ab", "\""), "\"ab\"");
            Assert.AreEqual(StringUtils.Wrap("\"ab\"", "\""), "\"\"ab\"\"");
            Assert.AreEqual(StringUtils.Wrap("ab", "'"), "'ab'");
            Assert.AreEqual(StringUtils.Wrap("'abcd'", "'"), "''abcd''");
            Assert.AreEqual(StringUtils.Wrap("\"abcd\"", "'"), "'\"abcd\"'");
            Assert.AreEqual(StringUtils.Wrap("'abcd'", "\""), "\"'abcd'\"");
        }



        #endregion

        #region Encoding

        [Test]
        public void TestToAsciiString()
        {
            //var defaultBytes = Encoding.Default.GetBytes("Apple");
            //var ASCIBytes       = Encoding.ASCII.GetBytes("Apple");
            //var BigEBytes       = Encoding.BigEndianUnicode.GetBytes("Apple");
            //var UTF3Bytes       = Encoding.UTF32.GetBytes("Apple");
            //var UTF7Bytes       = Encoding.UTF7.GetBytes("Apple");
            //var UTF8Bytes       = Encoding.UTF8.GetBytes("Apple");
            //var UnicBytes       = Encoding.Unicode.GetBytes("Apple");
            //
            //var s= defaultBytes.Select(x => x.ToString()).Aggregate((i, j) => i +","+ j);
            //
            //Console.WriteLine("defaultBytes: " + defaultBytes.Select(x => x.ToString()).Aggregate((i, j) => i + "," + j));
            //Console.WriteLine("ASCIBytes   : " + ASCIBytes.Select(x => x.ToString()).Aggregate((i, j) => i + "," + j));
            //Console.WriteLine("BigEBytes   : " + BigEBytes.Select(x => x.ToString()).Aggregate((i, j) => i + "," + j));
            //Console.WriteLine("UTF3Bytes   : " + UTF3Bytes.Select(x => x.ToString()).Aggregate((i, j) => i + "," + j));
            //Console.WriteLine("UTF7Bytes   : " + UTF7Bytes.Select(x => x.ToString()).Aggregate((i, j) => i + "," + j));
            //Console.WriteLine("UTF8Bytes   : " + UTF8Bytes.Select(x => x.ToString()).Aggregate((i, j) => i + "," + j));
            //Console.WriteLine("UnicBytes   : " +UnicBytes.Select(x => x.ToString()).Aggregate((i, j) => i + "," + j));


            //var asciBytes = new byte[] { 65, 112, 112, 108, 101 };
            //var bigEBytes = new byte[] { 0, 65, 0, 112, 0, 112, 0, 108, 0, 101 };
            //var utf3Bytes = new byte[] { 65, 0, 0, 0, 112, 0, 0, 0, 112, 0, 0, 0, 108, 0, 0, 0, 101, 0, 0, 0 };
            //var utf7Bytes = new byte[] { 65, 112, 112, 108, 101 };
            //var utf8Bytes = new byte[] { 65, 112, 112, 108, 101 };
            //var unicBytes = new byte[] { 65, 0, 112, 0, 112, 0, 108, 0, 101, 0 };
            //var defaultBytes = new byte[] { 65, 112, 112, 108, 101 };

            Assert.AreEqual(StringUtils.ToAsciiString(null), null);
            Assert.AreEqual(StringUtils.ToAsciiString(new byte[] { }), "");
            Assert.AreEqual(StringUtils.ToAsciiString(new byte[] { 65, 112, 112, 108, 101 }), "Apple");
        }

        [Test]
        public void TestToBigEndianUnicodeString()
        {
            Assert.AreEqual(StringUtils.ToBigEndianUnicodeString(null), null);
            Assert.AreEqual(StringUtils.ToBigEndianUnicodeString(new byte[] { }), "");
            Assert.AreEqual(StringUtils.ToBigEndianUnicodeString(new byte[] { 0, 65, 0, 112, 0, 112, 0, 108, 0, 101 }), "Apple");

        }

        [Test]
        public void TestToUnicodeString()
        {
            Assert.AreEqual(StringUtils.ToUnicodeString(null), null);
            Assert.AreEqual(StringUtils.ToUnicodeString(new byte[] { }), "");
            Assert.AreEqual(StringUtils.ToUnicodeString(new byte[] { 65, 0, 112, 0, 112, 0, 108, 0, 101, 0 }), "Apple");
        }

        [Test]
        public void TestToUtf32String()
        {
            Assert.AreEqual(StringUtils.ToUtf32String(null), null);
            Assert.AreEqual(StringUtils.ToUtf32String(new byte[] { }), "");
            Assert.AreEqual(StringUtils.ToUtf32String(new byte[] { 65, 0, 0, 0, 112, 0, 0, 0, 112, 0, 0, 0, 108, 0, 0, 0, 101, 0, 0, 0 }), "Apple");


        }

        [Test]
        public void TestToUtf7String()
        {
            Assert.AreEqual(StringUtils.ToUtf7String(null), null);
            Assert.AreEqual(StringUtils.ToUtf7String(new byte[] { }), "");
            Assert.AreEqual(StringUtils.ToUtf7String(new byte[] { 65, 112, 112, 108, 101 }), "Apple");
        }

        [Test]
        public void TestToUtf8String()
        {
            Assert.AreEqual(StringUtils.ToUtf8String(null), null);
            Assert.AreEqual(StringUtils.ToUtf8String(new byte[] { }), "");
            Assert.AreEqual(StringUtils.ToUtf8String(new byte[] { 65, 112, 112, 108, 101 }), "Apple");
        }

        [Test]
        public void TestToDefaultString()
        {
            Assert.AreEqual(StringUtils.ToDefaultString(null), null);
            Assert.AreEqual(StringUtils.ToDefaultString(new byte[] { }), "");
            Assert.AreEqual(StringUtils.ToDefaultString(new byte[] { 65, 112, 112, 108, 101 }), "Apple");
        }

        [Test]
        public void TestToAsciiBytes()
        {
            Assert.AreEqual(StringUtils.ToAsciiBytes(null), null);
            Assert.AreEqual(StringUtils.ToAsciiBytes(""), new byte[] { });
            Assert.AreEqual(StringUtils.ToAsciiBytes("Apple"), new byte[] { 65, 112, 112, 108, 101 });
        }

        [Test]
        public void TestToBigEndianUnicodeBytes()
        {
            Assert.AreEqual(StringUtils.ToBigEndianUnicodeBytes(null), null);
            Assert.AreEqual(StringUtils.ToBigEndianUnicodeBytes(""), new byte[] { });
            Assert.AreEqual(StringUtils.ToBigEndianUnicodeBytes("Apple"), new byte[] { 0, 65, 0, 112, 0, 112, 0, 108, 0, 101 });
        }

        [Test]
        public void TestToUnicodeBytes()
        {
            Assert.AreEqual(StringUtils.ToUnicodeBytes(null), null);
            Assert.AreEqual(StringUtils.ToUnicodeBytes(""), new byte[] { });
            Assert.AreEqual(StringUtils.ToUnicodeBytes("Apple"), new byte[] { 65, 0, 112, 0, 112, 0, 108, 0, 101, 0 });
        }

        [Test]
        public void TestToUtf32Bytes()
        {
            Assert.AreEqual(StringUtils.ToUtf32Bytes(null), null);
            Assert.AreEqual(StringUtils.ToUtf32Bytes(""), new byte[] { });
            Assert.AreEqual(StringUtils.ToUtf32Bytes("Apple"), new byte[] { 65, 0, 0, 0, 112, 0, 0, 0, 112, 0, 0, 0, 108, 0, 0, 0, 101, 0, 0, 0 });
        }

        [Test]
        public void TestToUtf7Bytes()
        {
            Assert.AreEqual(StringUtils.ToUtf7Bytes(null), null);
            Assert.AreEqual(StringUtils.ToUtf7Bytes(""), new byte[] { });
            Assert.AreEqual(StringUtils.ToUtf7Bytes("Apple"), new byte[] { 65, 112, 112, 108, 101 });
        }

        [Test]
        public void TestToUtf8Bytes()
        {
            Assert.AreEqual(StringUtils.ToUtf8Bytes(null), null);
            Assert.AreEqual(StringUtils.ToUtf8Bytes(""), new byte[] { });
            Assert.AreEqual(StringUtils.ToUtf8Bytes("Apple"), new byte[] { 65, 112, 112, 108, 101 });
        }

        [Test]
        public void TestToDefaultBytes()
        {
            Assert.AreEqual(StringUtils.ToDefaultBytes(null), null);
            Assert.AreEqual(StringUtils.ToDefaultBytes(""), new byte[] { });
            Assert.AreEqual(StringUtils.ToDefaultBytes("Apple"), new byte[] { 65, 112, 112, 108, 101 });
        }


        #endregion
    }
}
