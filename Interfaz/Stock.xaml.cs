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
	/// Lógica de interacción para Stock.xaml
	/// </summary>
	public partial class Stock : Window
	{
        //guarda todos los artículos en memoria para trabajar más cómodo con ellos
        private List<artículo> misArtículos = new List<artículo>();
        private List<string> misMarcas = new List<string>();

		public Stock()
		{
			this.InitializeComponent();
			
			// A partir de este punto se requiere la inserción de código para la creación del objeto.
		}

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            misArtículos = artículo.cargarTodos();
            misMarcas = artículo.sóloMarcas(misArtículos);
            foreach (artículo art in misArtículos) cmbCódigoArtículo.Items.Add(art.id.ToString());
            cmbMarca.ItemsSource = misMarcas;
            //cmbMarca.SelectedIndex = 0;
            resetearCombos();

            if (usuario.usuarioActual.nivel_seguridad != 0)//si no es administrador
            {
                lblCantidad.Visibility = Visibility.Hidden;
                txtCantidad.Visibility = Visibility.Hidden;
                btnIngresar.Visibility = Visibility.Hidden;
            }
        }

        private void cargarProductosXMarca(string marca)
        {
            cmbProducto.Items.Clear();
            foreach (string prod in sóloProductoXMarca(misArtículos, marca)) cmbProducto.Items.Add(prod);
            //cmbProducto.IsEnabled = true;
        }

        private void btnIngresar_Click(object sender, RoutedEventArgs e)
        {
            if (cmbCódigoArtículo.Text != "0" && !String.IsNullOrEmpty(txtCantidad.Text) && txtCantidad.Text != "0")
            {
                artículo.cargarAStock(int.Parse(cmbCódigoArtículo.Text), int.Parse(txtCantidad.Text));
                misArtículos = artículo.cargarTodos();
                resetearCombos();
            }
            else
            {
                MessageBox.Show("Debe elegir un artículo y llenar la cantidad de stock que desea ingresar antes de presionar este botón", "Atenti!");
            }
        }

        private void llenarEtiquetaStock(string cantidad)
        {
            lblStock.Content = "Del artículo seleccionado hay en stock: " + cantidad;
        }

        private void cmbCódigoArtículo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbCódigoArtículo.SelectedItem != null && cmbCódigoArtículo.SelectedItem.ToString() != "0")
            {
                artículo miArtículo = this.encontrarArtículoXID(int.Parse(cmbCódigoArtículo.SelectedItem.ToString()));
                cmbMarca.SelectedItem = miArtículo.Marca;
                cmbProducto.Text = miArtículo.Producto;
                cmbCapacidad.Text = miArtículo.Capacidad.ToString();
                llenarEtiquetaStock(miArtículo.Stock.ToString());
            }
        }

        private void resetearCombos()
        {
            cmbCódigoArtículo.Text = "0";
            cmbCapacidad.Foreground = Brushes.Gray;
            cmbCapacidad.Text = "Elija la capacidad";
            cmbCapacidad.IsEnabled = false;
            cmbMarca.Foreground = Brushes.Gray;
            cmbMarca.Text = "Elija su marca";
            cmbProducto.Foreground = Brushes.Gray;
            cmbProducto.Text = "Elija su producto";
            cmbProducto.IsEnabled = false;
            txtCantidad.Text = "0";
            lblStock.Content = "No hay ninguna marca seleccionada";
        }

        private void resetearCombosMarcaYaElegida()
        {
            cmbCódigoArtículo.Text = "0";

            cmbCapacidad.Foreground = Brushes.Gray;
            cmbCapacidad.Text = "Elija la capacidad";
            cmbCapacidad.IsEnabled = false;

            cmbMarca.Foreground = Brushes.Green;

            cmbProducto.Text = "Elija su producto";
            cmbProducto.Foreground = Brushes.Green;
            cmbProducto.IsEnabled = true;

            lblStock.Content = "No hay ningún producto seleccionado";
        }

        private void resetearCombosProductoYaElegido()
        {
            cmbCódigoArtículo.Text = "0";

            cmbCapacidad.Foreground = Brushes.Green;
            cmbCapacidad.Text = "Elija la capacidad";
            cmbCapacidad.IsEnabled = true;

            lblStock.Content = "No hay ningún producto seleccionado";
        }

        private void cmbMarca_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbMarca.SelectedItem != null)
            {
                if (auxiliar.chequearDatoCadenaVálido(misMarcas, cmbMarca.SelectedItem.ToString()))
                {
                    cargarProductosXMarca(cmbMarca.SelectedItem.ToString());
                    resetearCombosMarcaYaElegida();
                }
            }
        }

        private artículo encontrarArtículoXID(int idArtículo)
        {
            artículo unArtículo = misArtículos.Find(delegate(artículo art)
            { return art.id == idArtículo; });

            if (unArtículo != null)
            {
                return unArtículo;
            }
            else
            {
                return unArtículo = new artículo(); ;
            }
        }

        private List<string> sóloProductoXMarca (List<artículo> misArtículos, string marca)
        {
            List<string> productos = new List<string>();
            
            foreach (artículo art in misArtículos)
            {
                if (art.Marca == marca) productos.Add(art.Producto);
            }

            return filtrarRepetidosEnLista(productos);
        }

        //controla que no se repitan elementos en una lista
        private List<string> filtrarRepetidosEnLista(List<string> miLista)
        {
            List<string> resultados = new List<string>();
            bool swExiste;
            foreach (string cadena in miLista)
            {
                swExiste = false;
                foreach (string result in resultados)
                {
                    if (cadena == result)
                    {
                        swExiste = true;
                        break;
                    }
                }
                if (swExiste == false) resultados.Add(cadena);
            }

            return resultados;
        }

        private void cmbProducto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbProducto.SelectedItem != null)
            {
                List<string> misProductos = new List<string>();
                foreach (string prod in cmbProducto.Items) misProductos.Add(prod);

                if (auxiliar.chequearDatoCadenaVálido(misProductos, cmbProducto.SelectedItem.ToString()))
                {
                    cargarCapacidadesXProducto(misArtículos, cmbMarca.SelectedItem.ToString(), cmbProducto.SelectedItem.ToString());
                    resetearCombosProductoYaElegido();
                }
            }
        }

        private void cargarCapacidadesXProducto(List<artículo> misArtículos, string marca, string producto)
        {
            cmbCapacidad.Items.Clear();
            foreach (string capacidad in filtrarCapacidades(misArtículos, marca, producto)) cmbCapacidad.Items.Add(capacidad);
        }

        private List<string> filtrarCapacidades (List<artículo> artículos, string marca, string producto)
        {
            List<string> capacidades = new List<string>();

            foreach (artículo art in misArtículos)
            {
                if (art.Marca == marca && art.Producto == producto) capacidades.Add(art.Capacidad.ToString());
            }

            return filtrarRepetidosEnLista(capacidades);
        }

        private void cmbMarca_KeyUp(object sender, KeyEventArgs e)
        {
            //si es enter, mover el foco
            //if (e.Key == Key.Return) ;
            //List<string> productos = sóloProductoXMarca(misArtículos,cmbMarca.Text);
            cmbCódigoArtículo.Text = "0";
            if (cmbMarca.Text != "Elija su marca")
            {
                if (!auxiliar.chequearDatoCadenaVálido(misMarcas, cmbMarca.Text))
                {
                    cmbProducto.Text = "La marca no existe";
                    cmbProducto.IsEnabled = false;
                }
                else
                {
                    cmbProducto.Text = "Elija su producto";
                    cmbProducto.IsEnabled = true;
                }
            }
        }

        private void cmbProducto_KeyUp(object sender, KeyEventArgs e)
        {
            //si es enter, mover el foco
            //if (e.Key == Key.Return) ;
            List<string> productos = sóloProductoXMarca(misArtículos,cmbMarca.Text);
            cmbCódigoArtículo.Text = "0";
            if (cmbProducto.Text != "Elija su producto")
            {
                if (!auxiliar.chequearDatoCadenaVálido(productos, cmbProducto.Text))
                {
                    cmbCapacidad.Text = "El producto no existe";
                    cmbCapacidad.IsEnabled = false;
                }
                else
                {
                    cmbCapacidad.Text = "Elija la capacidad";
                    cmbCapacidad.IsEnabled = true;
                }
            }
        }

        private void cmbCapacidad_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbCapacidad.SelectedItem != null)
            {
                List<string> capacidades = new List<string>();
                foreach (string capacidad in cmbCapacidad.Items) capacidades.Add(capacidad);

                if (auxiliar.chequearDatoCadenaVálido(capacidades, cmbCapacidad.SelectedItem.ToString()))
                {
                    cmbCódigoArtículo.SelectedItem = artículo.recuperarIDporNombre(cmbMarca.Text + ", " + cmbProducto.Text + " x " + cmbCapacidad.SelectedItem.ToString()).ToString();
                }
                //else
                //{
                //    cmbCódigoArtículo.Text = "0";
                //}
            }
        }

        private void cmbCapacidad_KeyUp(object sender, KeyEventArgs e)
        {
            List<string> capacidades = new List<string>();
            foreach (string capacidad in cmbCapacidad.Items) capacidades.Add(capacidad);

            if (auxiliar.chequearDatoCadenaVálido(capacidades, cmbCapacidad.Text))
                cmbCódigoArtículo.SelectedItem = artículo.recuperarIDporNombre(cmbMarca.Text + ", " + cmbProducto.Text + " x " + cmbCapacidad.SelectedItem.ToString()).ToString();
            else
                cmbCódigoArtículo.Text = "0";
        }


	}
}