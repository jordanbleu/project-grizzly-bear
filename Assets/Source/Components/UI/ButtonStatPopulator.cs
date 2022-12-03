using Assets.Source.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Source.Components.UI
{
    internal class ButtonStatPopulator : StatPopulatorBase
    {
        public override string GetStatText() => InMemoryGameData.ButtonsPressed.ToString();
    }
}
