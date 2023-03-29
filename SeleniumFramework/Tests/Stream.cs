using SeleniumFrameworkTests.pageObjects;
using SeleniumFrameworkTests.utilities;

namespace SeleniumFrameworkTests.Tests
{
    [Order(2)]
    public class Stream : Base
    {
        // Delete the test classroom from the previous test for the clean start
        [Test]
        [Order(1), Category("Regression")]
        public void DeleteClassroomTest()
        {
            LoginAsTeacher();

            HomePage homePage = new HomePage(getDriver());
            homePage.deleteClassroom();
        }
        

        [Test]
        [Order(2), Category("Regression")]
        public void CreateNewClassroomTest()
        {
            LoginAsTeacher();

            HomePage homePage = new HomePage(getDriver());
            StreamPage streamPage = homePage.createNewClassroom("Class 101", "Automation", "Selenium", "101");

            streamPage.waitForClassNameDisplay();

            StringAssert.Contains("Class 101", streamPage.getClassNameLabel().Text);
        }

        [Test]
        [Order(3), Category("Regression")]
        public void CustomizeAppearanceTest()
        {
            LoginAsTeacher();

            HomePage homePage = new HomePage(getDriver());
            StreamPage streamPage = homePage.goToClassroom();

            streamPage.customizeAppearance();

            StringAssert.Contains("Class theme updated", streamPage.getMessageAfterThemeUpdate().Text);
        }

        [TestCase("Selenium Training 101", "Selenium")]
        [TestCase("What is automation testing", "Automation testing")]
        [Order(4), Category("Regression")]
        public void PostAnnouncement(string postMessage, string searchText)
        {
            LoginAsTeacher();

            HomePage homePage = new HomePage(getDriver());
            StreamPage streamPage = homePage.goToClassroom();

            streamPage.postAnnouncementWithVideo(postMessage, searchText);

            StringAssert.Contains("Post created", streamPage.getMessageAfterPost().Text);
        }

        [TestCase("Schdule Announcement - 101")]
        [Order(5), Category("Regression")]
        public void ScheduleAnnouncement(string postMessage)
        {
            LoginAsTeacher();

            HomePage homePage = new HomePage(getDriver());
            StreamPage streamPage = homePage.goToClassroom();

            streamPage.scheduleAnnouncement(postMessage);

            StringAssert.Contains("Scheduled", streamPage.getMessageAfterSchedule().Text);
        }


        [TestCase("Selenium Training 101")]
        [Order(6), Category("Regression")]
        public void deleteAnnouncement(string announcementToDelete)
        {
            LoginAsTeacher();

            HomePage homePage = new HomePage(getDriver());
            StreamPage streamPage = homePage.goToClassroom();

            streamPage.deleteAnnouncement(announcementToDelete);
        }
    }
}
