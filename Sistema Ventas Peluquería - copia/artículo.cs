using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace SistemaPeluquería
{
    public class artículo
    {
        private double _capacidad;

        public double Capacidad
        {
            get { return _capacidad; }
            set { _capacidad = value; }
        }
        private string _marca;

        public string Marca
        {
            get { return _marca; }
            set { _marca = value; }
        }
        private string _producto;

        public string Producto
        {
            get { return _producto; }
            set { _producto = value; }
        }

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

        public artículo(){}
        
        public artículo(string marca, string producto, double capacidad){
            this.Marca = marca;
            this.Producto = producto;
            this.Capacidad = capacidad;
        }

        private int _id;

        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

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

        public bool cargarAStock(int cuántoCargar)
        {
            throw new System.NotImplementedException();
        }

        public double recuperarStock()
        {
            return this.Stock;
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

        
    }
}
