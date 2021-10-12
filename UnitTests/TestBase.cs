using CopyDirectory;
using CopyDirectory.UserInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UnitTests
{
    public class TestBase
    {
     public DirectoryCopier Copier { get; set; }
        public DirectoryInfo Target { get; set; }
        public DirectoryInfo Source { get; set; }


        public TestBase()
        {
            var parent = Directory.GetParent(".").Parent.Parent.Parent.FullName;
            var source = parent + "\\test_source";
            var target = parent + "\\test_target";
            //Clean target directory so we have something clean to test from.
            Target = new DirectoryInfo(target);
            Source = new DirectoryInfo(source);
            foreach (FileInfo file in Target.GetFiles())
            {
                file.Delete();
            }
            //If I wanted to improve this I would do a test interface here
            Copier = new DirectoryCopier(new ConsoleInterface());
        }
    }
}
