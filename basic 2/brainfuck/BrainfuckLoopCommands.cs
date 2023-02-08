using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace func.brainfuck
{
    public class BrainfuckLoopCommands
    {
        public static void RegisterTo(IVirtualMachine vm)
        {
            var startEndDict = new Dictionary<int, int>();
            var startBracketsStack = new Stack<int>();
            
            InitializeBraketsPositions(vm, startEndDict, startBracketsStack);

            vm.RegisterCommand('[', b =>
            {
                startBracketsStack.Push(b.InstructionPointer);
                if (b.Memory[b.MemoryPointer] == 0)
                    b.InstructionPointer = startEndDict[b.InstructionPointer] - 1;
            });

            vm.RegisterCommand(']', b =>
            {
                var index = startBracketsStack.Pop();
                if (b.Memory[b.MemoryPointer] != 0)
                    b.InstructionPointer = index - 1;
            });
        }

        private static void InitializeBraketsPositions(IVirtualMachine vm,
            Dictionary<int, int> startEndDict, Stack<int> startBracketsStack)
        {
            for (int i = 0; i < vm.Instructions.Length; i++)
            {
                if (vm.Instructions[i] == '[')
                    startBracketsStack.Push(i);
                if (vm.Instructions[i] == ']')
                    startEndDict.Add(startBracketsStack.Pop(), i);
            }
        }
    }
}