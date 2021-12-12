using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Models;

namespace Domain.Publisher
{
    public class Publisher : IPublisher
    {
        public event EventHandler<WeatherForecastModel> OnChange;

        public void NotifySubscribers(WeatherForecastModel eventArgs)
        {
            //Create list of exception
            List<Exception> exceptions = new List<Exception>();

            if (OnChange == null) return;

            //Invoke OnChange Action by iterating on all subscribers event handlers
            foreach (Delegate handler in OnChange.GetInvocationList())
            {
                try
                {
                    //pass sender object and eventArgs while
                    handler.DynamicInvoke(this, eventArgs);
                }
                catch (Exception e)
                {
                    //Add exception in exception list if occurred any
                    exceptions.Add(e);
                }
            }

            //Check if any exception occurred while 
            //invoking the subscribers event handlers
            if (exceptions.Any())
            {
                //Throw aggregate exception of all exceptions 
                //occurred while invoking subscribers event handlers
                throw new AggregateException(exceptions);
            }
        }
    }
}

