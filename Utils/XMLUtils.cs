using System.Xml;

namespace XMLSearch.Utils
{
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

        public static XmlNamespaceManager GetNamespaceManager(XmlDocument doc, string namespacePrefix, string namespaceURI)
        {
            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(doc.NameTable);
            namespaceManager.AddNamespace(namespacePrefix, namespaceURI);
            // Adicione outros namespaces conforme necessário

            return namespaceManager;
        }

        public static XmlNode GetNode(XmlDocument doc, string xPath, string namespacePrefix, string namespaceURI)
        {
            XmlNamespaceManager namespaceManager = GetNamespaceManager(doc, namespacePrefix, namespaceURI);
            return doc.SelectSingleNode(xPath, namespaceManager);
        }
    }
}
