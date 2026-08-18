using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using NLog.Common;
using NLog.Config;
using NLog.Internal;
using NLog.Internal.Fakeables;
using NLog.Layouts;

namespace NLog.Targets
{
	// Token: 0x02000158 RID: 344
	[Target("EventLog")]
	public class EventLogTarget : TargetWithLayout, IInstallable
	{
		// Token: 0x06000C7B RID: 3195 RVA: 0x0001CF4F File Offset: 0x0001B14F
		public EventLogTarget() : this(AppDomainWrapper.CurrentDomain)
		{
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x0001CF5C File Offset: 0x0001B15C
		public EventLogTarget(IAppDomain appDomain)
		{
			this.Source = appDomain.FriendlyName;
			this.Log = "Application";
			this.MachineName = ".";
			this.MaxMessageLength = 16384;
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x0001CF96 File Offset: 0x0001B196
		public EventLogTarget(string name) : this(AppDomainWrapper.CurrentDomain)
		{
			base.Name = name;
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000C7E RID: 3198 RVA: 0x0001CFAA File Offset: 0x0001B1AA
		// (set) Token: 0x06000C7F RID: 3199 RVA: 0x0001CFB2 File Offset: 0x0001B1B2
		[DefaultValue(".")]
		public string MachineName { get; set; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000C80 RID: 3200 RVA: 0x0001CFBB File Offset: 0x0001B1BB
		// (set) Token: 0x06000C81 RID: 3201 RVA: 0x0001CFC3 File Offset: 0x0001B1C3
		public Layout EventId { get; set; }

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000C82 RID: 3202 RVA: 0x0001CFCC File Offset: 0x0001B1CC
		// (set) Token: 0x06000C83 RID: 3203 RVA: 0x0001CFD4 File Offset: 0x0001B1D4
		public Layout Category { get; set; }

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000C84 RID: 3204 RVA: 0x0001CFDD File Offset: 0x0001B1DD
		// (set) Token: 0x06000C85 RID: 3205 RVA: 0x0001CFE5 File Offset: 0x0001B1E5
		public Layout EntryType { get; set; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000C86 RID: 3206 RVA: 0x0001CFEE File Offset: 0x0001B1EE
		// (set) Token: 0x06000C87 RID: 3207 RVA: 0x0001CFF6 File Offset: 0x0001B1F6
		public Layout Source { get; set; }

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000C88 RID: 3208 RVA: 0x0001CFFF File Offset: 0x0001B1FF
		// (set) Token: 0x06000C89 RID: 3209 RVA: 0x0001D007 File Offset: 0x0001B207
		[DefaultValue("Application")]
		public string Log { get; set; }

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000C8A RID: 3210 RVA: 0x0001D010 File Offset: 0x0001B210
		// (set) Token: 0x06000C8B RID: 3211 RVA: 0x0001D018 File Offset: 0x0001B218
		[DefaultValue(16384)]
		public int MaxMessageLength
		{
			get
			{
				return this.maxMessageLength;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentException("MaxMessageLength cannot be zero or negative.");
				}
				this.maxMessageLength = value;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000C8C RID: 3212 RVA: 0x0001D030 File Offset: 0x0001B230
		// (set) Token: 0x06000C8D RID: 3213 RVA: 0x0001D038 File Offset: 0x0001B238
		[DefaultValue(EventLogTargetOverflowAction.Truncate)]
		public EventLogTargetOverflowAction OnOverflow { get; set; }

		// Token: 0x06000C8E RID: 3214 RVA: 0x0001D044 File Offset: 0x0001B244
		public void Install(InstallationContext installationContext)
		{
			string fixedSource = this.GetFixedSource();
			this.CreateEventSourceIfNeeded(fixedSource, true);
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x0001D060 File Offset: 0x0001B260
		public void Uninstall(InstallationContext installationContext)
		{
			string fixedSource = this.GetFixedSource();
			if (string.IsNullOrEmpty(fixedSource))
			{
				InternalLogger.Debug("Skipping removing of event source because it contains layout renderers");
				return;
			}
			EventLog.DeleteEventSource(fixedSource, this.MachineName);
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x0001D094 File Offset: 0x0001B294
		public bool? IsInstalled(InstallationContext installationContext)
		{
			string fixedSource = this.GetFixedSource();
			if (!string.IsNullOrEmpty(fixedSource))
			{
				return new bool?(EventLog.SourceExists(fixedSource, this.MachineName));
			}
			InternalLogger.Debug("Unclear if event source exists because it contains layout renderers");
			return null;
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x0001D0D8 File Offset: 0x0001B2D8
		protected override void InitializeTarget()
		{
			base.InitializeTarget();
			string fixedSource = this.GetFixedSource();
			if (string.IsNullOrEmpty(fixedSource))
			{
				InternalLogger.Debug("Skipping creation of event source because it contains layout renderers");
				return;
			}
			string text = EventLog.LogNameFromSourceName(fixedSource, this.MachineName);
			if (!text.Equals(this.Log, StringComparison.CurrentCultureIgnoreCase))
			{
				this.CreateEventSourceIfNeeded(fixedSource, false);
			}
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x0001D12C File Offset: 0x0001B32C
		protected override void Write(LogEventInfo logEvent)
		{
			string text = this.Layout.Render(logEvent);
			EventLogEntryType entryType = this.GetEntryType(logEvent);
			int eventID = 0;
			if (this.EventId != null)
			{
				eventID = Convert.ToInt32(this.EventId.Render(logEvent), CultureInfo.InvariantCulture);
			}
			short category = 0;
			if (this.Category != null)
			{
				category = Convert.ToInt16(this.Category.Render(logEvent), CultureInfo.InvariantCulture);
			}
			EventLog eventLog = this.GetEventLog(logEvent);
			if (text.Length > this.MaxMessageLength)
			{
				if (this.OnOverflow == EventLogTargetOverflowAction.Truncate)
				{
					text = text.Substring(0, this.MaxMessageLength);
					eventLog.WriteEntry(text, entryType, eventID, category);
					return;
				}
				if (this.OnOverflow == EventLogTargetOverflowAction.Split)
				{
					for (int i = 0; i < text.Length; i += this.MaxMessageLength)
					{
						string message = text.Substring(i, Math.Min(this.MaxMessageLength, text.Length - i));
						eventLog.WriteEntry(message, entryType, eventID, category);
					}
					return;
				}
				if (this.OnOverflow == EventLogTargetOverflowAction.Discard)
				{
					return;
				}
			}
			else
			{
				eventLog.WriteEntry(text, entryType, eventID, category);
			}
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x0001D22C File Offset: 0x0001B42C
		private EventLogEntryType GetEntryType(LogEventInfo logEvent)
		{
			if (this.EntryType != null)
			{
				string value = this.EntryType.Render(logEvent);
				EventLogEntryType result;
				if (EnumHelpers.TryParse<EventLogEntryType>(value, true, out result))
				{
					return result;
				}
			}
			if (logEvent.Level >= LogLevel.Error)
			{
				return EventLogEntryType.Error;
			}
			if (logEvent.Level >= LogLevel.Warn)
			{
				return EventLogEntryType.Warning;
			}
			return EventLogEntryType.Information;
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x0001D284 File Offset: 0x0001B484
		internal string GetFixedSource()
		{
			if (this.Source == null)
			{
				return null;
			}
			SimpleLayout simpleLayout = this.Source as SimpleLayout;
			if (simpleLayout != null && simpleLayout.IsFixedText)
			{
				return simpleLayout.FixedText;
			}
			return null;
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x0001D2BC File Offset: 0x0001B4BC
		private EventLog GetEventLog(LogEventInfo logEvent)
		{
			string text = (this.Source != null) ? this.Source.Render(logEvent) : null;
			if (this.eventLogInstance == null || !(text == this.eventLogInstance.Source) || !(this.eventLogInstance.Log == this.Log) || !(this.eventLogInstance.MachineName == this.MachineName))
			{
				this.eventLogInstance = new EventLog(this.Log, this.MachineName, text);
			}
			return this.eventLogInstance;
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x0001D350 File Offset: 0x0001B550
		private void CreateEventSourceIfNeeded(string fixedSource, bool alwaysThrowError)
		{
			if (string.IsNullOrEmpty(fixedSource))
			{
				InternalLogger.Debug("Skipping creation of event source because it contains layout renderers");
				return;
			}
			try
			{
				if (EventLog.SourceExists(fixedSource, this.MachineName))
				{
					string text = EventLog.LogNameFromSourceName(fixedSource, this.MachineName);
					if (!text.Equals(this.Log, StringComparison.CurrentCultureIgnoreCase))
					{
						EventLog.DeleteEventSource(fixedSource, this.MachineName);
						EventSourceCreationData sourceData = new EventSourceCreationData(fixedSource, this.Log)
						{
							MachineName = this.MachineName
						};
						EventLog.CreateEventSource(sourceData);
					}
				}
				else
				{
					EventSourceCreationData sourceData2 = new EventSourceCreationData(fixedSource, this.Log)
					{
						MachineName = this.MachineName
					};
					EventLog.CreateEventSource(sourceData2);
				}
			}
			catch (Exception ex)
			{
				InternalLogger.Error(ex, "Error when connecting to EventLog.");
				if (alwaysThrowError || ex.MustBeRethrown())
				{
					throw;
				}
			}
		}

		// Token: 0x0400032E RID: 814
		private EventLog eventLogInstance;

		// Token: 0x0400032F RID: 815
		private int maxMessageLength;
	}
}
