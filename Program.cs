
using System.Xml;
using System.Xml.XPath;

class Program
{
    static void Main()
    {
        string filePath = @"C:\Users\Dev\Downloads\31230760409075009613550010020302676872200497.xml";
        XPathDocument xmlDoc = new XPathDocument(filePath);
        XPathNavigator navigator = xmlDoc.CreateNavigator();
        // Cria uma instância da classe XPathNavigator que permite navegar pelo documento XML.
        XmlNamespaceManager namespaceManager = new XmlNamespaceManager(navigator.NameTable);
        // Gerenciar os namespaces
        namespaceManager.AddNamespace("nfe", "http://www.portalfiscal.inf.br/nfe");
        // nfe vai ser o gerenciador dos namespaces

        ProcessInfNFe(navigator, namespaceManager);
        ProcessNNF(navigator, namespaceManager);
        ProcessDhEmi(navigator, namespaceManager);
        ProcessXNome(navigator, namespaceManager);
        ProcessCNPJ(navigator, namespaceManager);
        ProcessVNF(navigator, namespaceManager);
    }

    static void ProcessInfNFe(XPathNavigator navigator, XmlNamespaceManager namespaceManager)
    {
        string infNfeId = navigator.SelectSingleNode("//nfe:infNFe", namespaceManager).GetAttribute("Id", "");
        Console.WriteLine("infNfe:Id: " + infNfeId.Substring(3));
    }

    static void ProcessNNF(XPathNavigator navigator, XmlNamespaceManager namespaceManager)
    {
        string nNF = navigator.SelectSingleNode("//nfe:nNF", namespaceManager).Value;
        Console.WriteLine("nNF: " + nNF);
    }

    static void ProcessDhEmi(XPathNavigator navigator, XmlNamespaceManager namespaceManager)
    {
        string dhEmi = navigator.SelectSingleNode("//nfe:dhEmi", namespaceManager).Value;
        Console.WriteLine("dhEmi: " + dhEmi);
    }

    static void ProcessCNPJ(XPathNavigator navigator, XmlNamespaceManager namespaceManager)
    {
        XPathNodeIterator iterator = navigator.Select("//nfe:emit/nfe:CNPJ | //nfe:dest/nfe:CNPJ", namespaceManager);

        while (iterator.MoveNext())
        {
            string value = iterator.Current.Value;
            Console.WriteLine("CNPJ: " + value);
        }
    }

    static void ProcessXNome(XPathNavigator navigator, XmlNamespaceManager namespaceManager)
    {
        XPathNodeIterator iterator = navigator.Select("//nfe:emit/nfe:xNome | //nfe:dest/nfe:xNome", namespaceManager);

        while (iterator.MoveNext())
        {
            string value = iterator.Current.Value;
            Console.WriteLine("xNome: " + value);
        }
    }

    static void ProcessVNF(XPathNavigator navigator, XmlNamespaceManager namespaceManager)
    {
        string vNF = navigator.SelectSingleNode("//nfe:total/nfe:ICMSTot/nfe:vNF", namespaceManager).Value;
        Console.WriteLine("total/ICMSTot/vNF: " + vNF);
    }
}
