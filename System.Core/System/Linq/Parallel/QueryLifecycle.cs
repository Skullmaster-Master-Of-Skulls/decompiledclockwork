using System;
using System.Diagnostics;

namespace System.Linq.Parallel
{
	// Token: 0x020001ED RID: 493
	internal static class QueryLifecycle
	{
		// Token: 0x06000FEA RID: 4074 RVA: 0x0003845C File Offset: 0x0003665C
		internal static void LogicalQueryExecutionBegin(int queryID)
		{
			Debugger.NotifyOfCrossThreadDependency();
			PlinqEtwProvider.Log.ParallelQueryBegin(queryID);
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x0003846E File Offset: 0x0003666E
		internal static void LogicalQueryExecutionEnd(int queryID)
		{
			PlinqEtwProvider.Log.ParallelQueryEnd(queryID);
		}
	}
}
