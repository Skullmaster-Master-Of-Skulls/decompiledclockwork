using System;
using Oracle.SqlAndPlsqlParser.LocalParsing;

namespace Oracle.SqlAndPlsqlParser.RuleProcessors.ODPCommands
{
	// Token: 0x02000315 RID: 789
	internal static class OracleMbEarleyWhereClauseRuleMultiProcessors
	{
		// Token: 0x06001CFE RID: 7422 RVA: 0x0011D1E8 File Offset: 0x0011B3E8
		public static object Process_WhereClause_WHERE_Condition_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			object result = null;
			((OracleLpParserContext)ctx).CurrentStatementClause = OracleLpStatementClauseType.WhereClause;
			((OracleLpParserContext)ctx).HandleBindVariables = true;
			return result;
		}

		// Token: 0x04001D6A RID: 7530
		public static OracleMbEarleyRuleMultiProcessorAddItem[] s_vRuleProcessorItems = new OracleMbEarleyRuleMultiProcessorAddItem[]
		{
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "where_clause",
				m_vRHSSymbols = new string[]
				{
					"'WHERE'",
					"condition"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyWhereClauseRuleMultiProcessors.Process_WhereClause_WHERE_Condition_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			}
		};
	}
}
