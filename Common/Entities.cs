using System;

namespace Common
{
    public static class Entities
    {
        public static Type GetType(string entityName)
        {
            Type entityType = Type.GetType("Common." + entityName);

            return entityType;
        }
    }
}