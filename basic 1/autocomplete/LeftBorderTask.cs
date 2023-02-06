/*using System;
using System.Collections.Generic;
using System.Linq;

namespace Autocomplete
{
    public class LeftBorderTask
    {
        public static int GetLeftBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
        {
            if (right == -1)
                return -1;
            if (left >= right - 1)
            {
                if (phrases[left].StartsWith(prefix, StringComparison.OrdinalIgnoreCase) || string.Compare(prefix, phrases[left], StringComparison.OrdinalIgnoreCase) < 0)
                    return -1;
                return left;
            }

            var middle = left + (right - left) / 2;

            if (string.Compare(prefix, phrases[middle], StringComparison.OrdinalIgnoreCase) > 0
                        && !phrases[middle].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                return GetLeftBorderIndex(phrases, prefix, middle, right);
            return GetLeftBorderIndex(phrases, prefix, left, middle);
        }
    }
}
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace Autocomplete
{
    public class LeftBorderTask
    {
        public static int GetLeftBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
        {
            if (left == right - 1)
                return left;

            var middle = left + (right - left) / 2;

            if (string.Compare(prefix, phrases[middle], StringComparison.OrdinalIgnoreCase) > 0
                        && !phrases[middle].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                return GetLeftBorderIndex(phrases, prefix, middle, right);
            return GetLeftBorderIndex(phrases, prefix, left, middle);
        }
    }
}
