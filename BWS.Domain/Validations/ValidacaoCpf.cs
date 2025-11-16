using System;
using System.Linq;

namespace BWS.Domain.Validations
{
    public static class ValidacaoCpf
    {
        public static bool ValidarCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf)) return false;

            
            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            if (cpf.Length != 11) return false;

            
            if (cpf.All(c => c == cpf[0])) return false;

            
            if (!ValidarDigito(cpf, 1)) return false;

            
            if (!ValidarDigito(cpf, 2)) return false;

            return true;
        }

        private static bool ValidarDigito(string cpf, int posicao)
        {
            int peso = posicao == 1 ? 10 : 11;
            int limite = posicao == 1 ? 9 : 10;
            int soma = 0;

            for (int i = 0; i < limite; i++)
                soma += (cpf[i] - '0') * (peso--);

            int resto = soma % 11;
            int digitoCalculado = resto < 2 ? 0 : 11 - resto;

            int digitoReal = cpf[posicao == 1 ? 9 : 10] - '0';

            return digitoCalculado == digitoReal;
        }
    }
}
