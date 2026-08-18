using System;
using System.IO;

namespace NLog.Internal.FileAppenders
{
	// Token: 0x02000087 RID: 135
	internal sealed class FileAppenderCache
	{
		// Token: 0x0600046D RID: 1133 RVA: 0x00009C85 File Offset: 0x00007E85
		private FileAppenderCache() : this(0, null, null)
		{
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00009C90 File Offset: 0x00007E90
		public FileAppenderCache(int size, IFileAppenderFactory appenderFactory, ICreateFileParameters createFileParams)
		{
			this.Size = size;
			this.Factory = appenderFactory;
			this.CreateFileParameters = createFileParams;
			this.appenders = new BaseFileAppender[this.Size];
			this.externalFileArchivingWatcher.OnChange += this.ExternalFileArchivingWatcher_OnChange;
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00009CEC File Offset: 0x00007EEC
		private void ExternalFileArchivingWatcher_OnChange(object sender, FileSystemEventArgs e)
		{
			if ((e.ChangeType & WatcherChangeTypes.Created) == WatcherChangeTypes.Created)
			{
				this.logFileWasArchived = true;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x00009D00 File Offset: 0x00007F00
		// (set) Token: 0x06000471 RID: 1137 RVA: 0x00009D08 File Offset: 0x00007F08
		public string ArchiveFilePatternToWatch
		{
			get
			{
				return this.archiveFilePatternToWatch;
			}
			set
			{
				if (this.archiveFilePatternToWatch != value)
				{
					this.archiveFilePatternToWatch = value;
					this.logFileWasArchived = false;
					this.externalFileArchivingWatcher.StopWatching();
				}
			}
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00009D31 File Offset: 0x00007F31
		public void InvalidateAppendersForInvalidFiles()
		{
			if (this.logFileWasArchived)
			{
				this.CloseAppenders();
				this.logFileWasArchived = false;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x00009D48 File Offset: 0x00007F48
		// (set) Token: 0x06000474 RID: 1140 RVA: 0x00009D50 File Offset: 0x00007F50
		public ICreateFileParameters CreateFileParameters { get; private set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x00009D59 File Offset: 0x00007F59
		// (set) Token: 0x06000476 RID: 1142 RVA: 0x00009D61 File Offset: 0x00007F61
		public IFileAppenderFactory Factory { get; private set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x00009D6A File Offset: 0x00007F6A
		// (set) Token: 0x06000478 RID: 1144 RVA: 0x00009D72 File Offset: 0x00007F72
		public int Size { get; private set; }

		// Token: 0x06000479 RID: 1145 RVA: 0x00009D7C File Offset: 0x00007F7C
		public BaseFileAppender AllocateAppender(string fileName)
		{
			BaseFileAppender baseFileAppender = null;
			int num = this.appenders.Length - 1;
			for (int i = 0; i < this.appenders.Length; i++)
			{
				if (this.appenders[i] == null)
				{
					num = i;
					break;
				}
				if (this.appenders[i].FileName == fileName)
				{
					BaseFileAppender baseFileAppender2 = this.appenders[i];
					for (int j = i; j > 0; j--)
					{
						this.appenders[j] = this.appenders[j - 1];
					}
					this.appenders[0] = baseFileAppender2;
					baseFileAppender = baseFileAppender2;
					break;
				}
			}
			if (baseFileAppender == null)
			{
				BaseFileAppender baseFileAppender3 = this.Factory.Open(fileName, this.CreateFileParameters);
				if (this.appenders[num] != null)
				{
					this.CloseAppender(this.appenders[num]);
					this.appenders[num] = null;
				}
				for (int k = num; k > 0; k--)
				{
					this.appenders[k] = this.appenders[k - 1];
				}
				this.appenders[0] = baseFileAppender3;
				baseFileAppender = baseFileAppender3;
				if (!string.IsNullOrEmpty(this.archiveFilePatternToWatch))
				{
					string fullPathForPattern = FileAppenderCache.GetFullPathForPattern(this.archiveFilePatternToWatch);
					string directoryName = Path.GetDirectoryName(fullPathForPattern);
					if (!Directory.Exists(directoryName))
					{
						Directory.CreateDirectory(directoryName);
					}
					this.externalFileArchivingWatcher.Watch(fullPathForPattern);
				}
			}
			return baseFileAppender;
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00009EB4 File Offset: 0x000080B4
		private static string GetFullPathForPattern(string pattern)
		{
			string fileName = Path.GetFileName(pattern);
			string text = pattern.Substring(0, pattern.Length - fileName.Length);
			if (string.IsNullOrEmpty(text))
			{
				text = ".";
			}
			return Path.Combine(Path.GetFullPath(text), fileName);
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00009EF8 File Offset: 0x000080F8
		public void CloseAppenders()
		{
			if (this.appenders != null)
			{
				for (int i = 0; i < this.appenders.Length; i++)
				{
					if (this.appenders[i] == null)
					{
						return;
					}
					this.CloseAppender(this.appenders[i]);
					this.appenders[i] = null;
				}
			}
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00009F44 File Offset: 0x00008144
		public void CloseAppenders(DateTime expireTime)
		{
			for (int i = 0; i < this.appenders.Length; i++)
			{
				if (this.appenders[i] == null)
				{
					return;
				}
				if (this.appenders[i].OpenTime < expireTime)
				{
					for (int j = i; j < this.appenders.Length; j++)
					{
						if (this.appenders[j] == null)
						{
							return;
						}
						this.CloseAppender(this.appenders[j]);
						this.appenders[j] = null;
					}
					return;
				}
			}
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00009FBC File Offset: 0x000081BC
		public void FlushAppenders()
		{
			foreach (BaseFileAppender baseFileAppender in this.appenders)
			{
				if (baseFileAppender == null)
				{
					return;
				}
				baseFileAppender.Flush();
			}
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00009FEC File Offset: 0x000081EC
		public FileCharacteristics GetFileCharacteristics(string fileName)
		{
			foreach (BaseFileAppender baseFileAppender in this.appenders)
			{
				if (baseFileAppender == null)
				{
					break;
				}
				if (baseFileAppender.FileName == fileName)
				{
					return baseFileAppender.GetFileCharacteristics();
				}
			}
			return null;
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0000A030 File Offset: 0x00008230
		public void InvalidateAppender(string fileName)
		{
			for (int i = 0; i < this.appenders.Length; i++)
			{
				if (this.appenders[i] == null)
				{
					return;
				}
				if (this.appenders[i].FileName == fileName)
				{
					this.CloseAppender(this.appenders[i]);
					for (int j = i; j < this.appenders.Length - 1; j++)
					{
						this.appenders[j] = this.appenders[j + 1];
					}
					this.appenders[this.appenders.Length - 1] = null;
					return;
				}
			}
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0000A0B8 File Offset: 0x000082B8
		private void CloseAppender(BaseFileAppender appender)
		{
			appender.Close();
			this.externalFileArchivingWatcher.StopWatching();
		}

		// Token: 0x040000E8 RID: 232
		private BaseFileAppender[] appenders;

		// Token: 0x040000E9 RID: 233
		private string archiveFilePatternToWatch;

		// Token: 0x040000EA RID: 234
		private readonly MultiFileWatcher externalFileArchivingWatcher = new MultiFileWatcher(NotifyFilters.FileName);

		// Token: 0x040000EB RID: 235
		private bool logFileWasArchived;

		// Token: 0x040000EC RID: 236
		public static readonly FileAppenderCache Empty = new FileAppenderCache();
	}
}
