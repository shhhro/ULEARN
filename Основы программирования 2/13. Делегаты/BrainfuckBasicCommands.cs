using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace func.brainfuck
{
	public class BrainfuckBasicCommands
	{
		private static readonly char[] Chars = CreateChars();

        public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
		{
			vm.RegisterCommand('.', b => write(Convert.ToChar(vm.Memory[vm.MemoryPointer])));
			vm.RegisterCommand('+', b => vm.Memory[vm.MemoryPointer]++ );
			vm.RegisterCommand('-', b => vm.Memory[vm.MemoryPointer]-- );
            vm.RegisterCommand(',', b => vm.Memory[vm.MemoryPointer] = (byte)read());
            vm.RegisterCommand('>', b => MoveCommand(vm, vm.Memory.Length - 1, 0, true));
			vm.RegisterCommand('<', b => MoveCommand(vm, 0, vm.Memory.Length - 1, false));
			foreach (var symbol in Chars)
                vm.RegisterCommand(symbol, b => vm.Memory[vm.MemoryPointer] = Convert.ToByte(symbol));
        }

		public static void MoveCommand(IVirtualMachine vm, int checkCondition, int changeCondition, bool moveForward) 
		{
			if (vm.MemoryPointer == checkCondition) vm.MemoryPointer = changeCondition;
			else
			{
                if (moveForward) vm.MemoryPointer++;
                else vm.MemoryPointer--;
            }
		}

		public static char[] CreateChars()
		{
			var sb = new StringBuilder();
			for(int firstChar = 48; firstChar <= 122;  firstChar++)
				if() sb.Append((char)firstChar);
			return sb.ToString().Where(chr => char.IsLetter(chr) || char.IsDigit(chr)).ToArray();
		}
	}
}