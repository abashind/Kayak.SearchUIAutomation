using JuribaKayak.SearchUIAutomation.Helpers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JuribaKayak.SearchUIAutomation.PageObjects.Controls
{
    public class TicketItem : BaseControl
    {
        public TicketItem(By locator) : base(locator) { }

        #region Fields and properties

        public int NumberOfFlights() => el.FindElements(flightLocator).Count;

        public int NumberOfSeats => GetNumberOfSeats();

        public IEnumerable<Flight> Flights => GetFlights();

        private By flightLocator = By.CssSelector(".flight > div");

        private By totalPriceLocator = By.ClassName("price-total");

        private By priceLocator = By.ClassName("price-text");

        public string Name => el.GetAttribute("aria-label");

        #endregion

        #region Method

        private IEnumerable<Flight> GetFlights() => el.FindElements(flightLocator)
            .Select(e => new Flight(By.Id(e.GetAttribute("id"))));

        private int GetNumberOfSeats()
        {
            if (!el.ElementExists(totalPriceLocator))
                return 1;
            return (int)Math.Round((float)GetTotalPrice() / GetPrice());
        }

        private int GetTotalPrice()
        {
            var text = el.FindElement(totalPriceLocator).Text;
            var textPrice = text.Trim()[1..].Replace(" total", string.Empty);
            return int.Parse(textPrice);
        }

        private int  GetPrice()
        {
            var text = el.FindElement(priceLocator).Text;
            var textPrice = text.Trim()[1..];
            return int.Parse(textPrice);
        }

        #endregion
    }
}
