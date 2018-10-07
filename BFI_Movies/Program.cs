using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BFI_Movies
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            // Declare a variable for use with printing out the lines
            String line;
            // Also declare a list for holding the different lines
            List<string> rawData = new List<string>();

            // Use a try catch incase the file can't be found
            try
            {
                // Create a stream reader and pass it the file name
                StreamReader reader = new StreamReader("bfi-weekend-box-office-report-24-26-august-2018.csv");

                // Read the first line
                line = reader.ReadLine();

                // Continue to read the whole file
                while (line != null)
                {
                    // Add the line to the list
                    rawData.Add(line);
                    // Read the next line
                    line = reader.ReadLine();
                }
                // Close the file
                reader.Close();
            }
            // Catch the execption if there is one
            catch (Exception e)
            {
                // Print that execption out
                Console.WriteLine("Exception: " + e.Message);
            }

            // Declare a variable for holding the split string
            string[] filmInfo;
            // And another for holding the global average
            int globalAv = 0;
            int ukUsaAv = 0;
            int ukUsaCount = 0;

            // Loop through the 15 different films from position 3 in the list
            for (int i = 2; i < 17; i++)
            {
                // Print out the film and it's information
                // Console.WriteLine($"{rawData[i]}");
                // Split the info into an array for looking through
                filmInfo = rawData[i].Split(',');

                // Setup some variables for calculating weekend gross
                string filmGross = "";
                int increm = 3;
                // Here we get the last charcter to check if we need another index from the array
                char last = filmInfo[increm][filmInfo[increm].Length - 1];
                // Untill we reach the end of a number, keep adding the elements
                while (last != '"')
                {
                    // Get the last character from the element to check for a " mark
                    last = filmInfo[increm][filmInfo[increm].Length - 1];
                    // Strip everything from the element but numbers
                    filmInfo[increm] = Regex.Replace(filmInfo[increm], "[^0-9]+", string.Empty);
                    // Add the value left behind onto the average for that film
                    filmGross += filmInfo[increm];
                    // Increment up another element
                    increm += 1;
                }
                // Once we've got all the vaues needed from the film, add it to the global average
                globalAv += Int32.Parse(filmGross);
                // Check to see if it is of UK/USA origin
                if (filmInfo[2] == "UK/USA")
                {
                    // If yes, then add on the weekend gross to the total
                    ukUsaAv += Int32.Parse(filmGross);
                    // Increment the number of films of this orgin for division later
                    ukUsaCount++;
                }
            }
            // Once we've got all the films' weekend gross, divide the total by 15 for the average
            globalAv = globalAv / 15;
            // Output the average into the console
            Console.WriteLine("The average weekend gross for the top 15 films was: £{0}", globalAv.ToString("N0"));
            // Then we want to output the UK/USA origin totals
            ukUsaAv = ukUsaAv / ukUsaCount;
            Console.WriteLine("The average weekend gross for films of UK/USA origin is: £{0}", ukUsaAv.ToString("N0"));
            Console.ReadLine();
        }
    }
}
