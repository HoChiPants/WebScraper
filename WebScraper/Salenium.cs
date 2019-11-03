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
            }
            catch
            {
                _driver.Quit();
                return null;
            }
            coursepath.Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"__KUALI_TLP\"]/div/div/div[4]/div/div")));
            string description = _driver.FindElement(By.XPath("//*[@id=\"__KUALI_TLP\"]/div/div/div[4]/div/div")).Text;
            _driver.Quit();
            return description;

        }


        //This is to get the credits for the classes
        private List<string> getCreditsAndDesc(string course)
        {
            IWebDriver _drivertemp = new ChromeDriver();
            WebDriverWait waittemp = new WebDriverWait(_drivertemp, TimeSpan.FromSeconds(9));
            List<string> answer = new List<string>();
            _drivertemp.Navigate().GoToUrl("https://catalog.utah.edu");
            waittemp.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"top\"]/div/div[3]/div/nav/ul/li[3]")));
            var coursepath = _drivertemp.FindElement(By.XPath("//*[@id=\"top\"]/div/div[3]/div/nav/ul/li[3]"));
            coursepath.Click();
            coursepath = _drivertemp.FindElement(By.Id("Search"));
            coursepath.SendKeys(course);
            waittemp.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"__KUALI_TLP\"]/div/table/tbody/tr/th/a")));
            coursepath = _drivertemp.FindElement(By.XPath("//*[@id=\"__KUALI_TLP\"]/div/table/tbody/tr/th/a"));
            coursepath.Click();
            waittemp.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"__KUALI_TLP\"]/div/div/div[2]/div/div/div")));
            coursepath = _drivertemp.FindElement(By.XPath("//*[@id=\"__KUALI_TLP\"]/div/div/div[2]/div/div/div"));
            answer.Add(coursepath.Text);
            coursepath = _drivertemp.FindElement(By.XPath("//*[@id=\"__KUALI_TLP\"]/div/div/div[4]/div/div"));
            answer.Add(coursepath.Text);
            _drivertemp.Quit();
            return answer;
        }


        //This is for finding all the enrollments and returning a dictionary of the classes with their information.
        public Dictionary<int,List<string>> getEnrollments(string url,string year, int semester, string count)
        {
            if(count == null || count == "")
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
            int totalcounter = 1;

            foreach (var row in rows)
            {
                var listrow = row.FindElements(By.TagName("td"));
                if(listrow.ElementAt(3).Text != "001" || Int32.Parse(listrow.ElementAt(0).Text) < 1000 || Int32.Parse(listrow.ElementAt(0).Text) > 7000 || listrow.ElementAt(4).Text.Contains("Seminar") || listrow.ElementAt(4).Text.Contains("Special Topics")) 
                {
                    continue;
                }
                else
                {
                    string course = listrow.ElementAt(1).Text;
                    course += listrow.ElementAt(2).Text;
                    string dep = listrow.ElementAt(1).Text;
                    string coursenum = listrow.ElementAt(2).Text;
                    string title = listrow.ElementAt(4).Text;
                    string courseenrolment = listrow.ElementAt(7).Text;
                    List<string> credits = getCreditsAndDesc(course);

                    finalValues.Add(counter++, new List<string>()
                        {
                        dep,
                        coursenum,
                        credits[0],
                        title,
                        courseenrolment,
                        semester.ToString(),
                        year,
                        credits[1]
                        }) ;

                }
                if(totalcounter == Int32.Parse(count))
                {
                    _driver.Quit();
                    return finalValues;
                }
                else
                {
                    totalcounter++;
                }
            }
            _driver.Quit();
            return finalValues;
            
        }
    }
}
