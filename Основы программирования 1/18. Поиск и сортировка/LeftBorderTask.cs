using System;
using System.Collections.Generic;

namespace Autocomplete
{
    public class LeftBorderTask
    {
        public static int GetLeftBorderIndex(IReadOnlyList<string> phrases, string prefix, int leftBorder, int rightBorder)
        {
            if (rightBorder - leftBorder <= 1) return leftBorder;
            var middle = leftBorder + (rightBorder - leftBorder) / 2;

            if (string.Compare(prefix, phrases[middle], StringComparison.OrdinalIgnoreCase) <= 0) return GetLeftBorderIndex(phrases, prefix, leftBorder, middle);
            return GetLeftBorderIndex(phrases, prefix, middle, rightBorder);
        }
    }
}