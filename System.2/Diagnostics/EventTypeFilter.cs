using System;

namespace System.Diagnostics
{
	// Token: 0x020004A0 RID: 1184
	public class EventTypeFilter : TraceFilter
	{
		// Token: 0x06002BF0 RID: 11248 RVA: 0x000C6CD4 File Offset: 0x000C4ED4
		public EventTypeFilter(SourceLevels level)
		{
			this.level = level;
		}

		// Token: 0x06002BF1 RID: 11249 RVA: 0x000C6CE3 File Offset: 0x000C4EE3
		public override bool ShouldTrace(TraceEventCache cache, string source, TraceEventType eventType, int id, string formatOrMessage, object[] args, object data1, object[] data)
		{
			return (eventType & (TraceEventType)this.level) > (TraceEventType)0;
		}

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x06002BF2 RID: 11250 RVA: 0x000C6CF0 File Offset: 0x000C4EF0
		// (set) Token: 0x06002BF3 RID: 11251 RVA: 0x000C6CF8 File Offset: 0x000C4EF8
		public SourceLevels EventType
		{
			get
			{
				return this.level;
			}
			set
			{
				this.level = value;
			}
		}

		// Token: 0x040026A2 RID: 9890
		private SourceLevels level;
	}
}
