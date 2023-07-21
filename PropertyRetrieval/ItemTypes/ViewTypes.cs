namespace PropertyRetrieval.ItemTypes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using PropertyRetrieval.DTO;
    using Skyline.DataMiner.Net;
    using Skyline.DataMiner.Net.Messages;

    internal class ViewTypes : BaseItemTypes
    {
        private readonly IConnection _connection;

        public ViewTypes(IConnection connection) : base(connection,"View")
        {
            _connection = connection;
        }

        protected override IEnumerable<ItemInfo> GetItemInfoFromServer()
        {
            var viewPropertyValues = _connection.HandleMessage(new GetInfoMessage { Type = InfoType.ViewInfo });

            List<ItemInfo> items = new List<ItemInfo>();

            foreach (var viewMessage in viewPropertyValues)
            {
                var viewInfo = (ViewInfoEventMessage)viewMessage;

                items.Add(new ItemInfo
                {
                    Id = Convert.ToString(viewInfo.ID),
                    Name = viewInfo.Name,
                    PropertyNameAndValues = viewInfo.Properties.Select(x => new KeyValuePair<string, string>(x.Name, x.Value)),
                });
            }

            return items;
        }
    }
}
