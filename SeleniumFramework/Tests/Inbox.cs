using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumFrameworkTests.pageObjects;
using SeleniumFrameworkTests.utilities;
using System.Collections.Immutable;

namespace SeleniumFrameworkTests.Tests
{
    [Order(4)]
    public class Inbox : Base
    {
        [Order(1), TestCase("Find this email")]
        public void searchEmailBySubject(string subjectToSearch)
        {
            LoginAsAdminUser();

            HomePage homepage = new HomePage(getDriver());
            homepage.searchEmail(subjectToSearch);
        }

        [Order(2), TestCase("Reply Test", "This is a reply message.")]
        public void replyEmail(string emailToReply, string message)
        {
            LoginAsAdminUser();

            HomePage homepage = new HomePage(getDriver());
            ComposePage composePage = homepage.selectEmailToReply(emailToReply);

            composePage.replyEmail(message);
        }

        [Order(3), TestCase("automation test 01")]
        public void flagEmail(string emailToFlag)
        {
            LoginAsAdminUser();

            HomePage homepage = new HomePage(getDriver());
            homepage.flagEmail(emailToFlag);
        }
    }
}
