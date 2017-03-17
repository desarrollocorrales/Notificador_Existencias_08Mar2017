using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NotificadorExistencias.Datos
{
    public interface IConsultasDatos
    {
        bool pruebaConn();

        List<Modelos.Notificacion> getInsumos();

        List<Modelos.Almacen> getAlmacenes();

        string getExistencias(string idInsumo, bool mostrarXAlmacen, List<Modelos.Almacen> almacen, decimal falta);
    }
}
