using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.MapManager
{
    public class MapHelper
    {
        private static void PrintMap(int[,] array, MapConfig mapConfig)
        {
            for (int y = 0; y < mapConfig.GetLevels(); y++)
            {
                string row = "";
                for (int x = 0; x < mapConfig.GetWidth(); x++)
                {
                    row += array[x, y];
                }

                Debug.Log(row);
            }
        }
        
        private static IEnumerable<Point> GetNeighbors(Point p, MapConfig mapConfig, int[,] array)
        {
            // Change neighbors to be x neighbors only.
            List<Point> neighbors = new List<Point>();
            if (p.x > 0 && array[p.x - 1, p.y] == 1)
            {
                neighbors.Add(new Point(p.x - 1, p.y));
            }

            if (p.x < mapConfig.GetWidth() - 1 && array[p.x + 1, p.y] == 1)
            {
                neighbors.Add(new Point(p.x + 1, p.y));
            }

            if (p.y > 0 && array[p.x, p.y - 1] == 1)
            {
                neighbors.Add(new Point(p.x, p.y - 1));
            }

            if (p.y < mapConfig.GetLevels() - 1 && array[p.x, p.y + 1] == 1)
            {
                neighbors.Add(new Point(p.x, p.y + 1));
            }

            return neighbors;
        }

        private static IEnumerable<Point> GetCone(Point p, MapConfig mapConfig, int[,] array)
        {
            //TODO: Implement this. This will be used to fetch a cone from a certain point.
            return null;
        }

    }
}