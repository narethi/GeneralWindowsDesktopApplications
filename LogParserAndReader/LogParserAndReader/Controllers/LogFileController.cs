using LogParserAndReader.Models.LogFile;
using LogParserAndReader.Parsers;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;

namespace LogParserAndReader.Controllers
{
    public class LogFileController : INotifyPropertyChanged
    {
        public delegate void VoidHandler();
        public event VoidHandler? LogFileLoaded;

        public FileSystemWatcher? LogFileWatcher;

        private LogFile? _loadedFile;
        
        /// <summary>
        /// This is the log file object that is used in the log list control
        /// </summary>
        public LogFile LoadedFile
        {
            get => _loadedFile ?? new();
            set 
            {
                _loadedFile = value;
                if(LogFileLoaded != null)
                {
                    LogFileLoaded();
                }
                OnPropertyChanged();
            }
        }


        #region file watcher

        /// <summary>
        /// Cleans up the existing file watcher, and starts a new file watcher for the next opened file
        /// </summary>
        /// <param name="fileName">This is the name of the file to watch</param>
        private void SetupFileWatcher(string fileName)
        {
            ClearFileWatcher();

            var fileInfo = new FileInfo(fileName);
            if(fileInfo.Exists && fileInfo.Directory != null)
            {
                LogFileWatcher = new FileSystemWatcher(fileInfo.Directory.FullName);
                LogFileWatcher.NotifyFilter = NotifyFilters.Attributes
                                    | NotifyFilters.CreationTime
                                    | NotifyFilters.DirectoryName
                                    | NotifyFilters.FileName
                                    | NotifyFilters.LastAccess
                                    | NotifyFilters.LastWrite
                                    | NotifyFilters.Security
                                    | NotifyFilters.Size;

                LogFileWatcher.Changed += OnChanged;
                LogFileWatcher.Deleted += OnDeleted;
                LogFileWatcher.Error += OnError;

                LogFileWatcher.EnableRaisingEvents = true;
            }
        }

        /// <summary>
        /// Clears the current file watcher (Unsubscribe from events, and dispose the watcher)
        /// </summary>
        private void ClearFileWatcher()
        {
            if (LogFileWatcher != null)
            {
                LogFileWatcher.EnableRaisingEvents = false;

                LogFileWatcher.Changed -= OnChanged;
                LogFileWatcher.Deleted -= OnDeleted;
                LogFileWatcher.Error -= OnError;

                LogFileWatcher.Dispose();
            }
        }

        /// <summary>
        /// Error event for the directory the log file is in
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnError(object sender, ErrorEventArgs e)
        {
            ClearFileWatcher();
        }

        /// <summary>
        /// Deleted event for the directory the log file is in
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDeleted(object sender, FileSystemEventArgs e)
        {
            ClearFileWatcher();
        }

        /// <summary>
        /// Changed event for the directory the log file is in
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            //Only reload the file if the file that changed is the log file
            if(e.FullPath.ToLowerInvariant().Equals(_loadedFile?.LogFileName.ToLowerInvariant()))
            {
                Application.Current.Dispatcher.Invoke(() => LoadedFile = LogFileParser.ReadLogsFromFile(e.FullPath));
            }
        }

        #endregion

        /// <summary>
        /// Takes a log file name and creates a "LogFile" that can be used in the log file control 
        /// </summary>
        /// <param name="fileName">This is the name of the file that will be opened</param>
        public void ReadFile(string fileName)
        {
            LoadedFile = LogFileParser.ReadLogsFromFile(fileName);
            SetupFileWatcher(LoadedFile.LogFileName);
        }

        #region INotifyPropertyChanged Implementation

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
