using Base.Domain.DTOs.Auth;
using Base.Domain.Entities;
using System.Threading.Tasks;

namespace Base.Domain.Interfaces.Application
{
    /// <summary>
    ///    Interface base
    /// </summary>
    public interface IUsuarioService
    {
        /// <summary>
        ///  Autenticação do Login
        /// </summary>
        /// <param name="obj">Informe os dados do Objeto</param>
        /// <returns>Retorna o Auth Data</returns>
        AuthData Autenticar(AuthRequest obj);

        /// <summary>
        ///  Verifica autenticação do usuario
        /// </summary>
        /// <param name="auth">Dados do Auth</param>
        /// <returns>Retorna o objeto do usuario</returns>
        Task<Usuario> Autenticado(AuthData auth);
    }
}
