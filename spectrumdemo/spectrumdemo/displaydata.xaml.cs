using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static spectrumdemo.payments;

namespace spectrumdemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class displaydata : ContentPage
    {

        // set up our ObservableCollection for the data

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

        public displaydata(ItemTappedEventArgs e)
        {
            InitializeComponent();
            // now that we have the data from the click, lets parse and display it.

            if (e.Item == null)
                return;
            else
            {
                var person = new Person()
                {
                    first = (e.Item as Person).first,  // typecast and move
                     last = (e.Item as Person).last,
                    Paid = (e.Item as Person).Paid,
                    amount = (e.Item as Person).amount


                };

                Persons.Add(person); // add to the list


            };
            
           


            BindingContext = this;  // just in case
        }
    }
}