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
            { "pickup-tutorial-kbm", "Press [LMB] to pick up / drop items" },
            { "drop-tutorial-kbm", "Press [LMB] again to drop items" },
            { "throw-tutorial-kbm", "Press [RMB] to throw items" },
            { "interact-tutorial-kbm", "Press [F] to interact" },
            { "drop-tutorial2-kbm", "Press [LMB] to drop items" },
            { "press-jump-to-begin-kbm", "Press [Space] to begin"},
            { "press-again-to-reset-kbm", "Press [F5] again to self-destruct.  Progress will be lost."},
            { "reset-tutorial-kbm", "If you get stuck, press [F5] twice to self-destruct"},
            { "checkpoint-kbm", "Checkpoint reached"},

            // xbox controls
            { "jump-tutorial-xbox", @"Press <sprite=""Xbox-button-prompts"" index=0> to jump" },
            { "pickup-tutorial-xbox", @"Press <sprite=""Xbox-button-prompts"" index=2>  to pick up / drop items" },
            { "drop-tutorial-xbox", @"Press <sprite=""Xbox-button-prompts"" index=2> again to drop items" },
            { "drop-tutorial2-xbox", @"Press <sprite=""Xbox-button-prompts"" index=2> to drop items" },
            { "throw-tutorial-xbox", @"Press <sprite=""Xbox-button-prompts"" index=3> to throw items" },
            { "interact-tutorial-xbox", @"Press <sprite=""Xbox-button-prompts"" index=1> to interact" },
            { "press-jump-to-begin-xbox", @"Press  <sprite=""Xbox-button-prompts"" index=0> to begin"},
            { "press-again-to-reset-xbox", @"Press <sprite=""Xbox-button-prompts"" index=4> again to self-destruct. Progress will be lost."},
            { "reset-tutorial-xbox", @"If you get stuck, press <sprite=""Xbox-button-prompts"" index=4> twice to self-destruct"},
            { "checkpoint-xbox", "Checkpoint reached"},



        };


    }
}
