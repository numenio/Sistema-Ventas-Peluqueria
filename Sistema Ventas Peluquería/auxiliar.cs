using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ProcesosNegocio
{
    public class auxiliar
    {
        public static bool chequearDatoNuméricoVálido(List<string> fuenteDatos, string dato)
        {
            if (string.IsNullOrEmpty(dato)) //si la cadena no es nula o vacía
                return false;
            else
            {
                if (esNúmero(dato)) //si la cadena es un número
                {
                    var miRespuesta = from misDatos in fuenteDatos where misDatos.Equals(dato) select misDatos; //se analiza si es un dato válido
                    foreach (var result in miRespuesta) return true;
                    return false;
                }
                return false;
            }
        }

        public static bool chequearDatoCadenaVálido(List<string> fuenteDatos, string dato)
        {
            if (string.IsNullOrEmpty(dato)) //si la cadena no es nula o vacía
                return false;
            else
            {
                var miRespuesta = from misDatos in fuenteDatos where misDatos.Equals(dato) select misDatos; //se analiza si es un dato válido
                foreach (var result in miRespuesta) return true;
                return false;
            }
        }

        public static bool esNúmero(string miCadena) //para saber si una cadena es un número 
        {
            try
            {
                double.Parse(miCadena);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static List<registroVentas> recuperarRegistrosVentas()
        {
            AdminData ad = new AdminData();
            List<registroVentas> misVentas = ad.recuperarRegistrosVentas(DateTime.Now, DateTime.Now, 0, 0);
            return misVentas;
        }

        public static List<registroVentas> recuperarRegistrosVentas(int idCliente, DateTime fechaInicio, DateTime fechaFin)
        {
            AdminData ad = new AdminData();
            List<registroVentas> misVentas = ad.recuperarRegistrosVentas(fechaInicio, fechaFin, idCliente, 0);
            return misVentas;
        }
    }
}
