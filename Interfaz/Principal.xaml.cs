using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ProcesosNegocio;

namespace Interfaz
{
	/// <summary>
	/// Lógica de interacción para Principal.xaml
	/// </summary>
	public partial class Principal : Window
	{
        Stock frmStock = null; 
        Clientes frmClientes = null; 
        Productos frmProductos = null; 
        Ventas frmVentas = null; 
        Pagos frmPagos = null;
        CuentasDeClientes frmCuentas = null;
        PromedioVentas frmPromedios = null;
        AdministrarUsuarios frmUsuarios = null;
        //public usuario usuarioActual;

		public Principal()
		{
			this.InitializeComponent();
			// A partir de este punto se requiere la inserción de código para la creación del objeto.
		}

        private void lblClientes_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.chequearVentanaAbierta("Clientes") == false) frmClientes = new Clientes();
            frmClientes.Show();
        }

        private void lblProductos_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.chequearVentanaAbierta("Productos") == false) frmProductos = new Productos();
            frmProductos.Show();
        }

        private void lblVentas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.chequearVentanaAbierta("Ventas") == false) frmVentas = new Ventas();
            frmVentas.Show();
        }

        private void lblStock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.chequearVentanaAbierta("Añadir a Stock") == false) frmStock = new Stock();
            frmStock.Show();
        }

        private void lblPagos_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.chequearVentanaAbierta("Pagos recibidos") == false) frmPagos = new Pagos();
            frmPagos.Show();
        }

        private bool chequearVentanaAbierta(String miVentana)
        {
            foreach (Window vent in App.Current.Windows)
            {
                if (miVentana == vent.Title)
                {
                    return true;
                }
            }
            return false;
        }

        private void lblCuentas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.chequearVentanaAbierta("Cuentas de Clientes") == false) frmCuentas = new CuentasDeClientes();
            frmCuentas.Show();
        }

        private void label1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.chequearVentanaAbierta("Promedio de Ventas") == false) frmPromedios = new PromedioVentas();
            frmPromedios.Show();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application actual = App.Current;
            actual.Shutdown();
        }

        private void lblAdministrarUsuarios_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.chequearVentanaAbierta("Usuarios") == false) frmUsuarios = new AdministrarUsuarios();
            frmUsuarios.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //si no es administrador, se le oculta administrar usuarios
            if (usuario.usuarioActual.nivel_seguridad != 0) lblAdministrarUsuarios.Visibility = Visibility.Hidden;
        }

        
	}
}