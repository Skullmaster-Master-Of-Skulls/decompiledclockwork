using System;
using Oracle.SqlAndPlsqlParser.LocalParsing;

namespace Oracle.SqlAndPlsqlParser.RuleProcessors.ODPCommands
{
	// Token: 0x02000306 RID: 774
	internal static class OracleMbEarleyBlockStmtRuleMultiProcessors
	{
		// Token: 0x06001B8D RID: 7053 RVA: 0x0010DFC4 File Offset: 0x0010C1C4
		public static object Process_BlockStmt_StaticDmlStmt_DECLAREDeclsOpt_StartWithRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			((OracleLpParserContext)ctx).CurrentStatementClause = OracleLpStatementClauseType.Declare;
			((OracleLpParserContext)ctx).HandleBindVariables = true;
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode.Children()[ruleMatchPosition], ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001B8E RID: 7054 RVA: 0x0010E010 File Offset: 0x0010C210
		public static object Process_BlockStmt_SeqOfStmts_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			((OracleLpParserContext)ctx).CurrentStatementClause = OracleLpStatementClauseType.SequenceOfStatements;
			((OracleLpParserContext)ctx).HandleBindVariables = true;
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode.Children()[ruleMatchPosition], ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001B8F RID: 7055 RVA: 0x0010E05C File Offset: 0x0010C25C
		public static object Process_BlockStmt_ExceptionHandlersOpt_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			((OracleLpParserContext)ctx).CurrentStatementClause = OracleLpStatementClauseType.ExceptionHandlers;
			((OracleLpParserContext)ctx).HandleBindVariables = true;
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode.Children()[ruleMatchPosition], ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x04001D5B RID: 7515
		public static OracleMbEarleyRuleMultiProcessorAddItem[] s_vRuleProcessorItems = new OracleMbEarleyRuleMultiProcessorAddItem[]
		{
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "block_stmt",
				m_vRHSSymbols = new string[]
				{
					"DECLARE_decls_opt"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyBlockStmtRuleMultiProcessors.Process_BlockStmt_StaticDmlStmt_DECLAREDeclsOpt_StartWithRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllRulesStartingWith
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "block_stmt",
				m_vRHSSymbols = new string[]
				{
					"seq_of_stmts"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyBlockStmtRuleMultiProcessors.Process_BlockStmt_SeqOfStmts_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "block_stmt",
				m_vRHSSymbols = new string[]
				{
					"exception_handlers_opt"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyBlockStmtRuleMultiProcessors.Process_BlockStmt_ExceptionHandlersOpt_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			}
		};
	}
}
