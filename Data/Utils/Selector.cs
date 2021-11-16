using System.Linq;

namespace se_training.Data
{
    public record Selector
    {
        public string[] Relations { get; set; }
        public string[] Fields { get; set; }

        public static Selector FromString(string selector)
        {
            var parts = selector.Split(',');
            var fields = parts.Where(p => p.StartsWith("fields=")).Select(p => p.Substring(7)).ToArray();
            var relations = parts.Where(p => p.StartsWith("relations=")).Select(p => p.Substring(11)).ToArray();
            return new Selector
            {
                Fields = fields,
                Relations = relations
            };
        }
    }
}