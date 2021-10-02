using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MiniProjeto_Library.Model
{
    public class SincronizadorModel
    {
        [FromForm(Name = "arquivo")]
        public IFormFile Arquivo { get; set; }

        [FromForm(Name = "tempo")]
        public int Tempo { get; set; }
    }
}
