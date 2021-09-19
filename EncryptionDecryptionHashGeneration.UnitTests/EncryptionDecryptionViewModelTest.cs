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
        [TestMethod]
        public void XORBinaryTest()
        {
            string ToBeEncrypted = "01100100100110010000100111101010101011110101";
            string Key = "f564d";
            string XORString = "10010001111111011101011010111100111000101010";
            var viewmodel = new EncryptionDecryptionViewModel();
            string result = viewmodel.XORBinary(ToBeEncrypted, Key);
            Assert.AreEqual(result, XORString);
        }
        [TestMethod]
        public void LegitHexadecimalTest()
        {
            string NonHexadecimalString = "123bkl0fb234r3grewgew";
            var viewmodel = new EncryptionDecryptionViewModel();
            bool result = viewmodel.LegitHexadecimal(NonHexadecimalString);
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void EncryptButtonCommandTest()
        {
            var viewmodel = new EncryptionDecryptionViewModel();
            Assert.IsFalse(viewmodel.EncryptButtonCommand.CanExecute(null));
            viewmodel.MyModel.EncryptButtonEnabled = true;
            Assert.IsTrue(viewmodel.EncryptButtonCommand.CanExecute(null));
        }
        [TestMethod]
        public void DecryptButtonCommandTest()
        {
            var viewmodel = new EncryptionDecryptionViewModel();
            Assert.IsFalse(viewmodel.DecryptButtonCommand.CanExecute(null));
            viewmodel.MyModel.DecryptButtonEnabled = true;
            Assert.IsTrue(viewmodel.DecryptButtonCommand.CanExecute(null));
        }
    }
}
