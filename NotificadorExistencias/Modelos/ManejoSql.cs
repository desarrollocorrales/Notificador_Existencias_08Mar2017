using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace NotificadorExistencias.Modelos
{
    public class ManejoSql
    {
        public bool ok { get; set; }
        public SqlDataReader reader { get; set; }
        public int numErr { get; set; }
        public string descErr { get; set; }
    }
}
