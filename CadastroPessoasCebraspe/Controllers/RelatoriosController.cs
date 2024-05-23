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
        private readonly RelatorioService _relatorioService;

        public RelatoriosController(RelatorioService relatorioService)
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