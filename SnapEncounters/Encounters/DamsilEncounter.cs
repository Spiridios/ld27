using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spiridios.SpiridiEngine;

namespace Spiridios.SnapEncounters.Encounters
{
    public class DamsilEncounter : Encounter
    {
        private Encounter expiredEncounter;
        private Encounter successLoveEncounter;
        private Encounter successFightEncounter;
        private SEActor enemy = new SEActor("Damsil.xml");

        public DamsilEncounter()
            : base(new TextureImage("Heart"), null)
        {
        }

        public override void Activate()
        {
            base.Activate();
            this.Actor = enemy;
            this.rightImage = ((SnapEncounters)SpiridiGame.Instance).Adventurer.AttackImage;

            if (((SnapEncounters)game).Adventurer.Gender == Adventurer.GenderType.Female)
            {
                this.expiredEncounter = new Encounter(
                      "\nThe damsil is outraged! She accuses you"
                    + "\nof stealing her hero! She pulls out the"
                    + "\nbiggest war hammer you've ever seen and"
                    + "\nprodcedes to smash you into an adventurer"
                    + "\npancake. With extra syrup."
                    );
                this.expiredEncounter.Actor = enemy;

                String successLove =
                      "\nThe damsil looks at you quizically"
                    + "\nand says \"you're a very nice hero"
                    + "\nand all, but I don't swing that way.\""
                    + "\nShe looks uncomfortible until she"
                    + "\nleaves. Runs away actually. Oh well,"
                    + "\nbetter to have loved and lost.";
                this.successLoveEncounter = new Encounter(successLove);
                this.successLoveEncounter.Actor = null;
            }
            else
            {
                this.expiredEncounter = new Encounter(
                      "\nThe damsil is outraged! She accuses you"
                    + "\nof staring at her breasts and treating"
                    + "\nher like a sex object. She pulls out the"
                    + "\ntiniest war hammer you've ever seen and"
                    + "\nprodcedes to smash you into adventurer"
                    + "\ndust."
                    );
                this.expiredEncounter.Actor = enemy;

                String successLove =
                      "\nThe damsil looks at you quizically"
                    + "\nand says \"I suppose you expect me"
                    + "\nto just swoon and run off with you?\""
                    + "\nShe flips you off, hops on a horse"
                    + "\nand rides off. Oh well, better to"
                    + "\nhave loved and lost.";
                this.successLoveEncounter = new Encounter(successLove);
                this.successLoveEncounter.Actor = null;
            }

            String successFight =
                  "\nThe damsil screams! You're supposed to"
                + "\nsave her, not kill her! You become just"
                + "\na little more evil.";

            this.successFightEncounter = new Encounter(successFight);
            this.successFightEncounter.Actor = enemy;

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
                    InsertEncounter(successLoveEncounter);
                    break;
                case (Choice.RightChoice):
                    ((SnapEncounters)game).Adventurer.GainXP(-1);
                    enemy.Kill();
                    InsertEncounter(successFightEncounter);
                    break;
            }
        }

    }
}
