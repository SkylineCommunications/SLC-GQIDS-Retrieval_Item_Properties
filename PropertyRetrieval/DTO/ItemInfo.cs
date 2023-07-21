namespace PropertyRetrieval.DTO
{
    using System.Collections.Generic;

    internal class ItemInfo
    {
        public string Name { get; set; }

        public string Id { get; set; }

        public IEnumerable<KeyValuePair<string,string>> PropertyNameAndValues { get; set; }
    }
}
