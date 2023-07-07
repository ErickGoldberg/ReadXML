
using XMLSearch.Data.Enum;

namespace XMLSearch.Data
{
    public class NFE : IXml
    {
        public string InNfe { get; set; }
        public int NNF { get; set; }
        public DateTime DhEmi { get; set; }
        public string EmitCnpj { get; set; }
        public string DestCnpj { get; set; }
        public string EmitXNome { get; set; }
        public string DestXNome { get; set; }
        public double VNF { get; set; }
        public EnumTypeXML TypeXml { get; set; }
        public string CnpjEmit { get; set; }
        public int NumberXml { get; set; }
        public string XmlKey { get; set; }
        public double Value { get; set; }
    }
}