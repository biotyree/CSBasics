using System;

namespace func.brainfuck
{
    public class BrainfuckBasicCommands
    {
        public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
        {
            vm.RegisterCommand('.', b => write(Convert.ToChar(b.Memory[b.MemoryPointer])));

            RegisterCommandChangeVal(vm);

            RegisterCommandMovePtr(vm);

            vm.RegisterCommand(',', b => b.Memory[b.MemoryPointer] = Convert.ToByte(read()));

            foreach (var symbol in "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890")
            {
                vm.RegisterCommand(symbol, b => b.Memory[b.MemoryPointer] = Convert.ToByte(symbol));
            }
        }

        private static void RegisterCommandChangeVal(IVirtualMachine vm)
        {
            vm.RegisterCommand('+', b =>
            {
                if (b.Memory[b.MemoryPointer] == byte.MaxValue)
                    b.Memory[b.MemoryPointer] = byte.MinValue;
                else
                    b.Memory[b.MemoryPointer]++;
            });
            vm.RegisterCommand('-', b =>
            {
                if (b.Memory[b.MemoryPointer] == byte.MinValue)
                    b.Memory[b.MemoryPointer] = byte.MaxValue;
                else
                    b.Memory[b.MemoryPointer]--;
            });
        }

        private static void RegisterCommandMovePtr(IVirtualMachine vm)
        {
            vm.RegisterCommand('>', b =>
            {
                if (b.MemoryPointer == b.Memory.Length - 1)
                    b.MemoryPointer = 0;
                else
                    b.MemoryPointer++;
            });
            vm.RegisterCommand('<', b =>
            {
                if (b.MemoryPointer == 0)
                    b.MemoryPointer = b.Memory.Length - 1;
                else
                    b.MemoryPointer--;
            });
        }
    }
}