using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TankBattle
{
    public class Human : Player
    {
        /// <summary>
        /// Constructot for Human Players. passes in the associated String (name), TankType, Color which it inherrits 
        /// from the Player Class. All functionality handled by Player Class. This does nothing.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="tank"></param>
        /// <param name="colour"></param>
        public Human(string name, TankType tank, Color colour) : base(name, tank, colour) { }

        /// <summary>
        /// Method Called each Round but is not
        /// required to do anything here.
        /// </summary>
        public override void BeginRound() { }
        
        /// <summary>
        /// Called when it is this Player's turn. Calls EnableTankControls() on the GameplayForm 
        /// so the Human can carry out their turn.
        /// </summary>
        /// <param name="gameplayForm"></param>
        /// <param name="currentGame"></param>
        public override void NewTurn(GameplayForm gameplayForm, Battle currentGame) { gameplayForm.EnableTankControls(); }

        /// <summary>
        /// Method called everytime player shoots but doesn't 
        /// need to anything here.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public override void ReportHit(float x, float y) { }
    }
}
