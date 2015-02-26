using System;
using System.Collections.Generic;
using System.Linq;

namespace RouteDefense.Core.Gameplay
{
    public class PathGenerator
    {
        public enum Direction
        {
            Down = 1,
            Up = 2,
            Right = 4,
        }

        public class Path : List<Direction>
        {
            private static double curve = 1.0;
            private static double curveDecreaser;


            public double CurveDecreaser
            {
                get { return PathGenerator.Path.curveDecreaser; }

                set { this.CurveDecreaser = value; }
            }

            public static Direction GetNewDirection(Direction allowed, Random rnd)
            {
                Direction newd;
                int maxd = Enum.GetValues(typeof(Direction)).Length;
                int[] vals = (int[])Enum.GetValues(typeof(Direction));

                do
                {
                    var t = rnd.Next(0, maxd);
                    newd = (Direction)vals[t];
                }
                while ((newd & allowed) == 0);
                return newd;
            }

            public static Path GenerateRandomPath(int startX, int startY, int endX, int endY)
            {
                Path newPath = new Path();
                Random rnd = new Random();
                curveDecreaser = 0.1;

                int currentX = startX;
                int currentY = startY;

                Direction doublePrevDirection = Direction.Down;
                Direction previousDirection = Direction.Right;
                Direction currentDirectin = Direction.Right;
                Direction newDirection = currentDirectin;

                while (currentX != endX)
                {
                    do
                    {
                        if (currentDirectin != previousDirection)
                        {
                            curve -= curveDecreaser;
                        }
                        if (curve <= 0)
                        {
                            newDirection = Direction.Right;
                        }
                        else if (currentDirectin == previousDirection && previousDirection == doublePrevDirection)
                        {
                            if (currentY >= (endY - 3)) newDirection = GetNewDirection(Direction.Up | Direction.Right, rnd); //bottom border is reached, only up and right allowed
                            else if (currentY <= 3) newDirection = GetNewDirection(Direction.Down | Direction.Right, rnd);  //top border is rached, only down and right allowed
                            else newDirection = GetNewDirection(Direction.Right | Direction.Down | Direction.Up, rnd); //all directions are possible

                        }
                    }
                    while ((newDirection | currentDirectin) == (Direction.Up | Direction.Down)); // excluding going back

                    if ((newDirection == Direction.Up) && (currentY - 1 == 1))
                    {
                        newDirection = Direction.Right;
                        curve -= curveDecreaser;
                    }

                    newPath.Add(newDirection);
                    doublePrevDirection = previousDirection;
                    previousDirection = currentDirectin;
                    currentDirectin = newDirection;

                    switch (newDirection)
                    {
                        case Direction.Up:
                            currentY--;
                            break;
                        case Direction.Right:
                            currentX++;
                            break;
                        case Direction.Down:
                            currentY++;
                            break;
                    }

                }
                return newPath;
            }
        }

        public static void InitializeStartEndPoints(int maxHeight, int maxWidth, out Tuple<int, int> startPoint, out Tuple<int, int> endPoint)
        {
            Random rand = new Random();

            startPoint = new Tuple<int, int>(0, maxHeight / 2); // startpoint = leftside
            endPoint = new Tuple<int, int>(maxWidth, rand.Next(2, maxHeight - 1));  // startpoint = rightside
        }

        public static List<Tuple<int, int>> GeneratePath(int maxWidth, int maxHeight)
        {
            Tuple<int, int> startPoint;
            Tuple<int, int> endPoint;

            PathGenerator.InitializeStartEndPoints(maxHeight, maxWidth, out startPoint, out endPoint);
            Path path = Path.GenerateRandomPath(startPoint.Item1, startPoint.Item2, maxWidth - 1, maxHeight - 1);
            List<Tuple<int, int>> nodeCordinates = new List<Tuple<int, int>>();
            nodeCordinates.Add(startPoint);

            for (int i = 0; i < path.Count; i++)
            {
                var lastCordinate = nodeCordinates.Last();
                switch (path[i])
                {
                    case Direction.Down:
                        nodeCordinates.Add(new Tuple<int, int>(lastCordinate.Item1, lastCordinate.Item2 + 1));
                        break;
                    case Direction.Up:
                        nodeCordinates.Add(new Tuple<int, int>(lastCordinate.Item1, lastCordinate.Item2 - 1));
                        break;
                    case Direction.Right:
                        nodeCordinates.Add(new Tuple<int, int>(lastCordinate.Item1 + 1, lastCordinate.Item2));
                        break;
                }
            }
            return nodeCordinates;
        }
    }
}
