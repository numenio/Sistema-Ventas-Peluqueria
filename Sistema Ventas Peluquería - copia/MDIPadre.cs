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
    public partial class MDIPadre : Form
    {
        public MDIPadre()
        {
            InitializeComponent();
        }
        
        private void MDIPadre_Load(object sender, EventArgs e)
        {
            cargarForm(new frmVentas());

            //artículo art = new artículo("miMarca", "MiProducto", 1.5);
            //art.eliminar();

        }

        private void cargarForm(Form quéForm)
        {
            quéForm.MdiParent = this;
            quéForm.Show();
        }

        private bool FormIsOpen(String FormABuscar)
        {
            bool lEncontrado = false;

            foreach (Form form in Application.OpenForms)
            {
                if (form.Name == FormABuscar)
                {
                    form.WindowState = FormWindowState.Normal;
                    form.Activate();
                    lEncontrado = true;
                    break;
                }
            }
            return lEncontrado;
        }

        private void lblPagos_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.FormIsOpen("Pagos") == false) cargarForm(new Pagos());
        }

        private void lblVentas_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.FormIsOpen("frmVentas") == false) cargarForm(new frmVentas());
        }

        private void lblArtículos_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.FormIsOpen("Articulos") == false) cargarForm(new Articulos());
        }

        private void lblClientes_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.FormIsOpen("Clientes") == false) cargarForm(new Clientes());
        }

        private void lblStock_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.FormIsOpen("Stock") == false) cargarForm(new Stock());
        }

        

       
        
    }
}
