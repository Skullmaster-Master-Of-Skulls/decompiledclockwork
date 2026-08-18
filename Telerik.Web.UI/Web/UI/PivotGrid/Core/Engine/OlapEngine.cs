using System;
using System.Collections.Generic;
using Telerik.Web.UI.PivotGrid.Core.Olap;

namespace Telerik.Web.UI.PivotGrid.Core.Engine
{
	// Token: 0x020006A9 RID: 1705
	internal class OlapEngine : CompositeEngine<IPivotResults>
	{
		// Token: 0x06003D75 RID: 15733 RVA: 0x000C5A5C File Offset: 0x000C3C5C
		protected override IPivotResults PrepareResult(object finalState)
		{
			PivotResultsProcessingState state = finalState as PivotResultsProcessingState;
			return new OlapPivotResults(state);
		}

		// Token: 0x06003D76 RID: 15734 RVA: 0x000C5A78 File Offset: 0x000C3C78
		protected override Queue<IEngineTask> PrepareTasks(object initialState)
		{
			Queue<IEngineTask> queue = new Queue<IEngineTask>();
			queue.Enqueue(new GenerateAllKeysTask());
			queue.Enqueue(new SortingTask());
			queue.Enqueue(new FormatTotalsTask());
			return queue;
		}
	}
}
