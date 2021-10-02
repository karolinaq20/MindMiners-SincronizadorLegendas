using Microsoft.AspNetCore.Http;

namespace MiniProjeto_Library.Interface
{
    public interface ISincronizador
    {
        string SincronizadorArquivo(IFormFile arquivo, int tempo);
    }
}
