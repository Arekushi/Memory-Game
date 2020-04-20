using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjJogoMemoria.MyComponents {
    public static class MyStrings {

        public static readonly string[] controls = { "X", "⛶", "_" };
        public static readonly string[] dificuldades = { "Fácil", "Médio", "Difícil" };

        /* Font - PixelOperator Path */
        public static readonly string fontPath = 
            Path.Combine(Path.GetFullPath(@"..\..\"), "Resources\\Fonts\\PixelOperator.ttf");

        /* Songs */
        public static readonly string openingSongPath =
            Path.Combine(Path.GetFullPath(@"..\..\"), "Resources\\Songs\\Opening.wav");

        public static readonly string battleSongPath =
            Path.Combine(Path.GetFullPath(@"..\..\"), "Resources\\Songs\\Battle.wav");

        /* Animations - adventure */
        public static readonly string adventureIdle =
            Path.Combine(Path.GetFullPath(@"..\..\"), "Resources\\Anime\\adventure\\idle\\Frame-");

        public static readonly string adventureHit =
            Path.Combine(Path.GetFullPath(@"..\..\"), "Resources\\Anime\\adventure\\hit\\Frame-");

        public static readonly string adventureDie =
            Path.Combine(Path.GetFullPath(@"..\..\"), "Resources\\Anime\\adventure\\die\\Frame-");

        public static readonly string adventureAttack =
            Path.Combine(Path.GetFullPath(@"..\..\"), "Resources\\Anime\\adventure\\attack\\Frame-");

        public static readonly string adventureLoading =
            Path.Combine(Path.GetFullPath(@"..\..\"), "Resources\\Anime\\adventure\\loading\\Frame-");

        /* Cards - Weapons */
        public static readonly string weaponsPath =
            Path.Combine(Path.GetFullPath(@"..\..\"), "Resources\\Cards\\w_");
    }
}
