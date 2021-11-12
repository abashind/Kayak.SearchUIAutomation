using JuribaKayak.SearchUIAutomation.Hooks;
using System;

namespace JuribaKayak.SearchUIAutomation.Helpers
{
    public class UiException : Exception
    {
        public UiException(string message) : base(message)
        {
            CLogs.Error(message);
            CLogs.Screen(DriverSetup.CurrentDriver);
        }
    }
}
