using System.IO;

namespace prjJogoMemoria.MyComponents
{
    public static class MyStrings
    {
        #region Variables

        public static readonly string[] controls = { "X", "⛶", "_" };
        public static readonly string[] Difficulties = { "Fácil", "Médio", "Difícil" };

        #endregion

        #region Fonts

        public static readonly string fontPath =
            Path.Combine(Path.GetFullPath(@"..\..\"), "Resources\\Fonts\\PixelOperator.ttf");

        #endregion

        #region Songs

        public static readonly string openingSongPath =
            Path.Combine(Path.GetFullPath(@"..\..\"), "Resources\\Songs\\Opening.wav");

        public static readonly string battleSongPath =
            Path.Combine(Path.GetFullPath(@"..\..\"), "Resources\\Songs\\Battle.wav");

        #endregion

        #region Animations - Adventure

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

        #endregion

        #region Cards

        public static readonly string weaponsPath =
            Path.Combine(Path.GetFullPath(@"..\..\"), "Resources\\Cards\\w_");

        #endregion
    }
}
