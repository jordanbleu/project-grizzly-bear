using Assets.Source.Data;

namespace Assets.Source.Components.UI
{
    internal class DeathStatPopulator : StatPopulatorBase
    {
        public override string GetStatText() => InMemoryGameData.Deaths.ToString();
    }
}
