using System;
using System.Windows.Forms;

namespace Digger
{
    class Terrain : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand { };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return true;
        }

        public int GetDrawingPriority()
        {
            return 0;
        }

        public string GetImageFileName()
        {
            return "Terrain.png";
        }
    }
    class Player : ICreature
    {
        public static int X = 0;
        public static int Y = 0;
        public CreatureCommand Act(int x, int y)
        {
            X = x; Y = y;
            switch (Game.KeyPressed)
            {
                case Keys.Up:
                    if (y != 0 && CheckCanMove(x, y-1)) return new CreatureCommand { DeltaX = 0, DeltaY = -1 };
                    break;
                case Keys.Down:
                    if (y != Game.MapHeight - 1 && CheckCanMove(x, y+1)) return new CreatureCommand { DeltaX = 0, DeltaY = 1 };
                    break;
                case Keys.Right:
                    if (x != Game.MapWidth - 1 && CheckCanMove(x+1, y)) return new CreatureCommand { DeltaX = 1, DeltaY = 0 };
                    break;
                case Keys.Left:
                    if (x != 0 && CheckCanMove(x-1, y)) return new CreatureCommand { DeltaX = -1, DeltaY = 0 };
                    break;
            }
            return new CreatureCommand { DeltaX = 0, DeltaY = 0 }; ;
        }
        public bool CheckCanMove(int x, int y)
        {
            return Game.Map[x, y] == null || Game.Map[x, y].GetImageFileName() != "Sack.png";
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject.GetImageFileName() == "Sack.png" || conflictedObject.GetImageFileName() == "Monster.png";
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

    class Sack : ICreature
    {
        private int FallingCellsCount = 0;

        public CreatureCommand Act(int x, int y)
        {
            while (y != Game.MapHeight - 1)
            {
                if (Game.Map[x, y + 1] == null || 
                    ((Game.Map[x, y + 1].GetImageFileName() == "Digger.png" 
                    || Game.Map[x, y + 1].GetImageFileName() == "Monster.png") && FallingCellsCount > 0))

                {
                    FallingCellsCount++;
                    return new CreatureCommand { DeltaX = 0, DeltaY = 1 };
                }
                else if (FallingCellsCount > 1)
                    return new CreatureCommand { DeltaX = 0, DeltaY = 0, TransformTo = new Gold() };
                else
                {
                    FallingCellsCount = 0;
                    return new CreatureCommand { };
                }
            }

            if (FallingCellsCount > 1)
                return new CreatureCommand { DeltaX = 0, DeltaY = 0, TransformTo = new Gold() };
            else
            {
                FallingCellsCount = 0;
                return new CreatureCommand { };
            }
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
    class Gold : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand { DeltaX = 0, DeltaY = 0 };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject.GetImageFileName() == "Digger.png") Game.Scores += 10;
            return true;
        }

        public int GetDrawingPriority()
        {
            return 3;
        }

        public string GetImageFileName()
        {
            return "Gold.png";
        }
    }

    class Monster : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            int deltaX = 0;
            int deltaY = 0;
            if (CheckPlayerAlive())
            {
                if (Player.X == x)
                {
                    if (Player.Y < y) deltaY = -1;
                    else if (Player.Y > y) deltaY = 1;
                }
                else if (Player.Y == y)
                {
                    if (Player.X < x) deltaX = -1;
                    else if (Player.X > x) deltaX = 1;
                }
                else
                {
                    if (Player.X < x) deltaX = -1;
                    else if (Player.X > x) deltaX = 1;
                }
            }
            else return new CreatureCommand() { };

            var nextMovePosition = Game.Map[x + deltaX, y + deltaY];
            if (nextMovePosition != null && CheckCanMove(x + deltaX, y + deltaY))
                return new CreatureCommand() { DeltaX = 0, DeltaY = 0 };

            return new CreatureCommand() { DeltaX = deltaX, DeltaY = deltaY };
        }

        public bool CheckPlayerAlive()
        {
            for (var x = 0; x < Game.MapWidth; x++)
            {
                for (var y = 0; y < Game.MapHeight; y++)
                {
                    if (Game.Map[x, y] is Player)
                    {
                        Player.X = x;
                        Player.Y = y;
                        return true;
                    }
                }
            }
            return false;
        }
        public bool CheckCanMove(int x, int y)
        {
            return Game.Map[x, y].GetImageFileName() == "Sack.png" || Game.Map[x, y].GetImageFileName() == "Monster.png" || Game.Map[x, y].GetImageFileName() == "Terrain.png";
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject.GetImageFileName() == "Sack.png" || conflictedObject.GetImageFileName() == "Monster.png";
        }

        public int GetDrawingPriority()
        {
            return 1;
        }

        public string GetImageFileName()
        {
            return "Monster.png";
        }
    }
}