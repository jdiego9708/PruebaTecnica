using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SISPruebaTecnica.Entidades.Modelos;
using SISPruebaTecnica.Entidades.ModelosConfiguracion;
using SISPruebaTecnica.Negocio.Interfaces;

namespace SISPruebaTecnica.Cliente.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IUsuariosServicio IUsuariosServicio;
        public IndexModel(ILogger<IndexModel> logger,
            IUsuariosServicio IUsuariosServicio)
        {
            _logger = logger;

            this.IUsuariosServicio = IUsuariosServicio;
        }
        public List<Usuarios> Usuarios { get; set; }
        public void OnGet()
        {
            this.ObtenerUsuarios();
        }
        private void ObtenerUsuarios()
        {
            try
            {
                BusquedaBindingModel busqueda = new()
                {
                    Tipo_busqueda = "TODOS",
                    Texto_busqueda = "TODOS"
                };

                RestResponseModel response = this.IUsuariosServicio.BuscarUsuarios(busqueda);

                if (response == null)
                    throw new Exception("Error buscando los usuarios");

                if (!response.IsSucess)
                    throw new Exception($"Error buscando los usuarios | {response.Response}");

                JToken jtoken = JToken.Parse(response.Response);

                this.Usuarios = JsonConvert.DeserializeObject<List<Usuarios>>(jtoken.ToString());
            }
            catch (Exception)
            {

            }
        }     
        public void EditUsuario(int id_usuario)
        {

        }
    }
}