using System;

namespace Assets.Source.Math
{
    [Serializable]
    public struct Range<T> where T : notnull,IComparable 
    {
        public T min;
        public T max;

        public Range(T min, T max)
        {
            this.min = min;
            this.max = max;
        } 
        
        public bool Surrounds(T value)
        {
         var minComp = value.CompareTo(min);
         var maxComp = value.CompareTo(max);
         return (minComp == -1 && maxComp == 1);
        }
    }
}