using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Source.Components.Debug
{
    public class GameObjectLogger : MonoBehaviour
    {
        public void Log(string message) =>
            UnityEngine.Debug.Log(message);

        public void LogWarning(string message) =>
            UnityEngine.Debug.LogWarning(message);

    }
}
