using Newtonsoft.Json;
using SISPruebaTecnica.AccesoDatos.Interfaces;
using SISPruebaTecnica.Entidades.Modelos;
using SISPruebaTecnica.Entidades.ModelosConfiguracion;
using SISPruebaTecnica.Negocio.Interfaces;
using System.Data;

namespace SISPruebaTecnica.Negocio.Servicios
{
    public class UsuariosServicio : IUsuariosServicio
    {
        public IUsuariosDac UsuariosDac { get; set; }   
        public UsuariosServicio(IUsuariosDac UsuariosDac)
        {
            this.UsuariosDac = UsuariosDac;
        }
        private bool Comprobaciones(Usuarios usuario, out string rpta)
        {
            rpta = "OK";
            try
            {
                if (string.IsNullOrEmpty(usuario.Nombre_usuario))
                    throw new Exception("El nombre no puede estar vacío");

                if (string.IsNullOrEmpty(usuario.Genero))
                    throw new Exception("El genero no puede estar vacío");

                if (usuario.Fecha_nacimiento > DateTime.Now)
                    throw new Exception("La fecha no puede ser mayor a la fecha actual");

                if (string.IsNullOrEmpty(usuario.Estado_usuario))
                    usuario.Estado_usuario = "ACTIVO";

                return true;
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
                return false;
            }
        }
        public RestResponseModel InsertarUsuario(Usuarios usuario)
        {
            RestResponseModel response = new();
            try
			{
                if (!this.Comprobaciones(usuario, out string rpta))
                    throw new Exception(rpta);

                rpta = this.UsuariosDac.InsertarUsuario(usuario);

                if (!rpta.Equals("OK"))
                    throw new Exception($"No se insertó el usuario | {rpta}");

                response.IsSucess = true;
                response.Response = JsonConvert.SerializeObject(usuario);
            }
			catch (Exception ex)
			{
                response.IsSucess = false;
                response.Response = ex.Message;
            }
            return response;
        }
        private bool ComprobacionesEditar(Usuarios usuario, out string rpta)
        {
            rpta = "OK";
            try
            {
                if (usuario.Id_usuario == 0)
                    throw new Exception("El id del usuario no puede estar en 0");

                if (string.IsNullOrEmpty(usuario.Nombre_usuario))
                    throw new Exception("El nombre no puede estar vacío");

                if (string.IsNullOrEmpty(usuario.Genero))
                    throw new Exception("El genero no puede estar vacío");

                if (usuario.Fecha_nacimiento > DateTime.Now)
                    throw new Exception("La fecha no puede ser mayor a la fecha actual");

                return true;
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
                return false;
            }
        }
        public RestResponseModel EditarUsuario(Usuarios usuario)
        {
            RestResponseModel response = new();
            try
            {
                if (!this.ComprobacionesEditar(usuario, out string rpta))
                    throw new Exception(rpta);

                rpta = this.UsuariosDac.EditarUsuario(usuario);

                if (!rpta.Equals("OK"))
                    throw new Exception($"No se editó el usuario | {rpta}");

                response.IsSucess = true;
                response.Response = JsonConvert.SerializeObject(usuario);
            }
            catch (Exception ex)
            {
                response.IsSucess = false;
                response.Response = ex.Message;
            }
            return response;
        }
        public RestResponseModel BuscarUsuarios(BusquedaBindingModel busqueda)
        {
            RestResponseModel response = new();
            try
            {
                if (string.IsNullOrEmpty(busqueda.Tipo_busqueda))
                    throw new Exception("El tipo de búsqueda no puede estar vacío");

                if (string.IsNullOrEmpty(busqueda.Texto_busqueda))
                    throw new Exception("El texto de búsqueda no puede estar vacío");

                string rpta = 
                    this.UsuariosDac.BuscarUsuarios(busqueda.Tipo_busqueda, 
                    busqueda.Texto_busqueda, out DataTable dtUsuarios);

                List<Usuarios> usuarios = new();

                if (dtUsuarios == null)
                {
                    if (rpta.Equals("OK"))
                    {
                        usuarios.Add(new Usuarios()
                        {
                            Nombre_usuario = "NO HAY USUARIOS",
                            Fecha_nacimiento = DateTime.Now,
                            Genero = "N/A",
                            Estado_usuario = "N/A",
                        });

                        response.IsSucess = true;
                        response.Response = JsonConvert.SerializeObject(usuarios);
                        return response;
                    }
                    else
                        throw new Exception($"Error | {rpta}");
                }    

                usuarios = (from DataRow row in dtUsuarios.Rows
                                           select new Usuarios(row)).ToList();
                response.IsSucess = true;
                response.Response = JsonConvert.SerializeObject(usuarios);
            }
            catch (Exception ex)
            {
                response.IsSucess = false;
                response.Response = ex.Message;
            }
            return response;
        }
        public RestResponseModel EliminarUsuario(int id_usuario)
        {
            RestResponseModel response = new();
            try
            {
                string rpta = this.UsuariosDac.EliminarUsuario(id_usuario);

                if (!rpta.Equals("OK"))
                    throw new Exception($"No se eliminó el usuario | {rpta}");

                response.IsSucess = true;
                response.Response = JsonConvert.SerializeObject(new { Id_usuario = id_usuario });
            }
            catch (Exception ex)
            {
                response.IsSucess = false;
                response.Response = ex.Message;
            }
            return response;
        }
    }
}
