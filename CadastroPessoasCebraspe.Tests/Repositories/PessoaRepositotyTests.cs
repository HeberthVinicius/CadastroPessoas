using System;
using System.Collections.Generic;
using CadastroPessoasCebraspe.Model;
using CadastroPessoasCebraspe.Repositories;
using NSubstitute;
using Xunit;
using Microsoft.Extensions.Configuration;

namespace CadastroPessoasCebraspe.Tests.Repositories
{
    public class PessoaRepositoryTests
    {
        private readonly IConfiguration _mockConfiguration;
        private readonly PessoaRepository _pessoaRepository;

        public PessoaRepositoryTests()
        {
            _mockConfiguration = Substitute.For<IConfiguration>();
            _mockConfiguration.GetConnectionString("DefaultConnection").Returns("YourConnectionStringHere");

            _pessoaRepository = new PessoaRepository(_mockConfiguration);
        }

        [Fact]
        public void CriarPessoa_DeveLancarExcecao_QuandoCPFInvalido()
        {
            var pessoa = new Pessoa { CPF = "123", Nome = "Nome", RG = "RG", Email = "email@teste.com" };
            Assert.Throws<ArgumentException>(() => _pessoaRepository.CriarPessoa(pessoa));
        }

        [Fact]
        public void DeletarPessoa_DeveLancarExcecao_QuandoCPFInvalido()
        {
            Assert.Throws<ArgumentException>(() => _pessoaRepository.DeletarPessoa("123"));
        }

        [Fact]
        public void GetPessoaByCPF_DeveLancarExcecao_QuandoCPFInvalido()
        {
            Assert.Throws<ArgumentException>(() => _pessoaRepository.GetPessoaByCPF("123"));
        }

        [Fact]
        public void AtualizarPessoa_DeveLancarExcecao_QuandoCamposObrigatoriosEstaoAusentes()
        {
            var pessoa = new Pessoa { Nome = "", RG = "", Email = "" };
            Assert.Throws<ArgumentException>(() => _pessoaRepository.AtualizarPessoa("123.456.789-00", pessoa));
        }

        [Fact]
        public void AtualizarPessoa_DeveLancarExcecao_QuandoCPFInvalido()
        {
            var pessoa = new Pessoa { Nome = "Nome", RG = "RG", Email = "email@teste.com" };
            Assert.Throws<ArgumentException>(() => _pessoaRepository.AtualizarPessoa("123", pessoa));
        }
    }
}
