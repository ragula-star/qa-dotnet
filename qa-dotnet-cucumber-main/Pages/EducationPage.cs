using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace qa_dotnet_cucumber.Pages
{
    public class EducationPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public EducationPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        private readonly By EducationTab = By.XPath("//a[text()='Education']");
        private readonly By AddNewBtn = By.XPath("/html/body/div[1]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/thead/tr/th[6]/div");
        private readonly By UniversityName = By.XPath("//input[@placeholder='College/University Name']");
        private readonly By Countrycollege = By.Name("country");
        private readonly By Title = By.Name("title");
        private readonly By Degree = By.XPath("//input[@placeholder='Degree']");
        private readonly By YearGraduation = By.Name("yearOfGraduation");
        private readonly By AddEducationBtn = By.XPath("//input[@type='button' and @value='Add']");
        private readonly By DeleteEducationIcons = By.XPath("/div[@data-tab='third']//i[contains(@class,'remove icon')]");
        private readonly By DuplicatePopup = By.XPath("//div[contains(@class,'ns-box') or contains(@class,'toast') or contains(text(),'already exists')]");

        public void GoToEducationtab()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(EducationTab)).Click();
            _wait.Until(d => d.FindElement(AddNewBtn).Displayed); 
        }

        private void TryClickAddNew()
        {
            try
            {
                _wait.Until(ExpectedConditions.ElementToBeClickable(AddNewBtn)).Click();
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("[WARN] Add New button not clickable, retrying...");
                _driver.Navigate().Refresh();
                _wait.Until(ExpectedConditions.ElementToBeClickable(AddNewBtn)).Click();
            }
        }

        public void AddEducation(string university, string countrycollege, string title, string degree, string yeargraduation)
        {
            TryClickAddNew();
            _wait.Until(ExpectedConditions.ElementIsVisible(UniversityName));

            _driver.FindElement(UniversityName).Clear();
            _driver.FindElement(UniversityName).SendKeys(university ?? "");

           
            if (!string.IsNullOrEmpty(countrycollege))
            {
                var select = new SelectElement(_driver.FindElement(Countrycollege));
                var option = select.Options.FirstOrDefault(o => o.Text == countrycollege);
                if (option != null)
                    select.SelectByText(countrycollege);
                else
                    Console.WriteLine($"[INFO] Skipping invalid country value: {countrycollege}");
            }

           
            if (!string.IsNullOrEmpty(title))
            {
                var select = new SelectElement(_driver.FindElement(Title));
                var option = select.Options.FirstOrDefault(o => o.Text == title);
                if (option != null)
                    select.SelectByText(title);
                else
                    Console.WriteLine($"[INFO] Skipping invalid title value: {title}");
            }

            _driver.FindElement(Degree).Clear();
            _driver.FindElement(Degree).SendKeys(degree ?? "");

            
            if (!string.IsNullOrEmpty(yeargraduation))
            {
                var select = new SelectElement(_driver.FindElement(YearGraduation));
                var option = select.Options.FirstOrDefault(o => o.Text == yeargraduation);
                if (option != null)
                    select.SelectByText(yeargraduation);
                else
                    Console.WriteLine($"[INFO] Skipping invalid year value: {yeargraduation}");
            }

            _driver.FindElement(AddEducationBtn).Click();
        }

        public void DeleteAllEducation()
        {
            By rowLocator = By.XPath("//div[@data-tab='third']//table/tbody/tr");
            By deleteLocator = By.XPath(".//i[contains(@class,'remove icon')]");

            while (_driver.FindElements(rowLocator).Count > 0)
            {
                var rows = _driver.FindElements(rowLocator);
                int initialCount = rows.Count;

                try
                {
                    var firstRow = rows[0];
                    _wait.Until(ExpectedConditions.ElementToBeClickable(firstRow.FindElement(deleteLocator))).Click();
                }
                catch (StaleElementReferenceException)
                {
                    continue;
                }

                _wait.Until(d => d.FindElements(rowLocator).Count < initialCount);
            }
        }

        public List<string> GetAllEducation()
        {
            var entries = new List<string>();
            var rows = _driver.FindElements(By.XPath("//div[@data-tab='third']//table//tr"));
            foreach (var row in rows)
            {
                try
                {
                    var university = row.FindElements(By.TagName("td")).FirstOrDefault()?.Text;
                    if (!string.IsNullOrEmpty(university))
                        entries.Add(university);
                }
                catch
                {
                    continue;
                }
            }
            return entries;
        }
        public void EditLastEducation(string university, string country, string title, string degree, string year)
        {
            
            var rows = _driver.FindElements(By.XPath("//div[@data-tab='third']//table//tbody/tr"));
            if (!rows.Any()) return;
            var lastRow = rows.Last();
            lastRow.FindElement(By.XPath(".//i[contains(@class,'outline write icon')]")).Click();

            AddEducation(university, country, title, degree, year);
        }


        public string GetDuplicateMessage()
        {
            try
            {
                return _wait.Until(ExpectedConditions.ElementIsVisible(DuplicatePopup)).Text.Trim();
            }
            catch
            {
                return string.Empty;
            }
        }
        public string AssertDuplicateMessage()
        {
            var message = GetDuplicateMessage();

            if (string.IsNullOrEmpty(message))
                return "No duplicate message found.";

            return message.Trim();
        }


        public string GetErrorMessage()
        {
            try
            {
                var errorLocator = By.XPath("//div[contains(@class,'toast') or contains(@class,'ui message')]");
                return _wait.Until(ExpectedConditions.ElementIsVisible(errorLocator)).Text.Trim();
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("No error message appeared for empty fields.");
                return string.Empty;
            }
        }
    }
}
