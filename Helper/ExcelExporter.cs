using ClosedXML.Excel;
using System.Collections.Generic;
using XMLSearch.Data;
using XMLSearch.Data.Enum;

namespace XMLSearch.Helper
{
    public static class ExcelExporter
    {
        public static void ExportToExcel(List<IXml> listXml, string filePath)
        {
            EnumTypeXML tipoArquivo = 0;
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
                    string cnpj;
                    if (xml.DestXNome != null)
                    {
                        cnpj = xml.DestCnpj;
                    }
                    else
                    {
                        cnpj = "Não possui cnpj";
                    }
                    {
                    }
                    if (tipoArquivo != EnumTypeXML.CTE)
                    {
                        worksheet.Cell(row, 1).Value = xml.TypeXml.ToString();
                        worksheet.Cell(row, 2).Value = xml.DhEmi.ToString();
                        worksheet.Cell(row, 3).Value = xml.CnpjEmit;
                        worksheet.Cell(row, 4).Value = xml.EmitXNome;
                        worksheet.Cell(row, 5).Value = cnpj;
                        worksheet.Cell(row, 6).Value = xml.DestXNome;
                        worksheet.Cell(row, 7).Value = xml.Value.ToString();
                        worksheet.Cell(row, 8).Value = xml.XmlKey;
                        worksheet.Cell(row, 9).Value = xml.Value.ToString();
                    }
                    else
                    {
                        worksheet.Cell(row, 1).Value = xml.TypeXml.ToString();
                        worksheet.Cell(row, 2).Value = xml.DhEmi.ToString();
                        worksheet.Cell(row, 3).Value = xml.CnpjEmit;
                        worksheet.Cell(row, 4).Value = xml.EmitXNome;
                        worksheet.Cell(row, 5).Value = cnpj;
                        worksheet.Cell(row, 6).Value = xml.DestXNome;
                        worksheet.Cell(row, 7).Value = xml.Value.ToString();
                        worksheet.Cell(row, 8).Value = xml.XmlKey;
                        worksheet.Cell(row, 9).Value = xml.Value.ToString();
                    }
                    row++;
                }

                // Salva o arquivo Excel
                workbook.SaveAs(filePath);
            }
        }
    }
}
