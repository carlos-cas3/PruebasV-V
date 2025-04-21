using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Laboratorio_Pag42_.selectores
{
    public class SelectoresTests
    {
#pragma warning disable NUnit1032
        IWebDriver driver;
        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            driver.Url = "https://curso.testautomation.es";
        }
        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }


        [Test]
        public void GetEachOfTheElements()
        {
            driver.FindElement(By.Id("SelectorsWeb")).Click(); //Selecciona el elemento de la web por id
            //en la segunda pagina, seleccionamos por ID el texto del Element 1
            driver.FindElement(By.Id("myId")).Text.Should().Be("Element 1");
        }
    }
}
