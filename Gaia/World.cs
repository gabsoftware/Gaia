using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text;
using ProtoBuf;
using System.IO;

namespace Gaia
{
    public enum WorldDifficulty
    {
        Easy,
        Normal,
        Medium,
        Hard,
        VeryHard
    }

    public enum WorldSize
    {
        Small   = 1024, // x  8192
        Medium  = 2048, // x 16384
        Big     = 4096, // x 32768
        Extra   = 8192  // x 65536
    }

    public enum WorldMode
    {
        Tutorial,
        FirstDays,
        Normal,
        Hard,
        Hell
    }

    [ProtoContract]
    class World
    {
        [ProtoMember(1)]
        public WorldDifficulty Difficulty
        {
            get;
            set;
        }

        [ProtoMember(2)]
        public WorldSize Size
        {
            get;
            set;
        }

        //[ProtoMember(3)]
        public int Width
        {
            get
            {
                return (int)(this.Size) * 8;
            }
        }

        //[ProtoMember(4)]
        public int Height
        {
            get
            {
                return (int)(this.Size);
            }
        }

        //[ProtoMember(5)]
        public Block[,] Tiles
        {
            get;
            set;
        }


        public void Initialize(WorldSize size)
        {
            //temporary
            this.Size = size;
            this.Generate("test");
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }

        public void Generate(string seed)
        {
            int betterSeed = this.BetterSeed( seed );
            Random r = new Random(betterSeed);

            int i, j, n, m;

            Texture2D texture;

            int surfaceLow  = r.Next( (int)(Math.Round( this.Height * 0.70 ) ), (int)(Math.Round( this.Height * 0.75 )));
            int surfaceHigh = r.Next( (int)(Math.Round( this.Height * 0.80 ) ), (int)(Math.Round( this.Height * 0.85 )));

            Helper.Log("Max height: " + this.Height.ToString());
            Helper.Log(surfaceLow.ToString() + " to " + surfaceHigh.ToString());

            //surface is divided in 8 parts
            int[] pikes_1  = new int[   8];

            //sub-divisions of surface
            int[] pikes_2  = new int[   16];
            int[] pikes_3  = new int[   32];
            int[] pikes_4  = new int[   64];
            int[] pikes_5  = new int[  128];
            int[] pikes_6  = new int[  256];
            int[] pikes_7  = new int[  512];
            int[] pikes_8  = new int[ 1024];
            int[] pikes_9  = new int[ 2048];
            int[] pikes_10 = new int[ 4096];
            int[] pikes_11 = new int[ 8192]; // small world limit
            
            int[] pikes_12 = new int[0];
            if( this.Size >= WorldSize.Medium )
                pikes_12 = new int[ 16384]; // medium world limit
            
            int[] pikes_13 = new int[0];
            if( this.Size >= WorldSize.Big )
                pikes_13 = new int[32768]; // big world limit
            
            int[] pikes_14 = new int[0];
            if( this.Size >= WorldSize.Extra )
                pikes_14 = new int[65536]; // extra world limit

            int[] pikes = null;

           
            for (i = 0, n = pikes_1.Length; i < n; i++)
            {
                pikes_1[i] = r.Next(surfaceLow, surfaceHigh);                
            }
            pikes_2  = fractalMountains( r, pikes_1,  pikes_2,  2  );
            pikes_3  = fractalMountains( r, pikes_2,  pikes_3,  3  );
            pikes_4  = fractalMountains( r, pikes_3,  pikes_4,  4  );
            pikes_5  = fractalMountains( r, pikes_4,  pikes_5,  5  );
            pikes_6  = fractalMountains( r, pikes_5,  pikes_6,  6  );
            pikes_7  = fractalMountains( r, pikes_6,  pikes_7,  7  );
            pikes_8  = fractalMountains( r, pikes_7,  pikes_8,  8  );
            pikes_9  = fractalMountains( r, pikes_8,  pikes_9,  9  );
            pikes_10 = fractalMountains( r, pikes_9,  pikes_10, 10 );
            if (this.Size >= WorldSize.Small)
            {
                pikes_11 = fractalMountains(r, pikes_10, pikes_11, 11);
                pikes = pikes_11;
            }
            if (this.Size >= WorldSize.Medium)
            {
                pikes_12 = fractalMountains(r, pikes_11, pikes_12, 12);
                pikes = pikes_12;
            }
            if (this.Size >= WorldSize.Big)
            {
                pikes_13 = fractalMountains(r, pikes_12, pikes_13, 13);
                pikes = pikes_13;
            }
            if (this.Size >= WorldSize.Extra)
            {
                pikes_14 = fractalMountains(r, pikes_13, pikes_14, 14);
                pikes = pikes_14;
            }
            
            for ( j = 0, m = pikes_10.Length; j < m; j++)
            {
                Helper.Log(pikes_10[j].ToString());
            }

            //create the Tile array
            Tiles = new Block[this.Width, this.Height];            
            
            for ( i = 0; i < this.Width; i++)
            {
                for (j = 0; j < pikes[i]; j++)
                {
                    Tiles[i, j] = new Block();
                    if( j < pikes[i] - 1 )
                    {
                        texture = Textures.Get( "dirt_middle" );
                    }
                    else
                    {
                        texture = Textures.Get( "dirt_top" );
                    }
                    Tiles[i, j].Initialize(texture, new Vector2(i, j), 2, TilePhysics.Solid, 0);
                }
            }

            bool save_ok = this.Save();

            
        }

        private int[] fractalMountains(Random r, int[] source, int[] dest, int level)
        {
            int size = Convert.ToInt32( Math.Pow( 2, level + 1 ) );
            int size2 = size / 2;
            int min, max, diff, entropy, i;
            double coef = r.NextDouble() / 2 + 0.1;

            Helper.Log("coef = " + coef.ToString());

            //first half
            for (i = 0; i < size2; i++)
            {
                dest[i * 2] = source[i];
                min  = Math.Min(source[i], source[i + 1]);
                max  = Math.Max(source[i], source[i + 1]);
                diff = (max - min) + 2;
                entropy = 1 + (int)(diff * ( 1 - coef ) );
                Helper.Log("min = " + min.ToString() + ", max = " + max.ToString() + ", entropy = " + entropy.ToString());
                min = min - entropy;
                max = max + entropy;
                Helper.Log("new min = " + min.ToString() + ", new max = " + max.ToString());
                dest[i * 2 + 1] = r.Next(min, max);
            }

            //second half
            for (i = size - 1; i >= size2; i--)
            {
                dest[i * 2 + 1] = source[i];
                min = Math.Min(source[i - 1], source[i]);
                max = Math.Max(source[i - 1], source[i]);
                diff = (max - min) + 2;
                entropy = 1 + (int)(diff * (1 - coef));
                Helper.Log("min = " + min.ToString() + ", max = " + max.ToString() + ", entropy = " + entropy.ToString());
                min = min - entropy;
                max = max + entropy;
                Helper.Log("new min = " + min.ToString() + ", new max = " + max.ToString());
                dest[i * 2] = r.Next(min, max);
            }
            
            return dest;
        }

        //generate a better seed by returning the MD5 of the specified string
        private int BetterSeed(string seed)
        {
            System.Security.Cryptography.MD5 md5;
            int result;
            
            md5 = System.Security.Cryptography.MD5.Create();
            byte[] hash = md5.ComputeHash(Helper.GetBytes(seed));

            result = BitConverter.ToInt32(hash, 0);
            Helper.Log( "Seed: " + result.ToString() );
            return result;
        }

        public bool Save()
        {
            bool result = false;

            using (var file = File.Create("world.bin"))
            {
                Serializer.Serialize<World>(file, this);
            }

            return result;
        }
    }
}
