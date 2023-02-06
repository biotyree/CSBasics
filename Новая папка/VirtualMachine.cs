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

        private Dictionary<char, Action<IVirtualMachine>> commands { get; set; }

        public VirtualMachine(string program, int memorySize = 30000)
        {
            Instructions = program;
            InstructionPointer = 0;

            Memory = new byte[memorySize];
            MemoryPointer = 0;

            commands = new Dictionary<char, Action<IVirtualMachine>>();
        }

        public void RegisterCommand(char symbol, Action<IVirtualMachine> execute)
        {
            commands[symbol] = execute;
        }

        public void Run()
        {
            while (InstructionPointer < Instructions.Length)
            {
                if (commands.ContainsKey(Instructions[InstructionPointer]))
                {
                    commands[Instructions[InstructionPointer]].Invoke(this);
                }
                InstructionPointer++;
            }
        }
    }
}