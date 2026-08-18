using System;
using System.Diagnostics;
using System.Security;
using System.Threading;
using log4net.Core;
using log4net.Layout;
using log4net.Util;

namespace log4net.Appender
{
	// Token: 0x0200001F RID: 31
	public class EventLogAppender : AppenderSkeleton
	{
		// Token: 0x06000106 RID: 262 RVA: 0x0000445F File Offset: 0x0000265F
		public EventLogAppender()
		{
			this.m_applicationName = Thread.GetDomain().FriendlyName;
			this.m_logName = "Application";
			this.m_machineName = ".";
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00004498 File Offset: 0x00002698
		[Obsolete("Instead use the default constructor and set the Layout property")]
		public EventLogAppender(ILayout layout) : this()
		{
			this.Layout = layout;
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000108 RID: 264 RVA: 0x000044A7 File Offset: 0x000026A7
		// (set) Token: 0x06000109 RID: 265 RVA: 0x000044AF File Offset: 0x000026AF
		public string LogName
		{
			get
			{
				return this.m_logName;
			}
			set
			{
				this.m_logName = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600010A RID: 266 RVA: 0x000044B8 File Offset: 0x000026B8
		// (set) Token: 0x0600010B RID: 267 RVA: 0x000044C0 File Offset: 0x000026C0
		public string ApplicationName
		{
			get
			{
				return this.m_applicationName;
			}
			set
			{
				this.m_applicationName = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600010C RID: 268 RVA: 0x000044C9 File Offset: 0x000026C9
		// (set) Token: 0x0600010D RID: 269 RVA: 0x000044D1 File Offset: 0x000026D1
		public string MachineName
		{
			get
			{
				return this.m_machineName;
			}
			set
			{
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000044D3 File Offset: 0x000026D3
		public void AddMapping(EventLogAppender.Level2EventLogEntryType mapping)
		{
			this.m_levelMapping.Add(mapping);
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600010F RID: 271 RVA: 0x000044E1 File Offset: 0x000026E1
		// (set) Token: 0x06000110 RID: 272 RVA: 0x000044E9 File Offset: 0x000026E9
		public log4net.Core.SecurityContext SecurityContext
		{
			get
			{
				return this.m_securityContext;
			}
			set
			{
				this.m_securityContext = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000111 RID: 273 RVA: 0x000044F2 File Offset: 0x000026F2
		// (set) Token: 0x06000112 RID: 274 RVA: 0x000044FA File Offset: 0x000026FA
		public int EventId
		{
			get
			{
				return this.m_eventId;
			}
			set
			{
				this.m_eventId = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00004503 File Offset: 0x00002703
		// (set) Token: 0x06000114 RID: 276 RVA: 0x0000450B File Offset: 0x0000270B
		public short Category
		{
			get
			{
				return this.m_category;
			}
			set
			{
				this.m_category = value;
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00004514 File Offset: 0x00002714
		public override void ActivateOptions()
		{
			try
			{
				base.ActivateOptions();
				if (this.m_securityContext == null)
				{
					this.m_securityContext = SecurityContextProvider.DefaultProvider.CreateSecurityContext(this);
				}
				bool flag = false;
				string text = null;
				using (this.SecurityContext.Impersonate(this))
				{
					flag = EventLog.SourceExists(this.m_applicationName);
					if (flag)
					{
						text = EventLog.LogNameFromSourceName(this.m_applicationName, this.m_machineName);
					}
				}
				if (flag && text != this.m_logName)
				{
					LogLog.Debug(EventLogAppender.declaringType, string.Concat(new string[]
					{
						"Changing event source [",
						this.m_applicationName,
						"] from log [",
						text,
						"] to log [",
						this.m_logName,
						"]"
					}));
				}
				else if (!flag)
				{
					LogLog.Debug(EventLogAppender.declaringType, string.Concat(new string[]
					{
						"Creating event source Source [",
						this.m_applicationName,
						"] in log ",
						this.m_logName,
						"]"
					}));
				}
				string text2 = null;
				using (this.SecurityContext.Impersonate(this))
				{
					if (flag && text != this.m_logName)
					{
						EventLog.DeleteEventSource(this.m_applicationName, this.m_machineName);
						EventLogAppender.CreateEventSource(this.m_applicationName, this.m_logName, this.m_machineName);
						text2 = EventLog.LogNameFromSourceName(this.m_applicationName, this.m_machineName);
					}
					else if (!flag)
					{
						EventLogAppender.CreateEventSource(this.m_applicationName, this.m_logName, this.m_machineName);
						text2 = EventLog.LogNameFromSourceName(this.m_applicationName, this.m_machineName);
					}
				}
				this.m_levelMapping.ActivateOptions();
				LogLog.Debug(EventLogAppender.declaringType, string.Concat(new string[]
				{
					"Source [",
					this.m_applicationName,
					"] is registered to log [",
					text2,
					"]"
				}));
			}
			catch (SecurityException e)
			{
				this.ErrorHandler.Error("Caught a SecurityException trying to access the EventLog.  Most likely the event source " + this.m_applicationName + " doesn't exist and must be created by a local administrator.  Will disable EventLogAppender.  See http://logging.apache.org/log4net/release/faq.html#trouble-EventLog", e);
				base.Threshold = Level.Off;
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00004798 File Offset: 0x00002998
		private static void CreateEventSource(string source, string logName, string machineName)
		{
			EventLog.CreateEventSource(new EventSourceCreationData(source, logName)
			{
				MachineName = machineName
			});
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000047BC File Offset: 0x000029BC
		protected override void Append(LoggingEvent loggingEvent)
		{
			int eventID = this.m_eventId;
			object obj = loggingEvent.LookupProperty("EventID");
			if (obj != null)
			{
				if (obj is int)
				{
					eventID = (int)obj;
				}
				else
				{
					string text = obj as string;
					if (text == null)
					{
						text = obj.ToString();
					}
					if (text != null && text.Length > 0)
					{
						int num;
						if (SystemInfo.TryParse(text, out num))
						{
							eventID = num;
						}
						else
						{
							this.ErrorHandler.Error("Unable to parse event ID property [" + text + "].");
						}
					}
				}
			}
			short category = this.m_category;
			object obj2 = loggingEvent.LookupProperty("Category");
			if (obj2 != null)
			{
				if (obj2 is short)
				{
					category = (short)obj2;
				}
				else
				{
					string text2 = obj2 as string;
					if (text2 == null)
					{
						text2 = obj2.ToString();
					}
					if (text2 != null && text2.Length > 0)
					{
						short num2;
						if (SystemInfo.TryParse(text2, out num2))
						{
							category = num2;
						}
						else
						{
							this.ErrorHandler.Error("Unable to parse event category property [" + text2 + "].");
						}
					}
				}
			}
			try
			{
				string text3 = base.RenderLoggingEvent(loggingEvent);
				if (text3.Length > EventLogAppender.MAX_EVENTLOG_MESSAGE_SIZE)
				{
					text3 = text3.Substring(0, EventLogAppender.MAX_EVENTLOG_MESSAGE_SIZE);
				}
				EventLogEntryType entryType = this.GetEntryType(loggingEvent.Level);
				using (this.SecurityContext.Impersonate(this))
				{
					EventLog.WriteEntry(this.m_applicationName, text3, entryType, eventID, category);
				}
			}
			catch (Exception e)
			{
				this.ErrorHandler.Error(string.Concat(new string[]
				{
					"Unable to write to event log [",
					this.m_logName,
					"] using source [",
					this.m_applicationName,
					"]"
				}), e);
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00004984 File Offset: 0x00002B84
		protected override bool RequiresLayout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00004988 File Offset: 0x00002B88
		protected virtual EventLogEntryType GetEntryType(Level level)
		{
			EventLogAppender.Level2EventLogEntryType level2EventLogEntryType = this.m_levelMapping.Lookup(level) as EventLogAppender.Level2EventLogEntryType;
			if (level2EventLogEntryType != null)
			{
				return level2EventLogEntryType.EventLogEntryType;
			}
			if (level >= Level.Error)
			{
				return EventLogEntryType.Error;
			}
			if (level == Level.Warn)
			{
				return EventLogEntryType.Warning;
			}
			return EventLogEntryType.Information;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000049D0 File Offset: 0x00002BD0
		private static int GetMaxEventLogMessageSize()
		{
			if (Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.Major >= 6)
			{
				return EventLogAppender.MAX_EVENTLOG_MESSAGE_SIZE_VISTA_OR_NEWER;
			}
			return EventLogAppender.MAX_EVENTLOG_MESSAGE_SIZE_DEFAULT;
		}

		// Token: 0x04000075 RID: 117
		private string m_logName;

		// Token: 0x04000076 RID: 118
		private string m_applicationName;

		// Token: 0x04000077 RID: 119
		private string m_machineName;

		// Token: 0x04000078 RID: 120
		private LevelMapping m_levelMapping = new LevelMapping();

		// Token: 0x04000079 RID: 121
		private log4net.Core.SecurityContext m_securityContext;

		// Token: 0x0400007A RID: 122
		private int m_eventId;

		// Token: 0x0400007B RID: 123
		private short m_category;

		// Token: 0x0400007C RID: 124
		private static readonly Type declaringType = typeof(EventLogAppender);

		// Token: 0x0400007D RID: 125
		private static readonly int MAX_EVENTLOG_MESSAGE_SIZE_DEFAULT = 32766;

		// Token: 0x0400007E RID: 126
		private static readonly int MAX_EVENTLOG_MESSAGE_SIZE_VISTA_OR_NEWER = 31837;

		// Token: 0x0400007F RID: 127
		private static readonly int MAX_EVENTLOG_MESSAGE_SIZE = EventLogAppender.GetMaxEventLogMessageSize();

		// Token: 0x02000020 RID: 32
		public class Level2EventLogEntryType : LevelMappingEntry
		{
			// Token: 0x1700004C RID: 76
			// (get) Token: 0x0600011C RID: 284 RVA: 0x00004A2B File Offset: 0x00002C2B
			// (set) Token: 0x0600011D RID: 285 RVA: 0x00004A33 File Offset: 0x00002C33
			public EventLogEntryType EventLogEntryType
			{
				get
				{
					return this.m_entryType;
				}
				set
				{
					this.m_entryType = value;
				}
			}

			// Token: 0x04000080 RID: 128
			private EventLogEntryType m_entryType;
		}
	}
}
