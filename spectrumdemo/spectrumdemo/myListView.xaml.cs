using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace spectrumdemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class myListView : ContentPage
    {
       

        public myListView()
        {

            ObservableCollection<string> Items;// { get; set; }
            InitializeComponent();

            IsGestureEnabled = false;

            // we could grab a menu through a php web call here and build the menu dynamicly.  


            Items = new ObservableCollection<string>
            {
                "Access The Spectrum Website",
                "Demo of Web Javascript",
                "Payments",
                "Logout"
            };

            MyListView.ItemsSource = Items;
            BindingContext = this;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)  //if it's nothing, go away
                return;

           

            await Navigation.PushAsync(new ShowData(e.Item.ToString())); //show menu in a list

            //Deselect Item
              ((ListView)sender).SelectedItem = null; 
        }
    }
}
