using System;

namespace Base.Helpers
{
    public static class MathHelper
    {
        public static double Arredondar(this double valor)
        {
            return (double)Math.Round(Convert.ToDecimal(valor), 2, MidpointRounding.AwayFromZero);
        }

        public static int ArredondarParaInteiro(this double valor, bool arredondarParaBaixo = false)
        {
            if (arredondarParaBaixo)
            {
                return Convert.ToInt32(Math.Floor(Convert.ToDecimal(valor)));
            }
            else
            {
                return Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(valor)));
            }
        }

        public static double PercentualVariacao(double valorPassado, double valorAtual)
        {
            var resultado = Convert.ToDouble(((valorAtual * 100) / valorPassado) - 100);
            if (Double.IsNaN(resultado) || Double.IsInfinity(resultado))
            {
                return 0;
            }
            return resultado;
        }

        public static double PercentualDe(double valor, double valorTotal)
        {
            var resultado = Convert.ToDouble((valor * 100) / valorTotal);
            if (Double.IsNaN(resultado) || Double.IsInfinity(resultado))
            {
                return 0;
            }
            return Math.Round(resultado, 2);
        }
    }
}
