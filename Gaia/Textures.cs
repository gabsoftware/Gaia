using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Gaia;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace Gaia
{
    static class Textures
    {
        private static System.Collections.Generic.Dictionary<String, Texture2D> textures;

        public static void Load( GaiaGame game )
        {
            textures = new Dictionary<String, Texture2D>();

            textures.Add( "player",      game.Content.Load<Texture2D>( "Graphics\\player"      ));
            textures.Add( "dirt_middle", game.Content.Load<Texture2D>( "Graphics\\dirt_middle" ));
            textures.Add( "dirt_bottom", game.Content.Load<Texture2D>( "Graphics\\dirt_bottom" ));
            textures.Add( "dirt_top",    game.Content.Load<Texture2D>( "Graphics\\dirt_top"    ));
        }

        public static Texture2D Get(String texture)
        {
            return textures[texture];
        }
    }
}
