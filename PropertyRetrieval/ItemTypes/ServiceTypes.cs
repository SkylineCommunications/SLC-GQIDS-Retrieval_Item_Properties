namespace PropertyRetrieval.ItemTypes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using PropertyRetrieval.DTO;
    using Skyline.DataMiner.Net;
    using Skyline.DataMiner.Net.Messages;

    internal class ServiceTypes : BaseItemTypes
    {
        private readonly IConnection _connection;

        public ServiceTypes(IConnection connection) : base(connection, "Service")
        {
            _connection = connection;
        }

        protected override IEnumerable<ItemInfo> GetItemInfoFromServer()
        {
            var servicePropertyValues = _connection.HandleMessage(new GetInfoMessage { Type = InfoType.ServiceInfo });

            List<ItemInfo> items = new List<ItemInfo>();

            foreach (var serviceMessage in servicePropertyValues)
            {
                var serviceInfo = (ServiceInfoEventMessage)serviceMessage;

                items.Add(new ItemInfo
                {
                    Id = Convert.ToString(serviceInfo.ID),
                    Name = serviceInfo.Name,
                    PropertyNameAndValues = serviceInfo.Properties.Select(x => new KeyValuePair<string, string>(x.Name, x.Value)),
                });
            }

            return items;
        }
    }
}
