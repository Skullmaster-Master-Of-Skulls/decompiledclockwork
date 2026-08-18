using System;
using Oracle.SqlAndPlsqlParser.LocalParsing;

namespace Oracle.SqlAndPlsqlParser.RuleProcessors.ODPCommands
{
	// Token: 0x02000309 RID: 777
	internal static class OracleMbEarleyCreateRuleMultiProcessors
	{
		// Token: 0x06001BB8 RID: 7096 RVA: 0x0010F82C File Offset: 0x0010DA2C
		public static object Process_Create_CreateDatabaseLink_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001BB9 RID: 7097 RVA: 0x0010F854 File Offset: 0x0010DA54
		public static object Process_Create_CreateIndex_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001BBA RID: 7098 RVA: 0x0010F87C File Offset: 0x0010DA7C
		public static object Process_Create_CreatePlsql_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			((OracleLpParserContext)ctx).CurrentStatementClause = OracleLpStatementClauseType.CreatePlsql;
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001BBB RID: 7099 RVA: 0x0010F8B0 File Offset: 0x0010DAB0
		public static object Process_Create_CreateUser_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001BBC RID: 7100 RVA: 0x0010F8D8 File Offset: 0x0010DAD8
		public static object Process_Create_CreateSchema_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001BBD RID: 7101 RVA: 0x0010F900 File Offset: 0x0010DB00
		public static object Process_Create_CreateView_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001BBE RID: 7102 RVA: 0x0010F928 File Offset: 0x0010DB28
		public static object Process_Create_CreateTable_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			((OracleLpParserContext)ctx).CurrentStatementClause = OracleLpStatementClauseType.CreateTable;
			((OracleLpParserContext)ctx).HandleBindVariables = true;
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001BBF RID: 7103 RVA: 0x0010F968 File Offset: 0x0010DB68
		public static object Process_Create_CreateSequence_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001BC0 RID: 7104 RVA: 0x0010F990 File Offset: 0x0010DB90
		public static object Process_Create_CreateRole_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001BC1 RID: 7105 RVA: 0x0010F9B8 File Offset: 0x0010DBB8
		public static object Process_Create_CreateSynonym_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x04001D5E RID: 7518
		public static OracleMbEarleyRuleMultiProcessorAddItem[] s_vRuleProcessorItems = new OracleMbEarleyRuleMultiProcessorAddItem[]
		{
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "create",
				m_vRHSSymbols = new string[]
				{
					"create_database_link"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyCreateRuleMultiProcessors.Process_Create_CreateDatabaseLink_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "create",
				m_vRHSSymbols = new string[]
				{
					"create_index"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyCreateRuleMultiProcessors.Process_Create_CreateIndex_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "create",
				m_vRHSSymbols = new string[]
				{
					"create_plsql"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyCreateRuleMultiProcessors.Process_Create_CreatePlsql_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "create",
				m_vRHSSymbols = new string[]
				{
					"create_role"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyCreateRuleMultiProcessors.Process_Create_CreateRole_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "create",
				m_vRHSSymbols = new string[]
				{
					"create_schema"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyCreateRuleMultiProcessors.Process_Create_CreateSchema_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "create",
				m_vRHSSymbols = new string[]
				{
					"create_sequence"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyCreateRuleMultiProcessors.Process_Create_CreateSequence_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "create",
				m_vRHSSymbols = new string[]
				{
					"create_synonym"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyCreateRuleMultiProcessors.Process_Create_CreateSynonym_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "create",
				m_vRHSSymbols = new string[]
				{
					"create_table"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyCreateRuleMultiProcessors.Process_Create_CreateTable_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "create",
				m_vRHSSymbols = new string[]
				{
					"create_user"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyCreateRuleMultiProcessors.Process_Create_CreateUser_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "create",
				m_vRHSSymbols = new string[]
				{
					"create_view"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyCreateRuleMultiProcessors.Process_Create_CreateView_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			}
		};
	}
}
