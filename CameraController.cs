using System;
using System.Text.RegularExpressions;

namespace Raycasting_console_3d
{

    internal class CameraController
    {
        private static double _cameraX = 5;
        private static double _cameraY = 5;
        private static double _cameraZ = 0;

        private const double _fov = Math.PI / 3;
        private const double _depth = 16; 

        public static void Movement() 
        {
            int _rangeX;
            int _rangeY;
            int _celling;
            int cameraRange;

            char wallColor;
            char floorColor;

            double _rayX;
            double _rayY;
            double rayAngle;
            double distanceWall;
            double distanceFloor;

            bool hitWall;

            DateTime dateTimeTo = DateTime.Now;
            double elapsedTime = (dateTimeTo - Engine.dateTimeFrom).TotalSeconds;
            Engine.dateTimeFrom = DateTime.Now;

            if (Console.KeyAvailable) 
            { 
                ConsoleKey keyboardButton = Console.ReadKey(true).Key;

                switch (keyboardButton) 
                {
                    case ConsoleKey.A: _cameraZ += 5 * elapsedTime; break;
                    case ConsoleKey.D: _cameraZ -= 5 * elapsedTime; break;
                    case ConsoleKey.W: _cameraX += Math.Sin(_cameraZ) * 5 * elapsedTime; _cameraY += Math.Cos(_cameraZ) * 5 * elapsedTime; if (MapController.Map[(int)_cameraY * MapController.mapWidth + (int)_cameraX] == '#') { _cameraX -= Math.Sin(_cameraZ) * 5 * elapsedTime; _cameraY -= Math.Cos(_cameraZ) * 5 * elapsedTime; } break;
                    case ConsoleKey.S: _cameraX -= Math.Sin(_cameraZ) * 5 * elapsedTime; _cameraY -= Math.Cos(_cameraZ) * 5 * elapsedTime; if (MapController.Map[(int)_cameraY * MapController.mapWidth + (int)_cameraX] == '#') { _cameraX += Math.Sin(_cameraZ) * 5 * elapsedTime; _cameraY += Math.Cos(_cameraZ) * 5 * elapsedTime; } break;
                }
            }

            for (int x = 0; x < Engine.screenWidth; x++) 
            {
                rayAngle = _cameraZ + _fov / 2 - x * _fov / Engine.screenWidth;
                _rayX = Math.Sin(rayAngle);
                _rayY = Math.Cos(rayAngle);

                distanceWall = 0;
                hitWall = false;

                while (!hitWall && distanceWall < _depth) 
                {
                    distanceWall += 0.1;

                    _rangeX = (int)(_cameraX + _rayX * distanceWall);
                    _rangeY = (int)(_cameraY + _rayY  * distanceWall);

                    if (_rangeX < 0 || _rangeX < 0 || _rangeY >= _depth + _cameraY || _rangeX >= _depth + _cameraX) 
                    { 
                        hitWall = true;
                        distanceWall = _depth;
                    } else
                    {
                        char mapCell = MapController.Map[_rangeY * MapController.mapWidth + _rangeX];

                        if (mapCell == '#') hitWall = true;
                        else MapController.Map[_rangeY * MapController.mapWidth + _rangeX] = '-';
                    }

                    _celling = (int)(Engine.screenHeight / 2d - Engine.screenHeight * _fov / distanceWall);
                    cameraRange = Engine.screenHeight - _celling;

                    if (distanceWall <= _depth / 4d) wallColor = '\u2588';
                    else if (distanceWall < _depth / 3d) wallColor = '\u2593';
                    else if (distanceWall < _depth / 2d) wallColor = '\u2592';
                    else if (distanceWall < _depth) wallColor = '\u2591';
                    else wallColor = ' ';

                    for (int y = 0; y < Engine.screenHeight; y++) 
                    {
                        if (y <= _celling) Engine.screenSize[y * Engine.screenWidth + x] = ' ';
                        else if (y > _celling && y <= cameraRange) Engine.screenSize[y * Engine.screenWidth + x] = wallColor;
                        else
                        {
                            distanceFloor = 1 - (y - Engine.screenHeight / 2d) / (Engine.screenHeight / 2d);

                            if (distanceFloor < 0.25) floorColor = '+';
                            else if (distanceFloor < 0.5) floorColor = '*';
                            else if (distanceFloor < 0.75) floorColor = '-';
                            else floorColor = '.';

                            Engine.screenSize[y * Engine.screenWidth + x] = floorColor;
                        }
                    }
                }
            }

            for (int x = 0; x < MapController.mapWidth; x++) { for (int y = 0; y < MapController.mapHeight; y++) Engine.screenSize[(y + 1) * Engine.screenWidth + x] = MapController.Map[y * MapController.mapWidth + x]; }
            Engine.screenSize[(int)(_cameraY + 1) * Engine.screenWidth + (int)_cameraX] = 'P';
        }
    }
}
