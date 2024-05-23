using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroPessoasCebraspe.Model;

namespace CadastroPessoasCebraspe.Service
{
    public interface IPessoaService
    {
        void CriarPessoa(Pessoa pessoa);
        IEnumerable<Pessoa>GetPessoas();
        Pessoa GetPessoaByCPF(string cpf);
        void AtualizarPessoa(string cpf, Pessoa pessoa);
        bool DeletarPessoa(String cpf);
    }
}