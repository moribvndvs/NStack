#region header

// <copyright file="StringExtensions.Inflector.cs" company="mikegrabski.com">
//    Copyright 2012 Mike Grabski
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

using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NStack.Extensions
{
    public static partial class StringExtensions
    {
        private static readonly List<Rule> Plurals = new List<Rule>();

        private static readonly List<Rule> Singulars = new List<Rule>();

        private static readonly List<string> Uncountables = new List<string>();

        static StringExtensions()
        {
            AddPlural("$", "s");
            AddPlural("s$", "s");
            AddPlural("(ax|test)is$", "$1es");
            AddPlural("(octop|vir|alumn|fung)us$", "$1i");
            AddPlural("(alias|status)$", "$1es");
            AddPlural("(bu)s$", "$1ses");
            AddPlural("(buffal|tomat|volcan)o$", "$1oes");
            AddPlural("([ti])um$", "$1a");
            AddPlural("sis$", "ses");
            AddPlural("(?:([^f])fe|([lr])f)$", "$1$2ves");
            AddPlural("(hive)$", "$1s");
            AddPlural("([^aeiouy]|qu)y$", "$1ies");
            AddPlural("(x|ch|ss|sh)$", "$1es");
            AddPlural("(matr|vert|ind)ix|ex$", "$1ices");
            AddPlural("([m|l])ouse$", "$1ice");
            AddPlural("^(ox)$", "$1en");
            AddPlural("(quiz)$", "$1zes");

            AddSingular("s$", "");
            AddSingular("(n)ews$", "$1ews");
            AddSingular("([ti])a$", "$1um");
            AddSingular("((a)naly|(b)a|(d)iagno|(p)arenthe|(p)rogno|(s)ynop|(t)he)ses$", "$1$2sis");
            AddSingular("(^analy)ses$", "$1sis");
            AddSingular("([^f])ves$", "$1fe");
            AddSingular("(hive)s$", "$1");
            AddSingular("(tive)s$", "$1");
            AddSingular("([lr])ves$", "$1f");
            AddSingular("([^aeiouy]|qu)ies$", "$1y");
            AddSingular("(s)eries$", "$1eries");
            AddSingular("(m)ovies$", "$1ovie");
            AddSingular("(x|ch|ss|sh)es$", "$1");
            AddSingular("([m|l])ice$", "$1ouse");
            AddSingular("(bus)es$", "$1");
            AddSingular("(o)es$", "$1");
            AddSingular("(shoe)s$", "$1");
            AddSingular("(cris|ax|test)es$", "$1is");
            AddSingular("(octop|vir|alumn|fung)i$", "$1us");
            AddSingular("(alias|status)es$", "$1");
            AddSingular("^(ox)en", "$1");
            AddSingular("(vert|ind)ices$", "$1ex");
            AddSingular("(matr)ices$", "$1ix");
            AddSingular("(quiz)zes$", "$1");

            AddIrregular("person", "people");
            AddIrregular("man", "men");
            AddIrregular("child", "children");
            AddIrregular("sex", "sexes");
            AddIrregular("move", "moves");
            AddIrregular("goose", "geese");
            AddIrregular("alumna", "alumnae");

            AddUncountable("equipment");
            AddUncountable("information");
            AddUncountable("rice");
            AddUncountable("money");
            AddUncountable("species");
            AddUncountable("series");
            AddUncountable("fish");
            AddUncountable("sheep");
            AddUncountable("deer");
            AddUncountable("aircraft");
            AddUncountable("moose");
        }

        private static void AddIrregular(string singular, string plural)
        {
            AddPlural(string.Format("({0}){1}$", singular[0], singular.Substring(1)),
                      string.Format("$1{0}", plural.Substring(1)));
            AddSingular(string.Format("({0}){1}$", plural[0], plural.Substring(1)),
                        string.Format("$1{0}", singular.Substring(1)));
        }

        private static void AddUncountable(string word)
        {
            Uncountables.Add(word.ToLower());
        }

        private static void AddPlural(string rule, string replacement)
        {
            Plurals.Add(new Rule(rule, replacement));
        }

        private static void AddSingular(string rule, string replacement)
        {
            Singulars.Add(new Rule(rule, replacement));
        }

        /// <summary>
        ///   Returns the plural form of the specified word (basic English-only).
        /// </summary>
        /// <param name="word"> An English word in singular form. </param>
        /// <returns> The pluralized form of <paramref name="word" /> . </returns>
        public static string Pluralize(this string word)
        {
            return ApplyRules(Plurals, word);
        }

        /// <summary>
        ///   Returns the singular form of the specified word (basic English-only).
        /// </summary>
        /// <param name="word"> An English word in plural form. </param>
        /// <returns> The singular version of <paramref name="word" /> . </returns>
        public static string Singularize(this string word)
        {
            return ApplyRules(Singulars, word);
        }

        private static string ApplyRules(List<Rule> rules, string word)
        {
            string result = word;

            if (!Uncountables.Contains(word.ToLower()))
            {
                for (int i = rules.Count - 1; i >= 0; i--)
                {
                    if ((result = rules[i].Apply(word)) != null)
                    {
                        break;
                    }
                }
            }

            return result;
        }

        #region Nested type: Rule

        private class Rule
        {
            private readonly Regex _regex;

            private readonly string _replacement;

            public Rule(string pattern, string replacement)
            {
                _regex = new Regex(pattern, RegexOptions.IgnoreCase);
                _replacement = replacement;
            }

            public string Apply(string word)
            {
                if (!_regex.IsMatch(word))
                {
                    return null;
                }

                return _regex.Replace(word, _replacement);
            }
        }

        #endregion
    }
}