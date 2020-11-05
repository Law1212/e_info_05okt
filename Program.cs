using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace kodol
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1.Feladat

            Console.WriteLine("Adj meg egy nyilt szöveget: \n");
            string openText = Console.ReadLine();

            // 2.Feladat

            openText = inEnglish(openText);

            // 3.Feladat

            Console.WriteLine("\nNyílt szöveg: \n");
            Console.WriteLine(openText + "\n");

            // 4. Feladat

            Console.WriteLine("Adjon meg egy 1-5 karakter hosszú szöveget, ami csak az angol abc betüit tartalmazza! \n");
            string secretText = Console.ReadLine();
            secretText.ToUpper();

            // 5. Feladat

            secretText = connectStrings(openText, secretText);
            Console.WriteLine("Secret text: " + secretText + "\n");


            // 6. Feladat

            Console.WriteLine("A titkosított szöveg: " + codeIt(openText ,secretText));

        }
        // *2.Feladat
        static string inEnglish(string input)
        {
            input = input.ToLower();
            List<char> charList = input.ToCharArray().ToList();

            for(int i = 0; i < charList.Count; i++)
            {
                if(charList[i] == 'á')
                {
                    charList[i] = 'a';
                }
                if (charList[i] == 'é')
                {
                    charList[i] = 'e';
                }
                if (charList[i] == 'í')
                {
                    charList[i] = 'i';
                }
                if (charList[i] == 'ö' || charList[i] == 'ő' || charList[i] == 'ó')
                {
                    charList[i] = 'o';
                }
                if (charList[i] == 'ü' || charList[i] == 'ű' || charList[i] == 'ú')
                {
                    charList[i] = 'u';
                }

            }
            string returnString = "";
            for (int j = 0; j < charList.Count; j++)
                returnString += charList[j];

            return returnString.ToUpper();
        }
                              
        static string connectStrings(string OpenText, string Bemenet)
        {

            do {
                Bemenet += Bemenet;
            } while (Bemenet.Length < OpenText.Length);
                
            List<char> bemenetList = Bemenet.ToCharArray().ToList();

            do {
                bemenetList.RemoveAt(bemenetList.Count - 1);
            } while (bemenetList.Count > OpenText.Length);

            string returnString = "";

            for (int i = 0; i < bemenetList.Count; i++)
                returnString += bemenetList[i];

            return returnString.ToUpper();
        }
        static string codeIt(string OpenText ,string SecretText)
        {
            // Reading .dat && creating a Jagged list to hold the data

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
            }
            return returnAsClassified;
        }
    }
}
