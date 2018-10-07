﻿using System;
using System.IO;
using System.Collections.Generic;

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
                Console.ReadLine();
            }
            // Catch the execption if there is one
            catch (Exception e)
            {
                // Print that execption out
                Console.WriteLine("Exception: " + e.Message);
            }

            for (int i = 2; i < 17; i++)
            {
                Console.WriteLine($"{rawData[i]}");
            }
        }
    }
}
