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

            foreach (string arquivoXml in arquivosXml)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(arquivoXml);

                string tipoArquivo = XmlTypeIdentifier.IdentifyXmlType(arquivoXml);
                Console.WriteLine($"Arquivo: {arquivoXml} | Tipo: {tipoArquivo}");

                string cnpj = null;
                DateTime dataEmi = DateTime.MinValue;
                double numeroNota = 0.0;

                switch (tipoArquivo)
                {
                    case "NFE":
                        var nfe = new GetNFE().ReaderNfe(doc);
                        cnpj = nfe.DestCnpj;
                        dataEmi = nfe.DhEmi;
                        numeroNota = nfe.VNF;
                        break;
                    case "CFE":
                        CFE cfe = new GetCFE().ReaderCfe(doc);
                        cnpj = cfe.EmitCnpj;
                        dataEmi = cfe.DhEmi;
                        numeroNota = cfe.VCFe;
                        break;
                    case "CTE":
                        CTE cte = new GetCTE().ReaderCte(doc);
                        cnpj = cte.EmitCnpj;
                        dataEmi = cte.DhEmi;
                        numeroNota = cte.VCte;
                        break;
                    case "NFCe":
                        NFE nfce = new GetNFE().ReaderNfe(doc);
                        cnpj = nfce.EmitCnpj;
                        dataEmi = nfce.DhEmi;
                        numeroNota = nfce.VNF;
                        break;
                }
                if (cnpj != null && numeroNota != 0)
                {
                    string pastaDestino = Path.Combine(pastaDestinoBase, tipoArquivo, dataEmi.Year.ToString(), dataEmi.Month.ToString(), cnpj);
                    Directory.CreateDirectory(pastaDestino);
                    string destinoArquivo = Path.Combine(pastaDestino, $"{numeroNota}.xml");
                    File.Copy(arquivoXml, destinoArquivo, overwrite: true);
                    Console.WriteLine($"Arquivo movido para: {destinoArquivo}");
                }
                else
                {
                    Console.WriteLine($"CNPJ ou número da nota é nulo para o arquivo: {arquivoXml}");
                }
            }

            List<IXml> listXml = new List<IXml>();

            

            // Exportar para o arquivo Excel
            string excelFilePath = Path.Combine(pastaDestinoBase, "dados.xlsx");
            ExportToExcel(listXml, excelFilePath);
        }

        static void ExportToExcel(List<IXml> listXml, string filePath)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("XML data");

                // Escreve os cabeçalhos das colunas
                worksheet.Cell(1, 1).Value = "Tipo XML";
                worksheet.Cell(1, 2).Value = "Data Emissão";
                worksheet.Cell(1, 3).Value = "CNPJ Emitente";
                worksheet.Cell(1, 4).Value = "Nome Emitente";
                worksheet.Cell(1, 5).Value = "CNPJ Destinatário";
                worksheet.Cell(1, 6).Value = "Nome Destinatário";
                worksheet.Cell(1, 7).Value = "Número XML";
                worksheet.Cell(1, 8).Value = "Chave XML";
                worksheet.Cell(1, 9).Value = "Valor";

                // Escreve os dados dos objetos IXml nas células
                int row = 2;
                foreach (IXml xml in listXml)
                {
                    worksheet.Cell(row, 1).Value = xml.TypeXml.ToString();
                    worksheet.Cell(row, 2).Value = xml.DhEmi.ToString();
                    worksheet.Cell(row, 3).Value = xml.CnpjEmit;
                    worksheet.Cell(row, 4).Value = xml.EmitXNome;
                    worksheet.Cell(row, 5).Value = xml.DestCnpj;
                    worksheet.Cell(row, 6).Value = xml.DestXNome;
                    worksheet.Cell(row, 7).Value = xml.NumberXml.ToString();
                    worksheet.Cell(row, 8).Value = xml.XmlKey;
                    worksheet.Cell(row, 9).Value = xml.Value.ToString();

                    row++;
                }

                // Salva o arquivo Excel
                workbook.SaveAs(filePath);
            }
        }
    }
}
