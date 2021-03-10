using Base.Domain.Enums;
using System;
using System.Linq;

namespace Base.Domain.DTOs.Auth
{
    public class AuthData
    {
        public Guid IdUsuario { get; set; }
        public TypeUser Permissao { get; set; }
        public Guid IdEmpresa { get; set; }

        public AuthData(Guid idUsuario, TypeUser permissao, Guid idEmpresa)
        {
            IdUsuario = idUsuario;
            Permissao = permissao;
            IdEmpresa = idEmpresa;
        }

        public override string ToString()
        {
            return IdUsuario + "." + ((int)Permissao) + "." + IdEmpresa;
        }

        public static AuthData Parse(string token)
        {
            if (token.Count(x => x == '.') != 2)
            {
                throw new Exception("Token inválido");
            }
            var data = token.Split('.');
            return new AuthData(Guid.Parse(data[0]), (TypeUser)Convert.ToInt32(data[1]), Guid.Parse(data[2]));
        }

        public bool HasData()
        {
            return !(IdUsuario == Guid.Empty) || !(IdEmpresa == Guid.Empty);
        }
    }
}
