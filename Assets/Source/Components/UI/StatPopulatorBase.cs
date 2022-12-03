using TMPro;
using UnityEngine;

namespace Assets.Source.Components.UI
{
    public abstract class StatPopulatorBase : MonoBehaviour
    {
        private TextMeshProUGUI textMesh;
        public abstract string GetStatText();

        private void Start()
        {
            textMesh = GetComponent<TextMeshProUGUI>();
            textMesh.text = GetStatText();            
        }


        

    }
}
