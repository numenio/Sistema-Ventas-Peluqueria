using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SistemaPeluquería
{
    public partial class frmVentas : Form
    {
        public frmVentas()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form formulario = new Clientes ();
            formulario.MdiParent = this.MdiParent;
            formulario.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form formulario = new Articulos ();
            formulario.MdiParent = this.MdiParent;
            formulario.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form formulario = new Stock ();
            formulario.MdiParent = this.MdiParent;
            formulario.Show();
        }

        private void frmVentas_Load(object sender, EventArgs e)
        {
            numHoras.Value = DateTime.Now.Hour;
            numMinutos.Value = DateTime.Now.Minute;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form formulario = new Pagos ();
            formulario.MdiParent = this.MdiParent;
            formulario.Show();
        }

        //private void horas_ValueChanged(object sender, EventArgs e)
        //{
        //}
    }
}
