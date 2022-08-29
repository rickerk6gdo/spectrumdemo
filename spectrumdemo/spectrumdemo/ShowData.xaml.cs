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
    public partial class ShowData : ContentPage
    {
        public ShowData(string choice)
        {
            InitializeComponent();

        // our maing menu parse

      

            switch (choice)
            {
                case "Access The Spectrum Website":
                    {
                        Navigation.PushAsync(new mywebview("https://www.spectrum.net"));

                        // website
                        break;
                    }
                case "Demo of Web Javascript":
                    Navigation.PushAsync(new mywebview("https://collage.mydealerpreviews.com/vwalk/index.php?rooftop_id=1847&seed=165&type=c&contact_id=197251&version=2.02323.2088"));
                    break;

                case "Payments":
                    {
                        Navigation.PushAsync(new payments());

                        // payments page

                        break;
                    }

                case "Logout":
                    {
                        Application.Current.MainPage = new NavigationPage(new MainPage());

                        // back to login
                        break;
                    }

                default:
                    break;

            }
        }

    }





}

