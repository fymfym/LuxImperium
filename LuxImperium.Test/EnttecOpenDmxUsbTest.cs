using Moq;
using Xunit;
using System.IO;
using LuxImperium.Services;

namespace LuxImperium.Test
{
    public class LuxImperiumGovernorTests
    {
        [Fact]
        public void LoadScene_Should_Throw_Exception_If_File_Not_Found()
        {
            var mockActionFactory = new Mock<IActionFactory>();
            var enttecOpenDmxUsb = new LuxImperiumGovernor(mockActionFactory.Object);

            Assert.Throws<FileNotFoundException>(() => enttecOpenDmxUsb.LoadScene("nonExistentFile.json"));
        }

        [Fact]
        public void LoadScene_Should_Throw_Exception_If_File_Invalid()
        {
            var mockActionFactory = new Mock<IActionFactory>();
            var enttecOpenDmxUsb = new LuxImperiumGovernor(mockActionFactory.Object);

            var invalidFilePath = "invalidFile.json";

            Assert.Throws<FileNotFoundException>(() => enttecOpenDmxUsb.LoadScene(invalidFilePath));
        }
        
        [Fact]
        public void Stop_Should_Do_Nothing_If_Not_Started()
        {
            var mockActionFactory = new Mock<IActionFactory>();
            var enttecOpenDmxUsb = new LuxImperiumGovernor(mockActionFactory.Object);

            var exception = Record.Exception(() => enttecOpenDmxUsb.Stop());
            Assert.Null(exception);
        }

        [Fact]
        public void RunActions_Should_Return_Empty_List_If_No_Actions()
        {
            var mockActionFactory = new Mock<IActionFactory>();
            var enttecOpenDmxUsb = new LuxImperiumGovernor(mockActionFactory.Object);

            var result = enttecOpenDmxUsb.RunActions();

            Assert.Empty(result);
        }
    }
}