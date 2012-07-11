using System;
using System.Collections.Generic;
using System.Text;


namespace ProcesosNegocio
{
    public class venta
    {
        public venta()
        {
            misArtículos = new List<ventaArtículo>();
            _fecha = DateTime.Now;
            _hora = DateTime.Now;
            _idCliente = 0;
        }

        private int _idCliente;

        public int idCliente
        {
            get { return _idCliente; }
            set { _idCliente = value; }
        }

        private List<ventaArtículo> misArtículos;

        public List<ventaArtículo> MisArtículos
        {
            get { return misArtículos; }
            set { misArtículos = value; }
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

        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }


        public bool guardar(pago miPago)
        {
            try
            {
                AdminData ad = new AdminData();
                ad.guardarVenta(this, miPago);
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public bool eliminar()
        {
            try
            {
                AdminData ad = new AdminData();
                ad.eliminarVenta(this);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool actualizar(pago miPago)
        {
            try
            {
                AdminData ad = new AdminData();
                ad.modificarVenta(this, miPago);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static List<filaParaImprimirVenta> presentarVentas(DateTime fechaInicio, DateTime fechaFin)
        {
            AdminData ad = new AdminData();
            return ad.armarListaVentasParaImprimir(fechaInicio, fechaFin);
        }
    }

    public class ventaArtículo
    {
        public int idArtículo { get; set; }
        public double cantidad { get; set; }
        public double precio  { get; set; }
    }
}
