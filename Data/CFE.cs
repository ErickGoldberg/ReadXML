
using XMLSearch.Data.Enum;

namespace XMLSearch.Data
{
    public class CFE : IXml
    {
        public string InfCfe { get; set; }
        public int CNF { get; set; }
        public DateTime DhEmi { get; set; }
        public string EmitCnpj { get; set; }
        public string EmitXNome { get; set; }
        public string EmitIE { get; set; }
        public double VCFe { get; set; }
        public EnumTypeXML TypeXml { get; set; }
        public string CnpjEmit { get; set; }
        public string DestCnpj { get; set; }
        public string DestXNome { get; set; }
        public int NumberXml { get; set; }
        public string XmlKey { get; set; }
        public double Value { get; set; }
    }
}
