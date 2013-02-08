#region header

// <copyright file="SemanticVersionTests.cs" company="mikegrabski.com">
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
using System.Linq;

using FluentAssertions;

using NUnit.Framework;

namespace NStack
{
    [TestFixture]
    public class SemanticVersionTests
    {
        #region Setup/Teardown

        [SetUp]
        public void Init()
        {
        }

        [TearDown]
        public void Cleanup()
        {
        }

        [TestFixtureSetUp]
        public void InitFixture()
        {
        }

        [TestFixtureTearDown]
        public void CleanupFixture()
        {
        }

        #endregion

        [Test]
        public void CompareTo_should_calculate_proper_precedence()
        {
            // Arrange
            var versions = new[]
                               {
                                   new SemanticVersion(1, 0, 0, SemanticVersion.Stage.Pre),
                                   new SemanticVersion(1, 0, 0, SemanticVersion.Stage.Alpha),
                                   new SemanticVersion(1, 0, 0, SemanticVersion.Stage.Alpha, 1),
                                   new SemanticVersion(1, 0, 0, SemanticVersion.Stage.Beta, 1),
                                   new SemanticVersion(1, 0, 0, SemanticVersion.Stage.Beta, 11),
                                   new SemanticVersion(1, 0, 0, SemanticVersion.Stage.Rc, 1),
                                   new SemanticVersion(1, 0, 0, SemanticVersion.Stage.Rc, 1, "1"),
                                   new SemanticVersion(1, 0, 0),
                                   new SemanticVersion(1, 0, 0, null, "0.3.7"),
                                   new SemanticVersion(1, 3, 7, null, "build"),
                                   new SemanticVersion(1, 3, 7, null, "build.2.b8f12d7"),
                                   new SemanticVersion(1, 3, 7, null, "build.11.e0f985a")
                               };

            // Act
            // Assert
            for (int i = 1; i < versions.Length; i++)
            {
                SemanticVersion previous = versions[i - 1];
                SemanticVersion current = versions[i];

                current.CompareTo(previous)
                    .Should()
                    .Be(1, "{0} should succeed {1}", current, previous);
            }

            for (int i = versions.Length - 2; i >= 0; i--)
            {
                SemanticVersion next = versions[i + 1];
                SemanticVersion current = versions[i];

                current.CompareTo(next)
                    .Should().Be(-1, "{0} should preceed {1}", current, next);
            }
        }

        [Test]
        public void Initializing_with_stage_and_step_should_set_PreRelease_property()
        {
            // Arrange
            IEnumerable<SemanticVersion.Stage> stages = Enum.GetValues(typeof (SemanticVersion.Stage)).Cast<SemanticVersion.Stage>();

            // Act
            // Assert
            foreach (SemanticVersion.Stage stage in stages)
            {
                var actual = new SemanticVersion(1, 0, stage: stage, step: 1);

                if (stage == SemanticVersion.Stage.None) actual.PreRelease.Should().BeNull();
                else actual.PreRelease.Should().Be(stage.ToString().ToLowerInvariant() + ".1");
            }
        }

        [Test]
        public void Initializing_with_stage_and_step_with_zero_should_set_PreRelease_property()
        {
            // Arrange
            IEnumerable<SemanticVersion.Stage> stages = Enum.GetValues(typeof (SemanticVersion.Stage)).Cast<SemanticVersion.Stage>();

            // Act
            // Assert
            foreach (SemanticVersion.Stage stage in stages)
            {
                var actual = new SemanticVersion(1, 0, stage: stage, step: 0);

                if (stage == SemanticVersion.Stage.None) actual.PreRelease.Should().BeNull();
                else actual.PreRelease.Should().Be(stage.ToString().ToLowerInvariant());
            }
        }

        [Test]
        public void Initializing_with_stage_should_set_PreRelease_property()
        {
            // Arrange
            IEnumerable<SemanticVersion.Stage> stages = Enum.GetValues(typeof (SemanticVersion.Stage)).Cast<SemanticVersion.Stage>();

            // Act
            // Assert
            foreach (SemanticVersion.Stage stage in stages)
            {
                var actual = new SemanticVersion(1, 0, stage: stage);

                if (stage == SemanticVersion.Stage.None) actual.PreRelease.Should().BeNull();
                else actual.PreRelease.Should().Be(stage.ToString().ToLowerInvariant());
            }
        }

        [Test]
        public void Initializing_with_invalid_Build_should_throw()
        {
            Assert.Throws<ArgumentException>(() => new SemanticVersion(1, 0, 0, null, "abc.1+build"));
        }

        [Test]
        public void Initializing_with_invalid_PreRelease_should_throw()
        {
            Assert.Throws<ArgumentException>(() => new SemanticVersion(1, 0, 0, preRelease: "abc.1+build"));
        }

        [Test]
        public void Parse_should_parse_full_version()
        {
            // Arrange
            var expected = new SemanticVersion(1, 0, 0, SemanticVersion.Stage.Alpha, 1, "build.001.201201191050");

            // Act
            SemanticVersion actual = SemanticVersion.Parse("1.0.0-alpha.1+build.001.201201191050");

            // Assert
            actual.Should().Be(expected);
        }

        [Test]
        public void Should_be_equivalent()
        {
            // Arrange
            var first = new SemanticVersion(1, 0, 0, SemanticVersion.Stage.Pre, 1, "build.001");
            var second = new SemanticVersion(1, 0, 0, SemanticVersion.Stage.Pre, 1, "build.001");

            // Act
            // Assert
            first.Should().Be(second);
        }

        [Test]
        public void Should_be_greater_than()
        {
            // Arrange
            var first = new SemanticVersion(1, 0, 0, SemanticVersion.Stage.Pre, 1, "build.001");
            var second = new SemanticVersion(1, 1, 0, SemanticVersion.Stage.Pre, 1, "build.001");

            // Act
            bool actual = second > first;

            // Assert
            actual.Should().BeTrue();
        }

        [Test]
        public void Should_be_less_than()
        {
            // Arrange
            var first = new SemanticVersion(1, 0, 0, SemanticVersion.Stage.Pre, 1, "build.001");
            var second = new SemanticVersion(1, 1, 0, SemanticVersion.Stage.Pre, 1, "build.001");

            // Act
            bool actual = first < second;

            // Assert
            actual.Should().BeTrue();
        }

        [Test]
        public void Should_not_be_equivalent()
        {
            // Arrange
            var first = new SemanticVersion(1, 0, 0, SemanticVersion.Stage.Pre, 1, "build.001");
            var second = new SemanticVersion(1, 1, 0, SemanticVersion.Stage.Pre, 1, "build.001");

            // Act
            // Assert
            first.Should().NotBe(second);
        }

        [Test]
        public void Should_sort_correctly()
        {
            // Arrange
            var expected = new[]
                               {
                                   new SemanticVersion(1, 0, 0, SemanticVersion.Stage.Pre),
                                   new SemanticVersion(1, 0, 0, SemanticVersion.Stage.Alpha),
                                   new SemanticVersion(1, 0, 0, SemanticVersion.Stage.Alpha, 1),
                                   new SemanticVersion(1, 0, 0, SemanticVersion.Stage.Beta, 1),
                                   new SemanticVersion(1, 0, 0, SemanticVersion.Stage.Beta, 11),
                                   new SemanticVersion(1, 0, 0, SemanticVersion.Stage.Rc, 1),
                                   new SemanticVersion(1, 0, 0, SemanticVersion.Stage.Rc, 1, "1"),
                                   new SemanticVersion(1, 0, 0),
                                   new SemanticVersion(1, 0, 0, null, "0.3.7"),
                                   new SemanticVersion(1, 3, 7, null, "build"),
                                   new SemanticVersion(1, 3, 7, null, "build.2.b8f12d7"),
                                   new SemanticVersion(1, 3, 7, null, "build.11.e0f985a")
                               };
            var r = new Random();
            int seed = r.Next();
            SemanticVersion[] actual =
                expected.OrderBy(v => (~(v.GetHashCode() & seed)) & (v.GetHashCode() | seed)).ToArray();

            // Act
            Array.Sort(actual);

            // Assert
            actual.Should().ContainInOrder(expected);
        }


        [Test]
        public void ToString_should_include_full_version_omitting_build()
        {
            // Arrange
            var first = new SemanticVersion(1, 0, 0, SemanticVersion.Stage.Pre, 1, "build.001");

            // Act
            // Assert
            first.ToString("F").Should().Be("1.0.0-pre.1");
        }

        [Test]
        public void ToString_should_include_full_version_plus_build()
        {
            // Arrange
            var first = new SemanticVersion(1, 0, 0, SemanticVersion.Stage.Pre, 1, "build.001");

            // Act
            // Assert
            first.ToString("f").Should().Be("1.0.0-pre.1+build.001");
        }

        [Test]
        public void ToString_should_include_significant_version_only()
        {
            // Arrange
            var first = new SemanticVersion(1, 0, 0, SemanticVersion.Stage.Pre, 1, "build.001");

            // Act
            // Assert
            first.ToString("s").Should().Be("1.0.0");
        }

        [Test]
        public void TryParse_should_fail_safely()
        {
            // Arrange
            SemanticVersion actual;

            // Act
            bool result = SemanticVersion.TryParse("bad", out actual);

            // Assert
            result.Should().BeFalse();
            actual.Should().BeNull();
        }

        [Test]
        public void TryParse_should_parse_full_version()
        {
            // Arrange
            var expected = new SemanticVersion(1, 0, 0, SemanticVersion.Stage.Alpha, 1, "build.001.201201191050");
            SemanticVersion actual;

            // Act
            bool result = SemanticVersion.TryParse("1.0.0-alpha.1+build.001.201201191050", out actual);

            // Assert
            result.Should().BeTrue();
            actual.Should().Be(expected);
        }
    }
}