using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace spectrumdemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class mywebview : ContentPage
    {
        public mywebview(string url)
        {
            InitializeComponent();

            
            webView.Source = url;
        }

        private void DisplayDataFromJavascript(string data1 = "")
        {


            // this.DisplayAlert("data1", data1, "OK");

            string[] val = data1.Split('|');
            string[] val2 = val[1].Split(';');
            switch (val[0])
            {

                case "CloseMe":
                    Navigation.PopAsync();  // this could go to any page in the app from the website.  Right now, we'll just go back to the previous page.
                    break;
                case "edit":
                    break;

                case "video":

                    break;

            }



        }

    }
}