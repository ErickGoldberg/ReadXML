using System;
using System.Globalization;
using System.Xml;
using XMLSearch.Data;
using XMLSearch.Utils;

namespace XMLSearch.Helper
{
    public class GetCFE
    {
        public CFE ReaderCfe(XmlDocument doc3)
        {
            try
            {
                var cfe = new CFE();
                cfe.InfCfe = ProcessinfCfe(doc3);
                cfe.CNF = ProcessCNF(doc3);
                cfe.DhEmi = ProcessDhEmi(doc3);
                cfe.EmitXNome = ProcessEmitXNome(doc3);
                cfe.EmitCnpj = ProcessEmitCNPJ(doc3);
                cfe.EmitIE = ProcessEmitIE(doc3);
                cfe.VCFe = ProcessVNF(doc3);
                return cfe;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex.Message}");
                return null;
            }
        }

        public string ProcessinfCfe(XmlDocument doc3)
        {
            XmlNode infCfeNode = XMLUtils.GetNode(doc3, "//infCFe", "cfe", "http://www.portalfiscal.inf.br/cfe");
            if (infCfeNode != null)
            {
                string infCfeId = XMLUtils.GetAttributeValue(infCfeNode, "Id");
                if (!string.IsNullOrEmpty(infCfeId))
                {
                    return infCfeId.Substring(3);
                }
                else
                {
                    Console.WriteLine("ID do elemento infCFe não encontrado.");
                }
            }
            else
            {
                Console.WriteLine("Elemento infCFe não encontrado.");
            }
            return string.Empty;
        }

        public int ProcessCNF(XmlDocument doc3)
        {
            XmlNode cNFNode = XMLUtils.GetNode(doc3, "//infCFe/ide/cNF", "cfe", "http://www.portalfiscal.inf.br/cfe");
            if (cNFNode != null)
            {
                string cNFValue = cNFNode.InnerText;
                return XMLUtils.ParseValue<int>(cNFValue);
            }
            else
            {
                Console.WriteLine("Elemento cNF não encontrado.");
            }
            return 0;
        }

        public DateTime ProcessDhEmi(XmlDocument doc3)
        {
            XmlNode dhEmiNode = XMLUtils.GetNode(doc3, "//infCFe/ide/dEmi", "cfe", "http://www.portalfiscal.inf.br/cfe");
            if (dhEmiNode != null)
            {
                string dhEmiValue = dhEmiNode.InnerText;
                DateTime dhEmiDateTime;

                if (DateTime.TryParseExact(dhEmiValue, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dhEmiDateTime))
                {
                    return dhEmiDateTime;
                }
                else
                {
                    Console.WriteLine("Valor não pode ser convertido para DateTime.");
                }
            }
            else
            {
                Console.WriteLine("Elemento dhEmi não encontrado.");
            }
            return DateTime.MinValue;
        }

        public string ProcessEmitCNPJ(XmlDocument doc3)
        {
            XmlNode emitCNPJNode = XMLUtils.GetNode(doc3, "//infCFe/emit/CNPJ", "cfe", "http://www.portalfiscal.inf.br/cfe");
            return emitCNPJNode?.InnerText ?? string.Empty;
        }

        public string ProcessEmitXNome(XmlDocument doc3)
        {
            XmlNode emitXNomeNode = XMLUtils.GetNode(doc3, "//infCFe/emit/xNome", "cfe", "http://www.portalfiscal.inf.br/cfe");
            return emitXNomeNode?.InnerText ?? string.Empty;
        }

        public string ProcessEmitIE(XmlDocument doc3)
        {
            XmlNode emitIENode = XMLUtils.GetNode(doc3, "//infCFe/emit/IE", "cfe", "http://www.portalfiscal.inf.br/cfe");
            return emitIENode?.InnerText ?? string.Empty;
        }

        public double ProcessVNF(XmlDocument doc3)
        {
            XmlNode vCFeNode = XMLUtils.GetNode(doc3, "//infCFe/total/vCFe", "cfe", "http://www.portalfiscal.inf.br/cfe");
            if (vCFeNode != null)
            {
                string vCFeValue = vCFeNode.InnerText;
                return XMLUtils.ParseValue<double>(vCFeValue);
            }
            else
            {
                Console.WriteLine("Elemento vCFe não encontrado.");
            }
            return 0.0;
        }
    }
}
