using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Source.Unity
{
    public static class UnityUtils
    {

        /// <summary>
        /// This is the proper way to check if an object is null according to some random
        /// guy on the internet.  It's apparently due to the way that C# classes wrap 
        /// C++ objects in memory.
        /// </summary>
        /// <param name="unityObject">Object to check</param>
        public static bool Exists(UnityEngine.Object unityObject) => unityObject != null && !unityObject.Equals(null);

        /// <summary>
        /// Creates a color from actual RGB values rather than floats because that is stupid.
        /// </summary>
        /// <returns>UnityEngine Color</returns>
        public static Color Color(byte r, byte g, byte b) => new Color((float)r / 255, (float)g / 255, (float)b / 255, 1);

        public const int EVERYTHING_LAYER_MASK = ~0;

        public const int NOTHING_LAYER_MASK = ~-1;

        public static Color InvertColor(Color color) => UnityUtils.Color((byte)(255 - color.r), (byte)(255 - color.g), (byte)(255 - color.b));


    }
}
