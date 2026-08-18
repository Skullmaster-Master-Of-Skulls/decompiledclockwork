using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Data.Entity.Core.Common.CommandTrees.Internal
{
	// Token: 0x0200012F RID: 303
	internal class PatternMatchRuleProcessor : DbExpressionRuleProcessingVisitor
	{
		// Token: 0x06000A26 RID: 2598 RVA: 0x000340A2 File Offset: 0x000322A2
		private PatternMatchRuleProcessor(ReadOnlyCollection<PatternMatchRule> rules)
		{
			this.ruleSet = rules;
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x000340B1 File Offset: 0x000322B1
		private DbExpression Process(DbExpression expression)
		{
			expression = this.VisitExpression(expression);
			return expression;
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x000340BD File Offset: 0x000322BD
		protected override IEnumerable<DbExpressionRule> GetRules()
		{
			return this.ruleSet;
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x000340C5 File Offset: 0x000322C5
		internal static Func<DbExpression, DbExpression> Create(params PatternMatchRule[] rules)
		{
			return new Func<DbExpression, DbExpression>(new PatternMatchRuleProcessor(new ReadOnlyCollection<PatternMatchRule>(rules)).Process);
		}

		// Token: 0x040002A8 RID: 680
		private readonly ReadOnlyCollection<PatternMatchRule> ruleSet;
	}
}
