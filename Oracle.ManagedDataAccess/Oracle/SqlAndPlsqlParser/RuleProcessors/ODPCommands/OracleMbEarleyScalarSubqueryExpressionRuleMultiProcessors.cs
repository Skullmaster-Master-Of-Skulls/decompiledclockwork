using System;
using System.Collections.Generic;
using Oracle.SqlAndPlsqlParser.LocalParsing;

namespace Oracle.SqlAndPlsqlParser.RuleProcessors.ODPCommands
{
	// Token: 0x020001FB RID: 507
	internal static class OracleMbEarleyScalarSubqueryExpressionRuleMultiProcessors
	{
		// Token: 0x0600125C RID: 4700 RVA: 0x000C5638 File Offset: 0x000C3838
		public static object Process_ScalarSubqueryExpression_LEFT_PARENTHESIS_Subquery_RIGHT_PARENTHESIS_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpScalarSubqueryExpression oracleLpScalarSubqueryExpression = new OracleLpScalarSubqueryExpression(null);
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("select");
			object obj = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
			ctx.RuleProcessorTable = ruleProcessorTable;
			oracleLpScalarSubqueryExpression.Subquery = (obj as OracleLpSubquery);
			return oracleLpScalarSubqueryExpression;
		}

		// Token: 0x04001446 RID: 5190
		public static OracleMbEarleyRuleMultiProcessorAddItem[] s_vRuleProcessorItems = new OracleMbEarleyRuleMultiProcessorAddItem[]
		{
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "scalar_subquery_expression",
				m_vRHSSymbols = new string[]
				{
					"'('",
					"subquery",
					"')'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyScalarSubqueryExpressionRuleMultiProcessors.Process_ScalarSubqueryExpression_LEFT_PARENTHESIS_Subquery_RIGHT_PARENTHESIS_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			}
		};
	}
}
