using System;
using System.Xml;
using XMLSearch.Data;
using XMLSearch.Utils;

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

        public NFE ReaderNfe(XmlDocument doc)
        {
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
            try
            {
                XmlNode infNfeNode = doc.SelectSingleNode("//nfe:infNFe", GetNamespaceManager(doc));
                if (infNfeNode != null)
                {
                    try
                    {
                        string infNfeId = XMLUtils.GetAttributeValue(infNfeNode, "Id");
                        if (!string.IsNullOrEmpty(infNfeId))
                        {
                            return infNfeId.Substring(3);
                        }
                        else
                        {
                            Console.WriteLine("ID do elemento infNFe não encontrado.");
                        }
                    }
                    catch (NullReferenceException)
                    {
                        Console.WriteLine("Atributo 'Id' do elemento infNFe não encontrado.");
                    }
                }
                else
                {
                    Console.WriteLine("Elemento infNFe não encontrado.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar XmlDocument: {ex.Message}");
            }
            return string.Empty;
        }

        public int ProcessNNF(XmlDocument doc)
        {
            try
            {
                XmlNode nNFNode = doc.SelectSingleNode("//nfe:nNF", GetNamespaceManager(doc));
                if (nNFNode != null)
                {
                    string nNFValue = nNFNode.InnerText;
                    return XMLUtils.ParseValue<int>(nNFValue);
                }
                else
                {
                    Console.WriteLine("Elemento nNF não encontrado.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar XmlDocument: {ex.Message}");
            }
            return 0;
        }

        public DateTime ProcessDhEmi(XmlDocument doc)
        {
            try
            {
                XmlNode dhEmiNode = doc.SelectSingleNode("//nfe:dhEmi", GetNamespaceManager(doc));
                if (dhEmiNode != null)
                {
                    string dhEmiValue = dhEmiNode.InnerText;
                    return XMLUtils.ParseValue<DateTime>(dhEmiValue);
                }
                else
                {
                    Console.WriteLine("Elemento dhEmi não encontrado.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar XmlDocument: {ex.Message}");
            }
            return DateTime.MinValue;
        }

        public string ProcessEmitCNPJ(XmlDocument doc)
        {
            try
            {
                XmlNode emitCNPJNode = doc.SelectSingleNode("//nfe:emit/nfe:CNPJ", GetNamespaceManager(doc));
                return emitCNPJNode?.InnerText;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar XmlDocument: {ex.Message}");
            }
            return string.Empty;
        }

        public string ProcessDestCNPJ(XmlDocument doc)
        {
            try
            {
                XmlNode destCNPJNode = doc.SelectSingleNode("//nfe:dest/nfe:CNPJ", GetNamespaceManager(doc));
                return destCNPJNode?.InnerText;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar XmlDocument: {ex.Message}");
            }
            return string.Empty;
        }

        public string ProcessEmitXNome(XmlDocument doc)
        {
            try
            {
                XmlNode emitXNomeNode = doc.SelectSingleNode("//nfe:emit/nfe:xNome", GetNamespaceManager(doc));
                return emitXNomeNode?.InnerText;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar XmlDocument: {ex.Message}");
            }
            return string.Empty;
        }

        public string ProcessDestXNome(XmlDocument doc)
        {
            try
            {
                XmlNode destXNomeNode = doc.SelectSingleNode("//nfe:dest/nfe:xNome", GetNamespaceManager(doc));
                return destXNomeNode?.InnerText;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar XmlDocument: {ex.Message}");
            }
            return string.Empty;
        }

        public double ProcessVNF(XmlDocument doc)
        {
            try
            {
                XmlNode vNFNode = doc.SelectSingleNode("//nfe:total/nfe:ICMSTot/nfe:vNF", GetNamespaceManager(doc));
                if (vNFNode != null)
                {
                    string vNFValue = vNFNode.InnerText;
                    return XMLUtils.ParseValue<double>(vNFValue);
                }
                else
                {
                    Console.WriteLine("Elemento vNF não encontrado.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar XmlDocument: {ex.Message}");
            }
            return 0.0;
        }
    }
}
