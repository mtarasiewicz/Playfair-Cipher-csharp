using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzyfrPlayfaira
{
    internal class PlayfairCipher
    {
        const string alphabet = "abcdefghiklmnopqrstuvwxyz";
        private static char[,] cipherArray = new char[5, 5];
        private static KeyValuePair<int, int> firstCoordinates;
        private static KeyValuePair<int, int> secondCoordinates;
        private static void GenerateArray(string key)
        {
            string control = String.Empty;
            int x = 0;
            int y = 0;
            int position;
            foreach (var l in key)
            {
                position = control.IndexOf(l);
                if (position == -1)
                {
                    if (y == 5)
                        throw new Exception("Error during encryption. To many letters in array. ");

                    control += l;
                    cipherArray[x, y] = l;
                    x++;

                    if (x == 5)
                    {
                        x = 0;
                        y++;
                    }
                }
            }

            foreach (var l in alphabet)
            {
                position = control.IndexOf(l);
                if (position == -1)
                {
                    if (y == 5)
                        throw new Exception("Error during encryption. To many letters in array. ");

                    control += l;
                    cipherArray[x, y] = l;
                    x++;

                    if (x == 5)
                    {
                        x = 0;
                        y++;
                    }
                }
            }
            
        }

        private static KeyValuePair<int, int> SignCoordinate(char c)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (cipherArray[j,i] == c)
                    {
                        KeyValuePair<int, int> coordinates = new(j, i);
                        return coordinates;
                    }
                }
            }
            throw new Exception("Coordinates not found");
        }
        public static string Encrypt(string key, string word)
        {
            word = word.PrepareText();
            key = key.PrepareKey();
            GenerateArray(key);
            
            string encryptText = "";
            for (int i = 0; i < word.Length; i += 2)
            {
                firstCoordinates = SignCoordinate(word[i]);
                secondCoordinates = SignCoordinate(word[i + 1]);

                if (firstCoordinates.Key == secondCoordinates.Key)
                {
                    encryptText += cipherArray[firstCoordinates.Key, (firstCoordinates.Value + 1) % 5];
                    encryptText += cipherArray[secondCoordinates.Key, (secondCoordinates.Value + 1) % 5];
                }
                else if (firstCoordinates.Value == secondCoordinates.Value)
                {
                    encryptText += cipherArray[(firstCoordinates.Key + 1) % 5, firstCoordinates.Value];
                    encryptText += cipherArray[(secondCoordinates.Key + 1) % 5, secondCoordinates.Value];
                }
                else
                {
                    encryptText += cipherArray[secondCoordinates.Key, firstCoordinates.Value];
                    encryptText += cipherArray[firstCoordinates.Key, secondCoordinates.Value];
                }
            }
            return encryptText;
        }

        public static string Decrypt(string encryptedText)
        {
            string decryptText = "";

            for (int i = 0; i < encryptedText.Length; i += 2)
            {
                firstCoordinates = SignCoordinate(encryptedText[i]);
                secondCoordinates = SignCoordinate(encryptedText[i + 1]);

                if (firstCoordinates.Key == secondCoordinates.Key)
                {
                    if (firstCoordinates.Value - 1 < 0)
                    {
                        decryptText += cipherArray[firstCoordinates.Key, 4];
                    }
                    else
                    {
                        decryptText += cipherArray[firstCoordinates.Key, firstCoordinates.Value - 1];
                    }

                    if (secondCoordinates.Value - 1 < 0)
                    {
                        decryptText += cipherArray[secondCoordinates.Key, 4];
                    }
                    else
                    {
                        decryptText += cipherArray[secondCoordinates.Key, secondCoordinates.Value - 1];
                    }
                }
                else if (firstCoordinates.Value == secondCoordinates.Value) 
                {
                    if (firstCoordinates.Key - 1 < 0)
                    {
                        decryptText += cipherArray[4, firstCoordinates.Value];
                    }
                    else
                    {
                        decryptText += cipherArray[firstCoordinates.Key - 1, firstCoordinates.Value];
                    }

                    if (secondCoordinates.Key -1 < 0)
                    {
                        decryptText += cipherArray[4, secondCoordinates.Value];
                    }
                    else
                    {
                        decryptText += cipherArray[secondCoordinates.Key - 1, secondCoordinates.Value];
                    }
                }
                else
                {
                    decryptText += cipherArray[secondCoordinates.Key, firstCoordinates.Value];
                    decryptText += cipherArray[firstCoordinates.Key, secondCoordinates.Value];
                }
            }
            return decryptText;
        }

        public static void PrintArray()
        {
            Console.WriteLine("Playfair cipher array: ");
            for (int i = 0; i < 5; i++)
            {
                
                for (int j = 0; j < 5; j++)
                {
                    Console.Write(cipherArray[j, i] + " ");   
                }
                
                Console.WriteLine();
            }
        }
    }
}
