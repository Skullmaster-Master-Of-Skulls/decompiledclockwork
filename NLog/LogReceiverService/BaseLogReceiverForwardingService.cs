using System;

namespace NLog.LogReceiverService
{
	// Token: 0x02000129 RID: 297
	public abstract class BaseLogReceiverForwardingService
	{
		// Token: 0x06000A6A RID: 2666 RVA: 0x00018CF2 File Offset: 0x00016EF2
		protected BaseLogReceiverForwardingService() : this(null)
		{
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x00018CFB File Offset: 0x00016EFB
		protected BaseLogReceiverForwardingService(LogFactory logFactory)
		{
			this.logFactory = logFactory;
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x00018D0C File Offset: 0x00016F0C
		public void ProcessLogMessages(NLogEvents events)
		{
			DateTime dateTime = new DateTime(events.BaseTimeUtc, DateTimeKind.Utc);
			LogEventInfo[] array = new LogEventInfo[events.Events.Length];
			for (int i = 0; i < events.Events.Length; i++)
			{
				NLogEvent nlogEvent = events.Events[i];
				LogLevel level = LogLevel.FromOrdinal(nlogEvent.LevelOrdinal);
				string loggerName = events.Strings[nlogEvent.LoggerOrdinal];
				LogEventInfo logEventInfo = new LogEventInfo();
				logEventInfo.Level = level;
				logEventInfo.LoggerName = loggerName;
				logEventInfo.TimeStamp = dateTime.AddTicks(nlogEvent.TimeDelta).ToLocalTime();
				logEventInfo.Message = events.Strings[nlogEvent.MessageOrdinal];
				logEventInfo.Properties.Add("ClientName", events.ClientName);
				for (int j = 0; j < events.LayoutNames.Count; j++)
				{
					logEventInfo.Properties.Add(events.LayoutNames[j], events.Strings[nlogEvent.ValueIndexes[j]]);
				}
				array[i] = logEventInfo;
			}
			this.ProcessLogMessages(array);
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x00018E34 File Offset: 0x00017034
		protected virtual void ProcessLogMessages(LogEventInfo[] logEvents)
		{
			ILogger logger = null;
			string b = string.Empty;
			foreach (LogEventInfo logEventInfo in logEvents)
			{
				if (logEventInfo.LoggerName != b)
				{
					if (this.logFactory != null)
					{
						logger = this.logFactory.GetLogger(logEventInfo.LoggerName);
					}
					else
					{
						logger = LogManager.GetLogger(logEventInfo.LoggerName);
					}
					b = logEventInfo.LoggerName;
				}
				logger.Log(logEventInfo);
			}
		}

		// Token: 0x040002A5 RID: 677
		private readonly LogFactory logFactory;
	}
}
