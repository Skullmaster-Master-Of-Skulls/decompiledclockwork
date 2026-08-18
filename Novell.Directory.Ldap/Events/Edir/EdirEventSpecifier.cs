using System;

namespace Novell.Directory.Ldap.Events.Edir
{
	// Token: 0x0200008B RID: 139
	public class EdirEventSpecifier
	{
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x00015098 File Offset: 0x00014098
		public EdirEventType EventType
		{
			get
			{
				return this.event_type;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x000150B0 File Offset: 0x000140B0
		public EdirEventResultType EventResultType
		{
			get
			{
				return this.event_result_type;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x000150C8 File Offset: 0x000140C8
		public string EventFilter
		{
			get
			{
				return this.event_filter;
			}
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x000150E0 File Offset: 0x000140E0
		public EdirEventSpecifier(EdirEventType eventType, EdirEventResultType eventResultType) : this(eventType, eventResultType, null)
		{
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x000150F8 File Offset: 0x000140F8
		public EdirEventSpecifier(EdirEventType eventType, EdirEventResultType eventResultType, string filter)
		{
			this.event_type = eventType;
			this.event_result_type = eventResultType;
			this.event_filter = filter;
		}

		// Token: 0x0400032B RID: 811
		private EdirEventType event_type;

		// Token: 0x0400032C RID: 812
		private EdirEventResultType event_result_type;

		// Token: 0x0400032D RID: 813
		private string event_filter;
	}
}
