using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NotificadorExistencias.Modelos;
using System.Data.SqlClient;

namespace NotificadorExistencias.Datos
{
    public class ConsultasDatos : IConsultasDatos
    {
        // Variable que almacena el estado de la conexión a la base de datos
        IConexion _conexion;

        public ConsultasDatos()
        {
            // Establece la cadena de conexión
            _conexion = new Conexion(Modelos.ConectionString.conn);
        }

        // realiza una prueba de conexion
        public bool pruebaConn()
        {
            using (var conn = this._conexion.getConexion())
            {
                try
                {
                    conn.Open();
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }

            }
        }

        // obtiene todos los insumos registrados
        public List<Notificacion> getInsumos()
        {
            List<Notificacion> result = new List<Notificacion>();
            Notificacion ent;

            string sql =
                "select idinsumo, descripcion " +
                "from insumos " +
                "order by idgruposi";

            // define conexion con la cadena de conexion
            using (var conn = this._conexion.getConexion())
            {
                // abre la conexion
                conn.Open();

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;

                    ManejoSql res = Utilerias.EjecutaSQL(sql, cmd);

                    if (res.ok)
                    {
                        while (res.reader.Read())
                        {
                            ent = new Notificacion();

                            ent.idInsumo = Convert.ToString(res.reader["idinsumo"]);
                            ent.insumo = Convert.ToString(res.reader["descripcion"]);

                            result.Add(ent);
                        }
                    }
                    else
                        throw new Exception(res.numErr + ": " + res.descErr);

                    // cerrar el reader
                    res.reader.Close();

                }
            }

            return result;
        }

        // obtiene todos los almacenes
        public List<Modelos.Almacen> getAlmacenes()
        {
            List<Modelos.Almacen> result = new List<Modelos.Almacen>();
            Modelos.Almacen ent;

            string sql =
                "select idalmacen, nombre " +
                "from almacen ";

            // define conexion con la cadena de conexion
            using (var conn = this._conexion.getConexion())
            {
                // abre la conexion
                conn.Open();

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;

                    ManejoSql res = Utilerias.EjecutaSQL(sql, cmd);

                    if (res.ok)
                    {
                        while (res.reader.Read())
                        {
                            ent = new Modelos.Almacen();

                            ent.idAlmacen = Convert.ToString(res.reader["idalmacen"]);
                            ent.almacen = Convert.ToString(res.reader["nombre"]);

                            result.Add(ent);
                        }
                    }
                    else
                        throw new Exception(res.numErr + ": " + res.descErr);

                    // cerrar el reader
                    res.reader.Close();

                }
            }

            return result;
        }

        // regresa una leyenda con la existencia del insumo
        // en caso de tener mas de las existencias proporcionadas regresara vacio
        public string getExistencias(string idInsumo, bool mostrarXAlmacen, List<Almacen> almacen, decimal falta)
        {
            string result = string.Empty;
            // define conexion con la cadena de conexion
            using (var conn = this._conexion.getConexion())
            {
                // abre la conexion
                conn.Open();

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;

                    if (mostrarXAlmacen)
                    {
                        string sqlA =
                            "select SUM(cantidad) as cantidad from movsinv where idinsumo = @idInsumo and idalmacen = @idalmacen";

                        foreach (Almacen al in almacen)
                        {
                            cmd.Parameters.AddWithValue("idInsumo", idInsumo);
                            cmd.Parameters.AddWithValue("idalmacen", al.idAlmacen);

                            ManejoSql res = Utilerias.EjecutaSQL(sqlA, cmd);

                            if (res.ok)
                            {
                                while (res.reader.Read())
                                {
                                    decimal cantidad = res.reader["cantidad"] == DBNull.Value ? 0 : Convert.ToDecimal(res.reader["cantidad"]);

                                    if(cantidad < falta)
                                        result += al.almacen + ": " + cantidad + Environment.NewLine;
                                }
                            }
                            else
                                throw new Exception(res.numErr + ": " + res.descErr);

                            // cerrar el reader
                            res.reader.Close();

                            cmd.Parameters.Clear();
                        }

                        if(result.Length > 0) result = result.Substring(0, result.Length - 1);
                    }
                    else
                    {
                        string sql = "select SUM(cantidad) as cantidad from movsinv where idinsumo = @idInsumo";

                        cmd.Parameters.AddWithValue("idInsumo", idInsumo);

                        ManejoSql res = Utilerias.EjecutaSQL(sql, cmd);

                        if (res.ok)
                        {
                            while (res.reader.Read())
                            {
                                decimal cantidad = res.reader["cantidad"] == DBNull.Value ? 0 : Convert.ToDecimal(res.reader["cantidad"]);

                                if (cantidad < falta)
                                    result = cantidad.ToString();
                            }
                        }
                        else
                            throw new Exception(res.numErr + ": " + res.descErr);

                        // cerrar el reader
                        res.reader.Close();

                        cmd.Parameters.Clear();
                    }
                }
            }
            return result;
        }
    }
}
