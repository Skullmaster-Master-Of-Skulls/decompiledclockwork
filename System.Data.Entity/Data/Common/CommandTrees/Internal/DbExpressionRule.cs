using System;

namespace System.Data.Common.CommandTrees.Internal
{
	// Token: 0x0200042E RID: 1070
	internal abstract class DbExpressionRule
	{
		// Token: 0x0600397B RID: 14715
		internal abstract bool ShouldProcess(DbExpression expression);

		// Token: 0x0600397C RID: 14716
		internal abstract bool TryProcess(DbExpression expression, out DbExpression result);

		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x0600397D RID: 14717
		internal abstract DbExpressionRule.ProcessedAction OnExpressionProcessed { get; }

		// Token: 0x020006B6 RID: 1718
		internal enum ProcessedAction
		{
			// Token: 0x04002048 RID: 8264
			Continue,
			// Token: 0x04002049 RID: 8265
			Reset,
			// Token: 0x0400204A RID: 8266
			Stop
		}
	}
}
