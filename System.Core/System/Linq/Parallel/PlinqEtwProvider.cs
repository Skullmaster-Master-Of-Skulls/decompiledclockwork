using System;
using System.Diagnostics.Tracing;
using System.Threading;
using System.Threading.Tasks;

namespace System.Linq.Parallel
{
	// Token: 0x02000202 RID: 514
	[EventSource(Name = "System.Linq.Parallel.PlinqEventSource", Guid = "159eeeec-4a14-4418-a8fe-faabcd987887", LocalizationResources = "System.Linq")]
	internal sealed class PlinqEtwProvider : EventSource
	{
		// Token: 0x0600104B RID: 4171 RVA: 0x000396B9 File Offset: 0x000378B9
		private PlinqEtwProvider()
		{
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x000396C1 File Offset: 0x000378C1
		[NonEvent]
		internal static int NextQueryId()
		{
			return Interlocked.Increment(ref PlinqEtwProvider.s_queryId);
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x000396D0 File Offset: 0x000378D0
		[NonEvent]
		internal void ParallelQueryBegin(int queryId)
		{
			if (base.IsEnabled(EventLevel.Informational, EventKeywords.All))
			{
				int valueOrDefault = Task.CurrentId.GetValueOrDefault();
				this.ParallelQueryBegin(PlinqEtwProvider.s_defaultSchedulerId, valueOrDefault, queryId);
			}
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x00039703 File Offset: 0x00037903
		[Event(1, Level = EventLevel.Informational, Task = (EventTask)1, Opcode = EventOpcode.Start)]
		private void ParallelQueryBegin(int taskSchedulerId, int taskId, int queryId)
		{
			base.WriteEvent(1, taskSchedulerId, taskId, queryId);
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x00039710 File Offset: 0x00037910
		[NonEvent]
		internal void ParallelQueryEnd(int queryId)
		{
			if (base.IsEnabled(EventLevel.Informational, EventKeywords.All))
			{
				int valueOrDefault = Task.CurrentId.GetValueOrDefault();
				this.ParallelQueryEnd(PlinqEtwProvider.s_defaultSchedulerId, valueOrDefault, queryId);
			}
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x00039743 File Offset: 0x00037943
		[Event(2, Level = EventLevel.Informational, Task = (EventTask)1, Opcode = EventOpcode.Stop)]
		private void ParallelQueryEnd(int taskSchedulerId, int taskId, int queryId)
		{
			base.WriteEvent(2, taskSchedulerId, taskId, queryId);
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x00039750 File Offset: 0x00037950
		[NonEvent]
		internal void ParallelQueryFork(int queryId)
		{
			if (base.IsEnabled(EventLevel.Verbose, EventKeywords.All))
			{
				int valueOrDefault = Task.CurrentId.GetValueOrDefault();
				this.ParallelQueryFork(PlinqEtwProvider.s_defaultSchedulerId, valueOrDefault, queryId);
			}
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x00039783 File Offset: 0x00037983
		[Event(3, Level = EventLevel.Verbose, Task = (EventTask)2, Opcode = EventOpcode.Start)]
		private void ParallelQueryFork(int taskSchedulerId, int taskId, int queryId)
		{
			base.WriteEvent(3, taskSchedulerId, taskId, queryId);
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x00039790 File Offset: 0x00037990
		[NonEvent]
		internal void ParallelQueryJoin(int queryId)
		{
			if (base.IsEnabled(EventLevel.Verbose, EventKeywords.All))
			{
				int valueOrDefault = Task.CurrentId.GetValueOrDefault();
				this.ParallelQueryJoin(PlinqEtwProvider.s_defaultSchedulerId, valueOrDefault, queryId);
			}
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x000397C3 File Offset: 0x000379C3
		[Event(4, Level = EventLevel.Verbose, Task = (EventTask)2, Opcode = EventOpcode.Stop)]
		private void ParallelQueryJoin(int taskSchedulerId, int taskId, int queryId)
		{
			base.WriteEvent(4, taskSchedulerId, taskId, queryId);
		}

		// Token: 0x0400093C RID: 2364
		internal static PlinqEtwProvider Log = new PlinqEtwProvider();

		// Token: 0x0400093D RID: 2365
		private static readonly int s_defaultSchedulerId = TaskScheduler.Default.Id;

		// Token: 0x0400093E RID: 2366
		private static int s_queryId = 0;

		// Token: 0x0400093F RID: 2367
		private const EventKeywords ALL_KEYWORDS = EventKeywords.All;

		// Token: 0x04000940 RID: 2368
		private const int PARALLELQUERYBEGIN_EVENTID = 1;

		// Token: 0x04000941 RID: 2369
		private const int PARALLELQUERYEND_EVENTID = 2;

		// Token: 0x04000942 RID: 2370
		private const int PARALLELQUERYFORK_EVENTID = 3;

		// Token: 0x04000943 RID: 2371
		private const int PARALLELQUERYJOIN_EVENTID = 4;

		// Token: 0x0200041B RID: 1051
		public class Tasks
		{
			// Token: 0x04001287 RID: 4743
			public const EventTask Query = (EventTask)1;

			// Token: 0x04001288 RID: 4744
			public const EventTask ForkJoin = (EventTask)2;
		}
	}
}
