using System;
using Oracle.SqlAndPlsqlParser.LocalParsing;

namespace Oracle.SqlAndPlsqlParser.RuleProcessors.ODPCommands
{
	// Token: 0x0200030F RID: 783
	internal static class OracleMbEarleyReturningClauseRuleMultiProcessors
	{
		// Token: 0x06001C35 RID: 7221 RVA: 0x00114EC0 File Offset: 0x001130C0
		public static object Process_ReturningClause_ReturningClause_Expr_StartWithRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			object result = null;
			OracleLpParserContext oracleLpParserContext = (OracleLpParserContext)ctx;
			OracleLpStatement currentStatement = oracleLpParserContext.CurrentStatement;
			oracleLpParserContext.CurrentStatementClause = OracleLpStatementClauseType.ReturningClause;
			currentStatement.HasReturningClause = true;
			oracleLpParserContext.HandleBindVariables = true;
			return result;
		}

		// Token: 0x04001D64 RID: 7524
		public static OracleMbEarleyRuleMultiProcessorAddItem[] s_vRuleProcessorItems = new OracleMbEarleyRuleMultiProcessorAddItem[]
		{
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "returning_clause",
				m_vRHSSymbols = new string[]
				{
					"returning_clause",
					"expr"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyReturningClauseRuleMultiProcessors.Process_ReturningClause_ReturningClause_Expr_StartWithRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllRulesStartingWith
			}
		};
	}
}
