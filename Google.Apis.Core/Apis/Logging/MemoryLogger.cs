using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Google.Apis.Util;

namespace Google.Apis.Logging
{
	// Token: 0x0200001C RID: 28
	public sealed class MemoryLogger : BaseLogger, ILogger
	{
		// Token: 0x06000094 RID: 148 RVA: 0x00003607 File Offset: 0x00001807
		public MemoryLogger(LogLevel minimumLogLevel, int maximumEntryCount = 1000, IClock clock = null) : this(minimumLogLevel, maximumEntryCount, clock, new List<string>(), null)
		{
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003618 File Offset: 0x00001818
		private MemoryLogger(LogLevel minimumLogLevel, int maximumEntryCount, IClock clock, List<string> logEntries, Type forType) : base(minimumLogLevel, clock, forType)
		{
			this._logEntries = logEntries;
			this.LogEntries = new ReadOnlyCollection<string>(this._logEntries);
			this._maximumEntryCount = maximumEntryCount;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00003644 File Offset: 0x00001844
		public IList<string> LogEntries { get; }

		// Token: 0x06000097 RID: 151 RVA: 0x0000364C File Offset: 0x0000184C
		protected override ILogger BuildNewLogger(Type type)
		{
			return new MemoryLogger(base.MinimumLogLevel, this._maximumEntryCount, base.Clock, this._logEntries, type);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x0000366C File Offset: 0x0000186C
		protected override void Log(LogLevel logLevel, string formattedMessage)
		{
			List<string> logEntries = this._logEntries;
			lock (logEntries)
			{
				if (this._logEntries.Count < this._maximumEntryCount)
				{
					this._logEntries.Add(formattedMessage);
				}
			}
		}

		// Token: 0x04000037 RID: 55
		private readonly int _maximumEntryCount;

		// Token: 0x04000038 RID: 56
		private readonly List<string> _logEntries;
	}
}
