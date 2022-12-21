using SISPruebaTecnica.Entidades.Modelos;
using SISPruebaTecnica.Entidades.ModelosConfiguracion;

namespace SISPruebaTecnica.Negocio.Interfaces
{
    public interface IUsuariosServicio
    {
        RestResponseModel EliminarUsuario(int id_usuario);
        RestResponseModel InsertarUsuario(Usuarios usuario);
        RestResponseModel EditarUsuario(Usuarios usuario);
        RestResponseModel BuscarUsuarios(BusquedaBindingModel busqueda);
    }
}
