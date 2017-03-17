using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NotificadorExistencias.Datos;

namespace NotificadorExistencias.Negocio
{
    public class ConsultasNegocio : IConsultasNegocio
    {
        IConsultasDatos _consultadatos;

        public ConsultasNegocio()
        {
            this._consultadatos = new ConsultasDatos();
        }

        public bool pruebaConn()
        {
            return this._consultadatos.pruebaConn();
        }


        public List<Modelos.Notificacion> getInsumos()
        {
            return this._consultadatos.getInsumos();
        }


        public string getExisInsumo(string idInsumo, bool mostrarXAlmacen, decimal falta)
        {
            List<Modelos.Almacen> almacen = new List<Modelos.Almacen>();

            if (mostrarXAlmacen)
                almacen = this._consultadatos.getAlmacenes();

            return this._consultadatos.getExistencias(idInsumo, mostrarXAlmacen, almacen, falta);
        }
    }
}
