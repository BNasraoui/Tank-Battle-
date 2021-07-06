using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    public class ComputerPlayer : Player
    {
        public ComputerPlayer(string name, TankType tank, Color colour) : base(name, tank, colour)
        {
            throw new NotImplementedException();
        }

        public override void BeginRound()
        {
            throw new NotImplementedException();
        }

        public override void NewTurn(GameplayForm gameplayForm, Battle currentGame)
        {
            throw new NotImplementedException();
        }

        public override void ReportHit(float x, float y)
        {
            throw new NotImplementedException();
        }
    }
}
