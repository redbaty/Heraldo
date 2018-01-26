using System.Collections.Generic;

namespace Heraldo
{
    public class HibernateObjects<T>
    {
        public HibernateObjects(T obj, Dictionary<string, object> objValues)
        {
            Obj = obj;
            ObjValues = objValues;
        }

        public T Obj { get; }
        public Dictionary<string, object> ObjValues { get; }
    }
}