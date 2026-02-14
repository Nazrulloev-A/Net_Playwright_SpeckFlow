using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using Reqnroll.BoDi;
using Reqnroll.Tracing;
using System.Reflection;
using TechTalk.SpecFlow;
using WebOrders_PW.PageObjects;
using Allure.Net.Commons;

namespace DemoFramewrok.Hooks
{
    [Binding]
    public class TestHooks
    {
        private readonly BasePage _basePage;
        private Fixtures.WebDriverFixture webDriverFixture;

        public TestHooks(BasePage basePage)
        {
            _basePage = basePage;
        }

        [BeforeScenario]
        public async Task BeforeScenario(IObjectContainer container, ScenarioContext context)
        {
            Console.WriteLine("****** Test Hook - BeforeScenario - Started - " + context.ScenarioInfo.Title);

            if (_basePage.Config is null)
            {
                _basePage.Config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
                    .AddUserSecrets(typeof(TestHooks).GetTypeInfo().Assembly)
                    .AddEnvironmentVariables()
                    .Build();
            }

            container.RegisterInstanceAs(_basePage.Config);

            webDriverFixture = new Fixtures.WebDriverFixture(_basePage.Config);
            await webDriverFixture.InitializeAsync();
            _basePage.Browser = webDriverFixture.Browser;

            _basePage.BrowserContext = await _basePage.Browser.NewContextAsync(new BrowserNewContextOptions
            {
                ViewportSize = new ViewportSize { Width = 1920, Height = 1080 }
            });

            _basePage.Page = await _basePage.BrowserContext.NewPageAsync();
            _basePage.Page.SetDefaultTimeout(60000);

            container.RegisterInstanceAs(_basePage.Page);
            container.RegisterInstanceAs(_basePage.Browser);

            Console.WriteLine("****** Test Hook - BeforeScenario - End");
        }

        //[AfterScenario]
        //public async Task AfterScenario(IObjectContainer container, ScenarioContext context)
        //{
        //    Console.WriteLine("****** Test Hook - AfterScenario - Started - " + context.ScenarioInfo.Title);

        //    if (context.TestError != null)
        //    {
        //        var resultsDir = "allure-results";
        //        var screenshotFileName = $"{resultsDir}/Fail_{context.ScenarioInfo.Title.ToIdentifier()}.png";

        //        if (!System.IO.Directory.Exists(resultsDir))
        //        {
        //            System.IO.Directory.CreateDirectory(resultsDir);
        //        }

        //        await _basePage.Page.ScreenshotAsync(new PageScreenshotOptions
        //        {
        //            Path = screenshotFileName,
        //            FullPage = true,
        //        });

        //        Console.WriteLine($"Screenshot saved: {screenshotFileName}");
        //    }

        //    await _basePage.Browser.CloseAsync();
        //    await _basePage.Browser.DisposeAsync();

        //    Console.WriteLine("****** Test Hook - AfterScenario - End");
        //}

        [AfterScenario]
        public async Task AfterScenario(IObjectContainer container, ScenarioContext context)
        {
            Console.WriteLine("****** Test Hook - AfterScenario - Started - " + context.ScenarioInfo.Title);

            try
            {
                if (context.TestError != null && _basePage?.Page != null)
                {
                    var resultsDir = "allure-results";
                    Directory.CreateDirectory(resultsDir);

                    var safeScenarioName = context.ScenarioInfo.Title
                        .Replace(" ", "_")
                        .Replace(":", "")
                        .Replace("/", "");

                    var screenshotPath = Path.Combine(
                        resultsDir,
                        $"Fail_{safeScenarioName}_{DateTime.Now:yyyyMMdd_HHmmss}.png"
                    );

                    var screenshotBytes = await _basePage.Page.ScreenshotAsync(new PageScreenshotOptions
                    {
                        Path = screenshotPath,
                        FullPage = true
                    });

                    // Attach to Allure
                    AllureApi.AddAttachment(
                        "Failure Screenshot",
                        "image/png",
                        screenshotBytes
                    );

                    Console.WriteLine($"Screenshot captured and attached: {screenshotPath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Screenshot capture failed: {ex.Message}");
            }
            finally
            {
                if (_basePage?.Browser != null)
                {
                    await _basePage.Browser.CloseAsync();
                    await _basePage.Browser.DisposeAsync();
                }

                Console.WriteLine("****** Test Hook - AfterScenario - End");
            }
        }


    }
}
