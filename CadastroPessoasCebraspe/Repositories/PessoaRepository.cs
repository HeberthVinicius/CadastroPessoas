using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CadastroPessoasCebraspe.Model;
using CadastroPessoasCebraspe.Utils;
using Dapper;

namespace CadastroPessoasCebraspe.Repositories
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly string _connectionString;

        public PessoaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void CriarPessoa(Pessoa pessoa)
        {
            if (!ManipulaCPF.ValidarCPF(pessoa.CPF))
            {
                throw new ArgumentException("CPF inválido.");
            }

            var cpfSemMascara = ManipulaCPF.RemoverMascara(pessoa.CPF);

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var pessoaCadastro = connection.QuerySingleOrDefault<Pessoa>(
                        "sp_GetPessoaByCPF", new { CPF = cpfSemMascara }, commandType: CommandType.StoredProcedure);

                    if (pessoaCadastro == null)
                    {
                        connection.Execute("sp_CriarPessoa", new
                        {
                            CPF = cpfSemMascara,
                            pessoa.Nome,
                            pessoa.RG,
                            pessoa.Email
                        }, commandType: CommandType.StoredProcedure);
                    }
                    else
                    {
                        throw new ArgumentException("CPF já cadastrado!");
                    }
                }
            }
            catch (SqlException ex)
            {
                
                throw ex.InnerException;
            }
        }   

        public IEnumerable<Pessoa> GetPessoas()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var pessoas = connection.Query<Pessoa>("sp_GetPessoa", commandType: CommandType.StoredProcedure).ToList();
                foreach (var pessoa in pessoas)
                {
                    pessoa.CPF = ManipulaCPF.FormatarCPF(pessoa.CPF);
                }
                return pessoas;
            }
        }

        public Pessoa GetPessoaByCPF(string cpf)
        {
            if (!ManipulaCPF.ValidarCPF(cpf))
            {
                throw new ArgumentException("CPF inválido.");
            }

            var cpfSemMascara = ManipulaCPF.RemoverMascara(cpf);

            using (var connection = new SqlConnection(_connectionString))
            {
                var pessoa = connection.QuerySingleOrDefault<Pessoa>("sp_GetPessoaByCPF", new { 
                    CPF = cpfSemMascara }, commandType: CommandType.StoredProcedure);
                
                if (pessoa != null)
                {
                    pessoa.CPF = ManipulaCPF.FormatarCPF(pessoa.CPF);
                }

                return pessoa;
            }
        }

        public void AtualizarPessoa(string cpf,Pessoa pessoa)
        {
            if (string.IsNullOrEmpty(pessoa.Nome) || string.IsNullOrEmpty(pessoa.RG) || string.IsNullOrEmpty(pessoa.Email))
            {
                throw new ArgumentException("Nome, Identidade e Email são campos obrigatórios.");
            }

            if (!ManipulaCPF.ValidarCPF(cpf))
            {
                throw new ArgumentException("CPF inválido.");
            }

            var cpfSemMascara = ManipulaCPF.RemoverMascara(cpf);

            using (var connection = new SqlConnection(_connectionString))
            {
                var pessoaCadastro = connection.QuerySingleOrDefault<Pessoa>(
                    "sp_GetPessoaByCPF", new { CPF = cpfSemMascara }, commandType: CommandType.StoredProcedure);

                if (pessoaCadastro == null)
                {
                    throw new ArgumentException("Pessoa com o CPF fornecido não está cadastrada.");
                }

                if (pessoaCadastro.CPF != cpfSemMascara)
                {
                    throw new ArgumentException("CPF não pode ser alterado.");
                }

                connection.Execute("sp_AtualizarPessoa", new { 
                    CPF = cpfSemMascara,
                    pessoa.Nome,
                    pessoa.RG,
                    pessoa.Email
                }, commandType: CommandType.StoredProcedure);
            }
        }

        public void DeletarPessoa(String cpf)
        {
            if (!ManipulaCPF.ValidarCPF(cpf))
            {
                throw new ArgumentException("CPF inválido.");
            }

            var cpfSemMascara = ManipulaCPF.RemoverMascara(cpf);

            using (var connection = new SqlConnection(_connectionString))
            {
                
                var pessoa = connection.QuerySingleOrDefault<Pessoa>("sp_GetPessoaByCPF", new { 
                    CPF = cpfSemMascara }, commandType: CommandType.StoredProcedure);

                if (pessoa == null)
                {
                    throw new ArgumentException("CPF não cadastrado.");
                }

                connection.Execute("sp_DeletarPessoa", new { 
                    CPF = cpfSemMascara}, commandType: CommandType.StoredProcedure);
            }
        }
    }
}