using System;
using UnityEngine;

namespace Assets.Source.Components.UI
{
    internal class PlaytimeStatPopulator : StatPopulatorBase
    {
        public override string GetStatText() 
        {
            var ts = TimeSpan.FromSeconds(Time.realtimeSinceStartup);
            // format to {hours}:{minutes}:{seconds}:{fractions of a second}
            return ts.ToString("hh':'mm':'ss'.'fff");
        }


    }
}
