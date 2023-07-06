using System;
using System.Xml;
using System.Xml.XPath;
using XMLSearch.Data;
using XMLSearch.Utils;

namespace XMLSearch.Helper
{
    internal class GetCTE
    {
        public CTE ReaderCte(XmlDocument doc2)
        {
            var cte = new CTE();
            try
            {
                cte.infCte = ProcessInfCte(doc2);
                cte.nCT = ProcessNCT(doc2);
                cte.dhEmi = ProcessDhEmi(doc2);
                cte.emitXNome = ProcessEmitXNome(doc2);
                cte.emitCnpj = ProcessEmitCNPJ(doc2);
                cte.remXNome = ProcessRemXNome(doc2);
                cte.remCnpj = ProcessRemCNPJ(doc2);
                cte.destXNome = ProcessDestXNome(doc2);
                cte.destCnpj = ProcessDestCNPJ(doc2);
                cte.vCte = ProcessvCte(doc2);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"O documento está faltando algum valor: {ex.Message}");
                return null;
            }
            return cte;
        }

        public string ProcessInfCte(XmlDocument doc2)
        {
            XmlNode InfCteNode = XMLUtils.GetNode(doc2, "//cte:infCte", "cte", "http://www.portalfiscal.inf.br/cte");
            if (InfCteNode != null)
            {
                string InfCte = XMLUtils.GetAttributeValue(InfCteNode, "Id");
                if (!string.IsNullOrEmpty(InfCte))
                {
                    return InfCte.Substring(3);
                }
                else
                {
                    Console.WriteLine("ID do elemento infCte não encontrado.");
                }
            }
            else
            {
                Console.WriteLine("Elemento infCte não encontrado.");
            }
            return string.Empty;
        }

        public int ProcessNCT(XmlDocument doc2)
        {
            XmlNode nCTNode = XMLUtils.GetNode(doc2, "//cte:nCT", "cte", "http://www.portalfiscal.inf.br/cte");
            if (nCTNode != null)
            {
                string nCTValue = nCTNode.InnerText;
                return XMLUtils.ParseValue<int>(nCTValue);
            }
            else
            {
                Console.WriteLine("Elemento nCT não encontrado.");
            }
            return 0;
        }

        public DateTime ProcessDhEmi(XmlDocument doc2)
        {
            XmlNode dhEmiNode = XMLUtils.GetNode(doc2, "//cte:dhEmi", "cte", "http://www.portalfiscal.inf.br/cte");
            if (dhEmiNode != null)
            {
                string dhEmiValue = dhEmiNode.InnerText;
                return XMLUtils.ParseValue<DateTime>(dhEmiValue);
            }
            else
            {
                Console.WriteLine("Elemento dhEmi não encontrado.");
            }
            return DateTime.MinValue;
        }

        public string ProcessEmitCNPJ(XmlDocument doc2)
        {
            XmlNode emitCNPJNode = XMLUtils.GetNode(doc2, "//cte:emit/cte:CNPJ", "cte", "http://www.portalfiscal.inf.br/cte");
            if (emitCNPJNode != null)
            {
                return emitCNPJNode.InnerText ?? string.Empty;
            }
            else
            {
                Console.WriteLine("Elemento emitCNPJ não encontrado.");
            }
            return string.Empty;
        }

        public string ProcessDestCNPJ(XmlDocument doc2)
        {
            XmlNode destCNPJNode = XMLUtils.GetNode(doc2, "//cte:dest/cte:CNPJ", "cte", "http://www.portalfiscal.inf.br/cte");
            if (destCNPJNode != null)
            {
                return destCNPJNode.InnerText ?? string.Empty;
            }
            else
            {
                Console.WriteLine("Elemento destCNPJ não encontrado.");
            }
            return string.Empty;
        }

        public string ProcessRemCNPJ(XmlDocument doc2)
        {
            XmlNode remCNPJNode = XMLUtils.GetNode(doc2, "//cte:rem/cte:CNPJ", "cte", "http://www.portalfiscal.inf.br/cte");
            if (remCNPJNode != null)
            {
                return remCNPJNode.InnerText ?? string.Empty;
            }
            else
            {
                Console.WriteLine("Elemento remCNPJ não encontrado.");
            }
            return string.Empty;
        }

        public string ProcessEmitXNome(XmlDocument doc2)
        {
            XmlNode emitXNomeNode = XMLUtils.GetNode(doc2, "//cte:emit/cte:xNome", "cte", "http://www.portalfiscal.inf.br/cte");
            if (emitXNomeNode != null)
            {
                return emitXNomeNode.InnerText ?? string.Empty;
            }
            else
            {
                Console.WriteLine("Elemento emitXNome não encontrado.");
            }
            return string.Empty;
        }

        public string ProcessDestXNome(XmlDocument doc2)
        {
            XmlNode destXNomeNode = XMLUtils.GetNode(doc2, "//cte:dest/cte:xNome", "cte", "http://www.portalfiscal.inf.br/cte");
            if (destXNomeNode != null)
            {
                return destXNomeNode.InnerText ?? string.Empty;
            }
            else
            {
                Console.WriteLine("Elemento destXNome não encontrado.");
            }
            return string.Empty;
        }

        public string ProcessRemXNome(XmlDocument doc2)
        {
            XmlNode remXNomeNode = XMLUtils.GetNode(doc2, "//cte:rem/cte:xNome", "cte", "http://www.portalfiscal.inf.br/cte");
            if (remXNomeNode != null)
            {
                return remXNomeNode.InnerText ?? string.Empty;
            }
            else
            {
                Console.WriteLine("Elemento remXNome não encontrado.");
            }
            return string.Empty;
        }

        public double ProcessvCte(XmlDocument doc2)
        {
            XmlNode vNFNode = XMLUtils.GetNode(doc2, "//cte:infCTeNorm/cte:infCarga/cte:vCarga", "cte", "http://www.portalfiscal.inf.br/cte");
            if (vNFNode != null)
            {
                string vNFValue = vNFNode.InnerText;
                return XMLUtils.ParseValue<double>(vNFValue);
            }
            else
            {
                Console.WriteLine("Elemento vCarga não encontrado.");
            }
            return 0.0;
        }
    }
}
