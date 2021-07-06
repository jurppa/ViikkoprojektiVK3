using Microsoft.VisualStudio.TestTools.UnitTesting;
using KoodinimiIdänpikajuna;
namespace AppUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AreNamesEqualShouldReturnTrue()
        {

         string[] expected = { "TPE", "HKI" };
         string[] actual = APIUtil.GetStationFullNames("Tampere","Helsinki");
        

         Assert.AreEqual(expected[0], actual[0]);
         Assert.AreEqual(expected[1], actual[1]);

        }
    }
}
