using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

// Test nyiltszöveg: "ezapróbaszövegamitkódolunk"
// Test titkosszöveg: "auto"
// Test right result: ETTDRIUOSTHJEATAINDCDIEINE

namespace kodol
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1.Feladat
            Console.Write("// 1.Feladat \n");
            Console.WriteLine("Adj meg egy nyilt szöveget: ");
            string openText = Console.ReadLine();

            // 2.Feladat

            openText = inEnglish(openText);

            // 3.Feladat

            Console.WriteLine("\n// 2. & 3. Feladat");
            Console.WriteLine("Nyílt szöveg: " + openText + "\n");

            // 4. Feladat

            Console.Write("// 4.Feladat \n");
            Console.WriteLine("Adjon meg egy 1-5 karakter hosszú szöveget, ami csak az angol abc betüit tartalmazza!");
            string secretText = Console.ReadLine();
            secretText.ToUpper();

            // 5. Feladat

            Console.Write("\n// 5.Feladat \n");
            secretText = connectStrings(openText, secretText);
            Console.WriteLine("Secret text: " + secretText + "\n");

            // 6. Feladat

            Console.Write("// 6.Feladat \n");
            Console.WriteLine("A titkosított szöveg: " + codeIt(openText ,secretText));

        }
        static string inEnglish(string input)
        {
            string returnString = "";
            input = input.ToUpper();

            // Converting the input into a char list

            List<char> charList = input.ToCharArray().ToList();

            // Checking for hungarian uga buga characters

            for (int i = 0; i < charList.Count; i++)
            {
                if(charList[i] == 'Á')
                    charList[i] = 'A';
                
                if (charList[i] == 'É')
                    charList[i] = 'E';
                
                if (charList[i] == 'Í')
                    charList[i] = 'I';
                
                if (charList[i] == 'Ö' || charList[i] == 'Ő' || charList[i] == 'Ó')
                    charList[i] = 'O';
                
                if (charList[i] == 'Ü' || charList[i] == 'Ű' || charList[i] == 'Ú')
                    charList[i] = 'U';
                returnString += charList[i];
            }

            // Checking for speical characters

            returnString = Regex.Replace(returnString, "[^A-Z]", "");

            return returnString;
        }
        static string connectStrings(string OpenText, string SecretText)
        {
            string returnString = "";

            // Lengthing the SecretText

            do {
                SecretText += SecretText;
            } while (SecretText.Length < OpenText.Length);

            // Making sure that the two strings are the same length

            List<char> SecretTextList = SecretText.ToCharArray().ToList();

            while (SecretTextList.Count != OpenText.Length)
                SecretTextList.RemoveAt(SecretTextList.Count - 1);
            
            // Adding together and returning the reformed string 

            for (int i = 0; i < SecretTextList.Count; i++)
                returnString += SecretTextList[i];

            return returnString.ToUpper();
        }
        static string codeIt(string OpenText ,string SecretText)
        {
            // Reading .dat file && creating a Jagged list to hold the values

            StreamReader objInput = new StreamReader("Vtabla.dat", System.Text.Encoding.Default);
            string contents = objInput.ReadToEnd().Trim();
            string[] split = System.Text.RegularExpressions.Regex.Split(contents, "\\s+", RegexOptions.None);

            List<List<string>> Vtabla = new List<List<string>>();
            for (int i = 0; i < split.Length; i++)
            {
                List<string> subVtabla = new List<string>();
                for (int j = 0; j < split[i].Length; j++)
                {
                    subVtabla.Add(split[i].Substring(j, 1));
                }
                Vtabla.Add(subVtabla);
            }

            // Classifing the OpenText

            string returnAsClassified = "";

            for (int i = 0; i < OpenText.Length; i++)
            {
                int columnIndex = 0;
                int rowIndex = 0;
                for (int j = 0; j < Vtabla.Count; j++)
                {
                    if (Vtabla[j][0] == OpenText[i].ToString())
                    {
                        columnIndex = j;
                    }
                }
                for (int k = 0; k < Vtabla[0].Count; k++)
                {
                    if(Vtabla[0][k] == SecretText[i].ToString())
                    {
                        rowIndex = k;
                    }
                }
                returnAsClassified += Vtabla[columnIndex][rowIndex];
            }
            return returnAsClassified;
        }
    }
}
