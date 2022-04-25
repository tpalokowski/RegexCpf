using System;
using System.Text.RegularExpressions;

namespace RegexCpf
{
    class Program
    {
        static bool VerificarEntradaCpf(string cpfString)
        {
            string padraoCpfRegex = @"^\d\d\d\d\d\d\d\d\d\d\d$";
            if (Regex.IsMatch(cpfString, padraoCpfRegex))
            {
                System.Console.WriteLine("Entrada válida");
                return true;
            }
            System.Console.WriteLine("Entrada inválida");
            return false;
        }

        static int[] ValidarDigitoVerificador(int[] cpf, int[] cpfAuxiliar, int verificaDigito)
        {
            int soma = 0, acumulador = 0, digitoResultado = 0, digitoResto = 0;
            for (int i = 0; i < cpf.Length - verificaDigito; i++) //laço for do tamanho do array - 2 casas pois é como será validado o primeiro digito verificador
            {
                soma = cpf[i] * cpfAuxiliar[i]; //multiplica o valor do cpf pelo peso correspondente
                acumulador = acumulador + soma; //soma os resultados das multiplicações
            }
            digitoResultado = acumulador / 11; //obtem o resultado das multiplicações dividido por 11
            digitoResto = acumulador % 11; //obtem o resto da divisão por 11

            if (digitoResto < 2) // se o resto é menor que 2, o primeiro dígito verificador passa a valer 0
            {
                cpf[cpf.Length - verificaDigito] = 0;
            }
            else  // do contrário, pega-se o valor 11 e subtrai o resto, o valor da subtração passa a ser o dígito verificador em questão
            {
                cpf[cpf.Length - verificaDigito] = 11 - digitoResto;
            }
            return cpf;
        }
        static void ImprimirCpf(int[] cpf)
        {
            for (int i = 0; i < cpf.Length; i++)
            {
                Console.Write($"{cpf[i]} ");
            }
        }

        static void Main(string[] args)
        {
            Console.Write("Insira seu CPF (somente números): ");
            string cpfObter = Console.ReadLine();
            string cpfString = cpfObter;
            int[] cpf = new int[11]; //declara o array a ser usado pelo usuário
            int[] cpfAuxiliar = new int[11]; //Usado para fazer as validações e somas
            int[] verificaCpf = new int[11]; //Usado para validar a entrada com os dígitos verificadores

            if (VerificarEntradaCpf(cpfString))
            {
                for (int i = 0; i < cpf.Length; i++)
                {
                    cpf[i] = (int)char.GetNumericValue(cpfString, i); // converte a String com números para um array de inteiros
                    verificaCpf[i] = (int)char.GetNumericValue(cpfString, i);
                }
                for (int i = 0; i < cpf.Length - 2; i++) //laço for do tamanho do array - 2 casas dos digitos verificadores
                {
                    cpfAuxiliar[i] = 10 - i;
                }
                cpf = ValidarDigitoVerificador(cpf, cpfAuxiliar, 2);

                for (int i = 0; i < cpf.Length - 1; i++) //laço for do tamanho do array - 1 casa agora, pois um dígito já foi preenchido
                {
                    cpfAuxiliar[i] = 11 - i; // atualiza os pesos
                }
                cpf = ValidarDigitoVerificador(cpf, cpfAuxiliar, 1);

                Console.Write("\nO CPF Inserido: ");
                ImprimirCpf(verificaCpf);
                Console.Write("\nO CPF validado: ");
                ImprimirCpf(cpf);

                if (cpf[9] == verificaCpf[9] && cpf[10] == verificaCpf[10])
                {
                    System.Console.Write("\nO CPF de entrada é válido!");
                }
                else
                {
                    System.Console.Write("\nO CPF de entrada é inválido!");
                }
            }
            else
            {
                System.Console.WriteLine("CPF Incorreto");
            }
        }
    }
}
