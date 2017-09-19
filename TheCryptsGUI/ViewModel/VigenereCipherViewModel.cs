using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCryptsCore;

namespace TheCryptsGUI.ViewModel
{
    class VigenereCipherViewModel: ViewModelBase
    {
        #region fields
        string plainText;
        string key;
        string cipherText;
        #endregion

        #region properties
        public string PlainText
        {
            get { return plainText; }
            set
            {
                plainText = value;
                OnPropertyChanged("PlainText");
            }
        }

        public string Key
        {
            get { return key; }
            set
            {
                key = value;
                OnPropertyChanged("Key");
            }
        }

        public string CipherText
        {
            get { return cipherText; }
            set
            {
                cipherText = value;
                OnPropertyChanged("CipherText");
            }
        }
        #endregion

        #region ctor
        public VigenereCipherViewModel()
        {
            //Add Commands
            Commands.Add("Encrypt", new RelayCommand(Encrypt));
            Commands.Add("Decrypt", new RelayCommand(Decrypt));
        }
        #endregion


        #region UI methods


        private void Encrypt(object obj)
        {
            CipherText = VigenereCipher.EncryptMessage(plainText, key);
        }

        private void Decrypt(object obj)
        {
            PlainText = VigenereCipher.DecryptMessage(cipherText, key);
        }
        #endregion
    }
}
