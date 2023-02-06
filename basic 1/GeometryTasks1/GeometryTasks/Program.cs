using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryTasks
{
    internal class Program
    {
        static int Main()
        {
            Console.WriteLine(Geometry.GetLength(new Segment() { Begin = new Vector() { X = 1, Y = 1 }, End = new Vector() { X = 5, Y = 4 } }));
            return 0;
        }
    }
}
