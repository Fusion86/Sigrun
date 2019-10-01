using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sigrun.Serialization.UETypes;
using System.IO;
using System.Linq;

namespace Sigrun.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CommonData_Slot0v008()
        {
            Gvas gvas = Gvas.Load(@"C:\Users\Wouter\AppData\Local\CodeVein\Saved\SaveGames\112073240\CommonData_Slot0v008.sav");

            if (gvas.Properties.First(x => x.Name == "__buffer") is UEArrayProperty<byte> arr)
                File.WriteAllBytes("CommonData_Slot0v008", arr.Value);
        }

        [TestMethod]
        public void GameData_Slot0v011()
        {
            Gvas gvas = Gvas.Load(@"C:\Users\Wouter\AppData\Local\CodeVein\Saved\SaveGames\112073240\GameData_Slot0v011.sav");

            if (gvas.Properties.First(x => x.Name == "__buffer") is UEArrayProperty<byte> arr)
                File.WriteAllBytes("GameData_Slot0v011", arr.Value);
        }
    }
}
