using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroPessoasCebraspe.Repositories;
using OfficeOpenXml;

namespace CadastroPessoasCebraspe.Service
{
    public class RelatorioService : IRelatorioService
    {
        private readonly IPessoaRepository _pessoasRepository;

        public RelatorioService(IPessoaRepository pessoaRepository)
        {
            _pessoasRepository = pessoaRepository;
        }

        public byte[] GerarRelatorioExcel()
        {
            var pessoas = _pessoasRepository.GetPessoas();

            using (var package = new ExcelPackage()){
                
                var worksheet = package.Workbook.Worksheets.Add("Pessoas");

                worksheet.Cells[1,1].Value =  "CPF";
                worksheet.Cells[1,2].Value =  "Nome";
                worksheet.Cells[1,3].Value =  "RG";
                worksheet.Cells[1,4].Value =  "Email";

                int row = 2;

                foreach (var pessoa in pessoas)
                {
                    worksheet.Cells[row, 1].Value = pessoa.CPF;
                    worksheet.Cells[row, 2].Value = pessoa.Nome;
                    worksheet.Cells[row, 3].Value = pessoa.RG;
                    worksheet.Cells[row, 4].Value = pessoa.Email;
                    row ++;
                }

                return package.GetAsByteArray();
            }
        }
        
    }
}