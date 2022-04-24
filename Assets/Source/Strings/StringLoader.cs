using Assets.Source.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Source.Strings
{
    /// <summary>
    /// Class responsible for loading the proper strings based on current configuration context, i.e. language, etc
    /// </summary>
    public class StringLoader
    {
        /// <summary>
        /// Loads a localized string from the string repository.  Will throw an exception if the string id doesn't exist.
        /// </summary>
        /// <param name="language">Current language</param>
        /// <param name="id">the id of the string to load</param>
        /// <returns></returns>
        public string LoadString(LanguageCode language, string id)
        {

            switch (language) {
                case LanguageCode.EN:
                    return Strings_En.Strings[id];
                default:
                    return string.Empty;
            }

        }

    }
}
