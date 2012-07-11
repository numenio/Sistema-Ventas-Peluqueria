using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;


namespace ProcesosNegocio
{
    public class cliente
    {
        public cliente()
        {
            Teléfonos = new List<Teléfono>();
            Emails = new List<Mail>();
            _nombre = "sin llenar";
            _apellido = "sin llenar";
            _dirección = "sin llenar";
            _localidad = "sin llenar";
            _peluquería = "sin llenar";
            fecha_nacimiento = "sin llenar";
        }

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

        public string fecha_nacimiento {get; set;}

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

        private List<Teléfono> _teléfonos;

        public List<Teléfono> Teléfonos
        {
            get { return _teléfonos; }
            set { _teléfonos = value; }
        }

        private List<Mail> _emails;

        public List<Mail> Emails
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

        public void actualizar()
        {
            AdminData ad = new AdminData();
            ad.modificarCliente(this);
        }

        public static List<int> recuperarValoresVenta(int idArtículo, int idCliente, DateTime fechaInicio, DateTime fechaFin)
        {
            AdminData ad = new AdminData();
            return ad.recuperarValoresVentas(idArtículo, idCliente, fechaInicio, fechaFin);
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

        
        //public static int recuperarIdCliente(string nombre, string apellido, string dirección, string localidad, string peluquería)
        //{
        //    AdminData ad = new AdminData();
        //    return ad.recuperar_ID_cliente(nombre, apellido, dirección, localidad, peluquería);
        //}

        //public static int recuperarIdCliente(string nombre, string apellido)
        //{
        //    AdminData ad = new AdminData();
        //    return ad.recuperar_ID_cliente(nombre, apellido);
        //}

        public static int recuperarIdCliente(string nombre)
        {
            AdminData ad = new AdminData();
            return ad.recuperar_IDcliente(nombre);
        }

        public static string recuperarNombreCliente(int id)
        {
            AdminData ad = new AdminData();
            return ad.recuperar_nombre_cliente(id);
        }

        public static List<string> recuperarTodosLosNombresDeClientes()
        {
            AdminData ad = new AdminData();
            return ad.recuperarTodosLosNombresDeClientes();
        }

        public static List<string> recuperarTodosLosID()
        {
            AdminData ad = new AdminData();
            return ad.recuperarTodosLosIDDeClientes();
        }

        public static saldo recuperarSaldo(int idCliente)
        {
            AdminData ad = new AdminData();
            return ad.recuperarSaldoCliente(idCliente);
        }

        public static void eliminar(int idCliente)
        {
            AdminData ad = new AdminData();
            ad.eliminarCliente(idCliente);
        }
    }

    public class Teléfono
    {
        public int idTeléfono { get; set; }
        public string Número { get; set; }
        public Teléfono() { }
        public Teléfono(string número) { Número = número; }
    }

    public class Mail
    {
        public int idMail { get; set; }
        public string Email { get; set; }
        public Mail() { }
        public Mail(string email) {Email = email; }
    }
}








