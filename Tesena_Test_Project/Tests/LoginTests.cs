using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Tesena_Test_Project.Tests
{
    public class LoginTests : PageTest
    {
        [Test]
        public async Task ValidLogin()
        {
            #region navigation
            // Load to Homepage
            await Page.GotoAsync("http://37.27.17.198:8084/cs/");

            // Go to Login page
            await Page.RunAndWaitForNavigationAsync(() =>
            Page.ClickAsync("[title=\"Pøihlášení k vašemu zákaznickému úètu\"]"));
            #endregion

            #region login input
            var email = Page.Locator("input#field-email");
            var password = Page.Locator("input#field-password");
            var loginButton = Page.Locator("button#submit-login");

            await email.FillAsync("testmail@gmail.com");
            await password.FillAsync("vitecojetopassy123");
            await loginButton.ClickAsync();
            #endregion

            #region check for Homepage
            // Verify Homepage
            Assert.That(Page.Url, Does.EndWith("/cs/"));
            #endregion

        }
        [Test]
        public async Task InvalidLogin()
        {
            #region navigation
            // Go to Homepage
            await Page.GotoAsync("http://37.27.17.198:8084/cs/");

            // Go to Login page
            await Page.RunAndWaitForNavigationAsync(() =>
            Page.ClickAsync("[title=\"Pøihlášení k vašemu zákaznickému úètu\"]"));
            #endregion

            #region login input
            // Login
            var email = Page.Locator("input#field-email");
            var password = Page.Locator("input#field-password");
            var loginButton = Page.Locator("button#submit-login");

            await email.FillAsync("wrongtestmail@gmail.com");
            await password.FillAsync("Objectreferenceisnotsettoaninstanceofanobject");
            await loginButton.ClickAsync();
            #endregion

            #region error box
            // Check for Error message
            var loginErrorBox = Page.Locator("//li[@class='alert alert-danger']");
            await loginErrorBox.WaitForAsync(new()
            {
                State = WaitForSelectorState.Visible,
                Timeout = 2000
            });

            Assert.That(await loginErrorBox.IsVisibleAsync(), Is.True);
            #endregion

            #region naviagtion
            // Go to Homepage
            await Page.RunAndWaitForNavigationAsync(() =>
            Page.ClickAsync("//img[@class='logo img-fluid']"));
            #endregion

            #region check for Homepage
            // Verify Homepage
            Assert.That(Page.Url, Does.EndWith("/cs/"));
            #endregion

        }
    }
}
