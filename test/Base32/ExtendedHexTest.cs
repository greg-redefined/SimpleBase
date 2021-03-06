﻿/*
     Copyright 2014-2016 Sedat Kapanoglu

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/
using System;
using System.Text;
using SimpleBase;
using NUnit.Framework;

namespace SimpleBaseTest
{
    [TestFixture]
    class ExtendedHexTest
    {
        private static string[][] testData = new[]
        {
            new[] { "", "" },
            new[] { "f", "CO======" },
            new[] { "fo", "CPNG====" },
            new[] { "foo", "CPNMU===" },
            new[] { "foob", "CPNMUOG=" },
            new[] { "fooba", "CPNMUOJ1" },
            new[] { "foobar", "CPNMUOJ1E8======" },
            new[] { "1234567890123456789012345678901234567890", "64P36D1L6ORJGE9G64P36D1L6ORJGE9G64P36D1L6ORJGE9G64P36D1L6ORJGE9G" },
        };

        [Test]
        [TestCaseSource("testData")]
        public void Encode_ReturnsExpectedValues(string input, string expectedOutput)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(input);
            string result = Base32.ExtendedHex.Encode(bytes, padding: true);
            Assert.AreEqual(expectedOutput, result);
        }

        [Test]
        [TestCaseSource("testData")]
        public void Decode_ReturnsExpectedValues(string expectedOutput, string input)
        {
            byte[] bytes = Base32.ExtendedHex.Decode(input);
            string result = Encoding.ASCII.GetString(bytes);
            Assert.AreEqual(expectedOutput, result);
            bytes = Base32.ExtendedHex.Decode(input.ToLowerInvariant());
            result = Encoding.ASCII.GetString(bytes);
            Assert.AreEqual(expectedOutput, result);
        }

        [Test]
        public void Encode_NullBytes_ThrowsArgumentNullException([Values(true, false)]bool padding)
        {
            Assert.Throws<ArgumentNullException>(() => Base32.ExtendedHex.Encode(null, padding));
        }

        [Test]
        public void Decode_NullString_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Base32.ExtendedHex.Decode(null));
        }

        [Test]
        [TestCase("!@#!#@!#@#!@")]
        [TestCase("||||")]
        public void Decode_InvalidInput_ThrowsArgumentException(string input)
        {
            Assert.Throws<ArgumentException>(() => Base32.ExtendedHex.Decode(input));
        }
    }
}