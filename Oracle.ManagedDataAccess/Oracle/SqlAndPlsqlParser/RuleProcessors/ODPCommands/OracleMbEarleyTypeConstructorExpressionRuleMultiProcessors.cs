using System;
using System.Collections.Generic;
using Oracle.SqlAndPlsqlParser.LocalParsing;

namespace Oracle.SqlAndPlsqlParser.RuleProcessors.ODPCommands
{
	// Token: 0x02000314 RID: 788
	internal static class OracleMbEarleyTypeConstructorExpressionRuleMultiProcessors
	{
		// Token: 0x06001CF5 RID: 7413 RVA: 0x0011CB90 File Offset: 0x0011AD90
		public static object Process_TypeConstructorExpression_Identifier_LeftParenthesis_StartWithRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpTypeConstructorExpression oracleLpTypeConstructorExpression = new OracleLpTypeConstructorExpression(null);
			OracleLpExpression ao = (OracleLpExpression)ctx.GetActiveObject(11);
			ctx.SetActiveObject(11, oracleLpTypeConstructorExpression);
			oracleLpTypeConstructorExpression.Name = new OracleLpName(ctx.Tokens[list[0].From].m_vContent);
			if (list.Count > 3)
			{
				OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[2], 0, -1, ctx);
			}
			ctx.SetActiveObject(11, ao);
			return oracleLpTypeConstructorExpression;
		}

		// Token: 0x06001CF6 RID: 7414 RVA: 0x0011CC14 File Offset: 0x0011AE14
		public static object Process_TypeConstructorExpression_Identifier_Dot_Identifier_LeftParenthesis_StartWithRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpTypeConstructorExpression oracleLpTypeConstructorExpression = new OracleLpTypeConstructorExpression(null);
			OracleLpExpression ao = (OracleLpExpression)ctx.GetActiveObject(11);
			ctx.SetActiveObject(11, oracleLpTypeConstructorExpression);
			oracleLpTypeConstructorExpression.ParentObjectName = new OracleLpName(ctx.Tokens[list[0].From].m_vContent);
			oracleLpTypeConstructorExpression.Name = new OracleLpName(ctx.Tokens[list[2].From].m_vContent);
			if (list.Count > 5)
			{
				OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[4], 0, -1, ctx);
			}
			ctx.SetActiveObject(11, ao);
			return oracleLpTypeConstructorExpression;
		}

		// Token: 0x06001CF7 RID: 7415 RVA: 0x0011CCBC File Offset: 0x0011AEBC
		public static object Process_TypeConstructorExpression_New_Identifier_LeftParenthesis_StartWithRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpTypeConstructorExpression oracleLpTypeConstructorExpression = new OracleLpTypeConstructorExpression(null);
			OracleLpExpression ao = (OracleLpExpression)ctx.GetActiveObject(11);
			ctx.SetActiveObject(11, oracleLpTypeConstructorExpression);
			oracleLpTypeConstructorExpression.Name = new OracleLpName(ctx.Tokens[list[1].From].m_vContent);
			if (5 == list.Count)
			{
				OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[3], 0, -1, ctx);
			}
			ctx.SetActiveObject(11, ao);
			return oracleLpTypeConstructorExpression;
		}

		// Token: 0x06001CF8 RID: 7416 RVA: 0x0011CD40 File Offset: 0x0011AF40
		public static object Process_TypeConstructorExpression_New_Identifier_Dot_Identifier_LeftParenthesis_StartWithRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpTypeConstructorExpression oracleLpTypeConstructorExpression = new OracleLpTypeConstructorExpression(null);
			OracleLpExpression ao = (OracleLpExpression)ctx.GetActiveObject(11);
			ctx.SetActiveObject(11, oracleLpTypeConstructorExpression);
			oracleLpTypeConstructorExpression.ParentObjectName = new OracleLpName(ctx.Tokens[list[1].From].m_vContent);
			oracleLpTypeConstructorExpression.Name = new OracleLpName(ctx.Tokens[list[3].From].m_vContent);
			if (7 == list.Count)
			{
				OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[5], 0, -1, ctx);
			}
			ctx.SetActiveObject(11, ao);
			return oracleLpTypeConstructorExpression;
		}

		// Token: 0x06001CF9 RID: 7417 RVA: 0x0011CDE8 File Offset: 0x0011AFE8
		public static object Process_TypeConstructorExpression_Expr_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("expr");
			OracleLpExpression oracleLpExpression = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx) as OracleLpExpression;
			ctx.RuleProcessorTable = ruleProcessorTable;
			List<LexerToken> tokens = ctx.Tokens;
			ParseNode currentParseNode = ctx.CurrentParseNode;
			int vBegin = tokens[currentParseNode.From].m_vBegin;
			int vEnd = tokens[currentParseNode.To - 1].m_vEnd;
			oracleLpExpression.Text = new OracleLpTextFragment(ctx.Script, vBegin, vEnd - vBegin);
			OracleLpTypeConstructorExpression oracleLpTypeConstructorExpression = ctx.GetActiveObject(11) as OracleLpTypeConstructorExpression;
			if (oracleLpTypeConstructorExpression.Parameters == null)
			{
				oracleLpTypeConstructorExpression.CreateParametersList();
			}
			oracleLpTypeConstructorExpression.Parameters.Add(oracleLpExpression);
			oracleLpExpression.ParentExpression = oracleLpTypeConstructorExpression;
			oracleLpTypeConstructorExpression.ParametersChanged();
			return oracleLpTypeConstructorExpression;
		}

		// Token: 0x06001CFA RID: 7418 RVA: 0x0011CEBC File Offset: 0x0011B0BC
		public static object Process_TypeConstructorExpression_Expr_TypeConstructorExpression_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("expr");
			OracleLpExpression oracleLpExpression = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx) as OracleLpExpression;
			ctx.RuleProcessorTable = ruleProcessorTable;
			List<LexerToken> tokens = ctx.Tokens;
			ParseNode currentParseNode = ctx.CurrentParseNode;
			int vBegin = tokens[currentParseNode.From].m_vBegin;
			int vEnd = tokens[currentParseNode.To - 1].m_vEnd;
			oracleLpExpression.Text = new OracleLpTextFragment(ctx.Script, vBegin, vEnd - vBegin);
			OracleLpTypeConstructorExpression oracleLpTypeConstructorExpression = ctx.GetActiveObject(11) as OracleLpTypeConstructorExpression;
			if (oracleLpTypeConstructorExpression.Parameters == null)
			{
				oracleLpTypeConstructorExpression.CreateParametersList();
			}
			oracleLpTypeConstructorExpression.Parameters.Add(oracleLpExpression);
			oracleLpExpression.ParentExpression = oracleLpTypeConstructorExpression;
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
			oracleLpTypeConstructorExpression.ParametersChanged();
			return oracleLpTypeConstructorExpression;
		}

		// Token: 0x06001CFB RID: 7419 RVA: 0x0011CFA8 File Offset: 0x0011B1A8
		public static object Process_TypeConstructorExpression_Comma_Expr_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("expr");
			OracleLpExpression oracleLpExpression = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx) as OracleLpExpression;
			ctx.RuleProcessorTable = ruleProcessorTable;
			List<LexerToken> tokens = ctx.Tokens;
			ParseNode currentParseNode = ctx.CurrentParseNode;
			int vBegin = tokens[currentParseNode.From].m_vBegin;
			int vEnd = tokens[currentParseNode.To - 1].m_vEnd;
			oracleLpExpression.Text = new OracleLpTextFragment(ctx.Script, vBegin, vEnd - vBegin);
			OracleLpTypeConstructorExpression oracleLpTypeConstructorExpression = ctx.GetActiveObject(11) as OracleLpTypeConstructorExpression;
			if (oracleLpTypeConstructorExpression.Parameters == null)
			{
				oracleLpTypeConstructorExpression.CreateParametersList();
			}
			oracleLpTypeConstructorExpression.Parameters.Add(oracleLpExpression);
			oracleLpExpression.ParentExpression = oracleLpTypeConstructorExpression;
			oracleLpTypeConstructorExpression.ParametersChanged();
			return oracleLpTypeConstructorExpression;
		}

		// Token: 0x06001CFC RID: 7420 RVA: 0x0011D084 File Offset: 0x0011B284
		public static object Process_TypeConstructorExpression_TypeConstructorExpression_Comma_Expr_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx);
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("expr");
			OracleLpExpression oracleLpExpression = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[2], 0, -1, ctx) as OracleLpExpression;
			ctx.RuleProcessorTable = ruleProcessorTable;
			List<LexerToken> tokens = ctx.Tokens;
			ParseNode currentParseNode = ctx.CurrentParseNode;
			int vBegin = tokens[currentParseNode.From].m_vBegin;
			int vEnd = tokens[currentParseNode.To - 1].m_vEnd;
			oracleLpExpression.Text = new OracleLpTextFragment(ctx.Script, vBegin, vEnd - vBegin);
			OracleLpTypeConstructorExpression oracleLpTypeConstructorExpression = ctx.GetActiveObject(11) as OracleLpTypeConstructorExpression;
			if (oracleLpTypeConstructorExpression.Parameters == null)
			{
				oracleLpTypeConstructorExpression.CreateParametersList();
			}
			oracleLpTypeConstructorExpression.Parameters.Add(oracleLpExpression);
			oracleLpExpression.ParentExpression = oracleLpTypeConstructorExpression;
			oracleLpTypeConstructorExpression.ParametersChanged();
			return oracleLpTypeConstructorExpression;
		}

		// Token: 0x04001D69 RID: 7529
		public static OracleMbEarleyRuleMultiProcessorAddItem[] s_vRuleProcessorItems = new OracleMbEarleyRuleMultiProcessorAddItem[]
		{
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "type_constructor_expression",
				m_vRHSSymbols = new string[]
				{
					"identifier",
					"'('"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyTypeConstructorExpressionRuleMultiProcessors.Process_TypeConstructorExpression_Identifier_LeftParenthesis_StartWithRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllRulesStartingWith
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "type_constructor_expression",
				m_vRHSSymbols = new string[]
				{
					"identifier",
					"'.'",
					"identifier",
					"'('"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyTypeConstructorExpressionRuleMultiProcessors.Process_TypeConstructorExpression_Identifier_Dot_Identifier_LeftParenthesis_StartWithRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllRulesStartingWith
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "type_constructor_expression",
				m_vRHSSymbols = new string[]
				{
					"'NEW'",
					"identifier",
					"'('"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyTypeConstructorExpressionRuleMultiProcessors.Process_TypeConstructorExpression_New_Identifier_LeftParenthesis_StartWithRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllRulesStartingWith
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "type_constructor_expression",
				m_vRHSSymbols = new string[]
				{
					"'NEW'",
					"identifier",
					"'.'",
					"identifier",
					"'('"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyTypeConstructorExpressionRuleMultiProcessors.Process_TypeConstructorExpression_New_Identifier_Dot_Identifier_LeftParenthesis_StartWithRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllRulesStartingWith
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "type_constructor_expression",
				m_vRHSSymbols = new string[]
				{
					"expr"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyTypeConstructorExpressionRuleMultiProcessors.Process_TypeConstructorExpression_Expr_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "type_constructor_expression",
				m_vRHSSymbols = new string[]
				{
					"expr",
					"type_constructor_expression"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyTypeConstructorExpressionRuleMultiProcessors.Process_TypeConstructorExpression_Expr_TypeConstructorExpression_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "type_constructor_expression",
				m_vRHSSymbols = new string[]
				{
					"','",
					"expr"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyTypeConstructorExpressionRuleMultiProcessors.Process_TypeConstructorExpression_Comma_Expr_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "type_constructor_expression",
				m_vRHSSymbols = new string[]
				{
					"type_constructor_expression",
					"','",
					"expr"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyTypeConstructorExpressionRuleMultiProcessors.Process_TypeConstructorExpression_TypeConstructorExpression_Comma_Expr_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			}
		};
	}
}
