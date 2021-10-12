using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CopyDirectory.UserInterface
{
    public class ConsoleInterface : IUserInterface
    {

        //If I wanted to improve this I would move the text to another file that the code would read from, allowing the text to remain
        // the same between UI, without coding in the text for each interface
        public void ShowIntro()
        {
            Console.WriteLine("Welcome to Directory Copier 2021! This application will allow you to copy all the files in a folder to a new folder! \n\n");
        }


        public void ShowAskUserForDirectories(out string sourceDirectory, out string targetDirectory)
        {
            Console.WriteLine("Please specify a source directory, and a target directory.");
            Console.Write("Source Directory: ");
            sourceDirectory = Console.ReadLine();
            Console.Write("Target Directory: ");
            targetDirectory = Console.ReadLine();
        }

        public bool ShowAskOverwrite()
        {
            Console.WriteLine("Would you like to overwrite existing files? (Y/N)");
           var response = Console.ReadKey();
            Console.WriteLine("");
            if (response.Key == ConsoleKey.Y)
                return true;
            else if (response.Key == ConsoleKey.N)
                return false;
            else
            {
                Console.WriteLine("Please enter either Y or N.");
                return ShowAskOverwrite();
            }
        }

        public void ShowProgress(int percent)
        {
            Console.WriteLine("Current progress in folder is " + percent + "%.");

        }

        public void ShowFileAlreadyExists(FileInfo file)
        {
            Console.WriteLine(file.Name + " already exists at " + file.FullName + ", skipping.");
        }

        public void ShowFileDetails(FileInfo file)
        {
            Console.WriteLine("Copying " + file.Name + " from " + file.FullName);
        }

        public void ShowException(Exception e)
        {
            Console.WriteLine(e.Message);
        }

        public void ShowCompletion()
        {
            Console.WriteLine("Your files have been copied successfully!");
        }

    }
}
