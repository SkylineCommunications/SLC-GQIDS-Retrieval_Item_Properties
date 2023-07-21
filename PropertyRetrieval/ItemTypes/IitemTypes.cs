namespace PropertyRetrieval.ItemTypes
{
    using System.Collections.Generic;
    using PropertyRetrieval.DTO;

    internal interface IitemTypes
    {
        IEnumerable<PropertyConfigInfo> PropertyNamesAndIds{ get; }

        IEnumerable<ItemInfo> ItemInfos { get; }

        IEnumerable<Dictionary<string,string>> PropertiesTable { get; }

        IEnumerable<PropertyUsage> PropertyUsages { get; }
    }
}
