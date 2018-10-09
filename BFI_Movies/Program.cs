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
            // Also declare a list for holding the different lines
            List<string> rawData = new List<string>();

			// Try to read the data from the resource
			try
			{
				// Use the BFI csv saved as a resource and split it on each line
				string[] films = Properties.Resources.bfi_weekend_box_office_report_24_26_august_2018.Split('\n');
				// Loop through each line and add it to the list
				foreach (string film in films)
				{
					// Add the line to the list
					rawData.Add(film);
				}
			}
			// Incase there is a problem reading the resource
			catch (Exception e)
			{
				// Print that execption out
				Console.WriteLine("Exception: " + e.Message);
				// Pause the program for the user to ready the exception
				Console.ReadLine();
				// Close the application once a key is pressed
				Environment.Exit(0);
			}

            // Declare a variable for holding the split string
            string[] filmInfo;
            // And another for holding the global average
            int globalAv = 0;
            int ukUsaAv = 0;
            int ukUsaCount = 0;
            bool firstChecked = false;
            int viewings = 0;

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
                // Now we need to figure out how many views there were but for the first film only
                if (firstChecked == false)
                {
                    // Divide the total gross by 8 (1 viewing = £8)
                    viewings = Int32.Parse(filmGross) / 8;
                    // We'll swap a boolean over so we only check the first iteration
                    firstChecked = true;
                }
            }
            // Once we've got all the films' weekend gross, divide the total by 15 for the average
            globalAv = globalAv / 15;
            // Output the average into the console
            Console.WriteLine("The average weekend gross for the top 15 films was: £{0}", globalAv.ToString("N0"));
            // Then we want to output the UK/USA origin totals
            ukUsaAv = ukUsaAv / ukUsaCount;
            Console.WriteLine("The average weekend gross for films of UK/USA origin is: £{0}", ukUsaAv.ToString("N0"));
            // Output how many tickets were sold for the Disney film
            Console.WriteLine("Disney’s Christopher Robin had {0} viewings this weekend", viewings);
            // Output the previous weekend gross for the Disney film
            int disneyGross = (2138824 / ((100 - 16))) * 100;
            Console.WriteLine("Last weekend, Disney's Christopher Robin grossed £{0}", disneyGross.ToString("N0"));
            // Output the previous weekend gross for the Mamma Mia film
            int mammaGross = (1701162 / ((100 - 24))) * 100;
            Console.WriteLine("Last weekend, Mamma Mia: Here we Go Again! grossed £{0}", mammaGross.ToString("N0"));
            Console.ReadLine();
        }
    }
}