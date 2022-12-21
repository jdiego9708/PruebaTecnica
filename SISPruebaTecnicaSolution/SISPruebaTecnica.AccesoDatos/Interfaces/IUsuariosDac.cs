using SISPruebaTecnica.Entidades.Modelos;
using System.Data;

namespace SISPruebaTecnica.AccesoDatos.Interfaces
{
    public interface IUsuariosDac
    {
        string EliminarUsuario(int id_usuario);
        string InsertarUsuario(Usuarios usuario);
        string EditarUsuario(Usuarios usuario);
        string BuscarUsuarios(string tipo_busqueda, string texto_busqueda,
            out DataTable dtUsuarios);
    }
}
