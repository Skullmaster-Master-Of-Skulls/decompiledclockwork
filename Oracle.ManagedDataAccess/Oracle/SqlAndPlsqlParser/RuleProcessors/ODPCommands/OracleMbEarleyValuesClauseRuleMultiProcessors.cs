using System;
using Oracle.SqlAndPlsqlParser.LocalParsing;

namespace Oracle.SqlAndPlsqlParser.RuleProcessors.ODPCommands
{
	// Token: 0x020001FE RID: 510
	internal static class OracleMbEarleyValuesClauseRuleMultiProcessors
	{
		// Token: 0x060012EC RID: 4844 RVA: 0x000CB14C File Offset: 0x000C934C
		public static object Process_ValuesClause_VALUES_ValuesClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			object result = null;
			((OracleLpParserContext)ctx).CurrentStatementClause = OracleLpStatementClauseType.ValuesClause;
			((OracleLpParserContext)ctx).HandleBindVariables = true;
			return result;
		}

		// Token: 0x04001449 RID: 5193
		public static OracleMbEarleyRuleMultiProcessorAddItem[] s_vRuleProcessorItems = new OracleMbEarleyRuleMultiProcessorAddItem[]
		{
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "values_clause",
				m_vRHSSymbols = new string[]
				{
					"'VALUES'",
					"values_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyValuesClauseRuleMultiProcessors.Process_ValuesClause_VALUES_ValuesClause_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			}
		};
	}
}
