using System;
using Microsoft.Xna.Framework.Graphics;
using Spiridios.SpiridiEngine;

namespace Spiridios.SnapEncounters
{
    public class SEActor : Actor
    {
        private double dyingElapsedTime = 0;
        private const double DyingTime = 1;

        public SEActor(String animationName)
            : base(new AnimatedImage(animationName))
        {
            this.DrawDead = true;
        }

        public void Kill()
        {
            dyingElapsedTime = 0;
            this.lifeStage = LifeStage.DYING;
        }

        public override void Update(TimeSpan elapsedTime)
        {
            base.Update(elapsedTime);
            if(this.lifeStage == LifeStage.DYING)
            {
                this.dyingElapsedTime += elapsedTime.TotalSeconds;
                if (this.dyingElapsedTime > DyingTime)
                {
                    this.lifeStage = LifeStage.DEAD;
                    ((AnimatedImage)this.Sprite.Image).SetCurrentAnimation("dead");
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
