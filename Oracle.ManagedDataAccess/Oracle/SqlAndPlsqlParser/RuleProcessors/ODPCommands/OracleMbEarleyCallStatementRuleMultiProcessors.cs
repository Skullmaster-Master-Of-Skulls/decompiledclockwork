using System;
using Oracle.SqlAndPlsqlParser.LocalParsing;

namespace Oracle.SqlAndPlsqlParser.RuleProcessors.ODPCommands
{
	// Token: 0x020001F9 RID: 505
	internal static class OracleMbEarleyCallStatementRuleMultiProcessors
	{
		// Token: 0x0600124F RID: 4687 RVA: 0x000C4EDC File Offset: 0x000C30DC
		public static object Process_CallStatement_CALL_ProcedureCall_INTO_Name_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			object result = null;
			((OracleLpParserContext)ctx).CurrentStatementClause = OracleLpStatementClauseType.CallIntoStatement;
			((OracleLpParserContext)ctx).HandleBindVariables = true;
			return result;
		}

		// Token: 0x04001444 RID: 5188
		public static OracleMbEarleyRuleMultiProcessorAddItem[] s_vRuleProcessorItems = new OracleMbEarleyRuleMultiProcessorAddItem[]
		{
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "call_statement",
				m_vRHSSymbols = new string[]
				{
					"'CALL'",
					"procedure_call",
					"'INTO'",
					"name"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyCallStatementRuleMultiProcessors.Process_CallStatement_CALL_ProcedureCall_INTO_Name_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			}
		};
	}
}
