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
    /// Lógica de interacción para AdministrarUsuarios.xaml
    /// </summary>
    public partial class AdministrarUsuarios : Window
    {
        public AdministrarUsuarios()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string cadena = "Limitado (sólo ve y guarda ventas)";
            string cadena2 = "Total (puede usar todo y añadir nuevos elementos)";
            cmbNivel.Items.Add(cadena);
            cmbNivel.Items.Add(cadena2);
            cmbNivel.SelectedIndex = 0;
            cargarUsuarios();
        }

        private void cargarUsuarios()
        {
            List<usuario> misUsuarios = usuario.cargarTodos();
            listUsuarios.Items.Clear();
            foreach (usuario unUsuario in misUsuarios)
            {
                if (unUsuario.nombre != "Administrador")
                {
                    controlContacto tarjeta = new controlContacto();
                    tarjeta.lblMail.Content = unUsuario.mail;
                    if (unUsuario.nivel_seguridad == 0)
                        tarjeta.lblNivel.Content = "Nivel de Acceso: Total";
                    else
                        tarjeta.lblNivel.Content = "Nivel de Acceso: Limitado";
                    tarjeta.lblPass.Content = "Pass: " + unUsuario.pass;
                    tarjeta.lblUsuario.Content = unUsuario.nombre;
                    listUsuarios.Items.Add(tarjeta);
                }
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (comprobarCampos())
            {
                int nivel = cmbNivel.Items.Count - 1 - cmbNivel.SelectedIndex; //en el combo están ordenados alrevés que en los niveles numéricos, cuanto más alto está en el combo, más bajo es su nivel de autorización
                usuario miUsuario = new usuario(txtNombre.Text.Trim(), txtPass.Text.Trim(), nivel, txtMail.Text.Trim());
                if (!miUsuario.guardar())
                    MessageBox.Show("Hubo un problema al guardar el usuario. O usted no tiene el nivel de autorización suficiente, o algo pasó. Contacte con la gente de sistemas", "Error");

                txtMail.Text = "";
                txtNombre.Text = "";
                txtPass.Text = "";
                cmbNivel.SelectedIndex = 0;

                cargarUsuarios();
            }
        }

        private bool comprobarCampos()
        {
            if (string.IsNullOrEmpty(txtNombre.Text.Trim())) return false;
            if (string.IsNullOrEmpty(txtMail.Text.Trim())) return false;
            if (string.IsNullOrEmpty(txtPass.Text.Trim())) return false;
            if (cmbNivel.SelectedIndex == -1) return false;
            return true;
        }

        private void lblEliminar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (listUsuarios.SelectedItem != null)
            {
                controlContacto tarjeta = (controlContacto)listUsuarios.SelectedItem;
                usuario unUsuario = new usuario(tarjeta.lblUsuario.Content.ToString());
                unUsuario.eliminar();
                listUsuarios.Items.Remove(listUsuarios.SelectedItem);
            }
        }
    }
}
