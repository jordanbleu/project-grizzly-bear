using System;

namespace Assets.Source.Data
{
    /// <summary>
    /// Static class that simply holds config data for the duration of the game.
    /// </summary>
    public static class InMemoryGameData
    {
        /// <summary>
        /// The code for the current langauge
        /// </summary>
        public static LanguageCode Language => LanguageCode.EN;

        /// <summary>
        /// The last checkpoint the user passed
        /// </summary>
        public static Checkpoint LastCheckpoint { get; set; } = Checkpoint.Frame1_Start;
        
        /// <summary>
        /// Total amount of time the player took to finish the game (not populated until the game is finished).
        /// NOTE FOR FUTURE - Use Time.realtimeSinceStartup
        /// </summary>
        public static TimeSpan TotalPlayTime { get; set; }

        /// <summary>
        /// How many times the player died
        /// </summary>
        public static int Deaths { get; set; }

        /// <summary>
        /// How many times the player jumped
        /// </summary>
        public static int Jumps { get; set; }

        /// <summary>
        /// How many items the player threw 
        /// </summary>
        public static int Throws { get; set; }

        public static int Pickups { get; set; }
        public static int Drops { get; set; }

        /// <summary>
        /// How many buttons the player pressed
        /// </summary>
        public static int ButtonsPressed { get; set; }
        public static int Damage { get; set; }

    }
}
