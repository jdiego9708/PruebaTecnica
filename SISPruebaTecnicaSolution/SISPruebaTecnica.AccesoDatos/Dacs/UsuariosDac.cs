using SISPruebaTecnica.AccesoDatos.Interfaces;
using SISPruebaTecnica.Entidades.Modelos;
using System.Data;
using System.Data.SqlClient;

namespace SISPruebaTecnica.AccesoDatos.Dacs
{
    public class UsuariosDac : IUsuariosDac
    {
        #region CONSTRUCTOR E INYECCION DE DEPENDENCIAS
        private readonly IConexionDac Conexion;
        public UsuariosDac(IConexionDac Conexion)
        {
            this.Conexion = Conexion;
        }
        #endregion

        #region MENSAJE
        private void SqlCon_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            string mensaje_error = e.Message;
            if (e.Errors != null)
            {
                if (e.Errors.Count > 0)
                {
                    mensaje_error += string.Join("|", e.Errors);
                }
            }
            this.Mensaje_error = mensaje_error;
        }
        #endregion

        #region PROPIEDADES
        public string Mensaje_error { get; set; }
        #endregion

        #region MÉTODO INSERTAR USUARIO
        public string InsertarUsuario(Usuarios usuario)
        {
            //Inicializamos la respuesta que vamos a devolver
            string rpta = "OK";
            SqlConnection SqlCon = new();
            try
            {
                //Asignamos un evento SqlInfoMessage para obtener errores con severidad < 10 desde SQL
                SqlCon.InfoMessage += new SqlInfoMessageEventHandler(SqlCon_InfoMessage);
                SqlCon.FireInfoMessageEventOnUserErrors = true;
                //Asignamos la cadena de conexión desde un método estático que lee el archivo de configuracion
                SqlCon.ConnectionString = Conexion.Cn();
                //Abrimos la conexión.
                SqlCon.Open();
                //Creamos un comando para ejecutar un procedimiento almacenado
                SqlCommand SqlCmd = new()
                {
                    Connection = SqlCon,
                    CommandText = "sp_Usuarios_i",
                    CommandType = CommandType.StoredProcedure
                };
                //Creamos cada parámetro y lo agregamos a la lista de parámetros del comando
                //El primer comando es el id del usuario que es parámetro de salida
                SqlParameter Id_usuario = new()
                {
                    ParameterName = "Id_usuario",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output,
                };
                SqlCmd.Parameters.Add(Id_usuario);
                //Los parámetros varchar se les asigna una propiedad extra y es el Size
                SqlParameter Nombre_usuario = new()
                {
                    ParameterName = "Nombre_usuario",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = usuario.Nombre_usuario,
                };
                SqlCmd.Parameters.Add(Nombre_usuario);

                SqlParameter Fecha_nacimiento = new()
                {
                    ParameterName = "Fecha_nacimiento",
                    SqlDbType = SqlDbType.Date,
                    Value = usuario.Fecha_nacimiento,
                };
                SqlCmd.Parameters.Add(Fecha_nacimiento);

                SqlParameter Genero = new()
                {
                    ParameterName = "Genero",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 1,
                    Value = usuario.Genero,
                };
                SqlCmd.Parameters.Add(Genero);

                SqlParameter Estado_usuario = new()
                {
                    ParameterName = "Estado_usuario",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = usuario.Estado_usuario,
                };
                SqlCmd.Parameters.Add(Estado_usuario);

                //Ejecutamos nuestro comando cuando agreguemos todos los parámetros requeridos
                rpta = SqlCmd.ExecuteNonQuery() > 0 ? "OK" : "ERROR";

                //Comprobamos la variable de respuesta Mensaje_error que guarda el mensaje específico
                //De cualquier error generado en SQL procedimiento almacenado
                if (!rpta.Equals("OK"))
                    if (!string.IsNullOrEmpty(this.Mensaje_error))
                        rpta = this.Mensaje_error;
                //Obtenemos el id usuario y lo asignamos a la instancia existente de usuario para usarlo después
                usuario.Id_usuario = Convert.ToInt32(SqlCmd.Parameters["Id_usuario"].Value);
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open)
                    SqlCon.Close();
            }
            return rpta;
        }
        #endregion

        #region MÉTODO EDITAR USUARIO
        public string EditarUsuario(Usuarios usuario)
        {
            //Inicializamos la respuesta que vamos a devolver
            string rpta = "OK";
            SqlConnection SqlCon = new();
            try
            {
                //Asignamos un evento SqlInfoMessage para obtener errores con severidad < 10 desde SQL
                SqlCon.InfoMessage += new SqlInfoMessageEventHandler(SqlCon_InfoMessage);
                SqlCon.FireInfoMessageEventOnUserErrors = true;
                //Asignamos la cadena de conexión desde un método estático que lee el archivo de configuracion
                SqlCon.ConnectionString = Conexion.Cn();
                //Abrimos la conexión.
                SqlCon.Open();
                //Creamos un comando para ejecutar un procedimiento almacenado
                SqlCommand SqlCmd = new()
                {
                    Connection = SqlCon,
                    CommandText = "sp_Usuarios_u",
                    CommandType = CommandType.StoredProcedure
                };
                //Creamos cada parámetro y lo agregamos a la lista de parámetros del comando
                //El primer comando es el id del usuario que es parámetro de salida
                SqlParameter Id_usuario = new()
                {
                    ParameterName = "Id_usuario",
                    SqlDbType = SqlDbType.Int,
                    Value = usuario.Id_usuario,
                };
                SqlCmd.Parameters.Add(Id_usuario);
                //Los parámetros varchar se les asigna una propiedad extra y es el Size
                SqlParameter Nombre_usuario = new()
                {
                    ParameterName = "Nombre_usuario",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = usuario.Nombre_usuario,
                };
                SqlCmd.Parameters.Add(Nombre_usuario);

                SqlParameter Fecha_nacimiento = new()
                {
                    ParameterName = "Fecha_nacimiento",
                    SqlDbType = SqlDbType.Date,
                    Value = usuario.Fecha_nacimiento,
                };
                SqlCmd.Parameters.Add(Fecha_nacimiento);

                SqlParameter Genero = new()
                {
                    ParameterName = "Genero",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 1,
                    Value = usuario.Genero,
                };
                SqlCmd.Parameters.Add(Genero);

                SqlParameter Estado_usuario = new()
                {
                    ParameterName = "Estado_usuario",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = usuario.Estado_usuario,
                };
                SqlCmd.Parameters.Add(Estado_usuario);

                //Ejecutamos nuestro comando cuando agreguemos todos los parámetros requeridos
                rpta = SqlCmd.ExecuteNonQuery() > 0 ? "OK" : "ERROR";

                //Comprobamos la variable de respuesta Mensaje_error que guarda el mensaje específico
                //De cualquier error generado en SQL procedimiento almacenado
                if (!rpta.Equals("OK"))
                    if (!string.IsNullOrEmpty(this.Mensaje_error))
                        rpta = this.Mensaje_error;
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open)
                    SqlCon.Close();
            }
            return rpta;
        }
        #endregion

        #region MÉTODO BUSCAR USUARIOS
        public string BuscarUsuarios(string tipo_busqueda, string texto_busqueda,
            out DataTable dtUsuarios)
        {
            //Inicializamos la respuesta que vamos a devolver
            dtUsuarios = new();
            string rpta = "OK";
            SqlConnection SqlCon = new();
            try
            {
                //Asignamos un evento SqlInfoMessage para obtener errores con severidad < 10 desde SQL
                SqlCon.InfoMessage += new SqlInfoMessageEventHandler(SqlCon_InfoMessage);
                SqlCon.FireInfoMessageEventOnUserErrors = true;
                //Asignamos la cadena de conexión desde un método estático que lee el archivo de configuracion
                SqlCon.ConnectionString = Conexion.Cn();
                //Abrimos la conexión.
                SqlCon.Open();
                //Creamos un comando para ejecutar un procedimiento almacenado
                SqlCommand SqlCmd = new()
                {
                    Connection = SqlCon,
                    CommandText = "sp_Usuarios_g",
                    CommandType = CommandType.StoredProcedure
                };
                //Creamos cada parámetro y lo agregamos a la lista de parámetros del comando
                //El primer comando es el id del usuario que es parámetro de salida
                SqlParameter Tipo_busqueda = new()
                {
                    ParameterName = "Tipo_busqueda",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = tipo_busqueda
                };
                SqlCmd.Parameters.Add(Tipo_busqueda);
                //Los parámetros varchar se les asigna una propiedad extra y es el Size
                SqlParameter Texto_busqueda = new()
                {
                    ParameterName = "Texto_busqueda",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = texto_busqueda,
                };
                SqlCmd.Parameters.Add(Texto_busqueda);

                //Ejecutamos nuestro comando cuando agreguemos todos los parámetros requeridos
                SqlDataAdapter SqlData = new(SqlCmd);
                SqlData.Fill(dtUsuarios);

                //Comprobamos la variable de respuesta Mensaje_error que guarda el mensaje específico
                //De cualquier error generado en SQL procedimiento almacenado
                if (dtUsuarios == null)
                {
                    if (!string.IsNullOrEmpty(this.Mensaje_error))
                        rpta = this.Mensaje_error;
                }
                else
                {
                    if (dtUsuarios.Rows.Count < 1)
                        dtUsuarios = null;
                }
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
                dtUsuarios = null;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open)
                    SqlCon.Close();
            }
            return rpta;
        }
        #endregion

        #region MÉTODO ELIMINAR USUARIO
        public string EliminarUsuario(int id_usuario)
        {
            //Inicializamos la respuesta que vamos a devolver
            string rpta = "OK";
            SqlConnection SqlCon = new();
            try
            {
                //Asignamos un evento SqlInfoMessage para obtener errores con severidad < 10 desde SQL
                SqlCon.InfoMessage += new SqlInfoMessageEventHandler(SqlCon_InfoMessage);
                SqlCon.FireInfoMessageEventOnUserErrors = true;
                //Asignamos la cadena de conexión desde un método estático que lee el archivo de configuracion
                SqlCon.ConnectionString = Conexion.Cn();
                //Abrimos la conexión.
                SqlCon.Open();
                //Creamos un comando para ejecutar un procedimiento almacenado
                SqlCommand SqlCmd = new()
                {
                    Connection = SqlCon,
                    CommandText = "sp_Usuarios_d",
                    CommandType = CommandType.StoredProcedure
                };
                //Creamos cada parámetro y lo agregamos a la lista de parámetros del comando
                //El primer comando es el id del usuario que es parámetro de salida
                SqlParameter Id_usuario = new()
                {
                    ParameterName = "Id_usuario",
                    SqlDbType = SqlDbType.Int,
                    Value = id_usuario,
                };
                SqlCmd.Parameters.Add(Id_usuario);

                //Ejecutamos nuestro comando cuando agreguemos todos los parámetros requeridos
                rpta = SqlCmd.ExecuteNonQuery() > 0 ? "OK" : "ERROR";

                //Comprobamos la variable de respuesta Mensaje_error que guarda el mensaje específico
                //De cualquier error generado en SQL procedimiento almacenado
                if (!rpta.Equals("OK"))
                    if (!string.IsNullOrEmpty(this.Mensaje_error))
                        rpta = this.Mensaje_error;
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open)
                    SqlCon.Close();
            }
            return rpta;
        }
        #endregion
    }
}
