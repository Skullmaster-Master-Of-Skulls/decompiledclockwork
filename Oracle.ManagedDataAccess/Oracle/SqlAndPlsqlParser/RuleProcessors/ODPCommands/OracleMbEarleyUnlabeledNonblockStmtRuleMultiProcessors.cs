using System;
using System.Collections.Generic;

namespace Oracle.SqlAndPlsqlParser.RuleProcessors.ODPCommands
{
	// Token: 0x02000317 RID: 791
	internal static class OracleMbEarleyUnlabeledNonblockStmtRuleMultiProcessors
	{
		// Token: 0x06001D18 RID: 7448 RVA: 0x0011E600 File Offset: 0x0011C800
		public static object Process_UnlabeledNonblockStmt_SqlStmt_SEMICOLON_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("sql_stmt");
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001D19 RID: 7449 RVA: 0x0011E64C File Offset: 0x0011C84C
		public static object Process_UnlabeledNonblockStmt_StaticDdlStmt_SEMICOLON_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("static_ddl_stmt");
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001D1A RID: 7450 RVA: 0x0011E698 File Offset: 0x0011C898
		public static object Process_UnlabeledNonblockStmt_StaticDmlStmt_SEMICOLON_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("static_dml_stmt");
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001D1B RID: 7451 RVA: 0x0011E6E4 File Offset: 0x0011C8E4
		public static object Process_UnlabeledNonblockStmt_ExecuteImmediateStatement_SEMICOLON_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("exec_immediate_statement");
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001D1C RID: 7452 RVA: 0x0011E730 File Offset: 0x0011C930
		public static object Process_UnlabeledNonblockStmt_SimStmt_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("sim_stmt");
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001D1D RID: 7453 RVA: 0x0011E778 File Offset: 0x0011C978
		public static object Process_UnlabeledNonblockStmt_NonblockCompoundStmt_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("nonblock_compound_stmt");
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x04001D6C RID: 7532
		public static OracleMbEarleyRuleMultiProcessorAddItem[] s_vRuleProcessorItems = new OracleMbEarleyRuleMultiProcessorAddItem[]
		{
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "unlabeled_nonblock_stmt",
				m_vRHSSymbols = new string[]
				{
					"sql_stmt",
					"';'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyUnlabeledNonblockStmtRuleMultiProcessors.Process_UnlabeledNonblockStmt_SqlStmt_SEMICOLON_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "unlabeled_nonblock_stmt",
				m_vRHSSymbols = new string[]
				{
					"static_ddl_stmt",
					"';'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyUnlabeledNonblockStmtRuleMultiProcessors.Process_UnlabeledNonblockStmt_StaticDdlStmt_SEMICOLON_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "unlabeled_nonblock_stmt",
				m_vRHSSymbols = new string[]
				{
					"static_dml_stmt",
					"';'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyUnlabeledNonblockStmtRuleMultiProcessors.Process_UnlabeledNonblockStmt_StaticDmlStmt_SEMICOLON_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "unlabeled_nonblock_stmt",
				m_vRHSSymbols = new string[]
				{
					"exec_immediate_statement",
					"';'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyUnlabeledNonblockStmtRuleMultiProcessors.Process_UnlabeledNonblockStmt_ExecuteImmediateStatement_SEMICOLON_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "unlabeled_nonblock_stmt",
				m_vRHSSymbols = new string[]
				{
					"sim_stmt"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyUnlabeledNonblockStmtRuleMultiProcessors.Process_UnlabeledNonblockStmt_SimStmt_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "unlabeled_nonblock_stmt",
				m_vRHSSymbols = new string[]
				{
					"nonblock_compound_stmt"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyUnlabeledNonblockStmtRuleMultiProcessors.Process_UnlabeledNonblockStmt_NonblockCompoundStmt_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			}
		};
	}
}
