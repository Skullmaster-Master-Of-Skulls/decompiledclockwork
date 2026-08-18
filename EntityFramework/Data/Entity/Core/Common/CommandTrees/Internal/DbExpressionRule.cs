using System;

namespace System.Data.Entity.Core.Common.CommandTrees.Internal
{
	// Token: 0x02000123 RID: 291
	internal abstract class DbExpressionRule
	{
		// Token: 0x0600092D RID: 2349
		internal abstract bool ShouldProcess(DbExpression expression);

		// Token: 0x0600092E RID: 2350
		internal abstract bool TryProcess(DbExpression expression, out DbExpression result);

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600092F RID: 2351
		internal abstract DbExpressionRule.ProcessedAction OnExpressionProcessed { get; }

		// Token: 0x02000124 RID: 292
		internal enum ProcessedAction
		{
			// Token: 0x04000290 RID: 656
			Continue,
			// Token: 0x04000291 RID: 657
			Reset,
			// Token: 0x04000292 RID: 658
			Stop
		}
	}
}
