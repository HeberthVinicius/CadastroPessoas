using System;
using System.Collections.Generic;
using CadastroPessoasCebraspe.Controllers;
using CadastroPessoasCebraspe.Model;
using CadastroPessoasCebraspe.Repositories;
using CadastroPessoasCebraspe.Service;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace CadastroPessoasCebraspe.Tests.Controllers
{
    public class PessoaControllerTests
    {
        private readonly IPessoaService _mockPessoaService;
        private readonly PessoaController _pessoaController;

        public PessoaControllerTests()
        {
            _mockPessoaService = Substitute.For<IPessoaService>();
            _pessoaController = new PessoaController(_mockPessoaService);
        }

        [Fact]
        public void GetPessoaByCPF_DeveRetornarNotFound_QuandoCPFInvalido()
        {
            // Arrange
            _mockPessoaService.GetPessoaByCPF("123").Returns((Pessoa)null);

            // Act
            var result = _pessoaController.GetPessoaByCPF("123");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetPessoaByCPF_DeveRetornarPessoa_QuandoCPFValido()
        {
            // Arrange
            var pessoa = new Pessoa { CPF = "123.456.789-00", Nome = "Nome", RG = "RG", Email = "email@teste.com" };
            _mockPessoaService.GetPessoaByCPF("123.456.789-00").Returns(pessoa);

            // Act
            var result = _pessoaController.GetPessoaByCPF("123.456.789-00") as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(pessoa, result.Value);
        }

        [Fact]
        public void CriarPessoa_DeveRetornarBadRequest_QuandoCPFInvalido()
        {
            // Arrange
            var pessoa = new Pessoa { CPF = "123", Nome = "Nome", RG = "RG", Email = "email@teste.com" };

            // Act
            var result = _pessoaController.CriarPessoa(pessoa);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void CriarPessoa_DeveRetornarOk_QuandoPessoaValida()
        {
            // Arrange
            var pessoa = new Pessoa { CPF = "123.456.789-00", Nome = "Nome", RG = "RG", Email = "email@teste.com" };

            // Act
            var result = _pessoaController.CriarPessoa(pessoa);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void DeletarPessoa_DeveRetornarNotFound_QuandoCPFInvalido()
        {
            // Arrange
            _mockPessoaService.DeletarPessoa("123").Returns(false);

            // Act
            var result = _pessoaController.DeletarPessoa("123");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeletarPessoa_DeveRetornarOk_QuandoCPFValido()
        {
            // Arrange
            _mockPessoaService.DeletarPessoa("123.456.789-00").Returns(true);

            // Act
            var result = _pessoaController.DeletarPessoa("123.456.789-00");

            // Assert
            Assert.IsType<OkResult>(result);
        }
    }
}
