using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NotificadorExistencias.Negocio
{
    public interface IConsultasNegocio
    {
        bool pruebaConn();

        List<Modelos.Notificacion> getInsumos();

        string getExisInsumo(string idInsumo, bool mostrarXAlmacen, decimal falta);
    }
}
