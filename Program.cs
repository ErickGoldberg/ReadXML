using System;
using System.Xml;
using System.IO;
using XMLSearch.Data;
using XMLSearch.Helper;

namespace SeuNamespace
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
                        NFE nfe = new GetNFE().ReaderNfe(doc);
                        cnpj = nfe.destCnpj;
                        dataEmi = nfe.dhEmi;
                        numeroNota = nfe.vNF;
                        break;
                    case "CFE":
                        CFE cfe = new GetCFE().ReaderCfe(doc);
                        cnpj = cfe.emitCnpj;
                        dataEmi = cfe.dhEmi;
                        numeroNota = cfe.vCFe;
                        break;
                    case "CTE":
                        CTE cte = new GetCTE().ReaderCte(doc);
                        cnpj = cte.emitCnpj;
                        dataEmi = cte.dhEmi;
                        numeroNota = cte.vCte;
                        break;
                    case "NFCe":
                        NFE nfce = new GetNFE().ReaderNfe(doc);
                        cnpj = nfce.emitCnpj;
                        dataEmi = nfce.dhEmi;
                        numeroNota = nfce.vNF;
                        break;
                }

                if (cnpj != null && numeroNota != null)
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
        }
    }
}
