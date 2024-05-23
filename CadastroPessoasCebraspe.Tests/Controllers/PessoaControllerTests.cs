using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using CadastroPessoasCebraspe.Controllers;
using CadastroPessoasCebraspe.Repositories;
using CadastroPessoasCebraspe.Model;
using CadastroPessoasCebraspe.Dtos;

namespace CadastroPessoasCebraspe.Tests.Controllers
{
    public class PessoaControllerTests
    {
        private readonly Mock<IPessoaRepository> _mockPessoaRepository;
        private readonly PessoaController _pessoaController;

        public PessoaControllerTests()
        {
            _mockPessoaRepository = new Mock<IPessoaRepository>();
            _pessoaController = new PessoaController(_mockPessoaRepository.Object);
        }

        [Fact]
        public void CriarPessoa_DeveRetornarBadRequest_QuandoCPFInvalido()
        {
            var pessoa = new Pessoa { CPF = "123", Nome = "Nome", RG = "RG", Email = "email@teste.com" };
            var result = _pessoaController.CriarPessoa(pessoa);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void DeletarPessoa_DeveRetornarBadRequest_QuandoCPFInvalido()
        {
            var result = _pessoaController.DeletarPessoa("123");
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void GetPessoaByCPF_DeveRetornarBadRequest_QuandoCPFInvalido()
        {
            var result = _pessoaController.GetPessoaByCPF("123");
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void AtualizarPessoa_DeveRetornarBadRequest_QuandoCamposObrigatoriosEstaoAusentes()
        {
            var pessoaDto = new AtualizarPessoaDto { Nome = "", RG = "", Email = "" };
            var result = _pessoaController.AtualizarPessoa("123.456.789-00", pessoaDto);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void AtualizarPessoa_DeveRetornarBadRequest_QuandoCPFInvalido()
        {
            var pessoaDto = new AtualizarPessoaDto { Nome = "Nome", RG = "RG", Email = "email@teste.com" };
            var result = _pessoaController.AtualizarPessoa("123", pessoaDto);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void AtualizarPessoa_DeveRetornarOk_QuandoDadosValidos()
        {
            var pessoaDto = new AtualizarPessoaDto { Nome = "Nome", RG = "RG", Email = "email@teste.com" };
            var result = _pessoaController.AtualizarPessoa("123.456.789-00", pessoaDto);
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
