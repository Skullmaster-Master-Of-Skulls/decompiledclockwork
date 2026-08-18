using System;
using System.Configuration;
using System.IO;
using System.Security.Permissions;
using System.Threading;
using System.Xml.Linq;

namespace OracleInternal.Common
{
	// Token: 0x0200009A RID: 154
	[ConfigurationPermission(SecurityAction.Assert, Unrestricted = true)]
	internal class FileWatcher : IDisposable
	{
		// Token: 0x0600069D RID: 1693 RVA: 0x0003A648 File Offset: 0x00038848
		internal FileWatcher(string path)
		{
			if (!string.IsNullOrWhiteSpace(path) && File.Exists(path))
			{
				this.m_watcher = new FileSystemWatcher
				{
					Path = Path.GetDirectoryName(path),
					Filter = Path.GetFileName(path),
					NotifyFilter = NotifyFilters.LastWrite
				};
				this.m_watcher.Changed += this.OnFileChangedNotificationReceived;
				this.m_watcher.EnableRaisingEvents = true;
				this.m_setUp = true;
				return;
			}
			this.m_setUp = false;
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0003A6CC File Offset: 0x000388CC
		private void OnFileChangedNotificationReceived(object sender, FileSystemEventArgs e)
		{
			string fullPath = e.FullPath;
			DateTime lastWriteTime = File.GetLastWriteTime(fullPath);
			if (lastWriteTime == this.m_lastWriteTime)
			{
				return;
			}
			this.m_lastWriteTime = lastWriteTime;
			string text = null;
			try
			{
				text = XElement.Load(fullPath).Element("oracle.manageddataaccess.client").ToString();
			}
			catch
			{
				text = null;
			}
			if (string.IsNullOrWhiteSpace(text))
			{
				return;
			}
			int hashCode = text.GetHashCode();
			if (this.m_hashCode == hashCode)
			{
				return;
			}
			this.m_hashCode = hashCode;
			this.m_watcher.EnableRaisingEvents = false;
			ThreadPool.QueueUserWorkItem(new WaitCallback(this.OnAppConfigFileChanged));
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0003A770 File Offset: 0x00038970
		private void OnAppConfigFileChanged(object state)
		{
			try
			{
				ProviderConfig.OnAppConfigFileChanged();
			}
			catch
			{
			}
			finally
			{
				if (this.m_watcher != null)
				{
					this.m_watcher.EnableRaisingEvents = true;
				}
			}
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0003A7BC File Offset: 0x000389BC
		public void Dispose()
		{
			if (this.m_watcher == null)
			{
				return;
			}
			try
			{
				this.m_watcher.EnableRaisingEvents = false;
				this.m_watcher.Changed -= this.OnFileChangedNotificationReceived;
				this.m_watcher.Dispose();
			}
			catch
			{
			}
			finally
			{
				this.m_watcher = null;
			}
		}

		// Token: 0x0400084A RID: 2122
		private FileSystemWatcher m_watcher;

		// Token: 0x0400084B RID: 2123
		private DateTime m_lastWriteTime;

		// Token: 0x0400084C RID: 2124
		private int m_hashCode;

		// Token: 0x0400084D RID: 2125
		internal bool m_setUp;
	}
}
