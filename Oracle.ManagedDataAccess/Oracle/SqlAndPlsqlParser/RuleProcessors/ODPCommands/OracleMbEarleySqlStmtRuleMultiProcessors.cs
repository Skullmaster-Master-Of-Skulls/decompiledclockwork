using System;
using System.Collections.Generic;
using Oracle.SqlAndPlsqlParser.LocalParsing;

namespace Oracle.SqlAndPlsqlParser.RuleProcessors.ODPCommands
{
	// Token: 0x02000313 RID: 787
	internal static class OracleMbEarleySqlStmtRuleMultiProcessors
	{
		// Token: 0x06001CF0 RID: 7408 RVA: 0x0011C6D0 File Offset: 0x0011A8D0
		public static object Process_SqlStmt_SqlQueryOrDmlStmt_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<LexerToken> tokens = ctx.Tokens;
			ParseNode currentParseNode = ctx.CurrentParseNode;
			int vBegin = tokens[currentParseNode.From].m_vBegin;
			int vEnd = tokens[currentParseNode.To].m_vEnd;
			((OracleLpParserContext)ctx).CurrentStatementText = new OracleLpTextFragment(ctx.Script, vBegin, vEnd - vBegin);
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("sql_query_or_dml_stmt");
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(currentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001CF1 RID: 7409 RVA: 0x0011C760 File Offset: 0x0011A960
		public static object Process_SqlStmt_FetchStmt_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("fetch_statement");
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001CF2 RID: 7410 RVA: 0x0011C7A8 File Offset: 0x0011A9A8
		public static object Process_SqlStmt_OpenCursorReferenceStatement_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("open_cursor_reference_statement");
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001CF3 RID: 7411 RVA: 0x0011C7F0 File Offset: 0x0011A9F0
		public static object Process_SqlStmt_OpenStatement_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("open_statement");
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x04001D68 RID: 7528
		public static OracleMbEarleyRuleMultiProcessorAddItem[] s_vRuleProcessorItems = new OracleMbEarleyRuleMultiProcessorAddItem[]
		{
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "sql_stmt",
				m_vRHSSymbols = new string[]
				{
					"sql_query_or_dml_stmt"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySqlStmtRuleMultiProcessors.Process_SqlStmt_SqlQueryOrDmlStmt_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "sql_stmt",
				m_vRHSSymbols = new string[]
				{
					"fetch_statement"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySqlStmtRuleMultiProcessors.Process_SqlStmt_FetchStmt_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "sql_stmt",
				m_vRHSSymbols = new string[]
				{
					"open_cursor_reference_statement"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySqlStmtRuleMultiProcessors.Process_SqlStmt_OpenCursorReferenceStatement_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "sql_stmt",
				m_vRHSSymbols = new string[]
				{
					"open_statement"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySqlStmtRuleMultiProcessors.Process_SqlStmt_OpenStatement_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			}
		};
	}
}
