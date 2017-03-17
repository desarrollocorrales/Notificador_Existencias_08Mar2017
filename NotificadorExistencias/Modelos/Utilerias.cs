using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace NotificadorExistencias.Modelos
{
    public enum Estatus
    {
        OK, ERROR
    }

    public partial class Response
    {
        public Estatus status { get; set; }
        public string resultado { get; set; }
        public string error { get; set; }
    }

    public static class ConectionString
    {
        public static string conn { get; set; }
    }

    public class Utilerias
    {
        /// <summary>
        /// Performs the ROT13 character rotation.
        /// </summary>
        public static string Transform(string value)
        {
            char[] array = value.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                int number = (int)array[i];

                if (number >= 'a' && number <= 'z')
                {
                    if (number > 'm')
                    {
                        number -= 13;
                    }
                    else
                    {
                        number += 13;
                    }
                }
                else if (number >= 'A' && number <= 'Z')
                {
                    if (number > 'M')
                    {
                        number -= 13;
                    }
                    else
                    {
                        number += 13;
                    }
                }
                array[i] = (char)number;
            }
            return new string(array);
        }

        /// <summary>
        /// Ejecuta una sql que rellenar un DataReader (sentencia select).
        /// </summary>
        /// <param name="SqlQuery">sentencia sql a ejecutar</param>
        /// <returns>Estatus de transacción y datos obtenidos</returns> 
        public static ManejoSql EjecutaSQL(string SqlQuery, SqlCommand cmd)
        {
            ManejoSql result = new ManejoSql();

            try
            {
                // Se define el tipo de comando
                cmd.CommandType = CommandType.Text;

                // Se define la instruccion Transact-SQL 
                cmd.CommandText = SqlQuery;

                // Ejecutamos sql.
                result.reader = cmd.ExecuteReader();

                // Se define si la respuesta no generó ningun error
                result.ok = true;

            }
            catch (SqlException sqlex)
            {
                // se define el numero de error segun SqlServer
                result.numErr = sqlex.Number;

                // Se define el mensaje de error
                result.descErr = sqlex.Message;

                // Se define estatus de transaccion
                result.ok = false;
            }
            catch (Exception ex)
            {
                // se define el numero de error segun SqlServer
                result.numErr = 0;

                // Se define el mensaje de error
                result.descErr = ex.Message;

                // Se define estatus de transaccion
                result.ok = false;
            }

            return result;
        }


        /// <summary>
        /// Ejecuta una sql que no devuelve datos (update, delete, insert).
        /// </summary>
        /// <param name="SqlQuery">sentencia sql a ejecutar</param>
        /// <param name="FilasAfectadas">Fila afectadas por la sentencia SQL</param>
        /// <returns></returns>
        public static ManejoSql EjecutaSQL(string SqlQuery, ref int FilasAfectadas, SqlCommand cmd)
        {
            ManejoSql result = new ManejoSql();

            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = SqlQuery;

                // regresa las filas afectadas
                FilasAfectadas = cmd.ExecuteNonQuery();

                result.ok = true;
            }
            catch (SqlException sqlex)
            {
                // se define el numero de error segun SqlServer
                result.numErr = sqlex.Number;

                // Se define el mensaje de error
                result.descErr = sqlex.Message;

                // Se define estatus de transaccion
                result.ok = false;
            }
            catch (Exception ex)
            {
                // se define el numero de error segun SqlServer
                result.numErr = 0;

                // Se define el mensaje de error
                result.descErr = ex.Message;

                // Se define estatus de transaccion
                result.ok = false;
            }

            return result;
        }
    }
}
