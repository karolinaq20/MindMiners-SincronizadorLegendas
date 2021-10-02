using Microsoft.AspNetCore.Http;
using MiniProjeto_Library.Interface;
using System;
using System.IO;
using System.Text;

namespace MiniProjeto_Library.Service
{
    public class SincronizadorService : ISincronizador
    {
        //Método para sincronizar
        public string SincronizadorArquivo(IFormFile arquivo, int tempo)
        {
            try
            {
                // Valida se o arquivo tem a extensão esperada
                if (!arquivo.FileName.EndsWith(".srt"))
                    throw new Exception("Arquivo deve possuir extensão .srt");

                string diretorioSaida = Path.Combine(Environment.CurrentDirectory, "Output");

                // Caso o diretório de saída não exista, faz a criação
                if (!Directory.Exists(diretorioSaida))
                    Directory.CreateDirectory(diretorioSaida);

                // Elabora nome do arquivo
                string arquivoAjustado = Path.Combine(diretorioSaida, $"Sincronizado_{arquivo.FileName.Replace(".srt", "", StringComparison.InvariantCultureIgnoreCase)}_{DateTime.Now.Ticks}.srt");

                // Inicia leitura de arquivo enviado por parâmetro
                using (StreamReader leitor = new StreamReader(arquivo.OpenReadStream(), Encoding.Default))
                {
                    string linha;

                    // Inicia escrita de novo arquivo com sincronização
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
