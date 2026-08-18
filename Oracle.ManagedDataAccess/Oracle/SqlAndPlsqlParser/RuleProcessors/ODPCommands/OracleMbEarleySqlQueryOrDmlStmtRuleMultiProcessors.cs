using System;
using System.Collections.Generic;
using Oracle.SqlAndPlsqlParser.LocalParsing;
using OracleInternal.Common;

namespace Oracle.SqlAndPlsqlParser.RuleProcessors.ODPCommands
{
	// Token: 0x02000312 RID: 786
	internal static class OracleMbEarleySqlQueryOrDmlStmtRuleMultiProcessors
	{
		// Token: 0x06001CD3 RID: 7379 RVA: 0x0011BCE4 File Offset: 0x00119EE4
		public static object Process_SqlQueryOrDmlStmt_Delete_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpParserContext oracleLpParserContext = (OracleLpParserContext)ctx;
			OracleLpStatement oracleLpStatement = new OracleLpDeleteStatement(oracleLpParserContext.CurrentStatementText, (IOracleMetadata)oracleLpParserContext.GetActiveObject(0));
			oracleLpParserContext.CurrentStatement = oracleLpStatement;
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			return oracleLpStatement;
		}

		// Token: 0x06001CD4 RID: 7380 RVA: 0x0011BD30 File Offset: 0x00119F30
		public static object Process_SqlQueryOrDmlStmt_Insert_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpParserContext oracleLpParserContext = (OracleLpParserContext)ctx;
			OracleLpStatement oracleLpStatement = new OracleLpInsertStatement(oracleLpParserContext.CurrentStatementText, (IOracleMetadata)oracleLpParserContext.GetActiveObject(0));
			oracleLpParserContext.CurrentStatement = oracleLpStatement;
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			return oracleLpStatement;
		}

		// Token: 0x06001CD5 RID: 7381 RVA: 0x0011BD7C File Offset: 0x00119F7C
		public static object Process_SqlQueryOrDmlStmt_Merge_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpParserContext oracleLpParserContext = (OracleLpParserContext)ctx;
			OracleLpStatement oracleLpStatement = new OracleLpMergeStatement(oracleLpParserContext.CurrentStatementText, (IOracleMetadata)oracleLpParserContext.GetActiveObject(0));
			oracleLpParserContext.CurrentStatement = oracleLpStatement;
			oracleLpParserContext.CurrentStatementClause = OracleLpStatementClauseType.Subquery;
			oracleLpParserContext.HandleBindVariables = true;
			return oracleLpStatement;
		}

		// Token: 0x06001CD6 RID: 7382 RVA: 0x0011BDC0 File Offset: 0x00119FC0
		public static object Process_SqlQueryOrDmlStmt_Select_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpParserContext oracleLpParserContext = (OracleLpParserContext)ctx;
			OracleLpStatement oracleLpStatement = new OracleLpSelectStatement(oracleLpParserContext.CurrentStatementText, (IOracleMetadata)oracleLpParserContext.GetActiveObject(0));
			oracleLpParserContext.CurrentStatement = oracleLpStatement;
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("select");
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return oracleLpStatement;
		}

		// Token: 0x06001CD7 RID: 7383 RVA: 0x0011BE2C File Offset: 0x0011A02C
		public static object Process_SqlQueryOrDmlStmt_Update_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpParserContext oracleLpParserContext = (OracleLpParserContext)ctx;
			OracleLpStatement oracleLpStatement = new OracleLpUpdateStatement(oracleLpParserContext.CurrentStatementText, (IOracleMetadata)oracleLpParserContext.GetActiveObject(0));
			oracleLpParserContext.CurrentStatement = oracleLpStatement;
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			return oracleLpStatement;
		}

		// Token: 0x06001CD8 RID: 7384 RVA: 0x0011BE78 File Offset: 0x0011A078
		public static object Process_Delete_AliasedTableExpressionClause_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("aliased_dml_table_expression_clause");
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[ruleMatchPosition], 0, -1, ctx);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001CD9 RID: 7385 RVA: 0x0011BEC4 File Offset: 0x0011A0C4
		public static object Process_Delete_WhereClause_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("where_clause");
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[ruleMatchPosition], 0, -1, ctx);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001CDA RID: 7386 RVA: 0x0011BF10 File Offset: 0x0011A110
		public static object Process_Delete_ReturningClause_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpStatement currentStatement = ((OracleLpParserContext)ctx).CurrentStatement;
			currentStatement.HasReturningClause = true;
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("returning_clause");
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[ruleMatchPosition], 0, -1, ctx);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001CDB RID: 7387 RVA: 0x0011BF70 File Offset: 0x0011A170
		public static object Process_Insert_Insert_EndsWithRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode.Children()[ruleMatchPosition], 0, -1, ctx);
		}

		// Token: 0x06001CDC RID: 7388 RVA: 0x0011BF9C File Offset: 0x0011A19C
		public static object Process_Insert_SingleTableInsert_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001CDD RID: 7389 RVA: 0x0011BFC4 File Offset: 0x0011A1C4
		public static object Process_Insert_MultiTableInsert_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001CDE RID: 7390 RVA: 0x0011BFEC File Offset: 0x0011A1EC
		public static object Process_SingleTableInsert_InsertIntoClause_SingleTableInsert_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpParserContext oracleLpParserContext = (OracleLpParserContext)ctx;
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("aliased_dml_table_expression_clause");
			result = OracleMbEarleyRuleMultiProcessor.TraverseAndProcessNodeSubtreeRules(list[0], ctx, ctx.RuleProcessorTable.RuleProcessors);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001CDF RID: 7391 RVA: 0x0011C058 File Offset: 0x0011A258
		public static object Process_SingleTableInsert_InsertIntoClause_SingleTableInsert_ErrorLoggingClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpParserContext oracleLpParserContext = (OracleLpParserContext)ctx;
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("aliased_dml_table_expression_clause");
			result = OracleMbEarleyRuleMultiProcessor.TraverseAndProcessNodeSubtreeRules(list[0], ctx, ctx.RuleProcessorTable.RuleProcessors);
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("error_logging_clause");
			result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[2], 0, -1, ctx);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001CE0 RID: 7392 RVA: 0x0011C0E4 File Offset: 0x0011A2E4
		public static object Process_SingleTableInsert_Subquery_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			object result = null;
			OracleLpParserContext oracleLpParserContext = (OracleLpParserContext)ctx;
			OracleLpStatement currentStatement = oracleLpParserContext.CurrentStatement;
			oracleLpParserContext.CurrentStatementClause = OracleLpStatementClauseType.Subquery;
			oracleLpParserContext.HandleBindVariables = true;
			return result;
		}

		// Token: 0x06001CE1 RID: 7393 RVA: 0x0011C114 File Offset: 0x0011A314
		public static object Process_SingleTableInsert_ValuesClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("values_clause");
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001CE2 RID: 7394 RVA: 0x0011C168 File Offset: 0x0011A368
		public static object Process_SingleTableInsert_ValuesClause_ReturningClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("values_clause");
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx);
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("returning_clause");
			result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001CE3 RID: 7395 RVA: 0x0011C1D4 File Offset: 0x0011A3D4
		public static object Process_MultiTableInsert_MultiTableInsert_Subquery_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpParserContext oracleLpParserContext = (OracleLpParserContext)ctx;
			OracleLpStatement currentStatement = oracleLpParserContext.CurrentStatement;
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx);
			oracleLpParserContext.CurrentStatementClause = OracleLpStatementClauseType.Subquery;
			oracleLpParserContext.HandleBindVariables = true;
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
			return result;
		}

		// Token: 0x06001CE4 RID: 7396 RVA: 0x0011C230 File Offset: 0x0011A430
		public static object Process_MultiTableInsert_MultiTableInsert_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001CE5 RID: 7397 RVA: 0x0011C258 File Offset: 0x0011A458
		public static object Process_MultiTableInsert_MultiTableInsert_MultiTableInsert_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpParserContext oracleLpParserContext = (OracleLpParserContext)ctx;
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			object obj = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx);
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
		}

		// Token: 0x06001CE6 RID: 7398 RVA: 0x0011C29C File Offset: 0x0011A49C
		public static object Process_MultiTableInsert_InsertIntoClause_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("aliased_dml_table_expression_clause");
			object result;
			if (ctx.CurrentRule.IsUnary)
			{
				result = OracleMbEarleyRuleMultiProcessor.TraverseAndProcessNodeSubtreeRules(ctx.CurrentParseNode, ctx, ctx.RuleProcessorTable.RuleProcessors);
			}
			else
			{
				result = OracleMbEarleyRuleMultiProcessor.TraverseAndProcessNodeSubtreeRules(ctx.CurrentParseNode.Children()[ruleMatchPosition], ctx, ctx.RuleProcessorTable.RuleProcessors);
			}
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001CE7 RID: 7399 RVA: 0x0011C318 File Offset: 0x0011A518
		public static object Process_MultiTableInsert_ValuesClause_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("values_clause");
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[ruleMatchPosition], 0, -1, ctx);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001CE8 RID: 7400 RVA: 0x0011C364 File Offset: 0x0011A564
		public static object Process_MultiTableInsert_ErrorLoggingClause_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("error_logging_clause");
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[ruleMatchPosition], 0, -1, ctx);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001CE9 RID: 7401 RVA: 0x0011C3B0 File Offset: 0x0011A5B0
		public static object Process_Update_AliasedTableExpressionClause_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("aliased_dml_table_expression_clause");
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[ruleMatchPosition], 0, -1, ctx);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001CEA RID: 7402 RVA: 0x0011C3FC File Offset: 0x0011A5FC
		public static object Process_Update_WhereClause_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("where_clause");
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[ruleMatchPosition], 0, -1, ctx);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001CEB RID: 7403 RVA: 0x0011C448 File Offset: 0x0011A648
		public static object Process_Update_UpdateSetClause_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[ruleMatchPosition], 0, -1, ctx);
		}

		// Token: 0x06001CEC RID: 7404 RVA: 0x0011C474 File Offset: 0x0011A674
		public static object Process_Update_ReturningClause_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("returning_clause");
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[ruleMatchPosition], 0, -1, ctx);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001CED RID: 7405 RVA: 0x0011C4C0 File Offset: 0x0011A6C0
		public static object Process_Update_ErrorLoggingClause_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("error_logging_clause");
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[ruleMatchPosition], 0, -1, ctx);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001CEE RID: 7406 RVA: 0x0011C50C File Offset: 0x0011A70C
		public static object Process_Update_SET_UpdateSetClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpParserContext oracleLpParserContext = (OracleLpParserContext)ctx;
			OracleLpStatement currentStatement = oracleLpParserContext.CurrentStatement;
			oracleLpParserContext.CurrentStatementClause = OracleLpStatementClauseType.UpdateSetClause;
			oracleLpParserContext.HandleBindVariables = true;
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
		}

		// Token: 0x04001D67 RID: 7527
		public static OracleMbEarleyRuleMultiProcessorAddItem[] s_vRuleProcessorItems = new OracleMbEarleyRuleMultiProcessorAddItem[]
		{
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "sql_query_or_dml_stmt",
				m_vRHSSymbols = new string[]
				{
					"delete"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySqlQueryOrDmlStmtRuleMultiProcessors.Process_SqlQueryOrDmlStmt_Delete_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "sql_query_or_dml_stmt",
				m_vRHSSymbols = new string[]
				{
					"insert"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySqlQueryOrDmlStmtRuleMultiProcessors.Process_SqlQueryOrDmlStmt_Insert_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "sql_query_or_dml_stmt",
				m_vRHSSymbols = new string[]
				{
					"merge"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySqlQueryOrDmlStmtRuleMultiProcessors.Process_SqlQueryOrDmlStmt_Merge_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "sql_query_or_dml_stmt",
				m_vRHSSymbols = new string[]
				{
					"select"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySqlQueryOrDmlStmtRuleMultiProcessors.Process_SqlQueryOrDmlStmt_Select_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "sql_query_or_dml_stmt",
				m_vRHSSymbols = new string[]
				{
					"update"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySqlQueryOrDmlStmtRuleMultiProcessors.Process_SqlQueryOrDmlStmt_Update_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "delete",
				m_vRHSSymbols = new string[]
				{
					"aliased_dml_table_expression_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySqlQueryOrDmlStmtRuleMultiProcessors.Process_Delete_AliasedTableExpressionClause_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "delete",
				m_vRHSSymbols = new string[]
				{
					"where_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySqlQueryOrDmlStmtRuleMultiProcessors.Process_Delete_WhereClause_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "delete",
				m_vRHSSymbols = new string[]
				{
					"returning_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySqlQueryOrDmlStmtRuleMultiProcessors.Process_Delete_ReturningClause_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "insert",
				m_vRHSSymbols = new string[]
				{
					"insert"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySqlQueryOrDmlStmtRuleMultiProcessors.Process_Insert_Insert_EndsWithRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllRulesEndingWith
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "insert",
				m_vRHSSymbols = new string[]
				{
					"single_table_insert"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySqlQueryOrDmlStmtRuleMultiProcessors.Process_Insert_SingleTableInsert_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "insert",
				m_vRHSSymbols = new string[]
				{
					"multi_table_insert"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySqlQueryOrDmlStmtRuleMultiProcessors.Process_Insert_MultiTableInsert_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "single_table_insert",
				m_vRHSSymbols = new string[]
				{
					"insert_into_clause",
					"single_table_insert"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySqlQueryOrDmlStmtRuleMultiProcessors.Process_SingleTableInsert_InsertIntoClause_SingleTableInsert_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "single_table_insert",
				m_vRHSSymbols = new string[]
				{
					"insert_into_clause",
					"single_table_insert",
					"error_logging_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySqlQueryOrDmlStmtRuleMultiProcessors.Process_SingleTableInsert_InsertIntoClause_SingleTableInsert_ErrorLoggingClause_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "single_table_insert",
				m_vRHSSymbols = new string[]
				{
					"subquery"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySqlQueryOrDmlStmtRuleMultiProcessors.Process_SingleTableInsert_Subquery_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "single_table_insert",
				m_vRHSSymbols = new string[]
				{
					"values_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySqlQueryOrDmlStmtRuleMultiProcessors.Process_SingleTableInsert_ValuesClause_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "single_table_insert",
				m_vRHSSymbols = new string[]
				{
					"values_clause",
					"returning_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySqlQueryOrDmlStmtRuleMultiProcessors.Process_SingleTableInsert_ValuesClause_ReturningClause_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "multi_table_insert",
				m_vRHSSymbols = new string[]
				{
					"multi_table_insert",
					"subquery"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySqlQueryOrDmlStmtRuleMultiProcessors.Process_MultiTableInsert_MultiTableInsert_Subquery_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "multi_table_insert",
				m_vRHSSymbols = new string[]
				{
					"multi_table_insert"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySqlQueryOrDmlStmtRuleMultiProcessors.Process_MultiTableInsert_MultiTableInsert_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "multi_table_insert",
				m_vRHSSymbols = new string[]
				{
					"multi_table_insert",
					"multi_table_insert"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySqlQueryOrDmlStmtRuleMultiProcessors.Process_MultiTableInsert_MultiTableInsert_MultiTableInsert_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "multi_table_insert",
				m_vRHSSymbols = new string[]
				{
					"insert_into_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySqlQueryOrDmlStmtRuleMultiProcessors.Process_MultiTableInsert_InsertIntoClause_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "multi_table_insert",
				m_vRHSSymbols = new string[]
				{
					"values_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySqlQueryOrDmlStmtRuleMultiProcessors.Process_MultiTableInsert_ValuesClause_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "multi_table_insert",
				m_vRHSSymbols = new string[]
				{
					"error_logging_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySqlQueryOrDmlStmtRuleMultiProcessors.Process_MultiTableInsert_ErrorLoggingClause_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "update",
				m_vRHSSymbols = new string[]
				{
					"aliased_dml_table_expression_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySqlQueryOrDmlStmtRuleMultiProcessors.Process_Update_AliasedTableExpressionClause_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "update",
				m_vRHSSymbols = new string[]
				{
					"update_set_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySqlQueryOrDmlStmtRuleMultiProcessors.Process_Update_UpdateSetClause_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "update",
				m_vRHSSymbols = new string[]
				{
					"where_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySqlQueryOrDmlStmtRuleMultiProcessors.Process_Update_WhereClause_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "update",
				m_vRHSSymbols = new string[]
				{
					"returning_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySqlQueryOrDmlStmtRuleMultiProcessors.Process_Update_ReturningClause_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "update",
				m_vRHSSymbols = new string[]
				{
					"error_logging_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySqlQueryOrDmlStmtRuleMultiProcessors.Process_Update_ErrorLoggingClause_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "update_set_clause",
				m_vRHSSymbols = new string[]
				{
					"'SET'",
					"update_set_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySqlQueryOrDmlStmtRuleMultiProcessors.Process_Update_SET_UpdateSetClause_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			}
		};
	}
}
