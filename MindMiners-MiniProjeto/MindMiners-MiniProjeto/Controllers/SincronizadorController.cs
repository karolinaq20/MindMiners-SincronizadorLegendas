using Microsoft.AspNetCore.Mvc;
using MiniProjeto_Library.Model;
using MiniProjeto_Library.Service;

namespace MindMiners_MiniProjeto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SincronizadorController : ControllerBase
    {
        ///<Summary>
        /// Sincronizador de Arquivos .srt
        ///</Summary>
        [HttpPost]
        public string Post([FromForm] SincronizadorModel sincronizadorModel)
        {
            //Chamada para sincronização de arquivo .srt
            SincronizadorService sincronizadorService = new SincronizadorService();
            return sincronizadorService.SincronizadorArquivo(sincronizadorModel.Arquivo, sincronizadorModel.Tempo);
        }
    }
}
