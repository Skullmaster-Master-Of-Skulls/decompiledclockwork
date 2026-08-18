using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using NLog.Time;

namespace NLog.Fluent
{
	// Token: 0x02000066 RID: 102
	public class LogBuilder
	{
		// Token: 0x06000249 RID: 585 RVA: 0x00008954 File Offset: 0x00006B54
		[CLSCompliant(false)]
		public LogBuilder(ILogger logger) : this(logger, LogLevel.Debug)
		{
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00008964 File Offset: 0x00006B64
		[CLSCompliant(false)]
		public LogBuilder(ILogger logger, LogLevel logLevel)
		{
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			if (logLevel == null)
			{
				throw new ArgumentNullException("logLevel");
			}
			this._logger = logger;
			this._logEvent = new LogEventInfo
			{
				Level = logLevel,
				LoggerName = logger.Name,
				TimeStamp = TimeSource.Current.Time
			};
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600024B RID: 587 RVA: 0x000089D0 File Offset: 0x00006BD0
		public LogEventInfo LogEventInfo
		{
			get
			{
				return this._logEvent;
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x000089D8 File Offset: 0x00006BD8
		public LogBuilder Exception(Exception exception)
		{
			this._logEvent.Exception = exception;
			return this;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x000089E7 File Offset: 0x00006BE7
		public LogBuilder Level(LogLevel logLevel)
		{
			if (logLevel == null)
			{
				throw new ArgumentNullException("logLevel");
			}
			this._logEvent.Level = logLevel;
			return this;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00008A0A File Offset: 0x00006C0A
		public LogBuilder LoggerName(string loggerName)
		{
			this._logEvent.LoggerName = loggerName;
			return this;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00008A19 File Offset: 0x00006C19
		public LogBuilder Message(string message)
		{
			this._logEvent.Message = message;
			return this;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00008A28 File Offset: 0x00006C28
		[StringFormatMethod("format")]
		public LogBuilder Message(string format, object arg0)
		{
			this._logEvent.Message = format;
			this._logEvent.Parameters = new object[]
			{
				arg0
			};
			return this;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00008A5C File Offset: 0x00006C5C
		[StringFormatMethod("format")]
		public LogBuilder Message(string format, object arg0, object arg1)
		{
			this._logEvent.Message = format;
			this._logEvent.Parameters = new object[]
			{
				arg0,
				arg1
			};
			return this;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00008A94 File Offset: 0x00006C94
		[StringFormatMethod("format")]
		public LogBuilder Message(string format, object arg0, object arg1, object arg2)
		{
			this._logEvent.Message = format;
			this._logEvent.Parameters = new object[]
			{
				arg0,
				arg1,
				arg2
			};
			return this;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00008AD0 File Offset: 0x00006CD0
		[StringFormatMethod("format")]
		public LogBuilder Message(string format, object arg0, object arg1, object arg2, object arg3)
		{
			this._logEvent.Message = format;
			this._logEvent.Parameters = new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3
			};
			return this;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00008B0F File Offset: 0x00006D0F
		[StringFormatMethod("format")]
		public LogBuilder Message(string format, params object[] args)
		{
			this._logEvent.Message = format;
			this._logEvent.Parameters = args;
			return this;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00008B2A File Offset: 0x00006D2A
		[StringFormatMethod("format")]
		public LogBuilder Message(IFormatProvider provider, string format, params object[] args)
		{
			this._logEvent.FormatProvider = provider;
			this._logEvent.Message = format;
			this._logEvent.Parameters = args;
			return this;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00008B51 File Offset: 0x00006D51
		public LogBuilder Property(object name, object value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this._logEvent.Properties[name] = value;
			return this;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00008B74 File Offset: 0x00006D74
		public LogBuilder Properties(IDictionary properties)
		{
			if (properties == null)
			{
				throw new ArgumentNullException("properties");
			}
			foreach (object key in properties.Keys)
			{
				this._logEvent.Properties[key] = properties[key];
			}
			return this;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00008BE8 File Offset: 0x00006DE8
		public LogBuilder TimeStamp(DateTime timeStamp)
		{
			this._logEvent.TimeStamp = timeStamp;
			return this;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00008BF7 File Offset: 0x00006DF7
		public LogBuilder StackTrace(StackTrace stackTrace, int userStackFrame)
		{
			this._logEvent.SetStackTrace(stackTrace, userStackFrame);
			return this;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00008C08 File Offset: 0x00006E08
		public void Write([CallerMemberName] string callerMemberName = null, [CallerFilePath] string callerFilePath = null, [CallerLineNumber] int callerLineNumber = 0)
		{
			if (callerMemberName != null)
			{
				this.Property("CallerMemberName", callerMemberName);
			}
			if (callerFilePath != null)
			{
				this.Property("CallerFilePath", callerFilePath);
			}
			if (callerLineNumber != 0)
			{
				this.Property("CallerLineNumber", callerLineNumber);
			}
			this._logger.Log(this._logEvent);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00008C5C File Offset: 0x00006E5C
		public void WriteIf(Func<bool> condition, [CallerMemberName] string callerMemberName = null, [CallerFilePath] string callerFilePath = null, [CallerLineNumber] int callerLineNumber = 0)
		{
			if (condition == null || !condition())
			{
				return;
			}
			if (callerMemberName != null)
			{
				this.Property("CallerMemberName", callerMemberName);
			}
			if (callerFilePath != null)
			{
				this.Property("CallerFilePath", callerFilePath);
			}
			if (callerLineNumber != 0)
			{
				this.Property("CallerLineNumber", callerLineNumber);
			}
			this._logger.Log(this._logEvent);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00008CC0 File Offset: 0x00006EC0
		public void WriteIf(bool condition, [CallerMemberName] string callerMemberName = null, [CallerFilePath] string callerFilePath = null, [CallerLineNumber] int callerLineNumber = 0)
		{
			if (!condition)
			{
				return;
			}
			if (callerMemberName != null)
			{
				this.Property("CallerMemberName", callerMemberName);
			}
			if (callerFilePath != null)
			{
				this.Property("CallerFilePath", callerFilePath);
			}
			if (callerLineNumber != 0)
			{
				this.Property("CallerLineNumber", callerLineNumber);
			}
			this._logger.Log(this._logEvent);
		}

		// Token: 0x040000CE RID: 206
		private readonly LogEventInfo _logEvent;

		// Token: 0x040000CF RID: 207
		private readonly ILogger _logger;
	}
}
