using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Metadata.Edm;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public static class Extensions
    {
        public static TEntity AddOrUpdate<TEntity>(this DbContext context, TEntity entity) where TEntity : class {
            var tracked = context.Set<TEntity>().Find(context.KeyValuesFor(entity));
            if (tracked != null) {
                context.Entry(tracked).CurrentValues.SetValues(entity);
                return tracked;
            }

            context.Set<TEntity>().Add(entity);
            return entity;
        }

        public static object[] KeyValuesFor(this DbContext context, object entity) {
            var entry = context.Entry(entity);
            return context.KeysFor(entity.GetType())
                .Select(k => entry.Property(k).CurrentValue)
                .ToArray();
        }

        public static IEnumerable<string> KeysFor(this DbContext context, Type entityType) {
            entityType = ObjectContext.GetObjectType(entityType);

            var metadataWorkspace =
                ((IObjectContextAdapter)context).ObjectContext.MetadataWorkspace;
            var objectItemCollection =
                (ObjectItemCollection)metadataWorkspace.GetItemCollection(DataSpace.OSpace);

            var ospaceType = metadataWorkspace
                .GetItems<EntityType>(DataSpace.OSpace)
                .SingleOrDefault(t => objectItemCollection.GetClrType(t) == entityType);

            if (ospaceType == null) {
                throw new ArgumentException(
                    string.Format(
                        "The type '{0}' is not mapped as an entity type.",
                        entityType.Name),
                    "entityType");
            }

            return ospaceType.KeyMembers.Select(k => k.Name);
        }
    }
}
