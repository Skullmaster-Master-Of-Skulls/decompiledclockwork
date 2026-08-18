using System;

namespace Oracle.SqlAndPlsqlParser.RuleProcessors.ODPCommands
{
	// Token: 0x02000304 RID: 772
	internal static class OracleMbEarleyAliasedDmlTableExpressionClauseRuleMultiProcessors
	{
		// Token: 0x06001B86 RID: 7046 RVA: 0x0010D944 File Offset: 0x0010BB44
		public static object Process_AliasedDmlTableExpressionClause_AliasedDmlTableExpressionClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x04001D59 RID: 7513
		public static OracleMbEarleyRuleMultiProcessorAddItem[] s_vRuleProcessorItems = new OracleMbEarleyRuleMultiProcessorAddItem[]
		{
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "aliased_dml_table_expression_clause",
				m_vRHSSymbols = new string[]
				{
					"aliased_dml_table_expression_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyAliasedDmlTableExpressionClauseRuleMultiProcessors.Process_AliasedDmlTableExpressionClause_AliasedDmlTableExpressionClause_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			}
		};
	}
}
