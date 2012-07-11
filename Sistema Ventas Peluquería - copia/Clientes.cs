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
    public partial class Clientes : Form
    {
        public Clientes()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //se cargan los datos del cliente menos tel y mails
            cliente cli = new cliente();
            cli.Nombre = txtNombre.Text;
            cli.Apellido = txtApellido.Text;
            cli.Dirección = txtDirección.Text;
            cli.Peluquería = txtPeluquería.Text;
            cli.Localidad = txtLocalidad.Text;

            ArrayList miArrayTel = new ArrayList();
            ArrayList miArrayMails = new ArrayList();
            
            //se guardan los teléfonos  
            foreach (string str in listBoxTeléfonos.Items)
            {
                miArrayTel.Add(str);
            }

            cli.Teléfonos = miArrayTel;

            //se guardan los mails
            foreach (string str in listBoxMail.Items)
            {
                miArrayMails.Add(str);
            }

            cli.Emails = miArrayMails;

            cli.guardar();
        }

        //arreglar que no se ponga cualquier verdura en tel y mail
        private void button1_Click(object sender, EventArgs e)
        {
            listBoxTeléfonos.Items.Add(txtTeléfono.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBoxMail.Items.Add(txtMail.Text);
        }

        private void Clientes_Load(object sender, EventArgs e)
        {
            List<cliente> misClientes = cliente.cargarTodos();
            dataGridViewClientes.DataSource = misClientes;
        }

        
    }
}
