using System;
using System.Globalization;
using Google.Apis.Util;

namespace Google.Apis.Logging
{
	// Token: 0x02000018 RID: 24
	public abstract class BaseLogger : ILogger
	{
		// Token: 0x06000075 RID: 117 RVA: 0x00003368 File Offset: 0x00001568
		protected BaseLogger(LogLevel minimumLogLevel, IClock clock, Type forType)
		{
			this.MinimumLogLevel = minimumLogLevel;
			this.IsDebugEnabled = (minimumLogLevel <= LogLevel.Debug);
			this.IsInfoEnabled = (minimumLogLevel <= LogLevel.Info);
			this.IsWarningEnabled = (minimumLogLevel <= LogLevel.Warning);
			this.IsErrorEnabled = (minimumLogLevel <= LogLevel.Error);
			this.Clock = (clock ?? SystemClock.Default);
			this.LoggerForType = forType;
			if (forType != null)
			{
				string text = forType.Namespace ?? "";
				if (text.Length > 0)
				{
					text += ".";
				}
				this._loggerForTypeString = text + forType.Name + " ";
				return;
			}
			this._loggerForTypeString = "";
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000076 RID: 118 RVA: 0x0000342B File Offset: 0x0000162B
		public IClock Clock { get; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003433 File Offset: 0x00001633
		public Type LoggerForType { get; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000078 RID: 120 RVA: 0x0000343B File Offset: 0x0000163B
		public LogLevel MinimumLogLevel { get; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00003443 File Offset: 0x00001643
		public bool IsDebugEnabled { get; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600007A RID: 122 RVA: 0x0000344B File Offset: 0x0000164B
		public bool IsInfoEnabled { get; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00003453 File Offset: 0x00001653
		public bool IsWarningEnabled { get; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600007C RID: 124 RVA: 0x0000345B File Offset: 0x0000165B
		public bool IsErrorEnabled { get; }

		// Token: 0x0600007D RID: 125
		protected abstract ILogger BuildNewLogger(Type type);

		// Token: 0x0600007E RID: 126 RVA: 0x00003463 File Offset: 0x00001663
		public ILogger ForType<T>()
		{
			return this.ForType(typeof(T));
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003478 File Offset: 0x00001678
		public ILogger ForType(Type type)
		{
			if (!(type == this.LoggerForType))
			{
				return this.BuildNewLogger(type);
			}
			return this;
		}

		// Token: 0x06000080 RID: 128
		protected abstract void Log(LogLevel logLevel, string formattedMessage);

		// Token: 0x06000081 RID: 129 RVA: 0x000034A0 File Offset: 0x000016A0
		private string FormatLogEntry(string severityString, string message, params object[] formatArgs)
		{
			string text = string.Format(message, formatArgs);
			string text2 = this.Clock.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.ffffff", CultureInfo.InvariantCulture);
			return string.Format("{0}{1} {2}{3}", new object[]
			{
				severityString,
				text2,
				this._loggerForTypeString,
				text
			});
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000034F8 File Offset: 0x000016F8
		public void Debug(string message, params object[] formatArgs)
		{
			if (this.IsDebugEnabled)
			{
				this.Log(LogLevel.Debug, this.FormatLogEntry("D", message, formatArgs));
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003517 File Offset: 0x00001717
		public void Info(string message, params object[] formatArgs)
		{
			if (this.IsInfoEnabled)
			{
				this.Log(LogLevel.Info, this.FormatLogEntry("I", message, formatArgs));
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003539 File Offset: 0x00001739
		public void Warning(string message, params object[] formatArgs)
		{
			if (this.IsWarningEnabled)
			{
				this.Log(LogLevel.Warning, this.FormatLogEntry("W", message, formatArgs));
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000355B File Offset: 0x0000175B
		public void Error(Exception exception, string message, params object[] formatArgs)
		{
			if (this.IsErrorEnabled)
			{
				this.Log(LogLevel.Error, string.Format("{0} {1}", this.FormatLogEntry("E", message, formatArgs), exception));
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003588 File Offset: 0x00001788
		public void Error(string message, params object[] formatArgs)
		{
			if (this.IsErrorEnabled)
			{
				this.Log(LogLevel.Error, this.FormatLogEntry("E", message, formatArgs));
			}
		}

		// Token: 0x04000026 RID: 38
		private const string DateTimeFormatString = "yyyy-MM-dd HH:mm:ss.ffffff";

		// Token: 0x04000027 RID: 39
		private readonly string _loggerForTypeString;
	}
}
