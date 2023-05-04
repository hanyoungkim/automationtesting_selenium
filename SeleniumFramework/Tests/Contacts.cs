using SeleniumFrameworkTests.pageObjects;
using SeleniumFrameworkTests.utilities;

namespace SeleniumFrameworkTests.Tests
{
    [Order(5)]
    public class Contacts : Base
    {
        [Order(1), TestCase("Lucas", "Kim", "info@sksolution.co.nz", "02108450284")]
        public void addNewContact(string firstName, string lastName, string email, string phone)
        {
            LoginAsAdminUser();

            HomePage homepage = new HomePage(getDriver());
            ContactsPage contactsPage = homepage.goToContactsPage();

            contactsPage.createNewGroup();

            contactsPage.addNewContact(firstName, lastName, email, phone);
            contactsPage.addContactToGroup();
        }

        [Order(2), TestCase("Lucas", "Kim", "info@sksolution.co.nz", "02108450284")]
        public void deleteContact(string firstName, string lastName, string email, string phone)
        {
            LoginAsAdminUser();

            HomePage homepage = new HomePage(getDriver());
            ContactsPage contactsPage = homepage.goToContactsPage();

            contactsPage.deleteContact();
        }
    }
}
