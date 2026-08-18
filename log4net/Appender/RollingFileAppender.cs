using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Threading;
using log4net.Core;
using log4net.Util;

namespace log4net.Appender
{
	// Token: 0x0200003B RID: 59
	public class RollingFileAppender : FileAppender
	{
		// Token: 0x06000201 RID: 513 RVA: 0x00006BF4 File Offset: 0x00004DF4
		~RollingFileAppender()
		{
			if (this.m_mutexForRolling != null)
			{
				this.m_mutexForRolling.Dispose();
				this.m_mutexForRolling = null;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000202 RID: 514 RVA: 0x00006C34 File Offset: 0x00004E34
		// (set) Token: 0x06000203 RID: 515 RVA: 0x00006C3C File Offset: 0x00004E3C
		public RollingFileAppender.IDateTime DateTimeStrategy
		{
			get
			{
				return this.m_dateTime;
			}
			set
			{
				this.m_dateTime = value;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000204 RID: 516 RVA: 0x00006C45 File Offset: 0x00004E45
		// (set) Token: 0x06000205 RID: 517 RVA: 0x00006C4D File Offset: 0x00004E4D
		public string DatePattern
		{
			get
			{
				return this.m_datePattern;
			}
			set
			{
				this.m_datePattern = value;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00006C56 File Offset: 0x00004E56
		// (set) Token: 0x06000207 RID: 519 RVA: 0x00006C5E File Offset: 0x00004E5E
		public int MaxSizeRollBackups
		{
			get
			{
				return this.m_maxSizeRollBackups;
			}
			set
			{
				this.m_maxSizeRollBackups = value;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000208 RID: 520 RVA: 0x00006C67 File Offset: 0x00004E67
		// (set) Token: 0x06000209 RID: 521 RVA: 0x00006C6F File Offset: 0x00004E6F
		public long MaxFileSize
		{
			get
			{
				return this.m_maxFileSize;
			}
			set
			{
				this.m_maxFileSize = value;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600020A RID: 522 RVA: 0x00006C78 File Offset: 0x00004E78
		// (set) Token: 0x0600020B RID: 523 RVA: 0x00006C8A File Offset: 0x00004E8A
		public string MaximumFileSize
		{
			get
			{
				return this.m_maxFileSize.ToString(NumberFormatInfo.InvariantInfo);
			}
			set
			{
				this.m_maxFileSize = OptionConverter.ToFileSize(value, this.m_maxFileSize + 1L);
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600020C RID: 524 RVA: 0x00006CA1 File Offset: 0x00004EA1
		// (set) Token: 0x0600020D RID: 525 RVA: 0x00006CA9 File Offset: 0x00004EA9
		public int CountDirection
		{
			get
			{
				return this.m_countDirection;
			}
			set
			{
				this.m_countDirection = value;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600020E RID: 526 RVA: 0x00006CB2 File Offset: 0x00004EB2
		// (set) Token: 0x0600020F RID: 527 RVA: 0x00006CBC File Offset: 0x00004EBC
		public RollingFileAppender.RollingMode RollingStyle
		{
			get
			{
				return this.m_rollingStyle;
			}
			set
			{
				this.m_rollingStyle = value;
				switch (this.m_rollingStyle)
				{
				case RollingFileAppender.RollingMode.Once:
					this.m_rollDate = false;
					this.m_rollSize = false;
					base.AppendToFile = false;
					return;
				case RollingFileAppender.RollingMode.Size:
					this.m_rollDate = false;
					this.m_rollSize = true;
					return;
				case RollingFileAppender.RollingMode.Date:
					this.m_rollDate = true;
					this.m_rollSize = false;
					return;
				case RollingFileAppender.RollingMode.Composite:
					this.m_rollDate = true;
					this.m_rollSize = true;
					return;
				default:
					return;
				}
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000210 RID: 528 RVA: 0x00006D30 File Offset: 0x00004F30
		// (set) Token: 0x06000211 RID: 529 RVA: 0x00006D38 File Offset: 0x00004F38
		public bool PreserveLogFileNameExtension
		{
			get
			{
				return this.m_preserveLogFileNameExtension;
			}
			set
			{
				this.m_preserveLogFileNameExtension = value;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00006D41 File Offset: 0x00004F41
		// (set) Token: 0x06000213 RID: 531 RVA: 0x00006D49 File Offset: 0x00004F49
		public bool StaticLogFileName
		{
			get
			{
				return this.m_staticLogFileName;
			}
			set
			{
				this.m_staticLogFileName = value;
			}
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00006D52 File Offset: 0x00004F52
		protected override void SetQWForFiles(TextWriter writer)
		{
			base.QuietWriter = new CountingQuietTextWriter(writer, this.ErrorHandler);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00006D66 File Offset: 0x00004F66
		protected override void Append(LoggingEvent loggingEvent)
		{
			this.AdjustFileBeforeAppend();
			base.Append(loggingEvent);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00006D75 File Offset: 0x00004F75
		protected override void Append(LoggingEvent[] loggingEvents)
		{
			this.AdjustFileBeforeAppend();
			base.Append(loggingEvents);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00006D84 File Offset: 0x00004F84
		protected virtual void AdjustFileBeforeAppend()
		{
			try
			{
				if (this.m_mutexForRolling != null)
				{
					this.m_mutexForRolling.WaitOne();
				}
				if (this.m_rollDate)
				{
					DateTime now = this.m_dateTime.Now;
					if (now >= this.m_nextCheck)
					{
						this.m_now = now;
						this.m_nextCheck = this.NextCheckDate(this.m_now, this.m_rollPoint);
						this.RollOverTime(true);
					}
				}
				if (this.m_rollSize && this.File != null && ((CountingQuietTextWriter)base.QuietWriter).Count >= this.m_maxFileSize)
				{
					this.RollOverSize();
				}
			}
			finally
			{
				if (this.m_mutexForRolling != null)
				{
					this.m_mutexForRolling.ReleaseMutex();
				}
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00006E44 File Offset: 0x00005044
		protected override void OpenFile(string fileName, bool append)
		{
			lock (this)
			{
				fileName = this.GetNextOutputFileName(fileName);
				long count = 0L;
				if (append)
				{
					using (base.SecurityContext.Impersonate(this))
					{
						if (System.IO.File.Exists(fileName))
						{
							count = new FileInfo(fileName).Length;
						}
						goto IL_7A;
					}
				}
				if (LogLog.IsErrorEnabled && this.m_maxSizeRollBackups != 0 && this.FileExists(fileName))
				{
					LogLog.Error(RollingFileAppender.declaringType, "RollingFileAppender: INTERNAL ERROR. Append is False but OutputFile [" + fileName + "] already exists.");
				}
				IL_7A:
				if (!this.m_staticLogFileName)
				{
					this.m_scheduledFilename = fileName;
				}
				base.OpenFile(fileName, append);
				((CountingQuietTextWriter)base.QuietWriter).Count = count;
			}
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00006F1C File Offset: 0x0000511C
		protected string GetNextOutputFileName(string fileName)
		{
			if (!this.m_staticLogFileName)
			{
				fileName = fileName.Trim();
				if (this.m_rollDate)
				{
					fileName = this.CombinePath(fileName, this.m_now.ToString(this.m_datePattern, DateTimeFormatInfo.InvariantInfo));
				}
				if (this.m_countDirection >= 0)
				{
					fileName = this.CombinePath(fileName, "." + this.m_curSizeRollBackups);
				}
			}
			return fileName;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00006F88 File Offset: 0x00005188
		private void DetermineCurSizeRollBackups()
		{
			this.m_curSizeRollBackups = 0;
			string text = null;
			string baseFile = null;
			using (base.SecurityContext.Impersonate(this))
			{
				text = Path.GetFullPath(this.m_baseFileName);
				baseFile = Path.GetFileName(text);
			}
			ArrayList existingFiles = this.GetExistingFiles(text);
			this.InitializeRollBackups(baseFile, existingFiles);
			LogLog.Debug(RollingFileAppender.declaringType, "curSizeRollBackups starts at [" + this.m_curSizeRollBackups + "]");
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00007010 File Offset: 0x00005210
		private string GetWildcardPatternForFile(string baseFileName)
		{
			if (this.m_preserveLogFileNameExtension)
			{
				return Path.GetFileNameWithoutExtension(baseFileName) + "*" + Path.GetExtension(baseFileName);
			}
			return baseFileName + '*';
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00007040 File Offset: 0x00005240
		private ArrayList GetExistingFiles(string baseFilePath)
		{
			ArrayList arrayList = new ArrayList();
			string text = null;
			using (base.SecurityContext.Impersonate(this))
			{
				string fullPath = Path.GetFullPath(baseFilePath);
				text = Path.GetDirectoryName(fullPath);
				if (Directory.Exists(text))
				{
					string fileName = Path.GetFileName(fullPath);
					string[] files = Directory.GetFiles(text, this.GetWildcardPatternForFile(fileName));
					if (files != null)
					{
						for (int i = 0; i < files.Length; i++)
						{
							string fileName2 = Path.GetFileName(files[i]);
							if (fileName2.StartsWith(Path.GetFileNameWithoutExtension(fileName)))
							{
								arrayList.Add(fileName2);
							}
						}
					}
				}
			}
			LogLog.Debug(RollingFileAppender.declaringType, "Searched for existing files in [" + text + "]");
			return arrayList;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00007104 File Offset: 0x00005304
		private void RollOverIfDateBoundaryCrossing()
		{
			if (this.m_staticLogFileName && this.m_rollDate && this.FileExists(this.m_baseFileName))
			{
				DateTime dateTime;
				using (base.SecurityContext.Impersonate(this))
				{
					if (this.DateTimeStrategy is RollingFileAppender.UniversalDateTime)
					{
						dateTime = System.IO.File.GetLastWriteTimeUtc(this.m_baseFileName);
					}
					else
					{
						dateTime = System.IO.File.GetLastWriteTime(this.m_baseFileName);
					}
				}
				LogLog.Debug(RollingFileAppender.declaringType, string.Concat(new string[]
				{
					"[",
					dateTime.ToString(this.m_datePattern, DateTimeFormatInfo.InvariantInfo),
					"] vs. [",
					this.m_now.ToString(this.m_datePattern, DateTimeFormatInfo.InvariantInfo),
					"]"
				}));
				if (!dateTime.ToString(this.m_datePattern, DateTimeFormatInfo.InvariantInfo).Equals(this.m_now.ToString(this.m_datePattern, DateTimeFormatInfo.InvariantInfo)))
				{
					this.m_scheduledFilename = this.CombinePath(this.m_baseFileName, dateTime.ToString(this.m_datePattern, DateTimeFormatInfo.InvariantInfo));
					LogLog.Debug(RollingFileAppender.declaringType, "Initial roll over to [" + this.m_scheduledFilename + "]");
					this.RollOverTime(false);
					LogLog.Debug(RollingFileAppender.declaringType, "curSizeRollBackups after rollOver at [" + this.m_curSizeRollBackups + "]");
				}
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00007284 File Offset: 0x00005484
		protected void ExistingInit()
		{
			this.DetermineCurSizeRollBackups();
			this.RollOverIfDateBoundaryCrossing();
			if (!base.AppendToFile)
			{
				bool flag = false;
				string nextOutputFileName = this.GetNextOutputFileName(this.m_baseFileName);
				using (base.SecurityContext.Impersonate(this))
				{
					flag = System.IO.File.Exists(nextOutputFileName);
				}
				if (flag)
				{
					if (this.m_maxSizeRollBackups == 0)
					{
						LogLog.Debug(RollingFileAppender.declaringType, "Output file [" + nextOutputFileName + "] already exists. MaxSizeRollBackups is 0; cannot roll. Overwriting existing file.");
						return;
					}
					LogLog.Debug(RollingFileAppender.declaringType, "Output file [" + nextOutputFileName + "] already exists. Not appending to file. Rolling existing file out of the way.");
					this.RollOverRenameFiles(nextOutputFileName);
				}
			}
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000732C File Offset: 0x0000552C
		private void InitializeFromOneFile(string baseFile, string curFileName)
		{
			if (!curFileName.StartsWith(Path.GetFileNameWithoutExtension(baseFile)))
			{
				return;
			}
			if (curFileName.Equals(baseFile))
			{
				return;
			}
			if (this.m_rollDate && !this.m_staticLogFileName)
			{
				string str = this.m_dateTime.Now.ToString(this.m_datePattern, DateTimeFormatInfo.InvariantInfo);
				string value = this.m_preserveLogFileNameExtension ? (Path.GetFileNameWithoutExtension(baseFile) + str) : (baseFile + str);
				string value2 = this.m_preserveLogFileNameExtension ? Path.GetExtension(baseFile) : "";
				if (!curFileName.StartsWith(value) || !curFileName.EndsWith(value2))
				{
					LogLog.Debug(RollingFileAppender.declaringType, "Ignoring file [" + curFileName + "] because it is from a different date period");
					return;
				}
			}
			try
			{
				int backUpIndex = this.GetBackUpIndex(curFileName);
				if (backUpIndex > this.m_curSizeRollBackups)
				{
					if (this.m_maxSizeRollBackups != 0)
					{
						if (-1 == this.m_maxSizeRollBackups)
						{
							this.m_curSizeRollBackups = backUpIndex;
						}
						else if (this.m_countDirection >= 0)
						{
							this.m_curSizeRollBackups = backUpIndex;
						}
						else if (backUpIndex <= this.m_maxSizeRollBackups)
						{
							this.m_curSizeRollBackups = backUpIndex;
						}
					}
					LogLog.Debug(RollingFileAppender.declaringType, string.Concat(new object[]
					{
						"File name [",
						curFileName,
						"] moves current count to [",
						this.m_curSizeRollBackups,
						"]"
					}));
				}
			}
			catch (FormatException)
			{
				LogLog.Debug(RollingFileAppender.declaringType, "Encountered a backup file not ending in .x [" + curFileName + "]");
			}
		}

		// Token: 0x06000220 RID: 544 RVA: 0x000074B0 File Offset: 0x000056B0
		private int GetBackUpIndex(string curFileName)
		{
			int result = -1;
			string text = curFileName;
			if (this.m_preserveLogFileNameExtension)
			{
				text = Path.GetFileNameWithoutExtension(text);
			}
			int num = text.LastIndexOf(".");
			if (num > 0)
			{
				SystemInfo.TryParse(text.Substring(num + 1), out result);
			}
			return result;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x000074F4 File Offset: 0x000056F4
		private void InitializeRollBackups(string baseFile, ArrayList arrayFiles)
		{
			if (arrayFiles != null)
			{
				string baseFile2 = baseFile.ToLower(CultureInfo.InvariantCulture);
				foreach (object obj in arrayFiles)
				{
					string text = (string)obj;
					this.InitializeFromOneFile(baseFile2, text.ToLower(CultureInfo.InvariantCulture));
				}
			}
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00007564 File Offset: 0x00005764
		private RollingFileAppender.RollPoint ComputeCheckPeriod(string datePattern)
		{
			string text = RollingFileAppender.s_date1970.ToString(datePattern, DateTimeFormatInfo.InvariantInfo);
			for (int i = 0; i <= 5; i++)
			{
				string text2 = this.NextCheckDate(RollingFileAppender.s_date1970, (RollingFileAppender.RollPoint)i).ToString(datePattern, DateTimeFormatInfo.InvariantInfo);
				LogLog.Debug(RollingFileAppender.declaringType, string.Concat(new object[]
				{
					"Type = [",
					i,
					"], r0 = [",
					text,
					"], r1 = [",
					text2,
					"]"
				}));
				if (text != null && text2 != null && !text.Equals(text2))
				{
					return (RollingFileAppender.RollPoint)i;
				}
			}
			return RollingFileAppender.RollPoint.InvalidRollPoint;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00007618 File Offset: 0x00005818
		public override void ActivateOptions()
		{
			if (this.m_dateTime == null)
			{
				this.m_dateTime = new RollingFileAppender.LocalDateTime();
			}
			if (this.m_rollDate && this.m_datePattern != null)
			{
				this.m_now = this.m_dateTime.Now;
				this.m_rollPoint = this.ComputeCheckPeriod(this.m_datePattern);
				if (this.m_rollPoint == RollingFileAppender.RollPoint.InvalidRollPoint)
				{
					throw new ArgumentException("Invalid RollPoint, unable to parse [" + this.m_datePattern + "]");
				}
				this.m_nextCheck = this.NextCheckDate(this.m_now, this.m_rollPoint);
			}
			else if (this.m_rollDate)
			{
				this.ErrorHandler.Error("Either DatePattern or rollingStyle options are not set for [" + base.Name + "].");
			}
			if (base.SecurityContext == null)
			{
				base.SecurityContext = SecurityContextProvider.DefaultProvider.CreateSecurityContext(this);
			}
			using (base.SecurityContext.Impersonate(this))
			{
				base.File = FileAppender.ConvertToFullPath(base.File.Trim());
				this.m_baseFileName = base.File;
			}
			this.m_mutexForRolling = new Mutex(false, this.m_baseFileName.Replace("\\", "_").Replace(":", "_").Replace("/", "_"));
			if (this.m_rollDate && this.File != null && this.m_scheduledFilename == null)
			{
				this.m_scheduledFilename = this.CombinePath(this.File, this.m_now.ToString(this.m_datePattern, DateTimeFormatInfo.InvariantInfo));
			}
			this.ExistingInit();
			base.ActivateOptions();
		}

		// Token: 0x06000224 RID: 548 RVA: 0x000077C0 File Offset: 0x000059C0
		private string CombinePath(string path1, string path2)
		{
			string extension = Path.GetExtension(path1);
			if (this.m_preserveLogFileNameExtension && extension.Length > 0)
			{
				return Path.Combine(Path.GetDirectoryName(path1), Path.GetFileNameWithoutExtension(path1) + path2 + extension);
			}
			return path1 + path2;
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00007808 File Offset: 0x00005A08
		protected void RollOverTime(bool fileIsOpen)
		{
			if (this.m_staticLogFileName)
			{
				if (this.m_datePattern == null)
				{
					this.ErrorHandler.Error("Missing DatePattern option in rollOver().");
					return;
				}
				string path = this.m_now.ToString(this.m_datePattern, DateTimeFormatInfo.InvariantInfo);
				if (this.m_scheduledFilename.Equals(this.CombinePath(this.File, path)))
				{
					this.ErrorHandler.Error("Compare " + this.m_scheduledFilename + " : " + this.CombinePath(this.File, path));
					return;
				}
				if (fileIsOpen)
				{
					base.CloseFile();
				}
				for (int i = 1; i <= this.m_curSizeRollBackups; i++)
				{
					string fromFile = this.CombinePath(this.File, "." + i);
					string toFile = this.CombinePath(this.m_scheduledFilename, "." + i);
					this.RollFile(fromFile, toFile);
				}
				this.RollFile(this.File, this.m_scheduledFilename);
			}
			this.m_curSizeRollBackups = 0;
			this.m_scheduledFilename = this.CombinePath(this.File, this.m_now.ToString(this.m_datePattern, DateTimeFormatInfo.InvariantInfo));
			if (fileIsOpen)
			{
				this.SafeOpenFile(this.m_baseFileName, false);
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00007948 File Offset: 0x00005B48
		protected void RollFile(string fromFile, string toFile)
		{
			if (this.FileExists(fromFile))
			{
				this.DeleteFile(toFile);
				try
				{
					LogLog.Debug(RollingFileAppender.declaringType, string.Concat(new string[]
					{
						"Moving [",
						fromFile,
						"] -> [",
						toFile,
						"]"
					}));
					using (base.SecurityContext.Impersonate(this))
					{
						System.IO.File.Move(fromFile, toFile);
					}
					return;
				}
				catch (Exception e)
				{
					this.ErrorHandler.Error(string.Concat(new string[]
					{
						"Exception while rolling file [",
						fromFile,
						"] -> [",
						toFile,
						"]"
					}), e, ErrorCode.GenericFailure);
					return;
				}
			}
			LogLog.Warn(RollingFileAppender.declaringType, string.Concat(new string[]
			{
				"Cannot RollFile [",
				fromFile,
				"] -> [",
				toFile,
				"]. Source does not exist"
			}));
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00007A58 File Offset: 0x00005C58
		protected bool FileExists(string path)
		{
			bool result;
			using (base.SecurityContext.Impersonate(this))
			{
				result = System.IO.File.Exists(path);
			}
			return result;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00007A98 File Offset: 0x00005C98
		protected void DeleteFile(string fileName)
		{
			if (this.FileExists(fileName))
			{
				string text = fileName;
				string text2 = string.Concat(new object[]
				{
					fileName,
					".",
					Environment.TickCount,
					".DeletePending"
				});
				try
				{
					using (base.SecurityContext.Impersonate(this))
					{
						System.IO.File.Move(fileName, text2);
					}
					text = text2;
				}
				catch (Exception exception)
				{
					LogLog.Debug(RollingFileAppender.declaringType, string.Concat(new string[]
					{
						"Exception while moving file to be deleted [",
						fileName,
						"] -> [",
						text2,
						"]"
					}), exception);
				}
				try
				{
					using (base.SecurityContext.Impersonate(this))
					{
						System.IO.File.Delete(text);
					}
					LogLog.Debug(RollingFileAppender.declaringType, "Deleted file [" + fileName + "]");
				}
				catch (Exception ex)
				{
					if (text == fileName)
					{
						this.ErrorHandler.Error("Exception while deleting file [" + text + "]", ex, ErrorCode.GenericFailure);
					}
					else
					{
						LogLog.Debug(RollingFileAppender.declaringType, "Exception while deleting temp file [" + text + "]", ex);
					}
				}
			}
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00007C0C File Offset: 0x00005E0C
		protected void RollOverSize()
		{
			base.CloseFile();
			LogLog.Debug(RollingFileAppender.declaringType, "rolling over count [" + ((CountingQuietTextWriter)base.QuietWriter).Count + "]");
			LogLog.Debug(RollingFileAppender.declaringType, "maxSizeRollBackups [" + this.m_maxSizeRollBackups + "]");
			LogLog.Debug(RollingFileAppender.declaringType, "curSizeRollBackups [" + this.m_curSizeRollBackups + "]");
			LogLog.Debug(RollingFileAppender.declaringType, "countDirection [" + this.m_countDirection + "]");
			this.RollOverRenameFiles(this.File);
			if (!this.m_staticLogFileName && this.m_countDirection >= 0)
			{
				this.m_curSizeRollBackups++;
			}
			this.SafeOpenFile(this.m_baseFileName, false);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00007CF4 File Offset: 0x00005EF4
		protected void RollOverRenameFiles(string baseFileName)
		{
			if (this.m_maxSizeRollBackups != 0)
			{
				if (this.m_countDirection < 0)
				{
					if (this.m_curSizeRollBackups == this.m_maxSizeRollBackups)
					{
						this.DeleteFile(this.CombinePath(baseFileName, "." + this.m_maxSizeRollBackups));
						this.m_curSizeRollBackups--;
					}
					for (int i = this.m_curSizeRollBackups; i >= 1; i--)
					{
						this.RollFile(this.CombinePath(baseFileName, "." + i), this.CombinePath(baseFileName, "." + (i + 1)));
					}
					this.m_curSizeRollBackups++;
					this.RollFile(baseFileName, this.CombinePath(baseFileName, ".1"));
					return;
				}
				if (this.m_curSizeRollBackups >= this.m_maxSizeRollBackups && this.m_maxSizeRollBackups > 0)
				{
					int num = this.m_curSizeRollBackups - this.m_maxSizeRollBackups;
					if (this.m_staticLogFileName)
					{
						num++;
					}
					string text = baseFileName;
					if (!this.m_staticLogFileName)
					{
						int num2 = text.LastIndexOf(".");
						if (num2 >= 0)
						{
							text = text.Substring(0, num2);
						}
					}
					this.DeleteFile(this.CombinePath(text, "." + num));
				}
				if (this.m_staticLogFileName)
				{
					this.m_curSizeRollBackups++;
					this.RollFile(baseFileName, this.CombinePath(baseFileName, "." + this.m_curSizeRollBackups));
				}
			}
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00007E6C File Offset: 0x0000606C
		protected DateTime NextCheckDate(DateTime currentDateTime, RollingFileAppender.RollPoint rollPoint)
		{
			DateTime result = currentDateTime;
			switch (rollPoint)
			{
			case RollingFileAppender.RollPoint.TopOfMinute:
				result = result.AddMilliseconds((double)(-(double)result.Millisecond));
				result = result.AddSeconds((double)(-(double)result.Second));
				result = result.AddMinutes(1.0);
				break;
			case RollingFileAppender.RollPoint.TopOfHour:
				result = result.AddMilliseconds((double)(-(double)result.Millisecond));
				result = result.AddSeconds((double)(-(double)result.Second));
				result = result.AddMinutes((double)(-(double)result.Minute));
				result = result.AddHours(1.0);
				break;
			case RollingFileAppender.RollPoint.HalfDay:
				result = result.AddMilliseconds((double)(-(double)result.Millisecond));
				result = result.AddSeconds((double)(-(double)result.Second));
				result = result.AddMinutes((double)(-(double)result.Minute));
				if (result.Hour < 12)
				{
					result = result.AddHours((double)(12 - result.Hour));
				}
				else
				{
					result = result.AddHours((double)(-(double)result.Hour));
					result = result.AddDays(1.0);
				}
				break;
			case RollingFileAppender.RollPoint.TopOfDay:
				result = result.AddMilliseconds((double)(-(double)result.Millisecond));
				result = result.AddSeconds((double)(-(double)result.Second));
				result = result.AddMinutes((double)(-(double)result.Minute));
				result = result.AddHours((double)(-(double)result.Hour));
				result = result.AddDays(1.0);
				break;
			case RollingFileAppender.RollPoint.TopOfWeek:
				result = result.AddMilliseconds((double)(-(double)result.Millisecond));
				result = result.AddSeconds((double)(-(double)result.Second));
				result = result.AddMinutes((double)(-(double)result.Minute));
				result = result.AddHours((double)(-(double)result.Hour));
				result = result.AddDays((double)((DayOfWeek)7 - result.DayOfWeek));
				break;
			case RollingFileAppender.RollPoint.TopOfMonth:
				result = result.AddMilliseconds((double)(-(double)result.Millisecond));
				result = result.AddSeconds((double)(-(double)result.Second));
				result = result.AddMinutes((double)(-(double)result.Minute));
				result = result.AddHours((double)(-(double)result.Hour));
				result = result.AddDays((double)(1 - result.Day));
				result = result.AddMonths(1);
				break;
			}
			return result;
		}

		// Token: 0x04000100 RID: 256
		private static readonly Type declaringType = typeof(RollingFileAppender);

		// Token: 0x04000101 RID: 257
		private RollingFileAppender.IDateTime m_dateTime;

		// Token: 0x04000102 RID: 258
		private string m_datePattern = ".yyyy-MM-dd";

		// Token: 0x04000103 RID: 259
		private string m_scheduledFilename;

		// Token: 0x04000104 RID: 260
		private DateTime m_nextCheck = DateTime.MaxValue;

		// Token: 0x04000105 RID: 261
		private DateTime m_now;

		// Token: 0x04000106 RID: 262
		private RollingFileAppender.RollPoint m_rollPoint;

		// Token: 0x04000107 RID: 263
		private long m_maxFileSize = 10485760L;

		// Token: 0x04000108 RID: 264
		private int m_maxSizeRollBackups;

		// Token: 0x04000109 RID: 265
		private int m_curSizeRollBackups;

		// Token: 0x0400010A RID: 266
		private int m_countDirection = -1;

		// Token: 0x0400010B RID: 267
		private RollingFileAppender.RollingMode m_rollingStyle = RollingFileAppender.RollingMode.Composite;

		// Token: 0x0400010C RID: 268
		private bool m_rollDate = true;

		// Token: 0x0400010D RID: 269
		private bool m_rollSize = true;

		// Token: 0x0400010E RID: 270
		private bool m_staticLogFileName = true;

		// Token: 0x0400010F RID: 271
		private bool m_preserveLogFileNameExtension;

		// Token: 0x04000110 RID: 272
		private string m_baseFileName;

		// Token: 0x04000111 RID: 273
		private Mutex m_mutexForRolling;

		// Token: 0x04000112 RID: 274
		private static readonly DateTime s_date1970 = new DateTime(1970, 1, 1);

		// Token: 0x0200003C RID: 60
		public enum RollingMode
		{
			// Token: 0x04000114 RID: 276
			Once,
			// Token: 0x04000115 RID: 277
			Size,
			// Token: 0x04000116 RID: 278
			Date,
			// Token: 0x04000117 RID: 279
			Composite
		}

		// Token: 0x0200003D RID: 61
		protected enum RollPoint
		{
			// Token: 0x04000119 RID: 281
			InvalidRollPoint = -1,
			// Token: 0x0400011A RID: 282
			TopOfMinute,
			// Token: 0x0400011B RID: 283
			TopOfHour,
			// Token: 0x0400011C RID: 284
			HalfDay,
			// Token: 0x0400011D RID: 285
			TopOfDay,
			// Token: 0x0400011E RID: 286
			TopOfWeek,
			// Token: 0x0400011F RID: 287
			TopOfMonth
		}

		// Token: 0x0200003E RID: 62
		public interface IDateTime
		{
			// Token: 0x17000084 RID: 132
			// (get) Token: 0x0600022D RID: 557
			DateTime Now { get; }
		}

		// Token: 0x0200003F RID: 63
		private class LocalDateTime : RollingFileAppender.IDateTime
		{
			// Token: 0x17000085 RID: 133
			// (get) Token: 0x0600022E RID: 558 RVA: 0x000080D2 File Offset: 0x000062D2
			public DateTime Now
			{
				get
				{
					return DateTime.Now;
				}
			}
		}

		// Token: 0x02000040 RID: 64
		private class UniversalDateTime : RollingFileAppender.IDateTime
		{
			// Token: 0x17000086 RID: 134
			// (get) Token: 0x06000230 RID: 560 RVA: 0x000080E1 File Offset: 0x000062E1
			public DateTime Now
			{
				get
				{
					return DateTime.UtcNow;
				}
			}
		}
	}
}
