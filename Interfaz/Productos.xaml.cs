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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ProcesosNegocio;

namespace Interfaz
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Productos : Window
    {
        private List<string> marcas;
        private List<string> productos;
        private List<string> capacidades;
        private bool swModificar;

        public Productos()
        {
            InitializeComponent();
            
        }

        //carga todos los artículos guardados en la BD
        private void Productos_Load(object sender, RoutedEventArgs e)
        {
            List<artículo> misArtículos = artículo.cargarTodos();           
            dataGridArtículos.ItemsSource = misArtículos;
            marcas = artículo.sóloMarcas(misArtículos);
            productos = artículo.sóloProductos(misArtículos);
            capacidades = artículo.sóloCapacidad(misArtículos);

            cmbMarca.ItemsSource = marcas;
            cmbProducto.ItemsSource = productos;
            cmbCapacidad.ItemsSource = capacidades;
            swModificar = false;

            if (usuario.usuarioActual.nivel_seguridad != 0)//si no es administrador
            {
                btnCancelarModificación.Visibility = Visibility.Hidden;
                btnIngresarArtículo.Visibility = Visibility.Hidden;
                lblEliminarArtículo.Visibility = Visibility.Hidden;
            }
        }

        //guarda un artículo nuevo
        private void btnIngresarArtículo_Click(object sender, RoutedEventArgs e)
        {
            if (!swModificar)
            {
                guardarArtículo();
            }
            else
            {
                modificarArtículo();
            }
            btnCancelarModificación_Click(sender, e);
        }

        private void guardarArtículo()
        {
            string marca = cmbMarca.Text.Trim();
            if (string.IsNullOrEmpty(marca))
            {
                MessageBox.Show("No ha elegido o escrito ninguna marca, por favor elija o escriba una marca y luego presione otra vez este botón.", "Atención!");
                return;
            }

            string producto = cmbProducto.Text.Trim();
            if (string.IsNullOrEmpty(producto))
            {
                MessageBox.Show("No puede dejar en blanco el producto, por favor elija un producto o escriba uno nuevo y luego presione de nuevo este botón.", "Atención!");
                return;
            }

            double capacidad;
            double número;
            if (Double.TryParse(cmbCapacidad.Text, out número))
            {
                capacidad = double.Parse(cmbCapacidad.Text);
            }
            else
            {
                MessageBox.Show("La capacidad ingresada no es correcta, por favor corrija el número", "Atención");
                return;
            }
            artículo miArtículo = new artículo(marca, producto, capacidad);

            if (artículo.YaExiste(miArtículo) == false)
            {
                miArtículo.guardar();
                List<artículo> misArtículos = artículo.cargarTodos();
                dataGridArtículos.ItemsSource = misArtículos;

                marcas = artículo.sóloMarcas(misArtículos);
                productos = artículo.sóloProductos(misArtículos);
                capacidades = artículo.sóloCapacidad(misArtículos);

                cmbMarca.ItemsSource = marcas;
                cmbProducto.ItemsSource = productos;
                cmbCapacidad.ItemsSource = capacidades;
            }
            else
            {
                MessageBox.Show("El producto '" + miArtículo.Producto + "', marca '" + miArtículo.Marca + "', y capacidad '" + miArtículo.Capacidad.ToString() + "' que desea ingresar YA EXISTE, sólo se puede ingresar una vez cada producto", "Atención");
                return;
            }
        }

        private void modificarArtículo()
        {
            string marca = cmbMarca.Text.Trim();
            if (string.IsNullOrEmpty(marca))
            {
                MessageBox.Show("No ha elegido o escrito ninguna marca, por favor elija o escriba una marca y luego presione otra vez este botón.", "Atención!");
                return;
            }

            string producto = cmbProducto.Text.Trim();
            if (string.IsNullOrEmpty(producto))
            {
                MessageBox.Show("No puede dejar en blanco el producto, por favor elija un producto o escriba uno nuevo y luego presione de nuevo este botón.", "Atención!");
                return;
            }

            double capacidad;
            double número;
            if (Double.TryParse(cmbCapacidad.Text, out número))
            {
                capacidad = double.Parse(cmbCapacidad.Text);
            }
            else
            {
                MessageBox.Show("La capacidad ingresada no es correcta, por favor corrija el número", "Atención");
                return;
            }
            artículo miArtículo = new artículo(marca, producto, capacidad);
            artículo artOriginal = (artículo)dataGridArtículos.SelectedItem; //se toma el artículo seleccionado en el grid para modificar
            miArtículo.id = artOriginal.id;

            if (artículo.YaExiste(miArtículo) == false)
            {
                miArtículo.actualizar();
                List<artículo> misArtículos = artículo.cargarTodos();
                dataGridArtículos.ItemsSource = misArtículos;
            }
            else
            {
                MessageBox.Show("El producto '" + miArtículo.Producto + "', marca '" + miArtículo.Marca + "', y capacidad '" + miArtículo.Capacidad.ToString() + "' que desea ingresar YA EXISTE, sólo se puede ingresar una vez cada producto", "Atención!");
                return;
            }
        }

        private void eliminarArtículo(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (dataGridArtículos.SelectedItem != null)
                {
                    
                    if (MessageBox.Show("Sólo es recomandable eliminar un producto si aún no está incluído en ninguna venta. ¿Está seguro de eliminar el producto? Se eliminarán también todas las ventas de este producto, los pagos de esas ventas y los ingresos a stock de este producto. Recomendamos no eliminar productos.","Atención!",MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                    artículo artículoParaEliminar = (artículo)dataGridArtículos.SelectedItem;
                    artículoParaEliminar.eliminar();
                    List<artículo> misArtículos = artículo.cargarTodos();
                    dataGridArtículos.ItemsSource = misArtículos;
                    }
                }
            }
            catch 
            {
                
            }
        }

        private void dataGridArtículos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
            if (dataGridArtículos.SelectedItem != null)
            {
                artículo artSeleccionado = (artículo)dataGridArtículos.SelectedItem;
                cmbMarca.SelectedItem = artSeleccionado.Marca;
                cmbProducto.SelectedItem = artSeleccionado.Producto;
                cmbCapacidad.SelectedItem = artSeleccionado.Capacidad.ToString();
                swModificar = true;
                btnCancelarModificación.Visibility = Visibility.Visible;
                btnIngresarArtículo.Content = "Modificar el Artículo";
            }
            else
            {
                swModificar = false;
            }
            }
            catch
            {

            }
        }

        private void btnCancelarModificación_Click(object sender, RoutedEventArgs e)
        {
            swModificar = false;
            btnCancelarModificación.Visibility = Visibility.Hidden;
            dataGridArtículos.SelectedIndex = -1;
            btnIngresarArtículo.Content = "Ingresar Artículo para Ventas -->";
            cmbMarca.Text = "";
            cmbProducto.Text = "";
            cmbCapacidad.Text = "";
        }
    }
}
