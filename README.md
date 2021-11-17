# Kayak.SearchUIAutomation
A test assignment.  
  
"Create simple framework based on https://www.kayak.com website.  
Prepare a checklist to demonstrate different test design techniques for Flight Search functionality.  
Automate the most important tests (at least 2-3) from prepared checklist to demonstrate main Automation testing patterns, 
all object-oriented programming paradigms should be applied.  
  
Please note that this should be done using technologies that we currently use - C# + SpecFlow + WebDriver"

### Remarks:  
* We could've used a scenario outline here but scenarios are too different for that.  
  
* Basically the search functionality itself is one of the first candidates for API testing,  
but the test assignment specification requires to use Selenium.  
  
* Ideally screenshots should be tacken when smth goes wrong but for that I need to  
implement scrolling to the desired ticket item which can't be done due to lack of time.  
  
* If run the tests with Chrome Headless Mode, kayak.com considers us as a bot, so  
setting Constants.HeadlessMode = true is not very usefull feature :)