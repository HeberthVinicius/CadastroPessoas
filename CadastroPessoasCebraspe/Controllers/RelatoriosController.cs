using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroPessoasCebraspe.Service;
using Microsoft.AspNetCore.Mvc;

namespace CadastroPessoasCebraspe.Controllers
{
    public class RelatoriosController : ControllerBase
    {
        private readonly IRelatorioService _relatorioService;

        public RelatoriosController(IRelatorioService relatorioService)
        {
            _relatorioService = relatorioService;
        }

        [HttpGet("excel")]
        public IActionResult GerarRelatorioExcel()
        {
            var fileContents = _relatorioService.GerarRelatorioExcel();
            return File(fileContents,"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet","RelatorioPessoas.xlsx");
        }
    }
}