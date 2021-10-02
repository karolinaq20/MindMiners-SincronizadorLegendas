using Microsoft.AspNetCore.Mvc;
using MindMiners_MiniProjeto.Attributes;
using MiniProjeto_Library.Model;
using MiniProjeto_Library.Service;

namespace MindMiners_MiniProjeto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]
    public class SincronizadorController : ControllerBase
    {
        [HttpPost]
        public string Post([FromForm] SincronizadorModel sincronizadorModel)
        {
            //Chamada para sincronização de arquivo .srt
            SincronizadorService sincronizadorService = new SincronizadorService();
            return sincronizadorService.SincronizadorArquivo(sincronizadorModel.Arquivo, 10);
        }
    }
}
