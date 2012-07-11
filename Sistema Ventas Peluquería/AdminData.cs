using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Globalization;
//borrar
//using System.Windows.Forms;

namespace ProcesosNegocio
{
    class AdminData
    {
        //OleDbConnection cnn;
        private static string _rutaBD = @"Provider=Microsoft.Jet.OLEDB.4.0; User ID=Admin; Jet OLEDB:Database Password=pa$$word; Data Source=peluquería.mdb";
      
        public string rutaBD
        {
            get
            {
                return _rutaBD;
            }
            set
            {
                _rutaBD = rutaBD;
            }
        }


        public bool guardarArtículo(artículo miArtículo)
        {
            
            OleDbConnection cnn = new OleDbConnection(_rutaBD);

            try
            {
                cnn.Open();

                string comandoInsert = "'" + miArtículo.Marca + "', '" + miArtículo.Producto + "', '" + miArtículo.Capacidad + "'";

                OleDbCommand cmd = new OleDbCommand("INSERT INTO Artículos (marca, producto, capacidad) VALUES (" + comandoInsert + ")", cnn);

                cmd.ExecuteNonQuery();

                cnn.Close();

                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public bool guardarCliente(cliente miCliente)
        {
            OleDbConnection cnn = new OleDbConnection(_rutaBD);

            try
            {
                cnn.Open();

                //se guarda el cliente
                string comandoInsert = "'" + miCliente.Nombre + "', '" + miCliente.Apellido + "', '" + miCliente.Dirección + "', '" + miCliente.Localidad + "', '" + miCliente.Peluquería + "', '" + miCliente.fecha_nacimiento + "'";
                OleDbCommand cmd = new OleDbCommand("INSERT INTO Clientes (nombre, apellido, dirección, localidad, peluquería, fechaNacimiento) VALUES (" + comandoInsert + ")", cnn);
                cmd.ExecuteNonQuery();

                //se recupera la id del cliente que se acaba de guardar
                int id = this.recuperar_ID_cliente(miCliente.Nombre, miCliente.Apellido);

                //se guardan los teléfonos de esa id
                guardarTeléfonos(miCliente.Teléfonos, id);

                //se guardan los mails de esa id
                guardarMails(miCliente.Emails, id);

                cnn.Close();

                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool guardarTeléfonos(List<Teléfono> teléfonos, int id)
        {
            OleDbConnection cnn = new OleDbConnection(_rutaBD);

            try
            {
                cnn.Open();

                foreach (Teléfono tel in teléfonos)
                {
                    string comandoInsert = id + ", '" + tel.Número + "'";
                    OleDbCommand cmd = new OleDbCommand("INSERT INTO Teléfonos (idCliente, número) VALUES (" + comandoInsert + ")", cnn);
                    cmd.ExecuteNonQuery();
                }

                cnn.Close();
                return true;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
        }

        private bool guardarMails(List<Mail> mails, int id)
        {
            OleDbConnection cnn = new OleDbConnection(_rutaBD);

            try
            {
                cnn.Open();

                foreach (Mail mail in mails)
                {
                    string comandoInsert =  id + ", '" + mail.Email + "'";
                    OleDbCommand cmd = new OleDbCommand("INSERT INTO Mails (idCliente, mail) VALUES (" + comandoInsert + ")", cnn);
                    cmd.ExecuteNonQuery();
                }

                cnn.Close();
                return true;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
                return false;
            }
        }

        private int recuperar_ID_cliente(string nombre, string apellido, string dirección, string localidad, string peluquería)
        {
            try
            {
                OleDbConnection cnn = new OleDbConnection(_rutaBD);
                cnn.Open();

                string cadenaSelect = "Select id from Clientes where nombre = '" + nombre + "' and apellido = '" + apellido + "' and dirección = '" + dirección + "' and localidad = '" + localidad + "' and peluquería = '" + peluquería +"'";
                OleDbCommand cmd = new OleDbCommand(cadenaSelect, cnn);
                OleDbDataReader miLector = cmd.ExecuteReader();

                miLector.Read();
                int id = miLector.GetInt32(0);

                miLector.Close();
                cnn.Close();
                return id;
            }
            catch (Exception ex)
                {
                    Debug.Write(ex.Message);
                    return 0;
                }
        }

        private int recuperar_ID_cliente(string nombre, string apellido)
        {
            try
            {
                OleDbConnection cnn = new OleDbConnection(_rutaBD);
                cnn.Open();

                string cadenaSelect = "Select id from Clientes where nombre = '" + nombre + "' and apellido = '" + apellido + "'";
                OleDbCommand cmd = new OleDbCommand(cadenaSelect, cnn);
                OleDbDataReader miLector = cmd.ExecuteReader();

                miLector.Read();
                int id = miLector.GetInt32(0);

                miLector.Close();
                cnn.Close();
                return id;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
                return 0;
            }
        }

        private int recuperar_ID_cliente(string nombre)
        {
            try
            {
                OleDbConnection cnn = new OleDbConnection(_rutaBD);
                cnn.Open();

                string cadenaSelect = "Select id from Clientes where nombre = '" + nombre + "'";
                OleDbCommand cmd = new OleDbCommand(cadenaSelect, cnn);
                OleDbDataReader miLector = cmd.ExecuteReader();

                miLector.Read();
                int id = miLector.GetInt32(0);

                miLector.Close();
                cnn.Close();
                return id;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
                return 0;
            }
        }

        public int recuperar_IDcliente(string nombreCliente)
        {
            string nombre = "", apellido = "";
            char[] caracteres = { ' ', ',', '.', ':', '\t' };
            //nombre = ;
            string[] cadena = nombreCliente.Split(caracteres);
            nombre = cadena[0];
            if (cadena.Length > 1) apellido = cadena[1];
            if (string.IsNullOrEmpty(apellido))
            {
                return this.recuperar_ID_cliente(nombre);
            }
            else
            {
                return this.recuperar_ID_cliente(nombre, apellido);
            }
        }

        public string recuperar_nombre_cliente(int id)
        {
            try
            {
                OleDbConnection cnn = new OleDbConnection(_rutaBD);
                cnn.Open();

                string cadenaSelect = "Select nombre, apellido from Clientes where id = " + id;
                OleDbCommand cmd = new OleDbCommand(cadenaSelect, cnn);
                OleDbDataReader miLector = cmd.ExecuteReader();

                miLector.Read();
                string nombre = miLector.GetString(0) + " " + miLector.GetString(1);

                miLector.Close();
                cnn.Close();
                return nombre;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
                return "";
            }
        }

        public saldo recuperarSaldoCliente(int idCliente)
        {
            saldo miSaldo = new saldo(); ;
            OleDbConnection cnn = new OleDbConnection(_rutaBD);
            double pagos, ventas;

            try
            {
                cnn.Open();

                //se recuperan los pagos
                OleDbCommand cmd = new OleDbCommand("select sum(pagos.monto) from pagos where idcliente = " + idCliente, cnn);
                OleDbDataReader miLector = cmd.ExecuteReader();

                if (miLector.HasRows)
                {
                    miLector.Read();

                    try
                    {
                        pagos = miLector.GetDouble(0);
                    }
                    catch
                    {
                        pagos = 0;
                    }
                }
                else
                {
                    pagos = 0;
                }

                //se recuperan las ventas
                cmd = new OleDbCommand("select sum(listaartículosVenta.cantidad * listaartículosventa.precio) from clientes, ventas, listaartículosventa where ventas.idcliente = clientes.id and listaartículosventa.idventa = ventas.id and clientes.id = " + idCliente, cnn);
                miLector = cmd.ExecuteReader();

                if (miLector.HasRows)
                {
                    miLector.Read();
                    try
                    {
                        ventas = miLector.GetDouble(0);
                    }
                    catch
                    {
                        ventas = 0;
                    }
                }
                else
                {
                    ventas = 0;
                }

                miSaldo.Totales = pagos - ventas;
                miSaldo.IdCliente = idCliente;
                miSaldo.Nombre = recuperar_nombre_cliente(idCliente);

                //miLector.Close();
                cnn.Close();

                return miSaldo;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                cnn.Close();
                return miSaldo;
            }
        }

        public bool guardarPago(pago miPago)
        {
            OleDbConnection cnn = new OleDbConnection(_rutaBD);

            try
            {
                cnn.Open();

                string comandoInsert = "'" + miPago.IdCliente + "', '" + miPago.IdVenta + "', '" + miPago.Monto + "', #" + miPago.Fecha + "#";

                OleDbCommand cmd = new OleDbCommand("INSERT INTO Pagos (idCliente, idVenta, monto, fecha) VALUES (" + comandoInsert + ")", cnn);

                cmd.ExecuteNonQuery();

                cnn.Close();

                return true;
            }
            catch
            {
                cnn.Close();
                return false;
            }
        }

        public bool guardarVenta(venta miVenta, pago miPago)
        {
            OleDbConnection cnn = new OleDbConnection(_rutaBD);

            try
            {
                cnn.Open();

                //se guardan los datos de la venta
                string comandoInsert = "'" + miVenta.idCliente + "', #" + miVenta.Fecha.ToShortDateString() + "#, #" + miVenta.Hora.ToShortTimeString() +"#";

                OleDbCommand cmd = new OleDbCommand("INSERT INTO Ventas (idCliente, fecha, hora) VALUES (" + comandoInsert + ")", cnn);

                cmd.ExecuteNonQuery();

                cnn.Close(); //cierro la conexión porque no está recuperando bien la id de la última venta

                int idVenta = recuperar_ID_Venta();

                cnn.Open();

                //por cada uno de los artículos comprados
                foreach (ventaArtículo art in miVenta.MisArtículos)
                {
                    //se guarda el artículo
                    comandoInsert = "'" + idVenta + "', '" + art.idArtículo + "', '" + art.cantidad + "', " + art.precio;

                    cmd = new OleDbCommand("INSERT INTO listaArtículosVenta (idVenta, idArtículo, cantidad, precio) VALUES (" + comandoInsert + ")", cnn);

                    cmd.ExecuteNonQuery();
                    
                    
                    //se guarda la salida del stock
                    comandoInsert = "'" + art.idArtículo + "', '" + idVenta + "', '" + art.cantidad + "', #" + miVenta.Fecha.ToShortDateString() + "#";

                    cmd = new OleDbCommand("INSERT INTO SalidasStock (idArtículo, idVenta, cantidadSalida, fecha) VALUES (" + comandoInsert + ")", cnn);

                    cmd.ExecuteNonQuery();
                }

                cnn.Close();

                //se guarda el pago de la venta
                miPago.IdVenta = idVenta;
                this.guardarPago(miPago);

                return true;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                cnn.Close();
                return false;
            }
        }

        private int recuperar_ID_Venta()
        {
            try
            {
                OleDbConnection cnn = new OleDbConnection(_rutaBD);
                cnn.Open();

                string cadenaSelect = "Select max(id) from Ventas";
                OleDbCommand cmd = new OleDbCommand(cadenaSelect, cnn);
                OleDbDataReader miLector = cmd.ExecuteReader();

                miLector.Read();
                int id = miLector.GetInt32(0);

                miLector.Close();
                cnn.Close();
                return id;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
                return 0;
            }
        }

        private List<int> recuperar_ID_Ventas_X_Artículo(int idArtículo)
        {
            List<int> idsVentas = new List<int>();

            try
            {
                OleDbConnection cnn = new OleDbConnection(_rutaBD);

                cnn.Open();

                string cadenaSelect = "select ventas.id from ventas, listaartículosventa where listaartículosventa.idventa = ventas.id and listaartículosventa.idArtículo = " + idArtículo;
                OleDbCommand cmd = new OleDbCommand(cadenaSelect, cnn);
                OleDbDataReader miLector = cmd.ExecuteReader();

                while (miLector.Read())
                    idsVentas.Add(miLector.GetInt32(0));

                miLector.Close();
                cnn.Close();
                return idsVentas;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
                return idsVentas;
            }
        }

        public artículo recuperarArtículo(int idArtículo)
        {
            artículo miArtículo = new artículo();
            OleDbConnection cnn = new OleDbConnection(_rutaBD);

            try
            {
                cnn.Open();
                OleDbCommand cmd = new OleDbCommand("Select id, marca, producto, capacidad from Artículos where id = " + idArtículo.ToString() + "", cnn);
                OleDbDataReader miLector = cmd.ExecuteReader();

                miLector.Read();
                //while (miLector.Read()) 
                //{
                    miArtículo.id = miLector.GetInt32(0);
                    miArtículo.Marca = miLector.GetString(1);
                    miArtículo.Producto = miLector.GetString(2);
                    miArtículo.Capacidad = miLector.GetDouble (3);
                //}

                miLector.Close();
                cnn.Close();

                return miArtículo;
            }
            catch
            {
                cnn.Close();
                return miArtículo;
            }
        }

        public artículo recuperarArtículo(string marca, string producto, double capacidad)
        {
            artículo miArtículo = new artículo();
            OleDbConnection cnn = new OleDbConnection(_rutaBD);

            try
            {
                cnn.Open();

                
                OleDbCommand cmd = new OleDbCommand("Select id from Artículos where marca = '" + marca + "' and producto = '" + producto + "' and capacidad = " + capacidad.ToString() + "" , cnn);
                OleDbDataReader miLector = cmd.ExecuteReader();

                miLector.Read();
                //while (miLector.Read()) 
                //{
                miArtículo.id = miLector.GetInt32(0);
                miArtículo.Marca = marca;
                miArtículo.Producto = producto;
                miArtículo.Capacidad = capacidad;
                //}

                miLector.Close();
                cnn.Close();

                return miArtículo;
            }
            catch (Exception ex)
            {
                Debug.Print (ex.Message);
                cnn.Close();
                return miArtículo;
            }
        }

        //si el artículo buscado existe ya en la base de datos
        public bool chequearArtículo(artículo artículoABuscar)
        {
            artículo miArtículo = recuperarArtículo(artículoABuscar.Marca, artículoABuscar.Producto, artículoABuscar.Capacidad);
            
            if (miArtículo.Producto != null)
            {
                return true;
            } 
            else 
            {
                return false;
            }
        }

        public bool cargarStockArtículo(int códigoArtículo, int cantidadStock)
        {
            OleDbConnection cnn = new OleDbConnection(_rutaBD);

            try
            {
                cnn.Open();

                OleDbCommand cmd = new OleDbCommand("INSERT INTO IngresosStock (idArtículo, CantidadIngreso, fecha) VALUES (" + códigoArtículo.ToString() + ", " + cantidadStock.ToString() + ", #"+ DateTime.Now.Date.ToString() + "#)", cnn);
                cmd.ExecuteNonQuery();
                cnn.Close();

                return true;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                cnn.Close();
                return false;
            }
        }

        public int recuperarSumaIngresosStock (int códigoArtículo, DateTime desdeCuándo)
        {
            OleDbConnection cnn = new OleDbConnection(_rutaBD);
            int sumador=0;

            try
            {
                cnn.Open();

                OleDbCommand cmd = new OleDbCommand("Select CantidadIngreso from IngresosStock where idArtículo = " + códigoArtículo.ToString() + " and fecha >= #" + desdeCuándo.Date.ToShortDateString() + "#", cnn);
                OleDbDataReader miLector = cmd.ExecuteReader();

                while (miLector.Read()) 
                {
                    sumador = sumador + miLector.GetInt32(0);
                }

                miLector.Close();
                cnn.Close();

                return sumador;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                cnn.Close();
                return sumador;
            }
        }

        public int recuperarSumaSalidasStock(int códigoArtículo, DateTime desdeCuándo)
        {
            OleDbConnection cnn = new OleDbConnection(_rutaBD);
            int sumador = 0;

            try
            {
                cnn.Open();

                OleDbCommand cmd = new OleDbCommand("Select CantidadSalida from SalidasStock where idArtículo = " + códigoArtículo.ToString() + " and fecha >= #" + desdeCuándo.Date.ToShortDateString() + "#", cnn);
                OleDbDataReader miLector = cmd.ExecuteReader();

                while (miLector.Read())
                {
                    sumador = sumador + miLector.GetInt32(0);
                }

                miLector.Close();
                cnn.Close();

                return sumador;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                cnn.Close();
                return sumador;
            }
        }

        public int recuperarSumaIngresosStock(int códigoArtículo)
        {
            DateTime fecha = new DateTime (2010, 1, 12);
            int result = recuperarSumaIngresosStock(códigoArtículo, fecha);
            return result;
        }

        public int recuperarSumaSalidasStock(int códigoArtículo)
        {
            DateTime fecha = new DateTime(2010, 1, 12);
            int result = recuperarSumaSalidasStock(códigoArtículo, fecha);
            return result;
        }

        public int recuperarTodasSalidasStock (int códigoArtículo, DateTime desdeCuándo)
        {
            OleDbConnection cnn = new OleDbConnection(_rutaBD);
            int sumador = 0;

            try
            {
                cnn.Open();

                OleDbCommand cmd = new OleDbCommand("Select CantidadSalida from SalidasStock where idArtículo = " + códigoArtículo.ToString() + " and fecha >= #" + desdeCuándo.Date.ToShortDateString() + "#", cnn);
                OleDbDataReader miLector = cmd.ExecuteReader();

                while (miLector.Read())
                {
                    sumador = sumador + miLector.GetInt32(0);
                }

                miLector.Close();
                cnn.Close();

                return sumador;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                cnn.Close();
                return sumador;
            }
        }

        public int recuperarTodasSalidasStock (int códigoArtículo)
        {
            DateTime fecha = new DateTime(2010, 1, 12);
            int result = recuperarTodasSalidasStock(códigoArtículo, fecha);
            return result;
        }

        private List<Teléfono> recuperarTeléfonos(int idCliente)
        {
            OleDbConnection cnn = new OleDbConnection(_rutaBD);
            List<Teléfono> teléfonos = new List<Teléfono>();

            try
            {
                cnn.Open();

                OleDbCommand cmd = new OleDbCommand("Select id, número from Teléfonos where idCliente = " + idCliente.ToString(), cnn);
                OleDbDataReader miLector = cmd.ExecuteReader();

                while (miLector.Read())
                {
                    Teléfono tel = new Teléfono();
                    tel.idTeléfono = miLector.GetInt32(0);
                    tel.Número = miLector.GetString(1);
                    teléfonos.Add (tel);
                }

                miLector.Close();
                cnn.Close();

                return teléfonos;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                cnn.Close();
                return teléfonos;
            }
        }

        private List<Mail> recuperarMails(int idCliente)
        {
            OleDbConnection cnn = new OleDbConnection(_rutaBD);
            List<Mail> mails = new List<Mail>();

            try
            {
                cnn.Open();

                OleDbCommand cmd = new OleDbCommand("Select id, mail from Mails where idCliente = " + idCliente.ToString(), cnn);
                OleDbDataReader miLector = cmd.ExecuteReader();

                while (miLector.Read())
                {
                    Mail miMail = new Mail();
                    miMail.idMail = miLector.GetInt32(0);
                    miMail.Email = miLector.GetString(1);
                    mails.Add(miMail);
                }

                miLector.Close();
                cnn.Close();

                return mails;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                cnn.Close();
                return mails;
            }
        }

        public bool nombrarBD(string nombreBD)
        {
            try
            {
                rutaBD = nombreBD;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool eliminarArtículo(artículo artículo_a_eliminar)
        {
            OleDbConnection cnn = new OleDbConnection(_rutaBD);
            try
            {
                cnn.Open();
                string cadenaSQL = "Delete from Artículos where Id=" + artículo_a_eliminar.id;
                OleDbCommand cmd = new OleDbCommand(cadenaSQL, cnn);
                cmd.ExecuteNonQuery();

                foreach (int idVenta in recuperar_ID_Ventas_X_Artículo(artículo_a_eliminar.id))
                {
                    cadenaSQL = "Delete from Pagos where IdVenta=" + artículo_a_eliminar.id;
                    cmd = new OleDbCommand(cadenaSQL, cnn);
                    cmd.ExecuteNonQuery();
                }

                cadenaSQL = "Delete from Ventas where IdArtículo=" + artículo_a_eliminar.id;
                cmd = new OleDbCommand(cadenaSQL, cnn);
                cmd.ExecuteNonQuery();

                cadenaSQL = "Delete from listaArtículosVentas where IdArtículo=" + artículo_a_eliminar.id;
                cmd = new OleDbCommand(cadenaSQL, cnn);
                cmd.ExecuteNonQuery();

                cadenaSQL = "Delete from IngresosStock where IdArtículo=" + artículo_a_eliminar.id;
                cmd = new OleDbCommand(cadenaSQL, cnn);
                cmd.ExecuteNonQuery();

                cadenaSQL = "Delete from SalidasStock where IdArtículo=" + artículo_a_eliminar.id;
                cmd = new OleDbCommand(cadenaSQL, cnn);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
            finally
            {
                cnn.Close();
            }
            
        }

        public bool eliminarCliente(int idCliente)
        {
            OleDbConnection cnn = new OleDbConnection(_rutaBD);
            try
            {
                cnn.Open();
                //borramos el cliente de la tabla cliente
                string cadenaSQL = "Delete from Clientes where Id = " + idCliente;
                OleDbCommand cmd = new OleDbCommand(cadenaSQL, cnn);
                cmd.ExecuteNonQuery();
                //borramos los teléfonos
                cadenaSQL = "Delete from Teléfonos where idCliente = " + idCliente;
                cmd.CommandText = cadenaSQL;
                cmd.ExecuteNonQuery();
                //borramos los mails
                cadenaSQL = "Delete from Mails where idCliente = " + idCliente;
                cmd.CommandText = cadenaSQL;
                cmd.ExecuteNonQuery();
                cnn.Close();

                cnn.Open();
                cadenaSQL = "select id from ventas where idcliente = " + idCliente;
                cmd = new OleDbCommand(cadenaSQL, cnn);
                OleDbDataReader miLector = cmd.ExecuteReader();

                while (miLector.Read())
                {
                    venta unaVenta = new venta();
                    unaVenta.Id=miLector.GetInt32(0);
                    eliminarVenta(unaVenta);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool eliminarPago(int pago_a_eliminar)
        {
            OleDbConnection cnn = new OleDbConnection(_rutaBD);
            try
            {
                cnn.Open();
                string cadenaSQL = "Delete from Pagos where Id=" + pago_a_eliminar;
                OleDbCommand cmd = new OleDbCommand(cadenaSQL, cnn);
                cmd.ExecuteNonQuery();
                cnn.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool eliminarVenta(venta venta_a_eliminar)
        {
            OleDbConnection cnn = new OleDbConnection(_rutaBD);
            try
            {
                cnn.Open();
                string cadenaSQL = "Delete from Ventas where Id=" + venta_a_eliminar.Id;
                OleDbCommand cmd = new OleDbCommand(cadenaSQL, cnn);
                cmd.ExecuteNonQuery();

                cadenaSQL = "Delete from Pagos where IdVenta=" + venta_a_eliminar.Id;
                cmd = new OleDbCommand(cadenaSQL, cnn);
                cmd.ExecuteNonQuery();

                cadenaSQL = "Delete from listaArtículosVenta where IdVenta=" + venta_a_eliminar.Id;
                cmd = new OleDbCommand(cadenaSQL, cnn);
                cmd.ExecuteNonQuery();

                cadenaSQL = "Delete from SalidasStock where IdVenta=" + venta_a_eliminar.Id;
                cmd = new OleDbCommand(cadenaSQL, cnn);
                cmd.ExecuteNonQuery();

                cnn.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool modificarArtículo(artículo artículo_a_modificar)
        {
            OleDbConnection cnn = new OleDbConnection(_rutaBD);
            try
            {
                cnn.Open();
                string cadenaSQL = "update artículos set marca = '" + artículo_a_modificar.Marca + "', producto = '" + artículo_a_modificar.Producto + "', capacidad = " + artículo_a_modificar.Capacidad + " where id = " + artículo_a_modificar.id;
                OleDbCommand cmd = new OleDbCommand(cadenaSQL, cnn);
                cmd.ExecuteNonQuery();
                cnn.Close();
                return true;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
        }

        public bool modificarCliente(cliente cliente_a_modificar)
        {
            OleDbConnection cnn = new OleDbConnection(_rutaBD);
            try
            {
                cnn.Open();
                string cadenaSQL = "UPDATE Clientes SET nombre = '" + cliente_a_modificar.Nombre + "', apellido = '" + cliente_a_modificar.Apellido + "', dirección = '" + cliente_a_modificar.Dirección + "', localidad = '" + cliente_a_modificar.Localidad + "', peluquería = '" + cliente_a_modificar.Peluquería + "', fechaNacimiento = #" + cliente_a_modificar.fecha_nacimiento + "# where id = " + cliente_a_modificar.Id;
                OleDbCommand cmd = new OleDbCommand(cadenaSQL, cnn);
                cmd.ExecuteNonQuery();

                //foreach (Teléfono tel in cliente_a_modificar.Teléfonos)
                //{
                //    cadenaSQL = "UPDATE Teléfonos SET número = '" + tel.Número + "' where id = " + tel.idTeléfono;
                //    cmd = new OleDbCommand(cadenaSQL, cnn);
                //    cmd.ExecuteNonQuery();
                //}

                //foreach (Mail miMail in cliente_a_modificar.Emails)
                //{
                //    cadenaSQL = "UPDATE Mails SET número = " + miMail.Email + " where id = " + miMail.idMail;
                //    cmd = new OleDbCommand(cadenaSQL, cnn);
                //    cmd.ExecuteNonQuery();
                //}

                //se borran los teléfonos y mails del cliente por si hubieron modificaciones
                cadenaSQL = "DELETE from Teléfonos where idCliente = " + cliente_a_modificar.Id;
                cmd = new OleDbCommand(cadenaSQL, cnn);
                cmd.ExecuteNonQuery();

                cadenaSQL = "DELETE from Mails where idCliente = " + cliente_a_modificar.Id;
                cmd = new OleDbCommand(cadenaSQL, cnn);
                cmd.ExecuteNonQuery();

                //se agregan los nuevos teléfonos y mails
                guardarTeléfonos(cliente_a_modificar.Teléfonos, cliente_a_modificar.Id);
                guardarMails(cliente_a_modificar.Emails, cliente_a_modificar.Id);

                cnn.Close();
                return true;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
        }

        //public void modificarPago(pago pago_a_modificar)
        //{
        //    OleDbConnection cnn = new OleDbConnection(_rutaBD);
        //    try
        //    {
        //        cnn.Open();
        //        string cadenaSQL = "Update pagos SET  where Id=" + pago_a_modificar.Id;
        //        OleDbCommand cmd = new OleDbCommand(cadenaSQL, cnn);
        //        cmd.ExecuteNonQuery();
        //        cnn.Close();
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        public bool modificarVenta(venta venta_a_modificar, pago pago_a_modificar)
        {
            OleDbConnection cnn = new OleDbConnection(_rutaBD);
            try
            {
                cnn.Open();
                //se actualizan los datos de la venta
                string cadenaSQL = "UPDATE ventas SET idcliente = " + venta_a_modificar.idCliente + ", fecha = #" + venta_a_modificar.Fecha.ToShortDateString() + "#, hora = #" + venta_a_modificar.Hora.ToShortTimeString() + "#";
                OleDbCommand cmd = new OleDbCommand(cadenaSQL, cnn);
                cmd.ExecuteNonQuery();

                //primero se eliminan todos los artículos de la venta por si se han borrado o modificado alguno de ellos
                cadenaSQL = "Delete from listaArtículosVenta where idVenta = " + venta_a_modificar.Id;
                cmd = new OleDbCommand(cadenaSQL, cnn);
                cmd.ExecuteNonQuery();

                //por cada uno de los artículos comprados
                foreach (ventaArtículo art in venta_a_modificar.MisArtículos)
                {
                    //luego se guardan nuevamente los artículos con sus posibles modificaciones
                    cadenaSQL = "'" + recuperar_ID_Venta() + "', '" + art.idArtículo + "', '" + art.cantidad + "', " + art.precio;
                    cmd = new OleDbCommand("INSERT INTO listaArtículosVenta (idVenta, idArtículo, cantidad, precio) VALUES (" + cadenaSQL + ")", cnn);
                    cmd.ExecuteNonQuery();

                    ////corregir que guarde la salida del stock, pero hay que ir viendo si se borra un artículo de la lista de los ya comprados, por lo que habría
                    ////que volver a sumarlo al stock
                    //cadenaSQL = "'" + art.idArtículo + "', '" + recuperar_ID_Venta() + "', '" + art.cantidad + "', #" + venta_a_modificar.Fecha.ToShortDateString() + "#";
                    //cmd = new OleDbCommand("INSERT INTO SalidasStock (idArtículo, idVenta, cantidadSalida, fecha) VALUES (" + cadenaSQL + ")", cnn);
                    //cmd.ExecuteNonQuery();
                }

                cnn.Close();

                //se actualiza el pago
                cadenaSQL = "update pagos set idCliente = " + pago_a_modificar.IdCliente + ", monto = " + pago_a_modificar.Monto + ", fecha = #" + pago_a_modificar.Fecha + "# where pagos.idVenta = " + pago_a_modificar.IdVenta;
                cmd = new OleDbCommand(cadenaSQL, cnn);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
        }

        public List<pagoParaMostrar> recuperarTodosLosPagosParaMostrar(int idCliente, DateTime fecha)
        {
            List<pagoParaMostrar> misPagos = new List<pagoParaMostrar>();
            OleDbConnection cnn = new OleDbConnection(_rutaBD);

            try
            {
                cnn.Open();
                OleDbCommand cmd;

                //si por error no hay fecha ni cliente
                if (idCliente == 0 && fecha == new DateTime(1111, 01, 01)) return misPagos;


                if (idCliente == 0) //si no es por cliente, o sea que es por fecha
                {
                    cmd = new OleDbCommand("select pagos.id, pagos.idcliente, pagos.idventa, pagos.monto, pagos.fecha, clientes.nombre, clientes.apellido from pagos, clientes where pagos.idcliente = clientes.id and fecha = #" + fecha.ToShortDateString() + "#", cnn);
                }
                else
                {
                    if (fecha == new DateTime(1111, 01, 01)) //cliente pero no fecha
                        cmd = new OleDbCommand("select pagos.id, pagos.idcliente, pagos.idventa, pagos.monto, pagos.fecha, clientes.nombre, clientes.apellido from pagos, clientes where pagos.idcliente = clientes.id and idcliente = " + idCliente, cnn);
                    else //fecha y cliente
                        cmd = new OleDbCommand("select pagos.id, pagos.idcliente, pagos.idventa, pagos.monto, pagos.fecha, clientes.nombre, clientes.apellido from pagos, clientes where pagos.idcliente = clientes.id and idcliente = " + idCliente + " and fecha = #" + fecha.ToShortDateString() + "#", cnn);
                }

                OleDbDataReader miLector = cmd.ExecuteReader();

                while (miLector.Read())
                {
                    pagoParaMostrar unPago = new pagoParaMostrar();
                    unPago.id = miLector.GetInt32(0);
                    unPago.idCliente = miLector.GetInt32(1);
                    unPago.idVenta = miLector.GetInt32(2);
                    unPago.monto = miLector.GetDouble(3);
                    unPago.fecha = miLector.GetDateTime(4).ToShortDateString();
                    unPago.nombreCliente = miLector.GetString(5) + " " + miLector.GetString(6);
                    if (unPago.idVenta != 0)
                        unPago.descripciónVenta = "Al ingresar venta";
                    else
                        unPago.descripciónVenta = "Pago sin venta";
                    misPagos.Add(unPago);
                }

                miLector.Close();
                return misPagos;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                cnn.Close();
                return misPagos;
            }
        }

        public List<pago> recuperarTodosLosPagos(int idCliente, DateTime fecha)
        {
            List<pago> misPagos = new List<pago>();
            OleDbConnection cnn = new OleDbConnection(_rutaBD);

            try
            {
                cnn.Open();
                OleDbCommand cmd;

                if (idCliente == 0) //si no es por cliente, o sea que es por fecha
                {
                    cmd = new OleDbCommand("select id, idcliente, idventa, monto, fecha from pagos where fecha = #" + fecha.ToShortDateString() + "#", cnn);
                }
                else
                {
                    cmd = new OleDbCommand("select id, idcliente, idventa, monto, fecha from pagos where idcliente = " + idCliente, cnn);
                }

                OleDbDataReader miLector = cmd.ExecuteReader();

                while (miLector.Read())
                {
                    pago unPago = new pago();
                    unPago.Id = miLector.GetInt32(0);
                    unPago.IdCliente = miLector.GetInt32(1);
                    unPago.IdVenta = miLector.GetInt32(2);
                    unPago.Monto = miLector.GetDouble(3);
                    unPago.Fecha = miLector.GetDateTime(4).ToShortDateString();
                    misPagos.Add(unPago);
                }

                miLector.Close();
                return misPagos;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                cnn.Close();
                return misPagos;
            }
    }

        public List<artículo> recuperarTodosLosArtículos()
        {
            List<artículo> misArtículos = new List<artículo>();// = new List<artículo>;
            OleDbConnection cnn = new OleDbConnection(_rutaBD);

            try
            {
                cnn.Open();
                OleDbCommand cmd = new OleDbCommand("Select id, marca, producto, capacidad from Artículos", cnn);
                OleDbDataReader miLector = cmd.ExecuteReader();

                
                while (miLector.Read()) 
                {
                    artículo miArtículo = new artículo();
                    miArtículo.id = miLector.GetInt32(0);
                    miArtículo.Marca = miLector.GetString(1);
                    miArtículo.Producto = miLector.GetString(2);
                    miArtículo.Capacidad = miLector.GetDouble(3);
                    int ingresos = this.recuperarSumaIngresosStock(miArtículo.id);
                    int salidas = this.recuperarSumaSalidasStock(miArtículo.id);
                    miArtículo.Stock = ingresos- salidas;

                    misArtículos.Add(miArtículo);
                }

                miLector.Close();
                cnn.Close();

                return misArtículos;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return misArtículos;
            }
        }

        public List<cliente> recuperarTodosLosClientes()
        {
            List<cliente> misClientes = new List<cliente>();
            OleDbConnection cnn = new OleDbConnection(_rutaBD);

            try
            {
                cnn.Open();

                OleDbCommand cmd = new OleDbCommand("Select id, nombre, apellido, dirección, localidad, peluquería, fechaNacimiento from Clientes", cnn);
                OleDbDataReader miLector = cmd.ExecuteReader();

                //el comando y el lector para los teléfonos y los mails
                OleDbCommand cmd2 = new OleDbCommand();
                cmd2.Connection = cnn;

                while (miLector.Read())
                {
                    cliente miCliente = new cliente();
                    miCliente.Id = miLector.GetInt32(0);
                    miCliente.Nombre = miLector.GetString(1);
                    miCliente.Apellido = miLector.GetString(2);
                    miCliente.Dirección = miLector.GetString(3);
                    miCliente.Localidad = miLector.GetString(4);
                    miCliente.Peluquería = miLector.GetString(5);
                    miCliente.fecha_nacimiento = miLector.GetString(6);
                    miCliente.Teléfonos = recuperarTeléfonos(miCliente.Id);
                    miCliente.Emails = recuperarMails(miCliente.Id);

                    misClientes.Add(miCliente);
                }

                miLector.Close();
                cnn.Close();

                return misClientes;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return misClientes;
            }
        }

        public cliente recuperarCliente(int idCliente)
        {
            cliente miCliente = new cliente();
            OleDbConnection cnn = new OleDbConnection(_rutaBD);

            try
            {
                cnn.Open();

                OleDbCommand cmd = new OleDbCommand("Select id, nombre, apellido, dirección, localidad, peluquería from Artículos where id = " + idCliente.ToString() + "", cnn);
                OleDbDataReader miLector = cmd.ExecuteReader();

                miLector.Read();
                //while (miLector.Read()) 
                //{
                miCliente.Id = miLector.GetInt32(0);
                miCliente.Nombre = miLector.GetString(1);
                miCliente.Apellido = miLector.GetString(2);
                miCliente.Dirección = miLector.GetString(3);
                miCliente.Localidad = miLector.GetString(4);
                miCliente.Peluquería = miLector.GetString(5);
                miCliente.Teléfonos = recuperarTeléfonos(miCliente.Id);
                miCliente.Emails = recuperarMails(miCliente.Id);
                //}

                

                miLector.Close();
                cnn.Close();

                return miCliente;
            }
            catch
            {
                return miCliente;
            }
        }

        public List<string> recuperarTodosLosNombresDeClientes()
        {
            List<string> misClientes = new List<string>();
            OleDbConnection cnn = new OleDbConnection(_rutaBD);

            try
            {
                cnn.Open();

                OleDbCommand cmd = new OleDbCommand("Select nombre, apellido from Clientes", cnn);
                OleDbDataReader miLector = cmd.ExecuteReader();
                
                while (miLector.Read())
                {
                    
                    string cadena = miLector.GetString(0);
                    cadena += " " + miLector.GetString(1);
                    misClientes.Add(cadena);
                }

                miLector.Close();
                cnn.Close();

                return misClientes;
            }
            catch
            {
                return misClientes;
            }
        }

        public List<string> recuperarTodosLosIDDeClientes()
        {
            List<string> misIDClientes = new List<string>();
            OleDbConnection cnn = new OleDbConnection(_rutaBD);

            try
            {
                cnn.Open();

                OleDbCommand cmd = new OleDbCommand("Select id from Clientes", cnn);
                OleDbDataReader miLector = cmd.ExecuteReader();

                while (miLector.Read())
                {
                    misIDClientes.Add(miLector.GetInt32(0).ToString());
                }

                miLector.Close();
                cnn.Close();

                return misIDClientes;
            }
            catch
            {
                return misIDClientes;
            }
        }

        public string recuperarNombreArtículo(int idArtículo)
        {
            string miArtículo="";
            OleDbConnection cnn = new OleDbConnection(_rutaBD);

            try
            {
                cnn.Open();
                OleDbCommand cmd = new OleDbCommand("Select marca, producto, capacidad from Artículos where id = " + idArtículo.ToString() + "", cnn);
                OleDbDataReader miLector = cmd.ExecuteReader();

                miLector.Read();
                
                miArtículo = miLector.GetString(0);
                miArtículo += ", " + miLector.GetString(1);
                if (miLector.GetDouble(2) != 0)
                    miArtículo += " x " + miLector.GetDouble(2);

                miLector.Close();
                cnn.Close();

                return miArtículo;
            }
            catch
            {
                return miArtículo;
            }
        }

        public List<string> recuperarTodosLosNombresArtículos()
        {
            List<string> misArtículos = new List<string>();
            OleDbConnection cnn = new OleDbConnection(_rutaBD);

            try
            {
                cnn.Open();
                OleDbCommand cmd = new OleDbCommand("Select marca, producto, capacidad from Artículos", cnn);
                OleDbDataReader miLector = cmd.ExecuteReader();

                string cadena;
                while(miLector.Read())
                {
                    cadena = miLector.GetString(0);
                    cadena += ", " + miLector.GetString(1);
                    if (miLector.GetDouble(2) != 0) cadena += " x " + miLector.GetDouble(2); //si tiene capacidad
                    misArtículos.Add(cadena);
                }
                miLector.Close();
                cnn.Close();

                return misArtículos;
            }
            catch
            {
                return misArtículos;
            }
        }

        public List<string> recuperarTodosLos_ID_Artículos()
        {
            List<string> misArtículos = new List<string>();
            OleDbConnection cnn = new OleDbConnection(_rutaBD);

            try
            {
                cnn.Open();
                OleDbCommand cmd = new OleDbCommand("Select id from Artículos", cnn);
                OleDbDataReader miLector = cmd.ExecuteReader();

                while (miLector.Read())
                {
                    misArtículos.Add (miLector.GetInt32(0).ToString());
                }
                miLector.Close();
                cnn.Close();

                return misArtículos;
            }
            catch
            {
                return misArtículos;
            }
        }

        public int recuperarIDArtículoPorNombre(string nombreArtículo)
        {
            string marca = "", producto = "", capacidad = "";
            char[] caracterEspacio = { ',' };
            string cadenaInicial, cadenaFinal;
            //se chequea si el artículo tiene capacidad
            if (nombreArtículo.IndexOf(" x ") != -1)
            {
                cadenaInicial = nombreArtículo.Substring(0, nombreArtículo.IndexOf(" x "));
                cadenaFinal = nombreArtículo.Replace(cadenaInicial, "");
            }
            else
            {
                cadenaInicial = nombreArtículo;
                cadenaFinal = "0";
            }
            
            capacidad = cadenaFinal.Replace(",", ".");
            capacidad = capacidad.Replace("x ", "");
            capacidad = capacidad.Trim();
            //capacidad = capacidad.Replace(",", ".");
            string[] cadena2 = cadenaInicial.Split(caracterEspacio);
            marca = cadena2[0].Trim();
            producto = cadena2[1].Trim();

            if (!string.IsNullOrEmpty(capacidad))                
                return this.recuperarIDArtículo(marca, producto, capacidad);
           
            return 0;
        }

        private int recuperarIDArtículo(string marca, string producto, string capacidad)
        {
            int miID = 0;
            OleDbConnection cnn = new OleDbConnection(_rutaBD);

            try
            {
                cnn.Open();
                OleDbCommand cmd = new OleDbCommand("Select id from Artículos where marca = '" + marca + "' and producto = '" + producto + "' and capacidad = " + capacidad, cnn);
                OleDbDataReader miLector = cmd.ExecuteReader();

                miLector.Read();
                miID = miLector.GetInt32(0);

                miLector.Close();
                cnn.Close();

                return miID;
            }
            catch
            {
                return miID;
            }
        }

        public List<registroVentas> recuperarRegistrosVentas(DateTime fechaInicio, DateTime fechaFin, int idCliente, int idVenta)
        {
            List<registroVentas> misVentas = new List<registroVentas>();
            OleDbConnection cnn = new OleDbConnection(_rutaBD);

            try
            {
                cnn.Open();
                OleDbCommand cmd;

                //se recuperan los datos iniciales de la venta según las fechas dadas
                if (idCliente == 0) //no se tiene en cuenta el cliente
                {
                    if (idVenta == 0) //ni cliente ni venta
                    {
                        cmd = new OleDbCommand("select ventas.id, clientes.nombre, clientes.apellido,  pagos.monto, ventas.fecha, ventas.hora from clientes, ventas, pagos where clientes.id = ventas.idcliente and pagos.idventa = ventas.id and ventas.fecha >= #" + fechaInicio.Year + "/" + fechaInicio.Month + "/" + fechaInicio.Day + "# and ventas.fecha <= #" + fechaFin.Year + "/" + fechaFin.Month + "/" + fechaFin.Day + "#", cnn);
                    }
                    else //no cliente, sí venta
                    {
                        cmd = new OleDbCommand("select ventas.id, clientes.nombre, clientes.apellido,  pagos.monto, ventas.fecha, ventas.hora from clientes, ventas, pagos where clientes.id = ventas.idcliente and pagos.idventa = ventas.id and ventas.fecha >= #" + fechaInicio.Year + "/" + fechaInicio.Month + "/" + fechaInicio.Day + "# and ventas.fecha <= #" + fechaFin.Year + "/" + fechaFin.Month + "/" + fechaFin.Day + "# and ventas.id = " + idVenta, cnn);
                    }
                }
                else
                {
                    if (idVenta == 0) //sí cliente, no venta
                    {
                        cmd = new OleDbCommand("select ventas.id, clientes.nombre, clientes.apellido,  pagos.monto, ventas.fecha, ventas.hora from clientes, ventas, pagos where clientes.id = ventas.idcliente and pagos.idventa = ventas.id and ventas.fecha >= #" + fechaInicio.Year + "/" + fechaInicio.Month + "/" + fechaInicio.Day + "# and ventas.fecha <= #" + fechaFin.Year + "/" + fechaFin.Month + "/" + fechaFin.Day + "# and clientes.id = " + idCliente, cnn);
                    }
                    else //sí cliente y sí venta
                    {
                        cmd = new OleDbCommand("select ventas.id, clientes.nombre, clientes.apellido,  pagos.monto, ventas.fecha, ventas.hora from clientes, ventas, pagos where clientes.id = ventas.idcliente and pagos.idventa = ventas.id and ventas.fecha >= #" + fechaInicio.Year + "/" + fechaInicio.Month + "/" + fechaInicio.Day + "# and ventas.fecha <= #" + fechaFin.Year + "/" + fechaFin.Month + "/" + fechaFin.Day + "# and clientes.id = " + idCliente + " and ventas.id = " + idVenta, cnn);
                    }
                }

                OleDbDataReader miLector = cmd.ExecuteReader();

                while (miLector.Read())
                {
                    registroVentas unaVenta = new registroVentas();
                    unaVenta.Id_Venta = miLector.GetInt32(0);
                    unaVenta.Cliente = miLector.GetString(1) + " " + miLector.GetString(2);
                    unaVenta.Pago=miLector.GetDouble(3);
                    unaVenta.Fecha = miLector.GetDateTime(4).ToShortDateString();
                    unaVenta.Hora = miLector.GetDateTime(5).ToShortTimeString();
                    misVentas.Add(unaVenta);
                }

                miLector.Close();
                
                //se recuperan los datos de los artículos incluídos en cada compra y se llena el balance de cada una de ellas

                double debe = 0;
                for (int i = 0; i < misVentas.Count; i++)
                {
                    cmd = new OleDbCommand("select artículos.marca, artículos.producto, artículos.capacidad, listaArtículosVenta.cantidad, listaArtículosVenta.precio from artículos, listaArtículosVenta, ventas where ventas.id = listaartículosventa.idventa and artículos.id = listaartículosventa.idartículo and ventas.id =" + misVentas[i].Id_Venta, cnn);

                    miLector = cmd.ExecuteReader();
                    debe = 0;

                    while (miLector.Read())
                    {
                        productoVenta miProducto = new productoVenta();
                        if (miLector.GetDouble(2) != 0)
                            miProducto.Producto = miLector.GetString(0) + ", " + miLector.GetString(1) + " x " + miLector.GetDouble(2).ToString();
                        else
                            miProducto.Producto = miLector.GetString(0) + ", " + miLector.GetString(1);

                        miProducto.Cantidad = miLector.GetDouble(3);
                        miProducto.Precio = miLector.GetDouble(4);

                        misVentas[i].MisArtículos.Add(miProducto); //se añade el artículo a la venta
                        debe += miProducto.Cantidad * miProducto.Precio;
                    }

                    misVentas[i].Debe = debe;
                    misVentas[i].Haber = misVentas[i].Pago;
                    misVentas[i].Saldo = misVentas[i].Haber - misVentas[i].Debe;

                    miLector.Close();
                }

                cnn.Close();

                return misVentas;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                cnn.Close();
                return misVentas;
            }
        }

        public List<filaParaImprimirVenta> armarListaVentasParaImprimir(DateTime fechaInicio, DateTime fechaFin)
        {
            List<filaParaImprimirVenta> miLista = new List<filaParaImprimirVenta>();
            List<registroVentas> misVentas = recuperarRegistrosVentas(fechaInicio, fechaFin, 0, 0); //0 es de todos los clientes
            List<string> nombresSinRepetir = new List<string>();

            bool swYaEstá;
            foreach (registroVentas unReg in misVentas) //se buscan los nombres de los clientes sin repetir
            {
                swYaEstá = false;
                if (nombresSinRepetir.Contains(unReg.Cliente)) swYaEstá = true;
                if (swYaEstá==false) nombresSinRepetir.Add(unReg.Cliente);
            }

            int sumador = 1;
            double debe=0, haber=0, saldo =0;
            foreach (string nombre in nombresSinRepetir)
            {
                filaParaImprimirVenta fila = new filaParaImprimirVenta();
                fila.Cliente = nombre;
                debe = 0;
                haber = 0;
                saldo = 0;
                foreach (registroVentas regVenta in misVentas)
                {
                    if (regVenta.Cliente == nombre)
                    {
                        debe += regVenta.Debe;
                        foreach(productoVenta prod in regVenta.MisArtículos)
                            fila.Detalle_compra += prod.Cantidad + " " + prod.Producto + " $" + prod.Precio + "\n";
                        fila.código_Ventas.Add(regVenta.Id_Venta);
                        haber += regVenta.Haber;
                        //saldo += regVenta.Saldo;
                    }
                }
                saldo = haber - debe;
                fila.Debe += debe.ToString();
                //fila.Detalle_compra += regVenta.Producto;
                fila.Haber += haber.ToString();
                fila.Saldo += saldo.ToString();
                fila.Nº = sumador;
                sumador++;
                miLista.Add(fila);
            }

            return miLista;
        }

        public List<int> recuperarValoresVentas(int idArtículo, int idCliente, DateTime fechaInicio, DateTime fechaFin)
        {
            List<int> valores = new List<int>();
            List<valorVenta> valoresVenta = new List<valorVenta>();

            try
            {
                List<registroVentas> ventas = recuperarRegistrosVentas(fechaInicio, fechaFin, idCliente, 0);
                List<registroVentas> ventasYaIngresadas = new List<registroVentas>();

                //se juntan todos los artículos por fecha y cliente
                #region juntar artículos por fecha y cliente
                foreach (registroVentas unaVenta in ventas) //por cada venta
                {
                    if (!ventasYaIngresadas.Contains(unaVenta))
                    {
                        valorVenta unValor = new valorVenta(); //se toman los datos de la venta
                        unValor.fecha = unaVenta.Fecha;
                        foreach (productoVenta prod in unaVenta.MisArtículos)
                            unValor.productos.Add(prod);

                        foreach (registroVentas otraVenta in ventas) //y se compara con cada una de las otras ventas
                        {
                            if (!ventasYaIngresadas.Contains(otraVenta) && unaVenta != otraVenta)
                            {
                                if (unaVenta.Fecha == otraVenta.Fecha) //si se repite la fecha se suman los productos
                                {
                                    //if (idCliente == 0) //si no hay cliente
                                    //{
                                        foreach (productoVenta prod in otraVenta.MisArtículos)
                                        {
                                            unValor.productos.Add(prod);
                                        }
                                        //unValor.nombreCliente = "Sin cliente";
                                        ventasYaIngresadas.Add(otraVenta);
                                        //valoresVenta.Add(unValor);
                                    //}
                                    //else //si hay cliente
                                    //{
                                    //    if (recuperar_IDcliente(unaVenta.Cliente) == idCliente)
                                    //    {
                                    //        foreach (productoVenta prod in otraVenta.MisArtículos)
                                    //        {
                                    //            unValor.productos.Add(prod);
                                    //        }
                                    //        unValor.nombreCliente = unaVenta.Cliente;
                                    //        ventasYaIngresadas.Add(otraVenta);
                                    //        valoresVenta.Add(unValor);
                                    //    }
                                    //}
                                }

                            }
                        }
                        if (idCliente == 0) //sino hay ciente
                        {
                            unValor.nombreCliente = "Sin cliente";
                            valoresVenta.Add(unValor);
                        }
                        else
                        {
                            if (idCliente == recuperar_IDcliente(unaVenta.Cliente))
                            {
                                unValor.nombreCliente = unaVenta.Cliente;
                                valoresVenta.Add(unValor);
                            }
                        }
                     }
                }
                
                #endregion
 
                #region se suman los valores de las ventas juntadas

                TimeSpan ts = fechaFin - fechaInicio; //se calcula la diferencia en días entre las dos fechas
                int días = ts.Days + 1; //se suma 1 porque la fecha de inicio tiene que estar incluída

                List<valorDíaVenta> valoresSegúnFechas = new List<valorDíaVenta>();

                foreach (valorVenta unValor in valoresVenta) //se toman la cantidad de un determinado producto de las fechas que tienen ventas
                {
                    valorDíaVenta unDía = new valorDíaVenta();
                    double sumador = 0;
                    unDía.fecha = unValor.fecha;
                    foreach (productoVenta prod in unValor.productos)
                        if (idArtículo == artículo.recuperarIDporNombre(prod.Producto)) sumador += prod.Cantidad;
                    unDía.valorDelDía=(int)sumador;
                    valoresSegúnFechas.Add(unDía);
                }

                for (int i = 0; i < días; i++)
                {
                    //bool swDíaConVenta=false;
                    valorDíaVenta valorDíario = new valorDíaVenta(); ;
                    foreach (valorDíaVenta unDía in valoresSegúnFechas)
                    {
                        if (unDía.fecha == fechaInicio.AddDays(i).ToShortDateString())
                        {
                            valorDíario=unDía;
                            //swDíaConVenta = true;
                            break;
                        }
                        
                    }
                    //if (swDíaConVenta)
                        valores.Add(valorDíario.valorDelDía);
                }


                #endregion

                return valores;


                //foreach (productoVenta producto in unaVenta.MisArtículos)
                //{
                //    if (idCliente == 0) //si no hay cliente
                //    {

                //    }
                //    else //si hay cliente
                //    {

                //    }
                //}
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return valores;
            }
                
        }

        internal class valorVenta
        {
            public string nombreCliente { get; set; }
            public string fecha { get; set; }
            public List<productoVenta> productos { get; set; }
            public valorVenta() { productos = new List<productoVenta>(); }
        }

        internal class valorDíaVenta
        {
            public string fecha { get; set; }
            public int valorDelDía { get; set; }
            public valorDíaVenta() { valorDelDía = 0; }
        }

        public bool guardarUsuario(string nombre, string pass, int nivelSeguridad, string mail)
        {

            OleDbConnection cnn = new OleDbConnection(_rutaBD);

            try
            {
                cnn.Open();

                string comandoInsert = "'" + nombre + "', '" + pass + "', " + nivelSeguridad + ", '" + mail + "'";

                OleDbCommand cmd = new OleDbCommand("INSERT INTO Usuarios (Nombre, Pass, Nivel_Seguridad, Email) VALUES (" + comandoInsert + ")", cnn);

                cmd.ExecuteNonQuery();

                cnn.Close();

                return true;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }

        }

        public bool comprobarUsuarioExiste(string nombre)
        {
            OleDbConnection cnn = new OleDbConnection(_rutaBD);

            try
            {
                cnn.Open();
                OleDbCommand cmd = new OleDbCommand("Select id from Usuarios where nombre = '" + nombre + "'", cnn);
                OleDbDataReader miLector = cmd.ExecuteReader();

                bool swUsuarioExiste;
                if (miLector.Read())
                    swUsuarioExiste = true;
                else
                    swUsuarioExiste = false;
                
                miLector.Close();
                cnn.Close();

                return swUsuarioExiste;
            }
            catch
            {
                cnn.Close();
                return false;
            }
        }

        public string recuperarPassUsuario(string nombre)
        {
            OleDbConnection cnn = new OleDbConnection(_rutaBD);

            try
            {
                cnn.Open();
                OleDbCommand cmd = new OleDbCommand("Select pass from Usuarios where nombre = '" + nombre + "'", cnn);
                OleDbDataReader miLector = cmd.ExecuteReader();

                miLector.Read();

                string passUsuario = miLector.GetString(0);

                miLector.Close();
                cnn.Close();

                return passUsuario;
            }
            catch (Exception ex)
            {
                cnn.Close();
                return ex.Message;
            }
        }

        public int recuperarNivelSeguridadUsuario(string nombre)
        {
            OleDbConnection cnn = new OleDbConnection(_rutaBD);

            try
            {
                cnn.Open();
                OleDbCommand cmd = new OleDbCommand("Select nivel_seguridad from Usuarios where nombre = '" + nombre + "'", cnn);
                OleDbDataReader miLector = cmd.ExecuteReader();

                miLector.Read();

                int nivel = miLector.GetInt32(0);

                miLector.Close();
                cnn.Close();

                return nivel;
            }
            catch
            {
                cnn.Close();
                return 1000; //cuanto más alto es el número, menos privilegios. Cero es el admin
            }
        }

        public List<usuario> recuperarTodosLosUsuarios()
        {
            OleDbConnection cnn = new OleDbConnection(_rutaBD);
            List<usuario> usuarios = new List<usuario>();

            try
            {
                cnn.Open();
                OleDbCommand cmd = new OleDbCommand("Select nombre, pass, nivel_seguridad, email from Usuarios", cnn);
                OleDbDataReader miLector = cmd.ExecuteReader();

                while (miLector.Read())
                {
                    string nombre = miLector.GetString(0);
                    string pass = miLector.GetString(1);
                    int nivel = miLector.GetInt32(2);
                    string email = miLector.GetString(3);
                    usuario miUsuario = new usuario(nombre, pass, nivel, email);
                    usuarios.Add(miUsuario);
                }
                miLector.Close();
                cnn.Close();

                return usuarios;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                cnn.Close();
                return usuarios;
            }
        }

       

        public bool eliminarUsuario (string nombre)
        {
            OleDbConnection cnn = new OleDbConnection(_rutaBD);

            try
            {
                cnn.Open();
                OleDbCommand cmd = new OleDbCommand("Delete from Usuarios where nombre = '" + nombre + "'", cnn);
                cmd.ExecuteNonQuery();

                cnn.Close();

                return true;
            }
            catch
            {
                cnn.Close();
                return false; 
            }
        }

        public bool cambiarPassUsuario(string nombre, string pass)
        {
            OleDbConnection cnn = new OleDbConnection(_rutaBD);

            try
            {
                cnn.Open();
                OleDbCommand cmd = new OleDbCommand("Update Usuarios SET pass = '" + pass + "' where nombre = '" + nombre + "'", cnn);
                cmd.ExecuteNonQuery();

                cnn.Close();

                return true;
            }
            catch
            {
                cnn.Close();
                return false;
            }
        }

        public bool cambiarNivelUsuario(string nombre, int nivelNuevo)
        {
            OleDbConnection cnn = new OleDbConnection(_rutaBD);

            try
            {
                cnn.Open();
                OleDbCommand cmd = new OleDbCommand("Update Usuarios SET nivel_seguridad = " + nivelNuevo + " where nombre = '" + nombre + "'", cnn);
                cmd.ExecuteNonQuery();

                cnn.Close();

                return true;
            }
            catch
            {
                cnn.Close();
                return false;
            }
        }

        public string recuperarMailUsuario(string nombre)
        {
            OleDbConnection cnn = new OleDbConnection(_rutaBD);

            try
            {
                cnn.Open();
                OleDbCommand cmd = new OleDbCommand("Select email from Usuarios where nombre = '" + nombre + "'", cnn);
                OleDbDataReader miLector = cmd.ExecuteReader();

                miLector.Read();

                string mailUsuario = miLector.GetString(0);

                miLector.Close();
                cnn.Close();

                return mailUsuario;
            }
            catch (Exception ex)
            {
                cnn.Close();
                return ex.Message;
            }
        }

        public bool cambiarMailUsuario(string nombre, string nuevoMail)
        {
            OleDbConnection cnn = new OleDbConnection(_rutaBD);

            try
            {
                cnn.Open();
                OleDbCommand cmd = new OleDbCommand("Update Usuarios SET email = '" + nuevoMail + "' where nombre = '" + nombre + "'", cnn);
                cmd.ExecuteNonQuery();

                cnn.Close();

                return true;
            }
            catch
            {
                cnn.Close();
                return false;
            }
        }

        //corregir
        //public bool respaldarBD(string rutaDondeRespaldar)
        //{
            //FileInfo[] MisFiles;
            //DirectoryInfo[] MisDir;
            //int ContadorDir = 0;
            //MisFiles = new DirectoryInfo(Origen).GetFiles();
            //MisDir = new DirectoryInfo(Origen).GetDirectories();
            //ContadorDir = MisDir.GetLength(0);
            //foreach (FileInfo Archivo in MisFiles)
            //{
            //try
            //{
            //Archivo.CopyTo(Destino + "\\" + Archivo.Name);
            //}
            //catch (Exception e)
            //{
            //MiFiFo.Enqueue("FAILURE: " + Destino + "\\" + Archivo.Name + "\r" + e.Message + "\r");
            //}
            //}
            //for (int i = 0; i &lt; ContadorDir; i++)
            //{
            //CrearDirectorio(Destino + "\\" + MisDir[i].Name);
            //MoverArchivos1(Destino + "\\" + MisDir[i].Name, MisDir[i].FullName);
            //}

        //}

    }
}
