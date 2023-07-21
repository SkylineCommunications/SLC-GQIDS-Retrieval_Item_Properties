namespace PropertyRetrieval.ItemTypes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using PropertyRetrieval.DTO;
    using Skyline.DataMiner.Net;
    using Skyline.DataMiner.Net.Messages;

    internal abstract class BaseItemTypes : IitemTypes
    {
        private readonly IConnection _connection;
        private readonly string _itemTypeName;

        private IEnumerable<PropertyConfigInfo> _propertyNamesAndIds;
        private IEnumerable<ItemInfo> _itemInfos;

        public BaseItemTypes(IConnection connection, string itemTypeName)
        {
            _connection = connection;
            _itemTypeName = itemTypeName;
        }

        public IEnumerable<PropertyConfigInfo> PropertyNamesAndIds
        {
            get
            {
                if (_propertyNamesAndIds == null)
                {
                    var allPropertyConfig = (GetPropertyConfigurationResponse)_connection.HandleSingleResponseMessage(new GetInfoMessage { Type = InfoType.PropertyConfiguration });
                    _propertyNamesAndIds = allPropertyConfig.Properties.Where(x => x.Type == _itemTypeName && x.IsReadOnly == false).Select(x => new PropertyConfigInfo { Id = x.ID, Name = x.Name } );
                }

                return _propertyNamesAndIds;
            }
        }

        public IEnumerable<ItemInfo> ItemInfos
        {
            get
            {
                if (_itemInfos == null)
                {
                    _itemInfos = GetItemInfoFromServer();
                }

                return _itemInfos;
            }
        }

        public IEnumerable<Dictionary<string, string>> PropertiesTable
        {
            get
            {
                List<Dictionary<string, string>> propTable = new List<Dictionary<string, string>>();
                foreach (var itemInfo in ItemInfos)
                {
                    var propertyDictionary = GetEmptyDictionary(PropertyNamesAndIds);

                    propertyDictionary["Name"] = itemInfo.Name;
                    propertyDictionary["Id"] = Convert.ToString(itemInfo.Id);

                    foreach (var propertyKeyValue in itemInfo.PropertyNameAndValues)
                    {
                        propertyDictionary[propertyKeyValue.Key] = propertyKeyValue.Value;
                    }

                    propTable.Add(propertyDictionary);
                }

                return propTable;
            }
        }

        public IEnumerable<PropertyUsage> PropertyUsages
        {
            get
            {
                Dictionary<string, PropertyUsage> dictPropUsages = new Dictionary<string, PropertyUsage>();

                foreach (var propConfigInfo in PropertyNamesAndIds)
                {
                    dictPropUsages.Add(propConfigInfo.Name, new PropertyUsage { ConfigInfo = propConfigInfo, NrOfFilledIn = 0, NrOfNotFilledIn = 0 });
                }

                foreach(var viewProperties in PropertiesTable)
                {
                    foreach(var propConfigInfo in PropertyNamesAndIds)
                    {
                       if(String.IsNullOrWhiteSpace(viewProperties[propConfigInfo.Name]))
                       {
                            dictPropUsages[propConfigInfo.Name].NrOfNotFilledIn++;
                       }
                       else
                       {
                            dictPropUsages[propConfigInfo.Name].NrOfFilledIn++;
                        }
                    }
                }

                return dictPropUsages.Values;
            }
        }

        protected abstract IEnumerable<ItemInfo> GetItemInfoFromServer();

        private Dictionary<string, string> GetEmptyDictionary(IEnumerable<PropertyConfigInfo> allViewPropertyNames)
        {
            Dictionary<string, string> allProperties = new Dictionary<string, string>
            {
                { "Name", string.Empty },
                { "Id", string.Empty },
            };

            foreach (var propName in allViewPropertyNames.Select(x=> x.Name))
            {
                allProperties.Add(propName, string.Empty);
            }

            return allProperties;
        }
    }
}
