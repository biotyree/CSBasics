using System.Collections.Generic;
using System.Linq;

namespace func.brainfuck
{
	public class BrainfuckLoopCommands
	{
		public static void RegisterTo(IVirtualMachine vm)
		{
			vm.RegisterCommand('[', b =>
            {
                startBracketsStack.Push(b.InstructionPointer);
				if (b.Memory[b.MemoryPointer] == 0)
				{
					var counter = startBracketsStack.Count;
					var i = b.InstructionPointer + 1;
					while(i < b.Instructions.Length && counter != 0)
					{
						if (b.Instructions[i] == ']')
							counter--;
						if (b.Instructions[i] == '[')
							counter++;
					}
					b.InstructionPointer = i - 1;
                }
			});
			vm.RegisterCommand(']', b =>
            {
				if (b.Memory[b.MemoryPointer] != 0)
					b.InstructionPointer = startBracketsStack.Pop();
            });
		}

		private static Stack<int> startBracketsStack = new Stack<int>();
    }
}