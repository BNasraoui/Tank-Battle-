using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TankBattle
{
    public class Missile : WeaponEffect
    {
        private float missileX;                 // Missile's X position
        private float missileY;                 // Missile's Y position
        private float missileAngle;             // Missile's Angle
        private float missileGravity;           // Missile's Gravity

        private float xVelocity;                // X Velocity of Missile
        private float yVelocity;                // Y Velocity of Missile   

        private Shrapnel missileExplosion;      // Shrapnel associated with Missile
        private Player missilePlayer;           // Player associated with Missile

        /// <summary>
        /// Constructor of Missile. Inherits from Weapon Effect. Passes in Missile's X&Y positions, power,gravity
        /// Shrapnel, and Player. Also converts Missile's angle to radians and stores the value in angleRadians,
        /// as well as initialising the magnitude, and directional velocities xVelocity and yVelocity. 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="angle"></param>
        /// <param name="power"></param>
        /// <param name="gravity"></param>
        /// <param name="explosion"></param>
        /// <param name="player"></param>
        public Missile(float x, float y, float angle, float power, float gravity, Shrapnel explosion, Player player)
        {
            missileX = x;                                                   // Passed variables stored in private variables.
            missileY = y;                                                   //
            missileAngle = angle;                                           //
            missileGravity = gravity;                                       //
            missileExplosion = explosion;                                   //
            missilePlayer = player;                                         //

            float angleRadians = (90 - angle) * (float)Math.PI / 180;       // angle converted to Radians
            float magnitude = power / 50;                                   
            xVelocity = Convert.ToSingle(Math.Cos(angleRadians) * magnitude);           
            yVelocity = Convert.ToSingle(Math.Sin(angleRadians) * -magnitude); 
        }

        /// <summary>
        /// Moves missile along it's trajectory
        /// checking if it has hit anything or
        /// left the map area.
        /// </summary>
        public override void Process()
        {
            for (int i = 0; i < 10; i++)
            {
                missileX += xVelocity;                                                                                  // iterate Missile position
                missileY += yVelocity;                                                                                  //

                missileX += (currentGame.WindSpeed() / 1000.0f);                                                        // factor WindSpeed into Missile's X position 

                if ((missileX >= Level.WIDTH-1) || (missileX < 0) || (missileY >= Level.HEIGHT-1) || missileY < 0)      // Missile off map
                {
                    currentGame.RemoveWeaponEffect(this);                                                               // Remove Missile
                    return;                                                                                             
                }
                else
                {
                    if(currentGame.CheckHitTank(missileX, missileY ) == true)                                           // Check if the Missile hit anything.
                    {
                        missilePlayer.ReportHit(missileX, missileY);                                                    // Report the hit 
                        missileExplosion.Ignite(missileX, missileY);                                                    // Detonate Missile
                        currentGame.AddWeaponEffect(missileExplosion);                                                  // Create Explosion
                        currentGame.RemoveWeaponEffect(this);                                                           // Destroy the Missile
                        return;                                                 
                    }
                   
                }
                yVelocity += missileGravity;                                                                            // Add Gravity to Missile
            }
        }

        /// <summary>
        /// Draws the Missile. Code Prvided to in Assignment.
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="size"></param>
        public override void Render(Graphics graphics, Size size)
        {
            float x = (float)this.missileX * size.Width / Level.WIDTH;
            float y = (float)this.missileY * size.Height / Level.HEIGHT;
            float s = size.Width / Level.WIDTH;

            RectangleF r = new RectangleF(x - s / 2.0f, y - s / 2.0f, s, s);
            Brush b = new SolidBrush(Color.WhiteSmoke);

            graphics.FillEllipse(b, r);
        }
    }
}
