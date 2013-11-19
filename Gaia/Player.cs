using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gaia
{
    public enum PlayerStatus
    {
        Standing,
        Walking,
        Jumping,
        Falling,
        Hurt,
        Dead
    }
    public enum PlayerDirection
    {
        Left,
        Right
    }

    class Player
    {
        public Texture2D Texture
        {
            get;
            set;
        }

        public Vector2 Position;

        public int Health
        {
            get;
            set;
        }

        public PlayerStatus Status
        {
            get;
            set;
        }

        public PlayerDirection Direction
        {
            get;
            set;
        }

        public int Width
        {
            get
            {
                return this.Texture.Width;
            }
        }

        public int Height
        {
            get
            {
                return this.Texture.Height;
            }
        }


        public void Initialize(Texture2D texture, Vector2 position)
        {
            this.Texture   = texture;
            this.Position  = position;
            this.Health    = 100;
            this.Status    = PlayerStatus.Standing;
            this.Direction = PlayerDirection.Left;
        }

        public void Update()
        {

        }

        public void Draw( SpriteBatch spriteBatch )
        {
            SpriteEffects spriteEffect;
            if (this.Direction == PlayerDirection.Left)
            {
                spriteEffect = SpriteEffects.None;
            }
            else
            {
                spriteEffect = SpriteEffects.FlipHorizontally;
            }

            spriteBatch.Draw(
                this.Texture,        // texture
                this.Position,       // position
                null,                // source rectangle
                Color.White,         // color tint
                0f,                  // rotation
                Vector2.Zero,        // origin
                1f,                  // scale
                spriteEffect,        // effect
                0f);                 // sort depth
        }
    }
}
