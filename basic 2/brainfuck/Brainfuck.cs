using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace func.brainfuck
{
    //var vmBuilder = new VmBuilder(memSize: 60)
    //.AddBasicCommands(() => Console.Read(), c => Console.Write(c))
    //.AddLoopCommands();
    //vmBuilder.Build(program1).Run(); // Build(...) возвращает созданную Vm
    //vmBuilder.Build(program2).Run();

    public class VmBuilder
    {
		private Func<string, VirtualMachine> vmCommands;


        public VmBuilder( int memSize = 60)
		{
			vmCommands = program => new VirtualMachine(program, memSize);
		}

		public VmBuilder AddBasicCommands(Func<int> read, Action<char> write)
		{
			var prevCommand = vmCommands;
			vmCommands = x =>
			{
				var vm = prevCommand(x);
				BrainfuckBasicCommands.RegisterTo(vm, read, write);
				return vm;
            };

            //addBasicCommands = x => BrainfuckBasicCommands.RegisterTo(x, read, write);
			return this;
		}

		public VmBuilder AddLoopCommands()
		{
            var prevCommand = vmCommands;
            vmCommands = x =>
			{ 
				var vm = prevCommand(x);
                BrainfuckLoopCommands.RegisterTo(vm);
				return vm;
            };
            //addLoopCommands = (x) => BrainfuckLoopCommands.RegisterTo(x);
            return this;
        }

		public IVirtualMachine Build(string program)
		{
			var vm = vmCommands(program);
			//var vm = new VirtualMachine(program, memSize);
			//addBasicCommands?.Invoke(vm);
			//addLoopCommands?.Invoke(vm);
            return vm;
		}
    }

    public class Brainfuck
	{
		public static string Run(string program, string input = "")
		{
			var inputIndex = 0;
			var output = new StringBuilder();
			Run(program,
				() => inputIndex >= input.Length ? -1 : input[inputIndex++],
				c => output.Append(c));
			return output.ToString();
		}

		public static void Run(string program, Func<int> read, Action<char> write, int memorySize = 30000)
		{
			//var vm = new VirtualMachine(program, memorySize);
			//BrainfuckBasicCommands.RegisterTo(vm, read, write);
			//BrainfuckLoopCommands.RegisterTo(vm);
			//vm.Run();
			var vmBuilder = new VmBuilder(memorySize)
			.AddBasicCommands(read, write)
			.AddLoopCommands();
			vmBuilder.Build(program).Run();
			// Build(...) возвращает созданную Vm
			//vmBuilder.Build(program2).Run();
		}
	}
}