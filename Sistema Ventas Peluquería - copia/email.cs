using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaPeluquería
{
    public class email
    {
        private string _uri;
        private int _id;
        private int _idCliente;

        public string Uri
        {
            get { return _uri; }
            set { _uri = value; }
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
