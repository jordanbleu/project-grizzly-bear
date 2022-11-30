using Assets.Source.Data;

namespace Assets.Source.Components.UI
{
    internal class DropStatPopulator : StatPopulatorBase
    {
        public override string GetStatText() => InMemoryGameData.Drops.ToString();
    }
}
