using OpenQA.Selenium;
using SeleniumFrameworkTests.pageObjects;
using SeleniumFrameworkTests.utilities;
using System.Collections.Immutable;

namespace SeleniumFrameworkTests.Tests
{
    [Order(3)]
    public class Drafts : Base
    {
        [Order(1), TestCase(
            "admin@sksolution.co.nz",
            "Test email from automation test 05 - Save to Drafts")]
        public void saveMessageToDrafts(string toAddress, string subject)
        {
            LoginAsAdminUser();

            HomePage homepage = new HomePage(getDriver());
            ComposePage composePage = homepage.goToComposePage();

            composePage.saveMessage(toAddress, subject);
        }

        [Order(2), TestCase(
            "admin@sksolution.co.nz",
            "Save to Drafts")]
        public void sendDraftedMessage(string toAddress, string subject)
        {
            LoginAsAdminUser();

            HomePage homepage = new HomePage(getDriver());            
            DraftPage draftPage = homepage.goToDraftsPage();

            ComposePage composePage = draftPage.sendDraftedMessage(subject);
            composePage.editDraftedMessageAndSend();
        }
    }
}
