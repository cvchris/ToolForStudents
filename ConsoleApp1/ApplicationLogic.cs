﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfApp1;

namespace ConsoleApp1
{
    public static class ApplicationLogic
    {
        private static List<Event> _allEvents;

        //roundTripWeight = the time to go to the university and back. (We assume that this will be done once a day).
        public static void Logic(List<Event> mandatoryEvents, List<LessonWithMultipleTimes> lessonsWithMultipleTimes,double roundTripWeight)
        {
            List<Event> allEvents = new List<Event>();
            allEvents.AddRange(mandatoryEvents);
            foreach (var lesson in lessonsWithMultipleTimes)
            {
                allEvents.AddRange(lesson.Times);
            }
            _allEvents = allEvents;

            //check for all overlapping events. if an non-fixed event has multiple occasions and one occasion is at the same time as one fixed, we should delete it from allEvents.
            //also if the non-fixed event has only one occassion (LessonWithMultipleETimes.Events.Count =1), and this is overlapping with a fixed event, log warning that there is no way that you can go to both
            checkForOverlapping();
            

            var notFixed = allEvents.Where(x => x.isFixed = false); //all the not fixed events

            List<TimeCalculate> times = new List<TimeCalculate>();

            foreach (var notFixedEvent in notFixed)
            {
                //search in allnonfixed if this is the only one with that lessonId. (if it is then automatically add it to final list- make it fixed)
                if(notFixed.Where(x=> x.Lesson == notFixedEvent.Lesson).ToList().Count == 1)
                {
                    //it may not be updated in the allEvents
                    notFixedEvent.isFixed = true; //we may need to mark it in another way
                }
                else
                {
                    var sameDayEvents = allEvents.Where(x => x.Day == notFixedEvent.Day).ToList();

                    double tempOffset = 0;
                    sameDayEvents = sameDayEvents.OrderBy(x => x.startTime).ToList();
                    var thisEvent = sameDayEvents.Single(x => x.Id == notFixedEvent.Id);

                    var time = new TimeCalculate
                    {
                        Event = thisEvent
                    };

                    double xronospetamenos = 0;

                    //find the total offset in that day. Begin from the first event of the day and sum the time to go.
                    //be careful: when an event has the same Lesson as the one in notFixedEvent... We should ignore it
                    //Also don't forget about dependencies
                    for (int i = 0; i < sameDayEvents.Count; i++)
                    {
                        if(sameDayEvents[i] != thisEvent && sameDayEvents[i].Lesson == thisEvent.Lesson)
                        {
                            //ignore it
                        }
                        else
                        {
                            //find the difference between this and the next event
                            int tempvalue = 1;
                            while (i + tempvalue < sameDayEvents.Count && sameDayEvents[i + tempvalue].Lesson == notFixedEvent.Lesson)
                            {
                                tempvalue++;
                            }

                            if (i + tempvalue < sameDayEvents.Count)
                            {
                                //meaning that we found match
                                var afterEvent = sameDayEvents[i + tempvalue];
                                xronospetamenos += (afterEvent.startTime - notFixedEvent.finishTime).TotalMinutes;

                                if (!afterEvent.isFixed) //&& samedayevents[i].isFixed
                                {
                                    //add dependency
                                    time.AfterEventDependency = afterEvent;
                                }
                            }
                            else
                            {
                                time.AfterEventDependency = null;
                                //we don't add the tempOffset because it doesn't have anything after that

                                //we couldn't find any match after this event, handle it
                            }


                        }
                    }
                    

                    //int index = sameDayEvents.IndexOf(thisEvent);
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

                    time.Time = tempOffset;
                    times.Add(time);
                }
            }

            /* from times we have:
                OptA -> 300min (No dependency)

            

            */
            //we need to make sure that for each LessonWithMultipleTimes there is only one event in the final calendar
        }

        private static void checkForOverlapping()
        {
            //get by day.
            IEnumerable<DayOfWeek> values = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>(); //gets all days in Enumerable
            foreach (DayOfWeek day in values)
            {
                var allEventsInSpecificDay = _allEvents.Where(x => x.Day == day).ToList();
                allEventsInSpecificDay = allEventsInSpecificDay.OrderBy(x => x.startTime).ToList();

                //not working, we could be having more that one overlapping

                //σωστος τροπος: για καθε event στην ημερα, τσεκαρε με ΟΛΑ τα υπολοιπα events εαν γίνεται καποιο overlapping... ισως ενας τροπος optimization είναι να μην κοιταει με τα προηγουμενα του (να το σιγουρεψω).

                //check for overlapping
                for (int i = 0; i < allEventsInSpecificDay.Count - 1; i++)
                {

                }

                /* old way, has bug

                //check for overlapping
                for (int i = 0; i < allEventsInSpecificDay.Count -1; i++)
                {
                    if(allEventsInSpecificDay[i].finishTime>allEventsInSpecificDay[i+1].startTime)
                    {
                        int howManyCannotBeDeleted = 0;

                        //check if either of them is non-fixed
                        if(allEventsInSpecificDay[i].isFixed && allEventsInSpecificDay[i+1].isFixed)
                        {
                            throw new Exception("We have a problem because both are fixed.");
                        }

                        if (!allEventsInSpecificDay[i].isFixed && !allEventsInSpecificDay[i+1].isFixed)
                        {
                            //both not fixed, find which is optimal to delete from the two
                        }



                        if(!allEventsInSpecificDay[i].isFixed)
                        {
                            //check if this event has another appearence
                            var otherEventsFromSameLesson = _allEvents.Where(x => x.Lesson == allEventsInSpecificDay[i].Lesson).ToList();
                            if(otherEventsFromSameLesson.Count > 1)
                            {
                                //check if the other ones are overlapping?
                                //for now we will just delete this event because it is not required
                                _allEvents.Remove(_allEvents.Single(x => x.Id == allEventsInSpecificDay[i].Id));
                            }
                            else
                            {
                                //this event cannot be deleted because there are not other options
                                howManyCannotBeDeleted++;
                                
                            }
                        }
                        
                        if(!allEventsInSpecificDay[i+1].isFixed)
                        {
                            //check if this event has another appearence
                            var otherEventsFromSameLesson = _allEvents.Where(x => x.Lesson == allEventsInSpecificDay[i+1].Lesson).ToList();
                            if (otherEventsFromSameLesson.Count > 1)
                            {
                                //check if the other ones are overlapping?
                                //for now we will just delete this event because it is not required
                                _allEvents.Remove(_allEvents.Single(x => x.Id == allEventsInSpecificDay[i+1].Id));
                            }
                            else
                            {
                                howManyCannotBeDeleted++;
                            }
                        }

                        if(howManyCannotBeDeleted ==2)
                        {
                            throw new Exception("We have a problem, both that are overlapping are not fixed.");
                        }
                    }
                }

                */
            }


            
            
        }

        /// <summary>
        /// if it is not the only one appearence, we can delete it
        /// </summary>
        /// <returns>True if it was deleted</returns>
        private static bool deleteIfNoProblem(int EventId)
        {
            var thisEvent = _allEvents.First(x => x.Id == EventId);

            //check if this event has another appearence
            var otherEventsFromSameLesson = _allEvents.Where(x => x.Lesson == thisEvent.Lesson).ToList();
            if (otherEventsFromSameLesson.Count > 1)
            {
                //check if the other ones are overlapping?
                //for now we will just delete this event because it is not required
                _allEvents.Remove(_allEvents.Single(x => x.Id == EventId));
                return true;
            }
            else
            {
                //this event cannot be deleted because there are not other options
                return false;

            }
        }
        

    }
}
