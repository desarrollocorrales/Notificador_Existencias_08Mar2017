using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NotificadorExistencias.Modelos
{
    public class Notificacion
    {
        public bool mostrar { get; set; }
        public string idInsumo { get; set; }
        public string insumo { get; set; }
        public string falta { get; set; }
        public bool mostrarXAlmacen { get; set; }
    }
}
