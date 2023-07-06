using System;
using System.Xml;

namespace XMLSearch.Helper
{
    public static class XmlTypeIdentifier
    {
        public static string IdentifyXmlType(string xmlFilePath)
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
                        return "NFE";
                    }
                    else
                    {
                        return "NFCe";
                    }
                }
                else if (rootNode.SelectSingleNode("//CFe/infCFe", nsManager) != null)
                    return "CFE";
                else if (rootNode.SelectSingleNode("//cte:infCte", nsManager) != null)
                    return "CTE";
                else
                    return "Desconhecido";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao identificar o tipo do XML: {ex.Message}");
                return "Desconhecido";
            }
        }
    }


}
