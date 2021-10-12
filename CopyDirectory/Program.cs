using CopyDirectory;
using CopyDirectory.UserInterface;
using System;
using System.IO;

namespace FileCopy
{
    class Program
    {
        static void Main(string[] args)
        {
            var copier = new DirectoryCopier(new ConsoleInterface());
            var parent = Directory.GetParent(".").Parent.Parent.Parent.FullName;
            var source = parent + "\\source";
            var target = parent + "\\target";
            copier.Start(source, target);
        }
    }
}
