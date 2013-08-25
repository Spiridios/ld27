﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spiridios.SpiridiEngine;

namespace Spiridios.SnapEncounters.Encounters
{
    public class WeaponEncounter : Encounter
    {
        private Encounter expiredEncounter;
        private Encounter successRangedEncounter;
        private Encounter successMeleEncounter;

        public WeaponEncounter()
            : base(new TextureImage("Mele"), new TextureImage("Ranged"))
        {
            String preamble = "\nA ninja jumps out of exactly where ninjas\n"
                + "jump from - nowhere, and demands your wallet.\n"
                + "Being a good adventurer you decide to fight.";

            String expired = "Your {0} skills were no match for his\n"
                + "Jeet Kune Do. If only you had a weapon in your\n"
                + "skllled hands, you would have easiliy slain the ninja";

            this.expiredEncounter = new Encounter(preamble);


            if(((SnapEncounters)game).Adventurer.Gender == Adventurer.GenderType.Female)
            {
                this.expiredEncounter
                    .AddLine(String.Format(expired, "Muay Thai"));
            }
            else
            {
                this.expiredEncounter
                    .AddLine(String.Format(expired, "Capoeira"));
            }


            String successMele =
                  "The ninja rushes you with limbs ablur, but\n"
                + "you stand your ground taking one swipe with\n"
                + "your trusty sword, easily slaying the ninja\n"
                + "Aren't you glad you had a weapon handy?";
            this.successMeleEncounter = new Encounter(preamble)
            .AddLine(successMele);

            String successRange =
                  "The ninja rushes you, but you already have\n"
                + "your bow drawn. Before the ninja knows what\n"
                + "hit him, he's slain.\n"
                + "Aren't you glad you had a weapon handy?";
            this.successRangedEncounter = new Encounter(preamble)
            .AddLine(successRange);
        }

        public override void Update(TimeSpan elapsedTime)
        {
            base.Update(elapsedTime);
            switch (this.choice)
            {
                case (Choice.Expired):
                    NextEncounter = expiredEncounter;
                    break;
                case (Choice.LeftChoice):
                    ((SnapEncounters)game).Adventurer.Weapon = Adventurer.WeaponType.Mele;
                    NextEncounter = successMeleEncounter;
                    break;
                case (Choice.RightChoice):
                    ((SnapEncounters)game).Adventurer.Weapon = Adventurer.WeaponType.Ranged;
                    NextEncounter = successRangedEncounter;
                    break;
            }
        }

    }
}
