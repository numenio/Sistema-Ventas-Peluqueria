using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProcesosNegocio
{
    public class usuario
    {
        public string nombre { get; set; }
        public string pass { get; set; }
        public int nivel_seguridad { get; set; }
        public string mail { get; set; }
        public static usuario usuarioActual { get; set; }

        //public usuario()
        //{
        //}

        public usuario (string _nombre) //para cargar desde BD el pass y el nivel 
        {
            AdminData ad = new AdminData();
            nombre = _nombre;
            pass= ad.recuperarPassUsuario(nombre);
            nivel_seguridad = ad.recuperarNivelSeguridadUsuario(nombre);
            mail = ad.recuperarMailUsuario(nombre);
        }

        public usuario(string _nombre, string _pass, int _nivel_seguridad, string _mail) //para guardar
        {
            nombre = _nombre;
            pass = _pass;
            nivel_seguridad = _nivel_seguridad;
            mail = _mail;
        }

        public static bool usuarioExiste(string nombre)
        {
            AdminData ad = new AdminData();
            return ad.comprobarUsuarioExiste(nombre);
        }

        public bool guardar()
        {
            if (string.IsNullOrEmpty(nombre)) return false;
            if (string.IsNullOrEmpty(pass)) return false;
            if (nivel_seguridad==null) return false;
            if (string.IsNullOrEmpty(mail)) return false;

            bool swDevolución;
            //if (nivel_seguridad == 0) //si es un administrador
            //{
                AdminData ad = new AdminData();
                if (!ad.comprobarUsuarioExiste(nombre)) //si no hay ya un usuario con ese nombre
                    swDevolución = ad.guardarUsuario(nombre, pass, nivel_seguridad, mail); //se lo guarda
                else
                    swDevolución = false;
            //}
            //else
            //{
            //    return false; //si no puede guardar porque es un laucha
            //}

            return swDevolución;
        }

        public static bool comprobarPass(string nombre, string pass)
        {
            AdminData ad = new AdminData();
            string passAlmacenado = ad.recuperarPassUsuario(nombre);
            if (passAlmacenado == pass)
                return true;
            else
                return false;
        }

        public static int recuperarNivelSeguridad(string nombre)
        {
            AdminData ad = new AdminData();
            return ad.recuperarNivelSeguridadUsuario(nombre);
        }

        public static string recuperarMail(string nombre)
        {
            AdminData ad = new AdminData();
            return ad.recuperarMailUsuario(nombre);
        }

        public static List<usuario> cargarTodos()
        {
            AdminData ad = new AdminData();
            return ad.recuperarTodosLosUsuarios();
        }

        public bool eliminar()
        {
            if (!usuarioVálido()) return false;

            AdminData ad = new AdminData();
            return ad.eliminarUsuario(nombre); //se lo elimina
        }

        public bool cambiarPass(string passNuevo)
        {
            if (!usuarioVálido()) return false;
            //if (nivel_seguridad != 0) return false; //si no es administrador, no puede 

            AdminData ad = new AdminData();
            return ad.cambiarPassUsuario(nombre, passNuevo);
        }

        public bool cambiarMail(string mailNuevo)
        {
            if (!usuarioVálido()) return false;
            //if (nivel_seguridad != 0) return false; //si no es administrador, no puede 

            AdminData ad = new AdminData();
            return ad.cambiarMailUsuario(nombre, mailNuevo);
        }

        public bool cambiarNivel(int nivelNuevo)
        {
            if (!usuarioVálido()) return false;
            //if (nivel_seguridad != 0) return false; //si no es administrador, no puede 

            AdminData ad = new AdminData();
            return ad.cambiarNivelUsuario(nombre, nivelNuevo);
        }

        private bool usuarioVálido() //chequea que se hayan llenado todos los campos y que el usuario exista
        {
            AdminData ad = new AdminData();
            if (!ad.comprobarUsuarioExiste(nombre)) return false; //si el usuario no existe
            if (string.IsNullOrEmpty(nombre)) return false;
            if (string.IsNullOrEmpty(pass)) return false;
            if (string.IsNullOrEmpty(mail)) return false;
            if (nivel_seguridad == null) return false;
            return true;
        }
    }
}
