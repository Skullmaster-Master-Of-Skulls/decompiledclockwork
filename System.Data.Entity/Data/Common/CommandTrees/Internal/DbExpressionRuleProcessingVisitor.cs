using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Data.Common.CommandTrees.Internal
{
	// Token: 0x0200042F RID: 1071
	internal abstract class DbExpressionRuleProcessingVisitor : DefaultExpressionVisitor
	{
		// Token: 0x06003980 RID: 14720
		protected abstract IEnumerable<DbExpressionRule> GetRules();

		// Token: 0x06003981 RID: 14721 RVA: 0x000DA9F0 File Offset: 0x000D8BF0
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

		// Token: 0x06003982 RID: 14722 RVA: 0x000DAA4C File Offset: 0x000D8C4C
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

		// Token: 0x06003983 RID: 14723 RVA: 0x000DAAA8 File Offset: 0x000D8CA8
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

		// Token: 0x0400185B RID: 6235
		private bool _stopped;
	}
}
