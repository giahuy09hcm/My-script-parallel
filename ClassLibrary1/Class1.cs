using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class Class1
    {
        private static Dictionary<string, long> _lstTime = new Dictionary<string, long>();
        private void RunTest(string ThreadName)
        {
            var driver = new ChromeDriver();
            Stopwatch timer = new Stopwatch();

            driver.Url = "https://qc03-portal.dev.local/";
            Thread.Sleep(1000);
            IWebElement txtUserName = driver.FindElement(By.Id("Username"));
            txtUserName.SendKeys("van.hoang");
            IWebElement txtPass = driver.FindElement(By.Id("txtPassword"));
            txtPass.SendKeys("vnr@@123");
            IWebElement btnLogin = driver.FindElement(By.XPath("//button[@value='login']"));
            btnLogin.Click();
            driver.Navigate().GoToUrl("https://qc03-portal.dev.local/my-app/#/attendance/overtime/overtime-approve");
            Thread.Sleep(8000);
            IWebElement btnAll = driver.FindElement(By.XPath("//div[@class='ant-tabs-tab ng-star-inserted']"));
            btnAll.Click();
            IWebElement btnSearch = driver.FindElement(By.TagName("vnr-toolbar-new"));
            IWebElement btnSearch1 = btnSearch.FindElement(By.TagName("button"));
            btnSearch1.Click();
            IWebElement btnSearch2 = driver.FindElement(By.XPath("//button[@class='ant-btn btn btn-info overtime__btnSearch ng-star-inserted']"));
            //IWebElement btnSearch3 = btnSearch2.FindElement(By.TagName("vnr-button"));
            timer.Start();
            btnSearch2.Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
            var loading = wait.Until((d) =>
            {
                try
                {
                    return driver.FindElement(By.XPath("//div[@class='ngx-bar ng-star-inserted']"));
                }
                catch
                {
                    return null;
                }
            });

            while (loading != null)
            {
                Thread.Sleep(100);
                try
                {
                    loading = driver.FindElement(By.XPath("//div[@class='ngx-bar ng-star-inserted']"));
                }
                catch
                {
                    loading = null;
                }
            }

        //Thread.Sleep(8000);
        timer.Stop();
            Console.WriteLine(string.Format("Thread {0} - Thời gian {1}", ThreadName, timer.ElapsedMilliseconds / (long)1000));
            _lstTime.Add(ThreadName, timer.ElapsedMilliseconds);

        }

    [Test]
    public void Test1()
    {
        RunTest("Test 1");
    }
    [Test]
    public void Test2()
    {
        RunTest("Test 2");
    }
    [Test]
    public void Test3()
    {
        RunTest("Test 3");
    }
    [Test]
    public void Test4()
    {
        RunTest("Test 4");
    }
    [Test]
    public void Test5()
    {
        RunTest("Test 5");
    }
        /*public void Test6()
        {
            RunTest("Test 6");
        }
        [Test]
        public void Test7()
        {
            RunTest("Test 7");
        }
        [Test]
        public void Test8()
        {
            RunTest("Test 8");
        }
        [Test]
        public void Test9()
        {
            RunTest("Test 9");
        }
        public void Test10()
        {
            RunTest("Test 10");
        }*/

        [TearDown]
    public void TearDown()
    {
        foreach (var t in _lstTime)
        {
            Console.WriteLine(string.Format("Thread {0} - Thời gian {1}", t.Key, t.Value));
        }
    }
}
}
