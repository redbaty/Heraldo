using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Proxier.Extensions;

namespace Heraldo
{
    public static class EntityFrameworkEhMelhorQueHibernate
    {
        public static T FindByObject<T>(this DbSet<T> dbSet, T toFind) where T : class
        {
            var defaultObject = Activator.CreateInstance(toFind.GetType().AddParameterlessConstructor())
                .GetPropertiesValue().ToDictionary(i => i.Key, i => i.First());
            var toFindObject = toFind.GetPropertiesValue().ToDictionary(i => i.Key, i => i.First());

            var toCompare = toFindObject.Where(pair =>
                !(Equals(defaultObject[pair.Key], pair.Value) || JsonConvert.SerializeObject(defaultObject[pair.Key]) ==
                  JsonConvert.SerializeObject(pair.Value))
            ).Select(i => i.Key).ToList();

            dbSet.Load();

            return Enumerable.FirstOrDefault((from obj in dbSet
                let objValues = obj.GetPropertiesValue().ToDictionary(i => i.Key, i => i.First())
                where toCompare.All(comp => objValues[comp] == toFindObject[comp])
                select obj));
        }
    }
}
