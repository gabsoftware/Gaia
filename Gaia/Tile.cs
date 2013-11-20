using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProtoBuf;

namespace Gaia
{
    [ProtoContract]
    class Tile
    {
        [ProtoMember(1)]
        public int TileID
        {
            get;
            set;
        }

        [ProtoMember(4)]
        public Block Block
        {
            get;
            set;
        }

        [ProtoMember(7)]
        public int X
        {
            get;
            set;
        }
        [ProtoMember(8)]
        public int Y
        {
            get;
            set;
        }

        public Vector2 Position
        {
            get
            {
                return new Vector2(X, Y);
            }
        }

        public void Initialize( Block block, int x, int y)
        {
            this.TileID   = 1;
            this.Block    = block;
            this.X        = x;
            this.Y        = y;
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                this.Block.TextureMiddle, // texture
                this.Position,            // position
                null,                     // source rectangle
                Color.White,              // color tint
                0f,                       // rotation
                Vector2.Zero,             // origin
                1f,                       // scale
                SpriteEffects.None,       // effect
                0f);                      // sort depth
        }
    }
}
