using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankBattle
{
    public partial class IntroForm : Form
    {
        /// <summary>
        /// Constructor for the Introform. Inherrits from form.
        /// Calls initializeComponent().
        /// </summary>
        public IntroForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Method for newGameButton Press. initialises
        /// Battle and Players, and calls BeginGame() to 
        /// start the game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newGameButton_Click(object sender, EventArgs e)
        {
            Battle game = new Battle(3, 2);
            Player player1 = new Human("Player 1", TankType.CreateTank(1), Battle.PlayerColour(1));
            Player player2 = new Human("Player 2", TankType.CreateTank(1), Battle.PlayerColour(2));
            Player player3 = new Human("Player 3", TankType.CreateTank(1), Battle.PlayerColour(3));
            game.SetPlayer(1, player1);
            game.SetPlayer(2, player2);
            game.SetPlayer(3, player3);
            game.BeginGame();

        }
    }
}
