

using XMLSearch.Data.Enum;

namespace XMLSearch.Data
{
    internal class CTE : IXml
    {
        public string InfCte  { get; set; }
        public int NCT { get; set; }
        public  DateTime DhEmi { get; set; }
        public string EmitCnpj { get; set; }
        public string DestCnpj { get; set; }
        public string RemCnpj { get; set; }
        public string EmitXNome { get; set; }
        public string DestXNome { get; set; }
        public string RemXNome { get; set; }
        public double VCte { get; set; }
        public EnumTypeXML TypeXml { get; set; }
        public string CnpjEmit { get; set; }
        public int NumberXml { get; set; }
        public string XmlKey { get; set; }
        public double Value { get; set; }
    }
}