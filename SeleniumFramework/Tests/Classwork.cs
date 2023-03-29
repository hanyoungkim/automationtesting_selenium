using SeleniumFramework.pageObjects;
using SeleniumFramework.utilities;

namespace SeleniumFramework.Tests
{
    [Order(3)]
    public class Classwork : Base
    {
        [Order(1)]
        [TestCase("Automation Testing"), Category("Regression")]
        [TestCase("Manual Testing"), Category("Regression")]
        [TestCase("Agile Testing"), Category("Regression")]
        public void createTopic(string topicName)
        {
            LoginAsTeacher();

            HomePage homePage = new HomePage(getDriver());
            homePage.goToClassroom();

            ClassworkPage classworkPage = homePage.goToClasswork();

            classworkPage.createTopic(topicName);

            StringAssert.Contains(topicName, classworkPage.getFirstLocatedTopic().Text);
        }
    }
}
