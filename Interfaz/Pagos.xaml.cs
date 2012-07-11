using System;
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
using ProcesosNegocio;

namespace Interfaz
{
	/// <summary>
	/// Lógica de interacción para Pagos.xaml
	/// </summary>
	public partial class Pagos : Window
	{
        private List<string> nombresClientes = new List<string>();
        private List<string> IDsClientes = new List<string>();
        private ObservableCollection<pagoParaMostrar> misPagos;
        
        //private bool swModificarCliente;

		public Pagos()
		{
			this.InitializeComponent();
			
			// A partir de este punto se requiere la inserción de código para la creación del objeto.
		}

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            IDsClientes = cliente.recuperarTodosLosID();

            if (IDsClientes.Count == 0) //si no hay clientes ingresados
            {
                cmbCódigoCliente.Text = "No hay clientes";
                cmbNombreCliente.Text = "No hay clientes";
                MessageBox.Show("No ha ingreso ningún cliente en el programa, por eso no puede ingresar Pagos. Vaya a 'Clientes' y ahí ingrese su cliente nuevo. Luego vuelva aquí a ingresar un Pago.", "Atención");
                btnIngresarPago.IsEnabled = false;
                
                Window.Close();
                return;
            }

            nombresClientes = cliente.recuperarTodosLosNombresDeClientes();
            //se llenan los combos de los clientes
            cmbCódigoCliente.ItemsSource = IDsClientes;
            cmbNombreCliente.ItemsSource = nombresClientes;
            cmbVerCódigoCliente.ItemsSource = IDsClientes;
            cmbVerNombreCliente.ItemsSource = nombresClientes;
            txtPago.Text = "0";
            dateFechaPago.Text = DateTime.Now.ToShortDateString();
            
            iniciarCampos();

            if (usuario.usuarioActual.nivel_seguridad != 0) //si no es administrador
            {
                tabNuevoPago.Visibility = Visibility.Hidden;
                lblEliminarPago.Visibility = Visibility.Hidden;
                tabPagoGuardado.Focus();
            }
        }

        private void cmbCódigoCliente_KeyUp(object sender, KeyEventArgs e)
        {
            //si lo que está escrito es una id válida, se busca el nombre del cliente
            if (auxiliar.chequearDatoNuméricoVálido(IDsClientes, cmbCódigoCliente.Text))
            {
                cmbNombreCliente.Text = cliente.recuperarNombreCliente(int.Parse(cmbCódigoCliente.Text));
            }
            else
            {
                if (cmbCódigoCliente.Text == "")
                {
                    cmbNombreCliente.Text = "";
                }
                else
                {
                    cmbNombreCliente.Text = "No existe";
                }
            }
        }

        private void cmbCódigoCliente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbCódigoCliente.SelectedItem != null)
            {
                if (auxiliar.chequearDatoNuméricoVálido(IDsClientes, cmbCódigoCliente.SelectedItem.ToString())) //si ese número corresponde a una id válida
                {
                    cmbNombreCliente.Text = cliente.recuperarNombreCliente(int.Parse(cmbCódigoCliente.SelectedItem.ToString())); //se busca el nombre de esa id
                }
            }
        }

        private void cmbNombreCliente_KeyUp(object sender, KeyEventArgs e)
        {
            //si lo que hay escrito es un nombre de cliente existente se busca su id
            if (auxiliar.chequearDatoCadenaVálido(nombresClientes, cmbNombreCliente.Text))
            {
                cmbCódigoCliente.Text = cliente.recuperarIdCliente(cmbNombreCliente.Text).ToString();
            }
            else
            {
                cmbCódigoCliente.Text = "0";
            }
        }

        private void cmbNombreCliente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbNombreCliente.SelectedItem != null)
            {
                if (auxiliar.chequearDatoCadenaVálido(nombresClientes, cmbNombreCliente.SelectedItem.ToString()))
                {
                    cmbCódigoCliente.Text = cliente.recuperarIdCliente(cmbNombreCliente.SelectedItem.ToString()).ToString();
                }
            }
        }

        private void btnIngresarPago_Click(object sender, RoutedEventArgs e)
        {
            if (auxiliar.esNúmero(txtPago.Text) && txtPago.Text != "0" && cmbCódigoCliente.Items.Contains(cmbCódigoCliente.Text))
            {
                //if (!swModificarCliente) //sólo si es ingresar un cliente nuevo y no modificar uno ya existente
                //{
                    pago unPago = new pago();
                    if (dateFechaPago.SelectedDate != null)
                    {
                        DateTime fecha = (DateTime)dateFechaPago.SelectedDate;
                        unPago.Fecha = fecha.ToShortDateString();
                    }
                    else
                        unPago.Fecha = DateTime.Now.ToShortDateString();
                    unPago.IdCliente = int.Parse(cmbCódigoCliente.Text);
                    unPago.IdVenta = 0;
                    unPago.Monto = double.Parse(txtPago.Text);

                    unPago.guardarPago();
                //}
                //else
                //{
                //    pago pagoOriginal = (pago)dataGridPagos.SelectedItem;
                //    pago unPago = new pago();
                //    if (dateFechaPago.SelectedDate != null)
                //        unPago.Fecha = (DateTime)dateFechaPago.SelectedDate;
                //    else
                //        unPago.Fecha = DateTime.Now;
                //    unPago.IdCliente = int.Parse(cmbCódigoCliente.Text);
                //    unPago.IdVenta = 0;

                //    unPago.Monto = double.Parse(txtPago.Text);

                //    //unPago.actualizar();
                //    swModificarCliente = false;
                //}
                
                if (misPagos != null) misPagos.Clear();
                iniciarCampos();
            }
            else
            {
                MessageBox.Show("No se puede guardar el Pago porque o no ha escrito un cliente existente, o no ha escrito un monto en el pago.", "Atenti!");
            }
        }

        private void iniciarCampos()
        {
            btnIngresarPago.Content = "Ingresar Pago";
            //btnCancelarModificación.Visibility = Visibility.Hidden;
            cmbCódigoCliente.Text = "";
            cmbNombreCliente.Text = "";
            txtPago.Text = "0";
            dateFechaPago.Text = "Elija fecha";
            tabPagoGuardado.IsEnabled = true;
        }

        private void radioPorCliente_Checked(object sender, RoutedEventArgs e)
        {
            cmbCódigoCliente.IsEnabled = true;
            cmbNombreCliente.IsEnabled = true;
            dateFechaPago.IsEnabled = false;
        }

        private void radioPorFecha_Checked(object sender, RoutedEventArgs e)
        {
            cmbCódigoCliente.IsEnabled = false;
            cmbNombreCliente.IsEnabled = false;
            dateFechaPago.IsEnabled = true;
        }

        private void lblEliminarPago_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (dataGridPagos.SelectedItem != null)
                {
                    if (MessageBox.Show("¿Está seguro que desea eliminar el pago seleccionado?", "Atención", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        pagoParaMostrar unPagoParaMostrar = (pagoParaMostrar)dataGridPagos.SelectedItem;
                        pago unPago = new pago();
                        unPago.Id = unPagoParaMostrar.id;
                        unPago.IdCliente = unPagoParaMostrar.idCliente;
                        unPago.IdVenta = unPagoParaMostrar.idVenta;
                        unPago.Monto = unPagoParaMostrar.monto;
                        unPago.Fecha = unPagoParaMostrar.fecha;
                        unPago.eliminar();
                        misPagos.Remove(unPagoParaMostrar);
                    }
                }
                else
                {
                    MessageBox.Show("No ha seleccionado ningún pago. Por favor seleccione uno de la lista superior", "Atenti!");
                }
            }
            catch
            {
            }
        }

        //private void lblModificarPago_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    swModificarCliente = true;
        //    btnCancelarModificación.Visibility = Visibility.Visible;
        //    tabPagoGuardado.IsEnabled = false;
        //    tabNuevoPago.Focus();
        //}

        //private void btnCancelarModificación_Click(object sender, RoutedEventArgs e)
        //{
        //    swModificarCliente = false;
        //    iniciarCampos();
        //}

        private void cmbVerCódigoCliente_KeyUp(object sender, KeyEventArgs e)
        {
            //si lo que está escrito es una id válida, se busca el nombre del cliente
            if (auxiliar.chequearDatoNuméricoVálido(IDsClientes, cmbVerCódigoCliente.Text))
            {
                cmbVerNombreCliente.Text = cliente.recuperarNombreCliente(int.Parse(cmbVerCódigoCliente.Text));
            }
            else
            {
                if (cmbVerCódigoCliente.Text == "")
                {
                    cmbVerNombreCliente.Text = "";
                }
                else
                {
                    cmbVerNombreCliente.Text = "No existe";
                    misPagos.Clear();
                }
            }
        }

        private void cmbVerNombreCliente_KeyUp(object sender, KeyEventArgs e)
        {
            //si lo que hay escrito es un nombre de cliente existente se busca su id
            if (auxiliar.chequearDatoCadenaVálido(nombresClientes, cmbVerNombreCliente.Text))
            {
                cmbVerCódigoCliente.Text = cliente.recuperarIdCliente(cmbVerNombreCliente.Text).ToString();
            }
            else
            {
                cmbVerCódigoCliente.Text = "0";
                misPagos.Clear();
            }
        }

        private void cmbVerNombreCliente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbVerNombreCliente.SelectedItem != null)
            {
                if (auxiliar.chequearDatoCadenaVálido(nombresClientes, cmbVerNombreCliente.SelectedItem.ToString()))
                {
                    cmbVerCódigoCliente.Text = cliente.recuperarIdCliente(cmbVerNombreCliente.SelectedItem.ToString()).ToString();
                    llenarPagos();
                }
            }
        }

        private void cmbVerCódigoCliente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbVerCódigoCliente.SelectedItem != null)
            {
                if (auxiliar.chequearDatoNuméricoVálido(IDsClientes, cmbVerCódigoCliente.SelectedItem.ToString())) //si ese número corresponde a una id válida
                {
                    cmbVerNombreCliente.Text = cliente.recuperarNombreCliente(int.Parse(cmbVerCódigoCliente.SelectedItem.ToString())); //se busca el nombre de esa id
                    
                    llenarPagos();
                }
            } 
        }

        private void llenarPagos()
        {
            if (misPagos != null) 
                if (misPagos.Count != 0)
                    misPagos.Clear();
            int idCliente;
            
            if (cmbVerCódigoCliente.SelectedItem != null)
            {
                if (auxiliar.chequearDatoNuméricoVálido(IDsClientes, cmbVerCódigoCliente.SelectedItem.ToString()))
                    idCliente = int.Parse(cmbVerCódigoCliente.SelectedItem.ToString());
                else
                    idCliente = 0;

                DateTime fecha;
                if (dateVerXFechaPago.SelectedDate == null)
                    fecha = new DateTime(1111, 01, 01);
                else
                {
                    try
                    {
                        fecha = DateTime.Parse(dateVerXFechaPago.Text);
                    }
                    catch
                    {
                        fecha = new DateTime(1111, 01, 01);
                    }
                }

                misPagos = new ObservableCollection<pagoParaMostrar>(pago.cargarTodosParaMostrar(idCliente, fecha));
                dataGridPagos.DataContext = misPagos;
            }
        }

        private void dateVerXFechaPago_CalendarClosed(object sender, RoutedEventArgs e)
        {
            llenarPagos();
        }

        private void dateVerXFechaPago_KeyUp(object sender, KeyEventArgs e)
        {
            llenarPagos();
        }
	}
}