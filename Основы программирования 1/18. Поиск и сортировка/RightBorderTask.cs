using System;
using System.Collections.Generic;
using System.Linq;

namespace Autocomplete
{
    public class RightBorderTask
    {
        public static int GetRightBorderIndex(IReadOnlyList<string> phrases, string prefix, int leftBorder, int rightBorder)
        {
            while (rightBorder - leftBorder > 1)
            {
                var middle = leftBorder + (rightBorder - leftBorder) / 2;
                if (string.Compare(prefix, phrases[middle], StringComparison.OrdinalIgnoreCase) >= 0
                   || phrases[middle].StartsWith(prefix))
                    leftBorder = middle;
                else rightBorder = middle;
            }
            return rightBorder;
        }
    }
}