using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;

namespace SistemaPeluquería
{
    class AdminData
    {
        //OleDbConnection cnn;
        private static string _rutaBD = @"Provider=Microsoft.Jet.OLEDB.4.0; User ID=Admin; Data Source=peluquería.mdb";
      
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

        //private bool abrirBD(string ruta)
        //{
        //    try
        //    {
        //        cnn = new OleDbConnection(ruta);
        //        cnn.Open();
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        //private bool cerrarBD(OleDbConnection cnn)
        //{
        //    try { 
        //        if (cnn.State == ConnectionState.Open) cnn.Close();
        //        return true;
        //    }
        //    catch { return false; }
        //}

        ////guardar una fecha en el XML si es que ya no está guardada
        //private void guardarFecha(DateTime fecha)
        //{
        //    bool swYaEstá = false;
        //    DataTable data = new DataTable();
        //    data.ReadXml("datos.xml");

        //    foreach (DataRow r in data.Rows)
        //    {
        //        if (string.Format("{0}", r.ItemArray[1]) == fecha.ToString())
        //        {
        //            swYaEstá = true;
        //            break;
        //        }
        //    }
        //    if (swYaEstá == false)
        //    {
        //        DataRow fila = data.NewRow();
        //        fila["fecha"] = fecha;
        //        data.Rows.Add(fila);
        //        data.WriteXml("datos.xml", XmlWriteMode.WriteSchema);
        //    }
        //}

        ////borrar una fecha del XML si es que está marcada
        //private void borrarFechaGuardada(DateTime fecha)
        //{
        //    DataTable data = new DataTable();
        //    data.ReadXml("datos.xml");

        //    //DataRow[] misFilas = data.Select();

        //    foreach (DataRow r in data.Rows)
        //    {
        //        if (string.Format("{0}", r.ItemArray[1]) == fecha.ToString())
        //        {
        //            data.Rows.Remove(r);
        //            break;
        //        }
        //    }

        //    data.WriteXml("datos.xml", XmlWriteMode.WriteSchema);

        //}

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
                string comandoInsert = "'" + miCliente.Nombre + "', '" + miCliente.Apellido + "', '" + miCliente.Dirección + "', '" + miCliente.Localidad + "', '" + miCliente.Peluquería + "'";
                OleDbCommand cmd = new OleDbCommand("INSERT INTO Clientes (marca, apellido, dirección, localidad, peluquería) VALUES (" + comandoInsert + ")", cnn);
                cmd.ExecuteNonQuery();

                //se recupera la id del cliente que se acaba de guardar
                int id = this.recuperar_ID_cliente(miCliente.Nombre, miCliente.Apellido, miCliente.Dirección, miCliente.Localidad, miCliente.Peluquería);

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

        private bool guardarTeléfonos(ArrayList teléfonos, int id)
        {
            OleDbConnection cnn = new OleDbConnection(_rutaBD);

            try
            {
                cnn.Open();

                foreach (string tel in teléfonos)
                {
                    string comandoInsert = "" + id + ", '" + tel + "'";
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

        private bool guardarMails(ArrayList mails, int id)
        {
            OleDbConnection cnn = new OleDbConnection(_rutaBD);

            try
            {
                cnn.Open();

                foreach (string mail in mails)
                {
                    string comandoInsert =  id + ", '" + mail + "'";
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

                string cadenaSelect = "Select id from Clientes where marca = '" + nombre + "' and apellido = '" + apellido + "' and dirección = '" + dirección + "' and localidad = '" + localidad + "' and peluquería = '" + peluquería +"'";
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

        public bool guardarPago(pago miPago)
        {
            OleDbConnection cnn = new OleDbConnection(_rutaBD);

            try
            {
                cnn.Open();

                string comandoInsert = "'" + miPago.IdCliente + "', '" + miPago.Monto + "', '" + miPago.Fecha + "'";

                OleDbCommand cmd = new OleDbCommand("INSERT INTO Clientes (idCliente, monto, fecha) VALUES (" + comandoInsert + ")", cnn);

                cmd.ExecuteNonQuery();

                cnn.Close();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool guardarVenta(venta miVenta)
        {
            OleDbConnection cnn = new OleDbConnection(_rutaBD);

            try
            {
                cnn.Open();

                string comandoInsert = "'" + miVenta.idCliente + "', '" + miVenta.idArtículo + "', '" + miVenta.Cantidad + "', '" + miVenta.Precio + "', '" + miVenta.Fecha + "', '" + miVenta.Hora + "'";

                OleDbCommand cmd = new OleDbCommand("INSERT INTO Clientes (idCliente, monto, fecha) VALUES (" + comandoInsert + ")", cnn);

                cmd.ExecuteNonQuery();

                cnn.Close();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public artículo recuperarArtículo(int idArtículo)
        {
            artículo miArtículo = new artículo();
            OleDbConnection cnn = new OleDbConnection(_rutaBD);

            try
            {
                cnn.Open();

                //quizás tire error el convertir idArtículo, que es un int, a string:
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

                //quizás tire error el convertir idArtículo, que es un int, a string:
                OleDbCommand cmd = new OleDbCommand("Select id from Artículos where marca = " + marca + " and producto = " + producto + " and capacidad = " + capacidad.ToString() + "" , cnn);
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
            catch
            {
                return miArtículo;
            }
        }

        //public artículo recuperarArtículo(string nombreArtículo)
        //{
        //    artículo miArtículo = new artículo();
        //    OleDbConnection cnn = new OleDbConnection(_rutaBD);

        //    try
        //    {
        //        cnn.Open();

        //        //quizás tire error el convertir idArtículo, que es un int, a string:
        //        OleDbCommand cmd = new OleDbCommand("Select id, marca, producto, capacidad from Artículos where marca = " + nombreArtículo + "", cnn);
        //        OleDbDataReader miLector = cmd.ExecuteReader();

        //        miLector.Read();
        //        //while (miLector.Read()) 
        //        //{
        //        miArtículo.id = miLector.GetInt32(0);
        //        miArtículo.Marca = miLector.GetString(1);
        //        miArtículo.Producto = miLector.GetString(2);
        //        miArtículo.Capacidad = miLector.GetDouble(3);
        //        //}

        //        miLector.Close();
        //        cnn.Close();

        //        return miArtículo;
        //    }
        //    catch
        //    {
        //        return miArtículo;
        //    }
        //}

        //public cliente recuperarCliente()
        //{
            
        //}

        public pago recuperarPago()
        {
            throw new System.NotImplementedException();
        }

        public venta recuperarVenta()
        {
            throw new System.NotImplementedException();
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

        public bool eliminarCliente(cliente cliente_a_eliminar)
        {
            OleDbConnection cnn = new OleDbConnection(_rutaBD);
            try
            {
                string cadenaSQL = "Delete from Clientes where Id=" + cliente_a_eliminar.Id;
                OleDbCommand cmd = new OleDbCommand(cadenaSQL, cnn);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool eliminarPago(pago pago_a_eliminar)
        {
            OleDbConnection cnn = new OleDbConnection(_rutaBD);
            try
            {
                string cadenaSQL = "Delete from Pagos where Id=" + pago_a_eliminar.Id;
                OleDbCommand cmd = new OleDbCommand(cadenaSQL, cnn);
                cmd.ExecuteNonQuery();
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
                string cadenaSQL = "Delete from Ventas where Id=" + venta_a_eliminar.id;
                OleDbCommand cmd = new OleDbCommand(cadenaSQL, cnn);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool modificarArtículo(artículo artículo_a_modificar)
        {
            throw new System.NotImplementedException();
        }

        public bool modificarCliente(cliente cliente_a_modificar)
        {
            throw new System.NotImplementedException();
        }

        public bool modificarPago(pago pago_a_modificar)
        {
            throw new System.NotImplementedException();
        }

        public bool modificarVenta(venta venta_a_modificar)
        {
            throw new System.NotImplementedException();
        }

        public List<artículo> recuperarTodosLosArtículos()
        {
            List<artículo> misArtículos = new List<artículo>();// = new List<artículo>;
            OleDbConnection cnn = new OleDbConnection(_rutaBD);

            try
            {
                cnn.Open();

                //quizás tire error el convertir idArtículo, que es un int, a string:
                OleDbCommand cmd = new OleDbCommand("Select id, marca, producto, capacidad from Artículos", cnn);
                OleDbDataReader miLector = cmd.ExecuteReader();

                
                while (miLector.Read()) 
                {
                    artículo miArtículo = new artículo();
                    miArtículo.id = miLector.GetInt32(0);
                    miArtículo.Marca = miLector.GetString(1);
                    miArtículo.Producto = miLector.GetString(2);
                    miArtículo.Capacidad = miLector.GetDouble(3);

                    misArtículos.Add(miArtículo);
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

        public List<cliente> recuperarTodosLosClientes()
        {
            List<cliente> misClientes = new List<cliente>();
            OleDbConnection cnn = new OleDbConnection(_rutaBD);

            try
            {
                cnn.Open();

                //quizás tire error el convertir idClientes, que es un int, a string:
                OleDbCommand cmd = new OleDbCommand("Select id, marca, apellido, dirección, localidad, peluquería from Clientes", cnn);
                OleDbDataReader miLector = cmd.ExecuteReader();


                while (miLector.Read())
                {
                    cliente miCliente = new cliente();
                    miCliente.Id = miLector.GetInt32(0);
                    miCliente.Nombre = miLector.GetString(1);
                    miCliente.Apellido = miLector.GetString(2);
                    miCliente.Dirección = miLector.GetString(3);
                    miCliente.Localidad = miLector.GetString(4);
                    miCliente.Peluquería = miLector.GetString(5);

                    misClientes.Add(miCliente);
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

        public cliente recuperarCliente(string nombre, string apellido, string peluquería)
        {
            throw new System.NotImplementedException();
        }
    }
}
