using Assets.Source.Data;

namespace Assets.Source.Components.UI
{
    internal class DamageStatPopulator : StatPopulatorBase
    {
        public override string GetStatText() => InMemoryGameData.Damage.ToString();

    }
}
