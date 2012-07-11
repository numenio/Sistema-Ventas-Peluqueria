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
    /// Lógica de interacción para PromedioVentas.xaml
    /// </summary>
    public partial class PromedioVentas : Window
    {
        private List<string> IDsClientes;
        private List<string> nombresClientes;

        public PromedioVentas()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listProductos.ItemsSource = artículo.recuperarTodosLosNombres();
            dateFechaFin.SelectedDate = DateTime.Now;
            dateFechaInicio.SelectedDate = DateTime.Now.Subtract(new TimeSpan(7,0,0,0,0)); //menos una semana
            radioBarras.IsChecked = true;
            IDsClientes = cliente.recuperarTodosLosID();
            nombresClientes = cliente.recuperarTodosLosNombresDeClientes();
            IDsClientes.Add("0");
            nombresClientes.Add("Todos los clientes");
            IDsClientes.Sort();
            cmbNombreCliente.ItemsSource = nombresClientes;
            cmbCódigoCliente.ItemsSource = IDsClientes;
            cmbNombreCliente.SelectedIndex = 0; //se seleccionan los primeros elementos del combo
        }

        private void dataGridProductos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listProductos.SelectedItem != null)
            {
                try
                {
                    if (gridGráfico.Children != null) gridGráfico.Children.Clear();//si hay un dibujo, se lo quita

                    prodParaDibujar unProdDibujar = new prodParaDibujar(); //se dibuja el producto seleccionado

                    int idCliente = 0; //si la id del cliente no es válida, se le atribuye que son todos los clientes
                    if (auxiliar.chequearDatoNuméricoVálido(IDsClientes, cmbCódigoCliente.Text))
                        int.Parse(cmbCódigoCliente.Text);

                    unProdDibujar.valores = cliente.recuperarValoresVenta(artículo.recuperarIDporNombre(listProductos.SelectedItem.ToString()), idCliente, dateFechaInicio.SelectedDate.Value, dateFechaFin.SelectedDate.Value); //se cargan los valores de las ventas

                    int borde = 80; //se calcula el punto de inicio y la separación de valores de acuerdo a la cantidad de los días seleccionados
                    double anchoGráfico = gridGráfico.Width;
                    double altoGráfico = gridGráfico.Height;
                    double separaciónX = (anchoGráfico - borde * 2) / unProdDibujar.valores.Count;
                    Point inicio = new Point(borde, altoGráfico - borde);
                    
                    if (!(bool)radioBarras.IsChecked)
                    {
                        Path gráfico = prodParaDibujar.dibujarGráficoLínea(unProdDibujar, inicio, separaciónX);
                       gridGráfico.Children.Add(gráfico);
                    }
                    else
                    {
                        foreach (Line miLínea in prodParaDibujar.dibujarGráficoBarras(unProdDibujar, inicio, separaciónX))
                            gridGráfico.Children.Add(miLínea);
                    }

                    Line ejeX = new Line(); //se dibuja el eje X
                    ejeX.X1 = borde; // - separaciónX; //se le quita un valor de x para que el primer valor no se solape con el 0
                    ejeX.Y1 = inicio.Y;
                    ejeX.X2 = anchoGráfico - borde;
                    ejeX.Y2 = inicio.Y;
                    ejeX.Stroke = Brushes.Red;
                    ejeX.StrokeThickness = 3;
                    gridGráfico.Children.Add(ejeX);

                    Label etiquetaInicioX = new Label(); //la etiqueta inicio de eje X con el día anterior al seleccionado como inicio
                    etiquetaInicioX.Content = dateFechaInicio.SelectedDate.Value.Subtract(new TimeSpan(1,0,0,0)).ToShortDateString();
                    Thickness ubicación = new Thickness(ejeX.X1 - 10, inicio.Y + 5, 0, 0);
                    etiquetaInicioX.Margin = ubicación;
                    etiquetaInicioX.Foreground = Brushes.Blue;
                    gridGráfico.Children.Add(etiquetaInicioX);

                    Label etiquetaFinX = new Label(); //la etiqueta fin de eje X
                    etiquetaFinX.Content = dateFechaFin.SelectedDate.Value.ToShortDateString();
                    ubicación = new Thickness(ejeX.X2 - 10, inicio.Y + 5, 0, 0);
                    etiquetaFinX.Margin = ubicación;
                    etiquetaFinX.Foreground = Brushes.Blue;
                    gridGráfico.Children.Add(etiquetaFinX);

                    Label etiquetaCantidadDías = new Label(); //la etiqueta en X de la cantida de días del intervalo de tiempo
                    TimeSpan días = dateFechaFin.SelectedDate.Value - dateFechaInicio.SelectedDate.Value;
                    etiquetaCantidadDías.Content = "--- Se muestran "+ (días.Days + 1) + " días ---";
                    ubicación = new Thickness ((ejeX.X2 / 2) - 25, inicio.Y + 5, 0, 0);
                    etiquetaCantidadDías.Margin = ubicación;
                    etiquetaCantidadDías.Foreground = Brushes.Blue;
                    gridGráfico.Children.Add(etiquetaCantidadDías);

                    int largoRayita = 5;
                    double sumador = inicio.X + separaciónX;
                    for (int i = 0; i < unProdDibujar.valores.Count; i++)
                    {
                        Line rayita = new Line(); //se dibujan las rayitas del eje X
                        rayita.X1 = sumador;
                        rayita.Y1 = inicio.Y - largoRayita;
                        rayita.X2 = sumador;
                        rayita.Y2 = inicio.Y + largoRayita;
                        rayita.Stroke = Brushes.Red;
                        rayita.StrokeThickness = 1;
                        gridGráfico.Children.Add(rayita);
                        sumador += separaciónX;
                    }


                    //---------- Eje Y --------------------
                    Line ejeY = new Line(); //se dibuja el eje Y
                    ejeY.X1 = ejeX.X1;
                    ejeY.Y1 = inicio.Y;
                    ejeY.X2 = ejeX.X1;
                    ejeY.Y2 = 0;// gridGráfico.Height - ejeY.Y1;
                    ejeY.Stroke = Brushes.Red;
                    ejeY.StrokeThickness = 3;
                    gridGráfico.Children.Add(ejeY);

                    Label etiquetaInicioY = new Label(); //la etiqueta inicio de eje y
                    etiquetaInicioY.Content = "0";
                    ubicación = new Thickness(ejeX.X1 - 35, inicio.Y -15 , 0, 0);
                    etiquetaInicioY.Margin = ubicación;
                    etiquetaInicioY.Foreground = Brushes.Blue;
                    gridGráfico.Children.Add(etiquetaInicioY);

                    Label etiquetaFinY = new Label(); //la etiqueta fin de eje y
                    etiquetaFinY.Content = (int)(ejeY.Y1 / 10); //se le quita el incremento que se le puso a los valores al dibujarlos por visual
                    ubicación = new Thickness(inicio.X - 45, -10, 0, 0);
                    etiquetaFinY.Margin = ubicación;
                    etiquetaFinY.Foreground = Brushes.Blue;
                    gridGráfico.Children.Add(etiquetaFinY);


                    //sumador = inicio.Y + 10;
                    for (int i = 10; i < ejeY.Y1 + 10; i +=10)
                    {
                        Line rayita = new Line(); //se dibujan las rayitas del eje y
                        rayita.X1 = ejeX.X1 + largoRayita;
                        rayita.Y1 = inicio.Y - i;
                        rayita.X2 = ejeX.X1 - largoRayita;
                        rayita.Y2 = inicio.Y - i;
                        rayita.Stroke = Brushes.Red;
                        rayita.StrokeThickness = 1;
                        gridGráfico.Children.Add(rayita);
                        //sumador+=10;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void cmbCódigoCliente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbCódigoCliente.SelectedItem != null)
            {
                if (!cmbNombreCliente.IsFocused)
                {
                    if (cmbCódigoCliente.SelectedItem.ToString() == "0")
                    {
                        cmbNombreCliente.SelectedItem = "Todos los clientes";
                    }
                    else
                    {
                        cmbNombreCliente.SelectedItem = cliente.recuperarNombreCliente(int.Parse(cmbCódigoCliente.SelectedItem.ToString()));
                    }
                }
            }
        }

        private void cmbCódigoCliente_KeyUp(object sender, KeyEventArgs e)
        {
            if (auxiliar.chequearDatoNuméricoVálido(IDsClientes, cmbCódigoCliente.Text))
            {
                if (cmbCódigoCliente.SelectedItem == "0")
                    cmbNombreCliente.SelectedItem = "Todos los clientes";
                else
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
                    cmbNombreCliente.Text = "Todos los clientes";
                }
            }
        }

        private void cmbNombreCliente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbNombreCliente.SelectedItem != null)
            {
                if (cmbNombreCliente.SelectedItem.ToString() == "Todos los clientes")
                {
                    cmbCódigoCliente.SelectedItem = "0";
                }
                else
                {
                    cmbCódigoCliente.SelectedItem = cliente.recuperarIdCliente(cmbNombreCliente.SelectedItem.ToString()).ToString();
                }
            }
        }

        private void cmbNombreCliente_KeyUp(object sender, KeyEventArgs e)
        {
            //si lo que hay escrito es un nombre de cliente existente se busca su id
            if (auxiliar.chequearDatoCadenaVálido(nombresClientes, cmbNombreCliente.Text))
            {
                if (cmbNombreCliente.SelectedItem.ToString() == "Todos los clientes")
                {
                    cmbCódigoCliente.SelectedItem = "0";
                }
                else
                {
                    cmbCódigoCliente.Text = cliente.recuperarIdCliente(cmbNombreCliente.Text).ToString();
                }
                //if (e.Key == Key.Return) llenarRegistros(int.Parse(cmbIdCliente.Text)); //si se aprieta enter
            }
            else
            {
                cmbCódigoCliente.Text = "0";
            }
        }

        //private tiposDibujo calcularTipoDibujo()
        //{
        //    tiposDibujo duración=tiposDibujo.mes;
        //    DateTime fechaInicio, fechaFin;
        //    if (dateFechaInicio.SelectedDate == null)
        //    {
        //        return duración;
        //    }

        //    if (dateFechaInicio.SelectedDate == null)
        //    {
        //        return duración;
        //    }

        //    fechaInicio=dateFechaInicio.SelectedDate.Value;
        //    fechaFin=dateFechaFin.SelectedDate.Value;
        //    TimeSpan calculando = fechaInicio - fechaFin;
        //    int días = calculando.Days;

        //    //corregir que use un if 
        //    switch (días)
        //    {
        //        case 5:
        //            duración=tiposDibujo.día;
        //            break;
        //    }
        //    return duración;
        //}
    }

    //class productoAuxiliar
    //{
    //    public int Código { get; set; }
    //    public string Nombre { get; set; }

    //    public static List<productoAuxiliar> cargarProductos(List<string> listaProductos)
    //    {
    //        List<productoAuxiliar> miLista = new List<productoAuxiliar>();

    //        foreach (string nombre in listaProductos)
    //        {
    //            productoAuxiliar unProd = new productoAuxiliar();
    //            unProd.Nombre = nombre;
    //            unProd.Código = artículo.recuperarIDporNombre(nombre);
    //            miLista.Add(unProd);
    //        }
    //        return miLista;
    //    }
    //}

    class prodParaDibujar
    {
        public prodParaDibujar() { valores = new List<int>(); }
        public List<int> valores {get; set;}
        //public tiposDibujo duración {get; set;}

        public static Path dibujarGráficoLínea (prodParaDibujar producto, Point coordInicio, double separaciónX)
        {
            PathFigure myPathFigure = new PathFigure();
            myPathFigure.StartPoint = coordInicio; //new Point(valorX, 100);
            PathSegmentCollection myPathSegmentCollection = new PathSegmentCollection();
            

            for (int i = 0; i < producto.valores.Count; i++)
            {
                LineSegment myLineSegment = new LineSegment();
                myLineSegment.Point = new Point(coordInicio.X + separaciónX, myPathFigure.StartPoint.Y - producto.valores[i]*10);
                myPathSegmentCollection.Add(myLineSegment);
                coordInicio.X = myLineSegment.Point.X;
            }
            

            myPathFigure.Segments = myPathSegmentCollection;

            PathFigureCollection myPathFigureCollection = new PathFigureCollection();
            myPathFigureCollection.Add(myPathFigure);

            PathGeometry myPathGeometry = new PathGeometry();
            myPathGeometry.Figures = myPathFigureCollection;

            Path myPath = new Path();
            myPath.Stroke = Brushes.Green;
            myPath.StrokeThickness = 2;
            myPath.Data = myPathGeometry; 
            return myPath;

        }


        public static List<Line> dibujarGráficoBarras(prodParaDibujar producto, Point coordInicio, double separaciónX)
        {
            List<Line> misBarras = new List<Line>();

            for (int i = 0; i < producto.valores.Count; i++)
            {
                Line miLínea = new Line();
                miLínea.Stroke = Brushes.Green;
                miLínea.StrokeThickness = 6;
                miLínea.X1 =coordInicio.X + separaciónX;
                miLínea.Y1 = coordInicio.Y;
                miLínea.X2 = miLínea.X1;
                miLínea.Y2 = coordInicio.Y - producto.valores[i] * 10;
                misBarras.Add(miLínea);

                coordInicio.X = miLínea.X1;
            }
            return misBarras;
        }
    }

    //public enum tiposDibujo {día, semana, mes, trimestre, año}

    //class datosGráficosDibujados
    //{
    //    public Path gráfico { get; set; }
    //    public int index { get; set; }
    //    public string nombreProducto { get; set; }
    //}


}
