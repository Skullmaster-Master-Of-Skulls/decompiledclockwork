using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;

namespace System.Diagnostics
{
	// Token: 0x020004D5 RID: 1237
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
	public sealed class EventLogTraceListener : TraceListener
	{
		// Token: 0x06002E9C RID: 11932 RVA: 0x000D206B File Offset: 0x000D026B
		public EventLogTraceListener()
		{
		}

		// Token: 0x06002E9D RID: 11933 RVA: 0x000D2073 File Offset: 0x000D0273
		public EventLogTraceListener(EventLog eventLog) : base((eventLog != null) ? eventLog.Source : string.Empty)
		{
			this.eventLog = eventLog;
		}

		// Token: 0x06002E9E RID: 11934 RVA: 0x000D2092 File Offset: 0x000D0292
		public EventLogTraceListener(string source)
		{
			this.eventLog = new EventLog();
			this.eventLog.Source = source;
		}

		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x06002E9F RID: 11935 RVA: 0x000D20B1 File Offset: 0x000D02B1
		// (set) Token: 0x06002EA0 RID: 11936 RVA: 0x000D20B9 File Offset: 0x000D02B9
		public EventLog EventLog
		{
			get
			{
				return this.eventLog;
			}
			set
			{
				this.eventLog = value;
			}
		}

		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x06002EA1 RID: 11937 RVA: 0x000D20C2 File Offset: 0x000D02C2
		// (set) Token: 0x06002EA2 RID: 11938 RVA: 0x000D20F2 File Offset: 0x000D02F2
		public override string Name
		{
			get
			{
				if (!this.nameSet && this.eventLog != null)
				{
					this.nameSet = true;
					base.Name = this.eventLog.Source;
				}
				return base.Name;
			}
			set
			{
				this.nameSet = true;
				base.Name = value;
			}
		}

		// Token: 0x06002EA3 RID: 11939 RVA: 0x000D2102 File Offset: 0x000D0302
		public override void Close()
		{
			if (this.eventLog != null)
			{
				this.eventLog.Close();
			}
		}

		// Token: 0x06002EA4 RID: 11940 RVA: 0x000D2118 File Offset: 0x000D0318
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.Close();
				}
				else
				{
					if (this.eventLog != null)
					{
						this.eventLog.Close();
					}
					this.eventLog = null;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06002EA5 RID: 11941 RVA: 0x000D2164 File Offset: 0x000D0364
		public override void Write(string message)
		{
			if (this.eventLog != null)
			{
				this.eventLog.WriteEntry(message);
			}
		}

		// Token: 0x06002EA6 RID: 11942 RVA: 0x000D217A File Offset: 0x000D037A
		public override void WriteLine(string message)
		{
			this.Write(message);
		}

		// Token: 0x06002EA7 RID: 11943 RVA: 0x000D2184 File Offset: 0x000D0384
		[ComVisible(false)]
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType severity, int id, string format, params object[] args)
		{
			if (base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, severity, id, format, args))
			{
				return;
			}
			EventInstance eventInstance = this.CreateEventInstance(severity, id);
			if (args == null)
			{
				this.eventLog.WriteEvent(eventInstance, new object[]
				{
					format
				});
				return;
			}
			if (string.IsNullOrEmpty(format))
			{
				string[] array = new string[args.Length];
				for (int i = 0; i < args.Length; i++)
				{
					array[i] = args[i].ToString();
				}
				EventLog eventLog = this.eventLog;
				EventInstance instance = eventInstance;
				object[] values = array;
				eventLog.WriteEvent(instance, values);
				return;
			}
			this.eventLog.WriteEvent(eventInstance, new object[]
			{
				string.Format(CultureInfo.InvariantCulture, format, args)
			});
		}

		// Token: 0x06002EA8 RID: 11944 RVA: 0x000D2238 File Offset: 0x000D0438
		[ComVisible(false)]
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType severity, int id, string message)
		{
			if (base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, severity, id, message))
			{
				return;
			}
			EventInstance instance = this.CreateEventInstance(severity, id);
			this.eventLog.WriteEvent(instance, new object[]
			{
				message
			});
		}

		// Token: 0x06002EA9 RID: 11945 RVA: 0x000D2284 File Offset: 0x000D0484
		[ComVisible(false)]
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType severity, int id, object data)
		{
			if (base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, severity, id, null, null, data))
			{
				return;
			}
			EventInstance instance = this.CreateEventInstance(severity, id);
			this.eventLog.WriteEvent(instance, new object[]
			{
				data
			});
		}

		// Token: 0x06002EAA RID: 11946 RVA: 0x000D22D4 File Offset: 0x000D04D4
		[ComVisible(false)]
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType severity, int id, params object[] data)
		{
			if (base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, severity, id, null, null, null, data))
			{
				return;
			}
			EventInstance instance = this.CreateEventInstance(severity, id);
			StringBuilder stringBuilder = new StringBuilder();
			if (data != null)
			{
				for (int i = 0; i < data.Length; i++)
				{
					if (i != 0)
					{
						stringBuilder.Append(", ");
					}
					if (data[i] != null)
					{
						stringBuilder.Append(data[i].ToString());
					}
				}
			}
			this.eventLog.WriteEvent(instance, new object[]
			{
				stringBuilder.ToString()
			});
		}

		// Token: 0x06002EAB RID: 11947 RVA: 0x000D2364 File Offset: 0x000D0564
		private EventInstance CreateEventInstance(TraceEventType severity, int id)
		{
			if (id > 65535)
			{
				id = 65535;
			}
			if (id < 0)
			{
				id = 0;
			}
			EventInstance eventInstance = new EventInstance((long)id, 0);
			if (severity == TraceEventType.Error || severity == TraceEventType.Critical)
			{
				eventInstance.EntryType = EventLogEntryType.Error;
			}
			else if (severity == TraceEventType.Warning)
			{
				eventInstance.EntryType = EventLogEntryType.Warning;
			}
			else
			{
				eventInstance.EntryType = EventLogEntryType.Information;
			}
			return eventInstance;
		}

		// Token: 0x04002784 RID: 10116
		private EventLog eventLog;

		// Token: 0x04002785 RID: 10117
		private bool nameSet;
	}
}
