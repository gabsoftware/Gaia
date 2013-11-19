using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gaia
{
    public enum TilePhysics
    {
        Air,
        Liquid,
        Sand,
        Solid
    }

    class Tile
    {
        public int Solidity
        {
            get;
            set;
        }

        public TilePhysics Physics
        {
            get;
            set;
        }

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

        public int Speed
        {
            get;
            set;
        }

        public void Initialize(Texture2D texture, Vector2 position, int solidity, TilePhysics physics, int speed)
        {
            this.Solidity = solidity;
            this.Physics  = physics;
            this.Texture  = texture;
            this.Position = position;
            this.Health   = 100;
            this.Speed    = speed;
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                this.Texture,        // texture
                this.Position,       // position
                null,                // source rectangle
                Color.White,         // color tint
                0f,                  // rotation
                Vector2.Zero,        // origin
                1f,                  // scale
                SpriteEffects.None,  // effect
                0f);                 // sort depth
        }
    }
}
