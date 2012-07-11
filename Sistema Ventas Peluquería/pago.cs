using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ProcesosNegocio
{
    public class pago
    {
        public int Id { get; set; }

        private double _monto;

        public double Monto
        {
            get { return _monto; }
            set { _monto = value; }
        }
        private string _fecha;

        public string Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        private int _idVenta;

        public int IdVenta
        {
            get { return _idVenta; }
            set { _idVenta = value; }
        }

        private int _idCliente;

        public int IdCliente
        {
            get { return _idCliente; }
            set { _idCliente = value; }
        }

        public bool guardarPago ()
        {
            try
            {
                AdminData ad = new AdminData();
                ad.guardarPago(this);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void eliminar()
        {
            AdminData ad=new AdminData();
            ad.eliminarPago(this.Id);
        }

        public static List<pago> cargarTodos(int idCliente, DateTime fecha)
        {
            AdminData ad = new AdminData();
            return ad.recuperarTodosLosPagos(idCliente, fecha);
        }

        public static List<pagoParaMostrar> cargarTodosParaMostrar(int idCliente, DateTime fecha)
        {
            AdminData ad = new AdminData();
            List<pagoParaMostrar> misPagos = ad.recuperarTodosLosPagosParaMostrar(idCliente, fecha);//new List<pagoParaMostrar>();
            return misPagos;
        }
    }

    public class pagoParaMostrar
    {
        public int id { get; set; }
        public int idCliente { get; set; }
        public int idVenta { get; set; }
        public double monto { get; set; }
        public string nombreCliente { get; set; }
        public string descripciónVenta { get; set; }
        public string fecha { get; set; }
    }
}
