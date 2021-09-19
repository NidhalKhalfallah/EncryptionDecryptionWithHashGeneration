using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EncryptionDecryptionHashGeneration.Models
{
    public class EncryptionDecryptionModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (!string.IsNullOrEmpty(propertyName))
            { this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }
        }


        bool encryptButtonEnabled;
        public bool EncryptButtonEnabled
        {
            get => encryptButtonEnabled;
            set
            {
                if (encryptButtonEnabled != value)
                {
                    encryptButtonEnabled = value;
                    this.RaisePropertyChanged();
                }
            }

        }


        bool decryptButtonEnabled;
        public bool DecryptButtonEnabled
        {
            get => decryptButtonEnabled;
            set
            {
                if (decryptButtonEnabled != value)
                {
                    decryptButtonEnabled = value;
                    this.RaisePropertyChanged();
                }
            }

        }


        string hashedtext;
        public string HashedText
        {
            get => hashedtext;
            set
            {
                if (hashedtext != value)
                {
                    hashedtext = value;
                    this.RaisePropertyChanged();
                }
            }

        }


        string fileaddress;
        public string FileAddress
        {
            get => fileaddress;
            set
            {
                if (fileaddress != value)
                {
                    fileaddress = value;
                    this.RaisePropertyChanged();
                }
            }

        }


        string fileextension;
        public string FileExtension
        {
            get => fileextension;
            set
            {
                if (fileextension != value)
                {
                    fileextension = value;
                    this.RaisePropertyChanged();
                }
            }

        }


        string imagedirectory;
        public string ImageDirectory
        {
            get => imagedirectory;
            set
            {
                if (imagedirectory != value)
                {
                    imagedirectory = value;
                    this.RaisePropertyChanged();
                }
            }

        }


        string imagename;
        public string ImageName
        {
            get => imagename;
            set
            {
                if (imagename != value)
                {
                    imagename = value;
                    this.RaisePropertyChanged();
                }
            }

        }


        string keytext;
        public string KeyText
        {
            get => keytext;
            set
            {
                if (keytext != value)
                {
                    keytext = value;
                    this.RaisePropertyChanged();
                }
            }

        }
    }
}
