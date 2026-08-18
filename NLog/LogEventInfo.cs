using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using NLog.Common;
using NLog.Internal;
using NLog.Layouts;
using NLog.Time;

namespace NLog
{
	// Token: 0x0200011C RID: 284
	public class LogEventInfo
	{
		// Token: 0x060007EA RID: 2026 RVA: 0x00011CA8 File Offset: 0x0000FEA8
		public LogEventInfo()
		{
			this.TimeStamp = TimeSource.Current.Time;
			this.SequenceID = Interlocked.Increment(ref LogEventInfo.globalSequenceId);
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x00011CDB File Offset: 0x0000FEDB
		public LogEventInfo(LogLevel level, string loggerName, [Localizable(false)] string message) : this(level, loggerName, null, message, null, null)
		{
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x00011CE9 File Offset: 0x0000FEE9
		public LogEventInfo(LogLevel level, string loggerName, IFormatProvider formatProvider, [Localizable(false)] string message, object[] parameters) : this(level, loggerName, formatProvider, message, parameters, null)
		{
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x00011CFC File Offset: 0x0000FEFC
		public LogEventInfo(LogLevel level, string loggerName, IFormatProvider formatProvider, [Localizable(false)] string message, object[] parameters, Exception exception) : this()
		{
			this.Level = level;
			this.LoggerName = loggerName;
			this.Message = message;
			this.Parameters = parameters;
			this.FormatProvider = formatProvider;
			this.Exception = exception;
			if (LogEventInfo.NeedToPreformatMessage(parameters))
			{
				this.CalcFormattedMessage();
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060007EE RID: 2030 RVA: 0x00011D4B File Offset: 0x0000FF4B
		// (set) Token: 0x060007EF RID: 2031 RVA: 0x00011D53 File Offset: 0x0000FF53
		public int SequenceID { get; private set; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x00011D5C File Offset: 0x0000FF5C
		// (set) Token: 0x060007F1 RID: 2033 RVA: 0x00011D64 File Offset: 0x0000FF64
		public DateTime TimeStamp { get; set; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x00011D6D File Offset: 0x0000FF6D
		// (set) Token: 0x060007F3 RID: 2035 RVA: 0x00011D75 File Offset: 0x0000FF75
		public LogLevel Level { get; set; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060007F4 RID: 2036 RVA: 0x00011D7E File Offset: 0x0000FF7E
		public bool HasStackTrace
		{
			get
			{
				return this.StackTrace != null;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060007F5 RID: 2037 RVA: 0x00011D8C File Offset: 0x0000FF8C
		public StackFrame UserStackFrame
		{
			get
			{
				if (this.StackTrace == null)
				{
					return null;
				}
				return this.StackTrace.GetFrame(this.UserStackFrameNumber);
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060007F6 RID: 2038 RVA: 0x00011DA9 File Offset: 0x0000FFA9
		// (set) Token: 0x060007F7 RID: 2039 RVA: 0x00011DB1 File Offset: 0x0000FFB1
		public int UserStackFrameNumber { get; private set; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060007F8 RID: 2040 RVA: 0x00011DBA File Offset: 0x0000FFBA
		// (set) Token: 0x060007F9 RID: 2041 RVA: 0x00011DC2 File Offset: 0x0000FFC2
		public StackTrace StackTrace { get; private set; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060007FA RID: 2042 RVA: 0x00011DCB File Offset: 0x0000FFCB
		// (set) Token: 0x060007FB RID: 2043 RVA: 0x00011DD3 File Offset: 0x0000FFD3
		public Exception Exception { get; set; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060007FC RID: 2044 RVA: 0x00011DDC File Offset: 0x0000FFDC
		// (set) Token: 0x060007FD RID: 2045 RVA: 0x00011DE4 File Offset: 0x0000FFE4
		public string LoggerName { get; set; }

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060007FE RID: 2046 RVA: 0x00011DF0 File Offset: 0x0000FFF0
		[Obsolete("This property should not be used.")]
		public string LoggerShortName
		{
			get
			{
				int num = this.LoggerName.LastIndexOf('.');
				if (num >= 0)
				{
					return this.LoggerName.Substring(num + 1);
				}
				return this.LoggerName;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060007FF RID: 2047 RVA: 0x00011E24 File Offset: 0x00010024
		// (set) Token: 0x06000800 RID: 2048 RVA: 0x00011E2C File Offset: 0x0001002C
		public string Message
		{
			get
			{
				return this.message;
			}
			set
			{
				this.message = value;
				this.ResetFormattedMessage();
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000801 RID: 2049 RVA: 0x00011E3B File Offset: 0x0001003B
		// (set) Token: 0x06000802 RID: 2050 RVA: 0x00011E43 File Offset: 0x00010043
		public object[] Parameters
		{
			get
			{
				return this.parameters;
			}
			set
			{
				this.parameters = value;
				this.ResetFormattedMessage();
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000803 RID: 2051 RVA: 0x00011E52 File Offset: 0x00010052
		// (set) Token: 0x06000804 RID: 2052 RVA: 0x00011E5A File Offset: 0x0001005A
		public IFormatProvider FormatProvider
		{
			get
			{
				return this.formatProvider;
			}
			set
			{
				if (this.formatProvider != value)
				{
					this.formatProvider = value;
					this.ResetFormattedMessage();
				}
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000805 RID: 2053 RVA: 0x00011E72 File Offset: 0x00010072
		public string FormattedMessage
		{
			get
			{
				if (this.formattedMessage == null)
				{
					this.CalcFormattedMessage();
				}
				return this.formattedMessage;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x00011E88 File Offset: 0x00010088
		public IDictionary<object, object> Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.InitEventContext();
				}
				return this.properties;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000807 RID: 2055 RVA: 0x00011E9E File Offset: 0x0001009E
		[Obsolete("Use LogEventInfo.Properties instead.", true)]
		public IDictionary Context
		{
			get
			{
				if (this.eventContextAdapter == null)
				{
					this.InitEventContext();
				}
				return this.eventContextAdapter;
			}
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x00011EB4 File Offset: 0x000100B4
		public static LogEventInfo CreateNullEvent()
		{
			return new LogEventInfo(LogLevel.Off, string.Empty, string.Empty);
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x00011ECA File Offset: 0x000100CA
		public static LogEventInfo Create(LogLevel logLevel, string loggerName, [Localizable(false)] string message)
		{
			return new LogEventInfo(logLevel, loggerName, null, message, null);
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x00011ED6 File Offset: 0x000100D6
		public static LogEventInfo Create(LogLevel logLevel, string loggerName, IFormatProvider formatProvider, [Localizable(false)] string message, object[] parameters)
		{
			return new LogEventInfo(logLevel, loggerName, formatProvider, message, parameters);
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00011EE4 File Offset: 0x000100E4
		public static LogEventInfo Create(LogLevel logLevel, string loggerName, IFormatProvider formatProvider, object message)
		{
			return new LogEventInfo(logLevel, loggerName, formatProvider, "{0}", new object[]
			{
				message
			});
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x00011F0A File Offset: 0x0001010A
		[Obsolete("use Create(LogLevel logLevel, string loggerName, Exception exception, IFormatProvider formatProvider, string message)")]
		public static LogEventInfo Create(LogLevel logLevel, string loggerName, [Localizable(false)] string message, Exception exception)
		{
			return new LogEventInfo(logLevel, loggerName, null, message, null, exception);
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x00011F17 File Offset: 0x00010117
		public static LogEventInfo Create(LogLevel logLevel, string loggerName, Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message)
		{
			return LogEventInfo.Create(logLevel, loggerName, exception, formatProvider, message, null);
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x00011F25 File Offset: 0x00010125
		public static LogEventInfo Create(LogLevel logLevel, string loggerName, Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, object[] parameters)
		{
			return new LogEventInfo(logLevel, loggerName, formatProvider, message, parameters, exception);
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x00011F34 File Offset: 0x00010134
		public AsyncLogEventInfo WithContinuation(AsyncContinuation asyncContinuation)
		{
			return new AsyncLogEventInfo(this, asyncContinuation);
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x00011F40 File Offset: 0x00010140
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"Log Event: Logger='",
				this.LoggerName,
				"' Level=",
				this.Level,
				" Message='",
				this.FormattedMessage,
				"' SequenceID=",
				this.SequenceID
			});
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00011FA3 File Offset: 0x000101A3
		public void SetStackTrace(StackTrace stackTrace, int userStackFrame)
		{
			this.StackTrace = stackTrace;
			this.UserStackFrameNumber = userStackFrame;
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x00011FB4 File Offset: 0x000101B4
		internal string AddCachedLayoutValue(Layout layout, string value)
		{
			lock (this.layoutCacheLock)
			{
				if (this.layoutCache == null)
				{
					this.layoutCache = new Dictionary<Layout, string>();
				}
				this.layoutCache[layout] = value;
			}
			return value;
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x00012010 File Offset: 0x00010210
		internal bool TryGetCachedLayoutValue(Layout layout, out string value)
		{
			bool result;
			lock (this.layoutCacheLock)
			{
				if (this.layoutCache == null)
				{
					value = null;
					result = false;
				}
				else
				{
					result = this.layoutCache.TryGetValue(layout, out value);
				}
			}
			return result;
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x00012068 File Offset: 0x00010268
		private static bool NeedToPreformatMessage(object[] parameters)
		{
			return parameters != null && parameters.Length != 0 && (parameters.Length > 3 || !LogEventInfo.IsSafeToDeferFormatting(parameters[0]) || (parameters.Length >= 2 && !LogEventInfo.IsSafeToDeferFormatting(parameters[1])) || (parameters.Length >= 3 && !LogEventInfo.IsSafeToDeferFormatting(parameters[2])));
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x000120B8 File Offset: 0x000102B8
		private static bool IsSafeToDeferFormatting(object value)
		{
			return value == null || value.GetType().IsPrimitive || value is string;
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x000120D8 File Offset: 0x000102D8
		private void CalcFormattedMessage()
		{
			if (this.Parameters == null || this.Parameters.Length == 0)
			{
				this.formattedMessage = this.Message;
				return;
			}
			try
			{
				this.formattedMessage = string.Format(this.FormatProvider ?? CultureInfo.CurrentCulture, this.Message, this.Parameters);
			}
			catch (Exception ex)
			{
				this.formattedMessage = this.Message;
				InternalLogger.Warn(ex, "Error when formatting a message.");
				if (ex.MustBeRethrown())
				{
					throw;
				}
			}
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x00012160 File Offset: 0x00010360
		private void ResetFormattedMessage()
		{
			this.formattedMessage = null;
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x00012169 File Offset: 0x00010369
		private void InitEventContext()
		{
			this.properties = new Dictionary<object, object>();
			this.eventContextAdapter = new DictionaryAdapter<object, object>(this.properties);
		}

		// Token: 0x0400025C RID: 604
		public static readonly DateTime ZeroDate = DateTime.UtcNow;

		// Token: 0x0400025D RID: 605
		private static int globalSequenceId;

		// Token: 0x0400025E RID: 606
		private readonly object layoutCacheLock = new object();

		// Token: 0x0400025F RID: 607
		private string formattedMessage;

		// Token: 0x04000260 RID: 608
		private string message;

		// Token: 0x04000261 RID: 609
		private object[] parameters;

		// Token: 0x04000262 RID: 610
		private IFormatProvider formatProvider;

		// Token: 0x04000263 RID: 611
		private IDictionary<Layout, string> layoutCache;

		// Token: 0x04000264 RID: 612
		private IDictionary<object, object> properties;

		// Token: 0x04000265 RID: 613
		private IDictionary eventContextAdapter;
	}
}
