using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

// Test nyiltszöveg: "Ez a próba szöveg, amit kódolunk!"
// Test titkosszöveg: "auto"
// Test right result: ETTDRIUOSTHJEATAINDCDIEINE

// Filename: "Vtabla.dat"

namespace kodol
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1.Task

            Console.Write("// 1.Feladat \n");
            Console.WriteLine("Adj meg egy nyilt szöveget: ");
            string openText = Console.ReadLine();

            // 2.Task

            openText = clearUpText(openText);

            // 3.Task

            Console.WriteLine("\n// 2. & 3. Feladat");
            Console.WriteLine("Nyílt szöveg: " + openText + "\n");

            // 4. Task

            Console.Write("// 4.Feladat \n");
            Console.WriteLine("Adjon meg egy 1-5 karakter hosszú szöveget, ami csak az angol abc betüit tartalmazza!");
            string secretText = Console.ReadLine();
            secretText.ToUpper();

            // 5. Task

            Console.Write("\n// 5.Feladat \n");
            secretText = connectTextLengths(openText, secretText);
            Console.WriteLine("Secret text: " + secretText + "\n");

            // 6. Task

            Console.Write("// 6.Feladat \n");
            Console.WriteLine("A titkosított szöveg: " + encryptText(openText, secretText, readSpreadsheet("Vtabla.dat")));

        }
        static string clearUpText(string text)
        {
            string textToReturn = "";
            text = text.ToUpper();

            // Converting the text into a list of letters

            List<char> letters = text.ToCharArray().ToList();

            // Checking for hungarian uga buga characters

            for (int i = 0; i < letters.Count; i++)
            {
                if (letters[i] == 'Á')
                    letters[i] = 'A';

                if (letters[i] == 'É')
                    letters[i] = 'E';

                if (letters[i] == 'Í')
                    letters[i] = 'I';

                if (letters[i] == 'Ö' || letters[i] == 'Ő' || letters[i] == 'Ó')
                    letters[i] = 'O';

                if (letters[i] == 'Ü' || letters[i] == 'Ű' || letters[i] == 'Ú')
                    letters[i] = 'U';
                textToReturn += letters[i];
            }

            // Checking for specical characters like: ';' | ',' | '.'

            textToReturn = Regex.Replace(textToReturn, "[^A-Z]", "");

            return textToReturn;
        }
        static string connectTextLengths(string textToLookUpTo, string textToChange)
        {
            string textToReturn = "";

            // Lengthening the textToChange

            if (textToLookUpTo.Length != textToChange.Length)
            {
                do
                {
                    textToChange += textToChange;
                } while (textToChange.Length < textToLookUpTo.Length);
            }

            // Removing extra letters at the end of the textToChange

            List<char> lettersOfTextToChange = textToChange.ToCharArray().ToList();

            while (lettersOfTextToChange.Count != textToLookUpTo.Length)
                lettersOfTextToChange.RemoveAt(lettersOfTextToChange.Count - 1);
            
            // Converting the letters into a complete text

            for (int i = 0; i < lettersOfTextToChange.Count; i++)
                textToReturn += lettersOfTextToChange[i];

            return textToReturn.ToUpper();
        }
        static string[] readSpreadsheet(string fileName)
        {
            // Reading the outside file and making it easy to use

            StreamReader objInput = new StreamReader(fileName, System.Text.Encoding.Default);
            string textContentOfTheFile = objInput.ReadToEnd().Trim();
            string[] contantByRows = Regex.Split(textContentOfTheFile, "\\s+", RegexOptions.None);
            return contantByRows;
        }
        static string encryptText(string textToBeEncrypted ,string SecretText, string[] tableByRows)
        {
            string textEnryptedToReturn = "";

            // Getting the indexes of the specific letters

            for (int i = 0; i < textToBeEncrypted.Length; i++)
            {
                int columnIndex = 0;
                int rowIndex = 0;
                for (int k = 0; k < tableByRows.Length; k++)
                {
                    if (tableByRows[k].ElementAt(0) == textToBeEncrypted[i])
                        columnIndex = k;
                }
                for (int k = 0; k < tableByRows[0].Length; k++)
                {
                    if(tableByRows[0].ElementAt(k) == SecretText[i])
                        rowIndex = k;
                }

                // Adding the coded letter to the text to return

                textEnryptedToReturn += tableByRows[columnIndex][rowIndex];
            }
            return textEnryptedToReturn;
        }
    }
}
