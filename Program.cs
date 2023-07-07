using System;
using System.Xml;
using System.IO;
using XMLSearch.Data;
using XMLSearch.Helper;
using XMLSearch.Data.Enum;
using System.Collections.Generic;
using ClosedXML.Excel;

namespace XMLSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            string pastaOrigem = @"C:\Users\Dev\Desktop\XMLs";
            string pastaDestinoBase = @"C:\Users\Dev\Desktop\XMLsOrganizados";

            string[] arquivosXml = Directory.GetFiles(pastaOrigem, "*.xml");

            List<IXml> listXml = new List<IXml>();

            foreach (string arquivoXml in arquivosXml)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(arquivoXml);

                EnumTypeXML tipoArquivo = XmlTypeIdentifier.IdentifyXmlType(arquivoXml);
                Console.WriteLine($"Arquivo: {arquivoXml} | Tipo: {tipoArquivo}");

                string cnpj = null;
                DateTime dataEmi = DateTime.MinValue;
                double numeroNota = 0.0;

                #region nfe

                var nfe = new GetNFE().ReaderNfe(doc);
                string emitXNome = nfe.EmitXNome;
                string destCnpj = nfe.DestCnpj;
                string DestXNome = nfe.DestXNome;
                string chaveXml = nfe.InNfe;

                #endregion

                #region cfe

                CFE cfe = new GetCFE().ReaderCfe(doc);
                emitXNome = cfe.EmitXNome;
                destCnpj = cfe.DestCnpj;
                DestXNome = cfe.DestXNome;
                chaveXml = cfe.InfCfe;

                #endregion

                #region cte

                CTE cte = new GetCTE().ReaderCte(doc);
                emitXNome = cte.EmitXNome;
                destCnpj = cte.DestCnpj;
                DestXNome = cte.DestXNome;
                chaveXml = cte.InfCte;

                #endregion

                #region nfce

                NFE nfce = new GetNFE().ReaderNfe(doc);
                emitXNome = nfce.EmitXNome;
                destCnpj = nfce.DestCnpj;
                DestXNome = nfce.DestXNome;
                chaveXml = nfce.InNfe;

                #endregion

                switch (tipoArquivo)
                {
                    case EnumTypeXML.NFE:
                        cnpj = nfe.DestCnpj;
                        dataEmi = nfe.DhEmi;
                        numeroNota = nfe.VNF;
                        break;
                    case EnumTypeXML.CFE:
                        cnpj = cfe.EmitCnpj;
                        dataEmi = cfe.DhEmi;
                        numeroNota = cfe.VCFe;
                        break;
                    case EnumTypeXML.CTE:
                        cnpj = cte.EmitCnpj;
                        dataEmi = cte.DhEmi;
                        numeroNota = cte.VCte;
                        break;
                    case EnumTypeXML.NFCe:
                        cnpj = nfce.EmitCnpj;
                        dataEmi = nfce.DhEmi;
                        numeroNota = nfce.VNF;
                        break;
                }
                if (cnpj != null && numeroNota != 0)
                {
                    string pastaDestino = Path.Combine(pastaDestinoBase, tipoArquivo.ToString(), dataEmi.Year.ToString(), dataEmi.Month.ToString(), cnpj);
                    Directory.CreateDirectory(pastaDestino);
                    string destinoArquivo = Path.Combine(pastaDestino, $"{numeroNota}.xml");
                    File.Copy(arquivoXml, destinoArquivo, overwrite: true);
                    Console.WriteLine($"Arquivo movido para: {destinoArquivo}");


                    XmlData xmlObject = new XmlData
                    {
                        TypeXml = tipoArquivo.ToString(),
                        DhEmi = dataEmi,
                        CnpjEmit = cnpj,
                        EmitXNome = emitXNome,
                        DestCnpj = destCnpj,
                        DestXNome = DestXNome,
                        NumberXml = numeroNota,
                        XmlKey = chaveXml,
                        Value = numeroNota
                    };
                    listXml.Add(xmlObject);
                }
                else
                {
                    Console.WriteLine($"CNPJ ou número da nota é nulo para o arquivo: {arquivoXml}");
                }
            }

            // Exportar para o arquivo Excel
            string excelFilePath = Path.Combine(pastaDestinoBase, "dados.xlsx");
            ExcelExporter.ExportToExcel(listXml, excelFilePath, EnumTypeXML.NFE);
        }
    }
}
