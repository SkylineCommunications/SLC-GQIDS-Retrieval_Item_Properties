namespace PropertyRetrieval.ItemTypes
{
	using System.Collections.Generic;
	using Skyline.DataMiner.Net;

	internal class ItemTypeHelper
    {
        public static IitemTypes GetItemTypesFromTypeName(string typeName, IConnection connection)
        {
            Dictionary<string, IitemTypes> allItemTypes = new Dictionary<string, IitemTypes>
            {
                {"View", new ViewTypes(connection)},
                {"Element", new ElementTypes(connection)},
                {"Service", new ServiceTypes(connection)},
            };

            return allItemTypes[typeName];
        }

        public static string[] GetItemTypes()
        {
            return new string[] { "View", "Element", "Service" };
        }
    }
}
