using Assets.Source.Data;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Source.Components.Behavior
{
    public class SceneLoader : MonoBehaviour
    {

        public void GoToBadEnding()
        {
            InMemoryGameData.IsBadEnding = false;
            StartCoroutine(BeginLoadingScene("bad-ending"));
        }

        public void GoToGoodEnding()
        {
            InMemoryGameData.IsBadEnding = false;
            StartCoroutine(BeginLoadingScene("good-ending"));
        }

        public void GoToGame()
        {
            StartCoroutine(BeginLoadingScene("game"));
        }

        public void GoToEndCredits()
        {
            StartCoroutine(BeginLoadingScene("end-credits"));
        }

        public void GoToIntroScreens()
        {
            // reset data


            StartCoroutine(BeginLoadingScene("intro-screens"));
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