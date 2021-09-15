using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace FacebookExercise1
{
    class Program
    {
        IWebDriver driver;
        public IWebDriver SetUpDriver()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            return driver;
        }

        public void Click(IWebElement element)
        {
            element.Click();
        }

        public void SendText(IWebElement element, string value)
        {
            element.SendKeys(value);
        }


        #region facebook loginpage locators
        By CreateAccountButton = By.XPath("//a[contains(text(),'Create New Account')]");
        By FacebookToEnglish = By.XPath("//*[contains(text(),'English (US)')]");
        By Facebook_Moto = By.XPath("//*[contains(text(),'Connect with friends and the world around you on Facebook.')]");
        By first_name_field = By.XPath("//div//input[@name='firstname']");
        By last_name_field = By.XPath("//div//input[@name='lastname']");
        By mobile_field = By.XPath("//div//input[@name='reg_email__']");
        By password_field = By.XPath("//div//input[@name='reg_passwd__']");
        By terms_id = By.Id("terms-link");
        By unavailable = By.Id("ansidansijdas");
        #endregion

        #region terms page 
        By title_term = By.XPath("//h2");
        By menu_5 = By.XPath("//div[@class='_5tko _52ye _3m9']/div[@id]");
        #endregion

        static void Main(string[] args)
        {
            IWebDriver Browser;
            IWebElement element;
            Program program = new Program();
            Browser = program.SetUpDriver();
            Browser.Url = "https://www.facebook.com/";

            //1st Test Case
            Console.Clear();
            element = Browser.FindElement(program.FacebookToEnglish);
            program.Click(element);
            Assert.AreEqual("Connect with friends and the world around you on Facebook.", Browser.FindElement(program.Facebook_Moto).Text);

            element = Browser.FindElement(program.CreateAccountButton);
            program.Click(element);

            element = Browser.FindElement(program.first_name_field);
            program.SendText(element, "Diego");

            element = Browser.FindElement(program.last_name_field);
            program.SendText(element, "Brockman");

            element = Browser.FindElement(program.mobile_field);
            program.SendText(element, "123456789");

            element = Browser.FindElement(program.password_field);
            program.SendText(element, "Secret1234");
            Console.Clear();
            try
            { 
                element = Browser.FindElement(program.unavailable); 
            }
            catch (NoSuchElementException e)
            {
                Console.Clear();
                Console.WriteLine("This locator is not available on the page\n");   
            }


            //2nd Test case 
            element = Browser.FindElement(program.terms_id);
            program.Click(element);
            Browser.SwitchTo().Window(Browser.WindowHandles[1]);
            Assert.AreEqual("Terms of Service", Browser.FindElement(program.title_term).Text);
            element = Browser.FindElement(program.menu_5);
            Assert.IsTrue(element.Displayed);
            string[] text = element.Text.Split('\n');
            foreach(var word in text)
            {
                Console.WriteLine($"{word}");
            }
        }
    }
}
