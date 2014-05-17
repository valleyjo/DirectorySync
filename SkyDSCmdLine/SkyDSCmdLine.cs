using System;
using System.IO;
using System.Security.Permissions;

namespace SkyDS
{
    public class SkyDSCmdLine
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
            if (args.Length != 4)
            {
                // Display the proper way to call the program.
                Console.WriteLine("Usage: SkyDS.exe (Directory of DLL) (DLL file name) (remote machine name)");
                return;
            }

            // construct the full path for the remote location
            var versionNum = "17.500.7707.2003";
            remoteDirectory = "\\\\" + args[3] + "\\ServicesFE\\SkyWeb\\" + versionNum + "\\web\\bin\\";
            Console.WriteLine("Watching file: " + args[1] + args[2]);
            Console.WriteLine("Copying to directory: " + remoteDirectory);

            // Initialize the file watcher
            var dllWatcher = new FileSystemWatcher(args[1]);
            var pdbWatcher = new FileSystemWatcher(args[1]);

            /* Watch for changes in LastAccess and LastWrite times
             * and the renaming of files or directories. */
            dllWatcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                                   | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            pdbWatcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                                   | NotifyFilters.FileName | NotifyFilters.DirectoryName;

            // Only watch DLL files.
            dllWatcher.Filter = args[2] + ".dll";
            pdbWatcher.Filter = args[2] + ".pdb";
            Console.WriteLine("DLL filter: " + dllWatcher.Filter);
            Console.WriteLine("PDB filter: " + pdbWatcher.Filter);

            // Add event handlers
            dllWatcher.Changed += OnChanged;
            dllWatcher.Created += OnChanged;
            dllWatcher.Deleted += OnDeleted;
            dllWatcher.Renamed += OnRenamed;
            pdbWatcher.Changed += OnChanged;
            pdbWatcher.Created += OnChanged;
            pdbWatcher.Deleted += OnDeleted;
            pdbWatcher.Renamed += OnRenamed;

            // Begin watching
            dllWatcher.EnableRaisingEvents = true;
            pdbWatcher.EnableRaisingEvents = true;

            // Wait for the user to quit the program
            Console.WriteLine("Press \'q\' to quit Sky Directory Sync (SkyDS).");
            while (Console.Read() != 'q')
            {
            }
        }

        // Define the event handlers
        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            // Specify what is done when a file is changed, created, or deleted
            var directoryComponents = e.FullPath.Split('\\');
            var fileName = directoryComponents[directoryComponents.Length - 1];
            var remoteFullPath = remoteDirectory + fileName;

            var finished = false;
            while (!finished)
            {
                try
                {
                    File.Copy(e.FullPath, remoteFullPath, true);
                    finished = true;
                }

                catch (Exception ex)
                {

                }
            }

            printChangeEvent(fileName, e.ChangeType.ToString(), remoteFullPath);
        }

        private static void OnDeleted(object source, FileSystemEventArgs e)
        {
            var directoryComponents = e.FullPath.Split('\\');
            var fileName = directoryComponents[directoryComponents.Length - 1];
            var remoteFullPath = remoteDirectory + fileName;

            printChangeEvent(fileName, e.ChangeType.ToString(), remoteFullPath);

            File.Delete(remoteFullPath);
        }

        private static void OnRenamed(object source, RenamedEventArgs e)
        {
            var newDirectoryComponents = e.FullPath.Split('\\');
            var newFileName = newDirectoryComponents[newDirectoryComponents.Length - 1];
            var oldDirectoryComponents = e.OldFullPath.Split('\\');
            var oldFileName = oldDirectoryComponents[oldDirectoryComponents.Length - 1];

            Console.WriteLine("File: {0} renamed to {1}", e.OldFullPath, e.FullPath);
            File.Delete(remoteDirectory + oldFileName);
            File.Copy(e.FullPath, remoteDirectory + newFileName, true);
        }

        private static void printChangeEvent(string filename, string eventType, string remoteFullPath)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n-- " + eventType + " --------------------------------");
            Console.ResetColor();
            Console.WriteLine(filename);
            Console.WriteLine("Remote file destination is: " + remoteFullPath);
        }
    }
}
