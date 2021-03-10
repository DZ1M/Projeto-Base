using System;
using System.Text;

namespace Base.Domain.DTOs
{
    public class ArquivoPDF
    {
        public ArquivoPDF(byte[] conteudo, string nome, string tipo)
        {
            Conteudo = conteudo;
            Nome = nome;
            Tipo = tipo;
        }
        public byte[] Conteudo { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public static ArquivoPDF Encapsular(byte[] conteudo, string nomeArquivo)
        {
            var conteudoString = Convert.ToBase64String(conteudo);
            if (!conteudoString.StartsWith("data:application"))
            {
                conteudoString = ASCIIEncoding.ASCII.GetString(conteudo);
                conteudo = Convert.FromBase64String(conteudoString.Substring(conteudoString.IndexOf(',') + 1));
            }
            var tipoArquivo = conteudoString.Substring(conteudoString.IndexOf('/') + 1, (conteudoString.IndexOf(';') - conteudoString.IndexOf('/') - 1)).Trim();

            return new ArquivoPDF(conteudo, nomeArquivo, tipoArquivo);
        }
    }
}
