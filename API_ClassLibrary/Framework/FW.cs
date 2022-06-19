using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace API_ClassLibrary.Framework
{
    public class FW
    {
        public static string WORKSPACE_DIRECTORY = Path.GetFullPath(@"../../../../");

        public static Logger Log => _logger ?? throw new NullReferenceException("_logger is null. SetLogger() first.");
        //public static Logger Log => _logger ?? Logger.Current;

        [ThreadStatic] public static Logger _LogCopy; 

        [ThreadStatic] public static DirectoryInfo CurrentTestDirectory;

        [ThreadStatic] public static Logger _logger;

        public static void SetLogger()
        {
            lock (_setLoggerLock)
            {
                var testResultDir = WORKSPACE_DIRECTORY + "TestResults";
                var testName = TestContext.CurrentContext.Test.Name.Split('(')[0];
                var fullPath = $"{testResultDir}/{testName}";

                if (Directory.Exists(fullPath))
                {
                    CurrentTestDirectory = Directory.CreateDirectory(fullPath + TestContext.CurrentContext.Test.ID);
                }
                else
                {
                    CurrentTestDirectory = Directory.CreateDirectory(fullPath);
                }

                _logger = new Logger(testName, CurrentTestDirectory.FullName + "/log.txt");
                _LogCopy = _logger;
            }
        }

        private static object _setLoggerLock = new object();

        public static DirectoryInfo CreateTestResultDirectory()
        {
            var testDirectory = WORKSPACE_DIRECTORY + "TestResults";
            if (Directory.Exists(testDirectory))
            {
                Directory.Delete(testDirectory, recursive: true);
            }

            return Directory.CreateDirectory(testDirectory);
        }
    }
}
