using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using SISPruebaTecnica.Entidades.Modelos;
using SISPruebaTecnica.Entidades.ModelosConfiguracion;
using SISPruebaTecnica.Negocio.Interfaces;

namespace SISPruebaTecnica.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ILogger<UsuariosController> logger;
        private IUsuariosServicio IUsuariosServicio { get; set; }
        public UsuariosController(ILogger<UsuariosController> logger,
            IUsuariosServicio IUsuariosServicio)
        {
            this.logger = logger;
            this.IUsuariosServicio = IUsuariosServicio;
        }

        [HttpPost]
        [Route("InsertarUsuario")]
        public IActionResult InsertarUsuario(JObject usuarioJson)
        {
            try
            {
                logger.LogInformation("Inicio de insertar usuario");

                if (usuarioJson == null)
                    throw new Exception("Insertar usuario vacío compruebe la info enviada");

                Usuarios usuario = usuarioJson.ToObject<Usuarios>();
                
                if (usuario == null)
                {
                    logger.LogInformation("Sin información de usuario");
                    throw new Exception("Sin información de usuario");
                }
                else
                {
                    RestResponseModel rpta = this.IUsuariosServicio.InsertarUsuario(usuario);
                    if (rpta.IsSucess)
                    {
                        logger.LogInformation($"Creación de usuario exitoso");
                        return Ok(rpta.Response);
                    }
                    else
                    {
                        return BadRequest(rpta.Response);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error el controlador de insertar usuario", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("EditarUsuario")]
        public IActionResult EditarUsuario(JObject usuarioJson)
        {
            try
            {
                logger.LogInformation("Inicio de editar usuario");

                if (usuarioJson == null)
                    throw new Exception("Editar usuario vacío compruebe la info enviada");

                Usuarios usuario = usuarioJson.ToObject<Usuarios>();

                if (usuario == null)
                {
                    logger.LogInformation("Sin información de usuario");
                    throw new Exception("Sin información de usuario");
                }
                else
                {
                    RestResponseModel rpta = this.IUsuariosServicio.EditarUsuario(usuario);
                    if (rpta.IsSucess)
                    {
                        logger.LogInformation($"Actualización de usuario exitoso");
                        return Ok(rpta.Response);
                    }
                    else
                    {
                        return BadRequest(rpta.Response);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error el controlador de editar usuario", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("BuscarUsuario")]
        public IActionResult BuscarUsuario(JObject busquedaJson)
        {
            try
            {
                logger.LogInformation("Inicio de buscar usuario");

                if (busquedaJson == null)
                    throw new Exception("Buscar usuario vacío compruebe la info enviada");

                BusquedaBindingModel busqueda = busquedaJson.ToObject<BusquedaBindingModel>();

                if (busqueda == null)
                {
                    logger.LogInformation("Sin información de busqueda usuario");
                    throw new Exception("Sin información de busqueda usuario");
                }
                else
                {
                    RestResponseModel rpta = this.IUsuariosServicio.BuscarUsuarios(busqueda);
                    if (rpta.IsSucess)
                    {
                        logger.LogInformation($"Busqueda de usuario exitoso");
                        return Ok(rpta.Response);
                    }
                    else
                    {
                        return BadRequest(rpta.Response);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error en el controlador de buscar usuario", ex);
                return BadRequest(ex.Message);
            }
        }

    }
}
