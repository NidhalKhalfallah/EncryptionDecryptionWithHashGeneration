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
        public void BinaryToHexadecimalTest()
        {
            char HexadecimalChar = '6';
            string Binary = "0110";
            var viewmodel = new EncryptionDecryptionViewModel();

            var result = viewmodel.BinaryToHexadecimal(Binary);
            Assert.AreEqual(result, HexadecimalChar);
        }
        [TestMethod]
        public void XORTwoCharsTest()
        {
            char HexadecimalChar1 = '7';
            char HexadecimalChar2 = 'b';
            string Binary = "1100";
            var viewmodel = new EncryptionDecryptionViewModel();

            string result = viewmodel.XORTwoChars(HexadecimalChar1, HexadecimalChar2);
            Assert.AreEqual(result, Binary);
        }
        [TestMethod]
        public void XORTest()
        {
            string HexadecimalString1 = "123bcae3467d297f9927aab1613a980d";
            string HexadecimalString2 = "f56425e2916b8b02f45c2dccaa9662b2";
            string XORString = "E75FEF01D716A27D6D7B877DCBACFABF";
            var viewmodel = new EncryptionDecryptionViewModel();

            string result = viewmodel.XOR(HexadecimalString1, HexadecimalString2);
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
            viewmodel.EncryptButtonEnabled = true;
            Assert.IsTrue(viewmodel.EncryptButtonCommand.CanExecute(null));
        }
        [TestMethod]
        public void DecryptButtonCommandTest()
        {
            var viewmodel = new EncryptionDecryptionViewModel();
            Assert.IsFalse(viewmodel.DecryptButtonCommand.CanExecute(null));
            viewmodel.DecryptButtonEnabled = true;
            Assert.IsTrue(viewmodel.DecryptButtonCommand.CanExecute(null));
        }
    }
}
