﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using EncryptionDecryptionHashGeneration.ViewModels;

namespace EncryptionDecryptionHashGeneration.Commands
{
    public class EncryptButtonCommand : ICommand
    {
        EncryptionDecryptionViewModel _encryptionDecryptionViewModel;

        public EncryptButtonCommand(EncryptionDecryptionViewModel viewModel)
        {
            _encryptionDecryptionViewModel = viewModel;            
        }

        public bool CanExecute(object parameter)
        {
            return (_encryptionDecryptionViewModel.EncryptCanExecute());
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _encryptionDecryptionViewModel.EncryptOnExecute(_encryptionDecryptionViewModel.MyModel.FileExtension);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
