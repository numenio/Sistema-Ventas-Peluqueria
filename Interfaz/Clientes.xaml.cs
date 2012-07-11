using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Data;
using ProcesosNegocio;


namespace Interfaz
{
	/// <summary>
	/// Lógica de interacción para Clientes.xaml
	/// </summary>
	public partial class Clientes : Window
	{
        private ObservableCollection<cliente> misClientes;
        private bool swModificarCliente;

		public Clientes()
		{
			this.InitializeComponent();
			
			// A partir de este punto se requiere la inserción de código para la creación del objeto.
		}

        //ingresar clientes
        private void btnIngresarCliente_Click(object sender, RoutedEventArgs e)
        {
            //se cargan los datos del cliente menos tel y mails
            cliente cli = new cliente();
            cli.Nombre = txtNombre.Text;
            cli.Apellido = txtApellido.Text;

            if (!swModificarCliente) //sólo si es ingresar un cliente nuevo y no modificar uno ya existente
            {
                //se comprueba que no haya otro cliente con el mismo nombre
                foreach (cliente clienteGuardado in misClientes)
                {
                    if (clienteGuardado.Nombre.ToLower() == cli.Nombre.ToLower() && clienteGuardado.Apellido.ToLower() == cli.Apellido.ToLower())
                    {
                        MessageBox.Show("Ya hay un cliente ingresado con el nombre " + cli.Nombre + " " + cli.Apellido + ". Por favor ingrese otro nombre distinto.", "Atención");
                        return;
                    }
                }
            }

            cli.Dirección = txtDirección.Text;
            cli.Peluquería = txtPeluquería.Text;
            cli.Localidad = txtLocalidad.Text;
            DateTime fecha = DateTime.Now;
            try
            { fecha = DateTime.Parse(txtFechaNacimiento.Text); } //si hay una fecha válida
            catch
            { MessageBox.Show("No ha escribo bien la fecha de nacimiento. Hay que escribirla dd/mm/aaaa. La d es día, m es mes, y a es año. O sea que son dos números para el día, dos para el mes, y cuatro para el año. Por ejemplo 02/04/1980.", "Error en la fecha"); return; }

            cli.fecha_nacimiento = fecha.ToShortDateString();

            //se guardan los teléfonos  
            if (txtTeléfono.Text != "") cli.Teléfonos.Add(new Teléfono(txtTeléfono.Text)); //lo que está en el txt
            foreach (string str in listBoxTeléfonos.Items) //lo que está en la lista
            {
                cli.Teléfonos.Add(new Teléfono(str));
            }
            //se guardan los mails
            if (txtMail.Text != "") cli.Emails.Add(new Mail(txtMail.Text)); //lo que está en el txt mails
            foreach (string str in listBoxMail.Items) //lo que está en la lista
            {
                cli.Emails.Add(new Mail(str));
            }

            pago unPago = new pago();

            if (!swModificarCliente)
            {
                cli.guardar();
                //se muestra el agregado en el grid
                misClientes.Add(cli);
            }
            else
            {
                cliente unCliente = (cliente)dataGridClientes.SelectedItem;
                cli.Id = unCliente.Id;
                cli.actualizar();
                swModificarCliente = false;
                misClientes.Clear();
                misClientes = new ObservableCollection<cliente>(cliente.cargarTodos());
                dataGridClientes.DataContext = misClientes;
            }
            
            iniciarCampos();
            tabItem2.Focus();
        }

        //arreglar que no se ponga cualquier verdura en tel y mail
        private void btnAgregarTeléfono_Click(object sender, RoutedEventArgs e)
        {
            listBoxTeléfonos.Items.Add(txtTeléfono.Text);
            txtTeléfono.Text = "";
            txtTeléfono.Focus();
        }

        private void btnAgregarMail_Click(object sender, RoutedEventArgs e)
        {
            listBoxMail.Items.Add(txtMail.Text);
            txtMail.Text = "";
            txtMail.Focus();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            misClientes = new ObservableCollection<cliente> (cliente.cargarTodos());
            dataGridClientes.DataContext = misClientes;
            btnCancelarModificarCliente.Visibility = Visibility.Hidden;
            if (usuario.usuarioActual.nivel_seguridad != 0) //si no es administrador
            {
                tabItem1.Visibility = Visibility.Hidden;
                lblEliminarCliente.Visibility = Visibility.Hidden;
                lblModificarCliente.Visibility = Visibility.Hidden;
                tabItem2.Focus();
            }
        }

        private void dataGridClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridClientes.SelectedItem != null)
            {
                try
                {
                    cliente miCliente = (cliente)dataGridClientes.SelectedItem;

                    if (miCliente != null)
                    {
                        if (miCliente.Teléfonos != null)
                        {
                            //ObservableCollection<teléfono> teléfonos = new ObservableCollection<teléfono>();
                            //foreach (string número in miCliente.Teléfonos)
                            //{
                            //    teléfono unNúmero = new teléfono();
                            //    unNúmero.numero = número;
                            //    teléfonos.Add(unNúmero);
                            //}
                            //dataGridTeléfonos.DataContext = teléfonos;
                            List<string> teléfonos = new List<string>();
                            foreach (Teléfono tel in miCliente.Teléfonos)
                                teléfonos.Add(tel.Número);
                            listTeléfonos.DataContext = new ObservableCollection<string>(teléfonos);
                        }


                        if (miCliente.Emails != null)
                        {
                            //ObservableCollection<mails> emails = new ObservableCollection<mails>();
                            //foreach (string mail in miCliente.Emails)
                            //{
                            //    mails unEmail = new mails();
                            //    unEmail.mail = mail;
                            //    emails.Add(unEmail);
                            //}
                            //dataGridMails.DataContext = emails;
                            List<string> emails = new List<string>();
                            foreach (Mail unEmail in miCliente.Emails)
                                emails.Add(unEmail.Email);
                            listMails.DataContext = new ObservableCollection<string>(emails);
                        }
                    }
                }
                catch 
                {
                    
                }
                
            }
        }

        private void lblQuitarTeléfono_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (listBoxTeléfonos.SelectedItem != null)
                listBoxTeléfonos.Items.Remove(listBoxTeléfonos.SelectedItem);
        }

        private void lblMail_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (listBoxMail.SelectedItem != null)
                listBoxMail.Items.Remove(listBoxMail.SelectedItem);
        }

        private void lblEliminarCliente_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show ("Sólo es recomendable eliminar un cliente si aún no le ha vendido nada. ¿Está seguro de Eliminar al cliente seleccionado? Se van a borrar también todas sus ventas, los artículos que salieron en esas ventas, y pagos. Recomendamos no eliminar clientes.","Atención!",MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    cliente unCliente;
                    if (dataGridClientes.SelectedItem != null)
                    {
                        unCliente = (cliente)dataGridClientes.SelectedItem;
                        cliente.eliminar(unCliente.Id);
                        misClientes.Remove(unCliente);
                    }
                }
                catch
                {
                    
                }
            }
        }

        private void iniciarCampos()
        {
            //if (modificarCliente)
            //{
            //    btnIngresarCliente.Content = "Guardar Modificaciones";
            //    btnCancelarModificarCliente.Visibility = Visibility.Hidden;
            //}
            //else
            //{
                btnIngresarCliente.Content = "Ingresar Cliente";
                btnCancelarModificarCliente.Visibility = Visibility.Hidden;
            //}

            txtNombre.Text = "";
            txtApellido.Text = "";
            txtDirección.Text = "";
            txtLocalidad.Text = "";
            txtPeluquería.Text = "";
            txtTeléfono.Text = "";
            txtMail.Text = "";
            listBoxMail.Items.Clear();
            listBoxTeléfonos.Items.Clear();
            txtFechaNacimiento.Text = "Elija fecha";
            tabItem2.IsEnabled = true;
        }

        private void btnCancelarModificarCliente_Click(object sender, RoutedEventArgs e)
        {
            swModificarCliente = false;
            iniciarCampos();
            tabItem2.Focus();
        }

        private void lblModificarCliente_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (dataGridClientes.SelectedItem != null)
                {
                    cliente unCliente = (cliente)dataGridClientes.SelectedItem;

                    btnIngresarCliente.Content = "Guardar Modificaciones";
                    btnCancelarModificarCliente.Visibility = Visibility.Visible;
                    txtNombre.Text = unCliente.Nombre;
                    txtApellido.Text = unCliente.Apellido;
                    txtDirección.Text = unCliente.Dirección;
                    txtLocalidad.Text = unCliente.Localidad;
                    txtPeluquería.Text = unCliente.Peluquería;
                    foreach (Teléfono tel in unCliente.Teléfonos)
                        listBoxTeléfonos.Items.Add(tel.Número);
                    foreach (Mail email in unCliente.Emails)
                        listBoxMail.Items.Add(email.Email);
                    txtFechaNacimiento.Text = unCliente.fecha_nacimiento;
                    swModificarCliente = true;
                    tabItem2.IsEnabled = false;
                    tabItem1.Focus();
                }
            }
            catch
            {

            }
        }
	}
}