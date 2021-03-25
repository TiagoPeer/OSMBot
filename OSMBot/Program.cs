using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Linq;

namespace OSMBot
{
    class Program
    {
        static IWebDriver driver;
        static void Main(string[] args)
        {

            var random = new Random();

            ChromeOptions options = new ChromeOptions();
            //options.AddArgument("--headless");
            options.AddArgument("--window-size=1920,1080");
            options.AddArgument("--log-level=3");
            driver = new ChromeDriver(options);

            login();

            //login
            void login()
            {
                try
                {
                    Console.WriteLine("--------------------");
                    Console.WriteLine("A navegar para OSM");
                    driver.Navigate().GoToUrl("https://en.onlinesoccermanager.com/privacynotice?nexturl=%2F");
                    System.Threading.Thread.Sleep(random.Next(2000, 3000));
                    //Privacy Page
                    driver.FindElements(By.TagName("i")).FirstOrDefault().Click();
                    System.Threading.Thread.Sleep(random.Next(500, 1000));
                    driver.FindElement(By.TagName("button")).Click();
                    System.Threading.Thread.Sleep(random.Next(3000, 4000));
                    //Login Page
                    var loginContainer = driver.FindElement(By.ClassName("register-information-container"));
                    System.Threading.Thread.Sleep(random.Next(1000, 1500));
                    loginContainer.FindElement(By.ClassName("btn-alternative")).Click();
                    Console.WriteLine("--------------------");
                    Console.WriteLine("A fazer login");
                    System.Threading.Thread.Sleep(random.Next(500, 1000));
                    driver.FindElement(By.Id("manager-name")).Click();
                    driver.FindElement(By.Id("manager-name")).Clear();
                    driver.FindElement(By.Id("manager-name")).SendKeys("TiagoPerr");
                    driver.FindElement(By.Id("password")).Click();
                    driver.FindElement(By.Id("password")).Clear();
                    driver.FindElement(By.Id("password")).SendKeys("sar88tnytx");
                    System.Threading.Thread.Sleep(random.Next(500, 1000));
                    driver.FindElement(By.ClassName("btn-new")).Click();
                    Console.WriteLine("Login Feito com sucesso");
                    Console.WriteLine("--------------------");
                    verQuantidadeDeMoedas();
                    System.Threading.Thread.Sleep(random.Next(3000, 4000));
                    verAnuncio();
                }
                catch (Exception e)
                {
                    Console.WriteLine("--------------------");
                    Console.WriteLine(e);
                    Console.WriteLine("Erro nao foi possivel fazer login, tentando de novo");
                    Console.WriteLine("--------------------");
                }
            }

            void verAnuncio()
            {
                try
                {
                    Console.WriteLine("Abrindo Anuncios");
                    Console.WriteLine("--------------------------");
                    driver.Navigate().GoToUrl("https://en.onlinesoccermanager.com/Career");
                    System.Threading.Thread.Sleep(random.Next(1000, 3000));
                    driver.FindElements(By.ClassName("wallet-container"))[0].Click();
                    System.Threading.Thread.Sleep(random.Next(3000, 5000));
                    driver.FindElement(By.Id("product-category-free")).FindElements(By.ClassName("product-free"))[0].Click();
                    System.Threading.Thread.Sleep(random.Next(3000, 5000));
                    try
                    {
                        //var caixaSemAnuncios = driver.FindElement(By.XPath("//div[@id='modal-dialog-alert']/div[4]/div/div/div/div[2]/div/div/p"));
                        var modalSemAnuncios = driver.FindElement(By.Id("modal-dialog-alert"));
                        driver.FindElement(By.ClassName("btn")).Click();
                        //var btnSemAnuncios = driver.FindElement(By.XPath("//div[@type='button']"));

                        //btnSemAnuncios.Click();
                        Console.WriteLine("Sem mais anuncios para ver");
                        Console.WriteLine("--------------------------");
                        Console.WriteLine("Esperando 1 hora");
                        Console.WriteLine("--------------------------");
                        Console.WriteLine("Proximo premio disponivel as : {0}", DateTime.Now.AddHours(1));

                        for (int i = 6; i >= 1; i--)
                        {
                            var tempoEspera = 600000;
                            Console.WriteLine("Esperando {0} minutos as {1}", i * 10, DateTime.Now);
                            System.Threading.Thread.Sleep(tempoEspera);
                        }
                        verAnuncio();
                    }
                    catch
                    {
                        Console.WriteLine("A ver anuncio");
                        Console.WriteLine("--------------------------");
                        //document.getElementsByTagName("iframe")[2]
                        System.Threading.Thread.Sleep(1000 * 40);
                        Console.WriteLine("A ver proximo Anuncio");
                        Console.WriteLine("--------------------------");
                        verAnuncio();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro, ver ficheiro Log");
                    ErrorLogging(e);
                    verAnuncio();
                }
            }

            void verQuantidadeDeMoedas()
            {
                try
                {
                    System.Threading.Thread.Sleep(random.Next(2500, 3500));
                    var moedas = driver.FindElement(By.ClassName("wallet-amount")).FindElement(By.TagName("span")).Text;
                    Console.WriteLine("Quantidade de moedas : " + moedas);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Impossivel ver as moedas");
                    ErrorLogging(e);
                }
            }
           
        }
        public static void ErrorLogging(Exception ex)
        {
            string strPath = @"C:\Users\tiago\Desktop\LogErrosBotOsm.txt";
            if (!File.Exists(strPath))
            {
                File.Create(strPath).Dispose();
            }
            using (StreamWriter sw = File.AppendText(strPath))
            {
                Console.WriteLine("Erro reportado");
                sw.WriteLine("=============Error Logging ===========");
                sw.WriteLine("===========Start============= " + DateTime.Now);
                sw.WriteLine("Error Message: " + ex.Message);
                sw.WriteLine("Stack Trace: " + ex.StackTrace);
                sw.WriteLine("===========End============= " + DateTime.Now);
            }
        }

        public static bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}
