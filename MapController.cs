using System;
using System.Text;

namespace Raycasting_console_3d
{
    public class MapController
    {
        public static StringBuilder Map = new StringBuilder();

        public const int mapWidth = 32;
        public const int mapHeight = 24;

        public static void GetMap() 
        {
            Map.Clear();
            Map.Append("################################");
            Map.Append("#..............................#");
            Map.Append("#..............................#");
            Map.Append("#.....................##########");
            Map.Append("#..............................#");
            Map.Append("#.................#............#");
            Map.Append("#.................#............#");
            Map.Append("#...............#####..........#");
            Map.Append("#....######.......#............#");
            Map.Append("#.........#.......#............#");
            Map.Append("#.........#....................#");
            Map.Append("#.........#....................#");
            Map.Append("#.........#....................#");
            Map.Append("#..............................#");
            Map.Append("#..............................#");
            Map.Append("#.................##############");
            Map.Append("#.................#............#");
            Map.Append("#.................#............#");
            Map.Append("#.................#............#");
            Map.Append("#.................#............#");
            Map.Append("#..............................#");
            Map.Append("#..............................#");
            Map.Append("#..............................#");
            Map.Append("################################");
        }
    }
}
