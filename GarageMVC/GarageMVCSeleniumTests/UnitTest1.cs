using GarageMVC;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Support.UI;
using Withywoods.WebTesting;

namespace GarageMVCSeleniumTests
{
    public class Tests
    {
        IWebDriver Driver { get; set; }
        HttpClient Client { get; set; }
        string HomeUrl { get; set; }

        [OneTimeSetUp]
        public void Setup()
        {
            StartProgramAtSomePort();
            Driver = StartAnyWebDriver();
        }

        void StartProgramAtSomePort()
        {
            WebApplicationFactoryFixture<Program> Waff = new WebApplicationFactoryFixture<Program>();
            int port = 5000;
            while (port < 6000) // Have to stop somewhere, but the main exit from the loop takes the form of a return.
            {
                Waff.HostUrl = $"https://localhost:{port}";
                try
                {
                    Client = Waff.WithWebHostBuilder(builder => builder.UseSetting("database", "inMemory")).CreateClient();
                    HomeUrl = Waff.HostUrl;
                    return;
                }
                catch (IOException ex)
                {
                    // Can the wording that contains "Failed to bind" be trusted to be stable?
                    if (!ex.Message.Contains("Failed to bind"))
                        throw new Exception("Creation of host failed due to something other than the chosen port being occupied", ex);
                }
                port++;
            }
            throw new Exception("Creation of host failed 1000 times");
        }

        IWebDriver StartAnyWebDriver()
        {
            List<Func<IWebDriver>> funcs = new List<Func<IWebDriver>>()
            {
                () => new ChromeDriver(),
                () => new FirefoxDriver(),
                () => new SafariDriver(),
                () => new EdgeDriver(),
                () => new InternetExplorerDriver()
            };
            List<Exception> exceptions = new List<Exception>();
            foreach (Func<IWebDriver> func in funcs)
            {
                try
                {
                    return func();
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            }
            throw new Exception("Creation of web driver failed");
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            Driver?.Dispose();
            Client?.Dispose();
        }

        [Test]
        public void EntryPageTest()
        {
            Driver.Navigate().GoToUrl(HomeUrl);

            Assert.That(Driver.PageSource.Contains("Welcome"));
        }

        [Test]
        public void LinkToCreateTest()
        {
            Driver.Navigate().GoToUrl(HomeUrl);
            Driver.FindElement(By.LinkText("Park a vehicle")).Click();
            Assert.That(Driver.PageSource.Contains("Park a Vehicle"));
            Assert.That(Driver.PageSource.Contains("Brand"));
        }
        [Test]
        public void CreateTest()
        {
            Driver.Navigate().GoToUrl(HomeUrl);
            Driver.FindElement(By.LinkText("Park a vehicle")).Click();
            Driver.FindElement(By.Name("RegistrationNumber")).SendKeys("ABC123");
            Driver.FindElement(By.Name("Brand")).SendKeys("Volvo");
            Driver.FindElement(By.Name("Model")).SendKeys("ÖV4");
            Driver.FindElement(By.Name("NumberOfWheels")).SendKeys("4");
            Driver.FindElement(By.Name("ParkingSpotNumber")).SendKeys("1");
            Driver.FindElement(By.XPath("//button[@type='submit']")).Submit();
            Assert.That(Driver.PageSource.Contains("Welcome"));
            Assert.That(Driver.PageSource.Contains("ABC123"));
        }
        [Test]
        public void DetailsTest()
        {
            Driver.Navigate().GoToUrl(HomeUrl);
            Driver.FindElement(By.LinkText("Park a vehicle")).Click();
            Driver.FindElement(By.Name("RegistrationNumber")).SendKeys("ABC125");
            Driver.FindElement(By.Name("Brand")).SendKeys("Saab");
            Driver.FindElement(By.Name("Model")).SendKeys("92");
            Driver.FindElement(By.Name("NumberOfWheels")).SendKeys("4");
            Driver.FindElement(By.Name("ParkingSpotNumber")).SendKeys("2");
            Driver.FindElement(By.XPath("//button[@type='submit']")).Submit();
            IWebElement row = Driver.FindElement(By.XPath("//td[contains(text(), 'ABC125')]/.."));
            Assert.That(row != null);
            row.FindElement(By.LinkText("Details")).Click();
            Assert.That(Driver.FindElement(By.XPath("//dt[contains(text(), 'RegistrationNumber')]/following::dd")).Text, Is.EqualTo("ABC125"));
            Assert.That(Driver.FindElement(By.XPath("//dt[contains(text(), 'Brand')]/following::dd")).Text, Is.EqualTo("Saab"));
            Assert.That(Driver.FindElement(By.XPath("//dt[contains(text(), 'Model')]/following::dd")).Text, Is.EqualTo("92"));
            Assert.That(Driver.FindElement(By.XPath("//dt[contains(text(), 'NumberOfWheels')]/following::dd")).Text, Is.EqualTo("4"));
        }

        [Test]
        public void CreateWithExplicitTypeAndColorTest()
        {
            Driver.Navigate().GoToUrl(HomeUrl);
            Driver.FindElement(By.LinkText("Park a vehicle")).Click();
            new SelectElement(Driver.FindElement(By.Name("Type"))).SelectByText("Truck");
            new SelectElement(Driver.FindElement(By.Name("Color"))).SelectByText("Red");
            Driver.FindElement(By.Name("RegistrationNumber")).SendKeys("ABC124");
            Driver.FindElement(By.Name("Brand")).SendKeys("Scania");
            Driver.FindElement(By.Name("Model")).SendKeys("Scania-Vabis 324");
            Driver.FindElement(By.Name("NumberOfWheels")).SendKeys("4");
            Driver.FindElement(By.Name("ParkingSpotNumber")).SendKeys("3");
            Driver.FindElement(By.XPath("//button[@type='submit']")).Submit();
            IWebElement row = Driver.FindElement(By.XPath("//td[contains(text(), 'ABC124')]/.."));
            Assert.That(row != null);
            row.FindElement(By.LinkText("Details")).Click();
            Assert.That(Driver.FindElement(By.XPath("//dt[contains(text(), 'Type')]/following::dd")).Text, Is.EqualTo("Truck"));
            Assert.That(Driver.FindElement(By.XPath("//dt[contains(text(), 'Color')]/following::dd")).Text, Is.EqualTo("Red"));
            Assert.That(Driver.FindElement(By.XPath("//dt[contains(text(), 'RegistrationNumber')]/following::dd")).Text, Is.EqualTo("ABC124"));
            Assert.That(Driver.FindElement(By.XPath("//dt[contains(text(), 'Brand')]/following::dd")).Text, Is.EqualTo("Scania"));
            Assert.That(Driver.FindElement(By.XPath("//dt[contains(text(), 'Model')]/following::dd")).Text, Is.EqualTo("Scania-Vabis 324"));
            Assert.That(Driver.FindElement(By.XPath("//dt[contains(text(), 'NumberOfWheels')]/following::dd")).Text, Is.EqualTo("4"));
        }

    }
}