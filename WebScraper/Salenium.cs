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
//Author Austin Stephens

namespace WebScraper
{
    class Salenium
    {
        private IWebDriver _driver;
        private WebDriverWait wait;

        public Salenium()
        {
            _driver = new ChromeDriver();
            wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
        }

        //This is to get the credits and description. The first parameter is the course name, the second is if you only want the description or both. This is because this method is called by another and the driver needs to stay up
        public List<string> getCreditsAndDesc(string course, bool once)
        {
            try
            {
                //make the variable to return and navigate to the webpage
                List<string> answer = new List<string>();
                _driver.Navigate().GoToUrl("https://catalog.utah.edu");
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"top\"]/div/div[3]/div/nav/ul/li[3]")));
                var coursepath = _driver.FindElement(By.XPath("//*[@id=\"top\"]/div/div[3]/div/nav/ul/li[3]"));
                coursepath.Click();
                coursepath = _driver.FindElement(By.Id("Search"));
                coursepath.SendKeys(course);
                try
                {
                    //try to send the keys to search for the class
                    wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"__KUALI_TLP\"]/div/table/tbody/tr/th/a")));
                    coursepath = _driver.FindElement(By.XPath("//*[@id=\"__KUALI_TLP\"]/div/table/tbody/tr/th/a"));
                    //check to see if the course path is the correct one.
                    if (coursepath.Text != course)
                    {
                        CheckRun(once);
                        return null;
                    }
                }
                catch
                {
                    try
                    {
                        //sometimes it populates too quick so delete the input and reinput the course
                        coursepath = _driver.FindElement(By.XPath("//*[@id=\"kuali-catalog-main\"]/div/div[1]/div[1]/button/i/i"));
                        coursepath.Click();
                        coursepath = _driver.FindElement(By.Id("Search"));
                        coursepath.SendKeys(course);
                        wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"__KUALI_TLP\"]/div/table/tbody/tr/th/a")));
                        coursepath = _driver.FindElement(By.XPath("//*[@id=\"__KUALI_TLP\"]/div/table/tbody/tr/th/a"));
                        //check to see if the course path is the correct one.
                        if (coursepath.Text != course)
                        {
                            CheckRun(once);
                            return null;
                        }
                    }
                    catch
                    {
                        CheckRun(once);
                        return null;
                    }
                }
                //click on the course
                coursepath.Click();
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"__KUALI_TLP\"]/div/div/div[2]/div/div/div")));
                coursepath = _driver.FindElement(By.XPath("//*[@id=\"__KUALI_TLP\"]/div/div/div[2]/div/div/div"));
                //add the course credits to the array
                answer.Add(coursepath.Text);
                //sometimes the div where the description is changes so first check 6 then check 4. If 6 is less than 50 characters go to 4.
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
                CheckRun(once);
                return answer;
            }
            //catch all errors and return null after possibly quitting the driver
            catch
            {
                CheckRun(once);
                return null;
                
            }
        }


        //This is for finding all the enrollments and returning a dictionary of the classes with their information.
        public Dictionary<int,List<string>> getEnrollments(string url,string year, int semester, string count)
        {
            try
            {
                //error checking on the input and to set the count to 100 if not previously set
                if (count == null || count == "")
                {
                    count = "100";
                }
                //go to the url and click on the link at the bottom for the seating availablity
                _driver.Navigate().GoToUrl(url);
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/section/main/div[2]/a")));
                var element = _driver.FindElement(By.XPath("/html/body/section/main/div[2]/a"));
                element.Click();
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"seatingAvailabilityTable\"]/tbody")));
                element = _driver.FindElement(By.XPath("//*[@id=\"seatingAvailabilityTable\"]/tbody"));
                //get all the data from the webpage
                var rows = element.FindElements(By.TagName("tr"));
                //make a dictionary to store the values
                Dictionary<int, List<string>> finalValues = new Dictionary<int, List<string>>();
                int counter = 0;
                int totalcounter = 0;

                foreach (var row in rows)
                {
                    var listrow = row.FindElements(By.TagName("td"));
                    //make sure the data is what want as per the assignment documentation
                    if (listrow.ElementAt(3).Text != "001" || Int32.Parse(listrow.ElementAt(2).Text) < 1000 || Int32.Parse(listrow.ElementAt(2).Text) > 7000 || listrow.ElementAt(4).Text.Contains("Seminar") || listrow.ElementAt(4).Text.Contains("Special Topics"))
                    {
                        continue;
                    }
                    else
                    {
                        //set all the values but the description and the course credits in the dictionary. They will be added later
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
                //for all the values in the dicitonary, get the course name and search for the description and credits on a different website
                foreach (var w in finalValues.Keys)
                {
                    string course = finalValues[w][0];
                    course += finalValues[w][1];
                    List<string> templist = getCreditsAndDesc(course,false);
                    if (templist == null)
                    {
                        _driver.Quit();
                        return null;
                    }
                    finalValues[w].Add(templist[0]);
                    finalValues[w].Add(templist[1]);
                }
                //quit the driver and return the dictionary
                _driver.Quit();
                return finalValues;
            }
            //if anything fails do this
            catch
            {
                _driver.Quit();
                return null;
            }
            
        }
        private void CheckRun(bool once)
        {
            if (once)
            {
                _driver.Quit();
            }
        }
    }
}
