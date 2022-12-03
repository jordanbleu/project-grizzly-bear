using Assets.Source.Data;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Source.Components.Behavior
{
    public class SceneLoader : MonoBehaviour
    {

        public void GoToBadEnding()
        {
            InMemoryGameData.FinishTime = DateTime.Now;
            InMemoryGameData.IsBadEnding = true;
            StartCoroutine(BeginLoadingScene("bad-ending"));
        }

        public void GoToGoodEnding()
        {
            InMemoryGameData.FinishTime = DateTime.Now;
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