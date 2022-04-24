using System.Collections.Generic;

namespace Assets.Source.Strings
{
    /// <summary>
    /// This is awful.  If this game ever becomes hugely famous this should be rewritten.
    /// </summary>
    public static class Strings_En
    {

        public static Dictionary<string, string> Strings = new Dictionary<string, string>() 
        {
            
            // todo: use sprites here, i think we can use the rich text tags in textmesh pro
            // kbm controls
            { "jump-tutorial-kbm", "Press [Space] to jump" },
            { "pickup-tutorial-kbm", "Press [LMB] to pick things up" },
            { "drop-tutorial-kbm", "Press [LMB] again to drop items" },
            { "throw-tutorial-kbm", "Press [RMB] to throw items" },

            // xbox controls
            { "jump-tutorial-xbox", @"Press <sprite=""Xbox-button-prompts"" index=0> to jump" },
            { "pickup-tutorial-xbox", @"Press <sprite=""Xbox-button-prompts"" index=2> to pick things up" },
            { "drop-tutorial-xbox", @"Press <sprite=""Xbox-button-prompts"" index=2> again to drop items" },
            { "throw-tutorial-xbox", "Press (Y) to throw items" },

        };


    }
}
