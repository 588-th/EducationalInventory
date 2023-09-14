using System;
using System.Collections.Generic;

namespace Logic
{
    public static class SearchData
    {
        public static List<object> GetEntityList(string entityName)
        {
            var unknownItemsList = DataBaseServer.Controll.GetItemsList(entityName);

            return unknownItemsList;
        }
    }
}