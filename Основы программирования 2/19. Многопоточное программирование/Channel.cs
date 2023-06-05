using NUnit.Framework;
using System.Collections.Generic;

namespace rocket_bot;

public class Channel<T> where T : class
{
	private List<T> sequence = new();
	public T this[int index]
	{
		get
		{
			lock (sequence)
			{
                return index >= Count ? null : sequence[index];
            }
		}
		set
		{
			lock (sequence)
			{
                if (index == Count) sequence.Add(value);
                if (!(index >= Count))
                {
                    sequence[index] = value;
					sequence.RemoveRange(index + 1, Count - 1 - index);
                }
            }
		}
	}

	public T LastItem()
	{
		lock(sequence)
        {
			return Count != 0 && sequence[Count - 1] != null ? sequence[Count - 1] : null;
        }
	}

	public void AppendIfLastItemIsUnchanged(T item, T knownLastItem)
	{
		lock (sequence)
		{
			if (knownLastItem == this.LastItem()) sequence.Add(item);
		}
	}

	public int Count
	{
		get
		{ lock (sequence) { return sequence.Count; } }
	}
}