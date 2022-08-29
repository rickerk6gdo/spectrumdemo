using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static spectrumdemo.payments;

namespace spectrumdemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class payments : ContentPage
    {
       

        public payments()
        {

            InitializeComponent();
            BindingContext = this; // set the binding
            Doit();  // do init code

       

        }




        private ObservableCollection<Person> _persons;
   
        public ObservableCollection<Person> Persons
        {
            get
            {
                return _persons ?? (_persons = new ObservableCollection<Person>());
            }
            set
            {

            }
        }
        

       
         
        void Doit()
        {
            // what i would do here in an actual app, is make a call to the server to get the data with a rest call... from that I would parse the data from the JSON on the server, decode
            // it and put it in an linked list to add to the listview...    that way, I could make a reference the linked list and when the item was clicked, grab the data to diplay.
            // in this case, I am just creating the list, then on the click, passing the data that is clicked on to the display page.


            var person = new Person()
            {
                first = "Rick",
                last  = "Ruhl",
                Paid = 22,
                amount =22



            };

            Persons.Add(person);


            person = new Person()
            {
                first = "Scott",
                last = "Richards",
                Paid = 86,
                amount =84



            };

            Persons.Add(person);

            person = new Person()
            {
                first = "Charles",
                last = "Dean",
                Paid = 24,
                amount =26



            };

            Persons.Add(person);

            person = new Person()
            {
                first = "Micki",
                last = "Ricki",
                Paid = 66,
                amount = 66



            };


            Persons.Add(person);

            BindingContext = this;

        }


        void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            var textsender = ((Label)sender).Text; // get the text from the header

      /// now lets order them

           switch (textsender)
                {

                case "First":
                    {

                        var orderedElements = _persons.OrderByDescending(x => x.first); // order it 

                        PaymentList.ItemsSource = orderedElements;  // display sort

                    }
                    break;

                case "Last":
                    {

                        var orderedElements = _persons.OrderByDescending(x => x.last);

                        PaymentList.ItemsSource = orderedElements;

                    }
                    break;
                case "Payment":
                    {

                        var orderedElements = _persons.OrderByDescending(x => x.Paid);

                        
                        PaymentList.ItemsSource = orderedElements;
                    }
                    break;
                case "Amt":
                    {

                        var orderedElements = _persons.OrderByDescending(x => x.amount);


                        PaymentList.ItemsSource = orderedElements;

                    }
                    break;
            }

  
        }
        public class Person  // our class for the data 
        {
            public string first { get; set; }
            public string last { get; set; }

            public decimal  Paid { get; set; }

            public decimal amount { get; set; }

        }

        private void PaymentList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)  // do we have data?
                return;          // if not, dont
            Navigation.PushAsync(new displaydata(e)); // if so, pass the data to the display page.
           

        }
    }
    }

