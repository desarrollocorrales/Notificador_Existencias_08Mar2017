using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace NotificadorExistencias.Modelos
{
    public interface IConexion
    {
        SqlConnection getConexion();
    }
}
