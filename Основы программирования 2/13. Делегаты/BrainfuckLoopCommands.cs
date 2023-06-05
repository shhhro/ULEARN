using System.Collections.Generic;

namespace func.brainfuck
{
	public class BrainfuckLoopCommands
	{
        private static Dictionary<int, int> bracketIndexes = new();
        
        public static void RegisterTo(IVirtualMachine vm)
        {
            CreateLoop(vm);
            vm.RegisterCommand('[', b => {
                if (vm.Memory[vm.MemoryPointer] == 0)
                    vm.InstructionPointer = bracketIndexes[vm.InstructionPointer]; });
            vm.RegisterCommand(']', vm => {
                if (vm.Memory[vm.MemoryPointer] != 0)
                    vm.InstructionPointer = bracketIndexes[vm.InstructionPointer]; });
        }

        public static void CreateLoop(IVirtualMachine vm)
        {
            Stack<int> loopStack = new Stack<int>();
            for (int i = 0; i < vm.Instructions.Length; i++)
            {
                switch (vm.Instructions[i])
                {
                    case '[':
                        loopStack.Push(i); break;
                    case ']':
                        bracketIndexes[i] = loopStack.Peek();
                        bracketIndexes[loopStack.Pop()] = i; break;
                    default: continue;
                }
            }
        }
    }
}