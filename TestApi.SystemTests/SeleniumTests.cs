using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestApi.SystemTests
{
    [TestClass]
    public abstract class SeleniumTest
    {

        //const int iisPort = 2020;
        private string _applicationName;
        private static Process _iisProcess;

        public SeleniumTest()
        {
            StartIIS();
        }

        protected SeleniumTest(string applicationName)
        {
            _applicationName = applicationName;
        }

        [ClassCleanup]
        public void TestCleanup()
        {
            // Ensure IISExpress is stopped
            if (_iisProcess.HasExited == false)
            {
                _iisProcess.Kill();
            }
        }

        private void StartIIS()
        {
            var applicationPath = GetApplicationPath(_applicationName);
            var programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);

            applicationPath = string.Format("\"{0}\"", applicationPath);

            _iisProcess = new Process();
            _iisProcess.StartInfo.ErrorDialog = true;
            _iisProcess.StartInfo.LoadUserProfile = true;
            _iisProcess.StartInfo.CreateNoWindow = false;
            _iisProcess.StartInfo.UseShellExecute = false;
            _iisProcess.StartInfo.FileName = programFiles + @"\IIS Express\iisexpress.exe";
            _iisProcess.StartInfo.Arguments = "/path:" + applicationPath;
            _iisProcess.Start();

            if (!_iisProcess.HasExited)
                Debug.Print("Is running");

            else
                Debug.Print("Is not running.");
        }

        protected virtual string GetApplicationPath(string applicationName)
        {
            var solutionFolder = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory)));
            return solutionFolder;
        }

        //public string GetAbsoluteUrl(string relativeUrl)
        //{
        //    if (!relativeUrl.StartsWith("/"))
        //    {
        //        relativeUrl = "/" + relativeUrl;
        //    }
        //    return String.Format("http://localhost:{0}{1}", iisPort, relativeUrl);
        //}

        public static string GetBaseUrl()
        {
            int iisExpressDefaultPort = 8080;

            return String.Format("http://localhost:{0}", iisExpressDefaultPort);
        }
    }
}
