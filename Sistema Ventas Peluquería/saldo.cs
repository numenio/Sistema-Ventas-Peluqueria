using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProcesosNegocio
{
    public class saldo
    {
        private int _idCliente;

        public int IdCliente
        {
            get { return _idCliente; }
            set { _idCliente = value; }
        }

        private string _nombre;

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        private double _totales;

        public double Totales
        {
            get { return _totales; }
            set { _totales = value; }
        }

    }
}
