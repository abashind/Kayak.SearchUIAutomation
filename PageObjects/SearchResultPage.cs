using JuribaKayak.SearchUIAutomation.Helpers;
using JuribaKayak.SearchUIAutomation.PageObjects.Controls;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace JuribaKayak.SearchUIAutomation.PageObjects
{
    class SearchResultsPage : BasePage
    {
        public SearchResultsPage() : base()
        {
            this.CheckLocator = By.Id("searchResultsList");
            this.MoreButton = new Button(By.CssSelector("[id$=\"loadMore\"]"));
        }

        #region Fields and properties

        public Button MoreButton;

        private By ticketItemLocator => By.ClassName("Base-Results-HorizonResult");

        public IEnumerable<TicketItem> TicketItems => Driver.FindElements(ticketItemLocator)
            .Select(e => new TicketItem(By.Id(e.GetAttribute("id"))));

        #endregion

        #region Methods

        /// <summary>
        /// Example of the polymorphism.
        /// </summary>
        /// <param name="secondsToWait"></param>
        /// <returns></returns>
        public override bool Exists(int secondsToWait = 20) =>
            base.Exists(secondsToWait) && MoreButton.Exists(waitElementIsVisible:30);

        #endregion
    }
}

