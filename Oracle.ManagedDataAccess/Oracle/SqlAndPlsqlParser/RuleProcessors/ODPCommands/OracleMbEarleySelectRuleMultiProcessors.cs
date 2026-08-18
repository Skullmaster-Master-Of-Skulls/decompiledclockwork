using System;
using System.Collections.Generic;
using Oracle.SqlAndPlsqlParser.LocalParsing;

namespace Oracle.SqlAndPlsqlParser.RuleProcessors.ODPCommands
{
	// Token: 0x02000310 RID: 784
	internal static class OracleMbEarleySelectRuleMultiProcessors
	{
		// Token: 0x06001C37 RID: 7223 RVA: 0x00118378 File Offset: 0x00116578
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

		// Token: 0x06001C38 RID: 7224 RVA: 0x001183E8 File Offset: 0x001165E8
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

		// Token: 0x06001C39 RID: 7225 RVA: 0x00118438 File Offset: 0x00116638
		public static object Process_Select_OrderByClause_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode.Children()[ruleMatchPosition], ctx.CurrentRuleIndex + 1, -1, ctx);
			((OracleLpParserContext)ctx).CurrentStatementClause = OracleLpStatementClauseType.OrderByClause;
			((OracleLpParserContext)ctx).HandleBindVariables = true;
			return result;
		}

		// Token: 0x06001C3A RID: 7226 RVA: 0x00118484 File Offset: 0x00116684
		public static object Process_Subquery_SimpleSetExpr_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001C3B RID: 7227 RVA: 0x0011849C File Offset: 0x0011669C
		public static object Process_Subquery_SimpleSetExpr_OrderByClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx);
			((OracleLpParserContext)ctx).CurrentStatementClause = OracleLpStatementClauseType.OrderByClause;
			((OracleLpParserContext)ctx).HandleBindVariables = true;
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
			return result;
		}

		// Token: 0x06001C3C RID: 7228 RVA: 0x001184F0 File Offset: 0x001166F0
		public static object Process_Subquery_SimpleSetExpr_RowLimitingClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx);
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
			return result;
		}

		// Token: 0x06001C3D RID: 7229 RVA: 0x0011852C File Offset: 0x0011672C
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

		// Token: 0x06001C3E RID: 7230 RVA: 0x00118590 File Offset: 0x00116790
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

		// Token: 0x06001C3F RID: 7231 RVA: 0x001185DC File Offset: 0x001167DC
		public static object Process_SimpleSetExpr_Subquery_SET_OPER_Subquery_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
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

		// Token: 0x06001C40 RID: 7232 RVA: 0x0011866C File Offset: 0x0011686C
		public static object Process_SimpleSetExpr_LEFT_PARENTHESIS_Subquery_RIGHT_PARENTHESIS_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
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

		// Token: 0x06001C41 RID: 7233 RVA: 0x001186C8 File Offset: 0x001168C8
		public static object Process_SET_OPER_INTERSECT_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleLpSetOperator.INTERSECT;
		}

		// Token: 0x06001C42 RID: 7234 RVA: 0x001186D0 File Offset: 0x001168D0
		public static object Process_SET_OPER_MINUS_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleLpSetOperator.MINUS;
		}

		// Token: 0x06001C43 RID: 7235 RVA: 0x001186D8 File Offset: 0x001168D8
		public static object Process_SET_OPER_UNION_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleLpSetOperator.UNION;
		}

		// Token: 0x06001C44 RID: 7236 RVA: 0x001186E0 File Offset: 0x001168E0
		public static object Process_SET_OPER_UNION_ALL_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleLpSetOperator.UNION_ALL;
		}

		// Token: 0x06001C45 RID: 7237 RVA: 0x001186E8 File Offset: 0x001168E8
		public static object Process_RowLimitingClause_RowLimitingClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001C46 RID: 7238 RVA: 0x00118710 File Offset: 0x00116910
		public static object Process_RowLimitingClause_RowLimitingClause_RowLimitingClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			object obj = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx);
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
		}

		// Token: 0x06001C47 RID: 7239 RVA: 0x0011874C File Offset: 0x0011694C
		public static object Process_RowLimitingClause_OFFSET_Digits_RowLimitingClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			ctx.PropertiesBag.Properties["OFFSET"] = ctx.Tokens[list[1].From].m_vContent;
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[2], 0, -1, ctx);
		}

		// Token: 0x06001C48 RID: 7240 RVA: 0x001187AC File Offset: 0x001169AC
		public static object Process_RowLimitingClause_Unit_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			string vContent = ctx.Tokens[ctx.CurrentParseNode.From].m_vContent;
			ctx.PropertiesBag.Properties["ROW_LIMITING_CLAUSE_UNIT"] = vContent;
			return vContent;
		}

		// Token: 0x06001C49 RID: 7241 RVA: 0x001187EC File Offset: 0x001169EC
		public static object Process_RowLimitingClause_Order_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			string vContent = ctx.Tokens[ctx.CurrentParseNode.From].m_vContent;
			ctx.PropertiesBag.Properties["FETCH_ORDER"] = vContent;
			return vContent;
		}

		// Token: 0x06001C4A RID: 7242 RVA: 0x0011882C File Offset: 0x00116A2C
		public static object Process_RowLimitingClause_FETCH_RowLimitingClause_RowLimitingClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			object obj = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[2], 0, -1, ctx);
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x00118868 File Offset: 0x00116A68
		public static object Process_RowLimitingClause_FETCH_RowLimitingClause_RowLimitingClause_RowLimitingClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			object obj = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
			obj = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[2], 0, -1, ctx);
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[3], 0, -1, ctx);
		}

		// Token: 0x06001C4C RID: 7244 RVA: 0x001188B4 File Offset: 0x00116AB4
		public static object Process_RowLimitingClause_FETCH_RowLimitingClause_RowLimitingClause_RowLimitingClause_RowLimitingClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			object obj = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
			obj = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[2], 0, -1, ctx);
			obj = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[3], 0, -1, ctx);
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[4], 0, -1, ctx);
		}

		// Token: 0x06001C4D RID: 7245 RVA: 0x00118910 File Offset: 0x00116B10
		public static object Process_RowLimitingClause_Digits_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			string vContent = ctx.Tokens[ctx.CurrentParseNode.From].m_vContent;
			ctx.PropertiesBag.Properties["FETCH_COUNT"] = vContent;
			return vContent;
		}

		// Token: 0x06001C4E RID: 7246 RVA: 0x00118950 File Offset: 0x00116B50
		public static object Process_RowLimitingClause_Digits_PERCENT_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			ctx.CurrentParseNode.Children();
			string vContent = ctx.Tokens[ctx.CurrentParseNode.From].m_vContent;
			ctx.PropertiesBag.Properties["FETCH_PERCENT"] = vContent;
			return vContent;
		}

		// Token: 0x06001C4F RID: 7247 RVA: 0x0011899C File Offset: 0x00116B9C
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

		// Token: 0x06001C50 RID: 7248 RVA: 0x00118A3C File Offset: 0x00116C3C
		public static object Process_WithClause_WITH_WithClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpQueryBlock oracleLpQueryBlock = (OracleLpQueryBlock)ctx.GetActiveObject(5);
			oracleLpQueryBlock.WithClause = new OracleLpWithClause(null);
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode.Children()[1], 0, -1, ctx);
			return oracleLpQueryBlock;
		}

		// Token: 0x06001C51 RID: 7249 RVA: 0x00118A80 File Offset: 0x00116C80
		public static object Process_WithClause_PlsqlDeclarations_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpQueryBlock oracleLpQueryBlock = (OracleLpQueryBlock)ctx.GetActiveObject(5);
			OracleLpWithClause withClause = oracleLpQueryBlock.WithClause;
			withClause.PlsqlDeclarations = new OracleLpPlsqlDeclarations();
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			return oracleLpQueryBlock;
		}

		// Token: 0x06001C52 RID: 7250 RVA: 0x00118AC4 File Offset: 0x00116CC4
		public static object Process_WithClause_SubqueryFactoringClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpQueryBlock oracleLpQueryBlock = (OracleLpQueryBlock)ctx.GetActiveObject(5);
			OracleLpWithClause withClause = oracleLpQueryBlock.WithClause;
			withClause.SubqueryFactoringClause = new OracleLpSubqueryFactoringClause(null);
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			return oracleLpQueryBlock;
		}

		// Token: 0x06001C53 RID: 7251 RVA: 0x00118B08 File Offset: 0x00116D08
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

		// Token: 0x06001C54 RID: 7252 RVA: 0x00118B70 File Offset: 0x00116D70
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

		// Token: 0x06001C55 RID: 7253 RVA: 0x00118BC0 File Offset: 0x00116DC0
		public static object Process_SubqueryFactoringClause_SubqueryFactoringClause_SubqueryFactoringClause_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[ruleMatchPosition + 1], 0, -1, ctx);
		}

		// Token: 0x06001C56 RID: 7254 RVA: 0x00118BF0 File Offset: 0x00116DF0
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

		// Token: 0x06001C57 RID: 7255 RVA: 0x00118C70 File Offset: 0x00116E70
		public static object Process_SubqueryFactoringClause_SearchClause_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[ruleMatchPosition], 0, -1, ctx);
		}

		// Token: 0x06001C58 RID: 7256 RVA: 0x00118C9C File Offset: 0x00116E9C
		public static object Process_SubqueryFactoringClause_CycleClause_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[ruleMatchPosition], 0, -1, ctx);
		}

		// Token: 0x06001C59 RID: 7257 RVA: 0x00118CC8 File Offset: 0x00116EC8
		public static object Process_PlsqlDeclarations_SubprgBody_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpQueryBlock oracleLpQueryBlock = (OracleLpQueryBlock)ctx.GetActiveObject(5);
			OracleLpWithClause withClause = oracleLpQueryBlock.WithClause;
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			return oracleLpQueryBlock;
		}

		// Token: 0x06001C5A RID: 7258 RVA: 0x00118D00 File Offset: 0x00116F00
		public static object Process_PlsqlDeclarations_PlsqlDeclarations_SubprgBody_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpQueryBlock oracleLpQueryBlock = (OracleLpQueryBlock)ctx.GetActiveObject(5);
			OracleLpWithClause withClause = oracleLpQueryBlock.WithClause;
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx);
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
			return oracleLpQueryBlock;
		}

		// Token: 0x06001C5B RID: 7259 RVA: 0x00118D50 File Offset: 0x00116F50
		public static object Process_ParExpression_LEFT_PARENTHESIS_Subquery_RIGHT_PARENTHESIS_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpParExpression oracleLpParExpression = new OracleLpParExpression(null);
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			object obj = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
			oracleLpParExpression.Subquery = (obj as OracleLpSubquery);
			return oracleLpParExpression;
		}

		// Token: 0x06001C5C RID: 7260 RVA: 0x00118D94 File Offset: 0x00116F94
		public static object Process_ColmappedQueryName_Identifier_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return new OracleLpColumnMappedQueryName
			{
				Name = new OracleLpName(ctx.Tokens[ctx.CurrentParseNode.From].m_vContent)
			};
		}

		// Token: 0x06001C5D RID: 7261 RVA: 0x00118DD0 File Offset: 0x00116FD0
		public static object Process_ColmappedQueryName_Identifier_ColmappedQueryName_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpColumnMappedQueryName oracleLpColumnMappedQueryName = new OracleLpColumnMappedQueryName();
			ctx.SetActiveObject(12, oracleLpColumnMappedQueryName);
			oracleLpColumnMappedQueryName.Name = new OracleLpName(ctx.Tokens[list[0].From].m_vContent);
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
			return oracleLpColumnMappedQueryName;
		}

		// Token: 0x06001C5E RID: 7262 RVA: 0x00118E30 File Offset: 0x00117030
		public static object Process_ColmappedQueryName_LEFT_PARENTHESIS_Identifier_StartWithRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpColumnMappedQueryName oracleLpColumnMappedQueryName = (OracleLpColumnMappedQueryName)ctx.GetActiveObject(12);
			oracleLpColumnMappedQueryName.AddColumnAlias(ctx.Tokens[list[1].From].m_vContent);
			return oracleLpColumnMappedQueryName;
		}

		// Token: 0x06001C5F RID: 7263 RVA: 0x00118E7C File Offset: 0x0011707C
		public static object Process_ColmappedQueryName_ColmappedQueryName_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[ruleMatchPosition], 0, -1, ctx);
		}

		// Token: 0x06001C60 RID: 7264 RVA: 0x00118EA4 File Offset: 0x001170A4
		public static object Process_ColmappedQueryName_COMMA_Identifier_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpColumnMappedQueryName oracleLpColumnMappedQueryName = (OracleLpColumnMappedQueryName)ctx.GetActiveObject(12);
			oracleLpColumnMappedQueryName.AddColumnAlias(ctx.Tokens[list[ruleMatchPosition + 1].From].m_vContent);
			return oracleLpColumnMappedQueryName;
		}

		// Token: 0x06001C61 RID: 7265 RVA: 0x00118EF0 File Offset: 0x001170F0
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

		// Token: 0x06001C62 RID: 7266 RVA: 0x00118F80 File Offset: 0x00117180
		public static object Process_QueryBlock_WithClause_StartWithRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode.Children()[ruleMatchPosition], 0, -1, ctx);
		}

		// Token: 0x06001C63 RID: 7267 RVA: 0x00118F9C File Offset: 0x0011719C
		public static object Process_QueryBlock_SelectClause_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode.Children()[ruleMatchPosition], 0, -1, ctx);
		}

		// Token: 0x06001C64 RID: 7268 RVA: 0x00118FB8 File Offset: 0x001171B8
		public static object Process_QueryBlock_FromClause_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpParserContext oracleLpParserContext = (OracleLpParserContext)ctx;
			oracleLpParserContext.CurrentStatementClause = OracleLpStatementClauseType.FromClause;
			oracleLpParserContext.HandleBindVariables = true;
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[ruleMatchPosition], 0, -1, ctx);
		}

		// Token: 0x06001C65 RID: 7269 RVA: 0x00118FFC File Offset: 0x001171FC
		public static object Process_QueryBlock_QueryBlock_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode.Children()[ruleMatchPosition], 0, -1, ctx);
		}

		// Token: 0x06001C66 RID: 7270 RVA: 0x00119018 File Offset: 0x00117218
		public static object Process_QueryBlock_GroupByClause_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode.Children()[ruleMatchPosition], 0, -1, ctx);
		}

		// Token: 0x06001C67 RID: 7271 RVA: 0x00119034 File Offset: 0x00117234
		public static object Process_QueryBlock_HavingClause_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			object result = null;
			((OracleLpParserContext)ctx).CurrentStatementClause = OracleLpStatementClauseType.HavingClause;
			((OracleLpParserContext)ctx).HandleBindVariables = true;
			return result;
		}

		// Token: 0x06001C68 RID: 7272 RVA: 0x00119060 File Offset: 0x00117260
		public static object Process_QueryBlock_ModelClause_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode.Children()[ruleMatchPosition], 0, -1, ctx);
		}

		// Token: 0x06001C69 RID: 7273 RVA: 0x0011907C File Offset: 0x0011727C
		public static object Process_QueryBlock_HierarchicalQueryClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			object result = null;
			((OracleLpParserContext)ctx).CurrentStatementClause = OracleLpStatementClauseType.HierarchicalQueryClause;
			((OracleLpParserContext)ctx).HandleBindVariables = true;
			return result;
		}

		// Token: 0x06001C6A RID: 7274 RVA: 0x001190A8 File Offset: 0x001172A8
		public static object Process_QueryBlock_WhereClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("where_clause");
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001C6B RID: 7275 RVA: 0x001190F0 File Offset: 0x001172F0
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

		// Token: 0x06001C6C RID: 7276 RVA: 0x00119164 File Offset: 0x00117364
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

		// Token: 0x06001C6D RID: 7277 RVA: 0x001191D8 File Offset: 0x001173D8
		public static object Process_FromClause_FROM_CartesianProduct_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpQueryBlock oracleLpQueryBlock = (OracleLpQueryBlock)ctx.GetActiveObject(5);
			oracleLpQueryBlock.FromClause = new OracleLpFromClause(oracleLpQueryBlock);
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
			return null;
		}

		// Token: 0x06001C6E RID: 7278 RVA: 0x0011921C File Offset: 0x0011741C
		public static object Process_CartesianProduct_TableReferenceOrJoinClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx) as OracleLpFromListTerm;
		}

		// Token: 0x06001C6F RID: 7279 RVA: 0x00119248 File Offset: 0x00117448
		public static object Process_CartesianProduct_CartesianProduct_COMMA_TableReferenceOrJoinClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx);
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[2], 0, -1, ctx) as OracleLpFromListTerm;
		}

		// Token: 0x06001C70 RID: 7280 RVA: 0x00119288 File Offset: 0x00117488
		public static object Process_SelectClause_SELECT_StartWithRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return null;
		}

		// Token: 0x06001C71 RID: 7281 RVA: 0x0011928C File Offset: 0x0011748C
		public static object Process_SelectClause_DistinctUniqueAll_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpQueryBlock oracleLpQueryBlock = (OracleLpQueryBlock)ctx.GetActiveObject(5);
			oracleLpQueryBlock.SelectClause.SelectionType = (OracleLpSelectionType)OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[ruleMatchPosition], 0, -1, ctx);
			return null;
		}

		// Token: 0x06001C72 RID: 7282 RVA: 0x001192D4 File Offset: 0x001174D4
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

		// Token: 0x06001C73 RID: 7283 RVA: 0x0011931C File Offset: 0x0011751C
		public static object Process_SelectClause_BULKCOLLECTOpt_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpQueryBlock oracleLpQueryBlock = (OracleLpQueryBlock)ctx.GetActiveObject(5);
			oracleLpQueryBlock.SelectClause.BulkCollect = true;
			return null;
		}

		// Token: 0x06001C74 RID: 7284 RVA: 0x00119344 File Offset: 0x00117544
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

		// Token: 0x06001C75 RID: 7285 RVA: 0x001193A0 File Offset: 0x001175A0
		public static object Process_DistinctUniqueAll_ALL_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleLpSelectionType.ALL;
		}

		// Token: 0x06001C76 RID: 7286 RVA: 0x001193A8 File Offset: 0x001175A8
		public static object Process_DistinctUniqueAll_DISTINCT_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleLpSelectionType.DISTINCT;
		}

		// Token: 0x06001C77 RID: 7287 RVA: 0x001193B0 File Offset: 0x001175B0
		public static object Process_DistinctUniqueAll_UNIQUE_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleLpSelectionType.UNIQUE;
		}

		// Token: 0x06001C78 RID: 7288 RVA: 0x001193B8 File Offset: 0x001175B8
		public static object Process_SelectList_STAR_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpQueryBlock oracleLpQueryBlock = (OracleLpQueryBlock)ctx.GetActiveObject(5);
			OracleLpSelectClause selectClause = oracleLpQueryBlock.SelectClause;
			OracleLpSelectTermAll item = new OracleLpSelectTermAll(selectClause);
			selectClause.SelectList.Add(item);
			return selectClause;
		}

		// Token: 0x06001C79 RID: 7289 RVA: 0x001193F0 File Offset: 0x001175F0
		public static object Process_SelectList_SelectTerm_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			return null;
		}

		// Token: 0x06001C7A RID: 7290 RVA: 0x0011940C File Offset: 0x0011760C
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

		// Token: 0x06001C7B RID: 7291 RVA: 0x00119460 File Offset: 0x00117660
		public static object Process_SelectList_SelectList_COMMA_SelectTerm_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx);
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[2], 0, -1, ctx);
			return null;
		}

		// Token: 0x06001C7C RID: 7292 RVA: 0x0011949C File Offset: 0x0011769C
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

		// Token: 0x06001C7D RID: 7293 RVA: 0x00119594 File Offset: 0x00117794
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

		// Token: 0x06001C7E RID: 7294 RVA: 0x001195FC File Offset: 0x001177FC
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

		// Token: 0x06001C7F RID: 7295 RVA: 0x0011968C File Offset: 0x0011788C
		public static object Process_SelectTerm_AliasedExpr_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001C80 RID: 7296 RVA: 0x001196A4 File Offset: 0x001178A4
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

		// Token: 0x06001C81 RID: 7297 RVA: 0x001197C8 File Offset: 0x001179C8
		public static object Process_TableReferenceOrJoinClause_JoinClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpQueryBlock oracleLpQueryBlock = (OracleLpQueryBlock)ctx.GetActiveObject(5);
			OracleLpFromListTerm oracleLpFromListTerm = new OracleLpFromListTerm(OracleLpFromListTermType.JoinClause, oracleLpQueryBlock.FromClause);
			oracleLpQueryBlock.FromClause.Terms.Add(oracleLpFromListTerm);
			oracleLpFromListTerm.TableReference = (OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx) as OracleLpTableReference);
			return oracleLpFromListTerm;
		}

		// Token: 0x06001C82 RID: 7298 RVA: 0x00119824 File Offset: 0x00117A24
		public static object Process_TableReferenceOrJoinClause_TableReference_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpQueryBlock oracleLpQueryBlock = (OracleLpQueryBlock)ctx.GetActiveObject(5);
			OracleLpFromListTerm oracleLpFromListTerm = new OracleLpFromListTerm(OracleLpFromListTermType.TableReference, oracleLpQueryBlock.FromClause);
			oracleLpQueryBlock.FromClause.Terms.Add(oracleLpFromListTerm);
			oracleLpFromListTerm.TableReference = (OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx) as OracleLpTableReference);
			return oracleLpFromListTerm;
		}

		// Token: 0x06001C83 RID: 7299 RVA: 0x00119880 File Offset: 0x00117A80
		public static object Process_TableReferenceOrJoinClause_LEFT_PARENTHESIS_JoinClause_RIGHT_PARENTHESIS_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpQueryBlock oracleLpQueryBlock = (OracleLpQueryBlock)ctx.GetActiveObject(5);
			OracleLpFromListTerm oracleLpFromListTerm = new OracleLpFromListTerm(OracleLpFromListTermType.JoinClause, oracleLpQueryBlock.FromClause);
			oracleLpQueryBlock.FromClause.Terms.Add(oracleLpFromListTerm);
			oracleLpFromListTerm.TableReference = (OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx) as OracleLpTableReference);
			return oracleLpFromListTerm;
		}

		// Token: 0x06001C84 RID: 7300 RVA: 0x001198E0 File Offset: 0x00117AE0
		public static object Process_TableReference_TableReferenceNN_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpTableReference ao = (OracleLpTableReference)ctx.GetActiveObject(7);
			OracleLpTableReference oracleLpTableReference = new OracleLpTableReference(null);
			ctx.SetActiveObject(7, oracleLpTableReference);
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			ctx.SetActiveObject(7, ao);
			return oracleLpTableReference;
		}

		// Token: 0x06001C85 RID: 7301 RVA: 0x00119928 File Offset: 0x00117B28
		public static object Process_TableReferenceNN_TableReference_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001C86 RID: 7302 RVA: 0x00119940 File Offset: 0x00117B40
		public static object Process_TableReferenceNN_TableReference_Identifier_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpTableReference oracleLpTableReference = (OracleLpTableReference)ctx.GetActiveObject(7);
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx);
			oracleLpTableReference.Alias = new OracleLpName(ctx.Tokens[list[1].From].m_vContent);
			return oracleLpTableReference;
		}

		// Token: 0x06001C87 RID: 7303 RVA: 0x001199A0 File Offset: 0x00117BA0
		public static object Process_TableReference_TableReference_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001C88 RID: 7304 RVA: 0x001199B8 File Offset: 0x00117BB8
		public static object Process_TableReference_TableReference_TableReference_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx);
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
		}

		// Token: 0x06001C89 RID: 7305 RVA: 0x001199F0 File Offset: 0x00117BF0
		public static object Process_TableReference_TableReference_FlashbackQueryClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx);
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
		}

		// Token: 0x06001C8A RID: 7306 RVA: 0x00119A28 File Offset: 0x00117C28
		public static object Process_TableReference_TableReference_FlashbackQueryClause_TableReference_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx);
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[2], 0, -1, ctx);
		}

		// Token: 0x06001C8B RID: 7307 RVA: 0x00119A70 File Offset: 0x00117C70
		public static object Process_TableReference_ContainersClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpTableReference oracleLpTableReference = (OracleLpTableReference)ctx.GetActiveObject(7);
			oracleLpTableReference.TableReferenceType = OracleLpTableReferenceType.ContainersClause;
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001C8C RID: 7308 RVA: 0x00119AA8 File Offset: 0x00117CA8
		public static object Process_TableReference_PivotClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001C8D RID: 7309 RVA: 0x00119AC0 File Offset: 0x00117CC0
		public static object Process_TableReference_RowPatternClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001C8E RID: 7310 RVA: 0x00119AD8 File Offset: 0x00117CD8
		public static object Process_TableReference_UnpivotClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001C8F RID: 7311 RVA: 0x00119AF0 File Offset: 0x00117CF0
		public static object Process_TableReference_QueryTableExpression_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001C90 RID: 7312 RVA: 0x00119B08 File Offset: 0x00117D08
		public static object Process_TableReference_ONLY_StartWithRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpTableReference oracleLpTableReference = (OracleLpTableReference)ctx.GetActiveObject(7);
			oracleLpTableReference.OnlyQTE = true;
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[2], 0, -1, ctx);
		}

		// Token: 0x06001C91 RID: 7313 RVA: 0x00119B44 File Offset: 0x00117D44
		public static object Process_TableReference_JsonTable_StartWithRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpTableReference oracleLpTableReference = (OracleLpTableReference)ctx.GetActiveObject(7);
			oracleLpTableReference.TableReferenceType = OracleLpTableReferenceType.JsonTable;
			return oracleLpTableReference;
		}

		// Token: 0x06001C92 RID: 7314 RVA: 0x00119B68 File Offset: 0x00117D68
		public static object Process_ContainersClause_CONTAINERS_LEFT_PARENTHESIS_Identifier_RIGHT_PARENTHESIS_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpTableReference oracleLpTableReference = (OracleLpTableReference)ctx.GetActiveObject(7);
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpQteNamedObject oracleLpQteNamedObject = new OracleLpQteNamedObject(oracleLpTableReference);
			oracleLpTableReference.QueryTableExpression = oracleLpQteNamedObject;
			oracleLpQteNamedObject.ObjectName = new OracleLpName(ctx.Tokens[list[2].From].m_vContent);
			return oracleLpTableReference;
		}

		// Token: 0x06001C93 RID: 7315 RVA: 0x00119BC4 File Offset: 0x00117DC4
		public static object Process_ContainersClause_CONTAINERS_LEFT_PARENTHESIS_Identifier_DOT_Identifier_RIGHT_PARENTHESIS_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpTableReference oracleLpTableReference = (OracleLpTableReference)ctx.GetActiveObject(7);
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpQteNamedObject oracleLpQteNamedObject = new OracleLpQteNamedObject(oracleLpTableReference);
			oracleLpTableReference.QueryTableExpression = oracleLpQteNamedObject;
			oracleLpQteNamedObject.SchemaName = new OracleLpName(ctx.Tokens[list[2].From].m_vContent);
			oracleLpQteNamedObject.ObjectName = new OracleLpName(ctx.Tokens[list[4].From].m_vContent);
			return oracleLpTableReference;
		}

		// Token: 0x06001C94 RID: 7316 RVA: 0x00119C48 File Offset: 0x00117E48
		public static object Process_QueryTableExpression_QueryTableExpressionNamedObject_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpTableReference oracleLpTableReference = (OracleLpTableReference)ctx.GetActiveObject(7);
			oracleLpTableReference.QueryTableExpression = new OracleLpQteNamedObject(oracleLpTableReference);
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001C95 RID: 7317 RVA: 0x00119C84 File Offset: 0x00117E84
		public static object Process_QueryTableExpression_QueryTableExpressionSubquery_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpTableReference oracleLpTableReference = (OracleLpTableReference)ctx.GetActiveObject(7);
			OracleLpQteSubquery oracleLpQteSubquery = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx) as OracleLpQteSubquery;
			oracleLpTableReference.QueryTableExpression = oracleLpQteSubquery;
			return oracleLpQteSubquery;
		}

		// Token: 0x06001C96 RID: 7318 RVA: 0x00119CC4 File Offset: 0x00117EC4
		public static object Process_QueryTableExpression_TableCollectionExpression_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpTableReference oracleLpTableReference = (OracleLpTableReference)ctx.GetActiveObject(7);
			OracleLpQteTableCollectionExpression oracleLpQteTableCollectionExpression = new OracleLpQteTableCollectionExpression(oracleLpTableReference);
			oracleLpTableReference.QueryTableExpression = oracleLpQteTableCollectionExpression;
			List<LexerToken> tokens = ctx.Tokens;
			ParseNode currentParseNode = ctx.CurrentParseNode;
			int vBegin = tokens[currentParseNode.From].m_vBegin;
			int vEnd = tokens[currentParseNode.To].m_vEnd;
			oracleLpQteTableCollectionExpression.ExpressionText = new OracleLpTextFragment(ctx.Script, vBegin, vEnd - vBegin);
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001C97 RID: 7319 RVA: 0x00119D50 File Offset: 0x00117F50
		public static object Process_QueryTableExpressionNamedObject_QueryTableExpressionNamedObjectT1_StartWithRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			object result;
			if (ctx.CurrentRule.IsUnary)
			{
				result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			}
			else
			{
				result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode.Children()[0], 0, -1, ctx);
			}
			return result;
		}

		// Token: 0x06001C98 RID: 7320 RVA: 0x00119DA0 File Offset: 0x00117FA0
		public static object Process_QueryTableExpressionNamedObject_SampleClause_EndWithRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode.Children()[ruleMatchPosition], 0, -1, ctx);
		}

		// Token: 0x06001C99 RID: 7321 RVA: 0x00119DBC File Offset: 0x00117FBC
		public static object Process_QueryTableExpressionNamedObjectT1_QueryTableExpressionNamedObjectT2_StartWithRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			object result;
			if (ctx.CurrentRule.IsUnary)
			{
				result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			}
			else
			{
				result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode.Children()[0], 0, -1, ctx);
			}
			return result;
		}

		// Token: 0x06001C9A RID: 7322 RVA: 0x00119E0C File Offset: 0x0011800C
		public static object Process_QueryTableExpressionNamedObjectT1_QueryTableExpressionNamedObjectT3_EndWithRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode.Children()[ruleMatchPosition], 0, -1, ctx);
		}

		// Token: 0x06001C9B RID: 7323 RVA: 0x00119E28 File Offset: 0x00118028
		public static object Process_QueryTableExpressionNamedObjectT2_Identifier_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpTableReference oracleLpTableReference = (OracleLpTableReference)ctx.GetActiveObject(7);
			OracleLpQteNamedObject oracleLpQteNamedObject = oracleLpTableReference.QueryTableExpression as OracleLpQteNamedObject;
			oracleLpQteNamedObject.ObjectName = new OracleLpName(ctx.Tokens[ctx.CurrentParseNode.From].m_vContent);
			return null;
		}

		// Token: 0x06001C9C RID: 7324 RVA: 0x00119E78 File Offset: 0x00118078
		public static object Process_QueryTableExpressionNamedObjectT2_Identifier_DOT_Identifier_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpTableReference oracleLpTableReference = (OracleLpTableReference)ctx.GetActiveObject(7);
			OracleLpQteNamedObject oracleLpQteNamedObject = oracleLpTableReference.QueryTableExpression as OracleLpQteNamedObject;
			oracleLpQteNamedObject.SchemaName = new OracleLpName(ctx.Tokens[list[0].From].m_vContent);
			oracleLpQteNamedObject.ObjectName = new OracleLpName(ctx.Tokens[list[2].From].m_vContent);
			return null;
		}

		// Token: 0x06001C9D RID: 7325 RVA: 0x00119EFC File Offset: 0x001180FC
		public static object Process_QueryTableExpressionNamedObjectT3_AT_DbLink_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpTableReference oracleLpTableReference = (OracleLpTableReference)ctx.GetActiveObject(7);
			OracleLpQteNamedObject oracleLpQteNamedObject = oracleLpTableReference.QueryTableExpression as OracleLpQteNamedObject;
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("dblink");
			oracleLpQteNamedObject.Dblink = (OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx) as OracleLpDbLink);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return oracleLpQteNamedObject;
		}

		// Token: 0x06001C9E RID: 7326 RVA: 0x00119F68 File Offset: 0x00118168
		public static object Process_QueryTableExpressionNamedObjectT3_PartitionExtensionClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001C9F RID: 7327 RVA: 0x00119F80 File Offset: 0x00118180
		public static object Process_QueryTableExpressionSubquery_Subquery_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			return new OracleLpQteSubquery(null)
			{
				Subquery = (OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[ruleMatchPosition], 0, -1, ctx) as OracleLpSubquery)
			};
		}

		// Token: 0x06001CA0 RID: 7328 RVA: 0x00119FBC File Offset: 0x001181BC
		public static object Process_QueryTableExpressionSubquery_SubqueryRestrictionClause_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode.Children()[ruleMatchPosition], 0, -1, ctx);
		}

		// Token: 0x06001CA1 RID: 7329 RVA: 0x00119FD8 File Offset: 0x001181D8
		public static object Process_JoinClause_TableReference_JoinClauseList_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx);
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
			return result;
		}

		// Token: 0x06001CA2 RID: 7330 RVA: 0x0011A014 File Offset: 0x00118214
		public static object Process_JoinClauseList_JoinClauseTerm_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001CA3 RID: 7331 RVA: 0x0011A02C File Offset: 0x0011822C
		public static object Process_JoinClauseList_JoinClauseTerm_JoinClauseList_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx);
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
			return null;
		}

		// Token: 0x06001CA4 RID: 7332 RVA: 0x0011A068 File Offset: 0x00118268
		public static object Process_JoinClauseTerm_InnerCrossJoinClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpFromListTerm oracleLpFromListTerm = (OracleLpFromListTerm)ctx.GetActiveObject(6);
			oracleLpFromListTerm.JoinClauses.Add(new OracleLpInnerCrossJoinClause(oracleLpFromListTerm));
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			return null;
		}

		// Token: 0x06001CA5 RID: 7333 RVA: 0x0011A0AC File Offset: 0x001182AC
		public static object Process_JoinClauseTerm_OuterJoinClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpFromListTerm oracleLpFromListTerm = (OracleLpFromListTerm)ctx.GetActiveObject(6);
			oracleLpFromListTerm.JoinClauses.Add(new OracleLpOuterJoinClause(oracleLpFromListTerm));
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			return null;
		}

		// Token: 0x06001CA6 RID: 7334 RVA: 0x0011A0F0 File Offset: 0x001182F0
		public static object Process_JoinClauseTerm_CrossOuterApplyClause_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpFromListTerm oracleLpFromListTerm = (OracleLpFromListTerm)ctx.GetActiveObject(6);
			oracleLpFromListTerm.JoinClauses.Add(new OracleLpCrossOuterApplyClause(oracleLpFromListTerm));
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			return null;
		}

		// Token: 0x06001CA7 RID: 7335 RVA: 0x0011A134 File Offset: 0x00118334
		public static object Process_InnerCrossJoinClause_InnerCrossJoinClause_JOIN_TableReference_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpFromListTerm oracleLpFromListTerm = (OracleLpFromListTerm)ctx.GetActiveObject(6);
			OracleLpInnerCrossJoinClause oracleLpInnerCrossJoinClause = (OracleLpInnerCrossJoinClause)oracleLpFromListTerm.JoinClauses[oracleLpFromListTerm.JoinClauses.Count - 1];
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx);
			oracleLpInnerCrossJoinClause.TableReference = (OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[2], 0, -1, ctx) as OracleLpTableReference);
			return null;
		}

		// Token: 0x06001CA8 RID: 7336 RVA: 0x0011A1A4 File Offset: 0x001183A4
		public static object Process_InnerCrossJoinClause_JOIN_TableReference_OnUsingCondition_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpFromListTerm oracleLpFromListTerm = (OracleLpFromListTerm)ctx.GetActiveObject(6);
			OracleLpInnerCrossJoinClause oracleLpInnerCrossJoinClause = (OracleLpInnerCrossJoinClause)oracleLpFromListTerm.JoinClauses[oracleLpFromListTerm.JoinClauses.Count - 1];
			oracleLpInnerCrossJoinClause.Type = OracleLpInnerCrossJoinType.Condition;
			oracleLpInnerCrossJoinClause.Condition = new OracleLpJoinCondition();
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			oracleLpInnerCrossJoinClause.TableReference = (OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx) as OracleLpTableReference);
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[2], 0, -1, ctx);
			return null;
		}

		// Token: 0x06001CA9 RID: 7337 RVA: 0x0011A228 File Offset: 0x00118428
		public static object Process_InnerCrossJoinClause_CROSS_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpFromListTerm oracleLpFromListTerm = (OracleLpFromListTerm)ctx.GetActiveObject(6);
			OracleLpInnerCrossJoinClause oracleLpInnerCrossJoinClause = (OracleLpInnerCrossJoinClause)oracleLpFromListTerm.JoinClauses[oracleLpFromListTerm.JoinClauses.Count - 1];
			oracleLpInnerCrossJoinClause.Type = OracleLpInnerCrossJoinType.Cross;
			return null;
		}

		// Token: 0x06001CAA RID: 7338 RVA: 0x0011A268 File Offset: 0x00118468
		public static object Process_InnerCrossJoinClause_NATURAL_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpFromListTerm oracleLpFromListTerm = (OracleLpFromListTerm)ctx.GetActiveObject(6);
			OracleLpInnerCrossJoinClause oracleLpInnerCrossJoinClause = (OracleLpInnerCrossJoinClause)oracleLpFromListTerm.JoinClauses[oracleLpFromListTerm.JoinClauses.Count - 1];
			oracleLpInnerCrossJoinClause.Type = OracleLpInnerCrossJoinType.Natural;
			return null;
		}

		// Token: 0x06001CAB RID: 7339 RVA: 0x0011A2A8 File Offset: 0x001184A8
		public static object Process_InnerCrossJoinClause_INNER_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpFromListTerm oracleLpFromListTerm = (OracleLpFromListTerm)ctx.GetActiveObject(6);
			OracleLpInnerCrossJoinClause oracleLpInnerCrossJoinClause = (OracleLpInnerCrossJoinClause)oracleLpFromListTerm.JoinClauses[oracleLpFromListTerm.JoinClauses.Count - 1];
			oracleLpInnerCrossJoinClause.Inner = true;
			return null;
		}

		// Token: 0x06001CAC RID: 7340 RVA: 0x0011A2E8 File Offset: 0x001184E8
		public static object Process_CrossOuterApplyClause_CrossOuterApplyClause_APPLY_TableReference_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpFromListTerm oracleLpFromListTerm = (OracleLpFromListTerm)ctx.GetActiveObject(6);
			OracleLpCrossOuterApplyClause oracleLpCrossOuterApplyClause = (OracleLpCrossOuterApplyClause)oracleLpFromListTerm.JoinClauses[oracleLpFromListTerm.JoinClauses.Count - 1];
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx);
			oracleLpCrossOuterApplyClause.TableReference = (OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[2], 0, -1, ctx) as OracleLpTableReference);
			return null;
		}

		// Token: 0x06001CAD RID: 7341 RVA: 0x0011A358 File Offset: 0x00118558
		public static object Process_CrossOuterApplyClause_CROSS_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpFromListTerm oracleLpFromListTerm = (OracleLpFromListTerm)ctx.GetActiveObject(6);
			OracleLpCrossOuterApplyClause oracleLpCrossOuterApplyClause = (OracleLpCrossOuterApplyClause)oracleLpFromListTerm.JoinClauses[oracleLpFromListTerm.JoinClauses.Count - 1];
			oracleLpCrossOuterApplyClause.Type = OracleLpCrossOuterApplyType.Cross;
			return null;
		}

		// Token: 0x06001CAE RID: 7342 RVA: 0x0011A398 File Offset: 0x00118598
		public static object Process_CrossOuterApplyClause_OUTER_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpFromListTerm oracleLpFromListTerm = (OracleLpFromListTerm)ctx.GetActiveObject(6);
			OracleLpCrossOuterApplyClause oracleLpCrossOuterApplyClause = (OracleLpCrossOuterApplyClause)oracleLpFromListTerm.JoinClauses[oracleLpFromListTerm.JoinClauses.Count - 1];
			oracleLpCrossOuterApplyClause.Type = OracleLpCrossOuterApplyType.Outer;
			return null;
		}

		// Token: 0x06001CAF RID: 7343 RVA: 0x0011A3D8 File Offset: 0x001185D8
		public static object Process_OuterJoinClause_OuterJoinClause_TableReference_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpFromListTerm oracleLpFromListTerm = (OracleLpFromListTerm)ctx.GetActiveObject(6);
			OracleLpOuterJoinClause oracleLpOuterJoinClause = (OracleLpOuterJoinClause)oracleLpFromListTerm.JoinClauses[oracleLpFromListTerm.JoinClauses.Count - 1];
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[ruleMatchPosition], 0, -1, ctx);
			oracleLpOuterJoinClause.TableReference = (OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[ruleMatchPosition + 1], 0, -1, ctx) as OracleLpTableReference);
			return null;
		}

		// Token: 0x06001CB0 RID: 7344 RVA: 0x0011A44C File Offset: 0x0011864C
		public static object Process_OuterJoinClause_TableReference_QueryPartitionClause_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[ruleMatchPosition + 1], 0, -1, ctx);
			return null;
		}

		// Token: 0x06001CB1 RID: 7345 RVA: 0x0011A478 File Offset: 0x00118678
		public static object Process_OuterJoinClause_QueryPartitionClause_StartWithRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx);
			return null;
		}

		// Token: 0x06001CB2 RID: 7346 RVA: 0x0011A4A4 File Offset: 0x001186A4
		public static object Process_OuterJoinClause_OnUsingCondition_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpFromListTerm oracleLpFromListTerm = (OracleLpFromListTerm)ctx.GetActiveObject(6);
			OracleLpOuterJoinClause oracleLpOuterJoinClause = (OracleLpOuterJoinClause)oracleLpFromListTerm.JoinClauses[oracleLpFromListTerm.JoinClauses.Count - 1];
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			oracleLpOuterJoinClause.Condition = new OracleLpJoinCondition();
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[ruleMatchPosition], 0, -1, ctx);
			return null;
		}

		// Token: 0x06001CB3 RID: 7347 RVA: 0x0011A504 File Offset: 0x00118704
		public static object Process_OuterJoinClause_OuterJoinType_JOIN_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx);
			return null;
		}

		// Token: 0x06001CB4 RID: 7348 RVA: 0x0011A530 File Offset: 0x00118730
		public static object Process_OuterJoinClause_NATURAL_OuterJoinType_JOIN_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpFromListTerm oracleLpFromListTerm = (OracleLpFromListTerm)ctx.GetActiveObject(6);
			OracleLpOuterJoinClause oracleLpOuterJoinClause = (OracleLpOuterJoinClause)oracleLpFromListTerm.JoinClauses[oracleLpFromListTerm.JoinClauses.Count - 1];
			oracleLpOuterJoinClause.Natural = true;
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
			return null;
		}

		// Token: 0x06001CB5 RID: 7349 RVA: 0x0011A58C File Offset: 0x0011878C
		public static object Process_OuterJoinType_OuterJoinType_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			return null;
		}

		// Token: 0x06001CB6 RID: 7350 RVA: 0x0011A5A8 File Offset: 0x001187A8
		public static object Process_OuterJoinType_OuterJoinType_OUTER_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpFromListTerm oracleLpFromListTerm = (OracleLpFromListTerm)ctx.GetActiveObject(6);
			OracleLpOuterJoinClause oracleLpOuterJoinClause = (OracleLpOuterJoinClause)oracleLpFromListTerm.JoinClauses[oracleLpFromListTerm.JoinClauses.Count - 1];
			oracleLpOuterJoinClause.Outer = true;
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx);
			return null;
		}

		// Token: 0x06001CB7 RID: 7351 RVA: 0x0011A604 File Offset: 0x00118804
		public static object Process_OuterJoinType_FULL_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpFromListTerm oracleLpFromListTerm = (OracleLpFromListTerm)ctx.GetActiveObject(6);
			OracleLpOuterJoinClause oracleLpOuterJoinClause = (OracleLpOuterJoinClause)oracleLpFromListTerm.JoinClauses[oracleLpFromListTerm.JoinClauses.Count - 1];
			oracleLpOuterJoinClause.Type = OracleLpOuterJoinType.Full;
			return null;
		}

		// Token: 0x06001CB8 RID: 7352 RVA: 0x0011A644 File Offset: 0x00118844
		public static object Process_OuterJoinType_LEFT_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpFromListTerm oracleLpFromListTerm = (OracleLpFromListTerm)ctx.GetActiveObject(6);
			OracleLpOuterJoinClause oracleLpOuterJoinClause = (OracleLpOuterJoinClause)oracleLpFromListTerm.JoinClauses[oracleLpFromListTerm.JoinClauses.Count - 1];
			oracleLpOuterJoinClause.Type = OracleLpOuterJoinType.Left;
			return null;
		}

		// Token: 0x06001CB9 RID: 7353 RVA: 0x0011A684 File Offset: 0x00118884
		public static object Process_OuterJoinType_RIGHT_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpFromListTerm oracleLpFromListTerm = (OracleLpFromListTerm)ctx.GetActiveObject(6);
			OracleLpOuterJoinClause oracleLpOuterJoinClause = (OracleLpOuterJoinClause)oracleLpFromListTerm.JoinClauses[oracleLpFromListTerm.JoinClauses.Count - 1];
			oracleLpOuterJoinClause.Type = OracleLpOuterJoinType.Right;
			return null;
		}

		// Token: 0x06001CBA RID: 7354 RVA: 0x0011A6C4 File Offset: 0x001188C4
		public static object Process_OnUsingCondition_ON_Condition_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpFromListTerm oracleLpFromListTerm = (OracleLpFromListTerm)ctx.GetActiveObject(6);
			OracleLpJoinCondition condition = oracleLpFromListTerm.JoinClauses[oracleLpFromListTerm.JoinClauses.Count - 1].Condition;
			condition.Type = OracleLpJoinConditionType.On;
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("condition");
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return condition;
		}

		// Token: 0x06001CBB RID: 7355 RVA: 0x0011A740 File Offset: 0x00118940
		public static object Process_OnUsingCondition_USING_StartWithRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpFromListTerm oracleLpFromListTerm = (OracleLpFromListTerm)ctx.GetActiveObject(6);
			OracleLpJoinCondition condition = oracleLpFromListTerm.JoinClauses[oracleLpFromListTerm.JoinClauses.Count - 1].Condition;
			condition.Type = OracleLpJoinConditionType.Using;
			return condition;
		}

		// Token: 0x06001CBC RID: 7356 RVA: 0x0011A780 File Offset: 0x00118980
		public static object Process_OnUsingCondition_LEFT_PARENTHESIS_Column_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpFromListTerm oracleLpFromListTerm = (OracleLpFromListTerm)ctx.GetActiveObject(6);
			OracleLpJoinCondition condition = oracleLpFromListTerm.JoinClauses[oracleLpFromListTerm.JoinClauses.Count - 1].Condition;
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("column");
			condition.Columns.Add(OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[ruleMatchPosition + 1], 0, -1, ctx) as OracleLpColumn);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return condition;
		}

		// Token: 0x06001CBD RID: 7357 RVA: 0x0011A808 File Offset: 0x00118A08
		public static object Process_OnUsingCondition_OnUsingCondition_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[ruleMatchPosition], 0, -1, ctx);
		}

		// Token: 0x06001CBE RID: 7358 RVA: 0x0011A830 File Offset: 0x00118A30
		public static object Process_OnUsingCondition_COMMA_Column_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpFromListTerm oracleLpFromListTerm = (OracleLpFromListTerm)ctx.GetActiveObject(6);
			OracleLpJoinCondition condition = oracleLpFromListTerm.JoinClauses[oracleLpFromListTerm.JoinClauses.Count - 1].Condition;
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("column");
			condition.Columns.Add(OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[ruleMatchPosition + 1], 0, -1, ctx) as OracleLpColumn);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return condition;
		}

		// Token: 0x06001CBF RID: 7359 RVA: 0x0011A8B8 File Offset: 0x00118AB8
		public static object Process_AsAlias_Identifier_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return ctx.Tokens[ctx.CurrentParseNode.From].m_vContent;
		}

		// Token: 0x06001CC0 RID: 7360 RVA: 0x0011A8D8 File Offset: 0x00118AD8
		public static object Process_AsAlias_AS_Identifier_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return ctx.Tokens[ctx.CurrentParseNode.Children()[1].From].m_vContent;
		}

		// Token: 0x04001D65 RID: 7525
		public static OracleMbEarleyRuleMultiProcessorAddItem[] s_vRuleProcessorItems = new OracleMbEarleyRuleMultiProcessorAddItem[]
		{
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "select",
				m_vRHSSymbols = new string[]
				{
					"subquery"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_Select_Subquery_StartWithRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllRulesStartingWith
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "select",
				m_vRHSSymbols = new string[]
				{
					"for_update_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_Select_ForUpdateClause_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "select",
				m_vRHSSymbols = new string[]
				{
					"order_by_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_Select_OrderByClause_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "subquery",
				m_vRHSSymbols = new string[]
				{
					"simple_set_expr"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_Subquery_SimpleSetExpr_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_Subquery_SimpleSetExpr_OrderByClause_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_Subquery_SimpleSetExpr_RowLimitingClause_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_Subquery_SimpleSetExpr_OrderByClause_RowLimitingClause_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "simple_set_expr",
				m_vRHSSymbols = new string[]
				{
					"query_block"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_SimpleSetExpr_QueryBlock_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "simple_set_expr",
				m_vRHSSymbols = new string[]
				{
					"subquery",
					"SET_OPER",
					"subquery"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_SimpleSetExpr_Subquery_SET_OPER_Subquery_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "simple_set_expr",
				m_vRHSSymbols = new string[]
				{
					"'('",
					"subquery",
					"')'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_SimpleSetExpr_LEFT_PARENTHESIS_Subquery_RIGHT_PARENTHESIS_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "SET_OPER",
				m_vRHSSymbols = new string[]
				{
					"'INTERSECT'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_SET_OPER_INTERSECT_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "SET_OPER",
				m_vRHSSymbols = new string[]
				{
					"'MINUS'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_SET_OPER_MINUS_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "SET_OPER",
				m_vRHSSymbols = new string[]
				{
					"'UNION'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_SET_OPER_UNION_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_SET_OPER_UNION_ALL_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "row_limiting_clause",
				m_vRHSSymbols = new string[]
				{
					"row_limiting_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_RowLimitingClause_RowLimitingClause_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_RowLimitingClause_RowLimitingClause_RowLimitingClause_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_RowLimitingClause_OFFSET_Digits_RowLimitingClause_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "row_limiting_clause",
				m_vRHSSymbols = new string[]
				{
					"'ROW'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_RowLimitingClause_Unit_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "row_limiting_clause",
				m_vRHSSymbols = new string[]
				{
					"'ROWS'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_RowLimitingClause_Unit_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "row_limiting_clause",
				m_vRHSSymbols = new string[]
				{
					"'FIRST'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_RowLimitingClause_Order_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "row_limiting_clause",
				m_vRHSSymbols = new string[]
				{
					"'NEXT'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_RowLimitingClause_Order_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_RowLimitingClause_FETCH_RowLimitingClause_RowLimitingClause_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_RowLimitingClause_FETCH_RowLimitingClause_RowLimitingClause_RowLimitingClause_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_RowLimitingClause_FETCH_RowLimitingClause_RowLimitingClause_RowLimitingClause_RowLimitingClause_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "row_limiting_clause",
				m_vRHSSymbols = new string[]
				{
					"digits"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_RowLimitingClause_Digits_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_RowLimitingClause_Digits_PERCENT_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "row_limiting_clause",
				m_vRHSSymbols = new string[]
				{
					"'ONLY'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_RowLimitingClause_GetFetchType_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_RowLimitingClause_GetFetchType_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_WithClause_WITH_WithClause_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "with_clause",
				m_vRHSSymbols = new string[]
				{
					"plsql_declarations"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_WithClause_PlsqlDeclarations_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "with_clause",
				m_vRHSSymbols = new string[]
				{
					"subquery_factoring_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_WithClause_SubqueryFactoringClause_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_WithClause_PlsqlDeclarations_SubqueryFactoringClause_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_SubqueryFactoringClause_ColmappedQueryName_AS_ParSubquery_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "subquery_factoring_clause",
				m_vRHSSymbols = new string[]
				{
					"subquery_factoring_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_SubqueryFactoringClause_SubqueryFactoringClause_PartialRule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_SubqueryFactoringClause_SubqueryFactoringClause_SubqueryFactoringClause_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "subquery_factoring_clause",
				m_vRHSSymbols = new string[]
				{
					"search_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_SubqueryFactoringClause_SearchClause_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "subquery_factoring_clause",
				m_vRHSSymbols = new string[]
				{
					"cycle_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_SubqueryFactoringClause_CycleClause_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "plsql_declarations",
				m_vRHSSymbols = new string[]
				{
					"subprg_body"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_PlsqlDeclarations_SubprgBody_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_PlsqlDeclarations_PlsqlDeclarations_SubprgBody_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_ParExpression_LEFT_PARENTHESIS_Subquery_RIGHT_PARENTHESIS_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "colmapped_query_name",
				m_vRHSSymbols = new string[]
				{
					"identifier"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_ColmappedQueryName_Identifier_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_ColmappedQueryName_Identifier_ColmappedQueryName_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_ColmappedQueryName_LEFT_PARENTHESIS_Identifier_StartWithRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllRulesStartingWith
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "colmapped_query_name",
				m_vRHSSymbols = new string[]
				{
					"colmapped_query_name"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_ColmappedQueryName_ColmappedQueryName_PartialRule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_ColmappedQueryName_COMMA_Identifier_PartialRule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_CycleClause_CYCLE_Identifier_SET_Identifier_TO_StringLiteral_DEFAULT_StringLiteral_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "query_block",
				m_vRHSSymbols = new string[]
				{
					"with_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_QueryBlock_WithClause_StartWithRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllRulesStartingWith
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "query_block",
				m_vRHSSymbols = new string[]
				{
					"select_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_QueryBlock_SelectClause_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "query_block",
				m_vRHSSymbols = new string[]
				{
					"from_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_QueryBlock_FromClause_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "query_block",
				m_vRHSSymbols = new string[]
				{
					"query_block"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_QueryBlock_QueryBlock_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "query_block",
				m_vRHSSymbols = new string[]
				{
					"group_by_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_QueryBlock_GroupByClause_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "query_block",
				m_vRHSSymbols = new string[]
				{
					"having_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_QueryBlock_HavingClause_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "query_block",
				m_vRHSSymbols = new string[]
				{
					"model_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_QueryBlock_ModelClause_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "query_block",
				m_vRHSSymbols = new string[]
				{
					"hierarchical_query_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_QueryBlock_HierarchicalQueryClause_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "query_block",
				m_vRHSSymbols = new string[]
				{
					"where_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_QueryBlock_WhereClause_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_QueryBlock_HierarchicalQueryClause_WhereClause_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_QueryBlock_WhereClause_HierarchicalQueryClause_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "from_clause",
				m_vRHSSymbols = new string[]
				{
					"'FROM'",
					"cartesian_product"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_FromClause_FROM_CartesianProduct_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "cartesian_product",
				m_vRHSSymbols = new string[]
				{
					"table_reference_or_join_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_CartesianProduct_TableReferenceOrJoinClause_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "cartesian_product",
				m_vRHSSymbols = new string[]
				{
					"cartesian_product",
					"','",
					"table_reference_or_join_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_CartesianProduct_CartesianProduct_COMMA_TableReferenceOrJoinClause_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "select_clause",
				m_vRHSSymbols = new string[]
				{
					"'SELECT'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_SelectClause_SELECT_StartWithRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllRulesStartingWith
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "select_clause",
				m_vRHSSymbols = new string[]
				{
					"distinct_unique_all"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_SelectClause_DistinctUniqueAll_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "select_clause",
				m_vRHSSymbols = new string[]
				{
					"select_list"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_SelectClause_SelectList_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "select_clause",
				m_vRHSSymbols = new string[]
				{
					"BULK_COLLECT_opt"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_SelectClause_BULKCOLLECTOpt_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "select_clause",
				m_vRHSSymbols = new string[]
				{
					"into_list"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_SelectClause_IntoList_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "distinct_unique_all",
				m_vRHSSymbols = new string[]
				{
					"'ALL'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_DistinctUniqueAll_ALL_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "distinct_unique_all",
				m_vRHSSymbols = new string[]
				{
					"'DISTINCT'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_DistinctUniqueAll_DISTINCT_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "distinct_unique_all",
				m_vRHSSymbols = new string[]
				{
					"'UNIQUE'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_DistinctUniqueAll_UNIQUE_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "select_list",
				m_vRHSSymbols = new string[]
				{
					"'*'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_SelectList_STAR_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "select_list",
				m_vRHSSymbols = new string[]
				{
					"select_term"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_SelectList_SelectTerm_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_SelectList_SelectList_COMMA_STAR_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_SelectList_SelectList_COMMA_SelectTerm_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "select_term",
				m_vRHSSymbols = new string[]
				{
					"expr"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_SelectTerm_Expr_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_SelectTerm_Identifier_DOT_STAR_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_SelectTerm_Identifier_DOT_Identifier_DOT_STAR_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "select_term",
				m_vRHSSymbols = new string[]
				{
					"\"aliased_expr\""
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_SelectTerm_AliasedExpr_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_AliasedExpr_Expr_AsAlias_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "table_reference_or_join_clause",
				m_vRHSSymbols = new string[]
				{
					"join_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_TableReferenceOrJoinClause_JoinClause_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "table_reference_or_join_clause",
				m_vRHSSymbols = new string[]
				{
					"table_reference"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_TableReferenceOrJoinClause_TableReference_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "table_reference_or_join_clause",
				m_vRHSSymbols = new string[]
				{
					"'('",
					"join_clause",
					"')'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_TableReferenceOrJoinClause_LEFT_PARENTHESIS_JoinClause_RIGHT_PARENTHESIS_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "table_reference",
				m_vRHSSymbols = new string[]
				{
					"table_reference#"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_TableReference_TableReferenceNN_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "table_reference#",
				m_vRHSSymbols = new string[]
				{
					"table_reference"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_TableReferenceNN_TableReference_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "table_reference#",
				m_vRHSSymbols = new string[]
				{
					"table_reference",
					"identifier"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_TableReferenceNN_TableReference_Identifier_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "table_reference",
				m_vRHSSymbols = new string[]
				{
					"table_reference"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_TableReference_TableReference_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "table_reference",
				m_vRHSSymbols = new string[]
				{
					"table_reference",
					"table_reference"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_TableReference_TableReference_TableReference_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "table_reference",
				m_vRHSSymbols = new string[]
				{
					"table_reference",
					"flashback_query_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_TableReference_TableReference_FlashbackQueryClause_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "table_reference",
				m_vRHSSymbols = new string[]
				{
					"table_reference",
					"flashback_query_clause",
					"table_reference"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_TableReference_TableReference_FlashbackQueryClause_TableReference_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "table_reference",
				m_vRHSSymbols = new string[]
				{
					"containers_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_TableReference_ContainersClause_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "table_reference",
				m_vRHSSymbols = new string[]
				{
					"pivot_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_TableReference_PivotClause_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "table_reference",
				m_vRHSSymbols = new string[]
				{
					"row_pattern_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_TableReference_RowPatternClause_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "table_reference",
				m_vRHSSymbols = new string[]
				{
					"unpivot_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_TableReference_UnpivotClause_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "table_reference",
				m_vRHSSymbols = new string[]
				{
					"query_table_expression"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_TableReference_QueryTableExpression_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "table_reference",
				m_vRHSSymbols = new string[]
				{
					"'ONLY'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_TableReference_ONLY_StartWithRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllRulesStartingWith
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "table_reference",
				m_vRHSSymbols = new string[]
				{
					"json_table"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_TableReference_JsonTable_StartWithRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllRulesStartingWith
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_ContainersClause_CONTAINERS_LEFT_PARENTHESIS_Identifier_RIGHT_PARENTHESIS_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_ContainersClause_CONTAINERS_LEFT_PARENTHESIS_Identifier_DOT_Identifier_RIGHT_PARENTHESIS_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "query_table_expression",
				m_vRHSSymbols = new string[]
				{
					"query_table_expression_named_object"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_QueryTableExpression_QueryTableExpressionNamedObject_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "query_table_expression",
				m_vRHSSymbols = new string[]
				{
					"query_table_expression_subquery"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_QueryTableExpression_QueryTableExpressionSubquery_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "query_table_expression",
				m_vRHSSymbols = new string[]
				{
					"table_collection_expression"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_QueryTableExpression_TableCollectionExpression_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "query_table_expression_named_object",
				m_vRHSSymbols = new string[]
				{
					"query_table_expression_named_object_t1"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_QueryTableExpressionNamedObject_QueryTableExpressionNamedObjectT1_StartWithRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllRulesStartingWith
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "query_table_expression_named_object",
				m_vRHSSymbols = new string[]
				{
					"sample_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_QueryTableExpressionNamedObject_SampleClause_EndWithRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllRulesEndingWith
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "query_table_expression_named_object_t1",
				m_vRHSSymbols = new string[]
				{
					"query_table_expression_named_object_t2"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_QueryTableExpressionNamedObjectT1_QueryTableExpressionNamedObjectT2_StartWithRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllRulesStartingWith
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "query_table_expression_named_object_t1",
				m_vRHSSymbols = new string[]
				{
					"query_table_expression_named_object_t3"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_QueryTableExpressionNamedObjectT1_QueryTableExpressionNamedObjectT3_EndWithRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllRulesEndingWith
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "query_table_expression_named_object_t2",
				m_vRHSSymbols = new string[]
				{
					"identifier"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_QueryTableExpressionNamedObjectT2_Identifier_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "query_table_expression_named_object_t2",
				m_vRHSSymbols = new string[]
				{
					"identifier",
					"'.'",
					"identifier"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_QueryTableExpressionNamedObjectT2_Identifier_DOT_Identifier_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "query_table_expression_named_object_t3",
				m_vRHSSymbols = new string[]
				{
					"'@'",
					"dblink"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_QueryTableExpressionNamedObjectT3_AT_DbLink_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "query_table_expression_named_object_t3",
				m_vRHSSymbols = new string[]
				{
					"partition_extension_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_QueryTableExpressionNamedObjectT3_PartitionExtensionClause_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "query_table_expression_subquery",
				m_vRHSSymbols = new string[]
				{
					"subquery"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_QueryTableExpressionSubquery_Subquery_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "query_table_expression_subquery",
				m_vRHSSymbols = new string[]
				{
					"subquery_restriction_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_QueryTableExpressionSubquery_SubqueryRestrictionClause_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "join_clause",
				m_vRHSSymbols = new string[]
				{
					"table_reference",
					"join_clause_list"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_JoinClause_TableReference_JoinClauseList_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "join_clause_list",
				m_vRHSSymbols = new string[]
				{
					"join_clause_term"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_JoinClauseList_JoinClauseTerm_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "join_clause_list",
				m_vRHSSymbols = new string[]
				{
					"join_clause_term",
					"join_clause_list"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_JoinClauseList_JoinClauseTerm_JoinClauseList_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "join_clause_term",
				m_vRHSSymbols = new string[]
				{
					"\"inner_cross_join_clause\""
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_JoinClauseTerm_InnerCrossJoinClause_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "join_clause_term",
				m_vRHSSymbols = new string[]
				{
					"outer_join_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_JoinClauseTerm_OuterJoinClause_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "join_clause_term",
				m_vRHSSymbols = new string[]
				{
					"\"cross_outer_apply_clause\""
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_JoinClauseTerm_CrossOuterApplyClause_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "\"inner_cross_join_clause\"",
				m_vRHSSymbols = new string[]
				{
					"\"inner_cross_join_clause\"",
					"'JOIN'",
					"table_reference"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_InnerCrossJoinClause_InnerCrossJoinClause_JOIN_TableReference_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_InnerCrossJoinClause_JOIN_TableReference_OnUsingCondition_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "\"inner_cross_join_clause\"",
				m_vRHSSymbols = new string[]
				{
					"'CROSS'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_InnerCrossJoinClause_CROSS_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "\"inner_cross_join_clause\"",
				m_vRHSSymbols = new string[]
				{
					"'NATURAL'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_InnerCrossJoinClause_NATURAL_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "\"inner_cross_join_clause\"",
				m_vRHSSymbols = new string[]
				{
					"'INNER'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_InnerCrossJoinClause_INNER_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "\"cross_outer_apply_clause\"",
				m_vRHSSymbols = new string[]
				{
					"\"cross_outer_apply_clause\"",
					"'APPLY'",
					"table_reference"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_CrossOuterApplyClause_CrossOuterApplyClause_APPLY_TableReference_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "\"cross_outer_apply_clause\"",
				m_vRHSSymbols = new string[]
				{
					"'CROSS'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_CrossOuterApplyClause_CROSS_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "\"cross_outer_apply_clause\"",
				m_vRHSSymbols = new string[]
				{
					"'OUTER'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_CrossOuterApplyClause_OUTER_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "outer_join_clause",
				m_vRHSSymbols = new string[]
				{
					"outer_join_clause",
					"table_reference"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_OuterJoinClause_OuterJoinClause_TableReference_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "outer_join_clause",
				m_vRHSSymbols = new string[]
				{
					"table_reference",
					"query_partition_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_OuterJoinClause_TableReference_QueryPartitionClause_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "outer_join_clause",
				m_vRHSSymbols = new string[]
				{
					"query_partition_clause"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_OuterJoinClause_QueryPartitionClause_StartWithRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllRulesStartingWith
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "outer_join_clause",
				m_vRHSSymbols = new string[]
				{
					"on_using_condition"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_OuterJoinClause_OnUsingCondition_PartialRule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_OuterJoinClause_OuterJoinType_JOIN_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_OuterJoinClause_NATURAL_OuterJoinType_JOIN_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "outer_join_type",
				m_vRHSSymbols = new string[]
				{
					"outer_join_type"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_OuterJoinType_OuterJoinType_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_OuterJoinType_OuterJoinType_OUTER_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "outer_join_type",
				m_vRHSSymbols = new string[]
				{
					"'FULL'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_OuterJoinType_FULL_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "outer_join_type",
				m_vRHSSymbols = new string[]
				{
					"'LEFT'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_OuterJoinType_LEFT_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "outer_join_type",
				m_vRHSSymbols = new string[]
				{
					"'RIGHT'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_OuterJoinType_RIGHT_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_OnUsingCondition_ON_Condition_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "on_using_condition",
				m_vRHSSymbols = new string[]
				{
					"'USING'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_OnUsingCondition_USING_StartWithRule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_OnUsingCondition_LEFT_PARENTHESIS_Column_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "on_using_condition",
				m_vRHSSymbols = new string[]
				{
					"on_using_condition"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_OnUsingCondition_OnUsingCondition_PartialRule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_OnUsingCondition_COMMA_Column_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "as_alias",
				m_vRHSSymbols = new string[]
				{
					"identifier"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_AsAlias_Identifier_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySelectRuleMultiProcessors.Process_AsAlias_AS_Identifier_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			}
		};
	}
}
