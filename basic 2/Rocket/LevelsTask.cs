using System;
using System.Collections.Generic;

namespace func_rocket
{
	public class LevelsTask
	{
		static readonly Physics standardPhysics = new Physics();
        static readonly Physics heavyPhysics = new Physics(-0.9, 300.0, 0.15);

        public static IEnumerable<Level> CreateLevels()
		{
			yield return new Level("Zero", 
				new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),
				new Vector(600, 200), 
				(size, v) => Vector.Zero, standardPhysics);
            yield return new Level("Heavy",
                new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),
                new Vector(600, 200),
                (size, v) => Vector.Zero, heavyPhysics);
        }
	}
}