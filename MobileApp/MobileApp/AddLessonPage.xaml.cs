﻿using Logic;
using Rg.Plugins.Popup.Services;
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
    public partial class AddLessonPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        List<Event> events = new List<Event>();

        public EventHandler SelectedStringChanged { get; set; }

        private int addedEntitiesCount;
        public AddLessonPage()
        {
            InitializeComponent();
            eventsList.ItemsSource = events;
            addedEntitiesCount = 0;
            var values = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>().ToList();
            values.Add(values[0]);
            values.RemoveAt(0);
            //var temp = values[0];
            //values[0] = values[6];
            //values[6] = temp;
            dayPicker.ItemsSource = values;
        }

        

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (dayPicker.SelectedItem == null)
            {
                DisplayAlert("Προσοχή","Πρέπει να επιλέξεις μια ημέρα","OK");
                return;
            }
            if(startTime.Time > finishTime.Time) //doesn't work, to fix
            {
                DisplayAlert("Προσοχή", "Η ημερομηνία λήξης του μαθήματος πρέπει να είναι μεγαλύτερη από της έναρξης", "OK");
                return;
            }
            if(startTime.Time == finishTime.Time)
            {
                DisplayAlert("Προσοχή", "Η ημερομηνία λήξης του μαθήματος πρέπει να είναι διαφορετική από της έναρξης", "OK");
                return;
            }



            //do a check that the values are correct.
            if (addedEntitiesCount == 0)
            {
                isFixedSwitch.IsEnabled = false;
                lessonName.IsEnabled = false;
            }
            var ev = new Event
            {
                Day = (DayOfWeek)dayPicker.SelectedItem,
                IsFixed = isFixedSwitch.IsToggled,
                Name = lessonName.Text,
                Id = EventId.getAndIncrementId()
            };
            ev.SetTime(startTime.Time.Hours, startTime.Time.Minutes, finishTime.Time.Hours, finishTime.Time.Minutes);
            
            events.Add(ev);
            eventsList.ItemsSource = null;
            eventsList.ItemsSource = events;
            dayPicker.SelectedItem = null;
            addedEntitiesCount++;
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            if (events.Count == 0)
            {
                DisplayAlert("Προσοχή", "Πρέπει να προσθέσεις μια ημερομηνία", "ΟΚ");
                return;
            }
            var lesson = new LessonWithMultipleTimes(events, isFixedSwitch.IsToggled)
            {
                Name = lessonName.Text
            };
            //lesson.Name = lessonName.Text;
            if (isFixedSwitch.IsToggled)
            {
                MainPage.fixedEvents.Add(lesson);
            }
            else
            {
                MainPage.NotFixedEvents.Add(lesson);
            }
            SelectedStringChanged(sender, e);
            PopupNavigation.Instance.PopAsync(true);

        }

        private async void eventsList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var ev = e.Item as Event;
            bool todelete = await DisplayAlert("Delete this?", "You sure you want to delete this?", "Yes", "No go back");
            if(todelete)
            {
                events.Remove(ev);
                eventsList.ItemsSource = null;
                eventsList.ItemsSource = events;
            }
        }
    }
}