using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Old_code
    {
        /*
            var notFixed = allEvents.Where(x => x.IsFixed = false); //all the not fixed events

            //List<TimeCalculate> times = new List<TimeCalculate>();

            foreach (var notFixedEvent in notFixed)
            {
                //search in allnonfixed if this is the only one with that lessonId. (if it is then automatically add it to final list- make it fixed)
                if (notFixed.Where(x => x.Lesson == notFixedEvent.Lesson).ToList().Count == 1)
                {
                    //it may not be updated in the allEvents
                    notFixedEvent.IsFixed = true; //we may need to mark it in another way
                }
                else
                {
                    var sameDayEvents = allEvents.Where(x => x.Day == notFixedEvent.Day).ToList();


                    sameDayEvents = sameDayEvents.OrderBy(x => x.startTime).ToList();
                    var thisEvent = sameDayEvents.Single(x => x.Id == notFixedEvent.Id);

                    var time = new TimeCalculate
                    {
                        //Event = thisEvent,
                        //Dependencies = new List<Event>()
                    };

                    double xronospetamenos = 0; //apothikevei se mia mera me vasi to programma posos xronos se xasimo yparxei

                    //find the total offset in that day. Begin from the first event of the day and sum the time to go.
                    //be careful: when an event has the same Lesson as the one in notFixedEvent... We should ignore it
                    //Also don't forget about dependencies
                    for (int i = 0; i < sameDayEvents.Count; i++)
                    {
                        //when it is its own event (samedayEvents[i] == thisEvent), we want to go to the else statement because we want to calculate the missed time.


                        if (sameDayEvents[i] != thisEvent && sameDayEvents[i].Lesson == thisEvent.Lesson) //From the same lesson but different event
                        {
                            //ignore it
                        }
                        else
                        {
                            //find the difference between this and the next event
                            int tempvalue = 1;
                            while (i + tempvalue < sameDayEvents.Count && sameDayEvents[i + tempvalue].Lesson == notFixedEvent.Lesson) //it is the same lesson
                            {
                                tempvalue++;
                            }

                            if (i + tempvalue < sameDayEvents.Count)//meaning that we found match
                            {
                                var afterEvent = sameDayEvents[i + tempvalue];
                                xronospetamenos += (afterEvent.startTime - notFixedEvent.finishTime).TotalMinutes;

                                if (!afterEvent.IsFixed) //&& samedayevents[i].isFixed
                                {
                                    //add dependency
                                    //time.Dependencies.Add(afterEvent);
                                    //time.AfterEventDependency = afterEvent;
                                }
                            }
                            else
                            {
                                //time.AfterEventDependency = null;
                                //we don't add the tempOffset because it doesn't have anything after that

                                //we couldn't find any match after this event, handle it
                            }


                        }
                    }



                    //int index = sameDayEvents.IndexOf(thisEvent); //old commented code
                    //int tempvalue = 1;
                    //while(index - tempvalue >= 0 && sameDayEvents[index-tempvalue].Lesson == notFixedEvent.Lesson)
                    //{
                    //    tempvalue++;
                    //}

                    //if(index-tempvalue>=0 )//meaning that we didn't reach the top of the list
                    //{
                    //    var beforeEvent = sameDayEvents[index - tempvalue];
                    //    tempOffset += (notFixedEvent.startTime - beforeEvent.finishTime).TotalMinutes;

                    //    if(!beforeEvent.isFixed)
                    //    {
                    //        //add dependency
                    //        time.BeforeEventDependency = beforeEvent;
                    //    }

                    //}
                    //else //index-tempvalue==0
                    //{
                    //    time.BeforeEventDependency = null;
                    //    //we don't add the tempOffset because it doesn't have anything before that
                    //    //it was the first of the list, or all the above were from the same lesson, handle it
                    //}


                    //tempvalue = 1;
                    //while(index + tempvalue < sameDayEvents.Count && sameDayEvents[index+tempvalue].Lesson == notFixedEvent.Lesson)
                    //{
                    //    tempvalue++;
                    //}

                    //if(index + tempvalue < sameDayEvents.Count)
                    //{
                    //    //meaning that we found match
                    //    var afterEvent = sameDayEvents[index + tempvalue];
                    //    tempOffset += (afterEvent.startTime - notFixedEvent.finishTime).TotalMinutes;

                    //    if (!afterEvent.isFixed)
                    //    {
                    //        //add dependency
                    //        time.AfterEventDependency= afterEvent;
                    //    }
                    //}
                    //else
                    //{
                    //    time.AfterEventDependency = null;
                    //    //we don't add the tempOffset because it doesn't have anything after that

                    //    //we couldn't find any match after this event, handle it
                    //}

                    time.Time = xronospetamenos;
                    times.Add(time);
                }
            }

            //we need to make sure that for each LessonWithMultipleTimes there is only one event in the final calendar

    */
    }
}
