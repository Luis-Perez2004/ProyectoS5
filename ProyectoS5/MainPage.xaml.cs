using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoS5
{
    public partial class MainPage : ContentPage
    {
        private const string Url = "http://192.168.0.10/moviles/post.php";
        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<ProyectoS5.Ws.Datos> _post;
        int ID, EDAD;
        string Nom, Ape;

        public MainPage()
        {
            InitializeComponent();
            Consultar();
        }

        private async void Consultar()
        {
            var content = await client.GetStringAsync(Url);
            List<ProyectoS5.Ws.Datos> posts = JsonConvert.DeserializeObject<List<ProyectoS5.Ws.Datos>>(content);
            _post = new ObservableCollection<Ws.Datos>(posts);

            MyListView.ItemsSource = _post;
        }

        private void btnPost_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewPost());
        }

        private void MyListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var Obj = (Ws.Datos)e.SelectedItem;
            var item = Obj.codigo.ToString();
            ID = Convert.ToInt32(item);
            Nom = Obj.nombre;
            Ape = Obj.apellido;
            EDAD = Convert.ToInt32(Obj.edad.ToString());
            try
            {
                Navigation.PushAsync(new ViewPut(ID,Nom,Ape,EDAD));
            }
            catch(Exception ex)
            {
                DisplayAlert("Alerta", "Error: " + ex.Message, "OK");
            }
        }
    }
}
