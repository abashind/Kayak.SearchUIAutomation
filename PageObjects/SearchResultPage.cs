using OpenQA.Selenium;

namespace JuribaKayak.SearchUIAutomation.PageObjects
{
    class SearchResultsPage : BasePage
    {
        public SearchResultsPage() : base()
        {
            this.CheckLocator = By.Id("searchResultsList");
        }
    }
}
