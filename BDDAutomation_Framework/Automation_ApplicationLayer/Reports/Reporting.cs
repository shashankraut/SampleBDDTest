using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;
using Automation_ApplicationLayer.BrowserUtilities;

namespace Automation_ApplicationLayer.Reports
{
    public static class Reporting
    {
        #region Member Declarations
        private static ExtentReports? extentReports;
        private static ExtentTest? testCase;
        private static ExtentHtmlReporter? htmlReporter;
        private static string? reportsPath;
        #endregion

        /// <summary>
        /// To configure and setup extent report
        /// </summary>
        /// <param name="reportName">Report name</param>
        /// <param name="reportTitle">Report title</param>
        /// <param name="reportPath">Path to store report</param>
        public static void SetupExtentReport(string reportName, string reportTitle, dynamic reportPath)
        {
            htmlReporter = new ExtentHtmlReporter(reportPath);
            reportsPath = reportPath;
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;
            htmlReporter.Config.DocumentTitle = reportTitle;
            htmlReporter.Config.ReportName = reportName;

            extentReports = new ExtentReports();
            extentReports.AttachReporter(htmlReporter);
        }

        /// <summary>
        /// To create test case with extent report instance
        /// </summary>
        /// <param name="testName">Test case name</param>
        public static void CreateTest(string testName)
        {
            testCase = extentReports?.CreateTest(testName);
        }

        /// <summary>
        /// To log messages to extent report for a specific test case
        /// </summary>
        /// <param name="status">Instance of <see cref="LogStatus"/> specifying error status</param>
        /// <param name="logMessage">Message to log</param>
        /// <param name="createScreenCapture">True if want to take a screenshot along with log, Else False</param>
        /// <param name="raiseAssertFailure">True if want to raise the assert failure after adding log to extent report</param>
        public static void Log(LogStatus status, string logMessage, bool createScreenCapture = false, bool raiseAssertFailure = false)
        {
            if (testCase == null) { return; }
            MediaEntityModelProvider? mediaEntityModelProvider = null;
            if (createScreenCapture)
            {
                try
                {
                    Screenshot? screenshot = BrowserUtils.GetScreenshot();
                    string? screenShot = screenshot?.AsBase64EncodedString;
                    mediaEntityModelProvider = MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenShot).Build();
                }
                catch (Exception ex)
                {
                    testCase.Log(Status.Info, $"Exception occurred while getting screenshot : {ex.Message}");
                    mediaEntityModelProvider = null;
                }
            }
            testCase.Log((Status)status, logMessage, mediaEntityModelProvider);
            if (raiseAssertFailure) NUnit.Framework.Assert.Fail(logMessage);
        }

        /// <summary>
        /// To update test case status
        /// </summary>
        /// <param name="status">Test case status</param>
        public static void TestStatus(System.Enum executionStatus)
        {
            switch (executionStatus)
            {
                case ExecutionStatus.OK:
                    testCase?.Pass("Test passed!!!");
                    break;
                case ExecutionStatus.StepDefinitionPending:
                    testCase?.Error("Step Definition pending!!!");
                    break;
                case ExecutionStatus.UndefinedStep:
                    testCase?.Error("Step Undefined!!!");
                    break;
                case ExecutionStatus.BindingError:
                    testCase?.Error("Step binding error!!!");
                    break;
                case ExecutionStatus.TestError:
                    testCase?.Error("Test Failed!!!");
                    break;
                case ExecutionStatus.Skipped:
                    testCase?.Skip("Test Skipped!!!");
                    break;
            }
        }

        /// <summary>
        /// To flush the extent report and finalize
        /// </summary>
        public static void Flush()
        {
            if (extentReports != null)
            {
                extentReports.Flush();
            }
        }
    }
}