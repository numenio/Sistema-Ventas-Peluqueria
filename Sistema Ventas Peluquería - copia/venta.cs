using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaPeluquería
{
    public class venta
    {
        private int _idCliente;

        public int idCliente
        {
            get { return _idCliente; }
            set { _idCliente = value; }
        }
        private int _idArtículo;

        public int idArtículo
        {
            get { return _idArtículo; }
            set { _idArtículo = value; }
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
        private DateTime _fecha;

        public DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }
        private DateTime _hora;

        public DateTime Hora
        {
            get { return _hora; }
            set { _hora = value; }
        }

        public int id
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        private int _id;
    }
}
