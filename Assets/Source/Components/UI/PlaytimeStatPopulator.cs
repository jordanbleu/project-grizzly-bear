using Assets.Source.Data;

namespace Assets.Source.Components.UI
{
    internal class PlaytimeStatPopulator : StatPopulatorBase
    {
        public override string GetStatText() 
        {
            var ts = InMemoryGameData.FinishTime - InMemoryGameData.StartTime;
            // format to {hours}:{minutes}:{seconds}:{fractions of a second}
            return ts.ToString("hh':'mm':'ss'.'fff");
        }


    }
}
