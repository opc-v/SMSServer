
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using SMSServer.Models;

namespace SMSServer.Controls
{
    public class OutgoingSMSSearchHandler : SearchHandler
    {
        public IList<OutgoingSMS> OutgoingSMSSs { get; set; }
        public Type SelectedItemNavigationTarget { get; set; }

        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);

            if (string.IsNullOrWhiteSpace(newValue))
            {
                ItemsSource = null;
            }
            else
            {
                ItemsSource = OutgoingSMSSs
                    .Where(sms => sms.Text.ToLower().Contains(newValue.ToLower()))
                    .ToList<OutgoingSMS>();
            }
        }

        protected override async void OnItemSelected(object item)
        {
            base.OnItemSelected(item);

            // Let the animation complete
            await Task.Delay(1000);

            ShellNavigationState state = (App.Current.MainPage as Shell).CurrentState;
            // The following route works because route names are unique in this application.
            await Shell.Current.GoToAsync($"{GetNavigationTarget()}?Id={((OutgoingSMS)item).Id}");
  
        }

        string GetNavigationTarget()
        {
            string result = (Shell.Current as AppShell).Routes.FirstOrDefault(route => route.Value.Equals(SelectedItemNavigationTarget)).Key;   
            return result;
        }
    }
}
