using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MiniProjeto_Library.Model
{
    public class SincronizadorModel
    {
        [Required]
        [FromForm(Name = "ArquivoSrt")]
        public IFormFile Arquivo { get; set; }

        [Required]
        [FromForm(Name = "TempoSegundos")]
        public int Tempo { get; set; }
    }
}
