namespace Assets.Source.Math
{
    public static class RandomUtils
    {

        /// <summary>
        /// Chooses an item from the passed in list at random.
        /// </summary>
        /// <param name="items">items to choose from</param>
        /// <returns>randomly chosen item</returns>
        public static T Choose<T>(params T[] items) { 
            var len = items.Length; 
            var sel = UnityEngine.Random.Range(0, len-1);
            return items[sel]; 
        }

    }
}
