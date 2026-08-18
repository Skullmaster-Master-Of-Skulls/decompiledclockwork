using System;

namespace System.Diagnostics
{
	// Token: 0x020004B0 RID: 1200
	public abstract class TraceFilter
	{
		// Token: 0x06002C94 RID: 11412
		public abstract bool ShouldTrace(TraceEventCache cache, string source, TraceEventType eventType, int id, string formatOrMessage, object[] args, object data1, object[] data);

		// Token: 0x06002C95 RID: 11413 RVA: 0x000C81FC File Offset: 0x000C63FC
		internal bool ShouldTrace(TraceEventCache cache, string source, TraceEventType eventType, int id, string formatOrMessage)
		{
			return this.ShouldTrace(cache, source, eventType, id, formatOrMessage, null, null, null);
		}

		// Token: 0x06002C96 RID: 11414 RVA: 0x000C821C File Offset: 0x000C641C
		internal bool ShouldTrace(TraceEventCache cache, string source, TraceEventType eventType, int id, string formatOrMessage, object[] args)
		{
			return this.ShouldTrace(cache, source, eventType, id, formatOrMessage, args, null, null);
		}

		// Token: 0x06002C97 RID: 11415 RVA: 0x000C823C File Offset: 0x000C643C
		internal bool ShouldTrace(TraceEventCache cache, string source, TraceEventType eventType, int id, string formatOrMessage, object[] args, object data1)
		{
			return this.ShouldTrace(cache, source, eventType, id, formatOrMessage, args, data1, null);
		}

		// Token: 0x040026E1 RID: 9953
		internal string initializeData;
	}
}
