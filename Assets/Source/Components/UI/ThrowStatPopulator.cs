using Assets.Source.Data;

namespace Assets.Source.Components.UI
{
    internal class ThrowStatPopulator : StatPopulatorBase
    {
        public override string GetStatText() => InMemoryGameData.Throws.ToString();
    }
}
