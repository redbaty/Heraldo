using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Proxier.Extensions;

namespace Heraldo
{
    public static class EntityFrameworkEhMelhorQueHibernate
    {
        public static IEnumerable<T> FindByObject<T>(this DbSet<T> dbSet, T toFind) where T : class
        {
            var hCompare = new HibernateCompare(toFind);

            var toCompare = hCompare.GetToCompare();

            dbSet.Load();

            foreach (var unknown in dbSet
                .Select(obj =>
                    new HibernateObjects<T>(obj, obj.GetPropertiesValue().ToDictionary(i => i.Key, i => i.First())))
                .Where(t => CompareNested(toCompare, t, hCompare.GetDictionary()))
                .Select(t => t.Obj))
                yield return unknown;
        }

        private static bool CompareNested<T>(List<string> toCompare, HibernateObjects<T> t,
            Dictionary<string, object> toFindObject) where T : class
        {
            foreach (var comp in toCompare)
            {
                var propertyInfo = t.Obj.GetType().GetProperty(comp);
                if (propertyInfo.PropertyType.Namespace.StartsWith("System"))
                {
                    if (t.ObjValues[comp] is ICollection sourceCollection &&
                        toFindObject[comp] is ICollection toFindCollection)
                        for (var index = 0; index < toFindCollection.OfType<object>().ToList().Count; index++)
                        {
                            var objec = toFindCollection.OfType<object>().ToList()[index];
                            var sourceobjec = sourceCollection.OfType<object>().ToList()[index];
                            var nestedCompare = new HibernateCompare(objec);
                            return nestedCompare.Compare(sourceobjec);
                        }

                    if (t.ObjValues[comp] != toFindObject[comp])
                        return false;
                }
                else
                {
                    return true;
                }
            }

            return true;
        }
    }
}