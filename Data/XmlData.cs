using XMLSearch.Data;
using XMLSearch.Data.Enum;

public class XmlData : IXml
{
    public string TypeXml { get; set; }
    public DateTime DhEmi { get; set; }
    public string CnpjEmit { get; set; }
    public string EmitXNome { get; set; }
    public string DestCnpj { get; set; }
    public string DestXNome { get; set; }
    public double NumberXml { get; set; }
    public string XmlKey { get; set; }
    public double Value { get; set; }
    EnumTypeXML IXml.TypeXml { get; set; }
    int IXml.NumberXml { get; set; }
}
