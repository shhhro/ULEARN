using DynamicData;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace LimitedSizeStack;

public class ListModel<TItem>
{
	public List<TItem> Items { get; }
    public LimitedSizeStack<Operation> HistoryStack { get; private set; }
    public ListModel(int undoLimit) : this(new List<TItem>(), undoLimit)
	{
	}

	public ListModel(List<TItem> items, int undoLimit)
	{
		Items = items;
        HistoryStack = new LimitedSizeStack<Operation>(undoLimit);
    }

	public void AddItem(TItem item)
	{
		Items.Add(item);
		HistoryStack.Push(new Operation(Items.Count - 1, OperationType.Add, item));
		
	}

	public void RemoveItem(int index)
	{
        HistoryStack.Push(new Operation(index, OperationType.Remove, Items[index]));
        Items.RemoveAt(index);
	}

	public void MoveUpItem(int index)
	{
		HistoryStack.Push(new Operation(index - 1, OperationType.MoveUp, Items[index]));
		var temp = Items[index];
		Items[index] = Items[index - 1];
		Items[index - 1] = temp;
	}

	public void MoveDownItem(int index)
	{
        HistoryStack.Push(new Operation(index + 1, OperationType.MoveDown, Items[index]));
        if (index == Items.Count - 1) return;
        var temp = Items[index];
        Items[index] = Items[index + 1];
        Items[index + 1] = temp;
    }

	public bool CanUndo()
	{
		return !(HistoryStack.Count == 0);
	}

	public void Undo()
	{
		var lastOperation = HistoryStack.Pop();
		switch(lastOperation.OperationType)
		{
			case OperationType.Add: Items.RemoveAt(lastOperation.Index); break;

            case OperationType.Remove: Items.Insert(lastOperation.Index, lastOperation.Value); break;

			case OperationType.MoveUp:
                var temp = Items[lastOperation.Index];
                Items[lastOperation.Index] = Items[lastOperation.Index + 1];
                Items[lastOperation.Index + 1] = temp; break;

			case OperationType.MoveDown:
                var temp1 = Items[lastOperation.Index];
                Items[lastOperation.Index] = Items[lastOperation.Index - 1];
                Items[lastOperation.Index - 1] = temp1; break;
        }
	}

public class Operation
	{
		public int Index { get; private set; }
		public OperationType OperationType { get; private set; }
		public TItem Value { get; private set; }

        public Operation(int index, OperationType operation, TItem value)
        {
            Index = index;
            OperationType = operation;
            Value = value;
		}
	}

	public enum OperationType 
	{ 
		Add,
		Remove,
		MoveUp,
		MoveDown
	}


}