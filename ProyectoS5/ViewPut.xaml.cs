using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoS5
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewPut : ContentPage
    {

        public ViewPut(int Id, string Nom, string Apellido, int Edad)
        {
            InitializeComponent();
            txtcodigo.Text = Id.ToString();
            txtnombre.Text = Nom;
            txtapellido.Text = Apellido;
            txtedad.Text = Edad.ToString();
        }

        private async void btnActualizar_Clicked(object sender, EventArgs e)
        {
            try
            {
                WebClient cliente = new WebClient();
                var parametros = new System.Collections.Specialized.NameValueCollection();

                cliente.UploadValues("http://192.168.0.10/moviles/post.php?codigo=" + Int32.Parse(txtcodigo.Text)
                    + "&" + "nombre=" + txtnombre.Text + "&" + "apellido=" + txtapellido.Text + "&"
                    + "edad=" + Int32.Parse(txtedad.Text), "PUT", parametros);

                await DisplayAlert("Notificación", "Usuario actualizado correctamente...", "OK");

                await Navigation.PushAsync(new MainPage());

            }
            catch (Exception ex)
            {
                await DisplayAlert("Alerta", "Error: " + ex.Message, "OK");
            }

        }

        private void btnRegresar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var mensaje = await this.DisplayAlert("Alerta!", "Seguro desea eliminar el usuario?", "SI", "No");
                if (mensaje)
                {
                    WebClient cliente = new WebClient();
                    var parametros = new System.Collections.Specialized.NameValueCollection();

                    cliente.UploadValues("http://192.168.0.10/moviles/post.php?codigo=" + Int32.Parse(txtcodigo.Text), "DELETE", parametros);

                    await DisplayAlert("Notificación", "Usuario eliminado correctamente...", "OK");

                    await Navigation.PushAsync(new MainPage());
                }
                else
                {
                    await DisplayAlert("Notificación", "El usuario no fue eliminado...", "OK");
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Alerta", "Error: " + ex.Message, "OK");
            }
        }
    }
}