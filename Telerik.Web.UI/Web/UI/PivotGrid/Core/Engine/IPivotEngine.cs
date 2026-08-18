using System;

namespace Telerik.Web.UI.PivotGrid.Core.Engine
{
	// Token: 0x02000D3A RID: 3386
	internal interface IPivotEngine : IPivotResults, IAggregateResultProvider
	{
		// Token: 0x14000132 RID: 306
		// (add) Token: 0x06007DD5 RID: 32213
		// (remove) Token: 0x06007DD6 RID: 32214
		event EventHandler<PivotEngineCompletedEventArgs> Completed;

		// Token: 0x06007DD7 RID: 32215
		void RebuildCube(ParallelState state);

		// Token: 0x06007DD8 RID: 32216
		void Clear();

		// Token: 0x06007DD9 RID: 32217
		void WaitForParallel();

		// Token: 0x06007DDA RID: 32218
		void RebuildCubeParallel(ParallelState state);
	}
}
