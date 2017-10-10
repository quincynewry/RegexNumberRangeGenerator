using System;
using System.Collections.Generic;
using System.Linq;

namespace RegexNumberRangeGenerator
{
    public static class RegexNumberRangeGenerator
    {
        public static string Generate(int min, int max)
        {
            string regex = string.Empty;

            var patterns = SplitPatterns(min, max);
            var lastPattern = patterns.Last();

            foreach (string pattern in patterns)
            {
                regex += pattern.Equals(lastPattern) ? pattern : pattern + "|";
            }

            return $"\\b({regex})\\b";
        }

        private static List<string> SplitPatterns(int min, int max)
        {
            List<string> subPatterns = new List<string>();
            List<int> ranges = GetRanges(min, max).Distinct().ToList();
            int start = min;

            foreach (int stop in ranges)
            {
                subPatterns.Add(RangeToPattern(start, stop));
                start = stop + 1;
            }

            return subPatterns;
        }

        private static List<int> GetRanges(int min, int max)
        {
            int ninesCount = 1;
            List<int> stops = new List<int>
            {
                max
            };

            int stop = FillByNines(min, ninesCount);
            int oldMin = min;

            while (min <= stop && stop < max)
            {
                stops.Add(stop);
                min = stop;
                ninesCount += 1;

                stop = FillByNines(min, ninesCount);
            }

            min = oldMin;
            int zeroesCount = 1;

            stop = FillByZeros(max + 1, zeroesCount) - 1;

            while (min < stop && stop <= max)
            {
                if (!stops.Contains(stop))
                {
                    stops.Add(stop);
                }

                zeroesCount += 1;
                stop = FillByZeros(max + 1, zeroesCount) - 1;
            }

            stops.Sort();

            return stops;
        }

        private static int FillByNines(int min, int ninesCount)
        {
            string minStr = min.ToString();
            var character = "9";
            int index = minStr.Length - ninesCount;

            return int.Parse((index < 0 ? "" : minStr.Substring(0, index)) + character + minStr.Substring(index + character.Length));
        }

        private static int FillByZeros(int max, int zeroesCount)
        {
            return Convert.ToInt32(max - (max % (Math.Pow(10, zeroesCount))));
        }

        private static string RangeToPattern(int start, int stop)
        {
            string pattern = string.Empty;
            int anyDigitCount = 0;
            int i, j = 0;

            for (i = 0; j < stop.ToString().Length; i++, j++)
            {
                var startDigit = i + 1 > start.ToString().Length ? string.Empty : start.ToString()[i].ToString();
                var stopDigit = j + 1 > stop.ToString().Length ? string.Empty : stop.ToString()[j].ToString();

                if (startDigit == stopDigit)
                {
                    pattern += startDigit;
                }
                else if (startDigit != "0" || stopDigit != "9")
                {
                    pattern += $"[{startDigit}-{stopDigit}]";
                }
                else
                {
                    anyDigitCount += 1;
                }
            }

            if (anyDigitCount > 0)
            {
                pattern += "[0-9]";
            }
            if (anyDigitCount > 1)
            {
                pattern += "{" + anyDigitCount.ToString() + "}";
            }
            return pattern;
        }
    }
}
