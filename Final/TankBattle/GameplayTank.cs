using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    public class GameplayTank
    {
       
        private int tankXPos;           // GameplayTank's Horizontal Position in Level
        private int tankYPos;           // GameplayTank's Vertical Position in Level
        private int currentHealth;      // GameplayTank's health
        private int turretPower;        // GameplayTank's TurretPower
        private int currentWeapon;      // GameplayTank's CurrentWeapom


        private float currentAngle;     // GameplayTank's angle
        private Bitmap currentBmp;      // GameplayTank's Bitmap
        private Color tankColour;       // GameplayTank's (Color)

        private TankType currentTank;   // GameplayTank's TankType 
        private Player currentPlayer;   // GameplayTank's Player
        private Battle currentBattle;   // GameplayTank's Batt;e

        /// <summary>
        /// GamePlay Tank's Constructor passes in the GamePlayTank's Player,
        /// X position, Y Position and Battle to currentPlayer, tankXPos,
        /// tankYPos, and currentBattle. It also sets the GamePlayTank's
        /// currentAngle, turretPower, currentWeapon, and creates the Tank
        /// Bitmap associated with the GamePlayTank
        /// </summary>
        /// <param name="player"></param>
        /// <param name="tankX"></param>
        /// <param name="tankY"></param>
        /// <param name="game"></param>
        public GameplayTank(Player player, int tankX, int tankY, Battle game)
        {
            tankXPos = tankX;                                                       // Pass Values from constructor
            tankYPos = tankY;                                                       //
            currentPlayer = player;                                                 //
            currentBattle = game;                                                   //

            currentTank = currentPlayer.CreateTank();                               // TankType Associated with GamePlayTank
            currentHealth = currentTank.GetHealth();                                // TankType's initial health

            currentAngle = 0;
            turretPower = 25;
            currentWeapon = 0;

            tankColour = currentPlayer.GetTankColour();                             // Get the Color associated with GameplayTank's TankType 
            currentBmp = currentTank.CreateTankBitmap(tankColour, currentAngle);    // Create the tank bitmap
           
        }

        /// <summary>
        /// Returns the currenPlayer.
        /// </summary>
        /// <returns>Player</returns>
        public Player Player()
        {
            return currentPlayer;
        }

        /// <summary>
        /// Creates a TankType associated 
        /// with the current Player.
        /// </summary>
        /// <returns></returns>
        public TankType CreateTank()
        {
            return currentPlayer.CreateTank();
        }

        /// <summary>
        /// Returns the currentAngle.
        /// </summary>
        /// <returns>Float between -90 & 90</returns>
        public float GetAngle()
        {
            return currentAngle;
        }

        /// <summary>
        /// Update's the currentAngle with
        /// using the value passed in and
        /// Redraws the TankType Bitmap using 
        /// the updated currentAngle.
        /// </summary>
        /// <param name="angle"></param>
        public void AimTurret(float angle)
        {
            currentAngle = angle;
            currentBmp = currentTank.CreateTankBitmap(tankColour, currentAngle);    
        }

        /// <summary>
        /// Returns the turretPower
        /// </summary>
        /// <returns>int between 5 & 100</returns>
        public int GetTankPower()
        {
            return turretPower;
        }

        /// <summary>
        /// Sets the turretPower to the value(int) 
        /// passed in.
        /// </summary>
        /// <param name="power"></param>
        public void SetTurretPower(int power)
        {
            turretPower = power;
        }

        /// <summary>
        /// Returns currentWeapon
        /// </summary>
        /// <returns>int between 0 and number of Weapons</returns>
        public int GetCurrentWeapon()
        {
            return currentWeapon;
        }

        /// <summary>
        /// Sets currentWeapon to the value
        /// passed in.
        /// </summary>
        /// <param name="newWeapon"></param>
        public void SetWeaponIndex(int newWeapon)
        {
            currentWeapon = newWeapon;
        }

        /// <summary>
        /// Draws the GameplayTank to Grapics, scaled to the 
        /// displaySize passed in.
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="displaySize"></param>
        public void Render(Graphics graphics, Size displaySize)
        {
            int startingArmour = 100;                                                                           // Code Provided in Assignment
            int drawX1 = displaySize.Width * tankXPos / Level.WIDTH;
            int drawY1 = displaySize.Height * tankYPos / Level.HEIGHT;
            int drawX2 = displaySize.Width * (tankXPos + TankType.WIDTH) / Level.WIDTH;
            int drawY2 = displaySize.Height * (tankYPos + TankType.HEIGHT) / Level.HEIGHT;
            graphics.DrawImage(currentBmp, new Rectangle(drawX1, drawY1, drawX2 - drawX1, drawY2 - drawY1));

            int drawY3 = displaySize.Height * (tankYPos - TankType.HEIGHT) / Level.HEIGHT;
            Font font = new Font("Arial", 8);
            Brush brush = new SolidBrush(Color.White);

            int pct = currentHealth * 100 / startingArmour;
            if (pct < 100)
            {
                graphics.DrawString(pct + "%", font, brush, new Point(drawX1, drawY3));
            }
        }
        
        /// <summary>
        /// Returns the GameplayTanks's
        /// horizontal position (tankXPos).
        /// </summary>
        /// <returns>int between 0 & Level.Width - TankType.Width</returns>
        public int X()
        {
            return tankXPos;
        }
        /// <summary>
        /// Returns the GameplayTanks's
        /// vertical position (tankYPos).
        /// </summary>
        /// <returns>int between 0 & Level.Height - TankType.Width</returns>
        public int GetYPos()
        {
            return tankYPos;
        }

        /// <summary>
        /// Calls the associated TankType's
        /// FireWeapon() passing in the
        /// currentWeapon, GamePlaytank, and 
        /// currentBattle.
        /// </summary>
        public void Shoot()
        {
            currentTank.FireWeapon(currentWeapon,this, currentBattle);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="damageAmount"></param>
        public void DamagePlayer(int damageAmount)
        {
            currentHealth -= damageAmount;
        }

        /// <summary>
        /// Checks the GameplayTanks currentHealth
        /// and returns false if the health is less
        /// than or equal to zero or true if otherwise
        /// </summary>
        /// <returns>bool true or false</returns>
        public bool IsAlive()
        {
            if(currentHealth > 0) { return true; }
            else { return false; }
        }

        /// <summary>
        /// Moves the GameplayTank
        /// down one tile if possible
        /// and applies appropriate 
        /// fall damage. Returns
        /// whether the tank moved
        /// or not.
        /// </summary>
        /// <returns>bool true or false</returns>
        public bool CalculateGravity()
        {
            if (IsAlive() == false) { return false; }                                                               // Don't perform Gravity checks on dead GameplayTanks
            Level currentLevel = currentBattle.GetMap();                                                            // Level associated with currentBattle

            if (currentLevel.CheckTankCollision(tankXPos,tankYPos+TankType.HEIGHT) == true) { return false; }       // Check if there is something below the GameplayTank     
            else
            {
                tankYPos++;                                                                                         // Move Gameplaytank down 1 space
                currentHealth--;                                                                                    // GameplayTank takes fall damage 
                if (tankYPos > Level.HEIGHT - TankType.HEIGHT) { currentHealth = 0; }                               // If GameplayTank fell off the Map it dies
                return true;                                                                                        // something moved
            }
            
        }
    }
}
