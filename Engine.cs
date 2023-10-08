using System;

namespace Raycasting_console_3d
{
    class Engine
    {
        public const int screenHeight = 100;
        public const int screenWidth = 230;

        public static readonly char[] screenSize = new char[screenHeight * screenWidth];

        public static DateTime dateTimeFrom;

        static void Main()
        {
            Console.CursorVisible = false;

            Console.SetWindowSize(screenWidth, screenHeight);
            Console.SetBufferSize(screenWidth, screenHeight);

            dateTimeFrom = DateTime.Now;

            while (true)
            {
                MapController.GetMap();
                CameraController.Movement();

                Console.SetCursorPosition(0, 0);
                Console.Write(screenSize);
            }

        }
    }
}
