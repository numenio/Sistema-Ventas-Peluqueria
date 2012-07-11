using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Linq;


namespace ProcesosNegocio
{
    public class artículo
    {
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Marca
        {
            get { return _marca; }
            set { _marca = value; }
        }

        public string Producto
        {
            get { return _producto; }
            set { _producto = value; }
        }

        private double _capacidad;

        public double Capacidad
        {
            get { return _capacidad; }
            set { _capacidad = value; }
        }
        private string _marca;

        private string _producto;

        private double _stock;

        public double Stock
        {
            get { return _stock; }
            set { _stock = value; }
        }

        public bool guardar()
        {
            try
            {
                AdminData ad = new AdminData();
                ad.guardarArtículo(this);
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public bool actualizar()
        {
            try
            {
                AdminData ad = new AdminData();
                return ad.modificarArtículo(this);
            }
            catch 
            {
                
                return false;
            }
        }

        public bool eliminar()
        {
            try
            {
                AdminData ad = new AdminData();
                ad.eliminarArtículo (this);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public artículo()
        {
            //_capacidad = 0;
            //_marca = "sin llenar";
            //_producto = "sin llenar";
            //_stock = 0;
        }
        
        public artículo(string marca, string producto, double capacidad){
            this.Marca = marca;
            this.Producto = producto;
            this.Capacidad = capacidad;
        }

        private int _id;


        //public bool cargar(int id_Artículo_a_Cargar)
        //{
        //    try
        //    {
        //        AdminData ad = new AdminData();
        //        this = ad.recuperarArtículo(id_Artículo_a_Cargar);
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        public static List<artículo> cargarTodos()
        {
            AdminData ad = new AdminData();
            List<artículo> misArtículos = ad.recuperarTodosLosArtículos();
            return misArtículos;
        }

        public static bool YaExiste (artículo artículoAChequear)
        {
            AdminData ad = new AdminData();
            
            if (ad.chequearArtículo(artículoAChequear))
                return true;
            else
                return false;
        }

        public static bool cargarAStock(int códigoArtículo, int cuántoCargar)
        {
            AdminData ad = new AdminData();
            
            if (ad.cargarStockArtículo(códigoArtículo, cuántoCargar))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static double recuperarTodoStock(int códigoArtículo)
        {
            AdminData ad = new AdminData();
            int ingresos=ad.recuperarSumaIngresosStock (códigoArtículo);
            int salidas=ad.recuperarTodasSalidasStock (códigoArtículo);
            return ingresos - salidas;
        }

        public static double recuperarTodoStock(int códigoArtículo, DateTime desdeCuándo)
        {
            AdminData ad = new AdminData();
            int ingresos = ad.recuperarSumaIngresosStock(códigoArtículo, desdeCuándo);
            int salidas = ad.recuperarTodasSalidasStock(códigoArtículo, desdeCuándo);
            return ingresos - salidas;
        }

        public static artículo cargarArtículo(int id)
        {
            AdminData ad = new AdminData();
            return ad.recuperarArtículo(id);
        }

        public bool cargarArtículo(string marca, string producto, double capacidad)
        {
            throw new System.NotImplementedException();
        }

        public static List<string> recuperarTodosLosNombres()
        {
            AdminData ad = new AdminData();
            return ad.recuperarTodosLosNombresArtículos();
        }

        public static string recuperarNombreArtículo(int id)
        {
            AdminData ad = new AdminData();
            return ad.recuperarNombreArtículo(id);
        }

        public static int recuperarIDporNombre(string nombre)
        {
            AdminData ad = new AdminData();
            return ad.recuperarIDArtículoPorNombre(nombre);
        }

        public static List<string> recuperarTodasID()
        {
            AdminData ad = new AdminData();
            return ad.recuperarTodosLos_ID_Artículos();
        }

        //controla que no se repitan los nombres de las marcas
        public static List<string> sóloMarcas(List<artículo> artículos)
        {
            var marcas = (from art in artículos select art.Marca).Distinct();
            List<string> marcasFiltradas = marcas.ToList();
            marcasFiltradas.Sort();
            return marcasFiltradas;   
        }

        public static List<string> sóloProductos(List<artículo> artículos)
        {
            var productos = (from prod in artículos select prod.Producto).Distinct();
            List<string> productosFiltrados = productos.ToList();
            productosFiltrados.Sort();
            return productosFiltrados;
        }

        public static List<string> sóloCapacidad(List<artículo> artículos)
        {
            var capacidad = (from capac in artículos select capac.Capacidad.ToString()).Distinct();
            List<string> capacidadesFiltradas = capacidad.ToList();
            capacidadesFiltradas.Sort();
            return capacidadesFiltradas;
        }
    }
}
