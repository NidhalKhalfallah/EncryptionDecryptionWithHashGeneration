using EncryptionDecryptionHashGeneration.Commands;
using EncryptionDecryptionHashGeneration.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;



namespace EncryptionDecryptionHashGeneration.ViewModels
{
    public class EncryptionDecryptionViewModel : INotifyPropertyChanged
    {
        public BrowseButtonCommand BrowseButtonCommand { get; set; }
        public EncryptButtonCommand EncryptButtonCommand { get; set; }
        public DecryptButtonCommand DecryptButtonCommand { get; set; }
        public EncryptionDecryptionModel MyModel { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;


        public EncryptionDecryptionViewModel()
        {
            BrowseButtonCommand = new BrowseButtonCommand(this);
            EncryptButtonCommand = new EncryptButtonCommand(this);
            DecryptButtonCommand = new DecryptButtonCommand(this);
            MyModel = new EncryptionDecryptionModel
            {
                EncryptButtonEnabled = false,
                DecryptButtonEnabled = false
            };
        }


        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (!string.IsNullOrEmpty(propertyName))
            { this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }

        }


        public bool EncryptCanExecute()
        {
            return (MyModel.EncryptButtonEnabled);
        }


        public bool DecryptCanExecute()
        {
            return (MyModel.DecryptButtonEnabled);
        }


        public void BrowseOnExecute()
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            bool? response = openFileDialog.ShowDialog();
            if ((System.IO.Path.GetExtension(openFileDialog.FileName) == ".png") || (System.IO.Path.GetExtension(openFileDialog.FileName) == ".encrypt"))
            {
                MyModel.FileAddress = openFileDialog.FileName;
                MyModel.ImageDirectory = System.IO.Path.GetDirectoryName(openFileDialog.FileName);
                MyModel.ImageName = System.IO.Path.GetFileNameWithoutExtension(openFileDialog.FileName);
                MyModel.FileExtension = System.IO.Path.GetExtension(openFileDialog.FileName);
                //Stream MyImage = File.OpenRead(openFileDialog.FileName);
                using (var md5 = MD5.Create())
                {
                    using (var stream = File.OpenRead(openFileDialog.FileName))
                    {
                        MyModel.HashedText = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "");
                    }
                }
            }
            else if (response == true) MessageBox.Show("Only .png and .encrypt files are accepted");
            if (System.IO.Path.GetExtension(openFileDialog.FileName) == ".png")
            {
                MyModel.EncryptButtonEnabled = true;
                MyModel.DecryptButtonEnabled = false;
                EncryptButtonCommand.RaiseCanExecuteChanged();
                DecryptButtonCommand.RaiseCanExecuteChanged();
            }
            if (System.IO.Path.GetExtension(openFileDialog.FileName) == ".encrypt")
            {
                MyModel.EncryptButtonEnabled = false;
                MyModel.DecryptButtonEnabled = true;
                EncryptButtonCommand.RaiseCanExecuteChanged();
                DecryptButtonCommand.RaiseCanExecuteChanged();
            }
        }


        public void EncryptOnExecute(string Extension)
        {
            string ObjectiveExtension = ".png";
            if (Extension == ".png") ObjectiveExtension = ".encrypt";
            if (File.Exists(MyModel.ImageDirectory + "/" + MyModel.ImageName + Extension))
            {
                if (LegitHexadecimal(MyModel.KeyText) == false) MessageBox.Show("Please enter a key in hexadecimal representation");
                else
                {
                    if (!Directory.Exists(MyModel.ImageDirectory))
                    {
                        Directory.CreateDirectory(MyModel.ImageDirectory);
                    }
                    if (!File.Exists(MyModel.ImageDirectory + "/" + MyModel.ImageName + ObjectiveExtension))
                    {
                        byte[] imgbyte = File.ReadAllBytes(MyModel.FileAddress);
                        var resultt = string.Concat(imgbyte.Select(b => Convert.ToString(b, 2).PadLeft(8, '0')));
                        var XoredResult = XORBinary(resultt, MyModel.KeyText);
                        byte[] buff = new byte[XoredResult.Length / 8];
                        for (var i = 0; i < XoredResult.Length / 8; i++)
                            buff[i] = Convert.ToByte(XoredResult.Substring(i * 8, 8), 2);
                        if (Extension == ".png")
                        {
                            File.WriteAllBytes(MyModel.ImageDirectory + "/" + MyModel.ImageName + ObjectiveExtension, buff);
                            MessageBox.Show("The encrypted file has been successfully created");
                        }
                        if (Extension == ".encrypt")
                        {
                            using (MemoryStream memstr = new MemoryStream(buff))
                            {
                                try
                                {
                                    Image imgg = Image.FromStream(memstr);
                                    imgg.Save(MyModel.ImageDirectory + "/" + MyModel.ImageName + ".png");
                                    MessageBox.Show("The file has been decrypted successfully!");
                                }
                                catch
                                {
                                    MessageBox.Show("The given key is invalid");
                                }
                            }
                        }
                    }
                    else MessageBox.Show("The file " + MyModel.ImageDirectory + "\\" + MyModel.ImageName + ObjectiveExtension + " already exists");
                }
            }
            else { MessageBox.Show("The file " + MyModel.ImageDirectory + "\\" + MyModel.ImageName + Extension + " doesn't exist anymore");
            }
        }


        //This method converts a hexadecimal character to the binary form
        public int HexadecimalToBinary(char HexadecimalChar)
        {
            int Binary = 2;
            int NewBinary = 0;
            if ((HexadecimalChar == 'a') || (HexadecimalChar == 'A')) Binary = 10;
            if ((HexadecimalChar == 'b') || (HexadecimalChar == 'B')) Binary = 11;
            if ((HexadecimalChar == 'c') || (HexadecimalChar == 'C')) Binary = 12;
            if ((HexadecimalChar == 'd') || (HexadecimalChar == 'D')) Binary = 13;
            if ((HexadecimalChar == 'e') || (HexadecimalChar == 'E')) Binary = 14;
            if ((HexadecimalChar == 'f') || (HexadecimalChar == 'F')) Binary = 15;
            if (Binary == 2) Binary = Convert.ToInt32(HexadecimalChar.ToString());
            int Newnumber = Binary;
            for (int i = 3; i >= 0; i--)
            {
                NewBinary += ((Newnumber / (int)Math.Pow(2, i)) * (int)Math.Pow(10, i));
                Newnumber = Newnumber % (int)Math.Pow(2, i);
            }
            return (NewBinary);
        }


        //This method gives the result of the XOR operation applied on two binary strings
        public string XORBinary(string BinaryToBeEncrypted, string HexadecimalKey)
        {
            string XORResult = "";
            //In case the given key is shorter than the ToBeEncrypted string we repeat the key until it becomes longer than the ToBeEncrypted string
            string LongKey = "";
            for (int i = 0; i < HexadecimalKey.Length; i++)
            {
                for (int j = 1; j < 5 - (HexadecimalToBinary(HexadecimalKey[i])).ToString().Length; j++)
                {
                    LongKey += "0";
                }
                LongKey += (HexadecimalToBinary(HexadecimalKey[i])).ToString();
            }
            while (LongKey.Length < BinaryToBeEncrypted.Length) LongKey += LongKey;
            string[] Ar = new string[BinaryToBeEncrypted.Length];
            for (int i = 0; i < BinaryToBeEncrypted.Length; i++)
            {
                Ar[i] = ((((int)LongKey[i]) + (Convert.ToInt32(BinaryToBeEncrypted[i]))) % 2).ToString();
            }
            XORResult = String.Concat(Ar);
            return (XORResult);
        }


        //This method checks if a string is hexadecimal
        public bool LegitHexadecimal(string ToBeChecked)
        {
            char[] hex_allowed = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'A', 'b', 'B', 'c', 'C', 'd', 'D', 'e', 'E', 'f', 'F' };
            bool isLegit = true;
            bool isHexadecimal = true;
            if ((ToBeChecked == null) || (ToBeChecked == "")) isLegit = false;
            else
            {
                foreach (char c in ToBeChecked)
                {
                    if (!(hex_allowed.Contains(c)))
                    {
                        isHexadecimal = false;
                        break;
                    }
                }
                if (isHexadecimal == false) isLegit = false;
            }
            return (isLegit);
        }
    }
}
