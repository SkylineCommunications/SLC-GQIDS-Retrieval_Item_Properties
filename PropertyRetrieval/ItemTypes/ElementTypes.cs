namespace PropertyRetrieval.ItemTypes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using PropertyRetrieval.DTO;
    using Skyline.DataMiner.Net;
    using Skyline.DataMiner.Net.Messages;

    internal class ElementTypes : BaseItemTypes
    {
        private readonly IConnection _connection;

        public ElementTypes(IConnection connection) : base(connection, "Element")
        {
            _connection = connection;
        }

        protected override IEnumerable<ItemInfo> GetItemInfoFromServer()
        {
            var elementPropertyValues = _connection.HandleMessage(new GetInfoMessage { Type = InfoType.ElementInfo });
            List<ItemInfo> items = new List<ItemInfo>();

            foreach (var elemenMessage in elementPropertyValues)
            {
                var elementInfo = (ElementInfoEventMessage)elemenMessage;

                items.Add(new ItemInfo
                {
                    Id = Convert.ToString(elementInfo.ElementID),
                    Name = elementInfo.Name,
                    PropertyNameAndValues = elementInfo.Properties.Select(x=> new KeyValuePair<string, string>(x.Name, x.Value)),
                });
            }

            return items;
        }
    }
}
