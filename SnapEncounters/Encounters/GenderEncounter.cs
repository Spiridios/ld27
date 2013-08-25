using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spiridios.SpiridiEngine;

namespace Spiridios.SnapEncounters.Encounters
{
    public class GenderEncounter : Encounter
    {
        private Encounter expiredEncounter;
        public GenderEncounter()
            : base(new TextureImage("Male"), new TextureImage("Female"))
        {
            this.expiredEncounter = new Encounter("")
            .AddLine("While we appreciate all the transgendered")
            .AddLine("adventurers out there, we've decided to")
            .AddLine("join ranks with all the jerk developers")
            .AddLine("out there and lazily only acknowledge")
            .AddLine("two genders. Since you do not fit our narrow")
            .AddLine("world view, you have failed your Snap")
            .AddLine("Encounters adventure.")
            .AddLine("")
            .AddLine("(restart)");
        }

        public override void Update(TimeSpan elapsedTime)
        {
            base.Update(elapsedTime);
            if (this.choice == Choice.Expired)
            {
                NextEncounter = expiredEncounter;
            }
        }
    }
}
