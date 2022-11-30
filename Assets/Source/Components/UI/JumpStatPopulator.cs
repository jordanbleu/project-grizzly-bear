using Assets.Source.Data;

namespace Assets.Source.Components.UI
{
    internal class JumpStatPopulator : StatPopulatorBase
    {
        public override string GetStatText() => InMemoryGameData.Jumps.ToString();
    }
}
