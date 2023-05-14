using SeleniumFrameworkTests.pageObjects;
using SeleniumFrameworkTests.utilities;

namespace SeleniumFrameworkTests.Tests
{
    [Order(6)]
    public class Folders : Base
    {
        [Test, Order(1)]
        public void cleanUpAllTestData()
        {
            LoginAsAdminUser();

            HomePage homepage = new HomePage(getDriver());
            SettingsPage settingsPage = homepage.goToSettingsPage();



        }
    }
}
