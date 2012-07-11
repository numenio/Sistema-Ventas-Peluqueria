using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProcesosNegocio
{
    public class registroVentas
    {
        public registroVentas()
        {
            _misArtículos= new List<productoVenta>();
        }

        private string _cliente;

        public string Cliente
        {
            get { return _cliente; }
            set { _cliente = value; }
        }

        //public int id_Venta {get; set;}

        private List<productoVenta> _misArtículos;

        public List<productoVenta> MisArtículos
        {
            get { return _misArtículos; }
            set { _misArtículos = value; }
        }
        
        public double Pago
        {
            get { return _pago; }
            set { _pago = value; }
        }

        private string _fecha;

        public string Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }
        private string _hora;

        public string Hora
        {
            get { return _hora; }
            set { _hora = value; }
        }
        private double _debe;

        public double Debe
        {
            get { return _debe; }
            set { _debe = value; }
        }
        private double _haber;

        public double Haber
        {
            get { return _haber; }
            set { _haber = value; }
        }
        private double _saldo;

        public double Saldo
        {
            get { return _saldo; }
            set { _saldo = value; }
        }

        private double _pago;

        private int _id_Venta;

        public int Id_Venta
        {
            get { return _id_Venta; }
            set { _id_Venta = value; }
        }
    }

    public class productoVenta
    {
        private string _producto;

        public string Producto
        {
            get { return _producto; }
            set { _producto = value; }
        }

        private double _cantidad;

        public double Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }

        private double _precio;

        public double Precio
        {
            get { return _precio; }
            set { _precio = value; }
        }
    }
}
