using System;
using System.Data;
using Moq;
using Dapper;
using Xunit;
using CadastroPessoasCebraspe.Model;
using CadastroPessoasCebraspe.Repositories;
using CadastroPessoasCebraspe.Utils;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace CadastroPessoasCebraspe.Tests.Repositories
{
    public class PessoaRepositoryTests
    {
        private readonly Mock<IDbConnection> _mockDbConnection;
        private readonly PessoaRepository _pessoaRepository;

        public PessoaRepositoryTests()
        {
            _mockDbConnection = new Mock<IDbConnection>();
            _pessoaRepository = new PessoaRepository(new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string>
            {
                {"ConnectionStrings:DefaultConnection", "YourConnectionStringHere"}
            }).Build());
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
