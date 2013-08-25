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
        private Encounter successEncounter;
        private string successExposition;

        private const string MALE_REPLACE = "Sir";
        private const string FEMALE_REPLACE = "Madam";

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

            this.successEncounter = new Encounter("");
            this.successExposition = "Whew, glad we got that cleared up {0}\n"
                + "we've kinda been wondering...\n"
                + "\n"
                + "Your next snap decision has\nto do with how you fight.";
        }

        public override void Update(TimeSpan elapsedTime)
        {
            base.Update(elapsedTime);
            switch (this.choice)
            {
                case (Choice.Expired):
                    NextEncounter = expiredEncounter;
                    break;
                case(Choice.LeftChoice):
                    ((SnapEncounters)game).Adventurer.Gender = Adventurer.GenderType.Male;
                    this.successEncounter.NextEncounter = this.NextEncounter;
                    NextEncounter = successEncounter.AddLine(String.Format(this.successExposition, MALE_REPLACE));
                    break;
                case(Choice.RightChoice):
                    ((SnapEncounters)game).Adventurer.Gender = Adventurer.GenderType.Female;
                    this.successEncounter.NextEncounter = this.NextEncounter;
                    NextEncounter = successEncounter.AddLine(String.Format(this.successExposition, FEMALE_REPLACE));
                    break;
            }
        }
    }
}
