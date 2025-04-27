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

            driver.FindElement(By.ClassName("className")).Text.Should().Be("Element 2");

            //opcion de la lista para el tercer elemeento el Findelments
            driver.FindElements(By.Id("myId"))[1].Text.Should().Be("Element 3");
            //opcion buscarlo dentro de la seccion div name elements
            var elementsSecction = driver.FindElement(By.Name("elements")); // Filtra la seccion
            elementsSecction.FindElement(By.Id("myId")).Text.Should().Be("Element 3"); //dentro de la sccion filtrada busca por id

            //opcion usando directamente el selector de CSS
            driver.FindElement(By.CssSelector("[name='elements'] #myId")).Text.Should().Be("Element 3");

            //para el Element 4, lo buscaremos por nombre
            driver.FindElement(By.Name("myName")).Text.Should().Be("Element 4");

            //para el Element 5, por CSS
            driver.FindElement(By.CssSelector("div [style='color:magenta']")).Text.Should().Be("Element 5");

            //para el Element 5 usar XPath: esto es arriesgado ya que depende de como se escriba el texto
            driver.FindElement(By.XPath("//* [contains(text(), 'Element 5')]")).Text.Should().Be("Element 5");


            //para ubicar el Element 6 por CSS
            driver.FindElement(By.CssSelector("[autotestid='Element6']")).Text.Should().Be("Element 6");

            //para ubicar Element 7
            var divElementsSection = driver.FindElements(By.CssSelector("[name='elements'] div")); // para obtener la lista de div
            divElementsSection[5].Text.Should().Be("Element 7"); // es 5 pues la lista inicia con 0
            divElementsSection[6].Text.Should().Be("Element 8");

            //para ubicar el Home1 y Home2 OPCION A
            var homeButtons = driver.FindElements(By.CssSelector("[name=\"refs\"] div > a")); // para obtener la lista HOME
            homeButtons[0].Text.Should().Be("Home1");
            homeButtons[1].Text.Should().Be("Home2");

            //para ubicar el Home1 y Home2 OPCION B
            homeButtons = driver.FindElements(By.PartialLinkText("Home")); // para obtener la lista home, obtiene 3
            homeButtons[1].Text.Should().Be("Home1");
            homeButtons[2].Text.Should().Be("Home2");

            //para ubicar el Home1 y Home2 OPCION C
            var rfsSection = driver.FindElement(By.Name("refs"));
            homeButtons = rfsSection.FindElements(By.PartialLinkText("Home"));
            homeButtons[0].Text.Should().Be("Home1");
            homeButtons[1].Text.Should().Be("Home2");


            // para ubicar los botones "Click me 1" y "Click me 2"
            var home1 = homeButtons[0];
            var button2 = driver.FindElement(RelativeBy.WithLocator(By.CssSelector("button")).RightOf(home1));
            button2.Text.Should().Be("Click me 2");


            //PARA obtener el valor de la tabla de usuarios inactivos Sandra
            var interativeTable = driver.FindElements(By.ClassName("styled-table"))[1]; //Nos retorna la tabla de inactivos, toma el 2do
            var inactiveUser = interativeTable.FindElements(By.CssSelector("tbody tr")); // Nos retorna 2 elementos
            Console.WriteLine(inactiveUser[1].Text);

        }
    }
}
