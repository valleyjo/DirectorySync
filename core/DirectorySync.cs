using System;
using System.IO;
using System.Security.Permissions;


namespace DirectorySync
{
    public class DirectorySync
    {
        private static string remoteDirectory;

        public static void Main()
        {
            Run();
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public static void Run()
        {
            string[] args = System.Environment.GetCommandLineArgs();

            // Validate the proper arguments were provided
            if (args.Length != 3)
            {
                // Display the proper way to call the program.
                Console.WriteLine("Usage: SkyCpCLI.exe (directory) (remote machine name)");
                return;
            }

            // construct the full path for the remote location
            var versionNum = "17.500.7707.2003";
            remoteDirectory = "\\\\" + args[2] + "\\ServicesFE\\SkyWeb\\" + versionNum + "\\web\\bin\\";
            Console.WriteLine("Watching directory: " + args[1]);
            Console.WriteLine("Copying to directory: " + remoteDirectory);

            // Initialize the file watcher
            var watcher = new FileSystemWatcher(args[1]);

            /* Watch for changes in LastAccess and LastWrite times
             * and the renaming of files or directories. */
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                                   | NotifyFilters.FileName | NotifyFilters.DirectoryName;

            // Only watch DLL files.
            //watcher.Filter = "*.txt";

            // Add event handlers
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);
            watcher.Renamed += new RenamedEventHandler(OnRenamed);

            // Begin watching
            watcher.EnableRaisingEvents = true;

            // Wait for the user to quit the program
            Console.WriteLine("Press \'q\' to quit the sample.");
            while (Console.Read() != 'q') ;
        }

        // Define the event handlers
        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            // Specify what is done when a file is changed, created, or deleted
            Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);
            var directoryComponents = e.FullPath.Split('\\');
            var fileName = directoryComponents[directoryComponents.Length - 1];
            var remoteFullPath = remoteDirectory + fileName;
            Console.WriteLine("Remote file destination is: ", remoteFullPath);

            //File.Copy(e.FullPath, remoteFullPath, true);
        }

        private static void OnRenamed(object source, RenamedEventArgs e)
        {
            // Specify what is done when a file is renamed
            Console.WriteLine("File: {0} renamed to {1}", e.OldFullPath, e.FullPath);
        }
    }
}
