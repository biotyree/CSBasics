using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Digger
{
    public class Terrain : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return true;
        }

        public int GetDrawingPriority()
        {
            return 1;
        }

        public string GetImageFileName()
        {
            return "Terrain.png";
        }
    }

    public class Way
    {
        public Ways CurrentWay { get; set; }
        public enum Ways
        {
            Down,
            Up,
            Right,
            Left,
            IncorrectWay
        }

        public Way()
        {
            CurrentWay = Ways.IncorrectWay;
        }

        public Way(Keys currentKey)
        {
            switch (currentKey)
            {
                case Keys.Down:
                    CurrentWay = Ways.Down;
                    break;
                case Keys.Up:
                    CurrentWay = Ways.Up;
                    break;
                case Keys.Right:
                    CurrentWay = Ways.Right;
                    break;
                case Keys.Left:
                    CurrentWay = Ways.Left;
                    break;
                default:
                    CurrentWay = Ways.IncorrectWay;
                    break;
            }
        }

        public static (Way, Way) GetMovesForMonster(int deltaX, int deltaY)
        {
            var correctWay = (new Way(), new Way());

            if (deltaX > 0)
            {
                correctWay.Item1.CurrentWay = Ways.Right;
            }
            if (deltaY > 0)
            {
                correctWay.Item2.CurrentWay = Ways.Down;
            }
            if (deltaX < 0)
            {
                correctWay.Item1.CurrentWay = Ways.Left;
            }
            if (deltaY < 0)
            {
                correctWay.Item2.CurrentWay = Ways.Up;
            }

            return correctWay;
        }

        public bool IsMoveCorrectForPLayer(int x, int y)
        {
            switch (CurrentWay)
            {
                case Ways.Down:
                    if (!(Game.Map[x, y + 1] is Sack))
                        return true;
                    goto default;
                case Ways.Up:
                    if (!(Game.Map[x, y - 1] is Sack))
                        return true;
                    goto default;
                case Ways.Right:
                    if (!(Game.Map[x + 1, y] is Sack))
                        return true;
                    goto default;
                case Ways.Left:
                    if (!(Game.Map[x - 1, y] is Sack))
                        return true;
                    goto default;
                default:
                    return false;
            }
        }

        public bool IsMoveCorrectForSack(int x, int y, int dropHeight)
        {
            return Game.Map[x, y + 1] is null ||
                (dropHeight >= 1 && (Game.Map[x, y + 1] is Player || Game.Map[x, y + 1] is Monster));
        }

        public bool IsMoveCorrectForMonster(int x, int y)
        {
            switch (CurrentWay)
            {
                case Ways.Down:
                    return !(Game.Map[x, y + 1] is Terrain) &&
                        !(Game.Map[x, y + 1] is Sack) && !(Game.Map[x, y + 1] is Monster);
                case Ways.Up:
                    return !(Game.Map[x, y - 1] is Terrain) &&
                        !(Game.Map[x, y - 1] is Sack) && !(Game.Map[x, y - 1] is Monster);
                case Ways.Right:
                    return !(Game.Map[x + 1, y] is Terrain) &&
                        !(Game.Map[x + 1, y] is Sack) && !(Game.Map[x + 1, y] is Monster);
                case Ways.Left:
                    return !(Game.Map[x - 1, y] is Terrain) &&
                        !(Game.Map[x - 1, y] is Sack) && !(Game.Map[x - 1, y] is Monster);
                default:
                    return false;
            }
        }

        public bool IsWayInMapCorrect(int x, int y)
        {
            switch (CurrentWay)
            {
                case Ways.Down:
                    if (y < Game.MapHeight - 1)
                        return true;
                    goto default;
                case Ways.Up:
                    if (y > 0)
                        return true;
                    goto default;
                case Ways.Right:
                    if (x < Game.MapWidth - 1)
                        return true;
                    goto default;
                case Ways.Left:
                    if (x > 0)
                        return true;
                    goto default;
                default: return false;
            }
        }

        public CreatureCommand GetCreatureCommand()
        {
            switch (CurrentWay)
            {
                case Ways.Down:
                    return new CreatureCommand { DeltaX = 0, DeltaY = 1 };
                case Ways.Up:
                    return new CreatureCommand { DeltaX = 0, DeltaY = -1 };
                case Ways.Right:
                    return new CreatureCommand { DeltaX = 1, DeltaY = 0 };
                case Ways.Left:
                    return new CreatureCommand { DeltaX = -1, DeltaY = 0 };
                default: return new CreatureCommand { };
            }
        }
    }

    public class Player : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            var way = new Way(Game.KeyPressed);
            if (!(way.IsWayInMapCorrect(x, y) && way.IsMoveCorrectForPLayer(x, y)))
                way.CurrentWay = Way.Ways.IncorrectWay;
            return way.GetCreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Sack || conflictedObject is Monster)
            {
                Game.IsOver = true;
                return true;
            }
            return false;
        }

        public int GetDrawingPriority()
        {
            return 1;
        }

        public string GetImageFileName()
        {
            return "Digger.png";
        }
    }

    public class Sack : ICreature
    {
        public int DropHeight = 0;

        public CreatureCommand Act(int x, int y)
        {
            var way = new Way(Keys.Down);

            if (way.IsWayInMapCorrect(x, y) && way.IsMoveCorrectForSack(x, y, DropHeight))
            {
                DropHeight++;
                return way.GetCreatureCommand();
            }

            if (DropHeight > 1)
            {
                DropHeight = 0;
                return new CreatureCommand { DeltaX = 0, DeltaY = 0, TransformTo = new Gold() };
            }
            DropHeight = 0;
            return new CreatureCommand { };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }

        public int GetDrawingPriority()
        {
            return 2;
        }

        public string GetImageFileName()
        {
            return "Sack.png";
        }
    }

    public class Gold : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand { };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Player)
                Game.Scores += 10;
            return true;
        }

        public int GetDrawingPriority()
        {
            return 2;
        }

        public string GetImageFileName()
        {
            return "Gold.png";
        }
    }

    public class Monster : ICreature
    {
        public static (int, int) GetDiggerCoordinates()
        {
            for (var x = 0; x < Game.MapWidth; x++)
                for (var y = 0; y < Game.MapHeight; y++)
                    if (Game.Map[x, y] is Player)
                    {
                        return (x, y);
                    }
            return (-1, -1);
        }

        public CreatureCommand GetPath(int diggerX, int diggerY, int monsterX, int monsterY)
        {
            var deltaX = diggerX - monsterX;
            var deltaY = diggerY - monsterY;

            var way = Way.GetMovesForMonster(deltaX, deltaY);

            if (way.Item1.IsWayInMapCorrect(monsterX, monsterY) &&
                way.Item1.IsMoveCorrectForMonster(monsterX, monsterY))
                return way.Item1.GetCreatureCommand();

            if (way.Item2.IsWayInMapCorrect(monsterX, monsterY) &&
                way.Item2.IsMoveCorrectForMonster(monsterX, monsterY))
                return way.Item2.GetCreatureCommand();

            way.Item1.CurrentWay = Way.Ways.IncorrectWay;
            return way.Item1.GetCreatureCommand();
        }

        public CreatureCommand Act(int x, int y)
        {
            var diggerPlace = GetDiggerCoordinates();
            if (diggerPlace != (-1, -1))
            {
                return GetPath(diggerPlace.Item1, diggerPlace.Item2, x, y);
            }
            return new CreatureCommand { };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject is Monster || conflictedObject is Sack;
        }

        public int GetDrawingPriority()
        {
            return 3;
        }

        public string GetImageFileName()
        {
            return "Monster.png";
        }
    }
}