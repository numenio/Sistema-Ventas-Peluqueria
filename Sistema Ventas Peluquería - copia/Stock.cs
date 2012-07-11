using System;
using System.Collections.Generic;
//using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace SistemaPeluquería
{
    public partial class Stock : Form
    {
        //guarda todos los artículos en memoria para trabajar más cómodo con ellos
        private List<artículo> misArtículos=new List<artículo>();
        
        public Stock()
        {
            InitializeComponent();
        }

        private void Stock_Load(object sender, EventArgs e)
        {
            misArtículos = artículo.cargarTodos();
            if (misArtículos.Count > 1)
            {
                artículo miArtículo = misArtículos[0];
                this.cargarArtículoEnCombos(miArtículo);
            }
            else
            {
                btnGuardar.Enabled = false;
            }
            

            //generarBD_XML(Application.StartupPath);
        }


        public void generarBD_XML(string ruta)
        {
            //la BD
            DataSet baseDatos = new DataSet("BD_peluquerías");

            //las tablas
            DataTable artículos = new DataTable("artículos");
            DataTable clientes = new DataTable("clientes");
            DataTable teléfonos = new DataTable("teléfonos");
            DataTable emails = new DataTable("emails");
            DataTable ventas = new DataTable("ventas");
            DataTable pagos = new DataTable("pago");
            DataTable stock = new DataTable("stock");

            //columnas de artículos
            DataColumn idArtículo = new DataColumn("idArtículo", typeof(int));
            idArtículo.AutoIncrement = true;
            idArtículo.ReadOnly = true;
            idArtículo.AllowDBNull = false;
            DataColumn marca = new DataColumn("marca", typeof(string));
            DataColumn producto = new DataColumn("producto", typeof(string));
            DataColumn capacidad = new DataColumn("__capacidad", typeof(int));
            artículos.Columns.Add(idArtículo);
            artículos.Columns.Add(marca);
            artículos.Columns.Add(producto);
            artículos.Columns.Add(capacidad);
            artículos.PrimaryKey = new DataColumn[] { artículos.Columns[0] };

            //columnas de clientes
            DataColumn idCliente = new DataColumn("idCliente", typeof(int));
            idCliente.AutoIncrement = true;
            idCliente.ReadOnly = true;
            idCliente.AllowDBNull = false;
            DataColumn nombre = new DataColumn("_nombre", typeof(string));
            DataColumn apellido = new DataColumn("_apellido", typeof(string));
            DataColumn dirección = new DataColumn("_dirección", typeof(string));
            DataColumn peluquería = new DataColumn("_peluquería", typeof(string));
            clientes.Columns.Add(idCliente);
            clientes.Columns.Add(nombre);
            clientes.Columns.Add(apellido);
            clientes.Columns.Add(dirección);
            clientes.Columns.Add(peluquería);
            clientes.PrimaryKey = new DataColumn[] { clientes.Columns[0] };


            //columnas teléfonos
            DataColumn idTeléfono = new DataColumn("idTeléfono", typeof(int));
            idTeléfono.AutoIncrement = true;
            idTeléfono.ReadOnly = true;
            idTeléfono.AllowDBNull = false;
            DataColumn idCl = new DataColumn("idCliente", typeof(int));
            DataColumn número = new DataColumn("número", typeof(int));
            teléfonos.Columns.Add(idTeléfono);
            teléfonos.Columns.Add(idCl);
            teléfonos.Columns.Add(número);
            teléfonos.PrimaryKey = new DataColumn[] { teléfonos.Columns[0] };

            //columnas e-mails
            DataColumn idEMail = new DataColumn("ideMail", typeof(int));
            idEMail.AutoIncrement = true;
            idEMail.ReadOnly = true;
            idEMail.AllowDBNull = false;
            DataColumn idClient = new DataColumn("idCliente", typeof(int));
            DataColumn email = new DataColumn("email", typeof(string));
            emails.Columns.Add(idEMail);
            emails.Columns.Add(idClient);
            emails.Columns.Add(email);
            emails.PrimaryKey = new DataColumn[] { emails.Columns[0] };


            //columnas pago
            DataColumn idPago = new DataColumn("idPago", typeof(int));
            idPago.AutoIncrement = true;
            idPago.ReadOnly = true;
            idPago.AllowDBNull = false;
            DataColumn idClien = new DataColumn("idCliente", typeof(int));
            DataColumn monto = new DataColumn("monto", typeof(decimal));
            DataColumn fecha = new DataColumn("fecha", typeof(DateTime));
            pagos.Columns.Add(idPago);
            pagos.Columns.Add(idClien);
            pagos.Columns.Add(monto);
            pagos.Columns.Add(fecha);
            pagos.PrimaryKey = new DataColumn[] { pagos.Columns[0] };


            //columnas de ventas
            DataColumn idVenta = new DataColumn("idVenta", typeof(int));
            idVenta.AutoIncrement = true;
            idVenta.ReadOnly = true;
            idVenta.AllowDBNull = false;
            DataColumn idClie = new DataColumn("idCliente", typeof(int));
            DataColumn idArtícul = new DataColumn("idArtículo", typeof(int));
            DataColumn cantidad = new DataColumn("cantidad", typeof(int));
            DataColumn precio = new DataColumn("precio", typeof(decimal));
            DataColumn fech = new DataColumn("fecha", typeof(DateTime));
            ventas.Columns.Add(idVenta);
            ventas.Columns.Add(idClie);
            ventas.Columns.Add(idArtícul);
            ventas.Columns.Add(cantidad);
            ventas.Columns.Add(precio);
            ventas.Columns.Add(fech);
            ventas.PrimaryKey = new DataColumn[] { ventas.Columns[0] };


            //columnas de stock
            DataColumn idIngresoStock = new DataColumn("idIngresoStock", typeof(int));
            idIngresoStock.AutoIncrement = true;
            idIngresoStock.ReadOnly = true;
            idIngresoStock.AllowDBNull = false;
            DataColumn idArtí = new DataColumn("idArtículo", typeof(int));
            DataColumn cant = new DataColumn("cantidad", typeof(int));
            stock.Columns.Add(idIngresoStock);
            stock.Columns.Add(idArtí);
            stock.Columns.Add(cant);
            stock.PrimaryKey = new DataColumn[] { stock.Columns[0] };


            baseDatos.Tables.Add(artículos);
            baseDatos.Tables.Add(clientes);
            baseDatos.Tables.Add(teléfonos );
            baseDatos.Tables.Add(emails );
            baseDatos.Tables.Add(ventas );
            baseDatos.Tables.Add(pagos );
            baseDatos.Tables.Add(stock );

            baseDatos.WriteXml(ruta + "\\BD.xml", XmlWriteMode.WriteSchema);
        }

        private void cargarArtículoEnCombos(artículo miArtículo)
        {
            cmbCódigo.Text = miArtículo.id.ToString();
            cmbMarca.Text = miArtículo.Marca;
            cmbProducto.Text = miArtículo.Producto;
            cmbCapacidad.Text = miArtículo.Capacidad.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            artículo miArtículo = artículo.cargarArtículo(int.Parse(cmbCódigo.Text));
            miArtículo.Stock = (double)numStock.Value;
            
            //arreglar que guarde la modificación del artículo
            //miArtículo
        }


    }
}
