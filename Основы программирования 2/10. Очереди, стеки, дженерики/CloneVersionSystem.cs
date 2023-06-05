using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace Clones;

public class CloneVersionSystem : ICloneVersionSystem
{
	static Dictionary<int, Clone> cloneList;
		
	public CloneVersionSystem()
	{
		cloneList = new Dictionary<int, Clone>();
    }

	public string Execute(string query)
	{
		Command command = CreateCommand(query);
		Clone clone = CreateClone(command.CloneIndex);

        switch (command.Name)
		{
			case "learn": clone.Learn(command.Program); break;

			case "rollback": clone.Rollback(); break;

			case "relearn": clone.Relearn(); break;

			case "clone": cloneList.Add(cloneList.Count + 1, new Clone(clone.LearnedProgram, clone.RemoveActions));
				break;

			case "check": return clone.Check();
			
			default: return null;
		}
		return null;
	}

	public Command CreateCommand(string query)
	{
		Command command;
        var commandInput = query.Split(' ');

        if (commandInput.Length > 2) command = 
				new Command(commandInput[0], int.Parse(commandInput[1]), commandInput[2]);
        else command = new Command(commandInput[0], int.Parse(commandInput[1]));

		return command;
    }

	public Clone CreateClone(int index)
	{
		Clone clone = new Clone();
        if (!cloneList.ContainsKey(index))
        {
            cloneList.Add(index, clone);
        }
        else clone = cloneList[index];
		return clone;
    }
}


public class Clone
{
	public ActionStack<string> LearnedProgram { get; private set; }
	public ActionStack<string> RemoveActions { get; private set; }

	public Clone()
	{
		LearnedProgram = new ActionStack<string>();
        RemoveActions = new ActionStack<string>();
	}

	public Clone(ActionStack<string> learnedProgram, ActionStack<string> remove)
	{
		LearnedProgram = learnedProgram.Clone();
		RemoveActions = remove.Clone();
	}

	public void Learn(string program)
	{
        LearnedProgram.Push(program);
        RemoveActions.Clear();
    }

	public void Rollback()
	{
        RemoveActions.Push(LearnedProgram.Peek());
		LearnedProgram.Pop();
    }

	public void Relearn()
	{
		if (RemoveActions.Count == 0) return;
        LearnedProgram.Push(RemoveActions.Peek());
		RemoveActions.Pop();
    }

	public string Check()
	{
        if (LearnedProgram.Count != 0)  return LearnedProgram.Peek(); 
		return "basic";
    }
}

public class Command
{
    public readonly string Name;
    public readonly int CloneIndex;
    public readonly string Program;

    public Command(string name, int cloneIndex, string program)
    {
        Name = name;
        CloneIndex = cloneIndex;
        Program = program;
    }

    public Command(string name, int cloneIndex)
    {
        Name = name;
        CloneIndex = cloneIndex;
        Program = null;
    }
}

public class ActionStack<T>
{
	public int Count { get; private set; }
	Action<T> Last { get; set; }
	public ActionStack<T> Clone()
	{
		return new ActionStack<T> { Count = Count, Last = Last };
	}

	public void Push(T value)
	{
		Last = new Action<T>(value, Last);
		Count++;
	}

	public T Pop()
	{
		var value = Last.Value;
		Last = Last.PreviousValue;
		Count--;
		return value;
	}

	public T Peek()
	{
		return Last.Value;
	}

	public void Clear()
	{
		Last = null;
		Count = 0;
	}

    public class Action<T>
    {
        public T Value { get; set; }
        public Action<T> PreviousValue { get; set; }

		public Action(T value, Action<T> previous)
		{
			Value = value;
			PreviousValue = previous;
		}

    }
}