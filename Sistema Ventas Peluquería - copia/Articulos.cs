using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
////using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SistemaPeluquería
{
    public partial class Articulos : Form
    {
        public Articulos()
        {
            InitializeComponent();
        }

        //carga todos los artículos guardados en la BD
        private void Articulos_Load(object sender, EventArgs e)
        {
            List<artículo> misArtículos = artículo.cargarTodos();
            dataGridViewArtículos.DataSource = misArtículos;
        }

        //guarda un artículo nuevo
        private void button1_Click(object sender, EventArgs e)
        {
            string marca = txtMarca.Text.Trim();
            string producto= txtProducto.Text.Trim();
            double capacidad= double.Parse(txtCapacidad.Text);
            artículo miArtículo = new artículo(marca, producto, capacidad);
            miArtículo.guardar();
        }
    }
}
