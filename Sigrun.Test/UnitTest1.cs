using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sigrun.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CommonData_Slot0v008()
        {
            Gvas gvas = Gvas.Load(@"C:\Users\Wouter\AppData\Local\CodeVein\Saved\SaveGames\112073240\CommonData_Slot0v008.sav");
        }
    }
}
