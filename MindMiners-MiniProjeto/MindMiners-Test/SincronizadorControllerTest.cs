using Microsoft.AspNetCore.Http;
using MindMiners_MiniProjeto.Controllers;
using MiniProjeto_Library.Model;
using Moq;
using System.IO;
using Xunit;

namespace MindMiners_Test
{
    public class SincronizadorControllerTest
    {
        [Fact]
        public void SincronizadorArquivo()
        {
            //Arrange
            var sincronizadorController = new SincronizadorController();
            var arquivoMock = new Mock<IFormFile>();

            // Cria arquivo para teste
            var memoria = new MemoryStream();
            var saida = new StreamWriter(memoria);
            saida.Write("1\r\n00:01:06,608 --> 00:01:07,609\r\nOi, Nat!\r\n\r\n2\r\n00:01:37,055 --> 00:01:38,682\r\nNós duas estamos\r\nde cabeça para baixo.");
            saida.Flush();
            memoria.Position = 0;
            arquivoMock.Setup(_ => _.OpenReadStream()).Returns(memoria);
            arquivoMock.Setup(_ => _.FileName).Returns("Test.srt");
            arquivoMock.Setup(_ => _.Length).Returns(memoria.Length);

            SincronizadorModel sincronizadorModel = new SincronizadorModel();
            sincronizadorModel.Arquivo = arquivoMock.Object;
            sincronizadorModel.Tempo = 10;

            //Act
            var resultado = sincronizadorController.Post(sincronizadorModel);

            //Assert
            Assert.True(File.Exists(resultado));
        }
    }
}
