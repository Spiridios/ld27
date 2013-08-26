using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spiridios.SpiridiEngine;

namespace Spiridios.SnapEncounters.Encounters
{
    public class ZombieEncounter : Encounter
    {
        private Encounter expiredEncounter;
        private Encounter successFightEncounter;
        private Encounter successFleeEncounter;
        private SEActor enemy = new SEActor("Zombie.xml");

        public ZombieEncounter()
            : base(null, new TextureImage("Flee"))
        {
        }

        public override void Activate()
        {
            base.Activate();
            this.Actor = enemy;
            this.leftImage = ((SnapEncounters)SpiridiGame.Instance).Adventurer.AttackImage;

            if (((SnapEncounters)game).Adventurer.Weapon == Adventurer.WeaponType.Mele)
            {
                this.expiredEncounter = new Encounter(
                      "\nThe zombie rushes you! You try to ward"
                    + "\nit off with your sword, but it just runs"
                    + "\nright through it, losing a limb in the"
                    + "\nprocess. You feel the bite and the last"
                    + "\nthing you remember is a sudden craving"
                    + "\nfor brains"
                    );
                this.expiredEncounter.Actor = enemy;

                this.successFightEncounter = new Encounter(
                      "\nYou charge the zombie hoping to"
                    + "\nconfuse it. The zombie just stands"
                    + "\nthere like a brainless zombie. You"
                    + "\nchop off a limb, then another, then"
                    + "\nfinally finish with the head. The"
                    + "\ndecapitated zombie head continues"
                    + "\nto stare at you..."
                    );
                this.successFightEncounter.Actor = enemy;
            }
            else
            {
                this.expiredEncounter = new Encounter(
                      "\nThe zombie rushes you! You try to shoot"
                    + "\nit with your bow, but your aim isn't"
                    + "\ntrue and you barely hit it in the arm."
                    + "\nYou feel the bite and the last thing"
                    + "\nyou remember is a sudden craving"
                    + "\nfor brains"
                    );
                this.expiredEncounter.Actor = enemy;

                this.successFightEncounter = new Encounter(
                      "\nYou load up your bow with not one,"
                    + "\nnot two, but three arrows, and let"
                    + "\nfly. Two of the arrows just flop"
                    + "\nand go nowhere. What were you thinking?"
                    + "\nThe third arrow ricochets off a rock"
                    + "\nand lands true."
                    );
                this.successFightEncounter.Actor = enemy;
            }

            String successFlee =
                  "\nYou run. Luckily the zombie is old"
                + "\nschool and just shuffles along,"
                + "\nbarely able to keep up with a snail.";

            this.successFleeEncounter = new Encounter(successFlee);
            this.successFleeEncounter.Actor = enemy;

        }

        public override void Update(TimeSpan elapsedTime)
        {
            base.Update(elapsedTime);
            switch (this.choice)
            {
                case (Choice.Expired):
                    NextEncounter = expiredEncounter;
                    ((SnapEncounters)game).Adventurer.Actor.Kill();
                    break;
                case (Choice.LeftChoice):
                    ((SnapEncounters)game).Adventurer.GainXP();
                    InsertEncounter(successFightEncounter);
                    enemy.Kill();
                    break;
                case (Choice.RightChoice):
                    ((SnapEncounters)game).Adventurer.GainXP(-1);
                    InsertEncounter(successFleeEncounter);
                    break;
            }
        }

    }
}
