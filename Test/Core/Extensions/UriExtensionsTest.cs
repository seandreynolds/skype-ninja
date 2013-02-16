using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using eigenein.SkypeNinja.Core.Extensions;

namespace eigenein.SkypeNinja.Test.Core.Extensions
{
    [TestFixture]
    internal class UriExtensionsTest
    {
        [TestCase(
            "http://www.foo.com",
            new string[0])]
        [TestCase(
            "http://www.foo.com?",
            new string[0])]
        [TestCase(
            "http://www.foo.com?q=",
            new[] { "q=" })]
        [TestCase(
            "http://www.foo.com?q=search&doit=no",
            new[] { "q=search", "doit=no" })]
        [TestCase(
            "http://www.foo.com?q=search&doit=no&go=further",
            new[] { "q=search", "doit=no", "go=further" })]
        [TestCase(
            "http://www.foo.com?q=&doit=no",
            new[] { "q=", "doit=no" })]
        [TestCase(
            "http://www.foo.com?q=&doit=no&=&test&good=true",
            new[] { "q=", "doit=no", "good=true" })]
        [TestCase(
            "http://www.foo.com?q=search&doit=no#validationError",
            new[] { "q=search", "doit=no" })]
        [TestCase(
            "http://www.foo.com?q=search&doit=no#validationError=true",
            new[] { "q=search", "doit=no" })]
        [TestCase(
            "http://www.foo.com?q=se%20arch&do%20it=no",
            new[] { "q=se arch", "do it=no" })]
        [TestCase(
            "http://www.foo.com?q=search&doit=#validationError",
            new[] { "q=search", "doit=" })]
        public void TestGetQueryParameters(string uriString, string[] expectedStrings)
        {
            Dictionary<string, string> expected = 
                expectedStrings.Select(item => item.Split('=')).ToDictionary(pair => pair[0], pair => pair[1]);
            CollectionAssert.AreEquivalent(
                expected,
                new Uri(uriString).GetQueryParameters(),
                "Parameters values do not match.");
        }
    }
}
