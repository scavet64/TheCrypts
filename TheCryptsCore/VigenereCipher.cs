using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCryptsCore
{
    public class VigenereCipher
    {
        public static string EncryptMessage(string plainText, string key)
        {

            plainText = plainText.ToUpper();
            key = key.ToUpper();
            char[] encryptedArray = new char[plainText.Length];

            for (int i = 0; i < plainText.Length; i++)
            {
                int mi = GetCharValue(plainText[i]);
                int ki = GetCharValue(key[i % key.Length]);

                int shiftedCharValue = CryptoUtilities.Mod((mi + ki), 26);
                encryptedArray[i] = GetIntsCharValue(shiftedCharValue);
            }

            return new string(encryptedArray);
        }

        public static string DecryptMessage(string cipherText, string key)
        {
            cipherText = cipherText.ToUpper();
            key = key.ToUpper();
            char[] encryptedArray = new char[cipherText.Length];

            for (int i = 0; i < cipherText.Length; i++)
            {
                int ci = GetCharValue(cipherText[i]);
                int ki = GetCharValue(key[i % key.Length]);

                int shiftedCharValue = CryptoUtilities.Mod((ci - ki), 26);
                encryptedArray[i] = GetIntsCharValue(shiftedCharValue);
            }

            return new string(encryptedArray); 
        }

        private static int GetCharValue(char c)
        {
            int tmp = (int)c;
            return tmp - 65;
        }

        private static char GetIntsCharValue(int i)
        {
           return ((char)(i + 65));
        }
    }
}
