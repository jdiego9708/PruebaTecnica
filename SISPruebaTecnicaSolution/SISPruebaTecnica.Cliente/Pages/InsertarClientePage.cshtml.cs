using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SISPruebaTecnica.Entidades.Modelos;
using SISPruebaTecnica.Entidades.ModelosConfiguracion;
using SISPruebaTecnica.Negocio.Interfaces;

namespace SISPruebaTecnica.Cliente.Pages
{
    public class InsertarClientePageModel : PageModel
    {
        private readonly IUsuariosServicio IUsuariosServicio;
        public InsertarClientePageModel(IUsuariosServicio IUsuariosServicio)
        {
            this.IUsuariosServicio = IUsuariosServicio;
        }
        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            try
            {
                var nombre = Request.Form["txtnombreUsuario"];
                var fecha = Request.Form["txtfecha"];
                var genero = Request.Form["listGenero"];

                Usuarios us = new()
                {
                    Nombre_usuario = nombre,
                    Fecha_nacimiento = Convert.ToDateTime(fecha),
                    Genero = genero,
                    Estado_usuario = "ACTIVO",
                };

                RestResponseModel response = this.IUsuariosServicio.InsertarUsuario(us);

                if (response == null)
                    throw new Exception();

                if (!response.IsSucess)
                    throw new Exception();

                JToken jtoken = JToken.Parse(response.Response);

                Usuarios usuario = JsonConvert.DeserializeObject<Usuarios>(jtoken.ToString());

                return Page();
            }
            catch (Exception)
            {
                return RedirectToPage("/NotFound");
            }
        }
    }
}
