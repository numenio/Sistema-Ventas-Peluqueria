using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProcesosNegocio
{
    public class filaParaImprimirVenta
    {
        public filaParaImprimirVenta()
        {
            código_Ventas = new List<int>();
        }

        private int _nº;

        public int Nº
        {
            get { return _nº; }
            set { _nº = value; }
        }

        public List<int> código_Ventas { get; set; }

        private string cliente;

        public string Cliente
        {
            get { return cliente; }
            set { cliente = value; }
        }
        private string detalle_compra;

        public string Detalle_compra
        {
            get { return detalle_compra; }
            set { detalle_compra = value; }
        }
        private string debe;

        public string Debe
        {
            get { return debe; }
            set { debe = value; }
        }
        private string haber;

        public string Haber
        {
            get { return haber; }
            set { haber = value; }
        }
        private string saldo;

        public string Saldo
        {
            get { return saldo; }
            set { saldo = value; }
        }
    }
}
