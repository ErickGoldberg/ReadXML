using System.Xml;

namespace XMLSearch.Utils
{
    // Trazer tudo que se repete nas classes para ca e deixar mais génerico(pode utilizar static em tudo)
    // para chamar é só botar "XMLUtils." e a continuação
    // e da para botar uma exceptions em algumas coisas
    
    public class XMLUtils
    {
        public static string GetAttributeValue(XmlNode node, string attributeName)
        {
            return node?.Attributes?[attributeName]?.Value;
        }

        public static T ParseValue<T>(string value)
        {
            if (string.IsNullOrEmpty(value))
                return default;

            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}
