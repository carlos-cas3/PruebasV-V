using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Laboratorio_Pag42_.Inicio
{
    [TestFixture]
    public class TestBasico
    {
#pragma warning disable NUnit1032
        ChromeDriver driver;

        [SetUp]
        public void SetUp()
        {
            // todo lo que esta dentro del SetUp se ejecutara antes de cualquier metodo
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3); // esto indica a selenium que cuando busque un elemento lo haga hasta por 3segundos
            
            driver.Url = "https://curso.testautomation.es"; // para navegar a la pagina web que vamos a testear
        }
       [TearDown]
        public void TearDown() 
        {
            driver.Quit();
        }

        [Test]
        public void TestBasicWebPage()
        {
            var normalLoadWeb = driver.FindElement(By.Id("NormalWeb"));        
            normalLoadWeb.Click();
            var titulo = driver.FindElement(By.CssSelector("h1")); // para obtener el elemento que tiene el tag h1
            titulo.Text.Should().Be("Normal load website"); // para obtener el valor texto

        }
        [Test]
        public void TestSlowLoadWebPage()
        {
            var sloLoadTextWeb = driver.FindElement(By.Id("SlowLoadWeb")); // para ubicar por id el elemento de la pagina
            sloLoadTextWeb.Click();
            var titulo = driver.FindElement(By.Id("title"));
            titulo.Text.Should().Be("Slow load website");

        }

        private void WaitForCondition(Func<bool> condition, int msTimeout = 4000)
        {
            // este codigo es muy util para controlar l
            var stopWatch = new Stopwatch(); // definimos una variable de tipo Stopwatch stopWatch.Start(); //iniciamos la variable.
            Exception? ex;
            do
            {
                try
                {
                    ex = null;
                    if (condition())
                    {
                        return;
                    }
                }
                catch (Exception e)
                {
                    ex = e;
                }
            } while (stopWatch.ElapsedMilliseconds < msTimeout);
            stopWatch.Stop();
            if (ex != null)
            {
                throw new TimeoutException("Error executing the condition", ex);
            }
            throw new TimeoutException("Error the condition was false", ex);// si la
  }

        private bool IsTextElemetn(IWebElement element, string expectedText)
        {
            return element.Text.Contains(expectedText);
        }

        [Test]
        public void TestSlowLoadTextWebPage()
        {
            var slowLoadTextWeb = driver.FindElement(By.Id("SlowSpeedTextWeb")); // para ubicar por el id el elemento de la pagina 
            slowLoadTextWeb.Click();
            var titulo = driver.FindElement(By.Id("title"));

            WaitForCondition(() => IsTextElemetn(titulo, "Slow load website")); // es una expresion lambda
        }
        
    }
}
