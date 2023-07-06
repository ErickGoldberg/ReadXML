using System.Xml;
using XMLSearch.Data;
using XMLSearch.Helper;

namespace SeuNamespace
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlDocument nfeDoc = new XmlDocument();
            nfeDoc.Load(@"C:\Users\Dev\Downloads\31230760409075009613550010020302676872200497.xml");
            GetNFE getNFE = new GetNFE();
            NFE nfe = getNFE.ReaderNfe(nfeDoc);

            Console.WriteLine("********************************************************");
            Console.WriteLine("Aqui estão os dados da Nfe: \n");
            Console.WriteLine("infNfe:Id: " + nfe.inNfe);
            Console.WriteLine("nNF: " + nfe.nNF);
            Console.WriteLine("dhEmi: " + nfe.dhEmi);
            Console.WriteLine("emitXNome: " + nfe.emitXNome);
            Console.WriteLine("emitCnpj: " + nfe.emitCnpj);
            Console.WriteLine("destXNome: " + nfe.destXNome);
            Console.WriteLine("destCnpj: " + nfe.destCnpj);
            Console.WriteLine("vNF: " + nfe.vNF);

            XmlDocument cteDoc = new XmlDocument();
            cteDoc.Load(@"C:/Users/Dev/Downloads/35230766812736000172570000003126911417922055.xml");
            GetCTE getCTE = new GetCTE();
            CTE cte = getCTE.ReaderCte(cteDoc);

            Console.WriteLine("********************************************************");
            Console.WriteLine("Aqui estão os dados da Cte: \n");
       
            Console.WriteLine("infCte: " + cte.infCte);
            Console.WriteLine("nCT: " + cte.nCT);
            Console.WriteLine("dhEmi: " + cte.dhEmi);
            Console.WriteLine("emitCnpj: " + cte.emitCnpj);
            Console.WriteLine("emitXNome: " + cte.emitXNome);
            Console.WriteLine("remCnpj: " + cte.remCnpj);
            Console.WriteLine("remXNome: " + cte.remXNome);
            Console.WriteLine("destCnpj: " + cte.destCnpj);
            Console.WriteLine("destXNome: " + cte.destXNome);
            Console.WriteLine("vCte: " + cte.vCte);


            XmlDocument cfeDoc = new XmlDocument();
            cfeDoc.Load(@"C:/Users/Dev/Downloads/35230457541070000174590001627770320902298517.xml");
            GetCFE getCFE = new GetCFE();
            CFE cfe = getCFE.ReaderCfe(cfeDoc);

            Console.WriteLine("********************************************************");
            Console.WriteLine("Aqui estão os dados da Cfe: \n");
           
            Console.WriteLine("infCfe: " + cfe.infCfe);
            Console.WriteLine("cNF: " + cfe.cNF);
            Console.WriteLine("dhEmi: " + cfe.dhEmi);
            Console.WriteLine("emitCnpj: " + cfe.emitCnpj);
            Console.WriteLine("emitXNome: " + cfe.emitXNome);
            Console.WriteLine("emitIE: " + cfe.emitIE);
            Console.WriteLine("vCFe: " + cfe.vCFe);
            Console.WriteLine("********************************************************");

            Console.ReadLine();
        }
    }
}
