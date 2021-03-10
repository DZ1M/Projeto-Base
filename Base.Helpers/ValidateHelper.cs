using System;
using System.Text.RegularExpressions;

namespace Base.Helpers
{
    public static class ValidateHelper
    {
        public static bool EmailValido(this string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static bool TelefoneValido(this string telefone)
        {
            try
            {
                return Regex.Match(telefone, @"(\+[0-9]{2}|\+[0-9]{2}\(0\)|\(\+[0-9]{2}\)\(0\)|00[0-9]{2}|0)([0-9]{9}|[0-9\-\s]{9,18})").Success;
            }
            catch
            {
                return false;
            }
        }

        public static bool ValidaCpfCnpj(this string text)
        {
            text = text.Replace(".", "").Replace("-", "").Replace("/", "").Replace("_", "");
            if (string.IsNullOrEmpty(text) || Convert.ToInt64(text) == 0)
            {
                return false;
            }
            else
            {
                int[] d = new int[14];
                int[] v = new int[2];
                int j, i, soma;
                string Sequencia, SoNumero;
                SoNumero = Regex.Replace(text, "[^0-9]", string.Empty);
                if (new string(SoNumero[0], SoNumero.Length) == SoNumero) return false;
                if (SoNumero.Length == 11)
                {
                    for (i = 0; i <= 10; i++) d[i] = Convert.ToInt32(SoNumero.Substring(i, 1));
                    for (i = 0; i <= 1; i++)
                    {
                        soma = 0;
                        for (j = 0; j <= 8 + i; j++) soma += d[j] * (10 + i - j);

                        v[i] = (soma * 10) % 11;
                        if (v[i] == 10) v[i] = 0;
                    }
                    return (v[0] == d[9] & v[1] == d[10]);
                }
                else if (SoNumero.Length == 14)
                {
                    Sequencia = "6543298765432";
                    for (i = 0; i <= 13; i++) d[i] = Convert.ToInt32(SoNumero.Substring(i, 1));
                    for (i = 0; i <= 1; i++)
                    {
                        soma = 0;
                        for (j = 0; j <= 11 + i; j++)
                            soma += d[j] * Convert.ToInt32(Sequencia.Substring(j + 1 - i, 1));

                        v[i] = (soma * 10) % 11;
                        if (v[i] == 10) v[i] = 0;
                    }
                    return (v[0] == d[12] & v[1] == d[13]);
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
