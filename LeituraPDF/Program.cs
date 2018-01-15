using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeituraPDF
{
    public class Program
    {
        public static void Main(string[] args)
        {

            while (true)
            {
                Console.WriteLine("Escolha uma Opção: \n");
                Console.WriteLine("1 - Ler um Arquivo");
                Console.WriteLine("2 - Alterar um Arquivo");

                char operacao = char.Parse(Console.ReadLine());

                switch (operacao)
                {
                    case '1':
                        Console.Write("Digite o nome do arquivo: ");
                        string nomearq = Console.ReadLine();
                        lerArquivo(nomearq);
                        break;
                    case '2':
                        Console.Write("Digite o nome do arquivo: ");
                        string arquivoOrigem = Console.ReadLine();
                        Console.Write("Digite o nome do novo arquivo à ser gerado: ");
                        string novoArq = Console.ReadLine();
                        string novoArquivo = @"C:\Users\Thiago Gabriel\Desktop\PDF\" + novoArq + ".pdf";
                        Console.Write("Digite o nome do texto à ser alterado: ");
                        string textoOrigem = Console.ReadLine();
                        Console.Write("Digite o nome do novo texto à ser escrito: ");
                        string novoTexto = Console.ReadLine();
                        alterarArquivo(arquivoOrigem, novoArquivo,textoOrigem, novoTexto);
                        break;
                    default:
                        break;
                }
                Console.ReadKey();
            }
        }

        public static void lerArquivo(string nomeArquivo)
        {
            try
            {
                StringBuilder text = new StringBuilder();
                PdfReader reader = new PdfReader(@"C:\Users\Thiago Gabriel\Desktop\PDF\" + nomeArquivo + ".pdf");

                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
                }
                Console.WriteLine(text.ToString());
                reader.Close();            }
            catch (Exception e)
            {
                Console.WriteLine("O arquivo não foi encontrado\n" + e.Message);
            }
        }

        public static void alterarArquivo(string nomeArquivo, string nomeNovoArq, string textoOrigem, string novoTexto) 
        {        
            try 
	        {
                try
                {
                    PdfReader reader = new PdfReader(@"C:\Users\Thiago Gabriel\Desktop\PDF\" + nomeArquivo + ".pdf");
                    for (int i = 1; i <= reader.NumberOfPages; i++)
                    {
                        byte[] conteudoBytes = reader.GetPageContent(i);
                        string conteudoString = PdfEncodings.ConvertToString(conteudoBytes, PdfObject.TEXT_PDFDOCENCODING).ToString();
                        conteudoString = conteudoString.Replace(textoOrigem, novoTexto);
                        reader.SetPageContent(i, PdfEncodings.ConvertToBytes(conteudoString, PdfObject.TEXT_PDFDOCENCODING));

                        conteudoString = conteudoString.Replace(textoOrigem, nomeArquivo);

                        reader.SetPageContent(i, PdfEncodings.ConvertToBytes(conteudoString, PdfObject.TEXT_PDFDOCENCODING));
                    }
                    new PdfStamper(reader, new FileStream(nomeNovoArq + ".pdf", FileMode.Create, FileAccess.Write)).Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao tentar alterar arquivo!" + ex.Message);
                }

            }
            catch (IOException ex)
	        {
                Console.WriteLine("Erro" + ex.Message);
	        }
        }
        

    }
}