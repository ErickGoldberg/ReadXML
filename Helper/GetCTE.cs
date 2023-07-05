using System;
using System.Xml;
using System.Xml.XPath;
using XMLSearch.Data;
using XMLSearch.Utils;

namespace XMLSearch.Helper
{
    internal class GetCTE
    {
        private XmlNamespaceManager GetNamespaceManager(XmlDocument doc)
        {
            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(doc.NameTable);
            namespaceManager.AddNamespace("cte", "http://www.portalfiscal.inf.br/cte");
            // Adicione outros namespaces conforme necessário

            return namespaceManager;
        }

        public CTE ReaderCte(XmlDocument doc2)
        {
            var cte = new CTE();
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
            return cte;
        }

        public string ProcessInfCte(XmlDocument doc2)
        {
            try
            {
                XmlNode InfCteNode = doc2.SelectSingleNode("//cte:infCte", GetNamespaceManager(doc2));
                if (InfCteNode != null)
                {
                    try
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
                    catch (NullReferenceException)
                    {
                        Console.WriteLine("Atributo 'Id' do elemento infCte não encontrado.");
                    }
                }
                else
                {
                    Console.WriteLine("Elemento infCte não encontrado.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar XmlDocument: {ex.Message}");
            }
            return string.Empty;
        }

        public int ProcessNCT(XmlDocument doc2)
        {
            try
            {
                XmlNode nCTNode = doc2.SelectSingleNode("//cte:nCT", GetNamespaceManager(doc2));
                if (nCTNode != null)
                {
                    string nCTValue = nCTNode.InnerText;
                    return XMLUtils.ParseValue<int>(nCTValue);
                }
                else
                {
                    Console.WriteLine("Elemento nCT não encontrado.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar XmlDocument: {ex.Message}");
            }
            return 0;
        }

        public DateTime ProcessDhEmi(XmlDocument doc2)
        {
            try
            {
                XmlNode dhEmiNode = doc2.SelectSingleNode("//cte:dhEmi", GetNamespaceManager(doc2));
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


        public string ProcessEmitCNPJ(XmlDocument doc2)
        {
            try
            {
                XmlNode emitCNPJNode = doc2.SelectSingleNode("//cte:emit/cte:CNPJ", GetNamespaceManager(doc2));
                return emitCNPJNode?.InnerText;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar XmlDocument: {ex.Message}");
            }
            return string.Empty;
        }

        public string ProcessDestCNPJ(XmlDocument doc2)
        {
            try
            {
                XmlNode destCNPJNode = doc2.SelectSingleNode("//cte:dest/cte:CNPJ", GetNamespaceManager(doc2));
                return destCNPJNode?.InnerText;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar XmlDocument: {ex.Message}");
            }
            return string.Empty;
        }

        public string ProcessRemCNPJ(XmlDocument doc2)
        {
            try
            {
                XmlNode remCNPJNode = doc2.SelectSingleNode("//cte:rem/cte:CNPJ", GetNamespaceManager(doc2));
                return remCNPJNode?.InnerText;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar XmlDocument: {ex.Message}");
            }
            return string.Empty;
        }

        public string ProcessEmitXNome(XmlDocument doc2)
        {
            try
            {
                XmlNode emitXNomeNode = doc2.SelectSingleNode("//cte:emit/cte:xNome", GetNamespaceManager(doc2));
                return emitXNomeNode?.InnerText;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar XmlDocument: {ex.Message}");
            }
            return string.Empty;
        }

        public string ProcessDestXNome(XmlDocument doc2)
        {
            try
            {
                XmlNode destXNomeNode = doc2.SelectSingleNode("//cte:dest/cte:xNome", GetNamespaceManager(doc2));
                return destXNomeNode?.InnerText;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar XmlDocument: {ex.Message}");
            }
            return string.Empty;
        }

        public string ProcessRemXNome(XmlDocument doc2)
        {
            try
            {
                XmlNode remXNomeNode = doc2.SelectSingleNode("//cte:rem/cte:xNome", GetNamespaceManager(doc2));
                return remXNomeNode?.InnerText;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar XmlDocument: {ex.Message}");
            }
            return string.Empty;
        }

        public double ProcessvCte(XmlDocument doc2)
        {
            try
            {
                XmlNode vNFNode = doc2.SelectSingleNode("//cte:infCTeNorm/cte:infCarga/cte:vCarga", GetNamespaceManager(doc2));
                if (vNFNode != null)
                {
                    string vNFValue = vNFNode.InnerText;
                    return XMLUtils.ParseValue<double>(vNFValue);
                }
                else
                {
                    Console.WriteLine("Elemento vCarga não encontrado.");
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