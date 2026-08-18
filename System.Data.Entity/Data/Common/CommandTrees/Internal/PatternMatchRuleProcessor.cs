using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Data.Common.CommandTrees.Internal
{
	// Token: 0x02000431 RID: 1073
	internal class PatternMatchRuleProcessor : DbExpressionRuleProcessingVisitor
	{
		// Token: 0x0600398A RID: 14730 RVA: 0x000DAB56 File Offset: 0x000D8D56
		private PatternMatchRuleProcessor(ReadOnlyCollection<PatternMatchRule> rules)
		{
			this.ruleSet = rules;
		}

		// Token: 0x0600398B RID: 14731 RVA: 0x000DAB65 File Offset: 0x000D8D65
		private DbExpression Process(DbExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(expression, "expression");
			expression = this.VisitExpression(expression);
			return expression;
		}

		// Token: 0x0600398C RID: 14732 RVA: 0x000DAB7D File Offset: 0x000D8D7D
		protected override IEnumerable<DbExpressionRule> GetRules()
		{
			return this.ruleSet;
		}

		// Token: 0x0600398D RID: 14733 RVA: 0x000DAB85 File Offset: 0x000D8D85
		internal static Func<DbExpression, DbExpression> Create(params PatternMatchRule[] rules)
		{
			EntityUtil.CheckArgumentNull<PatternMatchRule[]>(rules, "rules");
			return new Func<DbExpression, DbExpression>(new PatternMatchRuleProcessor(new ReadOnlyCollection<PatternMatchRule>(rules)).Process);
		}

		// Token: 0x0400185F RID: 6239
		private readonly ReadOnlyCollection<PatternMatchRule> ruleSet;
	}
}
