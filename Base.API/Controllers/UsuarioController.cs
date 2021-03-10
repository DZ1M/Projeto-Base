using Base.API.Auth;
using Base.Domain.DTOs;
using Base.Domain.DTOs.Auth;
using Base.Domain.Interfaces.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Base.API.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class UsuarioController : Controller
    {
        private readonly JwtIssuerOptions _jwtOptions;
        private IUsuarioService _service;
        public UsuarioController(IOptions<JwtIssuerOptions> jwtOptions, IUsuarioService service)
        {
            _service = service;
            _jwtOptions = jwtOptions.Value;
        }

        [HttpPost("Autenticar")]
        [AllowAnonymous]
        public virtual async Task<JsonResult> Autenticar([FromBody] AuthRequest data)
        {
            try
            {
                return await Task.Run(() =>
                {
                    //var ip = HttpContext.Connection.RemoteIpAddress.ToString();
                    var obj = _service.Autenticar(data);
                    var token = _jwtOptions.Token(obj).Result;
                    return Json(ApiReturn.Sucesso(obj, token));
                });
            }
            catch (Exception e)
            {
                return Json(ApiReturn.Erro(e.Message));
            }
        }
        [HttpGet("Autenticado")]
        [AllowAnonymous]
        public async Task<JsonResult> Autenticado()
        {
            try
            {
                var auth = _jwtOptions.GetAuthData(Request.Headers["Authorization"]);
                var obj = await _service.Autenticado(auth);
                return await Task.Run(() =>
                {
                    return Json(ApiReturn.Sucesso(obj, _jwtOptions.Renew(Request.Headers["Authorization"]).Result));
                });
            }
            catch (Exception e)
            {
                return Json(ApiReturn.Erro(e.Message));
            }
        }
    }
}
