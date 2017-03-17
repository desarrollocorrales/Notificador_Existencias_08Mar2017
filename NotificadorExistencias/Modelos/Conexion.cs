using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace NotificadorExistencias.Modelos
{
    public class Conexion : IConexion
    {
        private string _cademaConexion;

        public Conexion(string cademaConexion)
        {
            this._cademaConexion = cademaConexion;
        }

        public string getCadena()
        {
            return this._cademaConexion;
        }

        public SqlConnection getConexion()
        {
            return new SqlConnection(this._cademaConexion);
        }
    }
}
