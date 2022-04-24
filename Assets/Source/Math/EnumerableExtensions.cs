using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Source.Math
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Chooses a random item from a list of items.  If nothing exists
        /// in the list, the default value of <typeparamref name="T"/> will be returned
        /// </summary>
        /// <returns>the random item</returns>
        public static T PickOne<T>(this IList<T> list) 
        {
            UnityEngine.Random.InitState(DateTime.Now.Millisecond);

            if (!list.Any()) {
                return default;
            }

            var index = UnityEngine.Random.Range(0, list.Count);
            return list[index];        
        } 


    }
}
