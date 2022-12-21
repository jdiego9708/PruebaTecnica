using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using SISPruebaTecnica.Entidades.Modelos;
using SISPruebaTecnica.Entidades.ModelosConfiguracion;
using SISPruebaTecnica.Negocio.Interfaces;
using System.Collections.Generic;

namespace SISPruebaTecnica.Cliente.Pages
{
    public class EditarClientePageModel : PageModel
    {
        private readonly IUsuariosServicio IUsuariosServicio;
        public EditarClientePageModel(IUsuariosServicio IUsuariosServicio)
        {
            this.IUsuariosServicio = IUsuariosServicio;
        }
        public Usuarios Usuario { get; set; }
        public void OnGet(int id)
        {
            try
            {
                BusquedaBindingModel busqueda = new()
                {
                    Tipo_busqueda = "ID USUARIO",
                    Texto_busqueda = id.ToString()
                };

                RestResponseModel response = this.IUsuariosServicio.BuscarUsuarios(busqueda);

                if (response == null)
                    throw new Exception("Error buscando los usuarios");

                if (!response.IsSucess)
                    throw new Exception($"Error buscando los usuarios | {response.Response}");

                JToken jtoken = JToken.Parse(response.Response);

                List<Usuarios> usuarios = JsonConvert.DeserializeObject<List<Usuarios>>(jtoken.ToString());

                if (usuarios == null)
                    throw new Exception("Error buscando los usuarios");

                if (usuarios.Count == 0)
                    throw new Exception("Error buscando los usuarios");

                this.Usuario = usuarios[0];
            }
            catch (Exception)
            {

            }
        }
        public IActionResult OnPostActualizarUsuario()
        {
            try
            {
                var id = Request.Form["txtidUsuario"];
                var nombre = Request.Form["txtnombreUsuario"];
                var fecha = Request.Form["txtfecha"];
                var genero = Request.Form["listGenero"];

                Usuarios us = new()
                {
                    Id_usuario = Convert.ToInt32(id),
                    Nombre_usuario = nombre,
                    Fecha_nacimiento = Convert.ToDateTime(fecha),
                    Genero = genero,
                    Estado_usuario = "ACTIVO",
                };

                RestResponseModel response = this.IUsuariosServicio.EditarUsuario(us);

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
        public IActionResult OnPostEliminarUsuario()
        {
            try
            {
                var id = Request.Form["txtidUsuarioEliminar"];

                RestResponseModel response = this.IUsuariosServicio.EliminarUsuario(Convert.ToInt32(id));

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
