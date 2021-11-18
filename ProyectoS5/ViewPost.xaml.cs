using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoS5
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewPost : ContentPage
    {
        public ViewPost()
        {
            InitializeComponent();
        }

        private async void btnIngresar_Clicked(object sender, EventArgs e)
        {
            try
            {
                WebClient cliente = new WebClient();
                var parametros = new System.Collections.Specialized.NameValueCollection();
                

                parametros.Add("codigo", txtcodigo.Text);
                parametros.Add("nombre", txtnombre.Text);
                parametros.Add("apellido", txtapellido.Text);
                parametros.Add("edad", txtedad.Text);

                cliente.UploadValues("http://192.168.0.10/moviles/post.php", "POST", parametros);

                await DisplayAlert("Notificación", "Usuario ingresado correctamente...", "OK");
                txtcodigo.Text = "";
                txtnombre.Text = "";
                txtapellido.Text = "";
                txtedad.Text = "";
                txtcodigo.Focus();
            }
            catch(Exception ex)
            {
                await DisplayAlert("Alerta", "Error: " + ex.Message, "OK");
            }
        }

        private void btnRegresar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }
    }
}