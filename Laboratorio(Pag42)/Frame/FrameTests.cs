using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using FluentAssertions;
using System.Security.Cryptography.X509Certificates;

namespace Laboratorio_Pag42_.Frame
{
    public class FrameTests
    {
#pragma warning disable NUnit1032
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            driver.Url = "https://curso.testautomation.es";
        }
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [Test]
        public void FrameTest()
        {
            driver.FindElement(By.Id("DifferentFrames")).Click();   // hace el click en la primera pagina y va a la segunda

            driver.FindElement(By.CssSelector("button")).Click();   // hace click en boton de la segunda pagina.se muestran los dos iframe

            driver.SwitchTo().Frame(0); // nos ubicamos en el primer iframe
            var webElementLeft = driver.FindElement(By.CssSelector("h2")).Text; // optenemos el valor del texto
            webElementLeft.Should().Be("Welcome to the main frame!");

            //para el segundo iframe
            driver.SwitchTo().DefaultContent(); //retorna al contenedor general.
            driver.SwitchTo().Frame(1);
            var webElementRight = driver.FindElement(By.CssSelector("h2")).Text; //optenemos el valor del texto
            webElementRight.Should().Be("Content in the secondary frame!");


        }
    }
}
