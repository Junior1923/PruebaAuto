using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;
using System.IO;
using System.Threading;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

class Program
{
    static void Main(string[] args)
    {
        // Configurar el reporte ExtentReports
        var extent = new ExtentReports();
        var htmlReporter = new ExtentHtmlReporter("C:\\Users\\junio\\OneDrive\\Datos adjuntos\\Documentos\\6CUATRIMESTRE\\Prog3\\Tarea 4");
        extent.AttachReporter(htmlReporter);

        IWebDriver driver = new EdgeDriver(); // Inicializar el navegador Edge

        try
        {
            // Navegar a la página de inicio de sesión de Roblox
            driver.Navigate().GoToUrl("https://www.roblox.com/login?returnUrl=https%3A%2F%2Fwww.roblox.com%2Fdiscover%23%2F");

            // Localizar los elementos de entrada y enviar las credenciales
            IWebElement usernameInput = driver.FindElement(By.Id("login-username"));
            usernameInput.SendKeys("Necros1923");

            IWebElement passwordInput = driver.FindElement(By.Id("login-password"));
            passwordInput.SendKeys("Miguelina1923");

            // Capturar una captura de pantalla después de ingresar las credenciales
            string screenshotPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Screenshot_Login.png");
            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(screenshotPath);

            // Hacer clic en el botón de inicio de sesión
            IWebElement loginButton = driver.FindElement(By.Id("login-button"));
            loginButton.Click();

            // Esperar unos segundos para ver el resultado antes de cerrar el navegador
            Thread.Sleep(5000);

            // Agregar una descripción a la prueba en el informe
            var test = extent.CreateTest("Inicio de Sesión", "Prueba de inicio de sesión exitosa en Roblox")
                             .Info("Se completó el proceso de inicio de sesión.")
                             .AddScreenCaptureFromPath(screenshotPath); // Adjuntar captura de pantalla

            // Marcar la prueba como exitosa en el informe
            test.Pass("Inicio de sesión exitoso");
        }
        catch (Exception ex)
        {
            // Capturar una captura de pantalla en caso de error
            string screenshotPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Screenshot_Error.png");
            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(screenshotPath);

            // Agregar una descripción a la prueba en el informe
            var test = extent.CreateTest("Inicio de Sesión", "Prueba de inicio de sesión exitosa en Roblox")
                             .Info("Ocurrió un error durante el proceso de inicio de sesión.")
                             .AddScreenCaptureFromPath(screenshotPath); // captura de pantalla

            // Marcar la prueba como fallida en el informe y registrar el error
            test.Fail(ex.Message);
        }
        finally
        {
            // Cerrar el navegador
            driver.Quit();

            // Finalizar el informe
            extent.Flush();
        }
    }
}



