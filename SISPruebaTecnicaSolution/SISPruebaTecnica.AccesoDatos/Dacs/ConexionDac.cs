using Microsoft.Extensions.Configuration;
using SISPruebaTecnica.AccesoDatos.Interfaces;
using SISPruebaTecnica.Entidades.ModelosConfiguracion;

namespace SISPruebaTecnica.AccesoDatos.Dacs
{
    public class ConexionDac : IConexionDac
    {
        private readonly IConfiguration Configuration;
        private readonly ConnectionStrings ConnectionStringsModel;
        public ConexionDac(IConfiguration IConfiguration)
        {
            this.Configuration = IConfiguration;

            var settings = this.Configuration.GetSection("ConnectionStrings");
            this.ConnectionStringsModel = settings.Get<ConnectionStrings>();
        }
        public string Cn()
        {
            if (this.ConnectionStringsModel.ConexionBDPredeterminada.Equals("ConexionBDAzure"))
                return this.ConnectionStringsModel.ConexionBDAzure;
            else
                return this.ConnectionStringsModel.ConexionBDLocal;
        }
    }
}
