using System;
using System.Collections.Generic;
using System.Globalization;

namespace Base.Helpers
{
    public static class DateHelper
    {

        public static string DataPorExtenso(DateTime data)
        {
            CultureInfo culture = new CultureInfo("pt-BR");
            DateTimeFormatInfo dtInfo = culture.DateTimeFormat;
            string texto = "{dia} de {mes} de {ano} ás {hora}";
            texto = texto.Replace("{dia}", data.Day.ToString());
            texto = texto.Replace("{mes}", culture.TextInfo.ToTitleCase(dtInfo.GetMonthName(data.Month)).ToUpper());
            texto = texto.Replace("{ano}", data.Year.ToString());
            texto = texto.Replace("{hora}", data.Date.ToString("HH:mm"));

            return texto;
        }
        public static Dictionary<int, string> Meses()
        {
            var data = new Dictionary<int, string>();
            var dateFormatInfo = CultureInfo.GetCultureInfo("pt-BR").DateTimeFormat;
            for (int i = 1; i <= 12; i++)
            {
                data.Add(i, dateFormatInfo.GetMonthName(i));
            }

            return data;
        }
        public static string NomeMes(this int mes)
        {
            return CultureInfo.GetCultureInfo("pt-BR").DateTimeFormat.GetMonthName(mes);
        }
        public static int Idade(this DateTime date)
        {
            int idade = 0;
            idade = DateTime.Now.Year - date.Year;
            if (DateTime.Now.DayOfYear < date.DayOfYear)
            {
                idade = idade - 1;
            }

            return idade;
        }
        public static DateTime ProximoDiaUtil(this DateTime data)
        {
            data = new DateTime(data.Year, data.Month, data.Day, 0, 0, 0);
            while (Feriados(data.Year).Contains(data) || data.DayOfWeek == DayOfWeek.Saturday || data.DayOfWeek == DayOfWeek.Sunday)
            {
                data = data.AddDays(1);
            }
            return data;
        }

        private static List<DateTime> Feriados(int ano)
        {
            List<DateTime> feriados = new List<DateTime>();

            // Ano novo
            feriados.Add(new DateTime(ano, 1, 1));

            // Carnaval
            feriados.Add(Carnaval(ano));

            // Sexta-Feira Santa
            feriados.Add(SextaFeiraSanta(ano));

            // Páscoa
            feriados.Add(Pascoa(ano));

            // Tiradentes
            feriados.Add(new DateTime(ano, 4, 21));

            // Dia do Trabalhador
            feriados.Add(new DateTime(ano, 5, 1));

            // Corpus Christi
            feriados.Add(CorpusChristi(ano));

            // Independência do Brasil
            feriados.Add(new DateTime(ano, 9, 7));

            // Revolução Farroupilha
            feriados.Add(new DateTime(ano, 9, 20));

            // Nossa Senhora Aparecida
            feriados.Add(new DateTime(ano, 10, 12));

            // Finados
            feriados.Add(new DateTime(ano, 11, 2));

            // Proclamação da República
            feriados.Add(new DateTime(ano, 11, 15));

            // Natal
            feriados.Add(new DateTime(ano, 12, 25));

            return feriados;
        }

        private static DateTime CorpusChristi(int ano)
        {
            DateTime data = Pascoa(ano);
            return data.AddDays(60);
        }

        private static DateTime SextaFeiraSanta(int ano)
        {
            DateTime data = Pascoa(ano);
            return data.AddDays(-2);
        }

        private static DateTime Carnaval(int ano)
        {
            DateTime data = Pascoa(ano);
            return data.AddDays(-47);
        }

        private static DateTime Pascoa(int ano)
        {
            int mes = 3;
            int G = ano % 19 + 1;
            int C = ano / 100 + 1;
            int X = (3 * C) / 4 - 12;
            int Y = (8 * C + 5) / 25 - 5;
            int Z = (5 * ano) / 4 - X - 10;
            int E = (11 * G + 20 + Y - X) % 30;
            if (E == 24) { E++; }
            if ((E == 25) && (G > 11)) { E++; }
            int N = 44 - E;
            if (N < 21) { N = N + 30; }
            int dia = (N + 7) - ((Z + N) % 7);
            if (dia > 31)
            {
                dia = dia - 31;
                mes = 4;
            }
            return new DateTime(ano, mes, dia);
        }

    }
}
