using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Proxier.Extensions;

namespace Heraldo
{
    static class Program
    {
        private static void Main(string[] args)
        {
            var context = new HeraldoContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.TestClasses.Add(new TestClass
            {
                Name = "Heraldu",
                SubClass = new SubClass
                {
                    Test = "Hello world!"
                }
            });
            context.SaveChanges();
            var yo = context.TestClasses.FindByObject(new TestClass
            {
                Name = "Heraldu",
                SubClass = new SubClass
                {
                    Test = "Hello worlad!"
                }
            });
        }
    }


    public static class EntityFrameworkEhMelhorQueHibernate
    {
        public static T FindByObject<T>(this DbSet<T> dbSet, T toFind) where T : class
        {
            var propsToInclude = toFind.GetType().GetProperties()
                .Where(i => !i.PropertyType?.Namespace?.StartsWith("System") ?? false).ToList();

            foreach (var propertyInfo in propsToInclude)
                dbSet.Include(arg => propertyInfo);

            dbSet.Load();

            var delta = GetNonDefaultValues(toFind, propsToInclude);

            return dbSet.FirstOrDefault(x => Heraldo(x, x, delta));
        }

        private static Dictionary<string, object> GetNonDefaultValues<T>(this T toFind,
            List<PropertyInfo> propsToInclude)
            where T : class
        {
            var defaultObject = Activator.CreateInstance(toFind.GetType());
            var delta = toFind.GetPropertiesValue().ToDictionary(i => i.Key, i => i.First());

            foreach (var prop in defaultObject.GetPropertiesValue())
            {
                if (propsToInclude.All(i => i.Name != prop.Key) && Equals(delta[prop.Key], prop.First()))
                    delta.Remove(prop.Key);
            }

            return delta;
        }

        private static bool Heraldo<T>(T objec, T originalObject, Dictionary<string, object> delta)
            where T : class
        {
            var customProperties = objec.GetType().GetProperties()
                .Where(i => !i.PropertyType?.Namespace?.StartsWith("System") ?? false).ToList();

            Dictionary<string, object> values;
            if (objec == originalObject)
                values = originalObject.GetPropertiesValue().ToDictionary(i => i.Key, i => i.First());
            else
                values = originalObject.GetPropertiesValue().First(i => i.Key == objec.GetType().Name).First()
                    .GetPropertiesValue().ToDictionary(i => i.Key, i => i.First());
            foreach (var prop in delta)
            {
                if (customProperties.Any(i => i.Name == prop.Key))
                {
                    var compareValues = prop.Value.GetNonDefaultValues(customProperties);
                    if (!Heraldo(prop.Value, originalObject, compareValues))
                        return false;
                }

                else if (values[prop.Key] != prop.Value)
                    return false;
            }

            return true;
        }
    }

    class HeraldoContext : DbContext
    {
        public DbSet<TestClass> TestClasses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlite("Data Source=heraldo.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestClass>().HasOne(i => i.SubClass);

            base.OnModelCreating(modelBuilder);
        }
    }

    class TestClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SubClass SubClass { get; set; }
    }

    class SubClass
    {
        public int Id { get; set; }
        public string Test { get; set; }
    }
}
