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
	/// Lógica de interacción para Ventas.xaml
	/// </summary>
    public partial class Ventas : Window
    {
        private List<string> nombresClientes = new List<string>();
        private List<string> IDsClientes = new List<string>();
        private List<string> nombresArtículos = new List<string>();
        private List<string> IDsArtículos = new List<string>();
        private ObservableCollection<productoVenta> misProductos = new ObservableCollection<productoVenta>();
        private ObservableCollection<filaParaImprimirVenta> misVentas;

        public Ventas()
        {
            this.InitializeComponent();

            // A partir de este punto se requiere la inserción de código para la creación del objeto.
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //arreglar que cambie el foco con enter
            //Keyboard.Focus();
            
            nombresClientes = cliente.recuperarTodosLosNombresDeClientes();
            IDsClientes = cliente.recuperarTodosLosID();
            nombresArtículos = artículo.recuperarTodosLosNombres();
            IDsArtículos = artículo.recuperarTodasID();

            if (IDsClientes.Count == 0 && IDsArtículos.Count > 0) //si no hay clientes ingresados pero sí artículos
            {
                cmbCódigoCliente.Text = "No hay clientes";
                cmbNombreCliente.Text = "No hay clientes";
                MessageBox.Show("No ha ingreso ningún cliente en el programa, por eso no puede registrar una venta. Vaya a 'Clientes' y ahí ingrese su cliente nuevo. Luego vuelva aquí a registrar su venta.", "Atención");
                btnIngresarVenta.IsEnabled = false;
                Window.Close();
                return;
            }

            if (IDsClientes.Count > 0 && IDsArtículos.Count == 0) //si no hay artículos ingresados pero sí clientes
            {
                cmbCódigoArtículo.Text = "No hay artículos";
                cmbNombreArtículo.Text = "No hay artículos";
                MessageBox.Show("No ha ingreso ningún producto en el programa, por eso no puede registrar una venta. Vaya a 'Productos' y ahí ingrese su artículo nuevo. Luego vuelva aquí a registrar su venta.", "Atención");
                btnIngresarVenta.IsEnabled = false;
                Window.Close();
                return;

            }

            if (IDsClientes.Count > 0 && IDsArtículos.Count == 0) //si no hay ni artículos ni clientes
            {
                cmbCódigoArtículo.Text = "No hay productos";
                cmbNombreArtículo.Text = "No hay productos";
                cmbCódigoCliente.Text = "No hay clientes";
                cmbNombreCliente.Text = "No hay clientes";
                MessageBox.Show("No ha ingreso ningún cliente ni producto en el programa, por eso no puede registrar una venta. Vaya a 'Clientes' y ahí ingrese su cliente nuevo, y vaya a 'Productos' y ahí ingrese su producto nuevo. Luego vuelva aquí a registrar su venta.", "Atención");
                btnIngresarVenta.IsEnabled = false;
                Window.Close();
                return;
            }

            //se llenan los combos de los clientes
            cmbCódigoCliente.ItemsSource = IDsClientes;
            cmbNombreCliente.ItemsSource = nombresClientes;
            //se llenan los combos de los artículos
            cmbNombreArtículo.ItemsSource = nombresArtículos;
            cmbCódigoArtículo.ItemsSource = IDsArtículos;

            //dataGridVentas.ItemsSource = venta.presentarVentas(DateTime.Now, DateTime.Now);
            //dataGridArtículos.ItemsSource = misProductos;
            misVentas = new ObservableCollection<filaParaImprimirVenta> (venta.presentarVentas(DateTime.Now, DateTime.Now));
            dataGridVentas.DataContext = misVentas;
            dataGridArtículos.DataContext = misProductos;

            iniciarCampos();
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
            if (cmbCódigoCliente.SelectedItem != null )//&& cmbCódigoCliente.IsFocused)
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
            if (cmbNombreCliente.SelectedItem != null )// && cmbNombreCliente.IsFocused)
            {
                if (auxiliar.chequearDatoCadenaVálido(nombresClientes, cmbNombreCliente.SelectedItem.ToString()))
                {
                    cmbCódigoCliente.Text = cliente.recuperarIdCliente(cmbNombreCliente.SelectedItem.ToString()).ToString();
                }
            }
        }

        private void cmbCódigoArtículo_KeyUp(object sender, KeyEventArgs e)
        {
            //si lo que está escrito es una id válida, se busca el nombre del artículo
            if (auxiliar.chequearDatoNuméricoVálido(IDsArtículos, cmbCódigoArtículo.Text))
            {
                cmbNombreArtículo.Text = artículo.recuperarNombreArtículo(int.Parse(cmbCódigoArtículo.Text));
            }
            else
            {
                if (cmbCódigoArtículo.Text == "")
                {
                    cmbNombreArtículo.Text = "";
                }
                else
                {
                    cmbNombreArtículo.Text = "No existe";
                }
            }

            
        }

        private void cmbCódigoArtículo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbCódigoArtículo.SelectedItem != null)
            {
                if (auxiliar.chequearDatoNuméricoVálido(IDsArtículos, cmbCódigoArtículo.SelectedItem.ToString()))
                {
                    cmbNombreArtículo.SelectedItem = artículo.recuperarNombreArtículo(int.Parse(cmbCódigoArtículo.SelectedItem.ToString())).ToString();
                }
            }
        }

        private void cmbNombreArtículo_KeyUp(object sender, KeyEventArgs e)
        {
            if (auxiliar.chequearDatoCadenaVálido(nombresArtículos, cmbNombreArtículo.Text))
            {
                cmbCódigoArtículo.Text = artículo.recuperarIDporNombre(cmbNombreArtículo.Text).ToString();
            }
            else
            {
                cmbCódigoArtículo.Text = "0";
            }

            //if (e.Key == Key.Return) Keyboard.Focus(txtCantidad);
        }

        private void cmbNombreArtículo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbNombreArtículo.SelectedItem != null)
            {
                if (auxiliar.chequearDatoCadenaVálido(nombresArtículos, cmbNombreArtículo.SelectedItem.ToString()))
                {
                    cmbCódigoArtículo.SelectedItem = artículo.recuperarIDporNombre(cmbNombreArtículo.SelectedItem.ToString()).ToString();
                }
            }
        }

        private void btnIngresarVenta_Click(object sender, RoutedEventArgs e)
        {
            if (cmbCódigoCliente.Text == "0" || cmbCódigoCliente.Text == "") { msgCampoNoVálido("Código del Cliente"); return;}
            if (cmbNombreCliente.Text == "No existe" || cmbNombreCliente.Text == "") { msgCampoNoVálido("Nombre del Cliente"); return;}

            if (!auxiliar.esNúmero(txtPagos.Text) || txtPagos.Text == "") { msgCampoNoVálido("Pagos"); return;}
            if (!auxiliar.esNúmero(txtHora.Text) || txtHora.Text == "") {
                if (int.Parse(txtHora.Text) <= 0 || int.Parse(txtHora.Text) >= 23) msgCampoNoVálido("Hora"); 
                return;
            }
            if (!auxiliar.esNúmero(txtMinutos.Text) || txtMinutos.Text == "") 
            { 
                if (int.Parse(txtMinutos.Text) <= 0 || int.Parse(txtMinutos.Text) >=59) msgCampoNoVálido("Minutos"); 
                return; 
            }

            //como todo está ok se continúa
            venta miVenta = new venta();
            miVenta.idCliente = int.Parse(cmbCódigoCliente.Text);
            miVenta.Fecha = dateFechaVenta.SelectedDate.Value;
            miVenta.Hora = new DateTime(1, 1, 1, int.Parse(txtHora.Text), int.Parse(txtMinutos.Text), 0);

            foreach (productoVenta unProducto in misProductos)
            {
                ventaArtículo miArtículo = new ventaArtículo();
                miArtículo.cantidad = unProducto.Cantidad;
                miArtículo.precio = unProducto.Precio;
                miArtículo.idArtículo = artículo.recuperarIDporNombre(unProducto.Producto);


                miVenta.MisArtículos.Add(miArtículo);
                //Artículo = int.Parse(cmbCódigoArtículo.Text);
                //miVenta.Cantidad = double.Parse(txtCantidad.Text);
                //miVenta.Precio = double.Parse(txtPrecio.Text);
            }

            pago miPago = new pago();
            miPago.IdCliente = int.Parse(cmbCódigoCliente.Text);
            miPago.Monto = double.Parse(txtPagos.Text);
            DateTime fecha = dateFechaVenta.SelectedDate.Value;
            miPago.Fecha = fecha.ToString();

            if (!miVenta.guardar(miPago))
            {
                MessageBox.Show("Hubo un problema al guardar la venta, contacte al equipo de desarrollo de software", "Ups!");
                return;
            }

            //dataGridVentas.ItemsSource = auxiliar.recuperarRegistrosVentas();
            misProductos.Clear();
            misVentas.Clear();
            misVentas = new ObservableCollection<filaParaImprimirVenta> (venta.presentarVentas(DateTime.Now, DateTime.Now));
            dataGridVentas.DataContext = misVentas;
            iniciarCampos();
        }

        private void msgCampoNoVálido(string quéCampo)
        {
            string mensaje = "No ha escrito un valor válido en " + quéCampo + ". Por favor revise no haberlo dejado en blanco o escrito algo por error.";
            MessageBox.Show(mensaje, "Error");
        }

        private void iniciarCampos()
        {
            txtHora.Text = DateTime.Now.Hour.ToString(); //se llena la fecha y la hora
            txtMinutos.Text = DateTime.Now.Minute.ToString();
            dateFechaVenta.Text = DateTime.Now.Date.ToString();
            //se llenan los combos de los clientes
            cmbCódigoCliente.Text = cmbCódigoCliente.Items[0].ToString(); //se selecciona el primer elemento
            //se llenan los combos de los artículos
            cmbNombreArtículo.Text = "Elija un producto";
            cmbCódigoArtículo.Text = "0";
            txtCantidad.Text = "0";
            txtPagos.Text = "0";
            txtPrecio.Text = "0";
            cmbCódigoCliente.Focus();
        }

        private void txtCantidad_GotFocus(object sender, RoutedEventArgs e)
        {
            txtCantidad.SelectAll();
        }

        private void txtPrecio_GotFocus(object sender, RoutedEventArgs e)
        {
            txtPrecio.SelectAll();
        }

        private void txtPagos_GotFocus(object sender, RoutedEventArgs e)
        {
            txtPagos.SelectAll();
        }

        //private void btnModificarVenta_Click(object sender, RoutedEventArgs e)
        //{
        //    //try
        //    //{
        //    //    if (dataGridVentas.SelectedItem != null)
        //    //    {
        //    //        //venta ventaParaModificar = new venta();
        //    //        registroVentas ventaSeleccionada = (registroVentas)dataGridVentas.SelectedItem;

        //    //        //ventaParaModificar.idArtículo = artículo.recuperarIDporNombre(ventaSeleccionada.Producto);
        //    //        //ventaParaModificar.idCliente = cliente.recuperarIdCliente(ventaSeleccionada.Cliente);
        //    //        //ventaParaModificar.Cantidad = ventaSeleccionada.Cantidad;
        //    //        //ventaParaModificar.Precio = ventaSeleccionada.Precio;
        //    //        //txtPagos.Text = ventaSeleccionada.Pago.ToString();
        //    //        //ventaParaModificar.Fecha = DateTime.Parse(ventaSeleccionada.Fecha);
        //    //        //ventaParaModificar.Id = ventaSeleccionada.Id_Venta;

        //    //        //como todo está ok se continúa
        //    //        venta miVenta = new venta();
        //    //        miVenta.idCliente = int.Parse(cmbCódigoCliente.Text);
        //    //        miVenta.idArtículo = int.Parse(cmbCódigoArtículo.Text);
        //    //        miVenta.Cantidad = double.Parse(txtCantidad.Text);
        //    //        miVenta.Fecha = dateFechaVenta.SelectedDate.Value;
        //    //        miVenta.Precio = double.Parse(txtPrecio.Text);
        //    //        miVenta.Hora = new DateTime(1, 1, 1, int.Parse(txtHora.Text), int.Parse(txtMinutos.Text), 0);
        //    //        miVenta.Id = ventaSeleccionada.Id_Venta;

        //    //        pago miPago = new pago();
        //    //        miPago.IdCliente = int.Parse(cmbCódigoCliente.Text);
        //    //        miPago.Monto = double.Parse(txtPagos.Text);
        //    //        miPago.Fecha = dateFechaVenta.SelectedDate.Value;
        //    //        miPago.IdVenta = ventaSeleccionada.Id_Venta;


        //    //        miVenta.actualizar(miPago);

        //    //        dataGridVentas.ItemsSource = auxiliar.recuperarRegistrosVentas();
        //    //        iniciarCampos();
        //    //    }
        //    //}
        //    //catch
        //    //{
        //    //}
        //}

        private void dataGridVentas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //try
            //{
            //    if (dataGridVentas.SelectedItem != null)
            //    {
            //        //registroVentas ventaSeleccionada = (registroVentas)dataGridVentas.SelectedItem;
            //        //cmbCódigoArtículo.SelectedItem = artículo.recuperarIDporNombre(ventaSeleccionada.Producto).ToString();
            //        //cmbNombreArtículo.SelectedItem = ventaSeleccionada.Producto;
            //        //cmbCódigoCliente.SelectedItem = cliente.recuperarIdCliente(ventaSeleccionada.Cliente).ToString();
            //        //cmbNombreCliente.SelectedItem = ventaSeleccionada.Cliente;
            //        //txtCantidad.Text = ventaSeleccionada.Cantidad.ToString();
            //        //txtPrecio.Text = ventaSeleccionada.Precio.ToString();
            //        //txtPagos.Text = ventaSeleccionada.Pago.ToString();
            //        //dateFechaVenta.Text = ventaSeleccionada.Fecha;
            //        //txtHora.Text = DateTime.Parse(ventaSeleccionada.Hora).Hour.ToString();
            //        //txtMinutos.Text = DateTime.Parse(ventaSeleccionada.Hora).Minute.ToString();

            //        registroVentas ventaSeleccionada = (registroVentas)dataGridVentas.SelectedItem;
            //        cmbCódigoArtículo.SelectedItem = artículo.recuperarIDporNombre(ventaSeleccionada.Producto).ToString();
            //        cmbNombreArtículo.SelectedItem = ventaSeleccionada.Producto;
            //        cmbCódigoCliente.SelectedItem = cliente.recuperarIdCliente(ventaSeleccionada.Cliente).ToString();
            //        cmbNombreCliente.SelectedItem = ventaSeleccionada.Cliente;
            //        txtCantidad.Text = ventaSeleccionada.Cantidad.ToString();
            //        txtPrecio.Text = ventaSeleccionada.Precio.ToString();
            //        txtPagos.Text = ventaSeleccionada.Pago.ToString();
            //        dateFechaVenta.Text = ventaSeleccionada.Fecha;
            //        txtHora.Text = DateTime.Parse(ventaSeleccionada.Hora).Hour.ToString();
            //        txtMinutos.Text = DateTime.Parse(ventaSeleccionada.Hora).Minute.ToString();
            //    }
            //}
            //catch
            //{

            //}
        }

        private void btnEliminarVenta_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGridVentas.SelectedItem != null) //si hay una venta seleccinada
                {
                    MessageBoxResult respuesta = MessageBox.Show("¿Está seguro que quiere borrar la Venta seleccioanda? Se borrarán también la salida del stock de los productos inlcuídos en ella y los pagos de la misma.", "Pregunta", MessageBoxButton.YesNo);

                    if (respuesta == MessageBoxResult.Yes) //si está de acuerdo en borrar la venta
                    {
                        filaParaImprimirVenta fila = (filaParaImprimirVenta)dataGridVentas.SelectedItem;

                        foreach (int idVenta in fila.código_Ventas)
                        {
                            venta ventaParaEliminar = new venta();
                            //registroVentas ventaSeleccionada = (registroVentas)dataGridVentas.SelectedItem;

                            //ventaParaEliminar.idArtículo = artículo.recuperarIDporNombre(ventaSeleccionada.Producto);
                            //ventaParaEliminar.idCliente = cliente.recuperarIdCliente(ventaSeleccionada.Cliente);
                            //ventaParaEliminar.Cantidad = ventaSeleccionada.Cantidad;
                            //ventaParaEliminar.Precio = ventaSeleccionada.Precio;
                            //txtPagos.Text = ventaSeleccionada.Pago.ToString();
                            //ventaParaEliminar.Fecha = DateTime.Parse(ventaSeleccionada.Fecha);
                            //ventaParaEliminar.Id = ventaSeleccionada.Id_Venta;

                            ventaParaEliminar.Id = idVenta;
                            ventaParaEliminar.eliminar();
                        }
                        //dataGridVentas.ItemsSource = auxiliar.recuperarRegistrosVentas();
                        misVentas.Remove(fila);
                        iniciarCampos();
                    }
                }
                else
                {
                    MessageBox.Show("No seleccionó ninguna Venta para eliminar. Por favor seleccione una Venta de la lista superior, y luego presione de nuevo este botón.", "Atención!");
                }
            }
            catch
            {
            }
        }

        private void label1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ////borrar ----> aquí en fila se toman los códigos de las ventas, así se pueden recuperar para modificarlas
            //filaParaImprimirVenta fila;
            //if (dataGridVentas.SelectedItem != null)
            //    fila = (filaParaImprimirVenta)dataGridVentas.SelectedItem;

            frmImprimir formImprimir = new frmImprimir(venta.presentarVentas(DateTime.Now, DateTime.Now));
            formImprimir.Show();
        }

        private void btnIngresarArtículo_Click(object sender, RoutedEventArgs e)
        {
            //se chequea si todo está ok para ingresar la venta
            if (cmbCódigoArtículo.Text == "0" || cmbCódigoArtículo.Text == "") { msgCampoNoVálido("Código del Artículo"); return; }
            if (cmbNombreArtículo.Text == "No existe" || cmbNombreArtículo.Text == "") { msgCampoNoVálido("Nombre del Artículo"); return;}
            if (!auxiliar.esNúmero(txtCantidad.Text) || txtCantidad.Text == "" || txtCantidad.Text == "0") { msgCampoNoVálido("Cantidad"); return; }
            if (!auxiliar.esNúmero(txtPrecio.Text) || txtPrecio.Text == "" || txtPrecio.Text == "0") { msgCampoNoVálido("Precio"); return; }

            productoVenta miProducto = new productoVenta();
            miProducto.Producto = cmbNombreArtículo.Text;
            miProducto.Precio = double.Parse(txtPrecio.Text);
            miProducto.Cantidad = double.Parse(txtCantidad.Text);

            misProductos.Add(miProducto);

            cmbCódigoArtículo.Text = "0";
            txtCantidad.Text = "0";
            txtPrecio.Text = "0";
            cmbNombreArtículo.Text = "Elija un producto";

            Keyboard.Focus(cmbCódigoArtículo);
        }

        private void btnEliminarArtículo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGridArtículos.SelectedItem == null)
                {
                    MessageBox.Show("No ha elegido un artículo para eliminar de la lista de Artículos que se incluirán en la Venta, por favor elija el artículo que desee eliminar de la lista superior, y vuelva a usar este botón.", "Atenti!");
                }
                else
                {
                    misProductos.Remove((productoVenta)dataGridArtículos.SelectedItem);
                }
            }
            catch
            {
            }
        }

        private void txtPagos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return) Keyboard.Focus(cmbCódigoArtículo);
        }

        //private void cmbCódigoArtículo_KeyUp_1(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Return) Keyboard.Focus(cmbNombreArtículo);
        //}

        //private void cmbNombreArtículo_KeyUp_1(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Return) Keyboard.Focus(txtCantidad);
        //}

        private void txtCantidad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return) Keyboard.Focus(txtPrecio);
        }

        private void txtPrecio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return) Keyboard.Focus(btnIngresarArtículo);
        }

        private void cmbCódigoArtículo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return) Keyboard.Focus(cmbNombreArtículo);
        }

        private void cmbNombreArtículo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return) Keyboard.Focus(txtCantidad);
        }

        private void cmbCódigoCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return) Keyboard.Focus(cmbNombreCliente);
        }

        private void cmbNombreCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return) Keyboard.Focus(txtPagos); //si se aprieta enter
        }

    } 
}