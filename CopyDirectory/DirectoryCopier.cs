using CopyDirectory.UserInterface;
using System;
using System.Collections.Generic;
using System.IO;

namespace CopyDirectory
{
    public class DirectoryCopier
    {
        public IUserInterface Interface { get; set; }

        //Using this constructor will make the UI ask user the directory
        public DirectoryCopier(IUserInterface _interface){
            Interface = _interface;
            Interface.ShowIntro();
        }

        //Asks the user in the UI to provide directory
        public void Start()
        {
            string source;
            string target;
            var overwrite = Interface.ShowAskOverwrite();
            Interface.ShowAskUserForDirectories(out source, out target);
            Copy(source, target, overwrite);
            Interface.ShowCompletion();
        }

        //No need to provide in UI
        public void Start(string sourceDirectory, string targetDirectory)
        {
            var overwrite = Interface.ShowAskOverwrite();
            Copy(sourceDirectory, targetDirectory, overwrite);
        }

 
        //Copies all files and folders from one directory to another. Option to disable overwrite.
        public void Copy(string sourceDirectory, string targetDirectory, bool overwrite)
        {
            try
            {
                if (!Directory.Exists(sourceDirectory))
                    throw new DirectoryNotFoundException();
                if (!Directory.Exists(targetDirectory))
                    Directory.CreateDirectory(targetDirectory);

                long totalSize = 0;
                var totalFiles = new DirectoryInfo(sourceDirectory).GetFiles();
                foreach (var file in totalFiles)
                {
                    totalSize += file.Length;
                }
                //How many bytes before progress is checked. 1mb would probably be better but leaving as 512kb to show progress updates.
                byte[] buffer = new byte[512 * 512];
                long bytesCopied = 0;

                //Copy files in the directory
                foreach (string filename in Directory.EnumerateFiles(sourceDirectory))
                {
                    using (FileStream sourceStream = File.Open(filename, FileMode.Open))
                    {
                        string targetFile = targetDirectory + filename.Substring(filename.LastIndexOf('\\'));
                        //If user doesn't want to overwrite and file already exists, go to next file.
                        if (!overwrite && File.Exists(targetFile)){
                            Interface.ShowFileAlreadyExists(new FileInfo(sourceStream.Name));
                            break;
                        }

                        Interface.ShowFileDetails(new FileInfo(sourceStream.Name));
                        long fileLength = sourceStream.Length;
                        using (FileStream destinationStream = File.Create(targetFile))
                        {
                            int currentBlockSize = 0;
                            while ((currentBlockSize = sourceStream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                bytesCopied += currentBlockSize;
                                UpdateProgress(bytesCopied, totalSize);
                               destinationStream.Write(buffer, 0, currentBlockSize);
                            }
                        }
                    }
                }
                //Now copy folders and files inside them.
                DirectoryInfo sourceDir = new DirectoryInfo(sourceDirectory);
                var subDirs = sourceDir.GetDirectories();
                foreach (DirectoryInfo subDir in subDirs)
                {
                    string tempPath = Path.Combine(targetDirectory, subDir.Name);
                    Copy(subDir.FullName, tempPath, overwrite);
                }
            }
            catch(Exception e)
            {
                HandleException(e);
            }
        }
        private void UpdateProgress(long current, long total)
        {
            int percentage = (int)((current * 100) / total);
            Interface.ShowProgress(percentage);
        }

        private void HandleException(Exception e)
        {
            Interface.ShowException(e);
        }


    }
}
