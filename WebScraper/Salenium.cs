using Nest;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraper
{
    class Salenium
    {
        private IWebDriver _driver;
        private WebDriverWait wait;

        public Salenium()
        {
            _driver = new ChromeDriver();
            wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(9));
        }

        //This is to get the description of a certain class and return it a string
        public string getDescription(string course)
        {
            try
            {
                _driver.Navigate().GoToUrl("https://catalog.utah.edu");
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"top\"]/div/div[3]/div/nav/ul/li[3]")));
                var coursepath = _driver.FindElement(By.XPath("//*[@id=\"top\"]/div/div[3]/div/nav/ul/li[3]"));
                coursepath.Click();
                coursepath = _driver.FindElement(By.Id("Search"));
                coursepath.SendKeys(course);
                //check here to see if the course name is the same as the link
                try
                {
                    wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"__KUALI_TLP\"]/div/table/tbody/tr/th/a")));
                    coursepath = _driver.FindElement(By.XPath("//*[@id=\"__KUALI_TLP\"]/div/table/tbody/tr/th/a"));
                    if(coursepath.Text != course)
                    {
                        _driver.Quit();
                        return null;
                    }
                }
                catch
                {
                    try
                    {
                        coursepath = _driver.FindElement(By.XPath("//*[@id=\"kuali-catalog-main\"]/div/div[1]/div[1]/button/i/i"));
                        coursepath.Click();
                        coursepath = _driver.FindElement(By.Id("Search"));
                        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
                        coursepath.SendKeys(course);
                        wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"__KUALI_TLP\"]/div/table/tbody/tr/th/a")));
                        coursepath = _driver.FindElement(By.XPath("//*[@id=\"__KUALI_TLP\"]/div/table/tbody/tr/th/a"));
                    }
                    catch
                    {
                        _driver.Quit();
                        return null;
                    }

                }
                coursepath.Click();
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"__KUALI_TLP\"]/div/div/div[4]/div/div")));
                try
                {
                    coursepath = _driver.FindElement(By.XPath("//*[@id=\"__KUALI_TLP\"]/div/div/div[6]/div/div"));

                    if (coursepath.Text.Length < 50)
                    {
                        coursepath = _driver.FindElement(By.XPath("//*[@id=\"__KUALI_TLP\"]/div/div/div[4]/div/div"));
                    }
                }
                catch
                {
                    coursepath = _driver.FindElement(By.XPath("//*[@id=\"__KUALI_TLP\"]/div/div/div[4]/div/div"));
                }
                string description = coursepath.Text;
                _driver.Quit();
                return description;
            }
            catch
            {
                _driver.Quit();
                return null;
            }

        }


        //This is to get the credits for the classes
        private List<string> getCreditsAndDesc(string course)
        {
            try
            {
                List<string> answer = new List<string>();
                _driver.Navigate().GoToUrl("https://catalog.utah.edu");
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"top\"]/div/div[3]/div/nav/ul/li[3]")));
                var coursepath = _driver.FindElement(By.XPath("//*[@id=\"top\"]/div/div[3]/div/nav/ul/li[3]"));
                coursepath.Click();
                coursepath = _driver.FindElement(By.Id("Search"));
                coursepath.SendKeys(course);
                try
                {
                    wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"__KUALI_TLP\"]/div/table/tbody/tr/th/a")));
                    coursepath = _driver.FindElement(By.XPath("//*[@id=\"__KUALI_TLP\"]/div/table/tbody/tr/th/a"));
                }
                catch
                {
                    try
                    {
                        coursepath = _driver.FindElement(By.XPath("//*[@id=\"kuali-catalog-main\"]/div/div[1]/div[1]/button/i/i"));
                        coursepath.Click();
                        coursepath = _driver.FindElement(By.Id("Search"));
                        coursepath.SendKeys(course);
                        wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"__KUALI_TLP\"]/div/table/tbody/tr/th/a")));
                        coursepath = _driver.FindElement(By.XPath("//*[@id=\"__KUALI_TLP\"]/div/table/tbody/tr/th/a"));
                    }
                    catch
                    {
                        return null;
                    }
                }
                coursepath.Click();
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"__KUALI_TLP\"]/div/div/div[2]/div/div/div")));
                coursepath = _driver.FindElement(By.XPath("//*[@id=\"__KUALI_TLP\"]/div/div/div[2]/div/div/div"));
                answer.Add(coursepath.Text);
                try
                {
                    coursepath = _driver.FindElement(By.XPath("//*[@id=\"__KUALI_TLP\"]/div/div/div[6]/div/div"));

                    if (coursepath.Text.Length < 50)
                    {
                        coursepath = _driver.FindElement(By.XPath("//*[@id=\"__KUALI_TLP\"]/div/div/div[4]/div/div"));
                    }
                }
                catch
                {
                    coursepath = _driver.FindElement(By.XPath("//*[@id=\"__KUALI_TLP\"]/div/div/div[4]/div/div"));
                }
                answer.Add(coursepath.Text);
                return answer;
            }
            catch
            {
                return null;
            }
        }


        //This is for finding all the enrollments and returning a dictionary of the classes with their information.
        public Dictionary<int,List<string>> getEnrollments(string url,string year, int semester, string count)
        {
            try
            {
                if (count == null || count == "")
                {
                    count = "100";
                }
                _driver.Navigate().GoToUrl(url);
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/section/main/div[2]/a")));
                var element = _driver.FindElement(By.XPath("/html/body/section/main/div[2]/a"));
                element.Click();
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"seatingAvailabilityTable\"]/tbody")));
                element = _driver.FindElement(By.XPath("//*[@id=\"seatingAvailabilityTable\"]/tbody"));
                var rows = element.FindElements(By.TagName("tr"));
                Dictionary<int, List<string>> finalValues = new Dictionary<int, List<string>>();
                int counter = 0;
                int totalcounter = 0;

                foreach (var row in rows)
                {
                    var listrow = row.FindElements(By.TagName("td"));
                    if (listrow.ElementAt(3).Text != "001" || Int32.Parse(listrow.ElementAt(2).Text) < 1000 || Int32.Parse(listrow.ElementAt(2).Text) > 7000 || listrow.ElementAt(4).Text.Contains("Seminar") || listrow.ElementAt(4).Text.Contains("Special Topics"))
                    {
                        continue;
                    }
                    else
                    {
                        finalValues.Add(counter++, new List<string>()
                        {
                        listrow.ElementAt(1).Text,
                        listrow.ElementAt(2).Text,
                        listrow.ElementAt(4).Text,
                        listrow.ElementAt(7).Text,
                        semester.ToString(),
                        year,
                        });
                        totalcounter++;
                    }
                    if (totalcounter == Int32.Parse(count))
                    {
                        break;
                    }
                }

                foreach (var w in finalValues.Keys)
                {
                    string course = finalValues[w][0];
                    course += finalValues[w][1];
                    List<string> templist = getCreditsAndDesc(course);
                    if (templist == null)
                    {
                        _driver.Quit();
                        return null;
                    }
                    finalValues[w].Add(templist[0]);
                    finalValues[w].Add(templist[1]);
                }
                _driver.Quit();
                return finalValues;
            }
            catch
            {
                _driver.Quit();
                return null;
            }
            
        }
    }
}
