using System.Collections.Generic;

namespace LimitedSizeStack;

public class LimitedSizeStack<T>
{
	LinkedList<T> stack;
	int stackLimit;
    public LimitedSizeStack(int undoLimit)
	{
		stack = new LinkedList<T>();
		stackLimit = undoLimit;
	}

	public void Push(T item)
	{
		if (stack.Count == stackLimit)
		{
            if (stack.Count == 0) return;
            stack.RemoveFirst();
        }
		stack.AddLast(item);
	}

	public T Pop()
	{
		if(stack.Count == 0) return default(T);
		var lastElementValue = stack.Last.Value;
		stack.RemoveLast();
		return lastElementValue;
	}

	public int Count
	{
		get { return stack.Count; }
	}
}