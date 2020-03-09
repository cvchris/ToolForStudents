using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultsPage : ContentPage
    {
        public ResultsPage(List<Event> fixedEvents, Tuple<List<TimeCalculate>, List<List<int>>> tuple)
        {
            InitializeComponent();
            var times = tuple.Item1;
            var memory = tuple.Item2;
        }
    }
}