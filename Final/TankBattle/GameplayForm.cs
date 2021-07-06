using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankBattle
{
    public partial class GameplayForm : Form
    {
        private Color landscapeColour;
        private Random rng = new Random();
        private Image backgroundImage = null;
        private int levelWidth = 160;
        private int levelHeight = 120;
        private Battle currentGame;

        private BufferedGraphics backgroundGraphics;
        private BufferedGraphics gameplayGraphics;
        
        /// <summary>
        /// Constructor For GamePlaForm. Initialises
        /// Formstyles, sets baackgroundImage and 
        /// landscapeColour, and sets timer1 and 
        /// AngleSelector Variables. Passes in the 
        /// Battle associated with the GameplayForm
        /// </summary>
        /// <param name="game"></param>
        public GameplayForm(Battle game)
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.UserPaint, true);

            string[] imageFilenames = { "Images\\background1.jpg",
                                        "Images\\background2.jpg",
                                        "Images\\background3.jpg",
                                        "Images\\background4.jpg"};

            Color[] landscapeColours = {    Color.FromArgb(255, 0, 0, 0),
                                            Color.FromArgb(255, 73, 58, 47),
                                            Color.FromArgb(255, 148, 116, 93),
                                            Color.FromArgb(255, 133, 119, 109) };

            currentGame = game;                                                     // Battle passed to Private Variable

            int rndBackground = rng.Next(0, 3);                                     // Pick a random backgroundImage and landscapeColour
            backgroundImage = Image.FromFile(imageFilenames[rndBackground]);        //
            landscapeColour = landscapeColours[rndBackground];                      //

            InitializeComponent();

            AngleSelector.Minimum = -90;                                            // Set AngleSelector Variables
            AngleSelector.Maximum = 90;                                             //
            AngleSelector.Increment = 5;                                            //

            timer1.Interval = 20;                                                   // timer1 set to 20 millisecond intervals

            backgroundGraphics = InitRenderBuffer();                                // Set up screen to be Drawn
            gameplayGraphics = InitRenderBuffer();                                  //

            DrawBackground();                                                       // Draw Gameplay screen
            DrawGameplay();

            NewTurn();                                                              // Initialise turn
        }

        // From https://stackoverflow.com/questions/13999781/tearing-in-my-animation-on-winforms-c-sharp
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }

        /// <summary>
        /// Enables TankControls for Human
        /// Players
        /// </summary>
        public void EnableTankControls()
        {
            controlPanel.Enabled = true;
        }

        /// <summary>
        /// Updates AngleSelectors Angle
        /// value to the value passed.
        /// </summary>
        /// <param name="angle"></param>
        public void AimTurret(float angle)
        {
            AngleSelector.Value = Convert.ToDecimal(angle);
        }

        /// <summary>
        /// Updates PowerSelectors Turret
        /// Power Value to the value passed.
        /// </summary>
        /// <param name="power"></param>
        public void SetTurretPower(int power)
        {
            PowerSelector.Value = power;
        }

        /// <summary>
        /// Updates the selected Weapon to the 
        /// Weapon Passed
        /// </summary>
        /// <param name="weapon"></param>
        public void SetWeaponIndex(int weapon)
        {
            WeaponSelector.SelectedItem = weapon;
        }

        /// <summary>
        /// Enables the currentGameplayTank
        /// to fire.
        /// </summary>
        public void Shoot()
        {
            GameplayTank tank = currentGame.GetCurrentGameplayTank();                           // Get the gamePlayTank that is currently completing their turn
            tank.Shoot();                                                                       // GamePlayTank's Shoot function
            controlPanel.Enabled = false;                                                       // let Human control the ControlPanel
            timer1.Enabled = true;                                                              // set timings for animations 
        }

        /// <summary>
        /// Draw the Gameplay screen
        /// </summary>
        private void DrawGameplay()
        {
            backgroundGraphics.Render(gameplayGraphics.Graphics);                               // Render Background Graphics buffer to Gameplay buffer
            currentGame.DisplayPlayerTanks(gameplayGraphics.Graphics, displayPanel.Size);       // Display Tanks
            currentGame.RenderEffects(gameplayGraphics.Graphics, displayPanel.Size);            // Display game effects
        }

        /// <summary>
        /// NuewTurn() is called to update form 
        /// elements to reflect who the current 
        /// Player is
        /// </summary>
        private void NewTurn()
        {
            GameplayTank gameTank = currentGame.GetCurrentGameplayTank();                                                               // get an instance of the Current GameplayTank and Player
            Player gamePlayer = gameTank.Player();                                                                                      //

            this.Text = string.Format("Tank Battle - Round {0} of {1}", currentGame.CurrentRound(), currentGame.GetMaxRounds());        // Update Form Caption

            controlPanel.BackColor = gamePlayer.GetTankColour();                                                                        // Update controlPanel colour to represent Player
            CurrentName.Text = gamePlayer.GetName();                                                                                    // Update Displayed name
            
            AimTurret(gameTank.GetAngle());                                                                                             // Get the angle of the turret
            SetTurretPower(gameTank.GetTankPower());                                                                                    // Set the turret power

            if (currentGame.WindSpeed() > 0) { WindSpeedDirection.Text = (currentGame.WindSpeed() + " W"); }                            // Update Wind Speed and Direction 
            else if(currentGame.WindSpeed() < 0) { WindSpeedDirection.Text = (currentGame.WindSpeed() + " E"); }                        //

            WeaponSelector.Items.Clear();                                                                                               // Clear Weapons List in ComboBox


            TankType currentTankType = gameTank.CreateTank();                                                                           // Get a TankType representitive of the GameplayTank
            string[] currentWeaponArray = currentTankType.WeaponList();                                                                 // Update weaponArray
            foreach(string weapon in currentWeaponArray) { WeaponSelector.Items.Add(weapon); }                                          // Add weapons from WeaponArray to the WeaponSelector comboBox

            SetWeaponIndex(gameTank.GetCurrentWeapon());                                                                                // Update Current Weapon
            gamePlayer.NewTurn(this, currentGame);                                                                                      // Calls Battles's Newturn() method
        }

        private void DrawBackground()
        {
            Graphics graphics = backgroundGraphics.Graphics;
            Image background = backgroundImage;
            graphics.DrawImage(backgroundImage, new Rectangle(0, 0, displayPanel.Width, displayPanel.Height));

            Level battlefield = currentGame.GetMap();
            Brush brush = new SolidBrush(landscapeColour);

            for (int y = 0; y < Level.HEIGHT; y++)
            {
                for (int x = 0; x < Level.WIDTH; x++)
                {
                    if (battlefield.TerrainAt(x, y))
                    {
                        int drawX1 = displayPanel.Width * x / levelWidth;
                        int drawY1 = displayPanel.Height * y / levelHeight;
                        int drawX2 = displayPanel.Width * (x + 1) / levelWidth;
                        int drawY2 = displayPanel.Height * (y + 1) / levelHeight;
                        graphics.FillRectangle(brush, drawX1, drawY1, drawX2 - drawX1, drawY2 - drawY1);
                    }
                }
            }
        }

        public BufferedGraphics InitRenderBuffer()
        {
            BufferedGraphicsContext context = BufferedGraphicsManager.Current;
            Graphics graphics = displayPanel.CreateGraphics();
            Rectangle dimensions = new Rectangle(0, 0, displayPanel.Width, displayPanel.Height);
            BufferedGraphics bufferedGraphics = context.Allocate(graphics, dimensions);
            return bufferedGraphics;
        }

        private void displayPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = displayPanel.CreateGraphics();
            gameplayGraphics.Render(graphics);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (currentGame.ProcessEffects() == false)                          // Process Missiles and Shrapnel
            {
                DrawBackground();                                               // Redraw everything after possible movement
                DrawGameplay();                                                 //
                displayPanel.Invalidate();

                if (currentGame.CalculateGravity() == true) { return; }         // redraw if nothing moved

                else
                {
                    timer1.Enabled = false;                                     
                    if (currentGame.FinaliseTurn() == false) { NewTurn(); }     // NewTurn() called to set up for next Player's turn 
                    else
                    {
                        Dispose();                                              // Set up for next Round
                        currentGame.NextRound();                                //
                    }
                    return;
                }
            }

            else                                                                // Attack animations still completing
            {
                DrawGameplay();                                                 
                displayPanel.Invalidate();
                return;
            }
        }   

        private void controlPanel_Paint(object sender, PaintEventArgs e){ }
        private void label1_Click(object sender, EventArgs e) { }
        private void windLabel_Click(object sender, EventArgs e) { }
        private void label1_Click_1(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void WindSpeedDirection_Click(object sender, EventArgs e) { }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            GameplayTank currentTank = currentGame.GetCurrentGameplayTank();            // Get the GameplayTank
            currentTank.AimTurret(Convert.ToSingle(AngleSelector.Value));               // Aim the GamePlay's Turret using the AngleSelector Value
            DrawGameplay();                                                             // Update Screen
            displayPanel.Invalidate();                                                  //
        }

        private void ComboBox1_ValueChanged(object sender, EventArgs e)
        {
            GameplayTank currentTank = currentGame.GetCurrentGameplayTank();            // Get the GameplayTank
            currentTank.SetWeaponIndex(Convert.ToInt32(WeaponSelector.SelectedItem));   // Set the Weapon index using the Weapon Selector's current item
            DrawGameplay();                                                             // Update Screen
            displayPanel.Invalidate();                                                  //
        }

        private void PowerSelector_Scroll(object sender, EventArgs e)
        {
            GameplayTank currentTank = currentGame.GetCurrentGameplayTank();            // Get the GameplayTank
            currentTank.SetTurretPower(PowerSelector.Value);                            // Set the Turret Power of the GameplayTank using the Power Selector Calue
            Power.Text = string.Format("{0}", PowerSelector.Value);
            DrawGameplay();                                                             // Update Screen
            displayPanel.Invalidate();
        }

        private void WeaponSelector_SelectedIndexChanged(object sender, EventArgs e) { }

        private void FireButton_Click(object sender, EventArgs e)
        {
            Shoot();
        }
    }
}
