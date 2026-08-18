using System;
using System.Diagnostics.Tracing;

namespace System.Data
{
	// Token: 0x02000147 RID: 327
	[EventSource(Name = "Microsoft-AdoNet-SystemData")]
	internal sealed class SqlEventSource : EventSource
	{
		// Token: 0x0600134E RID: 4942 RVA: 0x00099EE4 File Offset: 0x000992E4
		private SqlEventSource()
		{
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x00099EF8 File Offset: 0x000992F8
		[Event(1, Keywords = (EventKeywords)1L)]
		public void BeginExecute(int objectId, string dataSource, string database, string commandText)
		{
			base.WriteEvent(1, new object[]
			{
				objectId,
				dataSource,
				database,
				commandText
			});
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x00099F28 File Offset: 0x00099328
		[Event(2, Keywords = (EventKeywords)1L)]
		public void EndExecute(int objectId, int compositeState, int sqlExceptionNumber)
		{
			base.WriteEvent(2, objectId, compositeState, sqlExceptionNumber);
		}

		// Token: 0x04000789 RID: 1929
		internal const string EventSourceName = "Microsoft-AdoNet-SystemData";

		// Token: 0x0400078A RID: 1930
		private const int BeginExecuteEventId = 1;

		// Token: 0x0400078B RID: 1931
		private const int EndExecuteEventId = 2;

		// Token: 0x0400078C RID: 1932
		internal static readonly SqlEventSource Log = new SqlEventSource();

		// Token: 0x02000369 RID: 873
		public static class Keywords
		{
			// Token: 0x06003454 RID: 13396 RVA: 0x00140C50 File Offset: 0x00140050
			private static void InternalDoNotCall()
			{
				throw new NotSupportedException();
			}

			// Token: 0x04001F17 RID: 7959
			public const EventKeywords SqlClient = (EventKeywords)1L;
		}

		// Token: 0x0200036A RID: 874
		public static class Tasks
		{
			// Token: 0x06003455 RID: 13397 RVA: 0x00140C64 File Offset: 0x00140064
			private static void InternalDoNotCall()
			{
				throw new NotSupportedException();
			}

			// Token: 0x04001F18 RID: 7960
			public const EventTask ExecuteCommand = (EventTask)1;
		}
	}
}
