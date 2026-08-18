using System;
using System.Collections.Generic;
using Oracle.SqlAndPlsqlParser.LocalParsing;

namespace Oracle.SqlAndPlsqlParser.RuleProcessors.ODPCommands
{
	// Token: 0x02000308 RID: 776
	internal static class OracleMbEarleyConditionRuleMultiProcessors
	{
		// Token: 0x06001B96 RID: 7062 RVA: 0x0010EFF0 File Offset: 0x0010D1F0
		public static object Process_Condition_ComparisonCondition_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001B97 RID: 7063 RVA: 0x0010F008 File Offset: 0x0010D208
		public static object Process_Condition_CompoundCondition_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001B98 RID: 7064 RVA: 0x0010F020 File Offset: 0x0010D220
		public static object Process_Condition_ExistsCondition_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return null;
		}

		// Token: 0x06001B99 RID: 7065 RVA: 0x0010F030 File Offset: 0x0010D230
		public static object Process_Condition_FloatingPointCondition_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return null;
		}

		// Token: 0x06001B9A RID: 7066 RVA: 0x0010F040 File Offset: 0x0010D240
		public static object Process_Condition_InCondition_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return null;
		}

		// Token: 0x06001B9B RID: 7067 RVA: 0x0010F050 File Offset: 0x0010D250
		public static object Process_Condition_IsOfTypeCondition_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return null;
		}

		// Token: 0x06001B9C RID: 7068 RVA: 0x0010F060 File Offset: 0x0010D260
		public static object Process_Condition_LikeCondition_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return null;
		}

		// Token: 0x06001B9D RID: 7069 RVA: 0x0010F070 File Offset: 0x0010D270
		public static object Process_Condition_LogicalCondition_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return null;
		}

		// Token: 0x06001B9E RID: 7070 RVA: 0x0010F080 File Offset: 0x0010D280
		public static object Process_Condition_ModelCondition_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return null;
		}

		// Token: 0x06001B9F RID: 7071 RVA: 0x0010F090 File Offset: 0x0010D290
		public static object Process_Condition_MultisetCondition_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return null;
		}

		// Token: 0x06001BA0 RID: 7072 RVA: 0x0010F0A0 File Offset: 0x0010D2A0
		public static object Process_Condition_NullCondition_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return null;
		}

		// Token: 0x06001BA1 RID: 7073 RVA: 0x0010F0B0 File Offset: 0x0010D2B0
		public static object Process_Condition_RangeCondition_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return null;
		}

		// Token: 0x06001BA2 RID: 7074 RVA: 0x0010F0C0 File Offset: 0x0010D2C0
		public static object Process_Condition_Xmlexists_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return null;
		}

		// Token: 0x06001BA3 RID: 7075 RVA: 0x0010F0D0 File Offset: 0x0010D2D0
		public static object Process_ComparisonCondition_BetweenCondition_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return null;
		}

		// Token: 0x06001BA4 RID: 7076 RVA: 0x0010F0E0 File Offset: 0x0010D2E0
		public static object Process_ComparisonCondition_GroupComparisonCondition_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return null;
		}

		// Token: 0x06001BA5 RID: 7077 RVA: 0x0010F0F0 File Offset: 0x0010D2F0
		public static object Process_ComparisonCondition_SimpleComparisonCondition_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001BA6 RID: 7078 RVA: 0x0010F108 File Offset: 0x0010D308
		public static object Process_SimpleComparisonCondition_Expr_SimpleComparisonCondition_Expr_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpCondition ao = (OracleLpCondition)ctx.GetActiveObject(9);
			OracleLpSimpleComparisionCondition oracleLpSimpleComparisionCondition = new OracleLpSimpleComparisionCondition(ctx.GetStatementBetweenGivenSrcAndTgtTokenIdx(ctx.CurrentParseNode.From, ctx.CurrentParseNode.To - 1));
			ctx.SetActiveObject(9, oracleLpSimpleComparisionCondition);
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			oracleLpSimpleComparisionCondition.LeftOperand = new OracleLpCondition(ctx.GetStatementBetweenGivenSrcAndTgtTokenIdx(list[0].From, list[0].To - 1))
			{
				ConditionType = OracleLpConditionType.ConditionString
			};
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
			oracleLpSimpleComparisionCondition.RightOperand = new OracleLpCondition(ctx.GetStatementBetweenGivenSrcAndTgtTokenIdx(list[2].From, list[2].To - 1))
			{
				ConditionType = OracleLpConditionType.ConditionString
			};
			ctx.SetActiveObject(9, ao);
			return oracleLpSimpleComparisionCondition;
		}

		// Token: 0x06001BA7 RID: 7079 RVA: 0x0010F1E4 File Offset: 0x0010D3E4
		public static object Process_SimpleComparisonCondition_LEFT_PARENTHESIS_ExprList_RIGHT_PARENTHESIS_CmpOp_ScalarSubqueryExpression_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return null;
		}

		// Token: 0x06001BA8 RID: 7080 RVA: 0x0010F1F4 File Offset: 0x0010D3F4
		public static object Process_SimpleComparisonCondition_LESSTHAN_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpCondition oracleLpCondition = (OracleLpCondition)ctx.GetActiveObject(9);
			oracleLpCondition.Operation = OracleLpConditionOperation.LESSTHAN;
			return OracleLpConditionOperation.LESSTHAN;
		}

		// Token: 0x06001BA9 RID: 7081 RVA: 0x0010F21C File Offset: 0x0010D41C
		public static object Process_SimpleComparisonCondition_GREATERTHAN_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpCondition oracleLpCondition = (OracleLpCondition)ctx.GetActiveObject(9);
			oracleLpCondition.Operation = OracleLpConditionOperation.GREATERTHAN;
			return OracleLpConditionOperation.GREATERTHAN;
		}

		// Token: 0x06001BAA RID: 7082 RVA: 0x0010F244 File Offset: 0x0010D444
		public static object Process_SimpleComparisonCondition_CmpOp_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001BAB RID: 7083 RVA: 0x0010F25C File Offset: 0x0010D45C
		public static object Process_SimpleComparisonCondition_LESSTHAN_EQUAL_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpCondition oracleLpCondition = (OracleLpCondition)ctx.GetActiveObject(9);
			oracleLpCondition.Operation = OracleLpConditionOperation.LESSTHANEQUAL;
			return OracleLpConditionOperation.LESSTHANEQUAL;
		}

		// Token: 0x06001BAC RID: 7084 RVA: 0x0010F284 File Offset: 0x0010D484
		public static object Process_SimpleComparisonCondition_GREATERTHAN_EQUAL_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpCondition oracleLpCondition = (OracleLpCondition)ctx.GetActiveObject(9);
			oracleLpCondition.Operation = OracleLpConditionOperation.GREATERTHANEQUAL;
			return OracleLpConditionOperation.GREATERTHANEQUAL;
		}

		// Token: 0x06001BAD RID: 7085 RVA: 0x0010F2AC File Offset: 0x0010D4AC
		public static object Process_CmpOp_EQUAL_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpCondition oracleLpCondition = (OracleLpCondition)ctx.GetActiveObject(9);
			oracleLpCondition.Operation = OracleLpConditionOperation.EQUAL;
			return OracleLpConditionOperation.EQUAL;
		}

		// Token: 0x06001BAE RID: 7086 RVA: 0x0010F2D4 File Offset: 0x0010D4D4
		public static object Process_CmpOp_NotEq_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001BAF RID: 7087 RVA: 0x0010F2EC File Offset: 0x0010D4EC
		public static object Process_CmpOp_NOTEQUAL_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return null;
		}

		// Token: 0x06001BB0 RID: 7088 RVA: 0x0010F2FC File Offset: 0x0010D4FC
		public static object Process_CmpOp_EXP_EQUAL_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return null;
		}

		// Token: 0x06001BB1 RID: 7089 RVA: 0x0010F30C File Offset: 0x0010D50C
		public static object Process_NotEq_LESSTHAN_GREATERTHAN_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return null;
		}

		// Token: 0x06001BB2 RID: 7090 RVA: 0x0010F31C File Offset: 0x0010D51C
		public static object Process_CompoundCondition_NOT_Condition_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return null;
		}

		// Token: 0x06001BB3 RID: 7091 RVA: 0x0010F32C File Offset: 0x0010D52C
		public static object Process_CompoundCondition_LEFT_PARENTHESIS_Condition_RIGHT_PARENTHESIS_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpCondition ao = (OracleLpCondition)ctx.GetActiveObject(9);
			OracleLpCompoundCondition oracleLpCompoundCondition = new OracleLpCompoundCondition(ctx.GetStatementBetweenGivenSrcAndTgtTokenIdx(ctx.CurrentParseNode.From, ctx.CurrentParseNode.To - 1));
			ctx.SetActiveObject(9, oracleLpCompoundCondition);
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			oracleLpCompoundCondition.LeftOperand = (OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx) as OracleLpCondition);
			oracleLpCompoundCondition.RightOperand = null;
			ctx.SetActiveObject(9, ao);
			return oracleLpCompoundCondition;
		}

		// Token: 0x06001BB4 RID: 7092 RVA: 0x0010F3AC File Offset: 0x0010D5AC
		public static object Process_CompoundCondition_Condition_ANDOR_Condition_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpCondition ao = (OracleLpCondition)ctx.GetActiveObject(9);
			OracleLpCompoundCondition oracleLpCompoundCondition = new OracleLpCompoundCondition(ctx.GetStatementBetweenGivenSrcAndTgtTokenIdx(ctx.CurrentParseNode.From, ctx.CurrentParseNode.To - 1));
			ctx.SetActiveObject(9, oracleLpCompoundCondition);
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			oracleLpCompoundCondition.LeftOperand = (OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx) as OracleLpCondition);
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
			oracleLpCompoundCondition.RightOperand = (OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[2], 0, -1, ctx) as OracleLpCondition);
			ctx.SetActiveObject(9, ao);
			return oracleLpCompoundCondition;
		}

		// Token: 0x06001BB5 RID: 7093 RVA: 0x0010F450 File Offset: 0x0010D650
		public static object Process_ANDOR_AND_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpCondition oracleLpCondition = (OracleLpCondition)ctx.GetActiveObject(9);
			oracleLpCondition.Operation = OracleLpConditionOperation.AND;
			return OracleLpConditionOperation.AND;
		}

		// Token: 0x06001BB6 RID: 7094 RVA: 0x0010F478 File Offset: 0x0010D678
		public static object Process_ANDOR_OR_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpCondition oracleLpCondition = (OracleLpCondition)ctx.GetActiveObject(9);
			oracleLpCondition.Operation = OracleLpConditionOperation.OR;
			return OracleLpConditionOperation.OR;
		}

		// Token: 0x04001D5D RID: 7517
		public static OracleMbEarleyRuleMultiProcessorAddItem[] s_vRuleProcessorItems = new OracleMbEarleyRuleMultiProcessorAddItem[]
		{
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "condition",
				m_vRHSSymbols = new string[]
				{
					"comparison_condition"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyConditionRuleMultiProcessors.Process_Condition_ComparisonCondition_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "condition",
				m_vRHSSymbols = new string[]
				{
					"compound_condition"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyConditionRuleMultiProcessors.Process_Condition_CompoundCondition_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "condition",
				m_vRHSSymbols = new string[]
				{
					"exists_condition"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyConditionRuleMultiProcessors.Process_Condition_ExistsCondition_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "condition",
				m_vRHSSymbols = new string[]
				{
					"floating_point_condition"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyConditionRuleMultiProcessors.Process_Condition_FloatingPointCondition_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "condition",
				m_vRHSSymbols = new string[]
				{
					"in_condition"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyConditionRuleMultiProcessors.Process_Condition_InCondition_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "condition",
				m_vRHSSymbols = new string[]
				{
					"is_of_type_condition"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyConditionRuleMultiProcessors.Process_Condition_IsOfTypeCondition_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "condition",
				m_vRHSSymbols = new string[]
				{
					"like_condition"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyConditionRuleMultiProcessors.Process_Condition_LikeCondition_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "condition",
				m_vRHSSymbols = new string[]
				{
					"logical_condition"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyConditionRuleMultiProcessors.Process_Condition_LogicalCondition_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "condition",
				m_vRHSSymbols = new string[]
				{
					"model_condition"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyConditionRuleMultiProcessors.Process_Condition_ModelCondition_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "condition",
				m_vRHSSymbols = new string[]
				{
					"multiset_condition"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyConditionRuleMultiProcessors.Process_Condition_MultisetCondition_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "condition",
				m_vRHSSymbols = new string[]
				{
					"null_condition"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyConditionRuleMultiProcessors.Process_Condition_NullCondition_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "condition",
				m_vRHSSymbols = new string[]
				{
					"range_condition"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyConditionRuleMultiProcessors.Process_Condition_RangeCondition_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "condition",
				m_vRHSSymbols = new string[]
				{
					"xmlexists"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyConditionRuleMultiProcessors.Process_Condition_Xmlexists_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "comparison_condition",
				m_vRHSSymbols = new string[]
				{
					"between_condition"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyConditionRuleMultiProcessors.Process_ComparisonCondition_BetweenCondition_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "comparison_condition",
				m_vRHSSymbols = new string[]
				{
					"group_comparison_condition"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyConditionRuleMultiProcessors.Process_ComparisonCondition_GroupComparisonCondition_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "comparison_condition",
				m_vRHSSymbols = new string[]
				{
					"simple_comparison_condition"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyConditionRuleMultiProcessors.Process_ComparisonCondition_SimpleComparisonCondition_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "simple_comparison_condition",
				m_vRHSSymbols = new string[]
				{
					"expr",
					"simple_comparison_condition",
					"expr"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyConditionRuleMultiProcessors.Process_SimpleComparisonCondition_Expr_SimpleComparisonCondition_Expr_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "simple_comparison_condition",
				m_vRHSSymbols = new string[]
				{
					"'('",
					"\"expr_list\"",
					"')'",
					"cmp_op",
					"scalar_subquery_expression"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyConditionRuleMultiProcessors.Process_SimpleComparisonCondition_LEFT_PARENTHESIS_ExprList_RIGHT_PARENTHESIS_CmpOp_ScalarSubqueryExpression_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "simple_comparison_condition",
				m_vRHSSymbols = new string[]
				{
					"'<'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyConditionRuleMultiProcessors.Process_SimpleComparisonCondition_LESSTHAN_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "simple_comparison_condition",
				m_vRHSSymbols = new string[]
				{
					"'>'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyConditionRuleMultiProcessors.Process_SimpleComparisonCondition_GREATERTHAN_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "simple_comparison_condition",
				m_vRHSSymbols = new string[]
				{
					"cmp_op"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyConditionRuleMultiProcessors.Process_SimpleComparisonCondition_CmpOp_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "simple_comparison_condition",
				m_vRHSSymbols = new string[]
				{
					"'<'",
					"'='"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyConditionRuleMultiProcessors.Process_SimpleComparisonCondition_LESSTHAN_EQUAL_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "simple_comparison_condition",
				m_vRHSSymbols = new string[]
				{
					"'>'",
					"'='"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyConditionRuleMultiProcessors.Process_SimpleComparisonCondition_GREATERTHAN_EQUAL_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "cmp_op",
				m_vRHSSymbols = new string[]
				{
					"'='"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyConditionRuleMultiProcessors.Process_CmpOp_EQUAL_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "cmp_op",
				m_vRHSSymbols = new string[]
				{
					"not_eq"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyConditionRuleMultiProcessors.Process_CmpOp_NotEq_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "cmp_op",
				m_vRHSSymbols = new string[]
				{
					"'!'",
					"'='"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyConditionRuleMultiProcessors.Process_CmpOp_NOTEQUAL_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "cmp_op",
				m_vRHSSymbols = new string[]
				{
					"'^'",
					"'='"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyConditionRuleMultiProcessors.Process_CmpOp_EXP_EQUAL_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "not_eq",
				m_vRHSSymbols = new string[]
				{
					"'<'",
					"'>'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyConditionRuleMultiProcessors.Process_NotEq_LESSTHAN_GREATERTHAN_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "compound_condition",
				m_vRHSSymbols = new string[]
				{
					"'NOT'",
					"condition"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyConditionRuleMultiProcessors.Process_CompoundCondition_NOT_Condition_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "compound_condition",
				m_vRHSSymbols = new string[]
				{
					"'('",
					"condition",
					"')'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyConditionRuleMultiProcessors.Process_CompoundCondition_LEFT_PARENTHESIS_Condition_RIGHT_PARENTHESIS_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "compound_condition",
				m_vRHSSymbols = new string[]
				{
					"condition",
					"AND_OR",
					"condition"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyConditionRuleMultiProcessors.Process_CompoundCondition_Condition_ANDOR_Condition_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "AND_OR",
				m_vRHSSymbols = new string[]
				{
					"'AND'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyConditionRuleMultiProcessors.Process_ANDOR_AND_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "AND_OR",
				m_vRHSSymbols = new string[]
				{
					"'OR'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyConditionRuleMultiProcessors.Process_ANDOR_OR_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			}
		};
	}
}
