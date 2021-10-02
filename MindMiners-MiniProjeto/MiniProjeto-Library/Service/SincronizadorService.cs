using Microsoft.AspNetCore.Http;
using MiniProjeto_Library.Interface;
using System;
using System.IO;
using System.Text;

namespace MiniProjeto_Library.Service
{
    public class SincronizadorService: ISincronizador
    {
        //Método para sincronizar
        public string SincronizadorArquivo(IFormFile arquivo, int tempo)
        {
            string arquivoAjustado = Path.Combine(Environment.CurrentDirectory, "Output", $"Sincronizado_{DateTime.Now.Ticks}_{arquivo.FileName}");

            try
            {
                using (StreamReader leitor = new StreamReader(arquivo.OpenReadStream(), Encoding.Default))
                {
                    string linha;

                    using (StreamWriter saida =
                       new StreamWriter(arquivoAjustado, false, Encoding.Default))
                    {
                        while ((linha = leitor.ReadLine()) != null)
                        {
                            // Valida se linha é vazia
                            if (string.IsNullOrEmpty(linha))
                            {
                                saida.Write("\r\n");
                                continue;
                            }

                            // Valida se é o trecho que deve ser modificado
                            if (linha.Contains("-->"))
                            {
                                string[] linhaCompleta = linha.Split("-->");
                                string inicio = linhaCompleta[0].Trim();
                                string fim = linhaCompleta[1].Trim();

                                saida.Write(String.Format(
                                        "{0:HH\\:mm\\:ss\\,fff} --> {1:HH\\:mm\\:ss\\,fff}\r\n",
                                        DateTime.Parse(inicio.Replace(",", ".")).AddSeconds(tempo),
                                        DateTime.Parse(fim.Replace(",", ".")).AddSeconds(tempo)));

                                continue;
                            }

                            // Caso não entre nas condicionais acima, escreve o texto e pula linha
                            saida.Write($"{linha}\r\n");
                        }
                    }
                }

                return arquivoAjustado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
