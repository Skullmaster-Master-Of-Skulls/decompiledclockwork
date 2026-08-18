using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using NLog.Common;
using NLog.Config;
using NLog.Internal;
using NLog.Internal.FileAppenders;
using NLog.Layouts;
using NLog.Time;

namespace NLog.Targets
{
	// Token: 0x0200015B RID: 347
	[Target("File")]
	public class FileTarget : TargetWithLayoutHeaderAndFooter, ICreateFileParameters
	{
		// Token: 0x06000C97 RID: 3223 RVA: 0x0001D420 File Offset: 0x0001B620
		public FileTarget()
		{
			this.ArchiveNumbering = ArchiveNumberingMode.Sequence;
			this.maxArchiveFiles = 0;
			this.ConcurrentWriteAttemptDelay = 1;
			this.ArchiveEvery = FileArchivePeriod.None;
			this.ArchiveAboveSize = -1L;
			this.ConcurrentWriteAttempts = 10;
			this.ConcurrentWrites = true;
			this.Encoding = Encoding.Default;
			this.BufferSize = 32768;
			this.AutoFlush = true;
			this.FileAttributes = Win32FileAttributes.Normal;
			this.LineEnding = LineEndingMode.Default;
			this.EnableFileDelete = true;
			this.OpenFileCacheTimeout = -1;
			this.OpenFileCacheSize = 5;
			this.CreateDirs = true;
			this.fileArchive = new FileTarget.DynamicFileArchive(this, this.MaxArchiveFiles);
			this.ForceManaged = false;
			this.ArchiveDateFormat = string.Empty;
			this.maxLogFilenames = 20;
			this.previousFileNames = new Queue<string>(this.maxLogFilenames);
			this.fileAppenderCache = FileAppenderCache.Empty;
			this.CleanupFileName = true;
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x0001D56B File Offset: 0x0001B76B
		public FileTarget(string name) : this()
		{
			base.Name = name;
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000C9A RID: 3226 RVA: 0x0001D57A File Offset: 0x0001B77A
		// (set) Token: 0x06000C9B RID: 3227 RVA: 0x0001D582 File Offset: 0x0001B782
		[RequiredParameter]
		public Layout FileName
		{
			get
			{
				return this.fileName;
			}
			set
			{
				this.fileName = value;
				if (base.IsInitialized)
				{
					this.SetCachedCleanedFileNamed(value);
					this.RefreshFileArchive();
					this.RefreshArchiveFilePatternToWatch();
				}
			}
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x0001D5A8 File Offset: 0x0001B7A8
		private void SetCachedCleanedFileNamed(Layout value)
		{
			SimpleLayout simpleLayout = value as SimpleLayout;
			if (simpleLayout != null && simpleLayout.IsFixedText)
			{
				this.cachedCleanedFileNamed = this.CleanupInvalidFileNameChars(simpleLayout.FixedText);
			}
			else
			{
				this.cachedCleanedFileNamed = null;
			}
			this.fileName = value;
			if (base.IsInitialized)
			{
				this.RefreshFileArchive();
				this.RefreshArchiveFilePatternToWatch();
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000C9D RID: 3229 RVA: 0x0001D5FD File Offset: 0x0001B7FD
		// (set) Token: 0x06000C9E RID: 3230 RVA: 0x0001D605 File Offset: 0x0001B805
		[DefaultValue(true)]
		public bool CleanupFileName { get; set; }

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000C9F RID: 3231 RVA: 0x0001D60E File Offset: 0x0001B80E
		// (set) Token: 0x06000CA0 RID: 3232 RVA: 0x0001D616 File Offset: 0x0001B816
		[Advanced]
		[DefaultValue(true)]
		public bool CreateDirs { get; set; }

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x0001D61F File Offset: 0x0001B81F
		// (set) Token: 0x06000CA2 RID: 3234 RVA: 0x0001D627 File Offset: 0x0001B827
		[DefaultValue(false)]
		public bool DeleteOldFileOnStartup { get; set; }

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000CA3 RID: 3235 RVA: 0x0001D630 File Offset: 0x0001B830
		// (set) Token: 0x06000CA4 RID: 3236 RVA: 0x0001D638 File Offset: 0x0001B838
		[DefaultValue(false)]
		[Advanced]
		public bool ReplaceFileContentsOnEachWrite { get; set; }

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000CA5 RID: 3237 RVA: 0x0001D641 File Offset: 0x0001B841
		// (set) Token: 0x06000CA6 RID: 3238 RVA: 0x0001D649 File Offset: 0x0001B849
		[DefaultValue(false)]
		public bool KeepFileOpen
		{
			get
			{
				return this.keepFileOpen;
			}
			set
			{
				this.keepFileOpen = value;
				if (base.IsInitialized)
				{
					this.RefreshArchiveFilePatternToWatch();
				}
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000CA7 RID: 3239 RVA: 0x0001D660 File Offset: 0x0001B860
		// (set) Token: 0x06000CA8 RID: 3240 RVA: 0x0001D668 File Offset: 0x0001B868
		[DefaultValue(20)]
		public int maxLogFilenames { get; set; }

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000CA9 RID: 3241 RVA: 0x0001D671 File Offset: 0x0001B871
		// (set) Token: 0x06000CAA RID: 3242 RVA: 0x0001D679 File Offset: 0x0001B879
		[DefaultValue(true)]
		public bool EnableFileDelete { get; set; }

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000CAB RID: 3243 RVA: 0x0001D682 File Offset: 0x0001B882
		// (set) Token: 0x06000CAC RID: 3244 RVA: 0x0001D68A File Offset: 0x0001B88A
		[Advanced]
		public Win32FileAttributes FileAttributes { get; set; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000CAD RID: 3245 RVA: 0x0001D693 File Offset: 0x0001B893
		// (set) Token: 0x06000CAE RID: 3246 RVA: 0x0001D69B File Offset: 0x0001B89B
		[Advanced]
		public LineEndingMode LineEnding
		{
			get
			{
				return this.lineEndingMode;
			}
			set
			{
				this.lineEndingMode = value;
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000CAF RID: 3247 RVA: 0x0001D6A4 File Offset: 0x0001B8A4
		// (set) Token: 0x06000CB0 RID: 3248 RVA: 0x0001D6AC File Offset: 0x0001B8AC
		[DefaultValue(true)]
		public bool AutoFlush { get; set; }

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x0001D6B5 File Offset: 0x0001B8B5
		// (set) Token: 0x06000CB2 RID: 3250 RVA: 0x0001D6BD File Offset: 0x0001B8BD
		[Advanced]
		[DefaultValue(5)]
		public int OpenFileCacheSize { get; set; }

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000CB3 RID: 3251 RVA: 0x0001D6C6 File Offset: 0x0001B8C6
		// (set) Token: 0x06000CB4 RID: 3252 RVA: 0x0001D6CE File Offset: 0x0001B8CE
		[Advanced]
		[DefaultValue(-1)]
		public int OpenFileCacheTimeout { get; set; }

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000CB5 RID: 3253 RVA: 0x0001D6D7 File Offset: 0x0001B8D7
		// (set) Token: 0x06000CB6 RID: 3254 RVA: 0x0001D6DF File Offset: 0x0001B8DF
		[DefaultValue(32768)]
		public int BufferSize { get; set; }

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x0001D6E8 File Offset: 0x0001B8E8
		// (set) Token: 0x06000CB8 RID: 3256 RVA: 0x0001D6F0 File Offset: 0x0001B8F0
		public Encoding Encoding { get; set; }

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x0001D6F9 File Offset: 0x0001B8F9
		// (set) Token: 0x06000CBA RID: 3258 RVA: 0x0001D701 File Offset: 0x0001B901
		[DefaultValue(true)]
		public bool ConcurrentWrites
		{
			get
			{
				return this.concurrentWrites;
			}
			set
			{
				this.concurrentWrites = value;
				if (base.IsInitialized)
				{
					this.RefreshArchiveFilePatternToWatch();
				}
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000CBB RID: 3259 RVA: 0x0001D718 File Offset: 0x0001B918
		// (set) Token: 0x06000CBC RID: 3260 RVA: 0x0001D720 File Offset: 0x0001B920
		[DefaultValue(false)]
		public bool NetworkWrites { get; set; }

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000CBD RID: 3261 RVA: 0x0001D729 File Offset: 0x0001B929
		// (set) Token: 0x06000CBE RID: 3262 RVA: 0x0001D731 File Offset: 0x0001B931
		[Advanced]
		[DefaultValue(10)]
		public int ConcurrentWriteAttempts { get; set; }

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000CBF RID: 3263 RVA: 0x0001D73A File Offset: 0x0001B93A
		// (set) Token: 0x06000CC0 RID: 3264 RVA: 0x0001D742 File Offset: 0x0001B942
		[DefaultValue(1)]
		[Advanced]
		public int ConcurrentWriteAttemptDelay { get; set; }

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000CC1 RID: 3265 RVA: 0x0001D74B File Offset: 0x0001B94B
		// (set) Token: 0x06000CC2 RID: 3266 RVA: 0x0001D753 File Offset: 0x0001B953
		[DefaultValue(false)]
		public bool ArchiveOldFileOnStartup { get; set; }

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000CC3 RID: 3267 RVA: 0x0001D75C File Offset: 0x0001B95C
		// (set) Token: 0x06000CC4 RID: 3268 RVA: 0x0001D764 File Offset: 0x0001B964
		[DefaultValue("")]
		public string ArchiveDateFormat { get; set; }

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000CC5 RID: 3269 RVA: 0x0001D76D File Offset: 0x0001B96D
		// (set) Token: 0x06000CC6 RID: 3270 RVA: 0x0001D775 File Offset: 0x0001B975
		public long ArchiveAboveSize
		{
			get
			{
				return this.archiveAboveSize;
			}
			set
			{
				this.archiveAboveSize = value;
				if (base.IsInitialized)
				{
					this.RefreshArchiveFilePatternToWatch();
				}
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000CC7 RID: 3271 RVA: 0x0001D78C File Offset: 0x0001B98C
		// (set) Token: 0x06000CC8 RID: 3272 RVA: 0x0001D794 File Offset: 0x0001B994
		public FileArchivePeriod ArchiveEvery
		{
			get
			{
				return this.archiveEvery;
			}
			set
			{
				this.archiveEvery = value;
				if (base.IsInitialized)
				{
					this.RefreshArchiveFilePatternToWatch();
				}
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000CC9 RID: 3273 RVA: 0x0001D7AB File Offset: 0x0001B9AB
		// (set) Token: 0x06000CCA RID: 3274 RVA: 0x0001D7B3 File Offset: 0x0001B9B3
		public Layout ArchiveFileName
		{
			get
			{
				return this.archiveFileName;
			}
			set
			{
				this.archiveFileName = value;
				if (base.IsInitialized)
				{
					this.RefreshFileArchive();
					this.RefreshArchiveFilePatternToWatch();
				}
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000CCB RID: 3275 RVA: 0x0001D7D0 File Offset: 0x0001B9D0
		// (set) Token: 0x06000CCC RID: 3276 RVA: 0x0001D7D8 File Offset: 0x0001B9D8
		[DefaultValue(0)]
		public int MaxArchiveFiles
		{
			get
			{
				return this.maxArchiveFiles;
			}
			set
			{
				this.maxArchiveFiles = value;
				this.fileArchive.MaxArchiveFileToKeep = value;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000CCD RID: 3277 RVA: 0x0001D7ED File Offset: 0x0001B9ED
		// (set) Token: 0x06000CCE RID: 3278 RVA: 0x0001D7F5 File Offset: 0x0001B9F5
		public ArchiveNumberingMode ArchiveNumbering { get; set; }

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000CCF RID: 3279 RVA: 0x0001D7FE File Offset: 0x0001B9FE
		// (set) Token: 0x06000CD0 RID: 3280 RVA: 0x0001D805 File Offset: 0x0001BA05
		public static IFileCompressor FileCompressor { get; set; } = new ZipArchiveFileCompressor();

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000CD1 RID: 3281 RVA: 0x0001D80D File Offset: 0x0001BA0D
		// (set) Token: 0x06000CD2 RID: 3282 RVA: 0x0001D824 File Offset: 0x0001BA24
		[DefaultValue(false)]
		public bool EnableArchiveFileCompression
		{
			get
			{
				return this.enableArchiveFileCompression && FileTarget.FileCompressor != null;
			}
			set
			{
				this.enableArchiveFileCompression = value;
				if (base.IsInitialized)
				{
					this.RefreshArchiveFilePatternToWatch();
				}
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000CD3 RID: 3283 RVA: 0x0001D83B File Offset: 0x0001BA3B
		// (set) Token: 0x06000CD4 RID: 3284 RVA: 0x0001D843 File Offset: 0x0001BA43
		[DefaultValue(false)]
		public bool ForceManaged { get; set; }

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x0001D84C File Offset: 0x0001BA4C
		protected internal string NewLineChars
		{
			get
			{
				return this.lineEndingMode.NewLineCharacters;
			}
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x0001D85C File Offset: 0x0001BA5C
		private void RefreshFileArchive()
		{
			LogEventInfo logEventInfo = LogEventInfo.CreateNullEvent();
			string archiveFileNamePattern = this.GetArchiveFileNamePattern(this.GetCleanedFileName(logEventInfo), logEventInfo);
			if (archiveFileNamePattern == null)
			{
				InternalLogger.Debug("no RefreshFileArchive because fileName is NULL");
				return;
			}
			if (!FileTarget.ContainsFileNamePattern(archiveFileNamePattern))
			{
				try
				{
					this.fileArchive.InitializeForArchiveFolderPath(Path.GetDirectoryName(archiveFileNamePattern));
				}
				catch (Exception ex)
				{
					if (ex.MustBeRethrownImmediately())
					{
						throw;
					}
					InternalLogger.Warn(ex, "Error while initializing archive folder.");
				}
			}
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x0001D948 File Offset: 0x0001BB48
		private void RefreshArchiveFilePatternToWatch()
		{
			if (this.fileAppenderCache != null)
			{
				bool flag = this.IsArchivingEnabled() && this.ConcurrentWrites && this.KeepFileOpen;
				if (flag)
				{
					LogEventInfo logEventInfo = LogEventInfo.CreateNullEvent();
					string text = this.GetArchiveFileNamePattern(this.GetCleanedFileName(logEventInfo), logEventInfo);
					if (!string.IsNullOrEmpty(text))
					{
						text = Path.Combine(Path.GetDirectoryName(text), FileTarget.ReplaceFileNamePattern(text, "*"));
						this.fileAppenderCache.ArchiveFilePatternToWatch = text;
						if (this.EnableArchiveFileCompression && this.appenderInvalidatorThread == null)
						{
							this.appenderInvalidatorThread = new Thread(delegate()
							{
								try
								{
									IL_00:
									while (!this.stopAppenderInvalidatorThreadWaitHandle.WaitOne(200))
									{
										lock (base.SyncRoot)
										{
											this.fileAppenderCache.InvalidateAppendersForInvalidFiles();
										}
									}
								}
								catch (Exception ex)
								{
									InternalLogger.Debug(ex, "Exception in FileTarget appender-invalidator thread.");
									goto IL_00;
								}
							});
							this.appenderInvalidatorThread.IsBackground = true;
							this.appenderInvalidatorThread.Start();
							return;
						}
					}
				}
				else
				{
					this.fileAppenderCache.ArchiveFilePatternToWatch = null;
					this.StopAppenderInvalidatorThread();
				}
			}
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x0001DA18 File Offset: 0x0001BC18
		private void StopAppenderInvalidatorThread()
		{
			if (this.appenderInvalidatorThread != null)
			{
				this.stopAppenderInvalidatorThreadWaitHandle.Set();
				this.appenderInvalidatorThread = null;
			}
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x0001DA38 File Offset: 0x0001BC38
		public void CleanupInitializedFiles()
		{
			this.CleanupInitializedFiles(DateTime.UtcNow.AddDays(-2.0));
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x0001DA64 File Offset: 0x0001BC64
		public void CleanupInitializedFiles(DateTime cleanupThreshold)
		{
			List<string> list = new List<string>();
			foreach (KeyValuePair<string, DateTime> keyValuePair in this.initializedFiles)
			{
				if (keyValuePair.Value < cleanupThreshold)
				{
					list.Add(keyValuePair.Key);
				}
			}
			foreach (string text in list)
			{
				this.UninitializeFile(text);
			}
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x0001DB10 File Offset: 0x0001BD10
		protected override void FlushAsync(AsyncContinuation asyncContinuation)
		{
			try
			{
				this.fileAppenderCache.FlushAppenders();
				asyncContinuation(null);
			}
			catch (Exception exception)
			{
				if (exception.MustBeRethrown())
				{
					throw;
				}
				asyncContinuation(exception);
			}
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x0001DB58 File Offset: 0x0001BD58
		private IFileAppenderFactory GetFileAppenderFactory()
		{
			if (!this.KeepFileOpen)
			{
				return RetryingMultiProcessFileAppender.TheFactory;
			}
			if (this.NetworkWrites)
			{
				return RetryingMultiProcessFileAppender.TheFactory;
			}
			if (this.ConcurrentWrites)
			{
				return MutexMultiProcessFileAppender.TheFactory;
			}
			if (this.IsArchivingEnabled())
			{
				return CountingSingleProcessFileAppender.TheFactory;
			}
			return SingleProcessFileAppender.TheFactory;
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x0001DB97 File Offset: 0x0001BD97
		private bool IsArchivingEnabled()
		{
			return this.ArchiveAboveSize != -1L || this.ArchiveEvery != FileArchivePeriod.None;
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x0001DBB4 File Offset: 0x0001BDB4
		protected override void InitializeTarget()
		{
			base.InitializeTarget();
			this.SetCachedCleanedFileNamed(this.FileName);
			this.RefreshFileArchive();
			this.appenderFactory = this.GetFileAppenderFactory();
			this.fileAppenderCache = new FileAppenderCache(this.OpenFileCacheSize, this.appenderFactory, this);
			this.RefreshArchiveFilePatternToWatch();
			if ((this.OpenFileCacheSize > 0 || this.EnableFileDelete) && this.OpenFileCacheTimeout > 0)
			{
				this.autoClosingTimer = new Timer(new TimerCallback(this.AutoClosingTimerCallback), null, this.OpenFileCacheTimeout * 1000, this.OpenFileCacheTimeout * 1000);
			}
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x0001DC50 File Offset: 0x0001BE50
		protected override void CloseTarget()
		{
			base.CloseTarget();
			foreach (string text in new List<string>(this.initializedFiles.Keys))
			{
				this.UninitializeFile(text);
			}
			if (this.autoClosingTimer != null)
			{
				this.autoClosingTimer.Change(-1, -1);
				this.autoClosingTimer.Dispose();
				this.autoClosingTimer = null;
			}
			this.StopAppenderInvalidatorThread();
			this.fileAppenderCache.CloseAppenders();
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x0001DCEC File Offset: 0x0001BEEC
		protected override void Write(LogEventInfo logEvent)
		{
			string fullPath = Path.GetFullPath(this.GetCleanedFileName(logEvent));
			byte[] bytesToWrite = this.GetBytesToWrite(logEvent);
			this.ProcessLogEvent(logEvent, fullPath, bytesToWrite);
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x0001DD17 File Offset: 0x0001BF17
		internal string GetCleanedFileName(LogEventInfo logEvent)
		{
			if (this.FileName == null)
			{
				return null;
			}
			return this.cachedCleanedFileNamed ?? this.CleanupInvalidFileNameChars(this.FileName.Render(logEvent));
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x0001DD54 File Offset: 0x0001BF54
		protected override void Write(AsyncLogEventInfo[] logEvents)
		{
			Dictionary<string, List<AsyncLogEventInfo>> dictionary = logEvents.BucketSort((AsyncLogEventInfo c) => this.FileName.Render(c.LogEvent));
			using (MemoryStream memoryStream = new MemoryStream())
			{
				List<AsyncContinuation> list = new List<AsyncContinuation>();
				foreach (KeyValuePair<string, List<AsyncLogEventInfo>> keyValuePair in dictionary)
				{
					string fullPath = Path.GetFullPath(this.CleanupInvalidFileNameChars(keyValuePair.Key));
					memoryStream.SetLength(0L);
					memoryStream.Position = 0L;
					LogEventInfo logEventInfo = null;
					foreach (AsyncLogEventInfo asyncLogEventInfo in keyValuePair.Value)
					{
						if (logEventInfo == null)
						{
							logEventInfo = asyncLogEventInfo.LogEvent;
						}
						byte[] bytesToWrite = this.GetBytesToWrite(asyncLogEventInfo.LogEvent);
						memoryStream.Write(bytesToWrite, 0, bytesToWrite.Length);
						list.Add(asyncLogEventInfo.Continuation);
					}
					this.FlushCurrentFileWrites(fullPath, logEventInfo, memoryStream, list);
				}
			}
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x0001DE80 File Offset: 0x0001C080
		private void ProcessLogEvent(LogEventInfo logEvent, string fileName, byte[] bytesToWrite)
		{
			this.fileAppenderCache.InvalidateAppendersForInvalidFiles();
			string text = (this.GetFileCharacteristics(fileName) != null) ? fileName : this.previousLogFileName;
			if (this.ShouldAutoArchive(text, logEvent, bytesToWrite.Length))
			{
				this.DoAutoArchive(text, logEvent);
			}
			if (this.ArchiveNumbering == ArchiveNumberingMode.Date && this.ArchiveEvery != FileArchivePeriod.None && this.ShouldDeleteOldArchives() && !this.previousFileNames.Contains(fileName))
			{
				if (this.previousFileNames.Count > this.maxLogFilenames)
				{
					this.previousFileNames.Dequeue();
				}
				string archiveFileNamePattern = this.GetArchiveFileNamePattern(fileName, logEvent);
				if (archiveFileNamePattern != null)
				{
					this.DeleteOldDateArchives(archiveFileNamePattern);
				}
				this.previousFileNames.Enqueue(fileName);
			}
			this.WriteToFile(fileName, logEvent, bytesToWrite, false);
			this.previousLogFileName = fileName;
			this.previousLogEventTimestamp = new DateTime?(logEvent.TimeStamp);
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x0001DF47 File Offset: 0x0001C147
		protected virtual string GetFormattedMessage(LogEventInfo logEvent)
		{
			return this.Layout.Render(logEvent);
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x0001DF58 File Offset: 0x0001C158
		protected virtual byte[] GetBytesToWrite(LogEventInfo logEvent)
		{
			string s = this.GetFormattedMessage(logEvent) + this.NewLineChars;
			return this.TransformBytes(this.Encoding.GetBytes(s));
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x0001DF8A File Offset: 0x0001C18A
		protected virtual byte[] TransformBytes(byte[] value)
		{
			return value;
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x0001DF90 File Offset: 0x0001C190
		private static string ReplaceNumberPattern(string pattern, int value)
		{
			int num = pattern.IndexOf("{#", StringComparison.Ordinal);
			int num2 = pattern.IndexOf("#}", StringComparison.Ordinal) + 2;
			int totalWidth = num2 - num - 2;
			return pattern.Substring(0, num) + Convert.ToString(value, 10).PadLeft(totalWidth, '0') + pattern.Substring(num2);
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x0001DFE4 File Offset: 0x0001C1E4
		private void FlushCurrentFileWrites(string currentFileName, LogEventInfo firstLogEvent, MemoryStream ms, List<AsyncContinuation> pendingContinuations)
		{
			Exception exception = null;
			try
			{
				if (currentFileName != null)
				{
					this.ProcessLogEvent(firstLogEvent, currentFileName, ms.ToArray());
				}
			}
			catch (Exception ex)
			{
				if (ex.MustBeRethrown())
				{
					throw;
				}
				exception = ex;
			}
			foreach (AsyncContinuation asyncContinuation in pendingContinuations)
			{
				asyncContinuation(exception);
			}
			pendingContinuations.Clear();
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x0001E06C File Offset: 0x0001C26C
		private static bool ContainsFileNamePattern(string fileName)
		{
			int num = fileName.IndexOf("{#", StringComparison.Ordinal);
			int num2 = fileName.IndexOf("#}", StringComparison.Ordinal);
			return num != -1 && num2 != -1 && num < num2;
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x0001E0A4 File Offset: 0x0001C2A4
		private void RollArchivesForward(string fileName, string pattern, int archiveNumber)
		{
			if (this.ShouldDeleteOldArchives() && archiveNumber >= this.MaxArchiveFiles)
			{
				File.Delete(fileName);
				return;
			}
			if (!File.Exists(fileName))
			{
				return;
			}
			string newFileName = FileTarget.ReplaceNumberPattern(pattern, archiveNumber);
			this.RollArchivesForward(newFileName, pattern, archiveNumber + 1);
			if (archiveNumber == 0)
			{
				this.ArchiveFile(fileName, newFileName);
				return;
			}
			this.RollArchiveForward(fileName, newFileName);
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x0001E0FC File Offset: 0x0001C2FC
		private void RollArchiveForward(string existingFileName, string newFileName)
		{
			InternalLogger.Info("Roll archive {0} to {1}", new object[]
			{
				existingFileName,
				newFileName
			});
			File.Move(existingFileName, newFileName);
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x0001E12C File Offset: 0x0001C32C
		private void ArchiveBySequence(string fileName, string pattern)
		{
			FileTarget.FileNameTemplate fileNameTemplate = new FileTarget.FileNameTemplate(Path.GetFileName(pattern));
			int num = fileNameTemplate.Template.Length - fileNameTemplate.EndAt;
			string searchPattern = fileNameTemplate.ReplacePattern("*");
			string directoryName = Path.GetDirectoryName(Path.GetFullPath(pattern));
			int num2 = -1;
			int num3 = -1;
			Dictionary<int, string> dictionary = new Dictionary<int, string>();
			try
			{
				string[] files = Directory.GetFiles(directoryName, searchPattern);
				int i = 0;
				while (i < files.Length)
				{
					string text = files[i];
					string text2 = Path.GetFileName(text);
					string value = text2.Substring(fileNameTemplate.BeginAt, text2.Length - num - fileNameTemplate.BeginAt);
					int num4;
					try
					{
						num4 = Convert.ToInt32(value, CultureInfo.InvariantCulture);
					}
					catch (FormatException)
					{
						goto IL_BE;
					}
					goto IL_94;
					IL_BE:
					i++;
					continue;
					IL_94:
					num2 = Math.Max(num2, num4);
					num3 = ((num3 != -1) ? Math.Min(num3, num4) : num4);
					dictionary[num4] = text;
					goto IL_BE;
				}
				num2++;
			}
			catch (DirectoryNotFoundException)
			{
				Directory.CreateDirectory(directoryName);
				num2 = 0;
			}
			if (num3 != -1 && this.ShouldDeleteOldArchives())
			{
				int num5 = num2 - this.MaxArchiveFiles + 1;
				for (int j = num3; j < num5; j++)
				{
					string text3;
					if (dictionary.TryGetValue(j, out text3))
					{
						InternalLogger.Info("Deleting old archive {0}", new object[]
						{
							text3
						});
						File.Delete(text3);
					}
				}
			}
			string text4 = FileTarget.ReplaceNumberPattern(pattern, num2);
			this.ArchiveFile(fileName, text4);
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x0001E2A4 File Offset: 0x0001C4A4
		private void ArchiveFile(string fileName, string archiveFileName)
		{
			this.UninitializeFile(fileName);
			string directoryName = Path.GetDirectoryName(archiveFileName);
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			if (this.EnableArchiveFileCompression)
			{
				InternalLogger.Info("Archiving {0} to compressed {1}", new object[]
				{
					fileName,
					archiveFileName
				});
				FileTarget.FileCompressor.CompressFile(fileName, archiveFileName);
				FileTarget.DeleteAndWaitForFileDelete(fileName);
				return;
			}
			InternalLogger.Info("Archiving {0} to {1}", new object[]
			{
				fileName,
				archiveFileName
			});
			File.Move(fileName, archiveFileName);
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x0001E324 File Offset: 0x0001C524
		private static void DeleteAndWaitForFileDelete(string fileName)
		{
			DateTime creationTime = new FileInfo(fileName).CreationTime;
			File.Delete(fileName);
			if (File.Exists(fileName))
			{
				FileInfo fileInfo;
				do
				{
					Thread.Sleep(100);
					fileInfo = new FileInfo(fileName);
				}
				while (fileInfo.Exists && fileInfo.CreationTime == creationTime);
			}
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x0001E3AC File Offset: 0x0001C5AC
		private void ArchiveByDateAndSequence(string fileName, string pattern, LogEventInfo logEvent)
		{
			string text = Path.GetFileName(pattern);
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			FileTarget.FileNameTemplate fileNameTemplate = new FileTarget.FileNameTemplate(text);
			string text2 = fileNameTemplate.ReplacePattern("*");
			string archiveDateFormatString = this.GetArchiveDateFormatString(this.ArchiveDateFormat);
			string directoryName = Path.GetDirectoryName(Path.GetFullPath(pattern));
			if (string.IsNullOrEmpty(directoryName))
			{
				return;
			}
			int num = fileNameTemplate.EndAt - fileNameTemplate.BeginAt - 2;
			DateTime archiveDate = this.GetArchiveDate(fileName, logEvent);
			int num3;
			List<string> list;
			if (Directory.Exists(directoryName))
			{
				List<DateAndSequenceArchive> source = this.FindDateAndSequenceArchives(directoryName, fileName, text2, num, archiveDateFormatString, fileNameTemplate).ToList<DateAndSequenceArchive>();
				int? num2 = (from a in source
				where a.HasSameFormattedDate(archiveDate)
				select a).Max((DateAndSequenceArchive a) => new int?(a.Sequence));
				num3 = ((num2 != null) ? (num2 + 1) : new int?(0)).Value;
				list = (from a in source
				orderby a.Date, a.Sequence
				select a.FileName).ToList<string>();
			}
			else
			{
				Directory.CreateDirectory(directoryName);
				num3 = 0;
				list = new List<string>();
			}
			string arg = num3.ToString().PadLeft(num, '0');
			string path = text2.Replace("*", string.Format("{0}.{1}", archiveDate.ToString(archiveDateFormatString), arg));
			string item = Path.Combine(directoryName, path);
			this.ArchiveFile(fileName, item);
			list.Add(item);
			this.EnsureArchiveCount(list);
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x0001E5B0 File Offset: 0x0001C7B0
		private void EnsureArchiveCount(List<string> oldArchiveFileNames)
		{
			if (!this.ShouldDeleteOldArchives())
			{
				return;
			}
			int num = oldArchiveFileNames.Count - this.MaxArchiveFiles;
			for (int i = 0; i < num; i++)
			{
				InternalLogger.Info("Deleting old archive {0}.", new object[]
				{
					oldArchiveFileNames[i]
				});
				File.Delete(oldArchiveFileNames[i]);
			}
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x0001E940 File Offset: 0x0001CB40
		private IEnumerable<DateAndSequenceArchive> FindDateAndSequenceArchives(string dirName, string logFileName, string fileNameMask, int minSequenceLength, string dateFormat, FileTarget.FileNameTemplate fileTemplate)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(dirName);
			int archiveFileNameMinLength = fileNameMask.Length + minSequenceLength;
			IEnumerable<string> archiveFileNames = from n in FileTarget.GetFiles(directoryInfo, fileNameMask)
			where n.Name.Length >= archiveFileNameMinLength
			orderby n.CreationTime
			select n.FullName;
			foreach (string archiveFileName in archiveFileNames)
			{
				string archiveFileNameWithoutPath = Path.GetFileName(archiveFileName) ?? "";
				DateTime date;
				int sequence;
				if (FileTarget.TryParseDateAndSequence(archiveFileNameWithoutPath, dateFormat, fileTemplate, out date, out sequence) && !string.IsNullOrEmpty(archiveFileNameWithoutPath) && !archiveFileNameWithoutPath.Equals(Path.GetFileName(logFileName)))
				{
					yield return new DateAndSequenceArchive(archiveFileName, date, dateFormat, sequence);
				}
			}
			yield break;
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x0001E98C File Offset: 0x0001CB8C
		private static bool TryParseDateAndSequence(string archiveFileNameWithoutPath, string dateFormat, FileTarget.FileNameTemplate fileTemplate, out DateTime date, out int sequence)
		{
			int num = fileTemplate.Template.Length - fileTemplate.EndAt;
			int beginAt = fileTemplate.BeginAt;
			int length = archiveFileNameWithoutPath.Length - num - beginAt;
			string text = archiveFileNameWithoutPath.Substring(beginAt, length);
			int startIndex = text.LastIndexOf('.') + 1;
			string text2 = text.Substring(startIndex);
			if (!int.TryParse(text2, NumberStyles.None, CultureInfo.CurrentCulture, out sequence))
			{
				date = default(DateTime);
				return false;
			}
			string s = text.Substring(0, text.Length - text2.Length - 1);
			return DateTime.TryParseExact(s, dateFormat, CultureInfo.CurrentCulture, DateTimeStyles.None, out date);
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x0001EA24 File Offset: 0x0001CC24
		private static IEnumerable<FileInfo> GetFiles(DirectoryInfo directoryInfo, string fileNameMask)
		{
			return directoryInfo.GetFiles(fileNameMask);
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x0001EA2D File Offset: 0x0001CC2D
		private static string ReplaceFileNamePattern(string pattern, string replacementValue)
		{
			return new FileTarget.FileNameTemplate(Path.GetFileName(pattern)).ReplacePattern(replacementValue);
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x0001EA40 File Offset: 0x0001CC40
		private void ArchiveByDate(string fileName, string pattern, LogEventInfo logEvent)
		{
			string text = FileTarget.ReplaceFileNamePattern(pattern, "*");
			string directoryName = Path.GetDirectoryName(Path.GetFullPath(pattern));
			string archiveDateFormatString = this.GetArchiveDateFormatString(this.ArchiveDateFormat);
			DateTime archiveDate = this.GetArchiveDate(fileName, logEvent);
			if (directoryName != null)
			{
				string text2 = Path.Combine(directoryName, text.Replace("*", archiveDate.ToString(archiveDateFormatString)));
				this.ArchiveFile(fileName, text2);
			}
			this.DeleteOldDateArchives(pattern);
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x0001EABC File Offset: 0x0001CCBC
		private void DeleteOldDateArchives(string pattern)
		{
			if (!this.ShouldDeleteOldArchives())
			{
				return;
			}
			string text = FileTarget.ReplaceFileNamePattern(pattern, "*");
			string directoryName = Path.GetDirectoryName(Path.GetFullPath(pattern));
			string archiveDateFormatString = this.GetArchiveDateFormatString(this.ArchiveDateFormat);
			if (directoryName != null)
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(directoryName);
				if (!directoryInfo.Exists)
				{
					Directory.CreateDirectory(directoryName);
					return;
				}
				IEnumerable<string> enumerable = from n in directoryInfo.GetFiles(text)
				orderby n.CreationTime
				select n.FullName;
				List<string> list = new List<string>();
				foreach (string text2 in enumerable)
				{
					string text3 = Path.GetFileName(text2);
					int num = text.LastIndexOf('*');
					if (num + archiveDateFormatString.Length <= text3.Length)
					{
						string s = text3.Substring(num, archiveDateFormatString.Length);
						DateTime minValue = DateTime.MinValue;
						if (DateTime.TryParseExact(s, archiveDateFormatString, CultureInfo.InvariantCulture, DateTimeStyles.None, out minValue))
						{
							list.Add(text2);
						}
					}
				}
				this.EnsureArchiveCount(list);
			}
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x0001EC04 File Offset: 0x0001CE04
		private string GetArchiveDateFormatString(string defaultFormat)
		{
			string text = defaultFormat;
			if (string.IsNullOrEmpty(text))
			{
				switch (this.ArchiveEvery)
				{
				case FileArchivePeriod.Year:
					return "yyyy";
				case FileArchivePeriod.Month:
					return "yyyyMM";
				case FileArchivePeriod.Hour:
					return "yyyyMMddHH";
				case FileArchivePeriod.Minute:
					return "yyyyMMddHHmm";
				}
				text = "yyyyMMdd";
			}
			return text;
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x0001EC68 File Offset: 0x0001CE68
		private DateTime GetArchiveDate(string fileName, LogEventInfo logEvent)
		{
			FileCharacteristics fileCharacteristics = this.GetFileCharacteristics(fileName);
			DateTime dateTime = TimeSource.Current.FromSystemTime(fileCharacteristics.LastWriteTimeUtc);
			InternalLogger.Trace("Calculating archive date. Last write time: {0}; Previous log event time: {1}", new object[]
			{
				dateTime,
				this.previousLogEventTimestamp
			});
			bool flag = this.previousLogEventTimestamp != null && this.previousLogEventTimestamp.Value > dateTime;
			if (flag)
			{
				InternalLogger.Trace("Using previous log event time (is more recent)");
				return this.previousLogEventTimestamp.Value;
			}
			if (this.PreviousLogOverlappedPeriod(fileCharacteristics, logEvent))
			{
				InternalLogger.Trace("Using previous log event time (previous log overlapped period)");
				return this.previousLogEventTimestamp.Value;
			}
			InternalLogger.Trace("Using last write time");
			return dateTime;
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x0001ED20 File Offset: 0x0001CF20
		private bool PreviousLogOverlappedPeriod(FileCharacteristics fileCharacteristics, LogEventInfo logEvent)
		{
			if (this.previousLogEventTimestamp == null)
			{
				return false;
			}
			string archiveDateFormatString = this.GetArchiveDateFormatString(string.Empty);
			string a = TimeSource.Current.FromSystemTime(fileCharacteristics.LastWriteTimeUtc).ToString(archiveDateFormatString, CultureInfo.InvariantCulture);
			string b = logEvent.TimeStamp.ToString(archiveDateFormatString, CultureInfo.InvariantCulture);
			if (a != b)
			{
				return false;
			}
			DateTime dateTime;
			switch (this.ArchiveEvery)
			{
			case FileArchivePeriod.Year:
				dateTime = this.previousLogEventTimestamp.Value.AddYears(1);
				break;
			case FileArchivePeriod.Month:
				dateTime = this.previousLogEventTimestamp.Value.AddMonths(1);
				break;
			case FileArchivePeriod.Day:
				dateTime = this.previousLogEventTimestamp.Value.AddDays(1.0);
				break;
			case FileArchivePeriod.Hour:
				dateTime = this.previousLogEventTimestamp.Value.AddHours(1.0);
				break;
			case FileArchivePeriod.Minute:
				dateTime = this.previousLogEventTimestamp.Value.AddMinutes(1.0);
				break;
			default:
				return false;
			}
			string b2 = dateTime.ToString(archiveDateFormatString, CultureInfo.InvariantCulture);
			return a == b2;
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x0001EE5C File Offset: 0x0001D05C
		private void DoAutoArchive(string fileName, LogEventInfo eventInfo)
		{
			FileInfo fileInfo = new FileInfo(fileName);
			if (!fileInfo.Exists)
			{
				return;
			}
			string archiveFileNamePattern = this.GetArchiveFileNamePattern(fileName, eventInfo);
			if (archiveFileNamePattern == null)
			{
				InternalLogger.Warn("Skip auto archive because fileName is NULL");
				return;
			}
			if (!FileTarget.ContainsFileNamePattern(archiveFileNamePattern))
			{
				if (this.fileArchive.Archive(archiveFileNamePattern, fileInfo.FullName, this.CreateDirs) && this.initializedFiles.ContainsKey(fileInfo.FullName))
				{
					this.initializedFiles.Remove(fileInfo.FullName);
					return;
				}
			}
			else
			{
				switch (this.ArchiveNumbering)
				{
				case ArchiveNumberingMode.Sequence:
					this.ArchiveBySequence(fileInfo.FullName, archiveFileNamePattern);
					return;
				case ArchiveNumberingMode.Rolling:
					this.RollArchivesForward(fileInfo.FullName, archiveFileNamePattern, 0);
					return;
				case ArchiveNumberingMode.Date:
					this.ArchiveByDate(fileInfo.FullName, archiveFileNamePattern, eventInfo);
					return;
				case ArchiveNumberingMode.DateAndSequence:
					this.ArchiveByDateAndSequence(fileInfo.FullName, archiveFileNamePattern, eventInfo);
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x0001EF30 File Offset: 0x0001D130
		private string GetArchiveFileNamePattern(string fileName, LogEventInfo eventInfo)
		{
			if (this.ArchiveFileName == null)
			{
				string str = this.EnableArchiveFileCompression ? ".zip" : Path.GetExtension(fileName);
				return Path.ChangeExtension(fileName, ".{#}" + str);
			}
			string path = this.ArchiveFileName.Render(eventInfo);
			path = this.CleanupInvalidFileNameChars(path);
			return Path.GetFullPath(path);
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x0001EF88 File Offset: 0x0001D188
		private bool ShouldDeleteOldArchives()
		{
			return this.MaxArchiveFiles > 0;
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x0001EF93 File Offset: 0x0001D193
		private bool ShouldAutoArchive(string fileName, LogEventInfo ev, int upcomingWriteSize)
		{
			return fileName != null && (this.ShouldAutoArchiveBasedOnFileSize(fileName, upcomingWriteSize) || this.ShouldAutoArchiveBasedOnTime(fileName, ev));
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x0001EFB0 File Offset: 0x0001D1B0
		private bool ShouldAutoArchiveBasedOnFileSize(string fileName, int upcomingWriteSize)
		{
			if (this.ArchiveAboveSize == -1L)
			{
				return false;
			}
			FileCharacteristics fileCharacteristics = this.GetFileCharacteristics(fileName);
			return fileCharacteristics != null && fileCharacteristics.FileLength + (long)upcomingWriteSize > this.ArchiveAboveSize;
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x0001EFE8 File Offset: 0x0001D1E8
		private bool ShouldAutoArchiveBasedOnTime(string fileName, LogEventInfo logEvent)
		{
			if (this.ArchiveEvery == FileArchivePeriod.None)
			{
				return false;
			}
			FileCharacteristics fileCharacteristics = this.GetFileCharacteristics(fileName);
			if (fileCharacteristics == null)
			{
				return false;
			}
			DateTime dateTime = TimeSource.Current.FromSystemTime(fileCharacteristics.CreationTimeUtc);
			string archiveDateFormatString = this.GetArchiveDateFormatString(string.Empty);
			string a = dateTime.ToString(archiveDateFormatString, CultureInfo.InvariantCulture);
			string b = logEvent.TimeStamp.ToString(archiveDateFormatString, CultureInfo.InvariantCulture);
			return a != b;
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x0001F058 File Offset: 0x0001D258
		private void AutoClosingTimerCallback(object state)
		{
			lock (base.SyncRoot)
			{
				if (base.IsInitialized)
				{
					try
					{
						DateTime expireTime = DateTime.UtcNow.AddSeconds((double)(-(double)this.OpenFileCacheTimeout));
						this.fileAppenderCache.CloseAppenders(expireTime);
					}
					catch (Exception ex)
					{
						InternalLogger.Warn(ex, "Exception in AutoClosingTimerCallback.");
						if (ex.MustBeRethrown())
						{
							throw;
						}
					}
				}
			}
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x0001F0E8 File Offset: 0x0001D2E8
		private byte[] GetHeaderBytes()
		{
			return this.GetLayoutBytes(base.Header);
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x0001F0F6 File Offset: 0x0001D2F6
		private byte[] GetFooterBytes()
		{
			return this.GetLayoutBytes(base.Footer);
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x0001F104 File Offset: 0x0001D304
		private void WriteToFile(string fileName, LogEventInfo logEvent, byte[] bytes, bool justData)
		{
			if (this.ReplaceFileContentsOnEachWrite)
			{
				this.ReplaceFileContent(fileName, bytes, true);
				return;
			}
			bool flag = this.InitializeFile(fileName, logEvent, justData);
			BaseFileAppender baseFileAppender = this.fileAppenderCache.AllocateAppender(fileName);
			if (flag)
			{
				this.WriteHeader(baseFileAppender);
			}
			baseFileAppender.Write(bytes);
			if (this.AutoFlush)
			{
				baseFileAppender.Flush();
			}
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x0001F15C File Offset: 0x0001D35C
		private bool InitializeFile(string fileName, LogEventInfo logEvent, bool justData)
		{
			bool result = false;
			if (!justData)
			{
				DateTime utcNow = DateTime.UtcNow;
				if (!this.initializedFiles.ContainsKey(fileName))
				{
					this.ProcessOnStartup(fileName, logEvent);
					this.initializedFiles[fileName] = utcNow;
					this.initializedFilesCounter++;
					result = true;
					if (this.initializedFilesCounter >= 100)
					{
						this.initializedFilesCounter = 0;
						this.CleanupInitializedFiles();
					}
				}
				this.initializedFiles[fileName] = utcNow;
			}
			return result;
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x0001F1CC File Offset: 0x0001D3CC
		private void UninitializeFile(string fileName)
		{
			this.WriteFooter(fileName);
			this.fileAppenderCache.InvalidateAppender(fileName);
			this.initializedFiles.Remove(fileName);
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x0001F1F0 File Offset: 0x0001D3F0
		private void WriteFooter(string fileName)
		{
			byte[] footerBytes = this.GetFooterBytes();
			if (footerBytes != null && File.Exists(fileName))
			{
				this.WriteToFile(fileName, null, footerBytes, true);
			}
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x0001F21C File Offset: 0x0001D41C
		private void ProcessOnStartup(string fileName, LogEventInfo logEvent)
		{
			if (this.ArchiveOldFileOnStartup)
			{
				try
				{
					this.DoAutoArchive(fileName, logEvent);
				}
				catch (Exception ex)
				{
					InternalLogger.Warn(ex, "Unable to archive old log file '{0}'.", new object[]
					{
						fileName
					});
					if (ex.MustBeRethrown())
					{
						throw;
					}
				}
			}
			if (this.DeleteOldFileOnStartup)
			{
				try
				{
					File.Delete(fileName);
				}
				catch (Exception ex2)
				{
					InternalLogger.Warn(ex2, "Unable to delete old log file '{0}'.", new object[]
					{
						fileName
					});
					if (ex2.MustBeRethrown())
					{
						throw;
					}
				}
			}
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x0001F2B0 File Offset: 0x0001D4B0
		private void ReplaceFileContent(string fileName, byte[] bytes, bool firstAttempt)
		{
			try
			{
				using (FileStream fileStream = File.Create(fileName))
				{
					byte[] headerBytes = this.GetHeaderBytes();
					if (headerBytes != null)
					{
						fileStream.Write(headerBytes, 0, headerBytes.Length);
					}
					fileStream.Write(bytes, 0, bytes.Length);
					byte[] footerBytes = this.GetFooterBytes();
					if (footerBytes != null)
					{
						fileStream.Write(footerBytes, 0, footerBytes.Length);
					}
				}
			}
			catch (DirectoryNotFoundException)
			{
				if (!this.CreateDirs || !firstAttempt)
				{
					throw;
				}
				Directory.CreateDirectory(Path.GetDirectoryName(fileName));
				this.ReplaceFileContent(fileName, bytes, false);
			}
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x0001F348 File Offset: 0x0001D548
		private void WriteHeader(BaseFileAppender appender)
		{
			FileCharacteristics fileCharacteristics = appender.GetFileCharacteristics();
			if (fileCharacteristics == null || fileCharacteristics.FileLength == 0L)
			{
				byte[] headerBytes = this.GetHeaderBytes();
				if (headerBytes != null)
				{
					appender.Write(headerBytes);
				}
			}
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x0001F37C File Offset: 0x0001D57C
		private FileCharacteristics GetFileCharacteristics(string filePath)
		{
			FileCharacteristics fileCharacteristics = this.fileAppenderCache.GetFileCharacteristics(filePath);
			if (fileCharacteristics != null)
			{
				return fileCharacteristics;
			}
			FileInfo fileInfo = new FileInfo(filePath);
			if (fileInfo.Exists)
			{
				return new FileCharacteristics(fileInfo.CreationTimeUtc, fileInfo.LastWriteTimeUtc, fileInfo.Length);
			}
			return null;
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x0001F3C8 File Offset: 0x0001D5C8
		private byte[] GetLayoutBytes(Layout layout)
		{
			if (layout == null)
			{
				return null;
			}
			string s = layout.Render(LogEventInfo.CreateNullEvent()) + this.NewLineChars;
			return this.TransformBytes(this.Encoding.GetBytes(s));
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x0001F404 File Offset: 0x0001D604
		private string CleanupInvalidFileNameChars(string fileName)
		{
			if (!this.CleanupFileName)
			{
				return fileName;
			}
			int num = fileName.LastIndexOfAny(FileTarget.DirectorySeparatorChars);
			string text = fileName.Substring(num + 1);
			string path = (num > 0) ? fileName.Substring(0, num + 1) : string.Empty;
			char[] array = null;
			for (int i = 0; i < text.Length; i++)
			{
				if (FileTarget.InvalidFileNameChars.Contains(text[i]))
				{
					if (array == null)
					{
						array = text.ToCharArray();
					}
					array[i] = '_';
				}
			}
			if (array != null)
			{
				text = new string(array);
				return Path.Combine(path, text);
			}
			return fileName;
		}

		// Token: 0x04000342 RID: 834
		private const int InitializedFilesCleanupPeriod = 2;

		// Token: 0x04000343 RID: 835
		private const int InitializedFilesCounterMax = 100;

		// Token: 0x04000344 RID: 836
		private const int ArchiveAboveSizeDisabled = -1;

		// Token: 0x04000345 RID: 837
		private static readonly char[] DirectorySeparatorChars = new char[]
		{
			Path.DirectorySeparatorChar,
			Path.AltDirectorySeparatorChar
		};

		// Token: 0x04000346 RID: 838
		private static readonly HashSet<char> InvalidFileNameChars = new HashSet<char>(Path.GetInvalidFileNameChars());

		// Token: 0x04000347 RID: 839
		private readonly Dictionary<string, DateTime> initializedFiles = new Dictionary<string, DateTime>();

		// Token: 0x04000348 RID: 840
		private LineEndingMode lineEndingMode = LineEndingMode.Default;

		// Token: 0x04000349 RID: 841
		private IFileAppenderFactory appenderFactory;

		// Token: 0x0400034A RID: 842
		private FileAppenderCache fileAppenderCache;

		// Token: 0x0400034B RID: 843
		private Timer autoClosingTimer;

		// Token: 0x0400034C RID: 844
		private Thread appenderInvalidatorThread;

		// Token: 0x0400034D RID: 845
		private EventWaitHandle stopAppenderInvalidatorThreadWaitHandle = new ManualResetEvent(false);

		// Token: 0x0400034E RID: 846
		private int initializedFilesCounter;

		// Token: 0x0400034F RID: 847
		private int maxArchiveFiles;

		// Token: 0x04000350 RID: 848
		private readonly FileTarget.DynamicFileArchive fileArchive;

		// Token: 0x04000351 RID: 849
		private Queue<string> previousFileNames;

		// Token: 0x04000352 RID: 850
		private Layout fileName;

		// Token: 0x04000353 RID: 851
		private Layout archiveFileName;

		// Token: 0x04000354 RID: 852
		private FileArchivePeriod archiveEvery;

		// Token: 0x04000355 RID: 853
		private long archiveAboveSize;

		// Token: 0x04000356 RID: 854
		private bool enableArchiveFileCompression;

		// Token: 0x04000357 RID: 855
		private string cachedCleanedFileNamed;

		// Token: 0x04000358 RID: 856
		private DateTime? previousLogEventTimestamp;

		// Token: 0x04000359 RID: 857
		private string previousLogFileName;

		// Token: 0x0400035A RID: 858
		private bool concurrentWrites;

		// Token: 0x0400035B RID: 859
		private bool keepFileOpen;

		// Token: 0x0200015C RID: 348
		private class DynamicFileArchive
		{
			// Token: 0x06000D17 RID: 3351 RVA: 0x0001F495 File Offset: 0x0001D695
			public DynamicFileArchive(FileTarget fileTarget, int maxArchivedFiles)
			{
				this.fileTarget = fileTarget;
				this.MaxArchiveFileToKeep = maxArchivedFiles;
			}

			// Token: 0x1700023A RID: 570
			// (get) Token: 0x06000D18 RID: 3352 RVA: 0x0001F4B6 File Offset: 0x0001D6B6
			// (set) Token: 0x06000D19 RID: 3353 RVA: 0x0001F4BE File Offset: 0x0001D6BE
			public int MaxArchiveFileToKeep { get; set; }

			// Token: 0x06000D1A RID: 3354 RVA: 0x0001F4D0 File Offset: 0x0001D6D0
			public void InitializeForArchiveFolderPath(string archiveFolderPath)
			{
				this.archiveFileQueue.Clear();
				if (Directory.Exists(archiveFolderPath))
				{
					string[] files = Directory.GetFiles(archiveFolderPath);
					foreach (string item in from f in files
					orderby FileTarget.DynamicFileArchive.ExtractArchiveNumberFromFileName(f)
					select f)
					{
						this.archiveFileQueue.Enqueue(item);
					}
				}
			}

			// Token: 0x06000D1B RID: 3355 RVA: 0x0001F55C File Offset: 0x0001D75C
			public bool Archive(string archiveFileName, string fileName, bool createDirectory)
			{
				if (this.MaxArchiveFileToKeep < 1)
				{
					InternalLogger.Warn("Archive is called. Even though the MaxArchiveFiles is set to less than 1");
					return false;
				}
				if (!File.Exists(fileName))
				{
					InternalLogger.Error("Error while archiving, Source File : {0} Not found.", new object[]
					{
						fileName
					});
					return false;
				}
				this.DeleteOldArchiveFiles();
				this.AddToArchive(archiveFileName, fileName, createDirectory);
				return true;
			}

			// Token: 0x06000D1C RID: 3356 RVA: 0x0001F5B0 File Offset: 0x0001D7B0
			private void AddToArchive(string archiveFileName, string fileName, bool createDirectory)
			{
				if (this.archiveFileQueue.Count != 0)
				{
					archiveFileName = this.GetNextArchiveFileName(archiveFileName);
				}
				try
				{
					this.fileTarget.ArchiveFile(fileName, archiveFileName);
					this.archiveFileQueue.Enqueue(archiveFileName);
				}
				catch (Exception ex)
				{
					InternalLogger.Error(ex, "Cannot archive file '{0}'.", new object[]
					{
						fileName
					});
					throw;
				}
			}

			// Token: 0x06000D1D RID: 3357 RVA: 0x0001F618 File Offset: 0x0001D818
			private void DeleteOldArchiveFiles()
			{
				if (this.MaxArchiveFileToKeep != 1 || !this.archiveFileQueue.Any<string>())
				{
					goto IL_78;
				}
				string text = this.archiveFileQueue.Dequeue();
				try
				{
					File.Delete(text);
					goto IL_78;
				}
				catch (Exception ex)
				{
					InternalLogger.Warn(ex, "Cannot delete old archive file : '{0}'.", new object[]
					{
						text
					});
					goto IL_78;
				}
				IL_47:
				string text2 = this.archiveFileQueue.Dequeue();
				try
				{
					File.Delete(text2);
				}
				catch (Exception ex2)
				{
					InternalLogger.Warn(ex2, "Cannot delete old archive file : '{0}'.", new object[]
					{
						text2
					});
				}
				IL_78:
				if (this.archiveFileQueue.Count < this.MaxArchiveFileToKeep)
				{
					return;
				}
				goto IL_47;
			}

			// Token: 0x06000D1E RID: 3358 RVA: 0x0001F6CC File Offset: 0x0001D8CC
			private string GetNextArchiveFileName(string fileName)
			{
				int num = (this.archiveFileQueue.Count == 0) ? 0 : FileTarget.DynamicFileArchive.ExtractArchiveNumberFromFileName(this.archiveFileQueue.Last<string>());
				string path = string.Format("{0}.{1}{2}", Path.GetFileNameWithoutExtension(fileName), num + 1, Path.GetExtension(fileName));
				return Path.Combine(Path.GetDirectoryName(fileName), path);
			}

			// Token: 0x06000D1F RID: 3359 RVA: 0x0001F728 File Offset: 0x0001D928
			private static int ExtractArchiveNumberFromFileName(string archiveFileName)
			{
				archiveFileName = Path.GetFileName(archiveFileName);
				int num = archiveFileName.LastIndexOf('.');
				if (num == -1)
				{
					return 0;
				}
				int num2 = archiveFileName.LastIndexOf('.', num - 1);
				string s = (num2 == -1) ? archiveFileName.Substring(num + 1) : archiveFileName.Substring(num2 + 1, num - num2 - 1);
				int result;
				if (!int.TryParse(s, out result))
				{
					return 0;
				}
				return result;
			}

			// Token: 0x04000378 RID: 888
			private readonly Queue<string> archiveFileQueue = new Queue<string>();

			// Token: 0x04000379 RID: 889
			private readonly FileTarget fileTarget;
		}

		// Token: 0x0200015D RID: 349
		private sealed class FileNameTemplate
		{
			// Token: 0x1700023B RID: 571
			// (get) Token: 0x06000D21 RID: 3361 RVA: 0x0001F783 File Offset: 0x0001D983
			public string Template
			{
				get
				{
					return this.template;
				}
			}

			// Token: 0x1700023C RID: 572
			// (get) Token: 0x06000D22 RID: 3362 RVA: 0x0001F78B File Offset: 0x0001D98B
			public int BeginAt
			{
				get
				{
					return this.startIndex;
				}
			}

			// Token: 0x1700023D RID: 573
			// (get) Token: 0x06000D23 RID: 3363 RVA: 0x0001F793 File Offset: 0x0001D993
			public int EndAt
			{
				get
				{
					return this.endIndex;
				}
			}

			// Token: 0x1700023E RID: 574
			// (get) Token: 0x06000D24 RID: 3364 RVA: 0x0001F79B File Offset: 0x0001D99B
			private bool FoundPattern
			{
				get
				{
					return this.startIndex != -1 && this.endIndex != -1;
				}
			}

			// Token: 0x06000D25 RID: 3365 RVA: 0x0001F7B4 File Offset: 0x0001D9B4
			public FileNameTemplate(string template)
			{
				this.template = template;
				this.startIndex = template.IndexOf("{#", StringComparison.Ordinal);
				if (this.startIndex != -1)
				{
					this.endIndex = template.IndexOf("#}", StringComparison.Ordinal) + "#}".Length;
				}
			}

			// Token: 0x06000D26 RID: 3366 RVA: 0x0001F808 File Offset: 0x0001DA08
			public string ReplacePattern(string replacementValue)
			{
				if (this.FoundPattern && !string.IsNullOrEmpty(replacementValue))
				{
					return this.template.Substring(0, this.BeginAt) + replacementValue + this.template.Substring(this.EndAt);
				}
				return this.Template;
			}

			// Token: 0x0400037C RID: 892
			public const string PatternStartCharacters = "{#";

			// Token: 0x0400037D RID: 893
			public const string PatternEndCharacters = "#}";

			// Token: 0x0400037E RID: 894
			private readonly string template;

			// Token: 0x0400037F RID: 895
			private readonly int startIndex;

			// Token: 0x04000380 RID: 896
			private readonly int endIndex;
		}
	}
}
