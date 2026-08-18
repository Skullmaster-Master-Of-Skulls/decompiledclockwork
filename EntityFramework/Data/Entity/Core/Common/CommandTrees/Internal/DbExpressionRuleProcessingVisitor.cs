using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Data.Entity.Core.Common.CommandTrees.Internal
{
	// Token: 0x02000125 RID: 293
	internal abstract class DbExpressionRuleProcessingVisitor : DefaultExpressionVisitor
	{
		// Token: 0x06000931 RID: 2353
		protected abstract IEnumerable<DbExpressionRule> GetRules();

		// Token: 0x06000932 RID: 2354 RVA: 0x0002F12C File Offset: 0x0002D32C
		private static Tuple<DbExpression, DbExpressionRule.ProcessedAction> ProcessRules(DbExpression expression, List<DbExpressionRule> rules)
		{
			for (int i = 0; i < rules.Count; i++)
			{
				DbExpressionRule dbExpressionRule = rules[i];
				DbExpression dbExpression;
				if (dbExpressionRule.ShouldProcess(expression) && dbExpressionRule.TryProcess(expression, out dbExpression))
				{
					if (dbExpressionRule.OnExpressionProcessed != DbExpressionRule.ProcessedAction.Continue)
					{
						return Tuple.Create<DbExpression, DbExpressionRule.ProcessedAction>(dbExpression, dbExpressionRule.OnExpressionProcessed);
					}
					expression = dbExpression;
				}
			}
			return Tuple.Create<DbExpression, DbExpressionRule.ProcessedAction>(expression, DbExpressionRule.ProcessedAction.Continue);
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0002F188 File Offset: 0x0002D388
		private DbExpression ApplyRules(DbExpression expression)
		{
			List<DbExpressionRule> rules = this.GetRules().ToList<DbExpressionRule>();
			Tuple<DbExpression, DbExpressionRule.ProcessedAction> tuple = DbExpressionRuleProcessingVisitor.ProcessRules(expression, rules);
			while (tuple.Item2 == DbExpressionRule.ProcessedAction.Reset)
			{
				rules = this.GetRules().ToList<DbExpressionRule>();
				tuple = DbExpressionRuleProcessingVisitor.ProcessRules(tuple.Item1, rules);
			}
			if (tuple.Item2 == DbExpressionRule.ProcessedAction.Stop)
			{
				this._stopped = true;
			}
			return tuple.Item1;
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x0002F1E4 File Offset: 0x0002D3E4
		protected override DbExpression VisitExpression(DbExpression expression)
		{
			DbExpression dbExpression = this.ApplyRules(expression);
			if (this._stopped)
			{
				return dbExpression;
			}
			dbExpression = base.VisitExpression(dbExpression);
			if (this._stopped)
			{
				return dbExpression;
			}
			return this.ApplyRules(dbExpression);
		}

		// Token: 0x04000293 RID: 659
		private bool _stopped;
	}
}
