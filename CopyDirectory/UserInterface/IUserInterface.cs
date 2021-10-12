using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CopyDirectory.UserInterface
{
    public interface IUserInterface
    {
        public void ShowAskUserForDirectories(out string sourceDirectory, out string targetDirectory);
        public bool ShowAskOverwrite();

        public void ShowIntro();
        public void ShowProgress(int percent);
        public void ShowFileDetails(FileInfo file);

        public void ShowCompletion();
        public void ShowException(Exception e);


        public void ShowFileAlreadyExists(FileInfo file);

    }
}
