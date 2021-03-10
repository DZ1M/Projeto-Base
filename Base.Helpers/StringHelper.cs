using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace Base.Helpers
{
    public static class StringHelper
    {
        public static string RemoverAcentos(this string texto)
        {
            if (texto == null) return string.Empty;

            const string comAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
            const string semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";

            for (var i = 0; i < comAcentos.Length; i++)
                texto = texto.Replace(comAcentos[i].ToString(), semAcentos[i].ToString());

            return texto;
        }
        public static string CapitalizeFirst(this string text)
        {
            return text.First().ToString().ToUpper() + text[1..].Trim();
        }
        public static string RemoverCaracteresEspeciaisValores(this string texto)
        {
            return texto.Replace(",", "").Replace(".", "").Replace("R$ ", "");
        }

        public static string RemoverCaracteresEspeciaisCpfCnpj(this string texto)
        {
            return texto.Replace(".", "").Replace("-", "").Replace("/", "");
        }

        public static string FormatarTextoParaUrl(string texto)
        {
            texto = RemoverAcentos(texto);

            var textoretorno = texto.Replace(" ", "-");

            const string permitidos = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmonopqrstuvwxyz0123456789-_";

            for (var i = 0; i < texto.Length; i++)
                if (!permitidos.Contains(texto.Substring(i, 1))) { textoretorno = textoretorno.Replace(texto.Substring(i, 1), ""); }

            return textoretorno;
        }

        public static string RetornaNumeros(this string texto)
        {
            if (string.IsNullOrEmpty(texto))
            {
                return "";
            }
            else
            {
                var numeros = new String(texto.Where(Char.IsDigit).ToArray());
                return string.IsNullOrEmpty(numeros) ? null : numeros;
            }
        }

        public static string AjustarTamanhoTexto(string valor, int tamanho)
        {
            if (valor.Length > tamanho)
                valor = valor.Substring(0, tamanho);

            return valor;
        }

        public static string RetornaLetras(this string texto)
        {
            return string.IsNullOrEmpty(texto) ? "" : new String(texto.Where(Char.IsLetter).ToArray()).ToUpper();
        }

        public static string FormataCpfCnpj(this string valor)
        {
            if (String.IsNullOrEmpty(valor))
            {
                return "";
            }

            valor = valor.Replace(".", "").Replace("-", "").Replace("/", "").Replace("_", "");

            if (valor.Length != 11 && valor.Length != 14)
            {
                if (valor.Length < 11)
                {
                    while (valor.Length < 11)
                    {
                        valor = "0" + valor;
                    }
                }
                else
                {
                    while (valor.Length < 14)
                    {
                        valor = "0" + valor;
                    }
                }
            }

            if (valor.Length == 11)
            {
                return Convert.ToUInt64(valor).ToString(@"000\.000\.000\-00");
            }
            else if (valor.Length == 14)
            {
                return Convert.ToUInt64(valor).ToString(@"00\.000\.000\/0000\-00");
            }

            return valor;
        }

        public static string CpfOuCnpj(this string valor)
        {
            if (VerificaCnpj(valor))
            {
                return "Cnpj";
            }
            else if (VerificaCpf(valor))
            {
                return "Cpf";
            }
            return String.Empty;
        }

        public static bool VerificaCpfCnpj(this string valor)
        {
            return (VerificaCpf(valor) || VerificaCnpj(valor));
        }

        public static bool ValidaEmail(this string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static bool VerificaCnpj(string valor)
        {
            valor = valor.Replace(".", "").Replace("-", "").Replace("/", "").Replace("_", "");
            if (long.TryParse(valor, out long value))
            {
                if (valor.ToString().Length == 14)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool VerificaCpf(string valor)
        {
            valor = valor.Replace(".", "").Replace("-", "").Replace("/", "").Replace("_", "");
            if (long.TryParse(valor, out long value))
            {
                if (valor.ToString().Length == 11)
                {
                    return true;
                }
            }
            return false;
        }

        public static string GerarChaveVerificacao()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            List<string> chave = new List<string>();

            for (int i = 0; i < 3; i++)
            {
                chave.Add(new string(
                    Enumerable.Repeat(chars, 4)
                              .Select(s => s[random.Next(s.Length)])
                              .ToArray()));
            }

            return String.Join("-", chave.ToArray());
        }

        public static string GerarCodigoPeloCpfCnpj(string valor)
        {
            // Busca valores na metade do CPF/CNPJ
            valor = valor.Replace(".", "").Replace("-", "").Replace("/", "").Replace("_", "");
            var valorByteArray = valor.Substring(2, 6).ToCharArray();

            // Inverte números
            Array.Reverse(valorByteArray);
            valor = new string(valorByteArray);

            // Busca letra do alfabeto equivalente ao número
            var valorFinal = "";
            foreach (Char c in valor.ToCharArray())
            {
                var character = (Char)((true ? 65 : 97) + (Convert.ToInt32(c) - 1));
                valorFinal += character.ToString();
            }

            // Retorna código final
            return String.Format("{0:X}", valorFinal).ToUpper();
        }

        public static string TrimIfNotNull(this string value)
        {
            if (value != null)
            {
                return value.Trim();
            }
            return null;
        }

        public static string ToUpperIfNotNull(this string value)
        {
            if (value != null)
            {
                return value.ToUpper();
            }
            return null;
        }

        public static string PreencherComZeros(this string valor, int tamanho)
        {
            if (valor.Length > tamanho)
            {
                throw new Exception(String.Format("Texto Helper > Valor para preenchimento é maior que o tamanho solicitado. Valor: {0}, tamanho esperado: {1}", valor, tamanho));
            }

            while (valor.Length < tamanho)
            {
                valor = "0" + valor;
            }

            return valor;
        }

        public static string PreencherComEspacos(this string valor, int tamanho)
        {
            if (valor.Length > tamanho)
            {
                throw new Exception(String.Format("Texto Helper > Valor para preenchimento é maior que o tamanho solicitado. Valor: {0}, tamanho esperado: {1}", valor, tamanho));
            }

            while (valor.Length < tamanho)
            {
                valor = " " + valor;
            }

            return valor;
        }

        public static string FormataCep(this string valor)
        {
            valor = valor.Replace("-", "").Trim();
            valor = valor.PreencherComZeros(8);
            valor = valor.Substring(0, 5) + "-" + valor[5..];
            return valor;
        }

        public static string ToCamelCase(this string valor)
        {
            if (!string.IsNullOrEmpty(valor) && valor.Length > 1)
            {
                return Char.ToUpper(valor[0]) + valor[1..];
            }
            return valor;
        }
    }
}
