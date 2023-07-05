using System;
using System.Xml;
using XMLSearch.Data;

namespace XMLSearch.Helper
{
    public class GetNFE
    {
        private XmlNamespaceManager GetNamespaceManager(XmlDocument doc)
        {
            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(doc.NameTable);
            namespaceManager.AddNamespace("nfe", "http://www.portalfiscal.inf.br/nfe");
            // Adicione outros namespaces conforme necessário

            return namespaceManager;
        }

        public NFE ReaderNfe(string filePath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);


            var nfe = new NFE();
            nfe.inNfe = ProcessInfNFe(doc);
            nfe.nNF = ProcessNNF(doc);
            nfe.dhEmi = ProcessDhEmi(doc);
            nfe.emitXNome = ProcessEmitXNome(doc);
            nfe.emitCnpj = ProcessEmitCNPJ(doc);
            nfe.destXNome = ProcessDestXNome(doc);
            nfe.destCnpj = ProcessDestCNPJ(doc);
            nfe.vNF = ProcessVNF(doc);
            return nfe;
        }

        public string ProcessInfNFe(XmlDocument doc)
        {
            XmlNode infNfeNode = doc.SelectSingleNode("//nfe:infNFe", GetNamespaceManager(doc));
            string infNfeId = infNfeNode?.Attributes["Id"]?.Value;
            return infNfeId?.Substring(3);
        }

        public int ProcessNNF(XmlDocument doc)
        {
            XmlNode nNFNode = doc.SelectSingleNode("//nfe:nNF", GetNamespaceManager(doc));
            return int.Parse(nNFNode?.InnerText);
        }

        public DateTime ProcessDhEmi(XmlDocument doc)
        {
            XmlNode dhEmiNode = doc.SelectSingleNode("//nfe:dhEmi", GetNamespaceManager(doc));
            return DateTime.Parse(dhEmiNode?.InnerText);
        }

        public string ProcessEmitCNPJ(XmlDocument doc)
        {
            XmlNode emitCNPJNode = doc.SelectSingleNode("//nfe:emit/nfe:CNPJ", GetNamespaceManager(doc));
            return emitCNPJNode?.InnerText;
        }

        public string ProcessDestCNPJ(XmlDocument doc)
        {
            XmlNode destCNPJNode = doc.SelectSingleNode("//nfe:dest/nfe:CNPJ", GetNamespaceManager(doc));
            return destCNPJNode?.InnerText;
        }

        public string ProcessEmitXNome(XmlDocument doc)
        {
            XmlNode emitXNomeNode = doc.SelectSingleNode("//nfe:emit/nfe:xNome", GetNamespaceManager(doc));
            return emitXNomeNode?.InnerText;
        }

        public string ProcessDestXNome(XmlDocument doc)
        {
            XmlNode destXNomeNode = doc.SelectSingleNode("//nfe:dest/nfe:xNome", GetNamespaceManager(doc));
            return destXNomeNode?.InnerText;
        }

        public double ProcessVNF(XmlDocument doc)
        {
            XmlNode vNFNode = doc.SelectSingleNode("//nfe:total/nfe:ICMSTot/nfe:vNF", GetNamespaceManager(doc));
            return double.Parse(vNFNode?.InnerText);
        }
    }
}
