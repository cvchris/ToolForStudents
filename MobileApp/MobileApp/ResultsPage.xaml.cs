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
        public ResultsPage(List<Event> fixedEvents, Tuple<List<TimeCalculate>, List<List<int>>> tuple,List<LessonWithMultipleTimes> optionalEvents)
        {
            InitializeComponent();
            var times = tuple.Item1;
            var combinations = tuple.Item2;

            TimeCalculate min = times.OrderBy(x => x.Time).First();
            List<int> idsToAdd = combinations[min.MemoryId];
            List<Event> toDisplay = fixedEvents.ToList(); //arxika vale ola ta fixed events
            List<Event> allOptionalEvents = new List<Event>();
            foreach (var lesson in optionalEvents)
            {
                allOptionalEvents.AddRange(lesson.Times);
            }
            foreach (var id in idsToAdd)
            {
                toDisplay.Add(allOptionalEvents.First(x => x.Id == id));
            }

            toDisplay = toDisplay
                .OrderBy(x => ((int)x.Day + 6) % 7)
                .ThenBy(x => x.startTime)
                .ToList();
            resultsToShowList.ItemsSource = toDisplay;



        }
    }
}