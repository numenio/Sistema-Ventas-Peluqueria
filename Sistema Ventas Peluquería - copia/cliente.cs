using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace SistemaPeluquería
{
    public class cliente
    {
        private string _nombre;

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        private string _apellido;

        public string Apellido
        {
            get { return _apellido; }
            set { _apellido = value; }
        }
        private string _dirección;

        public string Dirección
        {
            get { return _dirección; }
            set { _dirección = value; }
        }
        private string _localidad;

        public string Localidad
        {
            get { return _localidad; }
            set { _localidad = value; }
        }
        private string _peluquería;

        public string Peluquería
        {
            get { return _peluquería; }
            set { _peluquería = value; }
        }

        private ArrayList _teléfonos;

        public ArrayList Teléfonos
        {
            get { return _teléfonos; }
            set { _teléfonos = value; }
        }

        private ArrayList _emails;

        public ArrayList Emails
        {
            get { return _emails; }
            set { _emails = value; }
        }

        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public bool guardar()
        {
            try
            {
                AdminData ad = new AdminData();
                ad.guardarCliente(this);
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public static List<cliente> cargarTodos()
        {
            AdminData ad = new AdminData();
            List<cliente> misClientes = ad.recuperarTodosLosClientes();
            return misClientes;
        }

        public static cliente cargarCliente(int idCliente)
        {
            AdminData ad = new AdminData();
            return ad.recuperarCliente (idCliente);
        }
    }
}








