using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Xml;
using NLog.Time;

namespace NLog
{
	// Token: 0x02000143 RID: 323
	public class NLogTraceListener : TraceListener
	{
		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000B55 RID: 2901 RVA: 0x00019D7D File Offset: 0x00017F7D
		// (set) Token: 0x06000B56 RID: 2902 RVA: 0x00019D8B File Offset: 0x00017F8B
		public LogFactory LogFactory
		{
			get
			{
				this.InitAttributes();
				return this.logFactory;
			}
			set
			{
				this.attributesLoaded = true;
				this.logFactory = value;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000B57 RID: 2903 RVA: 0x00019D9B File Offset: 0x00017F9B
		// (set) Token: 0x06000B58 RID: 2904 RVA: 0x00019DA9 File Offset: 0x00017FA9
		public LogLevel DefaultLogLevel
		{
			get
			{
				this.InitAttributes();
				return this.defaultLogLevel;
			}
			set
			{
				this.attributesLoaded = true;
				this.defaultLogLevel = value;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000B59 RID: 2905 RVA: 0x00019DB9 File Offset: 0x00017FB9
		// (set) Token: 0x06000B5A RID: 2906 RVA: 0x00019DC7 File Offset: 0x00017FC7
		public LogLevel ForceLogLevel
		{
			get
			{
				this.InitAttributes();
				return this.forceLogLevel;
			}
			set
			{
				this.attributesLoaded = true;
				this.forceLogLevel = value;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000B5B RID: 2907 RVA: 0x00019DD7 File Offset: 0x00017FD7
		// (set) Token: 0x06000B5C RID: 2908 RVA: 0x00019DE5 File Offset: 0x00017FE5
		public bool DisableFlush
		{
			get
			{
				this.InitAttributes();
				return this.disableFlush;
			}
			set
			{
				this.attributesLoaded = true;
				this.disableFlush = value;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000B5D RID: 2909 RVA: 0x00019DF5 File Offset: 0x00017FF5
		public override bool IsThreadSafe
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000B5E RID: 2910 RVA: 0x00019DF8 File Offset: 0x00017FF8
		// (set) Token: 0x06000B5F RID: 2911 RVA: 0x00019E06 File Offset: 0x00018006
		public bool AutoLoggerName
		{
			get
			{
				this.InitAttributes();
				return this.autoLoggerName;
			}
			set
			{
				this.attributesLoaded = true;
				this.autoLoggerName = value;
			}
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x00019E18 File Offset: 0x00018018
		public override void Write(string message)
		{
			this.ProcessLogEventInfo(this.DefaultLogLevel, null, message, null, null, new TraceEventType?(TraceEventType.Resume), null);
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x00019E50 File Offset: 0x00018050
		public override void WriteLine(string message)
		{
			this.ProcessLogEventInfo(this.DefaultLogLevel, null, message, null, null, new TraceEventType?(TraceEventType.Resume), null);
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x00019E88 File Offset: 0x00018088
		public override void Close()
		{
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x00019E8C File Offset: 0x0001808C
		public override void Fail(string message)
		{
			this.ProcessLogEventInfo(LogLevel.Error, null, message, null, null, new TraceEventType?(TraceEventType.Error), null);
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x00019EC0 File Offset: 0x000180C0
		public override void Fail(string message, string detailMessage)
		{
			this.ProcessLogEventInfo(LogLevel.Error, null, message + " " + detailMessage, null, null, new TraceEventType?(TraceEventType.Error), null);
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x00019EFE File Offset: 0x000180FE
		public override void Flush()
		{
			if (!this.DisableFlush)
			{
				if (this.LogFactory != null)
				{
					this.LogFactory.Flush();
					return;
				}
				LogManager.Flush();
			}
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x00019F24 File Offset: 0x00018124
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
		{
			this.TraceData(eventCache, source, eventType, id, new object[]
			{
				data
			});
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x00019F4C File Offset: 0x0001814C
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < data.Length; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append("{");
				stringBuilder.Append(i);
				stringBuilder.Append("}");
			}
			this.ProcessLogEventInfo(NLogTraceListener.TranslateLogLevel(eventType), source, stringBuilder.ToString(), data, new int?(id), new TraceEventType?(eventType), null);
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x00019FCC File Offset: 0x000181CC
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id)
		{
			this.ProcessLogEventInfo(NLogTraceListener.TranslateLogLevel(eventType), source, string.Empty, null, new int?(id), new TraceEventType?(eventType), null);
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x0001A004 File Offset: 0x00018204
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
		{
			this.ProcessLogEventInfo(NLogTraceListener.TranslateLogLevel(eventType), source, format, args, new int?(id), new TraceEventType?(eventType), null);
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x0001A038 File Offset: 0x00018238
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
		{
			this.ProcessLogEventInfo(NLogTraceListener.TranslateLogLevel(eventType), source, message, null, new int?(id), new TraceEventType?(eventType), null);
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x0001A06B File Offset: 0x0001826B
		public override void TraceTransfer(TraceEventCache eventCache, string source, int id, string message, Guid relatedActivityId)
		{
			this.ProcessLogEventInfo(LogLevel.Debug, source, message, null, new int?(id), new TraceEventType?(TraceEventType.Transfer), new Guid?(relatedActivityId));
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x0001A094 File Offset: 0x00018294
		protected override string[] GetSupportedAttributes()
		{
			return new string[]
			{
				"defaultLogLevel",
				"autoLoggerName",
				"forceLogLevel",
				"disableFlush"
			};
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x0001A0CC File Offset: 0x000182CC
		private static LogLevel TranslateLogLevel(TraceEventType eventType)
		{
			switch (eventType)
			{
			case TraceEventType.Critical:
				return LogLevel.Fatal;
			case TraceEventType.Error:
				return LogLevel.Error;
			case (TraceEventType)3:
				break;
			case TraceEventType.Warning:
				return LogLevel.Warn;
			default:
				if (eventType == TraceEventType.Information)
				{
					return LogLevel.Info;
				}
				if (eventType == TraceEventType.Verbose)
				{
					return LogLevel.Trace;
				}
				break;
			}
			return LogLevel.Debug;
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x0001A120 File Offset: 0x00018320
		protected virtual void ProcessLogEventInfo(LogLevel logLevel, string loggerName, [Localizable(false)] string message, object[] arguments, int? eventId, TraceEventType? eventType, Guid? relatedActiviyId)
		{
			LogEventInfo logEventInfo = new LogEventInfo();
			logEventInfo.LoggerName = ((loggerName ?? this.Name) ?? string.Empty);
			if (this.AutoLoggerName)
			{
				StackTrace stackTrace = new StackTrace();
				int num = -1;
				MethodBase methodBase = null;
				for (int i = 0; i < stackTrace.FrameCount; i++)
				{
					StackFrame frame = stackTrace.GetFrame(i);
					MethodBase method = frame.GetMethod();
					if (!(method.DeclaringType == base.GetType()) && !(method.DeclaringType.Assembly == NLogTraceListener.systemAssembly))
					{
						num = i;
						methodBase = method;
						break;
					}
				}
				if (num >= 0)
				{
					logEventInfo.SetStackTrace(stackTrace, num);
					if (methodBase.DeclaringType != null)
					{
						logEventInfo.LoggerName = methodBase.DeclaringType.FullName;
					}
				}
			}
			if (eventType != null)
			{
				logEventInfo.Properties.Add("EventType", eventType.Value);
			}
			if (relatedActiviyId != null)
			{
				logEventInfo.Properties.Add("RelatedActivityID", relatedActiviyId.Value);
			}
			logEventInfo.TimeStamp = TimeSource.Current.Time;
			logEventInfo.Message = message;
			logEventInfo.Parameters = arguments;
			logEventInfo.Level = (this.forceLogLevel ?? logLevel);
			if (eventId != null)
			{
				logEventInfo.Properties.Add("EventID", eventId.Value);
			}
			ILogger logger;
			if (this.LogFactory != null)
			{
				logger = this.LogFactory.GetLogger(logEventInfo.LoggerName);
			}
			else
			{
				logger = LogManager.GetLogger(logEventInfo.LoggerName);
			}
			logger.Log(logEventInfo);
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x0001A2BC File Offset: 0x000184BC
		private void InitAttributes()
		{
			if (!this.attributesLoaded)
			{
				this.attributesLoaded = true;
				foreach (object obj in base.Attributes)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					string text = (string)dictionaryEntry.Key;
					string text2 = (string)dictionaryEntry.Value;
					string a;
					if ((a = text.ToUpperInvariant()) != null)
					{
						if (!(a == "DEFAULTLOGLEVEL"))
						{
							if (!(a == "FORCELOGLEVEL"))
							{
								if (!(a == "AUTOLOGGERNAME"))
								{
									if (a == "DISABLEFLUSH")
									{
										this.disableFlush = bool.Parse(text2);
									}
								}
								else
								{
									this.AutoLoggerName = XmlConvert.ToBoolean(text2);
								}
							}
							else
							{
								this.forceLogLevel = LogLevel.FromString(text2);
							}
						}
						else
						{
							this.defaultLogLevel = LogLevel.FromString(text2);
						}
					}
				}
			}
		}

		// Token: 0x040002BC RID: 700
		private static readonly Assembly systemAssembly = typeof(Trace).Assembly;

		// Token: 0x040002BD RID: 701
		private LogFactory logFactory;

		// Token: 0x040002BE RID: 702
		private LogLevel defaultLogLevel = LogLevel.Debug;

		// Token: 0x040002BF RID: 703
		private bool attributesLoaded;

		// Token: 0x040002C0 RID: 704
		private bool autoLoggerName;

		// Token: 0x040002C1 RID: 705
		private LogLevel forceLogLevel;

		// Token: 0x040002C2 RID: 706
		private bool disableFlush;
	}
}
