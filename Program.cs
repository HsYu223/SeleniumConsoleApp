using System;
using System.IO;
using System.Linq;
using System.Threading;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumConsoleApp.Model;
using Timer = System.Timers.Timer;

namespace SeleniumConsoleApp
{
    class Program
    {
        private readonly static string _url = "";
        private static Timer _timer;
        private static ParameterDataModel _parameter;

        static void Main(string[] args)
        {
            _parameter = JObject.Parse(
                           File.ReadAllText(@"parameter.json")).ToObject<ParameterDataModel>();
            _timer = new Timer(TimeSpan.FromMinutes(_parameter.Timer).TotalMilliseconds);

            TriggerEven();

            // Hook up the Elapsed event for the timer. 
            _timer.Elapsed += (sender, e) => TriggerEven();

            // Have the timer fire repeated events (true is the default)
            _timer.AutoReset = true;

            // Start the timer
            _timer.Enabled = true;

            Console.WriteLine("Press the Enter key to exit the program at any time... ");
            Console.ReadLine();
        }

        private static void TriggerEven()
        {
            // 08點檢查簽到
            if (DateTime.Now.Hour.Equals(8) &&
                (DateTime.Now.Minute >= _parameter.SignInAt && DateTime.Now.Minute <= _parameter.SignInBefore) &&
                _parameter.DayOfWeek.Contains((int)DateTime.Now.DayOfWeek))
            {
                SignIn();
            }

            // 18點檢查簽退
            if (DateTime.Now.Hour.Equals(18) &&
                (DateTime.Now.Minute >= _parameter.SignOutAt && DateTime.Now.Minute <= _parameter.SignOutBefore) &&
                _parameter.DayOfWeek.Contains((int)DateTime.Now.DayOfWeek))
            {
                SignOut();
            }
            Console.WriteLine("The Elapsed event was raised at {0}", DateTime.Now);
        }

        /// <summary>
        /// 簽到
        /// </summary>
        private static void SignIn()
        {
            using (var driver = new ChromeDriver("."))
            {
                //開啟網頁
                driver.Navigate().GoToUrl(_url);
                Thread.Sleep(2000);

                //點擊執行
                var submitButton = driver.FindElementsByCssSelector("button.checkin");
                Thread.Sleep(2000);
                submitButton.FirstOrDefault().Click();
                Thread.Sleep(2000);

                Verify(driver);

                driver.Navigate().GoToUrl(_url);
                driver.Quit();
            }
        }

        /// <summary>
        /// 簽退
        /// </summary>
        private static void SignOut()
        {
            using (var driver = new ChromeDriver("."))
            {
                //開啟網頁
                driver.Navigate().GoToUrl(_url);
                Thread.Sleep(2000);

                //點擊執行
                var submitButton = driver.FindElementsByCssSelector("button.checkout");
                Thread.Sleep(2000);
                submitButton.FirstOrDefault().Click();
                Thread.Sleep(2000);

                //如果有重複點選簽退
                try
                {
                    var confirm_win = driver.SwitchTo().Alert();
                    confirm_win.Accept();
                }
                catch (System.Exception ex)
                {

                }

                Thread.Sleep(2000);

                Verify(driver);

                driver.Navigate().GoToUrl(_url);
                driver.Quit();
            }
        }

        /// <summary>
        /// 解決驗證彈跳
        /// </summary>
        /// <param name="driver">The driver.</param>
        private static void Verify(ChromeDriver driver)
        {
            driver.SwitchTo().Frame(driver.FindElement(By.TagName("iframe")));

            Thread.Sleep(2000);

            //回答問題
            var labQuestion = driver.FindElement(By.XPath(@"//*[@id='labQuestion']")).Text;
            var answer = string.Empty;

            if (labQuestion.Contains("生日"))
            {
                answer = _parameter.Birthday;
            }
            else if (labQuestion.Contains("到職日"))
            {
                answer = _parameter.JobDay;
            }
            else if (labQuestion.Contains("外部EMAIL"))
            {
                answer = _parameter.EMail;
            }
            else if (labQuestion.Contains("身份證號"))
            {
                answer = _parameter.Id;
            }

            var txtAnswer = driver.FindElement(By.XPath(@"//*[@id='txtAnswer']"));
            Thread.Sleep(2000);
            txtAnswer.SendKeys(answer);
            Thread.Sleep(2000);

            var btnSave = driver.FindElement(By.XPath(@"//*[@id='btnSave']"));
            Thread.Sleep(2000);
            btnSave.Click();
            Thread.Sleep(2000);

            //如果有重複點選簽退
            var check_result = driver.SwitchTo().Alert();
            check_result.Accept();
            Thread.Sleep(2000);
        }
    }
}
