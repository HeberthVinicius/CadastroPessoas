using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroPessoasCebraspe.Utils
{
    public static class ManipulaCPF
    {

    public static string RemoverMascara(string cpf)
    {
        return new string(cpf.Where(char.IsDigit).ToArray());
    }

    public static string FormatarCPF(string cpf)
    {
        cpf = RemoverMascara(cpf);
        if (cpf.Length != 11)
        {
            throw new ArgumentException("CPF deve ter 11 dígitos.");
        }
        return Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00");
    }

    public static bool ValidarCPF(string cpf)
    {
        cpf = new string(cpf.Where(char.IsDigit).ToArray());

        if (cpf.Length != 11)
            return false;

        var tempCpf = cpf.Substring(0, 9);
        var digito = new int[2];

        for (int i = 0; i < 2; i++)
        {
            int sum = 0;
            for (int j = 0; j < 9 + i; j++)
                sum += (int.Parse(tempCpf[j].ToString()) * (10 + i - j));

            digito[i] = sum % 11;
            if (digito[i] < 2)
                digito[i] = 0;
            else
                digito[i] = 11 - digito[i];

            tempCpf += digito[i].ToString();
        }

        return cpf.EndsWith(digito[0].ToString() + digito[1].ToString());
    }
    }
}