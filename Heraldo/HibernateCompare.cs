using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Proxier.Extensions;

namespace Heraldo
{
    internal class HibernateCompare
    {
        /// <inheritdoc />
        public HibernateCompare(object baseObject)
        {
            BaseObject = baseObject ?? throw new ArgumentNullException(nameof(baseObject));
            DefaultObject = Activator.CreateInstance(baseObject.GetType().AddParameterlessConstructor());
        }

        public object BaseObject { get; set; }
        public object DefaultObject { get; set; }

        private Dictionary<string, object> Dictionary { get; set; }
        private Dictionary<string, object> Defaultdictionary { get; set; }

        private static JsonSerializerSettings JsonSettings => new JsonSerializerSettings
        {
            PreserveReferencesHandling = PreserveReferencesHandling.Objects
        };

        public Dictionary<string, object> GetDictionary()
        {
            return Dictionary ??
                   (Dictionary = BaseObject.GetPropertiesValue().ToDictionary(i => i.Key, i => i.First()));
        }

        public Dictionary<string, object> GetDefaultDictionary()
        {
            return Defaultdictionary ?? (Defaultdictionary =
                       DefaultObject.GetPropertiesValue().ToDictionary(i => i.Key, i => i.First()));
        }

        public List<string> GetToCompare()
        {
            return GetDictionary().Where(pair =>
                !(Equals(GetDefaultDictionary()[pair.Key], pair.Value) ||
                  JsonConvert.SerializeObject(GetDefaultDictionary()[pair.Key], JsonSettings) ==
                  JsonConvert.SerializeObject(pair.Value, JsonSettings))
            ).Select(i => i.Key).ToList();
        }

        public bool Compare(object toCompare)
        {
            var toCompareValues = toCompare.GetPropertiesValue().ToDictionary(i => i.Key, i => i.First());

            foreach (var compareKey in GetToCompare())
                if (!Equals(toCompareValues[compareKey], GetDictionary()[compareKey]))
                    return false;

            return true;
        }
    }
}