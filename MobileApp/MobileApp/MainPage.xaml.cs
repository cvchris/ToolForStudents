using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Logic;

namespace MobileApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public static List<LessonWithMultipleTimes> fixedEvents = new List<LessonWithMultipleTimes>();
        public static List<LessonWithMultipleTimes> NotFixedEvents = new List<LessonWithMultipleTimes>();

        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var page = new AddLessonPage();
            page.SelectedStringChanged += Handle_SelectedStringChanged;
            await PopupNavigation.Instance.PushAsync(page, true);           
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            fixedEventsList.ItemsSource = null;
            fixedEventsList.ItemsSource = fixedEvents;
            nonFixedEventsList.ItemsSource = null;
            nonFixedEventsList.ItemsSource = NotFixedEvents;
        }

        private void Handle_SelectedStringChanged(object sender, EventArgs rags)
        {
            // ... your code
            fixedEventsList.ItemsSource = null;
            fixedEventsList.ItemsSource = fixedEvents;
            nonFixedEventsList.ItemsSource = null;
            nonFixedEventsList.ItemsSource = NotFixedEvents;
        }

        private void Calculate_Clicked(object sender, EventArgs e)
        {
            List<Event> tempList = new List<Event>();
            foreach(var lesson in fixedEvents)
            {
                tempList.AddRange(lesson.Times);
            }
            Tuple<List<TimeCalculate>, List<List<int>>> result = ApplicationLogic.Logic(tempList, NotFixedEvents, 30);//change the time to go to uni to parameter
            Navigation.PushAsync(new ResultsPage(tempList,result,NotFixedEvents));
        }
    }
}
