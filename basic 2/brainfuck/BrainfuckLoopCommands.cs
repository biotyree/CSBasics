using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace func.brainfuck
{
    public class BrainfuckLoopCommands
    {
        //TODO: завести класс с полями для хранения инфы о скобках
        public static void RegisterTo(IVirtualMachine vm)
        {
            if (hashCode != vm.Instructions.GetHashCode())
                InitializeBraketsPositions(vm);

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

        private static int hashCode { get; set; }
        private static Dictionary<int, int> startEndDict = new Dictionary<int, int>();
        private static Stack<int> startBracketsStack = new Stack<int>();

        private static void InitializeBraketsPositions(IVirtualMachine vm)
        {
            hashCode = vm.Instructions.GetHashCode();
            startEndDict.Clear();
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