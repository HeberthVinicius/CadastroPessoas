using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroPessoasCebraspe.Model;
using CadastroPessoasCebraspe.Repositories;

namespace CadastroPessoasCebraspe.Services
{
    public class PessoaService : IPessoaService
    {
        private readonly IPessoaRepository _pessoaRepository;

        public PessoaService(IPessoaRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
        }

        public void AtualizarPessoa(string cpf, Pessoa pessoa)
        {
            _pessoaRepository.AtualizarPessoa(cpf, pessoa);
        }

        public void CriarPessoa(Pessoa pessoa)
        {
            _pessoaRepository.CriarPessoa(pessoa);
        }

        public bool DeletarPessoa(string cpf)
        {
            try
            {
                _pessoaRepository.DeletarPessoa(cpf);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Pessoa GetPessoaByCPF(string cpf)
        {
            return _pessoaRepository.GetPessoaByCPF(cpf);
            
        }

        public IEnumerable<Pessoa> GetPessoas()
        {
            return _pessoaRepository.GetPessoas();
        }
    }
}