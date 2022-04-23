using System;
using System.Collections.Generic;

namespace KIngPath
{
    public class Coords
    {
        public int x, y;
        public int length;

        public Coords(int coord_x, int coord_y, int len)
        {
            x = coord_x;
            y = coord_y;
            length = len;
        }
    }

    class Program
    {
        static bool is_legit(HashSet<(int, int)> obstacles,int x, int y)
        {
            if (!obstacles.Contains((x, y)) && (1 <= x) && (x <= 8) && (1 <= y) && (y <= 8))
            {
                return true;
            }
            return false;
        }

        static int ret(int[] start, int[] goal, HashSet<(int, int)> obstacles)
        {
            int[][] moves = new int[8][];
            var queue = new Queue<Coords>();
            var visited = new HashSet<(int,int)>();
            int x_temp, y_temp, length;
            var point = new Coords(start[0], start[1], 0);
            
            
            queue.Enqueue(point);

            while (queue.Count > 0)
            {
                var location = queue.Dequeue();
                x_temp = location.x;
                y_temp = location.y;
                length = location.length;
                

                if (x_temp == goal[0] && y_temp == goal[1])
                {
                    return length;
                }

                moves[0] = new int[] {x_temp + 1, y_temp + 1};
                moves[1] = new int[] {x_temp + 1, y_temp};
                moves[2] = new int[] {x_temp + 1, y_temp - 1};
                moves[3] = new int[] {x_temp, y_temp - 1};
                moves[4] = new int[] {x_temp - 1, y_temp - 1};
                moves[5] = new int[] {x_temp - 1, y_temp};
                moves[6] = new int[] {x_temp - 1, y_temp + 1};
                moves[7] = new int[] {x_temp, y_temp + 1};
                foreach (var cur in moves)
                {
                    if (is_legit(obstacles,cur[0],cur[1]) && !visited.Contains((cur[0],cur[1])))
                    {
                        visited.Add((cur[0],cur[1]));
                        queue.Enqueue(new Coords(cur[0],cur[1],length+1));
                    }
                }
            }
            return -1;
        }
        
        public static void Main(string[] args)
        {
            int numOfInps = Convert.ToInt32(Console.ReadLine());
            HashSet<(int, int)> obstacles = new HashSet<(int, int)> { };
            for (int i = 0; i < numOfInps; i++)
            {
                string[] inp = Console.ReadLine().Split();
                obstacles.Add((Convert.ToInt32(inp[0]), Convert.ToInt32(inp[1])));
            }

            int[] start = new int[2];
            string[] inp_start = Console.ReadLine().Split();
            start[0] = Convert.ToInt32(inp_start[0]);
            start[1] = Convert.ToInt32(inp_start[1]);

            int[] end = new int[2];
            string[] inp_end = Console.ReadLine().Split();
            end[0] = Convert.ToInt32(inp_end[0]);
            end[1] = Convert.ToInt32(inp_end[1]);


            Console.WriteLine(ret(start, end, obstacles));

        }
    }
}