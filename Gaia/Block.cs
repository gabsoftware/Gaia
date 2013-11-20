using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Gaia;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using ProtoBuf;

namespace Gaia
{  
    /// <summary>
    /// Defines a block
    /// </summary>
    [ProtoContract]
    class Block
    {
        /// <summary>
        /// The unique ID of the block
        /// </summary>
        [ProtoMember(1)]
        public int BlockID
        {
            get;
            set;
        }

        /// <summary>
        /// The physics of the block. Will influence how the player can interact with the block.
        /// </summary>
        [ProtoMember(2)]
        public BlockPhysics Physics
        {
            get;
            set;
        }

        /// <summary>
        /// The texture of the block
        /// </summary>
        [ProtoMember(3)]
        public string Texture
        {
            get;
            set;
        }

        /// <summary>
        /// The health of the block. Influences mining speed.
        /// </summary>
        [ProtoMember(4)]
        public int Health
        {
            get;
            set;
        }

        /// <summary>
        /// Damage inflicted when the player touches the block, per second
        /// </summary>
        [ProtoMember(5)]
        public int Damage
        {
            get;
            set;
        }

        /// <summary>
        /// Resistance [1-100] when the player travel through the block for liquid blocks, or run onto it for solid blocks.
        /// </summary>
        [ProtoMember(6)]
        public int Resistance
        {
            get;
            set;
        }

        /// <summary>
        /// Defines if the block is minable or not
        /// </summary>
        [ProtoMember(7)]
        public Boolean Minable
        {
            get;
            set;
        }

        /// <summary>
        /// The required power for a tool to be able to mine the block
        /// </summary>
        [ProtoMember(8)]
        public int RequiredPower
        {
            get;
            set;
        }

        /// <summary>
        /// Returns the texture for blocks in middle of similar blocks
        /// </summary>
        public Texture2D TextureMiddle
        {
            get
            {
                return Textures.Get(this.Texture + "_middle");
            }            
        }

        /// <summary>
        /// Returns the texture for blocks with top border
        /// </summary>
        public Texture2D TextureTop
        {
            get
            {
                return Textures.Get(this.Texture + "_top");
            }
        }

        /// <summary>
        /// Returns the texture for blocks with bottom border
        /// </summary>
        public Texture2D TextureBottom
        {
            get
            {
                return Textures.Get(this.Texture + "_bottom");
            }
        }

        /// <summary>
        /// Returns the texture for blocks with left border
        /// </summary>
        public Texture2D TextureLeft
        {
            get
            {
                return Textures.Get(this.Texture + "_left");
            }
        }

        /// <summary>
        /// Returns the texture for blocks with right border
        /// </summary>
        public Texture2D TextureRight
        {
            get
            {
                return Textures.Get(this.Texture + "_right");
            }
        }

        /// <summary>
        /// Returns the texture for blocks with top-left border
        /// </summary>
        public Texture2D TextureTopLeft
        {
            get
            {
                return Textures.Get(this.Texture + "_topleft");
            }
        }

        /// <summary>
        /// Returns the texture for blocks with top-right border
        /// </summary>
        public Texture2D TextureTopRight
        {
            get
            {
                return Textures.Get(this.Texture + "_topright");
            }
        }

        /// <summary>
        /// Returns the texture for blocks with bottom-left border
        /// </summary>
        public Texture2D TextureBottomLeft
        {
            get
            {
                return Textures.Get(this.Texture + "_bottomleft");
            }
        }

        /// <summary>
        /// Returns the texture for blocks with bottom-right border
        /// </summary>
        public Texture2D TextureBottomRight
        {
            get
            {
                return Textures.Get(this.Texture + "_bottomright");
            }
        }



    }
}
