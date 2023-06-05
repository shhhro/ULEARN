using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace linq_slideviews;

public static class ExtensionsTask
{
	public static double Median(this IEnumerable<double> items)
	{
        
        var itemsList = items.ToList();
        if (itemsList.Count == 0) throw new InvalidOperationException();
        itemsList.Sort();
        return itemsList.Count % 2 == 0 ? (itemsList[itemsList.Count / 2] + itemsList[itemsList.Count / 2 + 1]) / 2 
			: itemsList[itemsList.Count / 2 + 1];
	}

	public static IEnumerable<(T First, T Second)> Bigrams<T>(this IEnumerable<T> items)
	{
		var flag = true;
		T temp = default;
		foreach (var item in items)
		{
			if (flag) { temp = item; flag = false; continue; }
			yield return (temp, item);
			temp = item;
		}
	}
}