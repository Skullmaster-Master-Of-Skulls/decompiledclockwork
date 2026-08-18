using System;
using System.Collections.Generic;
using System.IO;
using NLog.Common;

namespace NLog.Internal
{
	// Token: 0x02000097 RID: 151
	internal class MultiFileWatcher : IDisposable
	{
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x0000A681 File Offset: 0x00008881
		// (set) Token: 0x060004CB RID: 1227 RVA: 0x0000A689 File Offset: 0x00008889
		public NotifyFilters NotifyFilters { get; set; }

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060004CC RID: 1228 RVA: 0x0000A694 File Offset: 0x00008894
		// (remove) Token: 0x060004CD RID: 1229 RVA: 0x0000A6CC File Offset: 0x000088CC
		public event FileSystemEventHandler OnChange;

		// Token: 0x060004CE RID: 1230 RVA: 0x0000A701 File Offset: 0x00008901
		public MultiFileWatcher() : this(NotifyFilters.Attributes | NotifyFilters.Size | NotifyFilters.LastWrite | NotifyFilters.CreationTime | NotifyFilters.Security)
		{
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0000A70E File Offset: 0x0000890E
		public MultiFileWatcher(NotifyFilters notifyFilters)
		{
			this.NotifyFilters = notifyFilters;
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x0000A728 File Offset: 0x00008928
		public void Dispose()
		{
			this.StopWatching();
			GC.SuppressFinalize(this);
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0000A738 File Offset: 0x00008938
		public void StopWatching()
		{
			lock (this)
			{
				foreach (FileSystemWatcher watcher in this.watcherMap.Values)
				{
					this.StopWatching(watcher);
				}
				this.watcherMap.Clear();
			}
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x0000A7C0 File Offset: 0x000089C0
		public void StopWatching(string fileName)
		{
			lock (this)
			{
				FileSystemWatcher watcher;
				if (this.watcherMap.TryGetValue(fileName, out watcher))
				{
					this.StopWatching(watcher);
					this.watcherMap.Remove(fileName);
				}
			}
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0000A81C File Offset: 0x00008A1C
		private void StopWatching(FileSystemWatcher watcher)
		{
			InternalLogger.Info("Stopping file watching for path '{0}' filter '{1}'", new object[]
			{
				watcher.Path,
				watcher.Filter
			});
			watcher.EnableRaisingEvents = false;
			watcher.Dispose();
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0000A85C File Offset: 0x00008A5C
		public void Watch(IEnumerable<string> fileNames)
		{
			if (fileNames == null)
			{
				return;
			}
			foreach (string fileName in fileNames)
			{
				this.Watch(fileName);
			}
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0000A8A8 File Offset: 0x00008AA8
		internal void Watch(string fileName)
		{
			string directoryName = Path.GetDirectoryName(fileName);
			if (!Directory.Exists(directoryName))
			{
				InternalLogger.Warn("Cannot watch {0} for changes as it doesn't exist", new object[]
				{
					directoryName
				});
				return;
			}
			lock (this)
			{
				if (!this.watcherMap.ContainsKey(fileName))
				{
					FileSystemWatcher fileSystemWatcher = new FileSystemWatcher
					{
						Path = directoryName,
						Filter = Path.GetFileName(fileName),
						NotifyFilter = this.NotifyFilters
					};
					fileSystemWatcher.Created += this.OnWatcherChanged;
					fileSystemWatcher.Changed += this.OnWatcherChanged;
					fileSystemWatcher.Deleted += this.OnWatcherChanged;
					fileSystemWatcher.EnableRaisingEvents = true;
					InternalLogger.Info("Watching path '{0}' filter '{1}' for changes.", new object[]
					{
						fileSystemWatcher.Path,
						fileSystemWatcher.Filter
					});
					this.watcherMap.Add(fileName, fileSystemWatcher);
				}
			}
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0000A9B4 File Offset: 0x00008BB4
		private void OnWatcherChanged(object source, FileSystemEventArgs e)
		{
			if (this.OnChange != null)
			{
				this.OnChange(source, e);
			}
		}

		// Token: 0x040000FC RID: 252
		private Dictionary<string, FileSystemWatcher> watcherMap = new Dictionary<string, FileSystemWatcher>();
	}
}
