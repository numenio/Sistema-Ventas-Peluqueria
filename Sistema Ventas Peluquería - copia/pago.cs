using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaPeluquería
{
    public class pago
    {
        private double _monto;

        public double Monto
        {
            get { return _monto; }
            set { _monto = value; }
        }
        private DateTime _fecha;

        public DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private int _idCliente;

        public int IdCliente
        {
            get { return _idCliente; }
            set { _idCliente = value; }
        }

        public static bool guardarPago ()
        {
            try
            {


                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
