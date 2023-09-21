using Automation_ApplicationLayer.BrowserUtilities;
using Automation_ApplicationLayer.Reports;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace BDDAutomation_Test.Hooks
{
    [Binding]
    public class HooksBindings
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks

        [BeforeTestRun]
        public static void InitializeReporter()
        {
            
            FileInfo assemblyFileInfo = new FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);

            var reportsPath = Path.Combine(assemblyFileInfo.DirectoryName, "Reports", $"Reports-{DateTime.Now.ToString("ddMMyyyy")}");
            if (!Directory.Exists(reportsPath)) Directory.CreateDirectory(reportsPath);
            Reporting.SetupExtentReport("BDDTest Automation Report", "Test Automation Report", reportsPath);
        }

        [BeforeScenario]
        public static void BeforeScenario(ScenarioContext scenarioContext)
        {
            Reporting.CreateTest(scenarioContext.ScenarioInfo.Title);
        }

        /// <summary>
        /// This method will be executed after each scenario and close the driver objects.
        /// </summary>
        [AfterScenario]
        public static void DriverClose(ScenarioContext scenarioContext)
        {
            Reporting.TestStatus(scenarioContext.ScenarioExecutionStatus);
        }

        [AfterStep]
        public static void InsertSteps(ScenarioContext scenarioContext)
        {
            var stepInfoText = scenarioContext.StepContext.StepInfo.Text;
            var stepType = scenarioContext.StepContext.StepInfo.StepInstance.StepDefinitionType;

            List<ScenarioExecutionStatus> failTypes = new List<ScenarioExecutionStatus>
                                {
                                        ScenarioExecutionStatus.BindingError,
                                        ScenarioExecutionStatus.TestError,
                                        ScenarioExecutionStatus.UndefinedStep,
                                };

            if (scenarioContext.ScenarioExecutionStatus != ScenarioExecutionStatus.OK)
            {
                if (scenarioContext.ScenarioExecutionStatus == ScenarioExecutionStatus.StepDefinitionPending)
                {
                    Reporting.Log(LogStatus.Error, $"Step of type {stepType} : {stepInfoText} - This step has been skipped and not executed.", true);
                }
                else if (failTypes.Contains(scenarioContext.ScenarioExecutionStatus))
                {
                    Reporting.Log(LogStatus.Fail, $"Step of type {stepType} : {stepInfoText} - {scenarioContext.TestError.Message}", true);
                }
            }
            else
            {
                Reporting.Log(LogStatus.Pass, $"Step of type {stepType} : {stepInfoText} - Passed!!");
            }
        }

        [AfterTestRun]
        public static void AfterScenario()
        {
            try
            {
                Reporting.Flush();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                BrowserUtils.Quit();
            }
        }
    }
}