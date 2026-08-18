using System;
using System.Collections.Generic;
using Oracle.SqlAndPlsqlParser.RuleProcessors;

namespace Oracle.SqlAndPlsqlParser.LocalParsing.Ansi
{
	// Token: 0x020001FC RID: 508
	internal static class OracleMbEarleySelectAnsiRuleMultiProcessors
	{
		// Token: 0x0600125E RID: 4702 RVA: 0x000C8B3C File Offset: 0x000C6D3C
		public static object Process_Select_Subquery_StartWithRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpSelectStatement oracleLpSelectStatement = (OracleLpSelectStatement)ctx.GetActiveObject(3);
			ctx.SetActiveObject(4, null);
			object obj;
			if (ctx.CurrentRule.IsUnary)
			{
				obj = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			}
			else
			{
				obj = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode.Children()[ruleMatchPosition], 0, -1, ctx);
			}
			oracleLpSelectStatement.Subquery = (OracleLpSubquery)obj;
			return oracleLpSelectStatement;
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x000C8BAC File Offset: 0x000C6DAC
		public static object Process_Select_ForUpdateClause_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			object result;
			if (ctx.CurrentRule.IsUnary)
			{
				result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			}
			else
			{
				result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode.Children()[ruleMatchPosition], 0, -1, ctx);
			}
			return result;
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x000C8BFC File Offset: 0x000C6DFC
		public static object Process_Select_OrderByClause_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode.Children()[ruleMatchPosition], ctx.CurrentRuleIndex + 1, -1, ctx);
			((OracleLpParserContext)ctx).CurrentStatementClause = OracleLpStatementClauseType.OrderByClause;
			((OracleLpParserContext)ctx).HandleBindVariables = true;
			return result;
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x000C8C48 File Offset: 0x000C6E48
		public static object Process_Subquery_SimpleSetExpr_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x000C8C60 File Offset: 0x000C6E60
		public static object Process_Subquery_SimpleSetExpr_OrderByClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx);
			((OracleLpParserContext)ctx).CurrentStatementClause = OracleLpStatementClauseType.OrderByClause;
			((OracleLpParserContext)ctx).HandleBindVariables = true;
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
			return result;
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x000C8CB4 File Offset: 0x000C6EB4
		public static object Process_Subquery_SimpleSetExpr_RowLimitingClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx);
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
			return result;
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x000C8CF0 File Offset: 0x000C6EF0
		public static object Process_Subquery_SimpleSetExpr_OrderByClause_RowLimitingClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx);
			((OracleLpParserContext)ctx).CurrentStatementClause = OracleLpStatementClauseType.OrderByClause;
			((OracleLpParserContext)ctx).HandleBindVariables = true;
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[2], 0, -1, ctx);
			return result;
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x000C8D54 File Offset: 0x000C6F54
		public static object Process_SimpleSetExpr_QueryBlock_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpSubquery oracleLpSubquery = (OracleLpSubquery)ctx.GetActiveObject(4);
			OracleLpQueryBlockSubquery oracleLpQueryBlockSubquery = new OracleLpQueryBlockSubquery(null);
			ctx.SetActiveObject(4, oracleLpQueryBlockSubquery);
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			if (oracleLpSubquery != null)
			{
				ctx.SetActiveObject(4, oracleLpSubquery);
			}
			return oracleLpQueryBlockSubquery;
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x000C8DA0 File Offset: 0x000C6FA0
		public static object Process_SimpleSetExpr_SimpleSetExpr_SET_OPER_SimpleSetExpr_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpSubquery oracleLpSubquery = (OracleLpSubquery)ctx.GetActiveObject(4);
			OracleLpSetExpressionSubquery oracleLpSetExpressionSubquery = new OracleLpSetExpressionSubquery(null);
			ctx.SetActiveObject(4, oracleLpSetExpressionSubquery);
			oracleLpSetExpressionSubquery.LeftSubquery = (OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx) as OracleLpSubquery);
			oracleLpSetExpressionSubquery.SetOperator = (OracleLpSetOperator)OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
			oracleLpSetExpressionSubquery.RightSubquery = (OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[2], 0, -1, ctx) as OracleLpSubquery);
			if (oracleLpSubquery != null)
			{
				ctx.SetActiveObject(4, oracleLpSubquery);
			}
			return oracleLpSetExpressionSubquery;
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x000C8E30 File Offset: 0x000C7030
		public static object Process_SimpleSetExpr_LEFT_PARENTHESIS_SimpleSetExpr_RIGHT_PARENTHESIS_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpSubquery oracleLpSubquery = (OracleLpSubquery)ctx.GetActiveObject(4);
			OracleLpCompoundSubquery oracleLpCompoundSubquery = new OracleLpCompoundSubquery(null);
			ctx.SetActiveObject(4, oracleLpCompoundSubquery);
			oracleLpCompoundSubquery.Subquery = (OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx) as OracleLpSubquery);
			if (oracleLpSubquery != null)
			{
				ctx.SetActiveObject(4, oracleLpSubquery);
			}
			return oracleLpCompoundSubquery;
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x000C8E8C File Offset: 0x000C708C
		public static object Process_SET_OPER_INTERSECT_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleLpSetOperator.INTERSECT;
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x000C8E94 File Offset: 0x000C7094
		public static object Process_SET_OPER_MINUS_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleLpSetOperator.MINUS;
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x000C8E9C File Offset: 0x000C709C
		public static object Process_SET_OPER_UNION_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleLpSetOperator.UNION;
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x000C8EA4 File Offset: 0x000C70A4
		public static object Process_SET_OPER_UNION_ALL_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleLpSetOperator.UNION_ALL;
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x000C8EAC File Offset: 0x000C70AC
		public static object Process_RowLimitingClause_RowLimitingClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x000C8ED4 File Offset: 0x000C70D4
		public static object Process_RowLimitingClause_RowLimitingClause_RowLimitingClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			object obj = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx);
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x000C8F10 File Offset: 0x000C7110
		public static object Process_RowLimitingClause_OFFSET_Digits_RowLimitingClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			ctx.PropertiesBag.Properties["OFFSET"] = ctx.Tokens[list[1].From].m_vContent;
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[2], 0, -1, ctx);
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x000C8F70 File Offset: 0x000C7170
		public static object Process_RowLimitingClause_Unit_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			string vContent = ctx.Tokens[ctx.CurrentParseNode.From].m_vContent;
			ctx.PropertiesBag.Properties["ROW_LIMITING_CLAUSE_UNIT"] = vContent;
			return vContent;
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x000C8FB0 File Offset: 0x000C71B0
		public static object Process_RowLimitingClause_Order_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			string vContent = ctx.Tokens[ctx.CurrentParseNode.From].m_vContent;
			ctx.PropertiesBag.Properties["FETCH_ORDER"] = vContent;
			return vContent;
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x000C8FF0 File Offset: 0x000C71F0
		public static object Process_RowLimitingClause_FETCH_RowLimitingClause_RowLimitingClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			object obj = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[2], 0, -1, ctx);
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x000C902C File Offset: 0x000C722C
		public static object Process_RowLimitingClause_FETCH_RowLimitingClause_RowLimitingClause_RowLimitingClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			object obj = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
			obj = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[2], 0, -1, ctx);
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[3], 0, -1, ctx);
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x000C9078 File Offset: 0x000C7278
		public static object Process_RowLimitingClause_FETCH_RowLimitingClause_RowLimitingClause_RowLimitingClause_RowLimitingClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			object obj = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
			obj = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[2], 0, -1, ctx);
			obj = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[3], 0, -1, ctx);
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[4], 0, -1, ctx);
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x000C90D4 File Offset: 0x000C72D4
		public static object Process_RowLimitingClause_Digits_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			string vContent = ctx.Tokens[ctx.CurrentParseNode.From].m_vContent;
			ctx.PropertiesBag.Properties["FETCH_COUNT"] = vContent;
			return vContent;
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x000C9114 File Offset: 0x000C7314
		public static object Process_RowLimitingClause_Digits_PERCENT_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			ctx.CurrentParseNode.Children();
			string vContent = ctx.Tokens[ctx.CurrentParseNode.From].m_vContent;
			ctx.PropertiesBag.Properties["FETCH_PERCENT"] = vContent;
			return vContent;
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x000C9160 File Offset: 0x000C7360
		public static object Process_RowLimitingClause_GetFetchType_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			string text;
			if (ctx.CurrentRule.IsUnary)
			{
				text = ctx.Tokens[ctx.CurrentParseNode.From].m_vContent;
			}
			else
			{
				List<ParseNode> list = ctx.CurrentParseNode.Children();
				text = ctx.Tokens[list[0].From].m_vContent + " " + ctx.Tokens[list[1].From].m_vContent;
			}
			ctx.PropertiesBag.Properties["FETCH_TYPE"] = text;
			return text;
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x000C9200 File Offset: 0x000C7400
		public static object Process_WithClause_WITH_WithClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpQueryBlock oracleLpQueryBlock = (OracleLpQueryBlock)ctx.GetActiveObject(5);
			oracleLpQueryBlock.WithClause = new OracleLpWithClause(null);
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode.Children()[1], 0, -1, ctx);
			return oracleLpQueryBlock;
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x000C9244 File Offset: 0x000C7444
		public static object Process_WithClause_PlsqlDeclarations_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpQueryBlock oracleLpQueryBlock = (OracleLpQueryBlock)ctx.GetActiveObject(5);
			OracleLpWithClause withClause = oracleLpQueryBlock.WithClause;
			withClause.PlsqlDeclarations = new OracleLpPlsqlDeclarations();
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			return oracleLpQueryBlock;
		}

		// Token: 0x06001279 RID: 4729 RVA: 0x000C9288 File Offset: 0x000C7488
		public static object Process_WithClause_SubqueryFactoringClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpQueryBlock oracleLpQueryBlock = (OracleLpQueryBlock)ctx.GetActiveObject(5);
			OracleLpWithClause withClause = oracleLpQueryBlock.WithClause;
			withClause.SubqueryFactoringClause = new OracleLpSubqueryFactoringClause(null);
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			return oracleLpQueryBlock;
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x000C92CC File Offset: 0x000C74CC
		public static object Process_WithClause_PlsqlDeclarations_SubqueryFactoringClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpQueryBlock oracleLpQueryBlock = (OracleLpQueryBlock)ctx.GetActiveObject(5);
			OracleLpWithClause withClause = oracleLpQueryBlock.WithClause;
			withClause.PlsqlDeclarations = new OracleLpPlsqlDeclarations();
			withClause.SubqueryFactoringClause = new OracleLpSubqueryFactoringClause(null);
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx);
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
			return oracleLpQueryBlock;
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x000C9334 File Offset: 0x000C7534
		public static object Process_SubqueryFactoringClause_SubqueryFactoringClause_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			object result;
			if (ctx.CurrentRule.IsUnary)
			{
				result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			}
			else
			{
				result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode.Children()[ruleMatchPosition], 0, -1, ctx);
			}
			return result;
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x000C9384 File Offset: 0x000C7584
		public static object Process_SubqueryFactoringClause_SubqueryFactoringClause_SubqueryFactoringClause_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[ruleMatchPosition + 1], 0, -1, ctx);
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x000C93B4 File Offset: 0x000C75B4
		public static object Process_SubqueryFactoringClause_ColmappedQueryName_AS_ParSubquery_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpSubqueryFactoringTerm oracleLpSubqueryFactoringTerm = new OracleLpSubqueryFactoringTerm(null);
			oracleLpSubqueryFactoringTerm.ColumnMappedQueryName = (OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[ruleMatchPosition], 0, -1, ctx) as OracleLpColumnMappedQueryName);
			OracleLpParExpression oracleLpParExpression = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[ruleMatchPosition + 2], 0, -1, ctx) as OracleLpParExpression;
			oracleLpSubqueryFactoringTerm.Subquery = oracleLpParExpression.Subquery;
			OracleLpQueryBlock oracleLpQueryBlock = (OracleLpQueryBlock)ctx.GetActiveObject(5);
			OracleLpWithClause withClause = oracleLpQueryBlock.WithClause;
			withClause.SubqueryFactoringClause.AddSubqueryFactoringTerm(oracleLpSubqueryFactoringTerm);
			return oracleLpSubqueryFactoringTerm;
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x000C9434 File Offset: 0x000C7634
		public static object Process_SubqueryFactoringClause_SearchClause_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[ruleMatchPosition], 0, -1, ctx);
		}

		// Token: 0x0600127F RID: 4735 RVA: 0x000C9460 File Offset: 0x000C7660
		public static object Process_SubqueryFactoringClause_CycleClause_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[ruleMatchPosition], 0, -1, ctx);
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x000C948C File Offset: 0x000C768C
		public static object Process_PlsqlDeclarations_SubprgBody_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpQueryBlock oracleLpQueryBlock = (OracleLpQueryBlock)ctx.GetActiveObject(5);
			OracleLpWithClause withClause = oracleLpQueryBlock.WithClause;
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			return oracleLpQueryBlock;
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x000C94C4 File Offset: 0x000C76C4
		public static object Process_PlsqlDeclarations_PlsqlDeclarations_SubprgBody_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpQueryBlock oracleLpQueryBlock = (OracleLpQueryBlock)ctx.GetActiveObject(5);
			OracleLpWithClause withClause = oracleLpQueryBlock.WithClause;
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx);
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
			return oracleLpQueryBlock;
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x000C9514 File Offset: 0x000C7714
		public static object Process_ParExpression_LEFT_PARENTHESIS_Subquery_RIGHT_PARENTHESIS_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpParExpression oracleLpParExpression = new OracleLpParExpression(null);
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			object obj = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
			oracleLpParExpression.Subquery = (obj as OracleLpSubquery);
			return oracleLpParExpression;
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x000C9558 File Offset: 0x000C7758
		public static object Process_ColmappedQueryName_Identifier_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return new OracleLpColumnMappedQueryName
			{
				Name = new OracleLpName(ctx.Tokens[ctx.CurrentParseNode.From].m_vContent)
			};
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x000C9594 File Offset: 0x000C7794
		public static object Process_ColmappedQueryName_Identifier_ColmappedQueryName_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpColumnMappedQueryName oracleLpColumnMappedQueryName = new OracleLpColumnMappedQueryName();
			ctx.SetActiveObject(12, oracleLpColumnMappedQueryName);
			oracleLpColumnMappedQueryName.Name = new OracleLpName(ctx.Tokens[list[0].From].m_vContent);
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
			return oracleLpColumnMappedQueryName;
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x000C95F4 File Offset: 0x000C77F4
		public static object Process_ColmappedQueryName_LEFT_PARENTHESIS_Identifier_StartWithRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpColumnMappedQueryName oracleLpColumnMappedQueryName = (OracleLpColumnMappedQueryName)ctx.GetActiveObject(12);
			oracleLpColumnMappedQueryName.AddColumnAlias(ctx.Tokens[list[1].From].m_vContent);
			return oracleLpColumnMappedQueryName;
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x000C9640 File Offset: 0x000C7840
		public static object Process_ColmappedQueryName_ColmappedQueryName_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[ruleMatchPosition], 0, -1, ctx);
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x000C9668 File Offset: 0x000C7868
		public static object Process_ColmappedQueryName_COMMA_Identifier_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpColumnMappedQueryName oracleLpColumnMappedQueryName = (OracleLpColumnMappedQueryName)ctx.GetActiveObject(12);
			oracleLpColumnMappedQueryName.AddColumnAlias(ctx.Tokens[list[ruleMatchPosition + 1].From].m_vContent);
			return oracleLpColumnMappedQueryName;
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x000C96B4 File Offset: 0x000C78B4
		public static object Process_CycleClause_CYCLE_Identifier_SET_Identifier_TO_StringLiteral_DEFAULT_StringLiteral_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			string vContent = ctx.Tokens[list[1].From].m_vContent;
			List<string> list2 = new List<string>();
			list2.Add(vContent);
			ctx.PropertiesBag.Properties["CYCLE_COLUMN_ALIASES"] = list2;
			vContent = ctx.Tokens[list[3].From].m_vContent;
			ctx.PropertiesBag.Properties["CYCLE_MARK_COLUMN_ALIASES"] = vContent;
			return null;
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x000C9744 File Offset: 0x000C7944
		public static object Process_QueryBlock_WithClause_StartWithRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode.Children()[ruleMatchPosition], 0, -1, ctx);
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x000C9760 File Offset: 0x000C7960
		public static object Process_QueryBlock_SelectClause_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode.Children()[ruleMatchPosition], 0, -1, ctx);
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x000C977C File Offset: 0x000C797C
		public static object Process_QueryBlock_FromClause_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpParserContext oracleLpParserContext = (OracleLpParserContext)ctx;
			oracleLpParserContext.CurrentStatementClause = OracleLpStatementClauseType.FromClause;
			oracleLpParserContext.HandleBindVariables = true;
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[ruleMatchPosition], 0, -1, ctx);
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x000C97C0 File Offset: 0x000C79C0
		public static object Process_QueryBlock_QueryBlock_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode.Children()[ruleMatchPosition], 0, -1, ctx);
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x000C97DC File Offset: 0x000C79DC
		public static object Process_QueryBlock_GroupByClause_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode.Children()[ruleMatchPosition], 0, -1, ctx);
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x000C97F8 File Offset: 0x000C79F8
		public static object Process_QueryBlock_HavingClause_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			object result = null;
			((OracleLpParserContext)ctx).CurrentStatementClause = OracleLpStatementClauseType.HavingClause;
			((OracleLpParserContext)ctx).HandleBindVariables = true;
			return result;
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x000C9824 File Offset: 0x000C7A24
		public static object Process_QueryBlock_ModelClause_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode.Children()[ruleMatchPosition], 0, -1, ctx);
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x000C9840 File Offset: 0x000C7A40
		public static object Process_QueryBlock_HierarchicalQueryClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			object result = null;
			((OracleLpParserContext)ctx).CurrentStatementClause = OracleLpStatementClauseType.HierarchicalQueryClause;
			((OracleLpParserContext)ctx).HandleBindVariables = true;
			return result;
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x000C986C File Offset: 0x000C7A6C
		public static object Process_QueryBlock_WhereClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("where_clause");
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x000C98B4 File Offset: 0x000C7AB4
		public static object Process_QueryBlock_HierarchicalQueryClause_WhereClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx);
			((OracleLpParserContext)ctx).CurrentStatementClause = OracleLpStatementClauseType.HierarchicalQueryClause;
			((OracleLpParserContext)ctx).HandleBindVariables = true;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("where_clause");
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x000C9928 File Offset: 0x000C7B28
		public static object Process_QueryBlock_WhereClause_HierarchicalQueryClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("where_clause");
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx);
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
			((OracleLpParserContext)ctx).CurrentStatementClause = OracleLpStatementClauseType.HierarchicalQueryClause;
			((OracleLpParserContext)ctx).HandleBindVariables = true;
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x000C999C File Offset: 0x000C7B9C
		public static object Process_FromClause_FROM_TableReferenceList_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpQueryBlock oracleLpQueryBlock = (OracleLpQueryBlock)ctx.GetActiveObject(5);
			oracleLpQueryBlock.FromClause = new OracleLpFromClauseAnsi(oracleLpQueryBlock);
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
			return null;
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x000C99E0 File Offset: 0x000C7BE0
		public static object Process_TableReferenceList_TableReference_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpQueryBlock oracleLpQueryBlock = (OracleLpQueryBlock)ctx.GetActiveObject(5);
			OracleLpTableReferenceAnsi oracleLpTableReferenceAnsi = (OracleLpTableReferenceAnsi)OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			oracleLpQueryBlock.FromClause.Terms.Add(oracleLpTableReferenceAnsi);
			return oracleLpTableReferenceAnsi;
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x000C9A28 File Offset: 0x000C7C28
		public static object Process_TableReferenceList_TableReferenceList_COMMA_TableReference_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpQueryBlock oracleLpQueryBlock = (OracleLpQueryBlock)ctx.GetActiveObject(5);
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx);
			OracleLpTableReferenceAnsi oracleLpTableReferenceAnsi = (OracleLpTableReferenceAnsi)OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[2], 0, -1, ctx);
			oracleLpQueryBlock.FromClause.Terms.Add(oracleLpTableReferenceAnsi);
			return oracleLpTableReferenceAnsi;
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x000C9A88 File Offset: 0x000C7C88
		public static object Process_TableReference_TablePrimaryOrJoinedTable_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x000C9AA0 File Offset: 0x000C7CA0
		public static object Process_TablePrimaryOrJoinedTable_TablePrimary_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpTableReferenceAnsiTablePrimary oracleLpTableReferenceAnsiTablePrimary = new OracleLpTableReferenceAnsiTablePrimary(null);
			ctx.SetActiveObject(7, oracleLpTableReferenceAnsiTablePrimary);
			oracleLpTableReferenceAnsiTablePrimary.TablePrimary = (OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx) as OracleLpTablePrimary);
			return oracleLpTableReferenceAnsiTablePrimary;
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x000C9AE0 File Offset: 0x000C7CE0
		public static object Process_TablePrimaryOrJoinedTable_JoinedTable_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpTableReferenceAnsiJoinedTable oracleLpTableReferenceAnsiJoinedTable = new OracleLpTableReferenceAnsiJoinedTable(null);
			ctx.SetActiveObject(7, oracleLpTableReferenceAnsiJoinedTable);
			oracleLpTableReferenceAnsiJoinedTable.JoinedTable = (OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx) as OracleLpJoinedTable);
			return oracleLpTableReferenceAnsiJoinedTable;
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x000C9B20 File Offset: 0x000C7D20
		public static object Process_SelectClause_SELECT_StartWithRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return null;
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x000C9B24 File Offset: 0x000C7D24
		public static object Process_SelectClause_DistinctUniqueAll_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpQueryBlock oracleLpQueryBlock = (OracleLpQueryBlock)ctx.GetActiveObject(5);
			oracleLpQueryBlock.SelectClause.SelectionType = (OracleLpSelectionType)OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[ruleMatchPosition], 0, -1, ctx);
			return null;
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x000C9B6C File Offset: 0x000C7D6C
		public static object Process_SelectClause_SelectList_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpParserContext oracleLpParserContext = (OracleLpParserContext)ctx;
			oracleLpParserContext.CurrentStatementClause = OracleLpStatementClauseType.SelectList;
			oracleLpParserContext.HandleBindVariables = true;
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[ruleMatchPosition], 0, -1, ctx);
			oracleLpParserContext.HandleBindVariables = true;
			return result;
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x000C9BB4 File Offset: 0x000C7DB4
		public static object Process_SelectClause_BULKCOLLECTOpt_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpQueryBlock oracleLpQueryBlock = (OracleLpQueryBlock)ctx.GetActiveObject(5);
			oracleLpQueryBlock.SelectClause.BulkCollect = true;
			return null;
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x000C9BDC File Offset: 0x000C7DDC
		public static object Process_SelectClause_IntoList_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			((OracleLpParserContext)ctx).CurrentStatementClause = OracleLpStatementClauseType.SelectIntoList;
			((OracleLpParserContext)ctx).HandleBindVariables = true;
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[ruleMatchPosition], 0, -1, ctx);
			((OracleLpParserContext)ctx).CurrentStatementClause = OracleLpStatementClauseType.SelectIntoList;
			((OracleLpParserContext)ctx).HandleBindVariables = true;
			return result;
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x000C9C38 File Offset: 0x000C7E38
		public static object Process_DistinctUniqueAll_ALL_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleLpSelectionType.ALL;
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x000C9C40 File Offset: 0x000C7E40
		public static object Process_DistinctUniqueAll_DISTINCT_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleLpSelectionType.DISTINCT;
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x000C9C48 File Offset: 0x000C7E48
		public static object Process_DistinctUniqueAll_UNIQUE_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleLpSelectionType.UNIQUE;
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x000C9C50 File Offset: 0x000C7E50
		public static object Process_SelectList_STAR_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpQueryBlock oracleLpQueryBlock = (OracleLpQueryBlock)ctx.GetActiveObject(5);
			OracleLpSelectClause selectClause = oracleLpQueryBlock.SelectClause;
			OracleLpSelectTermAll item = new OracleLpSelectTermAll(selectClause);
			selectClause.SelectList.Add(item);
			return selectClause;
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x000C9C88 File Offset: 0x000C7E88
		public static object Process_SelectList_SelectTerm_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			return null;
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x000C9CA4 File Offset: 0x000C7EA4
		public static object Process_SelectList_SelectList_COMMA_STAR_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx);
			OracleLpQueryBlock oracleLpQueryBlock = (OracleLpQueryBlock)ctx.GetActiveObject(5);
			OracleLpSelectClause selectClause = oracleLpQueryBlock.SelectClause;
			OracleLpSelectTermAll item = new OracleLpSelectTermAll(selectClause);
			selectClause.SelectList.Add(item);
			return selectClause;
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x000C9CF8 File Offset: 0x000C7EF8
		public static object Process_SelectList_SelectList_COMMA_SelectTerm_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx);
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[2], 0, -1, ctx);
			return null;
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x000C9D34 File Offset: 0x000C7F34
		public static object Process_SelectTerm_Expr_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpParserContext oracleLpParserContext = (OracleLpParserContext)ctx;
			OracleLpQueryBlock oracleLpQueryBlock = (OracleLpQueryBlock)ctx.GetActiveObject(5);
			OracleLpSelectClause selectClause = oracleLpQueryBlock.SelectClause;
			OracleLpSelectTermSpecific oracleLpSelectTermSpecific = new OracleLpSelectTermSpecific(selectClause);
			oracleLpSelectTermSpecific.BindRefStart = oracleLpParserContext.CurrentStatementBindVarCount;
			selectClause.SelectList.Add(oracleLpSelectTermSpecific);
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("expr");
			oracleLpParserContext.HandleBindVariables = true;
			OracleLpExpression oracleLpExpression = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx) as OracleLpExpression;
			ctx.RuleProcessorTable = ruleProcessorTable;
			List<LexerToken> tokens = ctx.Tokens;
			ParseNode currentParseNode = ctx.CurrentParseNode;
			int vBegin = tokens[currentParseNode.From].m_vBegin;
			int vEnd = tokens[currentParseNode.To - 1].m_vEnd;
			oracleLpExpression.Text = new OracleLpTextFragment(ctx.Script, vBegin, vEnd - vBegin);
			oracleLpSelectTermSpecific.Expression = oracleLpExpression;
			oracleLpSelectTermSpecific.BindRefEnd = oracleLpParserContext.CurrentStatementBindVarCount;
			return oracleLpSelectTermSpecific;
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x000C9E2C File Offset: 0x000C802C
		public static object Process_SelectTerm_Identifier_DOT_STAR_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpQueryBlock oracleLpQueryBlock = (OracleLpQueryBlock)ctx.GetActiveObject(5);
			OracleLpSelectClause selectClause = oracleLpQueryBlock.SelectClause;
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpSelectTermAll oracleLpSelectTermAll = new OracleLpSelectTermAll(selectClause);
			oracleLpSelectTermAll.ParentObjectName = new OracleLpName(ctx.Tokens[list[0].From].m_vContent);
			selectClause.SelectList.Add(oracleLpSelectTermAll);
			return oracleLpSelectTermAll;
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x000C9E94 File Offset: 0x000C8094
		public static object Process_SelectTerm_Identifier_DOT_Identifier_DOT_STAR_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpQueryBlock oracleLpQueryBlock = (OracleLpQueryBlock)ctx.GetActiveObject(5);
			OracleLpSelectClause selectClause = oracleLpQueryBlock.SelectClause;
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpSelectTermAll oracleLpSelectTermAll = new OracleLpSelectTermAll(selectClause);
			oracleLpSelectTermAll.SchemaName = new OracleLpName(ctx.Tokens[list[0].From].m_vContent);
			oracleLpSelectTermAll.ParentObjectName = new OracleLpName(ctx.Tokens[list[2].From].m_vContent);
			selectClause.SelectList.Add(oracleLpSelectTermAll);
			return oracleLpSelectTermAll;
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x000C9F24 File Offset: 0x000C8124
		public static object Process_SelectTerm_AliasedExpr_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x000C9F3C File Offset: 0x000C813C
		public static object Process_AliasedExpr_Expr_AsAlias_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpParserContext oracleLpParserContext = (OracleLpParserContext)ctx;
			OracleLpQueryBlock oracleLpQueryBlock = (OracleLpQueryBlock)ctx.GetActiveObject(5);
			OracleLpSelectClause selectClause = oracleLpQueryBlock.SelectClause;
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpSelectTermSpecific oracleLpSelectTermSpecific = new OracleLpSelectTermSpecific(selectClause);
			oracleLpSelectTermSpecific.BindRefStart = oracleLpParserContext.CurrentStatementBindVarCount;
			selectClause.SelectList.Add(oracleLpSelectTermSpecific);
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("expr");
			oracleLpParserContext.HandleBindVariables = true;
			OracleLpExpression oracleLpExpression = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx) as OracleLpExpression;
			ctx.RuleProcessorTable = ruleProcessorTable;
			List<LexerToken> tokens = ctx.Tokens;
			ParseNode parseNode = list[0];
			int vBegin = tokens[parseNode.From].m_vBegin;
			int vEnd = tokens[parseNode.To - 1].m_vEnd;
			oracleLpExpression.Text = new OracleLpTextFragment(ctx.Script, vBegin, vEnd - vBegin);
			oracleLpSelectTermSpecific.Expression = oracleLpExpression;
			oracleLpSelectTermSpecific.Alias = new OracleLpName(OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx) as string);
			oracleLpSelectTermSpecific.BindRefEnd = oracleLpParserContext.CurrentStatementBindVarCount;
			return oracleLpSelectTermSpecific;
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x000CA060 File Offset: 0x000C8260
		public static object Process_TablePrimary_TablePrimaryElement_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpTablePrimary ao = (OracleLpTablePrimary)ctx.GetActiveObject(8);
			OracleLpTablePrimaryTablePrimaryElement oracleLpTablePrimaryTablePrimaryElement = new OracleLpTablePrimaryTablePrimaryElement(null);
			ctx.SetActiveObject(8, oracleLpTablePrimaryTablePrimaryElement);
			oracleLpTablePrimaryTablePrimaryElement.TablePrimaryElement = (OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx) as OracleLpTablePrimaryElement);
			ctx.SetActiveObject(8, ao);
			return oracleLpTablePrimaryTablePrimaryElement;
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x000CA0B4 File Offset: 0x000C82B4
		public static object Process_TablePrimary_TablePrimaryElement_Identifier_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpTablePrimary ao = (OracleLpTablePrimary)ctx.GetActiveObject(8);
			OracleLpTablePrimaryTablePrimaryElement oracleLpTablePrimaryTablePrimaryElement = new OracleLpTablePrimaryTablePrimaryElement(null);
			ctx.SetActiveObject(8, oracleLpTablePrimaryTablePrimaryElement);
			oracleLpTablePrimaryTablePrimaryElement.TablePrimaryElement = (OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx) as OracleLpTablePrimaryElement);
			oracleLpTablePrimaryTablePrimaryElement.Alias = new OracleLpName(ctx.Tokens[list[1].From].m_vContent);
			ctx.SetActiveObject(8, ao);
			return oracleLpTablePrimaryTablePrimaryElement;
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x000CA134 File Offset: 0x000C8334
		public static object Process_TablePrimary_LEFT_PARENTHESIS_JoinedTable_RIGHT_PARENTHESIS_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpTablePrimary ao = (OracleLpTablePrimary)ctx.GetActiveObject(8);
			OracleLpTablePrimaryJoinedTable oracleLpTablePrimaryJoinedTable = new OracleLpTablePrimaryJoinedTable(null);
			ctx.SetActiveObject(8, oracleLpTablePrimaryJoinedTable);
			oracleLpTablePrimaryJoinedTable.JoinedTable = (OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx) as OracleLpJoinedTable);
			ctx.SetActiveObject(8, ao);
			return oracleLpTablePrimaryJoinedTable;
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x000CA18C File Offset: 0x000C838C
		public static object Process_TablePrimaryElement_ContainersClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x000CA1A4 File Offset: 0x000C83A4
		public static object Process_TablePrimaryElement_JsonTable_StartWithRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return new OracleLpTablePrimaryElementJsonTable(null);
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x000CA1BC File Offset: 0x000C83BC
		public static object Process_TablePrimaryElement_TablePrimaryElementQTE_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x000CA1D4 File Offset: 0x000C83D4
		public static object Process_TablePrimaryElementQTE_TablePrimaryElementQTE_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return new OracleLpTablePrimaryElementQueryTableExpression(null)
			{
				QueryTableExpression = (OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx) as OracleLpQueryTableExpression)
			};
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x000CA20C File Offset: 0x000C840C
		public static object Process_TablePrimaryElementQTE_TablePrimaryElementQTE_TablePrimaryElementQTE_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpTablePrimaryElementQueryTableExpression oracleLpTablePrimaryElementQueryTableExpression = new OracleLpTablePrimaryElementQueryTableExpression(null);
			oracleLpTablePrimaryElementQueryTableExpression.QueryTableExpression = (OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx) as OracleLpQueryTableExpression);
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
			return oracleLpTablePrimaryElementQueryTableExpression;
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x000CA258 File Offset: 0x000C8458
		public static object Process_TablePrimaryElementQTE_TablePrimaryElementQTE_FlashbackQueryClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpTablePrimaryElementQueryTableExpression oracleLpTablePrimaryElementQueryTableExpression = new OracleLpTablePrimaryElementQueryTableExpression(null);
			oracleLpTablePrimaryElementQueryTableExpression.QueryTableExpression = (OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx) as OracleLpQueryTableExpression);
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
			return oracleLpTablePrimaryElementQueryTableExpression;
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x000CA2A4 File Offset: 0x000C84A4
		public static object Process_TablePrimaryElementQTE_TablePrimaryElementQTE_FlashbackQueryClause_TablePrimaryElementQTE_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpTablePrimaryElementQueryTableExpression oracleLpTablePrimaryElementQueryTableExpression = new OracleLpTablePrimaryElementQueryTableExpression(null);
			oracleLpTablePrimaryElementQueryTableExpression.QueryTableExpression = (OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx) as OracleLpQueryTableExpression);
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[2], 0, -1, ctx);
			return oracleLpTablePrimaryElementQueryTableExpression;
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x000CA300 File Offset: 0x000C8500
		public static object Process_TablePrimaryElementQTE_PivotClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x000CA318 File Offset: 0x000C8518
		public static object Process_TablePrimaryElementQTE_RowPatternClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x000CA330 File Offset: 0x000C8530
		public static object Process_TablePrimaryElementQTE_UnpivotClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x000CA348 File Offset: 0x000C8548
		public static object Process_TablePrimaryElementQTE_QueryTableExpression_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x000CA360 File Offset: 0x000C8560
		public static object Process_TablePrimaryElementQTE_ONLY_LEFT_PARENTHESIS_QueryTableExpression_RIGHT_PARENTHESIS_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[2], 0, -1, ctx);
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x000CA388 File Offset: 0x000C8588
		public static object Process_ContainersClause_CONTAINERS_LEFT_PARENTHESIS_Identifier_RIGHT_PARENTHESIS_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpTablePrimaryElementContainers oracleLpTablePrimaryElementContainers = new OracleLpTablePrimaryElementContainers(null);
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpQteNamedObject oracleLpQteNamedObject = new OracleLpQteNamedObject(oracleLpTablePrimaryElementContainers);
			oracleLpTablePrimaryElementContainers.QueryTableExpression = oracleLpQteNamedObject;
			oracleLpQteNamedObject.ObjectName = new OracleLpName(ctx.Tokens[list[2].From].m_vContent);
			return oracleLpTablePrimaryElementContainers;
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x000CA3E0 File Offset: 0x000C85E0
		public static object Process_ContainersClause_CONTAINERS_LEFT_PARENTHESIS_Identifier_DOT_Identifier_RIGHT_PARENTHESIS_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpTablePrimaryElementContainers oracleLpTablePrimaryElementContainers = new OracleLpTablePrimaryElementContainers(null);
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpQteNamedObject oracleLpQteNamedObject = new OracleLpQteNamedObject(oracleLpTablePrimaryElementContainers);
			oracleLpTablePrimaryElementContainers.QueryTableExpression = oracleLpQteNamedObject;
			oracleLpQteNamedObject.SchemaName = new OracleLpName(ctx.Tokens[list[2].From].m_vContent);
			oracleLpQteNamedObject.ObjectName = new OracleLpName(ctx.Tokens[list[4].From].m_vContent);
			return oracleLpTablePrimaryElementContainers;
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x000CA460 File Offset: 0x000C8660
		public static object Process_QueryTableExpression_QueryTableExpressionNamedObject_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpQteNamedObject oracleLpQteNamedObject = new OracleLpQteNamedObject(null);
			ctx.SetActiveObject(13, oracleLpQteNamedObject);
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			return oracleLpQteNamedObject;
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x000CA494 File Offset: 0x000C8694
		public static object Process_QueryTableExpression_QueryTableExpressionSubquery_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx) as OracleLpQteSubquery;
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x000CA4C0 File Offset: 0x000C86C0
		public static object Process_QueryTableExpression_TableCollectionExpression_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpQteTableCollectionExpression oracleLpQteTableCollectionExpression = new OracleLpQteTableCollectionExpression(null);
			List<LexerToken> tokens = ctx.Tokens;
			ParseNode currentParseNode = ctx.CurrentParseNode;
			int vBegin = tokens[currentParseNode.From].m_vBegin;
			int vEnd = tokens[currentParseNode.To].m_vEnd;
			oracleLpQteTableCollectionExpression.ExpressionText = new OracleLpTextFragment(ctx.Script, vBegin, vEnd - vBegin);
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			return oracleLpQteTableCollectionExpression;
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x000CA534 File Offset: 0x000C8734
		public static object Process_QueryTableExpression_XMLTable_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpQteXMLTable result = new OracleLpQteXMLTable(null);
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			return result;
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x000CA560 File Offset: 0x000C8760
		public static object Process_QueryTableExpressionNamedObject_Identifier_DOT_StartWithRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpQteNamedObject oracleLpQteNamedObject = (OracleLpQteNamedObject)ctx.GetActiveObject(13);
			OracleLpName schemaName = new OracleLpName(ctx.Tokens[ctx.CurrentParseNode.Children()[0].From].m_vContent);
			oracleLpQteNamedObject.SchemaName = schemaName;
			return oracleLpQteNamedObject;
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x000CA5B0 File Offset: 0x000C87B0
		public static object Process_QueryTableExpressionNamedObject_QueryTableExpressionNamedObjectNN_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			object result;
			if (ctx.CurrentRule.IsUnary)
			{
				result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			}
			else
			{
				result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode.Children()[ruleMatchPosition], 0, -1, ctx);
			}
			return result;
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x000CA600 File Offset: 0x000C8800
		public static object Process_QueryTableExpressionNamedObject_SampleClause_EndWithRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode.Children()[ruleMatchPosition], 0, -1, ctx);
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x000CA61C File Offset: 0x000C881C
		public static object Process_QueryTableExpressionNamedObjectNN_Identifier_StartWithRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpQteNamedObject oracleLpQteNamedObject = (OracleLpQteNamedObject)ctx.GetActiveObject(13);
			string vContent;
			if (ctx.CurrentRule.IsUnary)
			{
				vContent = ctx.Tokens[ctx.CurrentParseNode.From].m_vContent;
			}
			else
			{
				vContent = ctx.Tokens[ctx.CurrentParseNode.Children()[0].From].m_vContent;
			}
			OracleLpName oracleLpName = new OracleLpName(vContent);
			oracleLpQteNamedObject.ObjectName = oracleLpName;
			return oracleLpName;
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x000CA698 File Offset: 0x000C8898
		public static object Process_QueryTableExpressionNamedObjectNN_AT_DbLink_EndWithRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpQteNamedObject oracleLpQteNamedObject = (OracleLpQteNamedObject)ctx.GetActiveObject(13);
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("dblink");
			oracleLpQteNamedObject.Dblink = (OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[ruleMatchPosition + 1], 0, -1, ctx) as OracleLpDbLink);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return oracleLpQteNamedObject;
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x000CA6FC File Offset: 0x000C88FC
		public static object Process_QueryTableExpressionNamedObjectNN_PartitionExtensionClause_EndWithRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode.Children()[ruleMatchPosition], 0, -1, ctx);
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x000CA718 File Offset: 0x000C8918
		public static object Process_QueryTableExpressionSubquery_Subquery_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			return new OracleLpQteSubquery(null)
			{
				Subquery = (OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[ruleMatchPosition], 0, -1, ctx) as OracleLpSubquery)
			};
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x000CA754 File Offset: 0x000C8954
		public static object Process_QueryTableExpressionSubquery_SubqueryRestrictionClause_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode.Children()[ruleMatchPosition], 0, -1, ctx);
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x000CA770 File Offset: 0x000C8970
		public static object Process_JoinedTable_TableReference_InnerCrossJoinClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpJoinedTable oracleLpJoinedTable = new OracleLpJoinedTable(null);
			oracleLpJoinedTable.TableReference = (OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx) as OracleLpTableReferenceAnsi);
			OracleLpBaseAnsiJoinClause ao = (OracleLpBaseAnsiJoinClause)ctx.GetActiveObject(14);
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
			oracleLpJoinedTable.JoinClause = (OracleLpBaseAnsiJoinClause)ctx.GetActiveObject(14);
			ctx.SetActiveObject(14, ao);
			return oracleLpJoinedTable;
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x000CA7E8 File Offset: 0x000C89E8
		public static object Process_JoinedTable_TableReference_OuterJoinClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpJoinedTable oracleLpJoinedTable = new OracleLpJoinedTable(null);
			oracleLpJoinedTable.TableReference = (OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx) as OracleLpTableReferenceAnsi);
			OracleLpBaseAnsiJoinClause ao = (OracleLpBaseAnsiJoinClause)ctx.GetActiveObject(14);
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
			oracleLpJoinedTable.JoinClause = (OracleLpBaseAnsiJoinClause)ctx.GetActiveObject(14);
			ctx.SetActiveObject(14, ao);
			return oracleLpJoinedTable;
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x000CA860 File Offset: 0x000C8A60
		public static object Process_JoinedTable_TableReference_CrossOuterApplyClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpJoinedTable oracleLpJoinedTable = new OracleLpJoinedTable(null);
			oracleLpJoinedTable.TableReference = (OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx) as OracleLpTableReferenceAnsi);
			OracleLpBaseAnsiJoinClause ao = (OracleLpBaseAnsiJoinClause)ctx.GetActiveObject(14);
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
			oracleLpJoinedTable.JoinClause = (OracleLpBaseAnsiJoinClause)ctx.GetActiveObject(14);
			ctx.SetActiveObject(14, ao);
			return oracleLpJoinedTable;
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x000CA8D8 File Offset: 0x000C8AD8
		public static object Process_InnerCrossJoinClause_InnerCrossJoinClause_JOIN_TablePrimary_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpAnsiInnerCrossJoinClauseNoCondition oracleLpAnsiInnerCrossJoinClauseNoCondition = new OracleLpAnsiInnerCrossJoinClauseNoCondition(null);
			ctx.SetActiveObject(14, oracleLpAnsiInnerCrossJoinClauseNoCondition);
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx);
			oracleLpAnsiInnerCrossJoinClauseNoCondition.TablePrimary = (OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[2], 0, -1, ctx) as OracleLpTablePrimary);
			return oracleLpAnsiInnerCrossJoinClauseNoCondition;
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x000CA92C File Offset: 0x000C8B2C
		public static object Process_InnerCrossJoinClause_JOIN_TableReference_OnUsingCondition_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpAnsiInnerCrossJoinClauseWithCondition oracleLpAnsiInnerCrossJoinClauseWithCondition = new OracleLpAnsiInnerCrossJoinClauseWithCondition(null);
			oracleLpAnsiInnerCrossJoinClauseWithCondition.Condition = new OracleLpJoinCondition();
			ctx.SetActiveObject(14, oracleLpAnsiInnerCrossJoinClauseWithCondition);
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			oracleLpAnsiInnerCrossJoinClauseWithCondition.TableReference = (OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx) as OracleLpTableReferenceAnsi);
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[2], 0, -1, ctx);
			return oracleLpAnsiInnerCrossJoinClauseWithCondition;
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x000CA98C File Offset: 0x000C8B8C
		public static object Process_InnerCrossJoinClause_CROSS_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpAnsiInnerCrossJoinClauseNoCondition oracleLpAnsiInnerCrossJoinClauseNoCondition = (OracleLpAnsiInnerCrossJoinClauseNoCondition)ctx.GetActiveObject(14);
			oracleLpAnsiInnerCrossJoinClauseNoCondition.Type = OracleLpInnerCrossJoinNoConditionType.Cross;
			return null;
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x000CA9B0 File Offset: 0x000C8BB0
		public static object Process_InnerCrossJoinClause_NATURAL_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpAnsiInnerCrossJoinClauseNoCondition oracleLpAnsiInnerCrossJoinClauseNoCondition = (OracleLpAnsiInnerCrossJoinClauseNoCondition)ctx.GetActiveObject(14);
			oracleLpAnsiInnerCrossJoinClauseNoCondition.Type = OracleLpInnerCrossJoinNoConditionType.Natural;
			return null;
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x000CA9D4 File Offset: 0x000C8BD4
		public static object Process_InnerCrossJoinClause_INNER_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpAnsiInnerCrossJoinClauseNoCondition oracleLpAnsiInnerCrossJoinClauseNoCondition = (OracleLpAnsiInnerCrossJoinClauseNoCondition)ctx.GetActiveObject(14);
			oracleLpAnsiInnerCrossJoinClauseNoCondition.Type = OracleLpInnerCrossJoinNoConditionType.NaturalInner;
			return null;
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x000CA9F8 File Offset: 0x000C8BF8
		public static object Process_CrossOuterApplyClause_CrossOuterApplyClause_APPLY_CrossOuterApplyClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[2], 0, -1, ctx);
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx);
			return result;
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x000CAA34 File Offset: 0x000C8C34
		public static object Process_CrossOuterApplyClause_CollectionExpression_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpAnsiCrossOuterApplyClauseCollectionExpression oracleLpAnsiCrossOuterApplyClauseCollectionExpression = new OracleLpAnsiCrossOuterApplyClauseCollectionExpression(null);
			ctx.SetActiveObject(14, oracleLpAnsiCrossOuterApplyClauseCollectionExpression);
			oracleLpAnsiCrossOuterApplyClauseCollectionExpression.CollectionExpression = (OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx) as OracleLpCollectionExpression);
			return oracleLpAnsiCrossOuterApplyClauseCollectionExpression;
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x000CAA74 File Offset: 0x000C8C74
		public static object Process_CrossOuterApplyClause_TablePrimary_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpAnsiCrossOuterApplyClauseTablePrimary oracleLpAnsiCrossOuterApplyClauseTablePrimary = new OracleLpAnsiCrossOuterApplyClauseTablePrimary(null);
			ctx.SetActiveObject(14, oracleLpAnsiCrossOuterApplyClauseTablePrimary);
			oracleLpAnsiCrossOuterApplyClauseTablePrimary.TablePrimary = (OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx) as OracleLpTablePrimary);
			return oracleLpAnsiCrossOuterApplyClauseTablePrimary;
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x000CAAB4 File Offset: 0x000C8CB4
		public static object Process_CrossOuterApplyClause_CROSS_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpAnsiCrossOuterApplyClause oracleLpAnsiCrossOuterApplyClause = (OracleLpAnsiCrossOuterApplyClause)ctx.GetActiveObject(14);
			oracleLpAnsiCrossOuterApplyClause.Type = OracleLpCrossOuterApplyType.Cross;
			return null;
		}

		// Token: 0x060012D4 RID: 4820 RVA: 0x000CAAD8 File Offset: 0x000C8CD8
		public static object Process_CrossOuterApplyClause_OUTER_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpAnsiCrossOuterApplyClause oracleLpAnsiCrossOuterApplyClause = (OracleLpAnsiCrossOuterApplyClause)ctx.GetActiveObject(14);
			oracleLpAnsiCrossOuterApplyClause.Type = OracleLpCrossOuterApplyType.Outer;
			return null;
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x000CAAFC File Offset: 0x000C8CFC
		public static object Process_OuterJoinClause_OuterJoinClause_TablePrimary_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpAnsiOuterJoinClauseNoCondition oracleLpAnsiOuterJoinClauseNoCondition = new OracleLpAnsiOuterJoinClauseNoCondition(null);
			ctx.SetActiveObject(14, oracleLpAnsiOuterJoinClauseNoCondition);
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx);
			oracleLpAnsiOuterJoinClauseNoCondition.TablePrimary = (OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx) as OracleLpTablePrimary);
			return oracleLpAnsiOuterJoinClauseNoCondition;
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x000CAB50 File Offset: 0x000C8D50
		public static object Process_OuterJoinClause_OuterJoinClause_TableReference_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpAnsiOuterJoinClauseWithCondition oracleLpAnsiOuterJoinClauseWithCondition = new OracleLpAnsiOuterJoinClauseWithCondition(null);
			ctx.SetActiveObject(14, oracleLpAnsiOuterJoinClauseWithCondition);
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx);
			oracleLpAnsiOuterJoinClauseWithCondition.TableReference = (OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx) as OracleLpTableReferenceAnsi);
			return oracleLpAnsiOuterJoinClauseWithCondition;
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x000CABA4 File Offset: 0x000C8DA4
		public static object Process_OuterJoinClause_TablePrimary_QueryPartitionClause_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[ruleMatchPosition + 1], 0, -1, ctx);
			return null;
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x000CABD0 File Offset: 0x000C8DD0
		public static object Process_OuterJoinClause_QueryPartitionClause_StartWithRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx);
			return null;
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x000CABFC File Offset: 0x000C8DFC
		public static object Process_OuterJoinClause_OnUsingCondition_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[ruleMatchPosition], 0, -1, ctx);
			return null;
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x000CAC28 File Offset: 0x000C8E28
		public static object Process_OuterJoinClause_OuterJoinType_JOIN_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx);
			return null;
		}

		// Token: 0x060012DB RID: 4827 RVA: 0x000CAC54 File Offset: 0x000C8E54
		public static object Process_OuterJoinClause_NATURAL_OuterJoinType_JOIN_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			IOracleLpAnsiOuterJoinClauseBase oracleLpAnsiOuterJoinClauseBase = (IOracleLpAnsiOuterJoinClauseBase)ctx.GetActiveObject(14);
			oracleLpAnsiOuterJoinClauseBase.Natural = true;
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
			return null;
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x000CAC94 File Offset: 0x000C8E94
		public static object Process_OuterJoinType_OuterJoinType_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			return null;
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x000CACB0 File Offset: 0x000C8EB0
		public static object Process_OuterJoinType_OuterJoinType_OUTER_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			IOracleLpAnsiOuterJoinClauseBase oracleLpAnsiOuterJoinClauseBase = (IOracleLpAnsiOuterJoinClauseBase)ctx.GetActiveObject(14);
			oracleLpAnsiOuterJoinClauseBase.Outer = true;
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx);
			return null;
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x000CACF0 File Offset: 0x000C8EF0
		public static object Process_OuterJoinType_FULL_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			IOracleLpAnsiOuterJoinClauseBase oracleLpAnsiOuterJoinClauseBase = (IOracleLpAnsiOuterJoinClauseBase)ctx.GetActiveObject(14);
			oracleLpAnsiOuterJoinClauseBase.Type = OracleLpOuterJoinType.Full;
			return null;
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x000CAD14 File Offset: 0x000C8F14
		public static object Process_OuterJoinType_LEFT_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			IOracleLpAnsiOuterJoinClauseBase oracleLpAnsiOuterJoinClauseBase = (IOracleLpAnsiOuterJoinClauseBase)ctx.GetActiveObject(14);
			oracleLpAnsiOuterJoinClauseBase.Type = OracleLpOuterJoinType.Left;
			return null;
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x000CAD38 File Offset: 0x000C8F38
		public static object Process_OuterJoinType_RIGHT_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			IOracleLpAnsiOuterJoinClauseBase oracleLpAnsiOuterJoinClauseBase = (IOracleLpAnsiOuterJoinClauseBase)ctx.GetActiveObject(14);
			oracleLpAnsiOuterJoinClauseBase.Type = OracleLpOuterJoinType.Right;
			return null;
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x000CAD5C File Offset: 0x000C8F5C
		public static object Process_OnUsingCondition_ON_Condition_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpBaseAnsiJoinClauseWithCondition oracleLpBaseAnsiJoinClauseWithCondition = (OracleLpBaseAnsiJoinClauseWithCondition)ctx.GetActiveObject(14);
			OracleLpJoinCondition condition = oracleLpBaseAnsiJoinClauseWithCondition.Condition;
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("condition");
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return condition;
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x000CADBC File Offset: 0x000C8FBC
		public static object Process_OnUsingCondition_USING_StartWithRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpBaseAnsiJoinClauseWithCondition oracleLpBaseAnsiJoinClauseWithCondition = (OracleLpBaseAnsiJoinClauseWithCondition)ctx.GetActiveObject(14);
			return oracleLpBaseAnsiJoinClauseWithCondition.Condition;
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x000CADE0 File Offset: 0x000C8FE0
		public static object Process_OnUsingCondition_LEFT_PARENTHESIS_Column_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpBaseAnsiJoinClauseWithCondition oracleLpBaseAnsiJoinClauseWithCondition = (OracleLpBaseAnsiJoinClauseWithCondition)ctx.GetActiveObject(14);
			OracleLpJoinCondition condition = oracleLpBaseAnsiJoinClauseWithCondition.Condition;
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("column");
			condition.Columns.Add(OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[ruleMatchPosition + 1], 0, -1, ctx) as OracleLpColumn);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return condition;
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x000CAE50 File Offset: 0x000C9050
		public static object Process_OnUsingCondition_OnUsingCondition_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[ruleMatchPosition], 0, -1, ctx);
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x000CAE78 File Offset: 0x000C9078
		public static object Process_OnUsingCondition_COMMA_Column_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpBaseAnsiJoinClauseWithCondition oracleLpBaseAnsiJoinClauseWithCondition = (OracleLpBaseAnsiJoinClauseWithCondition)ctx.GetActiveObject(14);
			OracleLpJoinCondition condition = oracleLpBaseAnsiJoinClauseWithCondition.Condition;
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("column");
			condition.Columns.Add(OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[ruleMatchPosition + 1], 0, -1, ctx) as OracleLpColumn);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return condition;
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x000CAEE8 File Offset: 0x000C90E8
		public static object Process_AsAlias_Identifier_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return ctx.Tokens[ctx.CurrentParseNode.From].m_vContent;
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x000CAF08 File Offset: 0x000C9108
		public static object Process_AsAlias_AS_Identifier_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return ctx.Tokens[ctx.CurrentParseNode.Children()[1].From].m_vContent;
		}

		// Token: 0x04001447 RID: 5191
		public static OracleMbEarleyRuleMultiProcessorAddItem[] s_vRuleProcessorItems = new OracleMbEarleyRuleMultiProcessorAddItem[]
		{
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "select",
				m_vRHSSymbols = new string[]
				{
					"subquery"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_Select_Subquery_StartWithRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllRulesStartingWith
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "select",
				m_vRHSSymbols = new string[]
				{
					"for_update_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_Select_ForUpdateClause_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "select",
				m_vRHSSymbols = new string[]
				{
					"order_by_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_Select_OrderByClause_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "subquery",
				m_vRHSSymbols = new string[]
				{
					"simple_set_expr"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_Subquery_SimpleSetExpr_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "subquery",
				m_vRHSSymbols = new string[]
				{
					"simple_set_expr",
					"order_by_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_Subquery_SimpleSetExpr_OrderByClause_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "subquery",
				m_vRHSSymbols = new string[]
				{
					"simple_set_expr",
					"row_limiting_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_Subquery_SimpleSetExpr_RowLimitingClause_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "subquery",
				m_vRHSSymbols = new string[]
				{
					"simple_set_expr",
					"order_by_clause",
					"row_limiting_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_Subquery_SimpleSetExpr_OrderByClause_RowLimitingClause_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "simple_set_expr",
				m_vRHSSymbols = new string[]
				{
					"query_block"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_SimpleSetExpr_QueryBlock_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "simple_set_expr",
				m_vRHSSymbols = new string[]
				{
					"simple_set_expr",
					"SET_OPER",
					"simple_set_expr"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_SimpleSetExpr_SimpleSetExpr_SET_OPER_SimpleSetExpr_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "simple_set_expr",
				m_vRHSSymbols = new string[]
				{
					"'('",
					"simple_set_expr",
					"')'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_SimpleSetExpr_LEFT_PARENTHESIS_SimpleSetExpr_RIGHT_PARENTHESIS_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "SET_OPER",
				m_vRHSSymbols = new string[]
				{
					"'INTERSECT'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_SET_OPER_INTERSECT_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "SET_OPER",
				m_vRHSSymbols = new string[]
				{
					"'MINUS'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_SET_OPER_MINUS_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "SET_OPER",
				m_vRHSSymbols = new string[]
				{
					"'UNION'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_SET_OPER_UNION_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "SET_OPER",
				m_vRHSSymbols = new string[]
				{
					"'UNION'",
					"'ALL'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_SET_OPER_UNION_ALL_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "row_limiting_clause",
				m_vRHSSymbols = new string[]
				{
					"row_limiting_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_RowLimitingClause_RowLimitingClause_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "row_limiting_clause",
				m_vRHSSymbols = new string[]
				{
					"row_limiting_clause",
					"row_limiting_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_RowLimitingClause_RowLimitingClause_RowLimitingClause_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "row_limiting_clause",
				m_vRHSSymbols = new string[]
				{
					"'OFFSET'",
					"digits",
					"row_limiting_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_RowLimitingClause_OFFSET_Digits_RowLimitingClause_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "row_limiting_clause",
				m_vRHSSymbols = new string[]
				{
					"'ROW'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_RowLimitingClause_Unit_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "row_limiting_clause",
				m_vRHSSymbols = new string[]
				{
					"'ROWS'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_RowLimitingClause_Unit_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "row_limiting_clause",
				m_vRHSSymbols = new string[]
				{
					"'FIRST'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_RowLimitingClause_Order_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "row_limiting_clause",
				m_vRHSSymbols = new string[]
				{
					"'NEXT'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_RowLimitingClause_Order_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "row_limiting_clause",
				m_vRHSSymbols = new string[]
				{
					"'FETCH'",
					"row_limiting_clause",
					"row_limiting_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_RowLimitingClause_FETCH_RowLimitingClause_RowLimitingClause_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "row_limiting_clause",
				m_vRHSSymbols = new string[]
				{
					"'FETCH'",
					"row_limiting_clause",
					"row_limiting_clause",
					"row_limiting_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_RowLimitingClause_FETCH_RowLimitingClause_RowLimitingClause_RowLimitingClause_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "row_limiting_clause",
				m_vRHSSymbols = new string[]
				{
					"'FETCH'",
					"row_limiting_clause",
					"row_limiting_clause",
					"row_limiting_clause",
					"row_limiting_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_RowLimitingClause_FETCH_RowLimitingClause_RowLimitingClause_RowLimitingClause_RowLimitingClause_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "row_limiting_clause",
				m_vRHSSymbols = new string[]
				{
					"digits"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_RowLimitingClause_Digits_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "row_limiting_clause",
				m_vRHSSymbols = new string[]
				{
					"digits",
					"'PERCENT'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_RowLimitingClause_Digits_PERCENT_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "row_limiting_clause",
				m_vRHSSymbols = new string[]
				{
					"'ONLY'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_RowLimitingClause_GetFetchType_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "row_limiting_clause",
				m_vRHSSymbols = new string[]
				{
					"'WITH'",
					"'TIES'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_RowLimitingClause_GetFetchType_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "with_clause",
				m_vRHSSymbols = new string[]
				{
					"'WITH'",
					"with_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_WithClause_WITH_WithClause_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "with_clause",
				m_vRHSSymbols = new string[]
				{
					"plsql_declarations"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_WithClause_PlsqlDeclarations_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "with_clause",
				m_vRHSSymbols = new string[]
				{
					"subquery_factoring_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_WithClause_SubqueryFactoringClause_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "with_clause",
				m_vRHSSymbols = new string[]
				{
					"plsql_declarations",
					"subquery_factoring_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_WithClause_PlsqlDeclarations_SubqueryFactoringClause_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "subquery_factoring_clause",
				m_vRHSSymbols = new string[]
				{
					"colmapped_query_name",
					"'AS'",
					"par_subquery"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_SubqueryFactoringClause_ColmappedQueryName_AS_ParSubquery_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "subquery_factoring_clause",
				m_vRHSSymbols = new string[]
				{
					"subquery_factoring_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_SubqueryFactoringClause_SubqueryFactoringClause_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "subquery_factoring_clause",
				m_vRHSSymbols = new string[]
				{
					"subquery_factoring_clause",
					"subquery_factoring_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_SubqueryFactoringClause_SubqueryFactoringClause_SubqueryFactoringClause_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "subquery_factoring_clause",
				m_vRHSSymbols = new string[]
				{
					"search_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_SubqueryFactoringClause_SearchClause_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "subquery_factoring_clause",
				m_vRHSSymbols = new string[]
				{
					"cycle_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_SubqueryFactoringClause_CycleClause_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "plsql_declarations",
				m_vRHSSymbols = new string[]
				{
					"subprg_body"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_PlsqlDeclarations_SubprgBody_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "plsql_declarations",
				m_vRHSSymbols = new string[]
				{
					"plsql_declarations",
					"subprg_body"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_PlsqlDeclarations_PlsqlDeclarations_SubprgBody_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "par_subquery",
				m_vRHSSymbols = new string[]
				{
					"'('",
					"subquery",
					"')'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_ParExpression_LEFT_PARENTHESIS_Subquery_RIGHT_PARENTHESIS_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "colmapped_query_name",
				m_vRHSSymbols = new string[]
				{
					"identifier"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_ColmappedQueryName_Identifier_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "colmapped_query_name",
				m_vRHSSymbols = new string[]
				{
					"identifier",
					"colmapped_query_name"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_ColmappedQueryName_Identifier_ColmappedQueryName_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "colmapped_query_name",
				m_vRHSSymbols = new string[]
				{
					"'('",
					"identifier"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_ColmappedQueryName_LEFT_PARENTHESIS_Identifier_StartWithRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllRulesStartingWith
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "colmapped_query_name",
				m_vRHSSymbols = new string[]
				{
					"colmapped_query_name"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_ColmappedQueryName_ColmappedQueryName_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "colmapped_query_name",
				m_vRHSSymbols = new string[]
				{
					"','",
					"identifier"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_ColmappedQueryName_COMMA_Identifier_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "cycle_clause",
				m_vRHSSymbols = new string[]
				{
					"'CYCLE'",
					"identifier",
					"'SET'",
					"identifier",
					"'TO'",
					"literal",
					"'DEFAULT'",
					"literal"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_CycleClause_CYCLE_Identifier_SET_Identifier_TO_StringLiteral_DEFAULT_StringLiteral_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "query_block",
				m_vRHSSymbols = new string[]
				{
					"with_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_QueryBlock_WithClause_StartWithRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllRulesStartingWith
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "query_block",
				m_vRHSSymbols = new string[]
				{
					"select_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_QueryBlock_SelectClause_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "query_block",
				m_vRHSSymbols = new string[]
				{
					"from_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_QueryBlock_FromClause_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "query_block",
				m_vRHSSymbols = new string[]
				{
					"query_block"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_QueryBlock_QueryBlock_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "query_block",
				m_vRHSSymbols = new string[]
				{
					"group_by_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_QueryBlock_GroupByClause_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "query_block",
				m_vRHSSymbols = new string[]
				{
					"having_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_QueryBlock_HavingClause_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "query_block",
				m_vRHSSymbols = new string[]
				{
					"model_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_QueryBlock_ModelClause_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "query_block",
				m_vRHSSymbols = new string[]
				{
					"hierarchical_query_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_QueryBlock_HierarchicalQueryClause_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "query_block",
				m_vRHSSymbols = new string[]
				{
					"where_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_QueryBlock_WhereClause_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "query_block",
				m_vRHSSymbols = new string[]
				{
					"hierarchical_query_clause",
					"where_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_QueryBlock_HierarchicalQueryClause_WhereClause_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "query_block",
				m_vRHSSymbols = new string[]
				{
					"where_clause",
					"hierarchical_query_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_QueryBlock_WhereClause_HierarchicalQueryClause_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "from_clause",
				m_vRHSSymbols = new string[]
				{
					"'FROM'",
					"table_reference_list"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_FromClause_FROM_TableReferenceList_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "table_reference_list",
				m_vRHSSymbols = new string[]
				{
					"table_reference"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_TableReferenceList_TableReference_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "table_reference_list",
				m_vRHSSymbols = new string[]
				{
					"table_reference_list",
					"','",
					"table_reference"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_TableReferenceList_TableReferenceList_COMMA_TableReference_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "table_reference",
				m_vRHSSymbols = new string[]
				{
					"table_primary_or_joined_table"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_TableReference_TablePrimaryOrJoinedTable_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "table_primary_or_joined_table",
				m_vRHSSymbols = new string[]
				{
					"table_primary"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_TablePrimaryOrJoinedTable_TablePrimary_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "table_primary_or_joined_table",
				m_vRHSSymbols = new string[]
				{
					"joined_table"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_TablePrimaryOrJoinedTable_JoinedTable_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "select_clause",
				m_vRHSSymbols = new string[]
				{
					"'SELECT'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_SelectClause_SELECT_StartWithRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllRulesStartingWith
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "select_clause",
				m_vRHSSymbols = new string[]
				{
					"distinct_unique_all"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_SelectClause_DistinctUniqueAll_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "select_clause",
				m_vRHSSymbols = new string[]
				{
					"select_list"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_SelectClause_SelectList_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "select_clause",
				m_vRHSSymbols = new string[]
				{
					"BULK_COLLECT_opt"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_SelectClause_BULKCOLLECTOpt_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "select_clause",
				m_vRHSSymbols = new string[]
				{
					"into_list"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_SelectClause_IntoList_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "distinct_unique_all",
				m_vRHSSymbols = new string[]
				{
					"'ALL'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_DistinctUniqueAll_ALL_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "distinct_unique_all",
				m_vRHSSymbols = new string[]
				{
					"'DISTINCT'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_DistinctUniqueAll_DISTINCT_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "distinct_unique_all",
				m_vRHSSymbols = new string[]
				{
					"'UNIQUE'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_DistinctUniqueAll_UNIQUE_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "select_list",
				m_vRHSSymbols = new string[]
				{
					"'*'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_SelectList_STAR_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "select_list",
				m_vRHSSymbols = new string[]
				{
					"select_term"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_SelectList_SelectTerm_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "select_list",
				m_vRHSSymbols = new string[]
				{
					"select_list",
					"','",
					"'*'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_SelectList_SelectList_COMMA_STAR_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "select_list",
				m_vRHSSymbols = new string[]
				{
					"select_list",
					"','",
					"select_term"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_SelectList_SelectList_COMMA_SelectTerm_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "select_term",
				m_vRHSSymbols = new string[]
				{
					"expr"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_SelectTerm_Expr_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "select_term",
				m_vRHSSymbols = new string[]
				{
					"identifier",
					"'.'",
					"'*'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_SelectTerm_Identifier_DOT_STAR_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "select_term",
				m_vRHSSymbols = new string[]
				{
					"identifier",
					"'.'",
					"identifier",
					"'.'",
					"'*'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_SelectTerm_Identifier_DOT_Identifier_DOT_STAR_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "select_term",
				m_vRHSSymbols = new string[]
				{
					"\"aliased_expr\""
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_SelectTerm_AliasedExpr_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "\"aliased_expr\"",
				m_vRHSSymbols = new string[]
				{
					"expr",
					"as_alias"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_AliasedExpr_Expr_AsAlias_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "table_primary",
				m_vRHSSymbols = new string[]
				{
					"table_primary_element"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_TablePrimary_TablePrimaryElement_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "table_primary",
				m_vRHSSymbols = new string[]
				{
					"table_primary_element",
					"identifier"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_TablePrimary_TablePrimaryElement_Identifier_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "table_primary",
				m_vRHSSymbols = new string[]
				{
					"'('",
					"joined_table",
					"')'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_TablePrimary_LEFT_PARENTHESIS_JoinedTable_RIGHT_PARENTHESIS_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "table_primary_element",
				m_vRHSSymbols = new string[]
				{
					"containers_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_TablePrimaryElement_ContainersClause_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "table_primary_element",
				m_vRHSSymbols = new string[]
				{
					"json_table"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_TablePrimaryElement_JsonTable_StartWithRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllRulesStartingWith
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "table_primary_element",
				m_vRHSSymbols = new string[]
				{
					"table_primary_element_qte"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_TablePrimaryElement_TablePrimaryElementQTE_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "table_primary_element_qte",
				m_vRHSSymbols = new string[]
				{
					"table_primary_element_qte"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_TablePrimaryElementQTE_TablePrimaryElementQTE_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "table_primary_element_qte",
				m_vRHSSymbols = new string[]
				{
					"table_primary_element_qte",
					"table_primary_element_qte"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_TablePrimaryElementQTE_TablePrimaryElementQTE_TablePrimaryElementQTE_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "table_primary_element_qte",
				m_vRHSSymbols = new string[]
				{
					"table_primary_element_qte",
					"flashback_query_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_TablePrimaryElementQTE_TablePrimaryElementQTE_FlashbackQueryClause_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "table_primary_element_qte",
				m_vRHSSymbols = new string[]
				{
					"table_primary_element_qte",
					"flashback_query_clause",
					"table_primary_element_qte"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_TablePrimaryElementQTE_TablePrimaryElementQTE_FlashbackQueryClause_TablePrimaryElementQTE_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "table_primary_element_qte",
				m_vRHSSymbols = new string[]
				{
					"pivot_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_TablePrimaryElementQTE_PivotClause_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "table_primary_element_qte",
				m_vRHSSymbols = new string[]
				{
					"row_pattern_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_TablePrimaryElementQTE_RowPatternClause_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "table_primary_element_qte",
				m_vRHSSymbols = new string[]
				{
					"unpivot_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_TablePrimaryElementQTE_UnpivotClause_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "table_primary_element_qte",
				m_vRHSSymbols = new string[]
				{
					"query_table_expression"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_TablePrimaryElementQTE_QueryTableExpression_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "table_primary_element_qte",
				m_vRHSSymbols = new string[]
				{
					"'ONLY'",
					"'('",
					"query_table_expression",
					"')'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_TablePrimaryElementQTE_ONLY_LEFT_PARENTHESIS_QueryTableExpression_RIGHT_PARENTHESIS_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "containers_clause",
				m_vRHSSymbols = new string[]
				{
					"'CONTAINERS'",
					"'('",
					"identifier",
					"')'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_ContainersClause_CONTAINERS_LEFT_PARENTHESIS_Identifier_RIGHT_PARENTHESIS_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "containers_clause",
				m_vRHSSymbols = new string[]
				{
					"'CONTAINERS'",
					"'('",
					"identifier",
					"'.'",
					"identifier",
					"')'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_ContainersClause_CONTAINERS_LEFT_PARENTHESIS_Identifier_DOT_Identifier_RIGHT_PARENTHESIS_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "query_table_expression",
				m_vRHSSymbols = new string[]
				{
					"query_table_expression_named_object"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_QueryTableExpression_QueryTableExpressionNamedObject_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "query_table_expression",
				m_vRHSSymbols = new string[]
				{
					"query_table_expression_subquery"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_QueryTableExpression_QueryTableExpressionSubquery_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "query_table_expression",
				m_vRHSSymbols = new string[]
				{
					"table_collection_expression"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_QueryTableExpression_TableCollectionExpression_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "query_table_expression",
				m_vRHSSymbols = new string[]
				{
					"xmltable"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_QueryTableExpression_XMLTable_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "query_table_expression_named_object",
				m_vRHSSymbols = new string[]
				{
					"identifier",
					"'.'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_QueryTableExpressionNamedObject_Identifier_DOT_StartWithRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllRulesStartingWith
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "query_table_expression_named_object",
				m_vRHSSymbols = new string[]
				{
					"query_table_expression_named_object#"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_QueryTableExpressionNamedObject_QueryTableExpressionNamedObjectNN_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "query_table_expression_named_object",
				m_vRHSSymbols = new string[]
				{
					"sample_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_QueryTableExpressionNamedObject_SampleClause_EndWithRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllRulesEndingWith
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "query_table_expression_named_object#",
				m_vRHSSymbols = new string[]
				{
					"identifier"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_QueryTableExpressionNamedObjectNN_Identifier_StartWithRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllRulesStartingWith
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "query_table_expression_named_object#",
				m_vRHSSymbols = new string[]
				{
					"'@'",
					"dblink"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_QueryTableExpressionNamedObjectNN_AT_DbLink_EndWithRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllRulesEndingWith
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "query_table_expression_named_object#",
				m_vRHSSymbols = new string[]
				{
					"partition_extension_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_QueryTableExpressionNamedObjectNN_PartitionExtensionClause_EndWithRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllRulesEndingWith
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "query_table_expression_subquery",
				m_vRHSSymbols = new string[]
				{
					"subquery"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_QueryTableExpressionSubquery_Subquery_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "query_table_expression_subquery",
				m_vRHSSymbols = new string[]
				{
					"subquery_restriction_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_QueryTableExpressionSubquery_SubqueryRestrictionClause_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "joined_table",
				m_vRHSSymbols = new string[]
				{
					"table_reference",
					"\"inner_cross_join_clause\""
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_JoinedTable_TableReference_InnerCrossJoinClause_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "joined_table",
				m_vRHSSymbols = new string[]
				{
					"table_reference",
					"outer_join_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_JoinedTable_TableReference_OuterJoinClause_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "joined_table",
				m_vRHSSymbols = new string[]
				{
					"table_reference",
					"cross_outer_apply_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_JoinedTable_TableReference_CrossOuterApplyClause_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "\"inner_cross_join_clause\"",
				m_vRHSSymbols = new string[]
				{
					"\"inner_cross_join_clause\"",
					"'JOIN'",
					"table_primary"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_InnerCrossJoinClause_InnerCrossJoinClause_JOIN_TablePrimary_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "\"inner_cross_join_clause\"",
				m_vRHSSymbols = new string[]
				{
					"'JOIN'",
					"table_reference",
					"on_using_condition"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_InnerCrossJoinClause_JOIN_TableReference_OnUsingCondition_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "\"inner_cross_join_clause\"",
				m_vRHSSymbols = new string[]
				{
					"'CROSS'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_InnerCrossJoinClause_CROSS_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "\"inner_cross_join_clause\"",
				m_vRHSSymbols = new string[]
				{
					"'NATURAL'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_InnerCrossJoinClause_NATURAL_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "\"inner_cross_join_clause\"",
				m_vRHSSymbols = new string[]
				{
					"'INNER'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_InnerCrossJoinClause_INNER_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "cross_outer_apply_clause",
				m_vRHSSymbols = new string[]
				{
					"cross_outer_apply_clause",
					"'APPLY'",
					"cross_outer_apply_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_CrossOuterApplyClause_CrossOuterApplyClause_APPLY_CrossOuterApplyClause_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "cross_outer_apply_clause",
				m_vRHSSymbols = new string[]
				{
					"collection_expression"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_CrossOuterApplyClause_CollectionExpression_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "cross_outer_apply_clause",
				m_vRHSSymbols = new string[]
				{
					"table_primary"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_CrossOuterApplyClause_TablePrimary_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "cross_outer_apply_clause",
				m_vRHSSymbols = new string[]
				{
					"'CROSS'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_CrossOuterApplyClause_CROSS_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "cross_outer_apply_clause",
				m_vRHSSymbols = new string[]
				{
					"'OUTER'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_CrossOuterApplyClause_OUTER_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "outer_join_clause",
				m_vRHSSymbols = new string[]
				{
					"outer_join_clause",
					"table_primary"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_OuterJoinClause_OuterJoinClause_TablePrimary_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "outer_join_clause",
				m_vRHSSymbols = new string[]
				{
					"outer_join_clause",
					"table_reference"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_OuterJoinClause_OuterJoinClause_TableReference_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "outer_join_clause",
				m_vRHSSymbols = new string[]
				{
					"table_primary",
					"query_partition_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_OuterJoinClause_TablePrimary_QueryPartitionClause_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "outer_join_clause",
				m_vRHSSymbols = new string[]
				{
					"query_partition_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_OuterJoinClause_QueryPartitionClause_StartWithRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllRulesStartingWith
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "outer_join_clause",
				m_vRHSSymbols = new string[]
				{
					"on_using_condition"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_OuterJoinClause_OnUsingCondition_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "outer_join_clause",
				m_vRHSSymbols = new string[]
				{
					"outer_join_type",
					"'JOIN'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_OuterJoinClause_OuterJoinType_JOIN_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "outer_join_clause",
				m_vRHSSymbols = new string[]
				{
					"'NATURAL'",
					"outer_join_type",
					"'JOIN'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_OuterJoinClause_NATURAL_OuterJoinType_JOIN_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "outer_join_type",
				m_vRHSSymbols = new string[]
				{
					"outer_join_type"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_OuterJoinType_OuterJoinType_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "outer_join_type",
				m_vRHSSymbols = new string[]
				{
					"outer_join_type",
					"'OUTER'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_OuterJoinType_OuterJoinType_OUTER_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "outer_join_type",
				m_vRHSSymbols = new string[]
				{
					"'FULL'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_OuterJoinType_FULL_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "outer_join_type",
				m_vRHSSymbols = new string[]
				{
					"'LEFT'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_OuterJoinType_LEFT_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "outer_join_type",
				m_vRHSSymbols = new string[]
				{
					"'RIGHT'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_OuterJoinType_RIGHT_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "on_using_condition",
				m_vRHSSymbols = new string[]
				{
					"'ON'",
					"condition"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_OnUsingCondition_ON_Condition_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "on_using_condition",
				m_vRHSSymbols = new string[]
				{
					"'USING'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_OnUsingCondition_USING_StartWithRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllRulesStartingWith
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "on_using_condition",
				m_vRHSSymbols = new string[]
				{
					"'('",
					"column"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_OnUsingCondition_LEFT_PARENTHESIS_Column_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "on_using_condition",
				m_vRHSSymbols = new string[]
				{
					"on_using_condition"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_OnUsingCondition_OnUsingCondition_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "on_using_condition",
				m_vRHSSymbols = new string[]
				{
					"','",
					"column"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_OnUsingCondition_COMMA_Column_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "as_alias",
				m_vRHSSymbols = new string[]
				{
					"identifier"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_AsAlias_Identifier_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "as_alias",
				m_vRHSSymbols = new string[]
				{
					"'AS'",
					"identifier"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectAnsiRuleMultiProcessors.Process_AsAlias_AS_Identifier_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			}
		};
	}
}
