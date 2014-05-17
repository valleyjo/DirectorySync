using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SkyDS
{

    public partial class SkyDS : Form
    {
        private string filepaths;
        private Dictionary<string, FileSystemWatcher> watchers = new Dictionary<string,FileSystemWatcher>();
        private string remoteDirectory;

        public SkyDS()
        {
            InitializeComponent();
        }

        private void SkyDSForm_Load(object sender, EventArgs e)
        {

        }

        private void chooseFilesToSync_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Multiselect = true;
            //dialog.Filter = "All files | *.* | Hidden files | .*";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string fullpath in dialog.FileNames)
                {
                    if (String.IsNullOrEmpty(fullpath))
                    {
                        continue;
                    }

                    string[] pathComponents = fullpath.Split('\\');
                    string filename = pathComponents[pathComponents.Length - 1];
                    string path = fullpath.TrimEnd(filename.ToCharArray());

                    watchers.Add(fullpath, new FileSystemWatcher(path, filename));
                }
            }
        }

        private void endWatch_Click(object sender, EventArgs e)
        {
            foreach (FileSystemWatcher watcher in watchers.Values)
            {
                watcher.EnableRaisingEvents = false;
            }

        }

        private void remoteMachineName_TextChanged(object sender, EventArgs e)
        {
            var versionNum = "17.500.7707.2003";
            remoteDirectory = "\\\\" + remoteMachineName.Text + "\\ServicesFE\\SkyWeb\\" + versionNum + "\\web\\bin\\";
        }

        private void PrintActivity(string header, Color color, string msg)
        {
            PrintActivity(new ActivityText(header, color, msg));
        }

        private void PrintActivity(ActivityText text)
        {
            if (activityArea.InvokeRequired)
            {
                activityArea.Invoke(new Action<ActivityText>(PrintActivity), text);
            }
            else
            {
                this.activityArea.ForeColor = text.Color;
                this.activityArea.AppendText(Environment.NewLine + Environment.NewLine + "----- " + text.Header + "-----------------------------");
                this.activityArea.ForeColor = Color.Black;
                this.activityArea.AppendText(Environment.NewLine + text.Msg);
            }
        }

        private void beginWatch_Click(object sender, EventArgs e)
        {
            PrintActivity("Copying to directory", Color.Green, "Copying to directory: " + remoteDirectory);

            foreach (FileSystemWatcher watcher in watchers.Values)
            {
                watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                                       | NotifyFilters.FileName | NotifyFilters.DirectoryName;
                watcher.Changed += OnChanged;
                watcher.Created += OnChanged;
                watcher.Deleted += OnDeleted;
                watcher.Renamed += OnRenamed;
                watcher.EnableRaisingEvents = true;

                PrintActivity("Now Watching", Color.Green, watcher.Filter);
            }
        }

        // Define the event handlers
        private void OnChanged(object source, FileSystemEventArgs e)
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

            PrintActivity(e.ChangeType.ToString(), Color.Green, remoteFullPath);
        }

        private void OnDeleted(object source, FileSystemEventArgs e)
        {
            var directoryComponents = e.FullPath.Split('\\');
            var fileName = directoryComponents[directoryComponents.Length - 1];
            var remoteFullPath = remoteDirectory + fileName;

            File.Delete(remoteFullPath);

            PrintActivity(e.ChangeType.ToString(), Color.Green, remoteFullPath);
        }

        private void OnRenamed(object source, RenamedEventArgs e)
        {
            var newDirectoryComponents = e.FullPath.Split('\\');
            var newFileName = newDirectoryComponents[newDirectoryComponents.Length - 1];
            var oldDirectoryComponents = e.OldFullPath.Split('\\');
            var oldFileName = oldDirectoryComponents[oldDirectoryComponents.Length - 1];

            var rename = "File: " + e.OldFullPath + "renamed to " + e.FullPath;
            File.Delete(remoteDirectory + oldFileName);
            File.Copy(e.FullPath, remoteDirectory + newFileName, true);

            PrintActivity(e.ChangeType.ToString(), Color.Green, rename);
        }
    }

    public class ActivityText
    {
        public Color Color { get; set; }
        public string Header { get; set; }
        public string Msg { get; set; }

        public ActivityText(string header, Color color, string msg)
        {
            this.Header = header;
            this.Color = color;
            this.Msg = msg;
        }
    }
}
