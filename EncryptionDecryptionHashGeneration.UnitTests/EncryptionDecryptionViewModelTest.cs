using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EncryptionDecryptionHashGeneration.ViewModels;

namespace EncryptionDecryptionHashGeneration.UnitTests
{
    [TestClass]
    public class EncryptionDecryptionViewModelTest
    {
        [TestMethod]
        public void HexadecimalToBinaryTest()
        {
            char HexadecimalChar = 'a';
            int Binary = 1010;
            var viewmodel = new EncryptionDecryptionViewModel();

            var result = viewmodel.HexadecimalToBinary(HexadecimalChar);
            Assert.AreEqual(result, Binary);
        }
    }
}
