using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroPessoasCebraspe.Dtos;
using CadastroPessoasCebraspe.Model;
using CadastroPessoasCebraspe.Repositories;
using CadastroPessoasCebraspe.Service;
using Microsoft.AspNetCore.Mvc;

namespace CadastroPessoasCebraspe.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaService _pessoaService;

        public PessoaController(IPessoaService pessoaService){

            _pessoaService = pessoaService;

        }

        [HttpPost]
        public IActionResult CriarPessoa([FromBody] Pessoa pessoa) {
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _pessoaService.CriarPessoa(pessoa);
                return Ok(new { Message = "Pessoa criada com sucesso!" });
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetPessoas()
        {
            var pessoas = _pessoaService.GetPessoas();
            return Ok(pessoas);
        }

        [HttpGet("{cpf}")]
        public IActionResult GetPessoaByCPF(string cpf)
        {   
            var pessoa = _pessoaService.GetPessoaByCPF(cpf);

            if (pessoa == null)
            {
                return NotFound();
            }
            return Ok(pessoa);
        }

        [HttpPut("{cpf}")]
        public IActionResult AtualizarPessoa(string cpf, [FromBody] AtualizarPessoaDto pessoaDto)
        {   

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _pessoaService.AtualizarPessoa(cpf, new Pessoa
                {
                    Nome = pessoaDto.Nome,
                    RG = pessoaDto.RG,
                    Email = pessoaDto.Email
                });

                return Ok(new { Message = "Atualização efetuada com sucesso!" });

            }
                catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                // Log da exception
                return StatusCode(500, new { message = "Ocorreu um erro ao atualizar a pessoa." });
            }
        }

        [HttpDelete("{cpf}")]
        public IActionResult DeletarPessoa(string cpf)
        {  
            try
            {
                _pessoaService.DeletarPessoa(cpf);
                return Ok(new {Mensagem =  "Deleção efetuada com sucesso!"});// ou nocontent
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
