using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace func.brainfuck
{
    public class BrainfuckBasicCommands
    {
        public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
        {
            vm.RegisterCommand('.', b => write((char)(b.Memory[b.MemoryPointer])));
            vm.RegisterCommand(',', b => b.Memory[b.MemoryPointer] = (byte)(read()));

            RegisterCommandChangeVal(vm);
            RegisterCommandMovePtr(vm);

            RegisterCommandSymbols(vm);
        }

        private static void RegisterCommandChangeVal(IVirtualMachine vm)
        {
            vm.RegisterCommand('+', b =>
            {
                b.Memory[b.MemoryPointer] = b.Memory[b.MemoryPointer] == byte.MaxValue
                ? byte.MinValue
                : ++b.Memory[b.MemoryPointer];
            });
            vm.RegisterCommand('-', b =>
            {
                b.Memory[b.MemoryPointer] = b.Memory[b.MemoryPointer] == byte.MinValue
                ? byte.MaxValue
                : --b.Memory[b.MemoryPointer];
            });
        }

        private static void RegisterCommandMovePtr(IVirtualMachine vm)
        {
            vm.RegisterCommand('>', b =>
            {
                b.MemoryPointer = b.MemoryPointer == b.Memory.Length - 1
                ? 0
                : ++b.MemoryPointer;
            });
            vm.RegisterCommand('<', b =>
            {
                b.MemoryPointer = b.MemoryPointer == 0
                ? b.Memory.Length - 1
                : --b.MemoryPointer;
            });
        }

        private static void RegisterCommandSymbols(IVirtualMachine vm)
        {
            var symbolsRanges = new (char, char)[]
            {
                ('a', 'z'),
                ('A', 'Z'),
                ('0', '9')
            };
            var list = new List<char>();
            foreach (var range in symbolsRanges)
                for (var symbol = range.Item1; symbol <= range.Item2; symbol++)
                {
                    var currentSymbol = symbol;
                    vm.RegisterCommand(currentSymbol, b => b.Memory[b.MemoryPointer] = (byte)(currentSymbol));
                }
        }
    }
}