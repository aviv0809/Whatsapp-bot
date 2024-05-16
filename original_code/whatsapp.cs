using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using System.Xml;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using OpenQA.Selenium.Interactions;
using System.Text;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;

namespace iskaTest
{
    class whatsapp : Base
    {
        //Base b = new Base();

        public static void whatsAppProject(IWebDriver driver)
        {

            driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(120));
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(120));
            driver.Navigate().GoToUrl("https://web.whatsapp.com/");
            Thread.Sleep(20000);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("side")));


            //1
            //string path = @"C:\whatsapp\numbers.csv";
            //String[] allLines = System.IO.File.ReadAllLines(path, System.Text.ASCIIEncoding.Default);
            ////System.Text.Encoding.UTF8

            //Thread.Sleep(15000);

            //for (int i = 0; i < allLines.Length; i++)
            //{
            //    driver.FindElement(By.XPath("//*[@id='side']/div[1]/div/label/input")).Click();
            //    Thread.Sleep(1000);
            //    SendKeys.SendWait(allLines[i]);
            //    Thread.Sleep(6000);
            //    SendKeys.SendWait(@"{Enter}");
            //    Thread.Sleep(6000);
            //    SendKeys.SendWait("בדיקה");
            //    Thread.Sleep(6000);
            //    SendKeys.SendWait(@"{Enter}");
            //    Thread.Sleep(6000);
            //}

            //2
            //Thread.Sleep(30000);
            //string path = @"C:\whatsapp\contacts.csv";

            Thread.Sleep(3000);
            String[] allLines = System.IO.File.ReadAllLines(Path.GetFullPath(Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath), @"..\")) + @"\contacts.csv", System.Text.ASCIIEncoding.Default);
            
            for (int i = 0; i < allLines.Length; i++)
            {
                string[] contact = allLines[i].Split(',');

                driver.Navigate().GoToUrl("https://api.whatsapp.com/send?phone=972" + contact[0]);

                driver.FindElement(By.Id("action-button")).Click();

                Thread.Sleep(3000);
                //driver.FindElement(By.PartialLinkText("use WhatsApp Web")).Click()

                List<IWebElement> WhatsAppWeb = new List<IWebElement>();
                WhatsAppWeb = driver.FindElements(By.PartialLinkText("WhatsApp Web")).ToList<IWebElement>();

                WhatsAppWeb[1].Click();

            //driver.FindElement(By.XPath("//a[contains(@href,'https://web.whatsapp.com/send/?phone=')]/span")).Click();


            WebDriverWait await = new WebDriverWait(driver, TimeSpan.FromSeconds(50));
                await.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("main")));
                Thread.Sleep(5000);
                //IList<IWebElement> selectElement = driver.FindElements(By.ClassName("_2vbn4"));

                //driver.FindElement(By.ClassName("_2S1VP")).SendKeys("היי " + contact[1] + ", מה שלומך?");
                //driver.FindElement(By.ClassName("_2vbn4")).Click();

                string msg = contact[1];
                if (msg.Contains("[שם]"))
                {
                    msg = msg.Replace("[שם]", contact[2]);
                }
                if (msg.Contains("[נציג]"))
                {
                    msg = msg.Replace("[נציג]", contact[3]);
                }
                //if (msg.Contains("[זוכה]"))
                //{
                //    msg = msg.Replace("[זוכה]", contact[4]);
                //}
                //if (msg.Contains("[סכום]"))
                //{
                //    msg = msg.Replace("[סכום]", contact[5]);
                //}
                //if (msg.Contains("[תאריך]"))
                //{
                //    msg = msg.Replace("[תאריך]", contact[6]);
                //}


                //if (msg.Contains("[שם]"))
                //{
                //     msg = msg.Replace("[שם]", contact[2]);
                //}

                //if (msg.Contains("[לינק]"))
                //{
                //    msg = msg.Replace("[לינק]", contact[3]);
                //}

                //if (msg.Contains("[ארגון]"))
                //{
                //    msg = msg.Replace("[ארגון]", contact[4]);
                //}

                if (msg.Contains("[פסיק]"))
                {
                    msg = msg.Replace("[פסיק]", ",");
                }


                WebDriverWait awaitMessageLineClickable = new WebDriverWait(driver, TimeSpan.FromSeconds(50));
                await.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("_3Uu1_")));

                driver.FindElement(By.ClassName("_3Uu1_")).Click(); // focus the message line

                Console.WriteLine("First focus on the message line");

                Thread.Sleep(3000); //4

                //isAlertPresent(driver);

                driver.FindElement(By.ClassName("_3Uu1_")).SendKeys(msg); // enter the message
                //driver.FindElement(By.XPath("//div[@title='הקלדת ההודעה']")).SendKeys(msg);
                //driver.FindElement(By.XPath("//div[text()='הקלדת הודעה']")).SendKeys(msg);

                //IList<IWebElement> ifAelrt = driver.FindElements(By.XPath("//div[text()='אישור']"));
                //if (ifAelrt.Count > 0)
                //{
                //    driver.FindElement(By.XPath("//div[text()='אישור']")).Click();
                //    driver.FindElement(By.ClassName("p3_M1")).Click(); // focus the message line
                //    driver.FindElement(By.ClassName("p3_M1")).SendKeys(msg); // enter the message
                //}

                Thread.Sleep(2000);

                //Check to see if message entered the message line (1)
                if (driver.FindElement(By.ClassName("_3Uu1_")).Text.Equals(msg))
                {
                    Console.WriteLine("Message entered the message line succesfully " + i + " " + contact[0]);
                    //driver.FindElement(By.ClassName("tvf2evcx oq44ahr5 lb5m6g5c svlsagor p2rjqpw5 epia9gcq")).Click(); //send the message
                    driver.FindElement(By.XPath("//span[@data-icon='send']")).Click();
                }
                else
                {
                    Thread.Sleep(2000);

                    //driver.FindElement(By.ClassName("p3_M1")).SendKeys(msg); // reenter the message
                    //driver.FindElement(By.XPath("//div[@title='הקלדת ההודעה']")).SendKeys(msg);
                    driver.FindElement(By.XPath("//div[text()='הקלדת הודעה']")).SendKeys(msg);
                    //text() = ‘Get started free’ 
                    Thread.Sleep(1000);

                    //Check to see if message entered the message line (2)
                    if (driver.FindElement(By.ClassName("_3Uu1_")).Text.Equals(msg))
                    {
                        Console.WriteLine("Message entered the message line succesfully " + i + " "+ contact[0]);
                        //driver.FindElement(By.ClassName("tvf2evcx oq44ahr5 lb5m6g5c svlsagor p2rjqpw5 epia9gcq")).Click(); //send the message
                        driver.FindElement(By.XPath("//span[@data-icon='send']")).Click();
                    }
                    else
                    {
                        Console.WriteLine("Message is not in the message line");
                    }

                }


                Thread.Sleep(5000);

                //selectElement[1].Click();
                //selectElement[1].SendKeys(msg);

                
                ////Thread.Sleep(3000);
                //SendKeys.SendWait("היי " + contact[1] + ", מה שלומך? " + i);
                //+ contact[1] for name
                //Thread.Sleep(3000);
                //SendKeys.SendWait(@"{Enter}");
                //Thread.Sleep(3000);
            }
        }
    }
}
