using System;
using System.Collections.Generic;

namespace func.brainfuck
{
	public class VirtualMachine : IVirtualMachine
	{
		public string Instructions { get; }
		public int InstructionPointer { get; set; }
		public byte[] Memory { get; }
		public int MemoryPointer { get; set; }
		private Dictionary<char, Action<IVirtualMachine>> _registeredCommands;

		public VirtualMachine(string program, int memorySize)
		{
			Instructions = program;
			Memory = new byte[memorySize];
			MemoryPointer = 0; InstructionPointer = 0;
            _registeredCommands = new Dictionary<char, Action<IVirtualMachine>>();
		}

		public void RegisterCommand(char symbol, Action<IVirtualMachine> execute )
		{
			if (!Instructions.Contains(symbol)) throw new ArgumentException();
			_registeredCommands.Add(symbol, execute);
		}

		public void Run()
		{
			for (; InstructionPointer < Instructions.Length; InstructionPointer++)
			{
				if (_registeredCommands.ContainsKey(Instructions[InstructionPointer])) 
					_registeredCommands[Instructions[InstructionPointer]](this);
			}
		}
	}
}


/* instructions = "xy"
var vm = new VirtualMachine("xy", 10);
vm.RegisterCommand('x', b => { res += "x->" + b.InstructionPointer + ", "; });
vm.RegisterCommand('y', b => { res += "y->" + b.InstructionPointer; });
vm.Run();
Assert.AreEqual("x->0, y->1", res);*/