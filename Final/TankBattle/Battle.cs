using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace TankBattle
{
    public class Battle
    {
        private int currentRound;           //
        private int startingPlayer;
        private int currentPlayer;
        private int windSpeed;
        private int maxRounds;
        private int[] playerPosistions;

        private int windSpeedMIN = -100;
        private int windSpeedMAX = 100; 
        private Level gameLevel;

        private GameplayTank[] tankArray;
        private WeaponEffect[] weaponList;
        private Player[] playerArray;

        /// <summary>
        /// Battle Class's Constructor. Initialises multiple variables and arrays used in Battle.cs 
        /// and passes in the number of rounds and p;ayers in the game.  
        /// </summary>
        /// <param name="numPlayers"></param>
        /// <param name="numRounds"></param>
        public Battle(int numPlayers, int numRounds)
        {
            playerArray = new Player[numPlayers];                           // Create Player Array
            weaponList = new WeaponEffect[100];                             // Weapons List 
            maxRounds = numRounds;                                          // initialise Number of Rounds                                                              
            int roundsPlayed = CurrentRound();                              // Calculate No. of rounds played
        }

        /// <summary>
        /// Gets the number of players in the game.
        /// </summary>
        /// <returns> Integer between 2 & 8.</returns>
        public int NumPlayers()
        {
            return playerArray.Length;                                      // Number of players taken from the playerArray Length
        }

        /// <summary>
        /// Gets the Current Round
        /// </summary>
        /// <returns>Integer between 1 & maxRounds</returns>
        public int CurrentRound()
        {
            return currentRound;                                            // Current Round initialised in BeginRound() and Iterated in CommenceRound()  
        }

        /// <summary>
        /// Gets The maximum number of rounds to be played
        /// </summary>
        /// <returns>integer between 1 & 8</returns>
        public int GetMaxRounds()
        {
            return maxRounds;                                               // Passed in from Battle's Constructor
        }

        /// <summary>
        /// Assigns player to the corresponding Player 
        /// in playerArray using PlayerNum
        /// </summary>
        /// <param name="playerNum"></param>
        /// <param name="player"></param>
        public void SetPlayer(int playerNum, Player player)
        {
            playerArray[playerNum - 1] = player;                            // Player Numbers (playerNum) indexed from 1, playerArray indexed from zero 
        }

        /// <summary>
        /// Gets the Player associated with
        /// the PlayerNum
        /// </summary>
        /// <param name="playerNum"></param>
        /// <returns> Player</returns>
        public Player Player(int playerNum)
        {
            return playerArray[playerNum - 1];                              // Player Numbers (playerNum) indexed from 1, playerArray indexed from zero
        }

        /// <summary>
        /// Takes a Player's number between 1 the
        /// number of players and returns the 
        /// associated GamePlayTank
        /// </summary>
        /// <param name="playerNum"></param>
        /// <returns>GameplayTank</returns>
        public GameplayTank PlayerTank(int playerNum)
        {
            return tankArray[playerNum - 1];                                // Player Numbers (playerNum) indexed from 1, tankArray indexed from zero
        }

        /// <summary>
        /// Tankes a Player's number and returns the 
        /// associated Color 
        /// </summary>
        /// <param name="playerNum"></param>
        /// <returns>Orange</returns>
        public static Color PlayerColour(int playerNum)
        {
            if (playerNum == 1) {return Color.Blue;}                        // Associate Colours with Player numbers               
            else if (playerNum == 2) {return Color.Orange;}
            else if (playerNum == 3) {return Color.Green;}
            else if (playerNum == 4) {return Color.Red;}
            else if (playerNum == 5) {return Color.Yellow;}
            else if (playerNum == 6) {return Color.Violet;}
            else if (playerNum == 7) {return Color.Black;}
            else if (playerNum == 8) {return Color.White;}
            else { { return Color.BlanchedAlmond; } }
        }

        /// <summary>
        /// Calculates each Tanks X Positions
        /// and returns an a numPlayers sized 
        /// array of 
        /// </summary>
        /// <param name="numPlayers"></param>
        /// <returns>numPlayers sized array of ints between 0 and 
        /// Level.WIDTH - TankType.Width</returns>
        public static int[] CalculatePlayerPositions(int numPlayers)
        {

            int[] playPosArray = new int[numPlayers];
            int playerDist;                                                 // Distance between players

            playerDist = Level.WIDTH / numPlayers;                          // Calculate Player Spaceing

            if (playerDist > Level.WIDTH / 4) {                             // Maintain reasonable spacing for games with less than 4 players 

                playerDist = Level.WIDTH / 4;                               

            }
            
            int newDist = TankType.WIDTH;                                   // Used to space out players properly without Destroying PlayerDist

            for (int i = 0; i < numPlayers; i++)                                  
            {
                    playPosArray[i] = (newDist - TankType.WIDTH );          // Place Player on next Left Position    

                    newDist = newDist + playerDist;                         // Update Player Distances 
                
            }
            return playPosArray;
        }

        /// <summary>
        /// Takes in the x positions of the
        /// GameplayTanks and rearranges into
        /// a random order
        /// </summary>
        /// <param name="array"></param>
        public static void RandomReorder(int[] array)
        {
            Random random = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                int j = random.Next(i, array.Length);                       // Get a random position in array
                int temp = array[i];                                        // preserve the i position in the array 
                array[i] = array[j];                                        // swap i & j position in array
                array[j] = temp;                                            //
            }      
        }

        /// <summary>
        /// Initialises the currentRound
        /// amd startingPlayer and calls
        /// CommenceRound()
        /// </summary>
        public void BeginGame()
        {
            currentRound = 1;
            startingPlayer = 0;
            CommenceRound();
        }

        /// <summary>
        /// Begins the round initialising current player,
        /// creating a new Level, calculating the GamePlayTank
        /// Array's x & Y positions, Creates the GameplayTanks,
        /// sets the windSpeed to a random number between -100 & 
        /// 100 and creates and shows the GamplayForm
        /// </summary>
        public void CommenceRound()
        {
            currentPlayer = startingPlayer;                                                                                              
            gameLevel = new Level();                                                                                                    // Create a new Level    
            playerPosistions = CalculatePlayerPositions(playerArray.Length);                                                            // Calculate the Gameplaytank X positions
            
            for (int i = 0; i < playerArray.Length; i++) { playerArray[i].BeginRound(); }                                               // Call BeginRound() on every Player

            RandomReorder(playerPosistions);                                                                                            // Reorder GameplayTank array's X positions

            tankArray = new GameplayTank[playerArray.Length];                                                                           // initialise the GameplayTank array 

            for (int i = 0; i < tankArray.Length; i++)                                                                                  // Loop through the tankArray assigning, it's
            {                                                                                                                           // associated Player, X Position, Y Position,
               tankArray[i]= new GameplayTank(playerArray[i], playerPosistions[i], gameLevel.TankPlace(playerPosistions[i]),this);      // and associated Battle
            }                                                                                                                           //

            Random rndSpeed = new Random();                                                                                             // Set windSpeed to a Random number between
            windSpeed = rndSpeed.Next(windSpeedMIN, windSpeedMAX);                                                                                       // -100 & 100 

            GameplayForm CurrentForm = new GameplayForm(this);                                                                          // Create the GameplayForm and Show() it
            CurrentForm.Show();                                                                                                         //
        }

        /// <summary>
        /// Gets the Level 
        /// associated with Battle
        /// </summary>
        /// <returns>Level</returns>
        public Level GetMap()
        {
            return gameLevel;
        }

        /// <summary>
        /// Draws the GamePlayTanks in tankArray 
        /// that are still alive.
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="displaySize"></param>
        public void DisplayPlayerTanks(Graphics graphics, Size displaySize)
        {
            foreach(GameplayTank tank in tankArray)                             // Check if the GameplayTank is alive
            {                                                                   // and if so, draw them
                if (tank.IsAlive()) { tank.Render(graphics, displaySize); }     //
            }                                                                   //    
        }

        /// <summary>
        /// Uses the currentPlayer variable to 
        /// retrieve the current GameplayTank 
        /// form the tankArray.
        /// </summary>
        /// <returns>GamePlayTank</returns>
        public GameplayTank GetCurrentGameplayTank()
        {
            return tankArray[currentPlayer];
        }

        /// <summary>
        /// Finds the first unassigned variable 
        /// in weaponList and assigns weaponEffect
        /// to that point in the array and assigns 
        /// this battle to weaponEffect
        /// </summary>
        /// <param name="weaponEffect"></param>
        public void AddWeaponEffect(WeaponEffect weaponEffect)
        {
            for(int i = 0; i < weaponList.Length; i++)
            {
                if (weaponList[i] == null)                                      // Has this place in the Array been assigned to?
                {
                    weaponList[i] = weaponEffect;                           
                    weaponList[i].SetCurrentGame(this);                         // set the weaponEffect's game to this game
                    return;                                                     
                }
            }
        }

        /// <summary>
        /// Calls WeaponEffect's Process()
        /// WeaponEffects on the WeaponList
        /// that are assigned to.
        /// </summary>
        /// <returns>true, or false</returns>
        public bool ProcessEffects()
        {
            bool effect = false;
            for (int i = 0; i < weaponList.Length; i++)
            {    
                if (weaponList[i] != null)                                      // Has this WeaponEffect been assigned to?
                {
                    weaponList[i].Process();                                    // WeaponEffect.Process called
                    effect = true;                                              // WeaponEffects were Processed
                }
            }
            return effect;                                                      // No WeaponEffects were Processed 
        }

        /// <summary>
        /// calls Render() on every
        /// WeaponEffect in weaponList 
        /// that is assigned to
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="displaySize"></param>
        public void RenderEffects(Graphics graphics, Size displaySize)
        {
            for (int i = 0; i < weaponList.Length; i++)
            {
                if (weaponList[i] != null)                                      // Has this WeaponEffect been assigned to?
                {
                    weaponList[i].Render(graphics, displaySize);                // Render() called
                }
            }
        }

        /// <summary>
        /// Finds weaponEffect in weaponList and sets it to
        /// null
        /// </summary>
        /// <param name="weaponEffect"></param>
        public void RemoveWeaponEffect(WeaponEffect weaponEffect)
        {
            for(int i = 0; i < weaponList.Length; i++)
                if(weaponEffect == weaponList[i])
                {
                    weaponList[i] = null;
                    return;
                }
        }

        /// <summary>
        /// Checks if a Missile hit something
        /// and returns true if it did.
        /// </summary>
        /// <param name="projectileX"></param>
        /// <param name="projectileY"></param>
        /// <returns> true or false</returns>
        public bool CheckHitTank(float projectileX, float projectileY)
        {
            if (projectileX >= Level.WIDTH || projectileX < 0 || projectileY >= Level.HEIGHT || projectileY < 0) { return false; }                  // Check if out of Level's boundaries
            else if (gameLevel.TerrainAt(Convert.ToInt32(projectileX), Convert.ToInt32(projectileY))){ return true; }                               // Check if Terrain was Hit

            foreach (GameplayTank tank in tankArray)                                                                                                // Check if any GameplayTank in tankArray was hit  
            {                                                                                                                                
                if (tank.IsAlive() && tank != tankArray[currentPlayer])                                                                             // Protect against Tank check being triggered by the firing  
                {                                                                                                                                   // GamePlayTank or a dead (null) tank.    

                    if ((projectileX >= tank.X() && projectileX < tank.X() + TankType.WIDTH) && (projectileY >= tank.GetYPos() &&                   // Return true if missile is within the tested TankTypes 
                        projectileY < tank.GetYPos() + TankType.HEIGHT)) { return true; }                                                           // boundaries
                }                                                                                                                               
            }                                                                                                                               

            return false;                                                                                     
        }

        /// <summary>
        /// Applies varying damage to a Tank depending on where the 
        /// tank falls within the explosion radius (radius).
        /// </summary>
        /// <param name="damageX"></param>
        /// <param name="damageY"></param>
        /// <param name="explosionDamage"></param>
        /// <param name="radius"></param>
        public void DamagePlayer(float damageX, float damageY, float explosionDamage, float radius)
        {
            foreach(GameplayTank tank in tankArray)
            {
                if (tank.IsAlive())
                {
                    float tankX = tank.X() + (TankType.WIDTH/2);                                                                                    // Find the middle of the Tank
                    float tankY = tank.GetYPos() + (TankType.HEIGHT/2);                                                                             //    

                    float tankDistance = Convert.ToSingle(Math.Sqrt(Math.Pow(damageX - tankX, 2) + Math.Pow(damageY - tankY, 2)));                  // Distance between Tank and explosion
                    float damage;       

                    if (tankDistance > radius) { damage = 0; }                                                                                      // Tank takes zero damage id outside of the explosion radius
                    else if(tankDistance > radius/2 && tankDistance < radius) { damage = (explosionDamage * (radius - tankDistance)) / radius; }    // Tank takes damage proportinal to the distance from the explosion
                    else { damage = explosionDamage; }                                                                                              // if direct hit tank takes explosionDamage

                    tank.DamagePlayer(Convert.ToInt32(damage));                                                                                     // call DamagePlayer() passing in the calculated damage
                }
            }
        }

        /// <summary>
        /// CalculateGravity() is called after WeaponEffect
        /// animations finished to make sure there are no 
        /// floating tiles or tanks
        /// </summary>
        /// <returns>true or false</returns>
        public bool CalculateGravity()
        {
            bool moved = false;

            if (gameLevel.CalculateGravity() == true) { moved = true; }                 // Something was moved if floating tiles were found

            for(int i = 0; i < tankArray.Length; i++)
            {
                if(tankArray[i].CalculateGravity() == true) { moved = true; }           // Something moved if floating tanks were found    
            }
            return moved;
        }

        /// <summary>
        /// FinaliseTurn() checks how many GameplayTank's
        /// are still aive, determines whether the round is
        /// over and if not iterates the currentPlayer to the 
        /// next alive Player. Returns true if the round is
        /// not over.
        /// </summary>
        /// <returns>true or false</returns>
        public bool FinaliseTurn()
        {
            int tankCount = 0;


            foreach (GameplayTank tank in tankArray)
            {
                if (tank.IsAlive() == true) { tankCount++; }                            // Check how many tanks are still alive 
            }

            if (tankCount >= 2)
            {

                currentPlayer++;                                                        // Update current Player
                if (currentPlayer >= tankArray.Length) { currentPlayer = 0; }           // protect aginast indexing out of tankArray

                while (tankArray[currentPlayer].IsAlive() == false)                     // if Tank is dead, try the nect GameplayTank
                {                                                                       //
                    currentPlayer++;
                    if (currentPlayer >= tankArray.Length) { currentPlayer = 0; }       // Protect against indexing out of tankArray
                }
                    
                                                                                                     
                Random rnd = new Random();                                              // update windSpeed
                windSpeed += rnd.Next(-10, 10);                                         //
                if(windSpeed > windSpeedMAX) { windSpeed = windSpeedMAX; }              // Keep Windspeed Within desired values;
                else if(windSpeed < windSpeedMIN) { windSpeed = windSpeedMIN; }         //
                return false;

                                    
            }
            if (tankCount == 0 || tankCount == 1) { ScoreWinner(); }                    // Show winner if less the 2 alive
            return true;
        }

        /// <summary>
        /// Finds the GamePlayTank that is 
        /// still alive and at the end of 
        /// the Round and call WonRound()
        /// on that GameplayTank
        /// </summary>
        public void ScoreWinner()
        {
            for(int i = 0; i < tankArray.Length; i++)
            {
                if (tankArray[i].IsAlive())
                {
                    playerArray[i].WonRound(); 
                }
            }
        }

        /// <summary>
        /// determines whether all rounds have been 
        /// played and if not iterates startingPlayer
        /// calls CommenceRound(). If all rounds are played out
        /// it shows the introForm
        /// </summary>
        public void NextRound()
        {
            currentRound++;
            if(currentRound <= maxRounds)
            {
                startingPlayer++;
                if(startingPlayer > playerArray.Length) { startingPlayer = 0; }         // Protect against indexxing out of playerArray
                CommenceRound();
            }
            else
            {
                IntroForm newIntro = new IntroForm();
                newIntro.Show();
            }
        }
        
        /// <summary>
        /// Gets the Windspeed.
        /// </summary>
        /// <returns>int between -100 & 100</returns>
        public int WindSpeed()
        {
            return windSpeed;
        }
    }
}
