using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;

namespace System.Diagnostics
{
	// Token: 0x0200075B RID: 1883
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
	public sealed class EventLogTraceListener : TraceListener
	{
		// Token: 0x060039BC RID: 14780 RVA: 0x000F4BAF File Offset: 0x000F3BAF
		public EventLogTraceListener()
		{
		}

		// Token: 0x060039BD RID: 14781 RVA: 0x000F4BB7 File Offset: 0x000F3BB7
		public EventLogTraceListener(EventLog eventLog) : base((eventLog != null) ? eventLog.Source : string.Empty)
		{
			this.eventLog = eventLog;
		}

		// Token: 0x060039BE RID: 14782 RVA: 0x000F4BD6 File Offset: 0x000F3BD6
		public EventLogTraceListener(string source)
		{
			this.eventLog = new EventLog();
			this.eventLog.Source = source;
		}

		// Token: 0x17000D67 RID: 3431
		// (get) Token: 0x060039BF RID: 14783 RVA: 0x000F4BF5 File Offset: 0x000F3BF5
		// (set) Token: 0x060039C0 RID: 14784 RVA: 0x000F4BFD File Offset: 0x000F3BFD
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

		// Token: 0x17000D68 RID: 3432
		// (get) Token: 0x060039C1 RID: 14785 RVA: 0x000F4C06 File Offset: 0x000F3C06
		// (set) Token: 0x060039C2 RID: 14786 RVA: 0x000F4C36 File Offset: 0x000F3C36
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

		// Token: 0x060039C3 RID: 14787 RVA: 0x000F4C46 File Offset: 0x000F3C46
		public override void Close()
		{
			if (this.eventLog != null)
			{
				this.eventLog.Close();
			}
		}

		// Token: 0x060039C4 RID: 14788 RVA: 0x000F4C5B File Offset: 0x000F3C5B
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
		}

		// Token: 0x060039C5 RID: 14789 RVA: 0x000F4C66 File Offset: 0x000F3C66
		public override void Write(string message)
		{
			if (this.eventLog != null)
			{
				this.eventLog.WriteEntry(message);
			}
		}

		// Token: 0x060039C6 RID: 14790 RVA: 0x000F4C7C File Offset: 0x000F3C7C
		public override void WriteLine(string message)
		{
			this.Write(message);
		}

		// Token: 0x060039C7 RID: 14791 RVA: 0x000F4C88 File Offset: 0x000F3C88
		[ComVisible(false)]
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType severity, int id, string format, params object[] args)
		{
			if (base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, severity, id, format, args))
			{
				return;
			}
			EventInstance instance = this.CreateEventInstance(severity, id);
			if (args == null)
			{
				this.eventLog.WriteEvent(instance, new object[]
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
				this.eventLog.WriteEvent(instance, array);
				return;
			}
			this.eventLog.WriteEvent(instance, new object[]
			{
				string.Format(CultureInfo.InvariantCulture, format, args)
			});
		}

		// Token: 0x060039C8 RID: 14792 RVA: 0x000F4D40 File Offset: 0x000F3D40
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

		// Token: 0x060039C9 RID: 14793 RVA: 0x000F4D90 File Offset: 0x000F3D90
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

		// Token: 0x060039CA RID: 14794 RVA: 0x000F4DE0 File Offset: 0x000F3DE0
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

		// Token: 0x060039CB RID: 14795 RVA: 0x000F4E74 File Offset: 0x000F3E74
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

		// Token: 0x040032DC RID: 13020
		private EventLog eventLog;

		// Token: 0x040032DD RID: 13021
		private bool nameSet;
	}
}
