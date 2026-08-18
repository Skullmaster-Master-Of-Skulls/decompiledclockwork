using System;

namespace Oracle.SqlAndPlsqlParser.RuleProcessors.ODPCommands
{
	// Token: 0x0200030E RID: 782
	internal static class OracleMbEarleyLiteralRuleMultiProcessors
	{
		// Token: 0x06001C31 RID: 7217 RVA: 0x00114E0C File Offset: 0x0011300C
		public static object Process_Literal_DatetimeLiteral_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return ctx.CurrentParseNode.Content(ctx.Tokens);
		}

		// Token: 0x06001C32 RID: 7218 RVA: 0x00114E20 File Offset: 0x00113020
		public static object Process_Literal_NumericLiteral_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return ctx.CurrentParseNode.Content(ctx.Tokens);
		}

		// Token: 0x06001C33 RID: 7219 RVA: 0x00114E34 File Offset: 0x00113034
		public static object Process_Literal_StringLiteral_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return ctx.CurrentParseNode.Content(ctx.Tokens);
		}

		// Token: 0x04001D63 RID: 7523
		public static OracleMbEarleyRuleMultiProcessorAddItem[] s_vRuleProcessorItems = new OracleMbEarleyRuleMultiProcessorAddItem[]
		{
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "literal",
				m_vRHSSymbols = new string[]
				{
					"datetime_literal"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyLiteralRuleMultiProcessors.Process_Literal_DatetimeLiteral_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "literal",
				m_vRHSSymbols = new string[]
				{
					"numeric_literal"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyLiteralRuleMultiProcessors.Process_Literal_NumericLiteral_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "literal",
				m_vRHSSymbols = new string[]
				{
					"string_literal"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyLiteralRuleMultiProcessors.Process_Literal_StringLiteral_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			}
		};
	}
}
