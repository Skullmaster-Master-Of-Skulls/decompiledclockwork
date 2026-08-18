using System;

namespace System.Data.Entity.Core.Common.CommandTrees.Internal
{
	// Token: 0x0200012E RID: 302
	internal class PatternMatchRule : DbExpressionRule
	{
		// Token: 0x06000A20 RID: 2592 RVA: 0x00034043 File Offset: 0x00032243
		private PatternMatchRule(Func<DbExpression, bool> matchFunc, Func<DbExpression, DbExpression> processor, DbExpressionRule.ProcessedAction onProcessed)
		{
			this.isMatch = matchFunc;
			this.process = processor;
			this.processed = onProcessed;
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x00034060 File Offset: 0x00032260
		internal override bool ShouldProcess(DbExpression expression)
		{
			return this.isMatch(expression);
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0003406E File Offset: 0x0003226E
		internal override bool TryProcess(DbExpression expression, out DbExpression result)
		{
			result = this.process(expression);
			return result != null;
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000A23 RID: 2595 RVA: 0x00034086 File Offset: 0x00032286
		internal override DbExpressionRule.ProcessedAction OnExpressionProcessed
		{
			get
			{
				return this.processed;
			}
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x0003408E File Offset: 0x0003228E
		internal static PatternMatchRule Create(Func<DbExpression, bool> matchFunc, Func<DbExpression, DbExpression> processor)
		{
			return PatternMatchRule.Create(matchFunc, processor, DbExpressionRule.ProcessedAction.Reset);
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x00034098 File Offset: 0x00032298
		internal static PatternMatchRule Create(Func<DbExpression, bool> matchFunc, Func<DbExpression, DbExpression> processor, DbExpressionRule.ProcessedAction onProcessed)
		{
			return new PatternMatchRule(matchFunc, processor, onProcessed);
		}

		// Token: 0x040002A5 RID: 677
		private readonly Func<DbExpression, bool> isMatch;

		// Token: 0x040002A6 RID: 678
		private readonly Func<DbExpression, DbExpression> process;

		// Token: 0x040002A7 RID: 679
		private readonly DbExpressionRule.ProcessedAction processed;
	}
}
