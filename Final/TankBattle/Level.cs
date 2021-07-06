using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    public class Level
    {
        public const int WIDTH = 160;                       // Level's Width
        public const int HEIGHT = 120;                      // Level's Height

        bool[,] levelArray = new bool[WIDTH, HEIGHT];       // bool array associated with Level

        /// <summary>
        /// Level's constructor. Draws the level assigning it to level using a randomly
        /// generated sin function to build the shape of Level 
        /// </summary>
        public Level()
        {
            int[] yArray = new int[WIDTH];                                                                      // Array which determines the maximum Y Value in levelArray along each X point in levelArray

            Random random = new Random();
            float AmplitudeSeed = random.Next(0, (HEIGHT / 4) - TankType.HEIGHT);                               // The Peak value of the sin function. Determines how far away the Peak Y value deviates from zeroPoint
            float freqSeed = random.Next((WIDTH/8), (WIDTH/2));                                                 // The Frequency of the of the sin function. Determines wide the peaks will be from each other.  
            int zeroPoint = random.Next(30, HEIGHT - 30 - TankType.HEIGHT);                                     // The X row at which the sin function rotates around

            for (int i = 0; i < WIDTH; i++)
            {
                yArray[i] =  Convert.ToInt32(zeroPoint + (AmplitudeSeed * Math.Sin(6.28 * freqSeed * i)));      // Sin function is the equivalant of Y = A + (Bsin(2*pi * f * i) 
    
                for (int j = yArray[i]; j < HEIGHT; j++) { levelArray[i, j] = true; }                           // Fill values in the corresponding Y coloumn lower than the yarray value. 
            }
        }

        /// <summary>
        /// Determines whether there is terrain found 
        /// at the X & Y vlues passed to it. Returns
        /// true if the the value at x,y in the levelArray
        /// is set and false otherwise.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>bool true or false</returns>
        public bool TerrainAt(int x, int y)
        {
            if(levelArray[x,y] == true) { return true; }
            else { return false; }    
        }

        /// <summary>
        /// Checks if there is room for the TankType
        /// in the surrounding area of the levelArray 
        /// at the points passed to it. Reutrns true 
        /// if there is a collision. 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>bool true or false></returns>
        public bool CheckTankCollision(int x, int y)
        {
            int xIndex = x;                                                 // Values used to index collision Area
            int yIndex = y;                                                 //

            while(yIndex < (y + TankType.HEIGHT))
            {
                if (levelArray[xIndex, yIndex] == true) { return true; }    // is there
                else
                {
                    xIndex++;                                               // Iterate X indexer 
                    if(xIndex == x + TankType.WIDTH)                        // Finished X row of tank Area? 
                    {
                        xIndex = x;
                        yIndex++;                 
                    }
                }
            }

            return false;                                                   // No Collsions so return false 
        }

        /// <summary>
        /// Finds the lowest Y point in 
        /// the levelArray column without
        /// terrain by calling CheckTankCollision. 
        /// and returns the Y value.
        /// </summary>
        /// <param name="x"></param>
        /// <returns>int between 0 & Level.HEIGHT-TankType.HEIGHT</returns>
        public int TankPlace(int x)
        {   
            for(int i = HEIGHT - TankType.HEIGHT-1; i >= 0; i--)
            {
               if (CheckTankCollision(x, i) == false)                       
               {
                    return i;                                               // Y point in levelArray with room for TankType
               }
            }

            return HEIGHT;                                                  // There will always be room at top of Array if for loop fails
        }

        /// <summary>
        /// Destroys all terrain within a circle 
        /// around the explosion Radius.
        /// </summary>
        /// <param name="destroyX"></param>
        /// <param name="destroyY"></param>
        /// <param name="radius"></param>
        public void DestroyTiles(float destroyX, float destroyY, float radius)
        {
            for(int indexY = 0; indexY < HEIGHT; indexY++)
            {
                for(int indexX = 0; indexX < WIDTH; indexX++)
                {
                    float distance = Convert.ToSingle(Math.Sqrt(Math.Pow(destroyX - indexX, 2) + Math.Pow(destroyY - indexY, 2)));      // Distance from explosion to index point

                    if (distance < radius) { levelArray[indexX, indexY] = false; }                                                      // Destroy Tiles within blast radius
                }
            }
        }

        /// <summary>
        /// Moves any terrain with an
        /// empty space beneath it down
        /// one tile. Returns true if 
        /// terrain moved and false
        /// otherwise.
        /// </summary>
        /// <returns>bool true or false</returns>
        public bool CalculateGravity()
        {
            bool terrainDropped = false;                                                                                                // bool returned

            for (int indexX = 0; indexX < WIDTH; indexX++)
            {
                for (int indexY = HEIGHT-2; indexY > 1; indexY--)                                           
                {
                    if((levelArray[indexX,indexY+1] == false) && (levelArray[indexX, indexY] == true))                                  // if there's a free space below a set array point.
                    {
                        levelArray[indexX, indexY + 1] = true;                                                                          // move the tile down one place in levelArray
                        levelArray[indexX, indexY] = false;                                                                             //

                        terrainDropped = true;                                                                                          // terrain was dropped
                    }
                }
            }

            return terrainDropped;                                                                                                      // if method gets to this, point no terrained dropped
        }
    }


} 