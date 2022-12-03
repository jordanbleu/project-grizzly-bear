using Assets.Source.Data;

namespace Assets.Source.Components.UI
{
    internal class EndingStatPopulator : StatPopulatorBase
    {
        public override string GetStatText()
        {
            if (InMemoryGameData.IsBadEnding)
            {
                return "You got the bad ending :(";
            }
            return "You got the good ending :)";
        }
    }
}
