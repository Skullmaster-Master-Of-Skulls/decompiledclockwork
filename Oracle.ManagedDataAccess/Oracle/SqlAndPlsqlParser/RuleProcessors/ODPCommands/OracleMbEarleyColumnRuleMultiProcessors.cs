using System;
using System.Collections.Generic;
using Oracle.SqlAndPlsqlParser.LocalParsing;

namespace Oracle.SqlAndPlsqlParser.RuleProcessors.ODPCommands
{
	// Token: 0x02000307 RID: 775
	internal static class OracleMbEarleyColumnRuleMultiProcessors
	{
		// Token: 0x06001B91 RID: 7057 RVA: 0x0010E250 File Offset: 0x0010C450
		public static object Process_Column_Identifier_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpColumn oracleLpColumn = new OracleLpColumn(((OracleLpParserContext)ctx).CurrentStatement);
			ctx.SetActiveObject(10, oracleLpColumn);
			oracleLpColumn.Name = new OracleLpName(ctx.Tokens[ctx.CurrentParseNode.From].m_vContent);
			return oracleLpColumn;
		}

		// Token: 0x06001B92 RID: 7058 RVA: 0x0010E2A0 File Offset: 0x0010C4A0
		public static object Process_Column_Column_Identifier_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpColumn oracleLpColumn = new OracleLpColumn(((OracleLpParserContext)ctx).CurrentStatement);
			ctx.SetActiveObject(10, oracleLpColumn);
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx);
			oracleLpColumn.Name = new OracleLpName(ctx.Tokens[list[1].From].m_vContent);
			return oracleLpColumn;
		}

		// Token: 0x06001B93 RID: 7059 RVA: 0x0010E30C File Offset: 0x0010C50C
		public static object Process_Column_Identifier_DOT_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpColumn oracleLpColumn = (OracleLpColumn)ctx.GetActiveObject(10);
			oracleLpColumn.ParentObjectName = new OracleLpName(ctx.Tokens[list[0].From].m_vContent);
			return null;
		}

		// Token: 0x06001B94 RID: 7060 RVA: 0x0010E35C File Offset: 0x0010C55C
		public static object Process_Column_Identifier_DOT_Identifier_DOT_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpColumn oracleLpColumn = (OracleLpColumn)ctx.GetActiveObject(10);
			oracleLpColumn.ParentObjectName = new OracleLpName(ctx.Tokens[list[2].From].m_vContent);
			oracleLpColumn.SchemaName = new OracleLpName(ctx.Tokens[list[0].From].m_vContent);
			return null;
		}

		// Token: 0x04001D5C RID: 7516
		public static OracleMbEarleyRuleMultiProcessorAddItem[] s_vRuleProcessorItems = new OracleMbEarleyRuleMultiProcessorAddItem[]
		{
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "column",
				m_vRHSSymbols = new string[]
				{
					"identifier"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyColumnRuleMultiProcessors.Process_Column_Identifier_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "column",
				m_vRHSSymbols = new string[]
				{
					"column",
					"identifier"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyColumnRuleMultiProcessors.Process_Column_Column_Identifier_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "column",
				m_vRHSSymbols = new string[]
				{
					"identifier",
					"'.'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyColumnRuleMultiProcessors.Process_Column_Identifier_DOT_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "column",
				m_vRHSSymbols = new string[]
				{
					"identifier",
					"'.'",
					"identifier",
					"'.'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyColumnRuleMultiProcessors.Process_Column_Identifier_DOT_Identifier_DOT_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			}
		};
	}
}
