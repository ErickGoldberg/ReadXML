using System;
using System.Globalization;
using System.Xml;
using XMLSearch.Data;

namespace XMLSearch.Helper
{
    public class GetCFE
    {
        private XmlNamespaceManager GetNamespaceManager(XmlDocument doc3)
        {
            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(doc3.NameTable);
            namespaceManager.AddNamespace("cfe", "http://www.portalfiscal.inf.br/cfe");
            // Adicione outros namespaces conforme necessário

            return namespaceManager;
        }

        private XmlNode GetNode(XmlDocument doc3, string xPath)
        {
            XmlNamespaceManager namespaceManager = GetNamespaceManager(doc3);
            return doc3.SelectSingleNode(xPath, namespaceManager);
        }

        private string GetAttributeValue(XmlNode node, string attributeName)
        {
            return node?.Attributes?[attributeName]?.Value;
        }

        private T ParseValue<T>(string value)
        {
            if (string.IsNullOrEmpty(value))
                return default;

            return (T)Convert.ChangeType(value, typeof(T));
        }

        public CFE ReaderCfe(XmlDocument doc3)
        {
            var cfe = new CFE();
            cfe.infCfe = ProcessinfCfe(doc3);
            cfe.cNF = ProcessCNF(doc3);
            cfe.dhEmi = ProcessDhEmi(doc3);
            cfe.emitXNome = ProcessEmitXNome(doc3);
            cfe.emitCnpj = ProcessEmitCNPJ(doc3);
            cfe.emitIE = ProcessEmitIE(doc3);
            cfe.vCFe = ProcessVNF(doc3);
            return cfe;
        }

        public string ProcessinfCfe(XmlDocument doc3)
        {
            try
            {
                XmlNode infCfeNode = GetNode(doc3, "//infCFe");
                if (infCfeNode != null)
                {
                    try
                    {
                        string infCfeId = GetAttributeValue(infCfeNode, "Id");
                        if (!string.IsNullOrEmpty(infCfeId))
                        {
                            return infCfeId.Substring(3);
                        }
                        else
                        {
                            Console.WriteLine("ID do elemento infCFe não encontrado.");
                        }
                    }
                    catch (NullReferenceException)
                    {
                        Console.WriteLine("Atributo 'Id' do elemento infCFe não encontrado.");
                    }
                }
                else
                { 
                    Console.WriteLine("Elemento infCFe não encontrado.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar XmlDocument: {ex.Message}");
            }
            return string.Empty;
        }

        public int ProcessCNF(XmlDocument doc3)
        {
            try
            {
                XmlNode cNFNode = GetNode(doc3, "//infCFe/ide/cNF");
                if (cNFNode != null)
                {
                    string cNFValue = cNFNode.InnerText;
                    return ParseValue<int>(cNFValue);
                }
                else
                {
                    Console.WriteLine("Elemento cNF não encontrado.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar XmlDocument: {ex.Message}");
            }
            return 0;
        }

        public string ProcessDhEmi(XmlDocument doc3)
        {
            try
            {
                XmlNode dhEmiNode = GetNode(doc3, "//infCFe/ide/dEmi");
                if (dhEmiNode != null)
                {
                    string dhEmiValue = dhEmiNode.InnerText;
                    DateTime dhEmiDateTime;

                    if (DateTime.TryParseExact(dhEmiValue, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dhEmiDateTime))
                    {
                        return dhEmiDateTime.ToString("yyyy-MM-dd HH:mm:ss");
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
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar XmlDocument: {ex.Message}");
            }
            return string.Empty;
        }

        public string ProcessEmitCNPJ(XmlDocument doc3)
        {
            try
            {
                XmlNode emitCNPJNode = GetNode(doc3, "//infCFe/emit/CNPJ");
                return emitCNPJNode?.InnerText;
            }
            catch (NullReferenceException)
            {
                return string.Empty;
            }
            catch (Exception ex)
            {
                return $"Erro ao processar o valor da tag emit/CNPJ: {ex.Message}";
            }
        }

        public string ProcessEmitXNome(XmlDocument doc3)
        {
            try
            {
                XmlNode emitXNomeNode = GetNode(doc3, "//infCFe/emit/xNome");
                return emitXNomeNode?.InnerText;
            }
            catch (NullReferenceException)
            {
                return string.Empty;
            }
            catch (Exception ex)
            {
                return $"Erro ao processar o valor da tag emit/xNome: {ex.Message}";
            }
        }

        public string ProcessEmitIE(XmlDocument doc3)
        {
            try
            {
                XmlNode emitIENode = GetNode(doc3, "//infCFe/emit/IE");
                return emitIENode?.InnerText;
            }
            catch (NullReferenceException)
            {
                return string.Empty;
            }
            catch (Exception ex)
            {
                return $"Erro ao processar o valor da tag emit/IE: {ex.Message}";
            }
        }

        public double ProcessVNF(XmlDocument doc3)
        {
            try
            {
                XmlNode vCFeNode = GetNode(doc3, "//infCFe/total/vCFe");
                if (vCFeNode != null)
                {
                    string vCFeValue = vCFeNode.InnerText;
                    double parsedValue;
                    if (double.TryParse(vCFeValue, out parsedValue))
                    {
                        return parsedValue;
                    }
                    else
                    {
                        Console.WriteLine("Valor não pode ser convertido para double.");
                    }
                }
                else
                {
                    Console.WriteLine("Elemento vCFe não encontrado.");
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
