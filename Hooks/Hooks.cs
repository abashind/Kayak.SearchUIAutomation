using System.Collections.Generic;
using System.IO;
using JuribaKayak.SearchUIAutomation.Helpers;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TechTalk.SpecRun.Framework;

namespace JuribaKayak.SearchUIAutomation.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        public IWebDriver Driver;
        private readonly ScenarioContext _scenarioContext;

        public Hooks(ScenarioContext scenarioContext) => _scenarioContext = scenarioContext;

        /// <summary>
        /// Terminate scenario live time browser.
        /// </summary>
        [AfterScenario]
        public void ReleaseScenarioLiveDriver()
        {
            if (DriverSetup.CurrentDriver?.IsBrowserOpen() == true)
                DriverSetup.CurrentDriver?.Quit();
        }

        /// <summary>
        /// Send step information into console and debug streams.
        /// </summary>
        [BeforeStep]
        public void PrintTeamcityMetadataBefore()
        {
            var stepType = _scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            var stepText = _scenarioContext.StepContext.StepInfo.Text.Replace(" '", " *").Replace("' ", "* ");
            CLogs.Step($"{stepType}: {stepText}");
            CLogs.Info($"{stepType}: {stepText}");
        }
    }

    /// <summary>
    /// A class for executing some actions only ONCE before (Apply method) and after (Restore method) the test run.
    /// </summary>
    public class TestRunInit : IDeploymentTransformationStep
    {
        /// <summary>
        /// This method will be executed only once before the test run, in spite of multithreading.
        /// If we used a BeforeTestRun hook the hook would be executed in every thread.
        /// </summary>
        /// <param name="deploymentContext">You can put some parameters into the .srprofile file to read them out here, see the example here
        /// - https://github.com/SpecFlowOSS/SpecFlow.Plus.Examples/blob/master/CustomDeploymentSteps/WindowsAppDriver/Default.srprofile</param>
        public void Apply(IDeploymentContext deploymentContext)
        {
            static void Clean(IEnumerable<FileInfo> files)
            {
                foreach (var file in files)
                {
                    if (file.IsLocked()) continue;
                    CLogs.Info($"Deleting {file.FullName}.");
                    file.Delete();
                    CLogs.Info($"{file.FullName} has been deleted.");
                }
            }

            var dirInfo = new DirectoryInfo(Directory.GetCurrentDirectory());
            Clean(dirInfo.GetFiles());

            dirInfo = Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "screenshots"));
            Clean(dirInfo.GetFiles());
        }

        /// <summary>
        /// This method will be executed only once after the test run, in spite of multithreading.
        /// If we used an AfterTestRun hook the hook would be executed in every thread.
        /// </summary>
        public void Restore(IDeploymentContext deploymentContext)
        {

        }
    }
}
