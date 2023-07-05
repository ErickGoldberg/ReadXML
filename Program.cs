using System;
using XMLSearch.Data;
using XMLSearch.Helper;

namespace SeuNamespace
{
    class Program
    {
        static void Main(string[] args)
        {
   
            GetNFE getNFE = new GetNFE();

            NFE nfe = getNFE.ReaderNfe(@"C:\Users\Dev\Downloads\31230760409075009613550010020302676872200497.xml");

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

            Console.WriteLine("********************************************************");
            Console.WriteLine("Aqui estão os dados da Cte:");
            
            Console.ReadLine();
        }
    }
}
