using System.Collections.Generic;
using System.Linq;

namespace DataBaseLocal
{
    public static class Controll
    {
        public static List<TEntity> GetItemsList<TEntity>() where TEntity : class
        {
            using (var dbContext = new LocalInventoryContext())
            {
                return dbContext.Set<TEntity>().ToList();
            }
        }
    }
}