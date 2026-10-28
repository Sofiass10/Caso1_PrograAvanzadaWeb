using Albums.Abstract.Models; 
using Microsoft.AspNetCore.Mvc;

namespace Albums.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public abstract class ApiBaseController : ControllerBase
    {        protected IActionResult Responder(Resultado resultado)
        {
            return resultado.Exito ? NoContent() : Error(resultado);
        }
        protected IActionResult Responder<T>(Resultado<T> resultado)
        {
            return resultado.Exito ? Ok(resultado.Datos) : Error(resultado);
        }

        protected IActionResult Error(Resultado resultado)
        {
            var cuerpo = new { mensaje = resultado.Mensaje };

            return resultado.Error switch
            {
                TipoError.NoEncontrado => NotFound(cuerpo),   // 404
                TipoError.Conflicto => Conflict(cuerpo),      // 409
                _ => BadRequest(cuerpo)                       // 400
            };
        }
    }
}