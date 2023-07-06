using System.Xml;
using XMLSearch.Data;
using XMLSearch.Utils;

public class GetNFE
{
    public NFE ReaderNfe(XmlDocument doc)
    {
        var nfe = new NFE();
        try
        {
            nfe.inNfe = ProcessInfNFe(doc);
            nfe.nNF = ProcessNNF(doc);
            nfe.dhEmi = ProcessDhEmi(doc);
            nfe.emitXNome = ProcessEmitXNome(doc);
            nfe.emitCnpj = ProcessEmitCNPJ(doc);
            nfe.destXNome = ProcessDestXNome(doc);
            nfe.destCnpj = ProcessDestCNPJ(doc);
            nfe.vNF = ProcessVNF(doc);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao processar XmlDocument(Algum valor não existe): {ex.Message}");
        }
        return nfe;
    }

    public string ProcessInfNFe(XmlDocument doc)
    {
        XmlNode infNfeNode = XMLUtils.GetNode(doc, "//nfe:infNFe", "nfe", "http://www.portalfiscal.inf.br/nfe");
        if (infNfeNode != null)
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
        else
        {
            Console.WriteLine("Elemento infNFe não encontrado.");
        }
        return string.Empty;
    }

    public int ProcessNNF(XmlDocument doc)
    {
        XmlNode nNFNode = XMLUtils.GetNode(doc, "//nfe:nNF", "nfe", "http://www.portalfiscal.inf.br/nfe");
        if (nNFNode != null)
        {
            string nNFValue = nNFNode.InnerText;
            return XMLUtils.ParseValue<int>(nNFValue);
        }
        else
        {
            Console.WriteLine("Elemento nNF não encontrado.");
        }
        return 0;
    }

    public DateTime ProcessDhEmi(XmlDocument doc)
    {
        XmlNode dhEmiNode = XMLUtils.GetNode(doc, "//nfe:dhEmi", "nfe", "http://www.portalfiscal.inf.br/nfe");
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

    public string ProcessEmitCNPJ(XmlDocument doc)
    {
        XmlNode emitCNPJNode = XMLUtils.GetNode(doc, "//nfe:emit/nfe:CNPJ", "nfe", "http://www.portalfiscal.inf.br/nfe");
        return emitCNPJNode?.InnerText;
    }

    public string ProcessDestCNPJ(XmlDocument doc)
    {
        XmlNode destCNPJNode = XMLUtils.GetNode(doc, "//nfe:dest/nfe:CNPJ", "nfe", "http://www.portalfiscal.inf.br/nfe");
        return destCNPJNode?.InnerText;
    }

    public string ProcessEmitXNome(XmlDocument doc)
    {
        XmlNode emitXNomeNode = XMLUtils.GetNode(doc, "//nfe:emit/nfe:xNome", "nfe", "http://www.portalfiscal.inf.br/nfe");
        return emitXNomeNode?.InnerText;
    }

    public string ProcessDestXNome(XmlDocument doc)
    {
        XmlNode destXNomeNode = XMLUtils.GetNode(doc, "//nfe:dest/nfe:xNome", "nfe", "http://www.portalfiscal.inf.br/nfe");
        return destXNomeNode?.InnerText;
    }

    public double ProcessVNF(XmlDocument doc)
    {
        XmlNode vNFNode = XMLUtils.GetNode(doc, "//nfe:total/nfe:ICMSTot/nfe:vNF", "nfe", "http://www.portalfiscal.inf.br/nfe");
        if (vNFNode != null)
        {
            string vNFValue = vNFNode.InnerText;
            return XMLUtils.ParseValue<double>(vNFValue);
        }
        else
        {
            Console.WriteLine("Elemento vNF não encontrado.");
        }
        return 0.0;
    }
}
