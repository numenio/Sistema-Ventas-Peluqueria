using System;
using System.Collections.Generic;
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
    /// Lógica de interacción para LogIn.xaml
    /// </summary>
    public partial class LogIn : Window
    {
        int contador = 0;
        int cantidadIngresosPermitidos = 3;
        bool swCerrarPrograma = true;

        public LogIn()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, RoutedEventArgs e)
        {
            comprobarIngreso();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (swCerrarPrograma)
            {
                Application actual = App.Current;
                actual.Shutdown();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtNombre.Focus();
        }

        private void comprobarIngreso()
        {
            if (usuario.comprobarPass(txtNombre.Text.Trim(), passBoxContraseña.Password.Trim()))
            {
                Principal ventana = new Principal();
                string nombre = txtNombre.Text.Trim();
                usuario unUsuario = new usuario(nombre, passBoxContraseña.Password.Trim(), usuario.recuperarNivelSeguridad(nombre), usuario.recuperarMail(nombre));
                //ventana.usuarioActual = unUsuario;
                usuario.usuarioActual = unUsuario;
                ventana.Show();
                swCerrarPrograma = false;
                this.Close();
            }
            else
            {
                contador++;
                if (contador != cantidadIngresosPermitidos)
                {
                    if (cantidadIngresosPermitidos-contador == 2) MessageBox.Show("El Usuario o la Contraseña son incorrectos, le quedan " + (cantidadIngresosPermitidos - contador) + " intentos.", "Error de acceso");
                    if (cantidadIngresosPermitidos - contador == 1) MessageBox.Show("Sólo queda " + (cantidadIngresosPermitidos - contador) + " intento. Si se equivoca nuevamente se le va a enviar un E-mail al propietario avisándole de este intento de acceso.", "Error de acceso");
                }
                else
                {
                    MessageBox.Show("Han terminado los " + cantidadIngresosPermitidos + " intentos permitidos. Este programa se va a cerrar y se va a enviar un E-mail al propietario avisándole de este intento de ACCESO NO AUTORIZADO.", "Alarma de intruso");
                    //se envía un mail avisando de los intentos
                    enviarMail.enviar(usuario.recuperarMail("Administrador"), "guillermo.toscani@googlemail.com", "Alerta de seguridad peluquería", "El día " + DateTime.Now.ToLongDateString() + " a las " + DateTime.Now.ToShortTimeString() + " horas, alguien acabó los " + cantidadIngresosPermitidos + " intentos permitidos para acceder al Sistema de Ventas de Peluquería que está en la máquina " + Environment.MachineName + " del propietario " + Environment.UserName, "smtp.googlemail.com", 587, "guillermo.toscani@googlemail.com", "26142615");

                    Application current = App.Current;
                    current.Shutdown();
                }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return) comprobarIngreso();
        }

        private void lblOlvido_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Si el usuario \"" + txtNombre.Text.Trim() + "\" existe, se le ha enviado a su cuenta de e-mail un mensaje con la contraseña correcta.", "Envío de contraseña");

            if (usuario.usuarioExiste(txtNombre.Text.Trim()))
            {
                usuario miUsuario = new usuario(txtNombre.Text.Trim());

                enviarMail.enviar(usuario.recuperarMail(miUsuario.nombre), "guillermo.toscani@googlemail.com", "Contraseña Sistema Peluquería", "La contraseña del usuario \"" + miUsuario.nombre + "\" para ingresar al Sistema de Ventas de Peluquería que está en la máquina " + Environment.MachineName + " del propietario " + Environment.UserName + " es \"" + miUsuario.pass + "\"", "smtp.googlemail.com", 587, "guillermo.toscani@googlemail.com", "26142615");
            }
        }
    }
}
