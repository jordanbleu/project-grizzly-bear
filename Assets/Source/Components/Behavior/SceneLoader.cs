using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Source.Components.Behavior
{
    public class SceneLoader : MonoBehaviour
    {

        public void GoToBadEnding()
        {
            StartCoroutine(BeginLoadingScene("bad-ending"));
        }

        public void GoToGoodEnding()
        {
            StartCoroutine(BeginLoadingScene("good-ending"));
        }

        private IEnumerator BeginLoadingScene(string name)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name);
            
            // Wait until the asynchronous scene fully loads
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }

        public void ResetScene() => StartCoroutine(BeginLoadingScene(SceneManager.GetActiveScene().name));

    }
}