namespace SzyfrPlayfaira
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Szyfrowanie playfaira");
            Console.WriteLine();
            Console.WriteLine("Podaj klucz: ");
            string key = Console.ReadLine();
            Console.WriteLine("Podaj słowo do zaszyfrowania: ");
            string word = Console.ReadLine();
            Console.WriteLine();

            Console.WriteLine($"Klucz: {key}");
            Console.WriteLine($"Słowo: {word}");
            Console.WriteLine($"Słowo po przygotowaniu: {word.PrepareText()}");

            Console.WriteLine();
            string encrypt = PlayfairCipher.Encrypt(key: key, word: word);
            Console.WriteLine($"Zaszyfrowane słowo: {encrypt}");
            Console.WriteLine($"Odszyfrowane słowo: {PlayfairCipher.Decrypt(encrypt)}");



            PlayfairCipher.PrintArray();

        }
    }
}