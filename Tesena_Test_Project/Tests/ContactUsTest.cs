using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Tesena_Test_Project.Tests
{
    public class ContactUs : PageTest
    {
        [Test]
        public async Task Contact()
        {
            #region navigation
            // Load to Homepage
            await Page.GotoAsync("http://37.27.17.198:8084/cs/");

            // Go to Contact
            await Page.RunAndWaitForNavigationAsync(() =>
            Page.ClickAsync("div#contact-link>a"));
            #endregion

            #region Message input
            var email = Page.Locator("//input[@class='form-control']");
            var messageContent = Page.Locator("//textarea[@class='form-control']");
            var sendButton = Page.Locator("//input[@class='btn btn-primary']");

            await email.FillAsync("testmail@gmail.com");
            await messageContent.FillAsync("This is an automated Testing message");
            await sendButton.ClickAsync();
            #endregion

            #region confirmation box
            // Check for confirmation message
            var MessageSentBox = Page.Locator("//li[text()='E-mail byl úspěšně odeslán našemu týmu.']");
            await MessageSentBox.WaitForAsync(new()
            {
                State = WaitForSelectorState.Visible,
                Timeout = 2000
            });

            Assert.That(await MessageSentBox.IsVisibleAsync(), Is.True);
            #endregion

            #region navigation
            // go to Homepage
            await Page.RunAndWaitForNavigationAsync(() =>
            Page.ClickAsync("//img[@class='logo img-fluid']"));
            #endregion

            #region verify Homepage
            // Verify Homepage
            Assert.That(Page.Url, Does.EndWith("/cs/"));
            #endregion

        }
    }
}
