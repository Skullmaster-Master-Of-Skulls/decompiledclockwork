using System;
using System.Collections;
using System.Diagnostics;

namespace log4net.Util
{
	// Token: 0x02000104 RID: 260
	public sealed class LogLog
	{
		// Token: 0x14000011 RID: 17
		// (add) Token: 0x0600076E RID: 1902 RVA: 0x000176BC File Offset: 0x000158BC
		// (remove) Token: 0x0600076F RID: 1903 RVA: 0x000176F0 File Offset: 0x000158F0
		public static event LogReceivedEventHandler LogReceived;

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x00017723 File Offset: 0x00015923
		public Type Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000771 RID: 1905 RVA: 0x0001772C File Offset: 0x0001592C
		public DateTime TimeStamp
		{
			get
			{
				return this.timeStampUtc.ToLocalTime();
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000772 RID: 1906 RVA: 0x00017747 File Offset: 0x00015947
		public DateTime TimeStampUtc
		{
			get
			{
				return this.timeStampUtc;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000773 RID: 1907 RVA: 0x0001774F File Offset: 0x0001594F
		public string Prefix
		{
			get
			{
				return this.prefix;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x00017757 File Offset: 0x00015957
		public string Message
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000775 RID: 1909 RVA: 0x0001775F File Offset: 0x0001595F
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x00017767 File Offset: 0x00015967
		public override string ToString()
		{
			return this.Prefix + this.Source.Name + ": " + this.Message;
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0001778A File Offset: 0x0001598A
		public LogLog(Type source, string prefix, string message, Exception exception)
		{
			this.timeStampUtc = DateTime.UtcNow;
			this.source = source;
			this.prefix = prefix;
			this.message = message;
			this.exception = exception;
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x000177BC File Offset: 0x000159BC
		static LogLog()
		{
			try
			{
				LogLog.InternalDebugging = OptionConverter.ToBoolean(SystemInfo.GetAppSetting("log4net.Internal.Debug"), false);
				LogLog.QuietMode = OptionConverter.ToBoolean(SystemInfo.GetAppSetting("log4net.Internal.Quiet"), false);
				LogLog.EmitInternalMessages = OptionConverter.ToBoolean(SystemInfo.GetAppSetting("log4net.Internal.Emit"), true);
			}
			catch (Exception ex)
			{
				LogLog.Error(typeof(LogLog), "Exception while reading ConfigurationSettings. Check your .config file is well formed XML.", ex);
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000779 RID: 1913 RVA: 0x00017844 File Offset: 0x00015A44
		// (set) Token: 0x0600077A RID: 1914 RVA: 0x0001784B File Offset: 0x00015A4B
		public static bool InternalDebugging
		{
			get
			{
				return LogLog.s_debugEnabled;
			}
			set
			{
				LogLog.s_debugEnabled = value;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600077B RID: 1915 RVA: 0x00017853 File Offset: 0x00015A53
		// (set) Token: 0x0600077C RID: 1916 RVA: 0x0001785A File Offset: 0x00015A5A
		public static bool QuietMode
		{
			get
			{
				return LogLog.s_quietMode;
			}
			set
			{
				LogLog.s_quietMode = value;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600077D RID: 1917 RVA: 0x00017862 File Offset: 0x00015A62
		// (set) Token: 0x0600077E RID: 1918 RVA: 0x00017869 File Offset: 0x00015A69
		public static bool EmitInternalMessages
		{
			get
			{
				return LogLog.s_emitInternalMessages;
			}
			set
			{
				LogLog.s_emitInternalMessages = value;
			}
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x00017871 File Offset: 0x00015A71
		public static void OnLogReceived(Type source, string prefix, string message, Exception exception)
		{
			if (LogLog.LogReceived != null)
			{
				LogLog.LogReceived(null, new LogReceivedEventArgs(new LogLog(source, prefix, message, exception)));
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000780 RID: 1920 RVA: 0x00017893 File Offset: 0x00015A93
		public static bool IsDebugEnabled
		{
			get
			{
				return LogLog.s_debugEnabled && !LogLog.s_quietMode;
			}
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x000178A6 File Offset: 0x00015AA6
		public static void Debug(Type source, string message)
		{
			if (LogLog.IsDebugEnabled)
			{
				if (LogLog.EmitInternalMessages)
				{
					LogLog.EmitOutLine("log4net: " + message);
				}
				LogLog.OnLogReceived(source, "log4net: ", message, null);
			}
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x000178D3 File Offset: 0x00015AD3
		public static void Debug(Type source, string message, Exception exception)
		{
			if (LogLog.IsDebugEnabled)
			{
				if (LogLog.EmitInternalMessages)
				{
					LogLog.EmitOutLine("log4net: " + message);
					if (exception != null)
					{
						LogLog.EmitOutLine(exception.ToString());
					}
				}
				LogLog.OnLogReceived(source, "log4net: ", message, exception);
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000783 RID: 1923 RVA: 0x0001790E File Offset: 0x00015B0E
		public static bool IsWarnEnabled
		{
			get
			{
				return !LogLog.s_quietMode;
			}
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x00017918 File Offset: 0x00015B18
		public static void Warn(Type source, string message)
		{
			if (LogLog.IsWarnEnabled)
			{
				if (LogLog.EmitInternalMessages)
				{
					LogLog.EmitErrorLine("log4net:WARN " + message);
				}
				LogLog.OnLogReceived(source, "log4net:WARN ", message, null);
			}
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x00017945 File Offset: 0x00015B45
		public static void Warn(Type source, string message, Exception exception)
		{
			if (LogLog.IsWarnEnabled)
			{
				if (LogLog.EmitInternalMessages)
				{
					LogLog.EmitErrorLine("log4net:WARN " + message);
					if (exception != null)
					{
						LogLog.EmitErrorLine(exception.ToString());
					}
				}
				LogLog.OnLogReceived(source, "log4net:WARN ", message, exception);
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000786 RID: 1926 RVA: 0x00017980 File Offset: 0x00015B80
		public static bool IsErrorEnabled
		{
			get
			{
				return !LogLog.s_quietMode;
			}
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0001798A File Offset: 0x00015B8A
		public static void Error(Type source, string message)
		{
			if (LogLog.IsErrorEnabled)
			{
				if (LogLog.EmitInternalMessages)
				{
					LogLog.EmitErrorLine("log4net:ERROR " + message);
				}
				LogLog.OnLogReceived(source, "log4net:ERROR ", message, null);
			}
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x000179B7 File Offset: 0x00015BB7
		public static void Error(Type source, string message, Exception exception)
		{
			if (LogLog.IsErrorEnabled)
			{
				if (LogLog.EmitInternalMessages)
				{
					LogLog.EmitErrorLine("log4net:ERROR " + message);
					if (exception != null)
					{
						LogLog.EmitErrorLine(exception.ToString());
					}
				}
				LogLog.OnLogReceived(source, "log4net:ERROR ", message, exception);
			}
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x000179F4 File Offset: 0x00015BF4
		private static void EmitOutLine(string message)
		{
			try
			{
				Console.Out.WriteLine(message);
				Trace.WriteLine(message);
			}
			catch
			{
			}
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x00017A28 File Offset: 0x00015C28
		private static void EmitErrorLine(string message)
		{
			try
			{
				Console.Error.WriteLine(message);
				Trace.WriteLine(message);
			}
			catch
			{
			}
		}

		// Token: 0x040002C2 RID: 706
		private const string PREFIX = "log4net: ";

		// Token: 0x040002C3 RID: 707
		private const string ERR_PREFIX = "log4net:ERROR ";

		// Token: 0x040002C4 RID: 708
		private const string WARN_PREFIX = "log4net:WARN ";

		// Token: 0x040002C6 RID: 710
		private readonly Type source;

		// Token: 0x040002C7 RID: 711
		private readonly DateTime timeStampUtc;

		// Token: 0x040002C8 RID: 712
		private readonly string prefix;

		// Token: 0x040002C9 RID: 713
		private readonly string message;

		// Token: 0x040002CA RID: 714
		private readonly Exception exception;

		// Token: 0x040002CB RID: 715
		private static bool s_debugEnabled = false;

		// Token: 0x040002CC RID: 716
		private static bool s_quietMode = false;

		// Token: 0x040002CD RID: 717
		private static bool s_emitInternalMessages = true;

		// Token: 0x02000105 RID: 261
		public class LogReceivedAdapter : IDisposable
		{
			// Token: 0x0600078B RID: 1931 RVA: 0x00017A5C File Offset: 0x00015C5C
			public LogReceivedAdapter(IList items)
			{
				this.items = items;
				this.handler = new LogReceivedEventHandler(this.LogLog_LogReceived);
				LogLog.LogReceived += this.handler;
			}

			// Token: 0x0600078C RID: 1932 RVA: 0x00017A88 File Offset: 0x00015C88
			private void LogLog_LogReceived(object source, LogReceivedEventArgs e)
			{
				this.items.Add(e.LogLog);
			}

			// Token: 0x17000190 RID: 400
			// (get) Token: 0x0600078D RID: 1933 RVA: 0x00017A9C File Offset: 0x00015C9C
			public IList Items
			{
				get
				{
					return this.items;
				}
			}

			// Token: 0x0600078E RID: 1934 RVA: 0x00017AA4 File Offset: 0x00015CA4
			public void Dispose()
			{
				LogLog.LogReceived -= this.handler;
			}

			// Token: 0x040002CE RID: 718
			private readonly IList items;

			// Token: 0x040002CF RID: 719
			private readonly LogReceivedEventHandler handler;
		}
	}
}
