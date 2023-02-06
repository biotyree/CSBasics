using System;
using System.Collections.Generic;

namespace Autocomplete
{
    public class RightBorderTask
    {
        public static int GetRightBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
        {
            if (phrases.Count > 0)
            {
                left = 0;
                right = phrases.Count - 1;
                var middle = -1;

                while (left < right)
                {
                    middle = left + (right - left) / 2;
                    if (string.Compare(prefix, phrases[middle], StringComparison.OrdinalIgnoreCase) >= 0
                        || phrases[middle].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                        left = middle + 1;
                    else
                        right = middle;
                }

                if (string.Compare(prefix, phrases[right], StringComparison.OrdinalIgnoreCase) < 0
                        && !phrases[right].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    return right;
            }
            return phrases.Count;
        }
    }
}