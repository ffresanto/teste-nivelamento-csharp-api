using System.Globalization;

namespace Questao1
{
    class ContaBancaria {
        public int Numero { get; }
        public string Titular { get; set; }
        public double Saldo { get; private set; }
        public static double TaxaSaque { get; private set; } = 3.50;


        public ContaBancaria(int numero, string titular)
        {
            Numero = numero;
            Titular = titular;
            Saldo = 0;
        }

        public ContaBancaria(int numero, string titular, double depositoInicial) : this(numero, titular)
        {
            Deposito(depositoInicial);
        }

        public void Deposito(double quantia)
        {
            if (quantia > 0)
                Saldo += quantia;
        }

        public void Saque(double quantia)
        {
            if (quantia > 0 && Saldo >= quantia)
            {
                Saldo -= quantia + TaxaSaque;
            }
        }

        public override string ToString()
        {
            return $"Conta {Numero}, Titular: {Titular}, Saldo: $ {Saldo.ToString("F2")}";
        }

    }
}
