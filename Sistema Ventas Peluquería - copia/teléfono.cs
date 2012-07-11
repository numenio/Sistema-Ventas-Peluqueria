using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaPeluquería
{
    public class teléfono
    {
        private double _número;
        private int _id;
        private int _idCliente;

        public double Número
        {
            get { return _número; }
            set { _número = value; }
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

        public int idCliente
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    }
}
