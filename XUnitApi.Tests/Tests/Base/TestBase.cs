using API_ClassLibrary.Framework;
using API_ClassLibrary.Framework.Reporting;
using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Tests.Tests.Base
{
    public abstract class TestBase
    {
        /*[OneTimeSetUp]
        public virtual void BeforeAll()
        {
            FW.CreateTestResultDirectory();
        }

        [SetUp]
        public virtual void BeforeEach()
        {
            FW.SetLogger();
        }*/

        public Microsoft.VisualStudio.TestTools.UnitTesting.TestContext TestContext { get; set; }

        [ClassInitialize]
        public static void SetUpReport(Microsoft.VisualStudio.TestTools.UnitTesting.TestContext testContext)
        {
            var dir = testContext.TestRunDirectory;
            Reporter.SetUpReport(dir, "SmokeTest", "Smoke test result");
        }

        [TestInitialize]
        public void SetUpTest()
        {
            Reporter.CreateTest(TestContext.TestName);
        }

        [TestCleanup]
        public void TearDownTest()
        {
            var testStatus = TestContext.CurrentTestOutcome;
            Status status;

            switch (testStatus)
            {
                case UnitTestOutcome.Failed:
                    status = Status.Fail;
                    Reporter.TestStatus(status.ToString());
                    break;
                case UnitTestOutcome.Inconclusive:
                    break;
                case UnitTestOutcome.Passed:
                    status = Status.Pass;
                    break;
                case UnitTestOutcome.InProgress:
                    break;
                case UnitTestOutcome.Error:
                    break;
                case UnitTestOutcome.Timeout:
                    break;
                case UnitTestOutcome.Aborted:
                    break;
                case UnitTestOutcome.Unknown:
                    break;
                case UnitTestOutcome.NotRunnable:
                    break;
                default:
                    break;
            }
        }

        [ClassCleanup]
        public static void CleanUp()
        {
            Reporter.FlushReport();
        }
    }
}
