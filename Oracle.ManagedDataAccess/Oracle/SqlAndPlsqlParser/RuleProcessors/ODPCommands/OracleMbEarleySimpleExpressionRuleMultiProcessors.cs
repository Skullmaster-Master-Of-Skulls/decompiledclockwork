using System;
using System.Collections.Generic;
using Oracle.SqlAndPlsqlParser.LocalParsing;

namespace Oracle.SqlAndPlsqlParser.RuleProcessors.ODPCommands
{
	// Token: 0x02000311 RID: 785
	internal static class OracleMbEarleySimpleExpressionRuleMultiProcessors
	{
		// Token: 0x06001CC2 RID: 7362 RVA: 0x0011AF30 File Offset: 0x00119130
		public static object Process_SimpleExpression_CONNECT_BY_ISCYCLE_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return new OracleLpPseudoColumnExpression(null)
			{
				PseudoColumnExpressionType = OracleLpPseudoColumnExpressionType.CONNECT_BY_ISCYCLE
			};
		}

		// Token: 0x06001CC3 RID: 7363 RVA: 0x0011AF4C File Offset: 0x0011914C
		public static object Process_SimpleExpression_CONNECT_BY_ISLEAF_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return new OracleLpPseudoColumnExpression(null)
			{
				PseudoColumnExpressionType = OracleLpPseudoColumnExpressionType.CONNECT_BY_ISLEAF
			};
		}

		// Token: 0x06001CC4 RID: 7364 RVA: 0x0011AF68 File Offset: 0x00119168
		public static object Process_SimpleExpression_NULL_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return new OracleLpSimpleExpression(null)
			{
				SimpleExpressionType = OracleLpSimpleExpressionType.NULL
			};
		}

		// Token: 0x06001CC5 RID: 7365 RVA: 0x0011AF84 File Offset: 0x00119184
		public static object Process_SimpleExpression_ROWID_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return new OracleLpPseudoColumnExpression(null)
			{
				PseudoColumnExpressionType = OracleLpPseudoColumnExpressionType.ROWID
			};
		}

		// Token: 0x06001CC6 RID: 7366 RVA: 0x0011AFA0 File Offset: 0x001191A0
		public static object Process_SimpleExpression_Identifier_DOT_ROWID_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpPseudoColumnExpression oracleLpPseudoColumnExpression = new OracleLpPseudoColumnExpression(null);
			oracleLpPseudoColumnExpression.PseudoColumnExpressionType = OracleLpPseudoColumnExpressionType.ROWID;
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			oracleLpPseudoColumnExpression.ParentObjectName = new OracleLpName(ctx.Tokens[list[0].From].m_vContent);
			return oracleLpPseudoColumnExpression;
		}

		// Token: 0x06001CC7 RID: 7367 RVA: 0x0011AFF0 File Offset: 0x001191F0
		public static object Process_SimpleExpression_Identifier_DOT_Identifier_DOT_ROWID_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpPseudoColumnExpression oracleLpPseudoColumnExpression = new OracleLpPseudoColumnExpression(null);
			oracleLpPseudoColumnExpression.PseudoColumnExpressionType = OracleLpPseudoColumnExpressionType.ROWID;
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			oracleLpPseudoColumnExpression.SchemaName = new OracleLpName(ctx.Tokens[list[0].From].m_vContent);
			oracleLpPseudoColumnExpression.ParentObjectName = new OracleLpName(ctx.Tokens[list[2].From].m_vContent);
			return oracleLpPseudoColumnExpression;
		}

		// Token: 0x06001CC8 RID: 7368 RVA: 0x0011B068 File Offset: 0x00119268
		public static object Process_SimpleExpression_ROWNUM_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return new OracleLpPseudoColumnExpression(null)
			{
				PseudoColumnExpressionType = OracleLpPseudoColumnExpressionType.ROWNUM
			};
		}

		// Token: 0x06001CC9 RID: 7369 RVA: 0x0011B084 File Offset: 0x00119284
		public static object Process_SimpleExpression_ColOj_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return new OracleLpColumnExpression(null)
			{
				Plus = true,
				Column = (OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx) as OracleLpColumn)
			};
		}

		// Token: 0x06001CCA RID: 7370 RVA: 0x0011B0C0 File Offset: 0x001192C0
		public static object Process_SimpleExpression_Column_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpColumnExpression oracleLpColumnExpression = new OracleLpColumnExpression(null);
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("column");
			oracleLpColumnExpression.Column = (OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx) as OracleLpColumn);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return oracleLpColumnExpression;
		}

		// Token: 0x06001CCB RID: 7371 RVA: 0x0011B114 File Offset: 0x00119314
		public static object Process_SimpleExpression_Literal_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpConstantExpression oracleLpConstantExpression = new OracleLpConstantExpression(null);
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("literal");
			object expressionValue = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, 0, -1, ctx);
			ctx.RuleProcessorTable = ruleProcessorTable;
			oracleLpConstantExpression.ExpressionValue = expressionValue;
			return oracleLpConstantExpression;
		}

		// Token: 0x06001CCC RID: 7372 RVA: 0x0011B160 File Offset: 0x00119360
		public static object Process_SimpleExpression_CONNECT_BY_ROOT_Expr_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpPseudoColumnExpression oracleLpPseudoColumnExpression = new OracleLpPseudoColumnExpression(null);
			oracleLpPseudoColumnExpression.PseudoColumnExpressionType = OracleLpPseudoColumnExpressionType.CONNECT_BY_ROOT;
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			oracleLpPseudoColumnExpression.Expression = (OracleLpExpression)OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
			return oracleLpPseudoColumnExpression;
		}

		// Token: 0x06001CCD RID: 7373 RVA: 0x0011B1A4 File Offset: 0x001193A4
		public static object Process_SimpleExpression_Identifier_DOT_SimpleExpression_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpSequenceExpression oracleLpSequenceExpression = new OracleLpSequenceExpression(null);
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			oracleLpSequenceExpression.SequenceName = ctx.Tokens[list[0].From].m_vContent;
			oracleLpSequenceExpression.SequenceExpressionType = (OracleLpSequenceExpressionType)OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[2], 0, -1, ctx);
			return oracleLpSequenceExpression;
		}

		// Token: 0x06001CCE RID: 7374 RVA: 0x0011B204 File Offset: 0x00119404
		public static object Process_SimpleExpression_Identifier_DOT_Identifier_DOT_SimpleExpression_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpSequenceExpression oracleLpSequenceExpression = new OracleLpSequenceExpression(null);
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			oracleLpSequenceExpression.SchemaName = ctx.Tokens[list[0].From].m_vContent;
			oracleLpSequenceExpression.SequenceName = ctx.Tokens[list[2].From].m_vContent;
			oracleLpSequenceExpression.SequenceExpressionType = (OracleLpSequenceExpressionType)OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[4], 0, -1, ctx);
			return oracleLpSequenceExpression;
		}

		// Token: 0x06001CCF RID: 7375 RVA: 0x0011B284 File Offset: 0x00119484
		public static object Process_SimpleExpression_CURRVAL_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleLpSequenceExpressionType.CURRVAL;
		}

		// Token: 0x06001CD0 RID: 7376 RVA: 0x0011B28C File Offset: 0x0011948C
		public static object Process_SimpleExpression_NEXTVAL_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleLpSequenceExpressionType.NEXTVAL;
		}

		// Token: 0x06001CD1 RID: 7377 RVA: 0x0011B294 File Offset: 0x00119494
		public static object Process_ColOj_Column_LEFT_PARENTHESIS_PLUS_RIGHT_PARENTHESIS_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("column");
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x04001D66 RID: 7526
		public static OracleMbEarleyRuleMultiProcessorAddItem[] s_vRuleProcessorItems = new OracleMbEarleyRuleMultiProcessorAddItem[]
		{
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "simple_expression",
				m_vRHSSymbols = new string[]
				{
					"'CONNECT_BY_ISCYCLE'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySimpleExpressionRuleMultiProcessors.Process_SimpleExpression_CONNECT_BY_ISCYCLE_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "simple_expression",
				m_vRHSSymbols = new string[]
				{
					"'CONNECT_BY_ISLEAF'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySimpleExpressionRuleMultiProcessors.Process_SimpleExpression_CONNECT_BY_ISLEAF_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "simple_expression",
				m_vRHSSymbols = new string[]
				{
					"'NULL'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySimpleExpressionRuleMultiProcessors.Process_SimpleExpression_NULL_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "simple_expression",
				m_vRHSSymbols = new string[]
				{
					"'ROWID'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySimpleExpressionRuleMultiProcessors.Process_SimpleExpression_ROWID_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "simple_expression",
				m_vRHSSymbols = new string[]
				{
					"identifier",
					"'.'",
					"'ROWID'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySimpleExpressionRuleMultiProcessors.Process_SimpleExpression_Identifier_DOT_ROWID_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "simple_expression",
				m_vRHSSymbols = new string[]
				{
					"identifier",
					"'.'",
					"identifier",
					"'.'",
					"'ROWID'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySimpleExpressionRuleMultiProcessors.Process_SimpleExpression_Identifier_DOT_Identifier_DOT_ROWID_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "simple_expression",
				m_vRHSSymbols = new string[]
				{
					"'ROWNUM'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySimpleExpressionRuleMultiProcessors.Process_SimpleExpression_ROWNUM_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "simple_expression",
				m_vRHSSymbols = new string[]
				{
					"col_oj"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySimpleExpressionRuleMultiProcessors.Process_SimpleExpression_ColOj_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "simple_expression",
				m_vRHSSymbols = new string[]
				{
					"column"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySimpleExpressionRuleMultiProcessors.Process_SimpleExpression_Column_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "simple_expression",
				m_vRHSSymbols = new string[]
				{
					"literal"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySimpleExpressionRuleMultiProcessors.Process_SimpleExpression_Literal_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "simple_expression",
				m_vRHSSymbols = new string[]
				{
					"'CONNECT_BY_ROOT'",
					"expr"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySimpleExpressionRuleMultiProcessors.Process_SimpleExpression_CONNECT_BY_ROOT_Expr_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "simple_expression",
				m_vRHSSymbols = new string[]
				{
					"identifier",
					"'.'",
					"simple_expression"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySimpleExpressionRuleMultiProcessors.Process_SimpleExpression_Identifier_DOT_SimpleExpression_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "simple_expression",
				m_vRHSSymbols = new string[]
				{
					"identifier",
					"'.'",
					"identifier",
					"'.'",
					"simple_expression"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySimpleExpressionRuleMultiProcessors.Process_SimpleExpression_Identifier_DOT_Identifier_DOT_SimpleExpression_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "simple_expression",
				m_vRHSSymbols = new string[]
				{
					"'CURRVAL'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySimpleExpressionRuleMultiProcessors.Process_SimpleExpression_CURRVAL_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "simple_expression",
				m_vRHSSymbols = new string[]
				{
					"'NEXTVAL'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySimpleExpressionRuleMultiProcessors.Process_SimpleExpression_NEXTVAL_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "col_oj",
				m_vRHSSymbols = new string[]
				{
					"column",
					"'('",
					"'+'",
					"')'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySimpleExpressionRuleMultiProcessors.Process_ColOj_Column_LEFT_PARENTHESIS_PLUS_RIGHT_PARENTHESIS_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			}
		};
	}
}
