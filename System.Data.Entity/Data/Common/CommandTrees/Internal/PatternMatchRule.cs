using System;

namespace System.Data.Common.CommandTrees.Internal
{
	// Token: 0x02000430 RID: 1072
	internal class PatternMatchRule : DbExpressionRule
	{
		// Token: 0x06003984 RID: 14724 RVA: 0x000DAAE2 File Offset: 0x000D8CE2
		private PatternMatchRule(Func<DbExpression, bool> matchFunc, Func<DbExpression, DbExpression> processor, DbExpressionRule.ProcessedAction onProcessed)
		{
			this.isMatch = matchFunc;
			this.process = processor;
			this.processed = onProcessed;
		}

		// Token: 0x06003985 RID: 14725 RVA: 0x000DAAFF File Offset: 0x000D8CFF
		internal override bool ShouldProcess(DbExpression expression)
		{
			return this.isMatch(expression);
		}

		// Token: 0x06003986 RID: 14726 RVA: 0x000DAB0D File Offset: 0x000D8D0D
		internal override bool TryProcess(DbExpression expression, out DbExpression result)
		{
			result = this.process(expression);
			return result != null;
		}

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x06003987 RID: 14727 RVA: 0x000DAB22 File Offset: 0x000D8D22
		internal override DbExpressionRule.ProcessedAction OnExpressionProcessed
		{
			get
			{
				return this.processed;
			}
		}

		// Token: 0x06003988 RID: 14728 RVA: 0x000DAB2A File Offset: 0x000D8D2A
		internal static PatternMatchRule Create(Func<DbExpression, bool> matchFunc, Func<DbExpression, DbExpression> processor)
		{
			return PatternMatchRule.Create(matchFunc, processor, DbExpressionRule.ProcessedAction.Reset);
		}

		// Token: 0x06003989 RID: 14729 RVA: 0x000DAB34 File Offset: 0x000D8D34
		internal static PatternMatchRule Create(Func<DbExpression, bool> matchFunc, Func<DbExpression, DbExpression> processor, DbExpressionRule.ProcessedAction onProcessed)
		{
			EntityUtil.CheckArgumentNull<Func<DbExpression, bool>>(matchFunc, "matchFunc");
			EntityUtil.CheckArgumentNull<Func<DbExpression, DbExpression>>(processor, "processor");
			return new PatternMatchRule(matchFunc, processor, onProcessed);
		}

		// Token: 0x0400185C RID: 6236
		private readonly Func<DbExpression, bool> isMatch;

		// Token: 0x0400185D RID: 6237
		private readonly Func<DbExpression, DbExpression> process;

		// Token: 0x0400185E RID: 6238
		private readonly DbExpressionRule.ProcessedAction processed;
	}
}
