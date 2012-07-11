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
	/// Lógica de interacción para CuentasDeClientes.xaml
	/// </summary>
	public partial class CuentasDeClientes : Window
	{
        List<string> nombresClientes = new List<string>();
        List<string> IDsClientes = new List<string>();

		public CuentasDeClientes()
		{
			this.InitializeComponent();
			
			// A partir de este punto se requiere la inserción de código para la creación del objeto.
		}

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            nombresClientes = cliente.recuperarTodosLosNombresDeClientes();
            IDsClientes = cliente.recuperarTodosLosID();
            cmbClientes.ItemsSource = nombresClientes;
            cmbIdCliente.ItemsSource = IDsClientes;
            llenarResumenSaldos();
        }

        private void cmbIdCliente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbIdCliente.SelectedItem != null)
            {
                if (!cmbClientes.IsFocused)
                {
                    cmbClientes.SelectedItem = cliente.recuperarNombreCliente(int.Parse(cmbIdCliente.SelectedItem.ToString()));
                }
                //llenarRegistros(int.Parse(cmbIdCliente.SelectedItem.ToString()));
            }
        }

        private void llenarRegistros (int idCliente)
        {
            List<registroVentas> misRegistros = new List<registroVentas>();
            DateTime fechaInicio, fechaFin;
            if (dateFechaInicio.SelectedDate != null) 
                fechaInicio = dateFechaInicio.SelectedDate.Value;
            else
                fechaInicio=new DateTime(1111, 11, 11);

            if (dateFechaFin.SelectedDate != null)
                fechaFin = dateFechaFin.SelectedDate.Value;
            else
                fechaFin = new DateTime(3000,11,11);

            misRegistros=auxiliar.recuperarRegistrosVentas(idCliente, fechaInicio, fechaFin);
            dataGridMovimientos.ItemsSource=misRegistros;
            
            double entradas=0, salidas=0, totales=0;
            foreach (registroVentas registro in misRegistros)
            {
                entradas += registro.Haber;
                salidas += registro.Debe;
            }
            
            totales = entradas - salidas;
            lblDeudas.Content = "El cliente ha comprado $" + salidas;
            lblPagos.Content = "El cliente ha entregado $" + entradas;

            string cadenaFechas; //se llena el comienzo de la cadena de las fechas del total
            if (dateFechaInicio.SelectedDate != null)
                cadenaFechas = "Desde el " + dateFechaInicio.SelectedDate.Value.ToShortDateString();
            else
                cadenaFechas = "Desde el infinito";

            if (dateFechaFin.SelectedDate != null)
                cadenaFechas += " hasta el " + dateFechaFin.SelectedDate.Value.ToShortDateString();
            else
                cadenaFechas += " hasta el infinito";

            if (totales == 0) //se completa la cadena con el monto en rojo, verde o negro según su valor
            {
                lblSaldo.Foreground = Brushes.Black;
                lblSaldo.Content =  ", el saldo del cliente es $" + totales + ". No debe nada, ni tiene dinero a favor";
            }
            else
            {
                if (totales > 0)
                {
                    lblSaldo.Foreground = Brushes.Green;
                    lblSaldo.Content = cadenaFechas + ", el cliente tiene un saldo de $" + totales + " a su favor";
                }
                else
                {
                    lblSaldo.Foreground = Brushes.Red;
                    lblSaldo.Content = cadenaFechas + ", el cliente tiene un saldo de $" + totales + " como deuda";
                }
            }
        }

        private void llenarResumenSaldos()
        {
            List<string> misClientes = cliente.recuperarTodosLosID();
            List<saldo> deudas = new List<saldo>();
            List<saldo> aFavor = new List<saldo>();
            List<saldo> equilibradas = new List<saldo>();

            foreach (string unCliente in misClientes)
            {
                saldo aux = cliente.recuperarSaldo(int.Parse(unCliente));
                if (aux.Totales == 0)
                    equilibradas.Add(aux);
                else
                {
                    if (aux.Totales > 0)
                        aFavor.Add(aux);
                    else
                        deudas.Add(aux);
                }
            }

            dataGridDeudas.ItemsSource = deudas;
            dataGridEquilibrio.ItemsSource = equilibradas;
            dataGridSaldoFavor.ItemsSource = aFavor;
        }

        private void cmbClientes_KeyUp(object sender, KeyEventArgs e)
        {
            //si lo que hay escrito es un nombre de cliente existente se busca su id
            if (auxiliar.chequearDatoCadenaVálido(nombresClientes, cmbClientes.Text))
            {
                cmbIdCliente.Text = cliente.recuperarIdCliente(cmbClientes.Text).ToString();
                //if (e.Key == Key.Return) llenarRegistros(int.Parse(cmbIdCliente.Text)); //si se aprieta enter
            }
            else
            {
                cmbIdCliente.Text = "0";
            }
        }

        private void cmbIdCliente_KeyUp(object sender, KeyEventArgs e)
        {
            //si lo que está escrito es una id válida, se busca el nombre del cliente
            if (auxiliar.chequearDatoNuméricoVálido(IDsClientes, cmbIdCliente.Text))
            {
                cmbClientes.Text = cliente.recuperarNombreCliente(int.Parse(cmbIdCliente.Text));
            }
            else
            {
                if (cmbIdCliente.Text == "")
                {
                    cmbClientes.Text = "";
                }
                else
                {
                    cmbClientes.Text = "No existe";
                }
            }
        }

        private void cmbClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbClientes.SelectedItem != null)
            {
                cmbIdCliente.SelectedItem = cliente.recuperarIdCliente(cmbClientes.SelectedItem.ToString()).ToString();
            }
        }

        private void btnCargarVentas_Click(object sender, RoutedEventArgs e)
        {
            if (cmbIdCliente.SelectedItem != null) //si hay algo escrito en id del cliente
            { //y esa id es válida
                if (auxiliar.chequearDatoNuméricoVálido(IDsClientes, cmbIdCliente.SelectedItem.ToString()))
                {
                    llenarRegistros(int.Parse(cmbIdCliente.Text));
                }
            }
        }

        private void dataGridMovimientos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridMovimientos.SelectedItem != null)
            {
                try
                {
                    registroVentas unRegistro = (registroVentas)dataGridMovimientos.SelectedItem;
                    dataGridProductos.ItemsSource = unRegistro.MisArtículos;
                }
                catch
                {
                }
            }
        }

	}
}