#region header

// <copyright file="SemanticVersionRangeTests.cs" company="mikegrabski.com">
//    Copyright 2013 Mike Grabski
// 
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
// </copyright>

#endregion

using System;
using System.Collections.Generic;

using FluentAssertions;
using FluentAssertions.Primitives;

using NUnit.Framework;

namespace NStack
{
    [TestFixture]
    public class SemanticVersionRangeTests
    {
        #region Setup/Teardown

        [SetUp]
        public void SetUpTest()
        {
        }

        [TearDown]
        public void TearDownTest()
        {
        }

        [TestFixtureSetUp]
        public void SetUpFixture()
        {
        }

        [TestFixtureTearDown]
        public void TearDownFixture()
        {
        }

        #endregion

        [Test]
        public void Compare_should_return_false_for_values_out_of_range()
        {
            // Arrange
            var rules = new List<Tuple<SemanticVersionRange, SemanticVersion>>
                {
                    new Tuple<SemanticVersionRange, SemanticVersion>(
                        new SemanticVersionRange(SemanticVersionRange.Inclusive(new SemanticVersion(1, 0, 0)),
                                                 SemanticVersionRange.Inclusive(SemanticVersion.MaxValue)),
                        new SemanticVersion(0, 9, 0)
                        ),
                    new Tuple<SemanticVersionRange, SemanticVersion>(
                        new SemanticVersionRange(SemanticVersionRange.Exclusive(SemanticVersion.Unspecified),
                                                 SemanticVersionRange.Inclusive(new SemanticVersion(1, 0, 0))),
                        new SemanticVersion(1, 1, 0)
                        ),
                    new Tuple<SemanticVersionRange, SemanticVersion>(
                        new SemanticVersionRange(SemanticVersionRange.Exclusive(SemanticVersion.Unspecified),
                                                 SemanticVersionRange.Exclusive(new SemanticVersion(1, 0, 0))),
                        new SemanticVersion(1, 0, 0)
                        ),
                    new Tuple<SemanticVersionRange, SemanticVersion>(
                        new SemanticVersionRange(SemanticVersionRange.Inclusive(new SemanticVersion(1, 0, 0)),
                                                 SemanticVersionRange.Inclusive(new SemanticVersion(1, 0, 0))),
                        new SemanticVersion(2, 0, 0)
                        ),
                    new Tuple<SemanticVersionRange, SemanticVersion>(
                        new SemanticVersionRange(SemanticVersionRange.Exclusive(new SemanticVersion(1, 0, 0)),
                                                 SemanticVersionRange.Inclusive(SemanticVersion.MaxValue)),
                        new SemanticVersion(1, 0, 0)
                        ),
                    new Tuple<SemanticVersionRange, SemanticVersion>(
                        new SemanticVersionRange(SemanticVersionRange.Exclusive(new SemanticVersion(1, 0, 0)),
                                                 SemanticVersionRange.Exclusive(new SemanticVersion(2, 0, 0))),
                        new SemanticVersion(1, 0, 0)
                        ),
                    new Tuple<SemanticVersionRange, SemanticVersion>(
                        new SemanticVersionRange(SemanticVersionRange.Exclusive(new SemanticVersion(1, 0, 0)),
                                                 SemanticVersionRange.Exclusive(new SemanticVersion(2, 0, 0))),
                        new SemanticVersion(2, 0, 0)
                        ),
                    new Tuple<SemanticVersionRange, SemanticVersion>(
                        new SemanticVersionRange(SemanticVersionRange.Exclusive(new SemanticVersion(1, 0, 0)),
                                                 SemanticVersionRange.Exclusive(new SemanticVersion(2, 0, 0))),
                        new SemanticVersion(3, 0, 0)
                        ),
                    new Tuple<SemanticVersionRange, SemanticVersion>(
                        new SemanticVersionRange(SemanticVersionRange.Inclusive(new SemanticVersion(1, 0, 0)),
                                                 SemanticVersionRange.Inclusive(new SemanticVersion(2, 0, 0))),
                        new SemanticVersion(3, 0, 0)
                        )
                };

            foreach (Tuple<SemanticVersionRange, SemanticVersion> rule in rules)
            {
                // Act

                // Assert

                rule.Item1.Contains(rule.Item2).Should().BeFalse("{0} -> {1}", rule.Item1, rule.Item2);
            }
        }

        [Test]
        public void Compare_should_return_true_for_values_in_range()
        {
            // Arrange
            var rules = new List<Tuple<SemanticVersionRange, SemanticVersion>>
                {
                    new Tuple<SemanticVersionRange, SemanticVersion>(
                        new SemanticVersionRange(SemanticVersionRange.Inclusive(new SemanticVersion(1, 0, 0)),
                                                 SemanticVersionRange.Inclusive(SemanticVersion.MaxValue)),
                        new SemanticVersion(1, 0, 0)
                        ),
                    new Tuple<SemanticVersionRange, SemanticVersion>(
                        new SemanticVersionRange(SemanticVersionRange.Exclusive(SemanticVersion.Unspecified),
                                                 SemanticVersionRange.Inclusive(new SemanticVersion(1, 0, 0))),
                        new SemanticVersion(1, 0, 0)
                        ),
                    new Tuple<SemanticVersionRange, SemanticVersion>(
                        new SemanticVersionRange(SemanticVersionRange.Exclusive(SemanticVersion.Unspecified),
                                                 SemanticVersionRange.Exclusive(new SemanticVersion(1, 0, 0))),
                        new SemanticVersion(0, 9, 0)
                        ),
                    new Tuple<SemanticVersionRange, SemanticVersion>(
                        new SemanticVersionRange(SemanticVersionRange.Inclusive(new SemanticVersion(1, 0, 0)),
                                                 SemanticVersionRange.Inclusive(new SemanticVersion(1, 0, 0))),
                        new SemanticVersion(1, 0, 0)
                        ),
                    new Tuple<SemanticVersionRange, SemanticVersion>(
                        new SemanticVersionRange(SemanticVersionRange.Exclusive(new SemanticVersion(1, 0, 0)),
                                                 SemanticVersionRange.Inclusive(SemanticVersion.MaxValue)),
                        new SemanticVersion(1, 1, 0)
                        ),
                    new Tuple<SemanticVersionRange, SemanticVersion>(
                        new SemanticVersionRange(SemanticVersionRange.Exclusive(new SemanticVersion(1, 0, 0)),
                                                 SemanticVersionRange.Exclusive(new SemanticVersion(2, 0, 0))),
                        new SemanticVersion(1, 5, 0)
                        ),
                    new Tuple<SemanticVersionRange, SemanticVersion>(
                        new SemanticVersionRange(SemanticVersionRange.Inclusive(new SemanticVersion(1, 0, 0)),
                                                 SemanticVersionRange.Inclusive(new SemanticVersion(2, 0, 0))),
                        new SemanticVersion(1, 0, 0)
                        )
                };

            foreach (Tuple<SemanticVersionRange, SemanticVersion> rule in rules)
            {
                // Act

                // Assert

                rule.Item1.Contains(rule.Item2).Should().BeTrue();
            }
        }

        [Test]
        public void ExclusiveVersion_CompareTo_should_return_negative_values()
        {
            // Arrange
            var version =
                (SemanticVersionRange.ExclusiveVersion) SemanticVersionRange.Exclusive(new SemanticVersion(1, 0, 0));
            version.IsLowerBound = false;

            var a = (SemanticVersionRange.ExclusiveVersion) SemanticVersionRange.Exclusive(new SemanticVersion(1, 0, 0));
            a.IsLowerBound = false;

            var b = (SemanticVersionRange.ExclusiveVersion) SemanticVersionRange.Exclusive(new SemanticVersion(1, 0, 1));
            b.IsLowerBound = false;

            // Act
            // Assert
            version.CompareTo(a).Should().BeLessThan(0);
            version.CompareTo(b).Should().BeLessThan(0);
        }

        [Test]
        public void ExclusiveVersion_CompareTo_should_return_positive_value()
        {
            // Arrange
            SemanticVersion version = SemanticVersionRange.Exclusive(new SemanticVersion(1, 0, 0));

            // Act
            // Assert
            version.CompareTo(new SemanticVersion(0, 9, 0)).Should().BeGreaterThan(0);
        }

        [Test]
        public void ParseRangeString_should_parse_valid_ranges()
        {
            // Arrange
            var rules = new Dictionary<string, Tuple<SemanticVersion, SemanticVersion>>
                {
                    {
                        "1.0",
                        new Tuple<SemanticVersion, SemanticVersion>(
                            SemanticVersionRange.Inclusive(new SemanticVersion(1, 0, 0)),
                            SemanticVersionRange.Inclusive(SemanticVersion.MaxValue))
                    },
                    {
                        "(,1.0]",
                        new Tuple<SemanticVersion, SemanticVersion>(
                            SemanticVersionRange.Exclusive(SemanticVersion.Unspecified),
                            SemanticVersionRange.Inclusive(new SemanticVersion(1, 0, 0)))
                    },
                    {
                        "(,1.0)",
                        new Tuple<SemanticVersion, SemanticVersion>(
                            SemanticVersionRange.Exclusive(SemanticVersion.Unspecified),
                            SemanticVersionRange.Exclusive(new SemanticVersion(1, 0, 0)))
                    },
                    {
                        "[1.0]",
                        new Tuple<SemanticVersion, SemanticVersion>(
                            SemanticVersionRange.Inclusive(new SemanticVersion(1, 0, 0)),
                            SemanticVersionRange.Inclusive(new SemanticVersion(1, 0, 0)))
                    },
                    {
                        "(1.0,)",
                        new Tuple<SemanticVersion, SemanticVersion>(
                            SemanticVersionRange.Exclusive(new SemanticVersion(1, 0, 0)),
                            SemanticVersionRange.Exclusive(SemanticVersion.MaxValue))
                    },
                    {
                        "(1.0,2.0)",
                        new Tuple<SemanticVersion, SemanticVersion>(
                            SemanticVersionRange.Exclusive(new SemanticVersion(1, 0, 0)),
                            SemanticVersionRange.Exclusive(new SemanticVersion(2, 0, 0)))
                    },
                    {
                        "[1.0,2.0]",
                        new Tuple<SemanticVersion, SemanticVersion>(
                            SemanticVersionRange.Inclusive(new SemanticVersion(1, 0, 0)),
                            SemanticVersionRange.Inclusive(new SemanticVersion(2, 0, 0)))
                    }
                };

            foreach (KeyValuePair<string, Tuple<SemanticVersion, SemanticVersion>> rule in rules)
            {
                // Act
                Tuple<SemanticVersion, SemanticVersion> actual = SemanticVersionRange.ParseRangeString(rule.Key);

                // Assert
                actual.Item1.Should().Be(rule.Value.Item1);
                actual.Item2.Should().Be(rule.Value.Item2);
            }
        }

        [Test]
        public void Parse_should_return_parsed_range()
        {
            // Arrange
            var expected = new SemanticVersionRange("1.0");

            // Act
            SemanticVersionRange actual = SemanticVersionRange.Parse("1.0");

            // Assert
            actual.Should().Be(expected);
        }

        [Test]
        public void ToString_should_render_proper_NuGet_style_range_strings()
        {
            // Arrange
            var formats = new[] {"1.0", "(,1.0]", "(,1.0)", "[1.0]", "(1.0,)", "(1.0,2.0)", "[1.0,2.0]"};

            foreach (string format in formats)
            {
                // Act
                var range = new SemanticVersionRange(format);

                // Assert
                AndConstraint<StringAssertions> actual = range.ToString().Should().Be(format);
            }
        }

        [Test]
        public void TryParse_should_not_throw_and_returns_false()
        {
            // Arrange
            var values = new[] {null, string.Empty, " ", "asdasdasd"};
            SemanticVersionRange result = null;
            bool success = false;

            foreach (string value in values)
            {
                // Act
                Assert.DoesNotThrow(() => success = SemanticVersionRange.TryParse(value, out result));

                // Assert
                success.Should().BeFalse();
                result.Should().BeNull();
            }
        }

        [Test]
        public void TryParse_should_return_false_and_assign_result()
        {
            // Arrange
            SemanticVersionRange result = null;

            // Act
            bool actual = SemanticVersionRange.TryParse("1.0", out result);

            // Assert
            actual.Should().BeTrue();
            result.Should().Be(new SemanticVersionRange("1.0"));
        }
    }
}