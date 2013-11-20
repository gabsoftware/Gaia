using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gaia
{
    /// <summary>
    /// Describes the physics of the block.
    /// </summary>
    public enum BlockPhysics
    {
        /// <summary>
        /// Player can go through and fall into Air block with little resistance.
        /// </summary>
        Air,

        /// <summary>
        /// Player can swim into and fall into Liquid block with medium resistance.
        /// </summary>
        Liquid,

        /// <summary>
        /// Player can walk and run onto Solid blocks.
        /// </summary>
        Solid,

        /// <summary>
        /// Similar to Solid blocks except that Sandy blocks fall when the underneath block disapears.
        /// </summary>
        Sandy,

        /// <summary>
        /// Similar to Liquid blocks but with high resistance and cannot swim.
        /// </summary>
        Slimy,

        /// <summary>
        /// Similar to Solid blocks but player will stick to any side of the block he is in contact to.
        /// </summary>
        Sticky
    }

    static class Blocks
    {
        public static Block Dirt
        {
            get
            {
                return new Block()
                {
                    BlockID = 1,
                    Physics = BlockPhysics.Solid,
                    Texture = "dirt",
                    Health = 50,
                    Damage = 0,
                    Resistance = 20,
                    Minable = true,
                    RequiredPower = 1
                };
            }
        }
    }
}
