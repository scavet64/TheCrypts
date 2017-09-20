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


        ////////////////////////////////////////////////////Cryptanalysis////////////////////////////////////////////////////


        public static void test()
        {
            string VigenereCryptogram = @"FEJNIGCHVHZVWDJJCJWVVXMGRGYZZLCHRZTVASKTWTQTZTHUNHFYHVUEZTRVJNTUIACRPGBQUAPUBBDRRXAFFEJRSCCOEZVRVSFSSODTDUGGXFKNSGRMVCSBWLPROLXNKNSZFEUORAXTKGZXVUTNPHCKVVHDDIVZOAMLZYHRWEUOHJJSRTWATYSROPTNZMVGJNUUQPJSZUBNULPXOVWYNKKRAERSCAPWFURFJNUXCPTSYOZYBAEJUBAGVYGBBHLZWARNWGQGCHRZKUNNNKDRNPVJHUAOLMVNLHZTYVWATAFGJIECSPXUCJRVBCVXBAXTYOBTCHVJFVEEIGBQLOEJIPCOIUBGXPNKFRBTZRZGXOFXCAUYJVCXNAKRCAPIEZSEEACYWAUONZCANSRYWFCHVCOLXFDKBVWTYKAVMSKUTVWVZYWOUEUGBTNRJCSYRSKKBRMTFXOVWDIUDFYAKZSERNXUBGQEIUCSJNUZVRPRZTRVWGFLHUNWYKSYBTYXCHPHKNSZDDUEUEJVVROAMTYKZBFWROZVWGFLHUNWZTRNWDRRZGQEKOARFEYGRGQAKGPFDRUYSABELVCADSZTGRYAIGPYNFIUAGAAMKZNCNZMVGRNRIZBBETAFGJIEKRINHZIZRCHVYSABEFLFRVAZTWAPPVXTRLTCEGGRLCOBBWEGROPNNFZKVCHJZOAMIEMHUNJFRHVWGRTRFFAPOBTXFKNSINHZIZRCHVZFNVPCOBTXFKNSUXRJKGNWDKNSTAIEJWAPOWZVRFHVKZFFECOGGNNVJOYXNXZWZNWZZVVWTVTHSJCLRHVNSRTROJTVJPENAKNSINRPZWZNOEKCSDSNUIYMRVROKJNUJFNFACUBTBIXNCSAECOSSJNUYHNATKUGNHSFSSGQIEMOPXMIGRRFOLRRONSLXSGXUKZSEJSLJRRWHRXYNWDZTGGJNKRMGQEVDDRAIDKBGNRNGGERGZJOAMLZYHRWIEMOTJIEYCGQEKOFRBODKAVWUKKGNWDUKQNMEJUTZRNLZSFMRRMURMANGMHWTZROGUAJZCHATVTGROOISGSRLDKRBEEICWGQAUAZYNDTUBFLIFAGANSJGBQFEJRSCCIWUBRVIXNHPJLCYIPQATUBQRTZUBOHSFYHEXNXGBNVEWUFVCWRYOFUEVVGRCWZZVNQAZXHERGXKFVCWRYOFUEVVGRNTYOBTJNUZSRVIEMKVCHRCSVADRTRQRSKXSFBFLRQBWFLYWBWOWYVENDJGBQOAXKBQBOWJFRJMJGGYNEGZVNCWRYOPQAFYDENSVTHYHDIKOZBAEJGYNEGGBQCHVYIYUEENIFQOWZVRWIXNHJNRVYHNATCKROHAIOBTRNXXSCXRKGBQLLFBSAKYJAQUJLFTUJRLUGUBWIQOBTBHIOSXCHVTKRQERXRGNNJZSCBFIUAGQEJZOTN";
            int keyLen = 6;
            Dictionary<char, double> frequencies;
            List<char> keyChars = new List<char>();


            VigenereCryptogram.Replace(" ", "").Replace("\n", "").Replace("\r", "");
            List<string> substitutionTexts = SplitTextWithKeyLen(VigenereCryptogram, keyLen);


            foreach (string caesarCryptogram in substitutionTexts)
            {
                Console.WriteLine(caesarCryptogram);
                Console.WriteLine(string.Format("Length: {0}", caesarCryptogram.Length));

                frequencies = AnalyseFrequency(caesarCryptogram);

                double maxFreq = frequencies.Values.Max();
                char maxChar = frequencies.Keys.Where(c => frequencies[c] == maxFreq).FirstOrDefault();

                //'E' is the maxChar
                Console.WriteLine(string.Format("E is probably: {0} - {1}", maxChar, maxFreq));
                Console.WriteLine(string.Format("Key would be: {0}", (char)(maxChar - 'e' + 'A')));

                keyChars.Add((char)(maxChar - 'e' + 'A'));

                foreach (var item in frequencies)
                {
                    Console.WriteLine(string.Format("{0}: {1:0.00}", item.Key, item.Value));
                }

                Console.WriteLine("******************************");
                Console.WriteLine("");
            }

            string VigenereKEY = "";
            foreach (char c in keyChars)
            {
                VigenereKEY += c;
            }

            Console.WriteLine("Key found: {0}", VigenereKEY);

            Console.WriteLine(DecipherVeginere(VigenereCryptogram, VigenereKEY));

        }

        static List<string> SplitTextWithKeyLen(string text, int keyLen)
        {
            List<string> result = new List<string>();
            StringBuilder[] sb = new StringBuilder[keyLen];

            for (int i = 0; i < keyLen; i++)
            {
                sb[i] = new StringBuilder();
            }

            for (int i = 0; i < text.Length; i++)
            {
                sb[i % keyLen].Append(text[i]);
            }

            foreach (var item in sb)
            {
                result.Add(item.ToString());
            }

            return result;
        }

        static Dictionary<char, double> AnalyseFrequency(string text)
        {
            if (text == null)
                return null;

            Dictionary<char, double> frequencies = new Dictionary<char, double>();
            int textLength = text.Length;

            for (int i = 0; i < textLength; i++)
            {
                char c = text[i];

                char key = '#';

                //ignore chars that are not letters
                if ((c >= 'a' && c <= 'z'))
                    key = c;

                if (c >= 'A' && c <= 'Z')
                    key = (char)(c + 'a' - 'A');

                if (frequencies.Keys.Contains(key))
                    frequencies[key] = frequencies[key] + 1;
                else
                    frequencies[key] = 1;
            }

            //cannot enumerate throught the dictionnay keys directly.
            List<char> keys = frequencies.Keys.ToList();

            foreach (char c in keys)
            {
                frequencies[c] /= textLength;
            }

            return frequencies;
        }

        private static string DecipherVeginere(string text, string key)
        {
            StringBuilder result = new StringBuilder();
            int keyLength = key.Length;
            int diff;
            char decoded;

            for (int i = 0; i < text.Length; i++)
            {
                diff = text[i] - key[i % keyLength];

                //diff should never be more than 25 or less than -25
                if (diff < 0)
                    diff += 26;

                decoded = (char)(diff + 'a');
                result.Append(decoded);
            }

            return result.ToString();
        }
    }
}
