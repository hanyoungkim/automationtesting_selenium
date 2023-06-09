﻿using SeleniumFrameworkTests.pageObjects;
using SeleniumFrameworkTests.utilities;

namespace SeleniumFrameworkTests.Tests
{
    [Order(99)]
    public class CleanUp : Base
    {
        [Test, Order(1)]
        public void cleanUpAllTestData()
        {
            LoginAsAdminUser();

            HomePage homepage = new HomePage(getDriver());
            homepage.DeleteAllEmailsFromInbox();
            homepage.goToSentFolder();
            homepage.DeleteAllEmailsFromSent();
        }
    }
}
