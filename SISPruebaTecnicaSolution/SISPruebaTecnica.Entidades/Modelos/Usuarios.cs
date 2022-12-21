using System.Data;

namespace SISPruebaTecnica.Entidades.Modelos
{
    public class Usuarios
    {
        public Usuarios()
        {

        }
        public Usuarios(DataRow row)
        {
            this.Id_usuario = Convert.ToInt32(row["Id_usuario"]);
            this.Nombre_usuario = Convert.ToString(row["Nombre_usuario"]);
            this.Fecha_nacimiento = Convert.ToDateTime(row["Fecha_nacimiento"]);
            this.Genero = Convert.ToString(row["Genero"]);
            this.Estado_usuario = Convert.ToString(row["Estado_usuario"]);
        }
        public int Id_usuario { get; set; }
        public string Nombre_usuario { get; set; }
        public DateTime Fecha_nacimiento { get; set; }
        public string Genero { get; set; }
        public string Estado_usuario { get; set; }

        public string Fecha_nacimiento_vista
        {
            get
            {
                return $"{this.Fecha_nacimiento:yyyy-MM-dd}";
            }
        }
    }
}
