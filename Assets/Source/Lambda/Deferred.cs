using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Source.Lambda
{

    /// <summary>
    /// Similar to the <seealso cref="Lazy{T}"/> but it also caches the result forever.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Deferred<T>
    {
        private bool lambdaExecuted = false;

        private T result = default(T);

        private readonly Func<T> func;

        public Deferred(Func<T> func)
        {
            this.func = func;
        }

        public T Value {
            get {

                if (lambdaExecuted) {
                    return result;
                }

                result = func();

                lambdaExecuted = true;

                return result; 
            }
        }

        public void Evict() {
            lambdaExecuted = false;
        }

    }
}
