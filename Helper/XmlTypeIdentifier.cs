using System;
using System.Xml;
using XMLSearch.Data.Enum;

namespace XMLSearch.Helper
{
    public static class XmlTypeIdentifier
    {
        public static EnumTypeXML IdentifyXmlType(string xmlFilePath)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFilePath);

                XmlNamespaceManager nsManager = new XmlNamespaceManager(xmlDoc.NameTable);
                nsManager.AddNamespace("nfe", "http://www.portalfiscal.inf.br/nfe");
                nsManager.AddNamespace("nfce", "http://www.portalfiscal.inf.br/nfce");
                nsManager.AddNamespace("cfe", "http://www.portalfiscal.inf.br/cfe");
                nsManager.AddNamespace("cte", "http://www.portalfiscal.inf.br/cte");

                XmlNode rootNode = xmlDoc.DocumentElement;

                // Verifique os elementos ou atributos específicos para identificar o tipo de XML
                if (rootNode.SelectSingleNode("//nfe:infNFe", nsManager) != null)
                {
                    XmlNode modNode = rootNode.SelectSingleNode("//nfe:mod", nsManager);
                    string value = modNode.InnerText;
                    if (value == "55")
                    {
                        return (EnumTypeXML)1;
                    }
                    else
                    {
                        return (EnumTypeXML)4;
                    }
                }
                else if (rootNode.SelectSingleNode("//CFe/infCFe", nsManager) != null)
                    return (EnumTypeXML)2;
                else if (rootNode.SelectSingleNode("//cte:infCte", nsManager) != null)
                    return (EnumTypeXML)3;
                else
                    return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao identificar o tipo do XML: {ex.Message}");
                return 0;
            }
        }
    }


}
