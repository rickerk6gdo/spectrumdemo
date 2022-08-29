using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace spectrumdemo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Constants.isvideo = false;  // for a bug in andriod webview to take a picture
        }

      

        private void myButton_Clicked(object sender, EventArgs e)
        {
            //  logon ok, lets go
            Navigation.PushAsync(new myListView()); // call the menupage but it's a list too.


        }
    }
}
