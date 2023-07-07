using XMLSearch.Data.Enum;

namespace XMLSearch.Data
{
    public interface IXml
    {
        public EnumTypeXML TypeXml { get; set; }

        public DateTime DhEmi { get; set; }

        public string CnpjEmit { get; set; }

        public string EmitXNome { get; set; }

        public string DestCnpj { get; set; }

        public string DestXNome { get; set; }

        public int NumberXml { get; set; }

        public string XmlKey { get; set; }

        public double Value { get; set; }
    }
}