using Assets.Source.Data;
using Assets.Source.Strings;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Source.Components.UI
{
    /// <summary>
    /// Sets the attached text mesh pro object's text to the localized variant
    /// </summary>
    public class TextMeshLocalizer : MonoBehaviour
    {

        [SerializeField]
        private TextMeshProUGUI textMeshProObject;

        [SerializeField]
        private string textKey;
        
        [SerializeField]
        [Tooltip("Just drag the player object here")]
        private PlayerInput playerInputSystem;
        
        private void Update()
        {
            var controlContext = playerInputSystem.currentControlScheme;

            string key;
            switch (controlContext) {
                case "Gamepad":
                    key = $"{textKey}-xbox";
                    break;
                default:
                    key = $"{textKey}-kbm";
                    break;
            }
            
            var sl = new StringLoader();
            var str = sl.LoadString(InMemoryGameData.Language, key);
            textMeshProObject.SetText(str);
        }
    }
}