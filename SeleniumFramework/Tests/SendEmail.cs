using OpenQA.Selenium;
using SeleniumFrameworkTests.pageObjects;
using SeleniumFrameworkTests.utilities;
using System.Collections.Immutable;

namespace SeleniumFrameworkTests.Tests
{
    [Order(2)]
    [Parallelizable(ParallelScope.Children)]
    public class SendEmail : Base
    {
        [Order(1), Category("Regression")]
        [TestCase(
            "admin@sksolution.co.nz",
            "Test email from automation test 01 - Find this email",
            "This email was sent by automation test by Lucas.")]
        public void sendPlainEmail(string toAddress, string subject, string message)
        {
            LoginAsAdminUser();

            HomePage homepage = new HomePage(getDriver());
            ComposePage composePage = homepage.goToComposePage();   

            composePage.sendPlainEmail(toAddress,subject,message);
        }

        [Order(2), Category("Regression")]
        [TestCase(
            "admin@sksolution.co.nz", "info@sksolution.co.nz", "lucas.test.c9@gmail.com",
            "Test email from automation test 02 - Reply Test",
            "This email was sent by automation test by Lucas.")]
        public void sendEmailWithCcAndBcc(string toAddress, string ccAddress, string bccAddress, string subject, string message)
        {
            LoginAsAdminUser();

            HomePage homepage = new HomePage(getDriver());
            ComposePage composePage = homepage.goToComposePage();

            composePage.sendEmailWithCCAndBcc(toAddress, ccAddress, bccAddress, subject, message);
        }

        [Order(3)]
        [TestCase(
            "admin@sksolution.co.nz",
            "Test email from automation test 03 - Attachments",
            "This email was sent by automation test by Lucas.",
            "test.txt", "test2.txt")]
        public void sendEmailWithAttachments(string toAddress, string subject, string message, string fileName1, string filename2)
        {
            LoginAsAdminUser();

            HomePage homepage = new HomePage(getDriver());
            ComposePage composePage = homepage.goToComposePage();

            composePage.sendEmailWith2Attachments(toAddress, subject, message, fileName1, filename2);
        }

        [Order(4)]
        [TestCase(
            "admin@sksolution.co.nz",
            "Test email from automation test 04 - Image")]
        public void sendEmailWithImage(string toAddress, string subject)
        {
            LoginAsAdminUser();

            HomePage homepage = new HomePage(getDriver());
            ComposePage composePage = homepage.goToComposePage();

            composePage.sendEmailWithImage(toAddress, subject);
        }
    }
}
