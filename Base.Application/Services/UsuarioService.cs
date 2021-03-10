using Base.Domain.DTOs.Auth;
using Base.Domain.Entities;
using Base.Domain.Enums;
using Base.Domain.Interfaces.Application;
using Base.Domain.Interfaces.Repositorys;
using Base.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Base.Application.Services
{
    public sealed class UsuarioService : IUsuarioService
    {
        private IRepositoryBase<Usuario> Repository { get; set; }

        public UsuarioService(IRepositoryBase<Usuario> Repository)
        {
            this.Repository = Repository;
        }
        public AuthData Autenticar(AuthRequest obj)
        {
            if (string.IsNullOrWhiteSpace(obj.User) || string.IsNullOrWhiteSpace(obj.Password))
            {
                throw new Exception("Preencha todos os campos !");
            }

            string senhaCrypto = EncryptHelper.EncryptRijndael(obj.Password);
            var dt = Repository.Query(x => x.Email == obj.User && x.Senha == senhaCrypto).FirstOrDefault();

            if (dt == null)
                throw new Exception("E-mail ou senha inválido(s)");

            return new AuthData(dt.Id, TypeUser.Administrator, dt.IdEmpresa);
        }

        public async Task<Usuario> Autenticado(AuthData auth) => await Repository.Query(x => x.Id == auth.IdUsuario).FirstOrDefaultAsync();

    }
}
