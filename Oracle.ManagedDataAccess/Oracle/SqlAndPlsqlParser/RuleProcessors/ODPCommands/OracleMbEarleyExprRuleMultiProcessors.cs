using System;
using System.Collections.Generic;
using System.Text;
using Oracle.SqlAndPlsqlParser.LocalParsing;

namespace Oracle.SqlAndPlsqlParser.RuleProcessors.ODPCommands
{
	// Token: 0x0200030B RID: 779
	internal static class OracleMbEarleyExprRuleMultiProcessors
	{
		// Token: 0x06001BC4 RID: 7108 RVA: 0x0011111C File Offset: 0x0010F31C
		public static object Process_Expr_Expr_StartWithRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
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

		// Token: 0x06001BC5 RID: 7109 RVA: 0x0011116C File Offset: 0x0010F36C
		public static object Process_Expr_Attribute_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001BC6 RID: 7110 RVA: 0x00111190 File Offset: 0x0010F390
		public static object Process_Expr_BindVar_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpBindVarExpression oracleLpBindVarExpression = new OracleLpBindVarExpression(null);
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("bind_var");
			oracleLpBindVarExpression.BindParameter = (OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx) as OracleLpBindParameter);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return oracleLpBindVarExpression;
		}

		// Token: 0x06001BC7 RID: 7111 RVA: 0x001111E4 File Offset: 0x0010F3E4
		public static object Process_Expr_CaseExpression_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			OracleLpExpression oracleLpExpression = new OracleLpExpression(null);
			oracleLpExpression.ExpressionType = OracleLpExpressionType.CASE_EXPRESSION;
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			return oracleLpExpression;
		}

		// Token: 0x06001BC8 RID: 7112 RVA: 0x00111220 File Offset: 0x0010F420
		public static object Process_Expr_CompoundExpression_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001BC9 RID: 7113 RVA: 0x00111244 File Offset: 0x0010F444
		public static object Process_Expr_CursorExpression_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("cursor_expression");
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001BCA RID: 7114 RVA: 0x00111288 File Offset: 0x0010F488
		public static object Process_Expr_DateTimeExpression_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001BCB RID: 7115 RVA: 0x001112AC File Offset: 0x0010F4AC
		public static object Process_Expr_FunctionExpression_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001BCC RID: 7116 RVA: 0x001112D0 File Offset: 0x0010F4D0
		public static object Process_Expr_IntervalExpression_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001BCD RID: 7117 RVA: 0x001112F4 File Offset: 0x0010F4F4
		public static object Process_Expr_ModelExpression_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("model_expression");
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001BCE RID: 7118 RVA: 0x00111338 File Offset: 0x0010F538
		public static object Process_Expr_MultisetExpression_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("multiset_expression");
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001BCF RID: 7119 RVA: 0x0011137C File Offset: 0x0010F57C
		public static object Process_Expr_ObjectAccessExpression_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("object_access_expression");
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001BD0 RID: 7120 RVA: 0x001113C0 File Offset: 0x0010F5C0
		public static object Process_Expr_ScalarSubqueryExpression_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("scalar_subquery_expression");
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001BD1 RID: 7121 RVA: 0x00111404 File Offset: 0x0010F604
		public static object Process_Expr_SimpleExpression_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("simple_expression");
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001BD2 RID: 7122 RVA: 0x0011144C File Offset: 0x0010F64C
		public static object Process_Expr_TypeConstructorExpression_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("type_constructor_expression");
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001BD3 RID: 7123 RVA: 0x00111490 File Offset: 0x0010F690
		public static object Process_CompoundExpression_Expr_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpCompoundUnaryExpression oracleLpCompoundUnaryExpression = new OracleLpCompoundUnaryExpression(null);
			string text = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx) as string;
			text = text.ToUpper();
			string a;
			if ((a = text) != null)
			{
				if (!(a == "+"))
				{
					if (!(a == "-"))
					{
						if (a == "PRIOR")
						{
							oracleLpCompoundUnaryExpression.UnaryOperator = OracleLpCompoundExpressionUnaryOperator.PRIOR;
						}
					}
					else
					{
						oracleLpCompoundUnaryExpression.UnaryOperator = OracleLpCompoundExpressionUnaryOperator.MINUS;
					}
				}
				else
				{
					oracleLpCompoundUnaryExpression.UnaryOperator = OracleLpCompoundExpressionUnaryOperator.PLUS;
				}
			}
			oracleLpCompoundUnaryExpression.Operand = (OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx) as OracleLpExpression);
			return oracleLpCompoundUnaryExpression;
		}

		// Token: 0x06001BD4 RID: 7124 RVA: 0x00111530 File Offset: 0x0010F730
		public static object Process_CompoundExpression_LEFT_PARENTHESIS_Expr_RIGHT_PARENTHESIS_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			return new OracleLpCompoundEvaluateExpression(null)
			{
				Operand = (OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx) as OracleLpExpression)
			};
		}

		// Token: 0x06001BD5 RID: 7125 RVA: 0x0011156C File Offset: 0x0010F76C
		public static object Process_CompoundExpression_Expr_CompoundExpression_Expr_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpCompoundBinaryExpression oracleLpCompoundBinaryExpression = new OracleLpCompoundBinaryExpression(null);
			oracleLpCompoundBinaryExpression.LeftOperand = (OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx) as OracleLpExpression);
			string text = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx) as string;
			oracleLpCompoundBinaryExpression.RightOperand = (OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[2], 0, -1, ctx) as OracleLpExpression);
			string a;
			if ((a = text) != null)
			{
				if (!(a == "*"))
				{
					if (!(a == "/"))
					{
						if (!(a == "+"))
						{
							if (!(a == "-"))
							{
								if (a == "||")
								{
									oracleLpCompoundBinaryExpression.BinaryOperator = OracleLpCompoundExpressionBinaryOperator.CONCATENATE;
								}
							}
							else
							{
								oracleLpCompoundBinaryExpression.BinaryOperator = OracleLpCompoundExpressionBinaryOperator.SUBTRACT;
							}
						}
						else
						{
							oracleLpCompoundBinaryExpression.BinaryOperator = OracleLpCompoundExpressionBinaryOperator.ADD;
						}
					}
					else
					{
						oracleLpCompoundBinaryExpression.BinaryOperator = OracleLpCompoundExpressionBinaryOperator.DIVIDE;
					}
				}
				else
				{
					oracleLpCompoundBinaryExpression.BinaryOperator = OracleLpCompoundExpressionBinaryOperator.MULTIPLY;
				}
			}
			return oracleLpCompoundBinaryExpression;
		}

		// Token: 0x06001BD6 RID: 7126 RVA: 0x0011164C File Offset: 0x0010F84C
		public static object Process_CompoundExpression_GetIdentifier_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return ctx.Tokens[ctx.CurrentParseNode.From].m_vContent;
		}

		// Token: 0x06001BD7 RID: 7127 RVA: 0x0011166C File Offset: 0x0010F86C
		public static object Process_CompoundExpression_CONCATENATE_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return "||";
		}

		// Token: 0x06001BD8 RID: 7128 RVA: 0x00111674 File Offset: 0x0010F874
		public static object Process_DatetimeExpression_Expr_AT_DatetimeExpression_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpDatetimeExpression oracleLpDatetimeExpression = new OracleLpDatetimeExpression(null);
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			List<LexerToken> tokens = ctx.Tokens;
			ParseNode currentParseNode = ctx.CurrentParseNode;
			int vBegin = tokens[currentParseNode.From].m_vBegin;
			int vEnd = tokens[currentParseNode.To - 1].m_vEnd;
			oracleLpDatetimeExpression.Text = new OracleLpTextFragment(ctx.Script, vBegin, vEnd - vBegin);
			oracleLpDatetimeExpression.Datetime = (OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx) as OracleLpExpression);
			OracleLpExpression ao = (OracleLpExpression)ctx.GetActiveObject(11);
			ctx.SetActiveObject(11, oracleLpDatetimeExpression);
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[2], 0, -1, ctx);
			ctx.SetActiveObject(11, ao);
			return oracleLpDatetimeExpression;
		}

		// Token: 0x06001BD9 RID: 7129 RVA: 0x00111730 File Offset: 0x0010F930
		public static object Process_DatetimeExpression_LOCAL_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpDatetimeExpression oracleLpDatetimeExpression = (OracleLpDatetimeExpression)ctx.GetActiveObject(11);
			oracleLpDatetimeExpression.DatetimeExpessionType = OracleLpDatetimeExpressionType.LOCAL;
			return oracleLpDatetimeExpression;
		}

		// Token: 0x06001BDA RID: 7130 RVA: 0x00111754 File Offset: 0x0010F954
		public static object Process_DatetimeExpression_TIME_ZONE_DatetimeExpression_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpDatetimeExpression oracleLpDatetimeExpression = (OracleLpDatetimeExpression)ctx.GetActiveObject(11);
			oracleLpDatetimeExpression.DatetimeExpessionType = OracleLpDatetimeExpressionType.TIME_ZONE;
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleLpTimeZoneExpression oracleLpTimeZoneExpression = new OracleLpTimeZoneExpression(oracleLpDatetimeExpression);
			oracleLpDatetimeExpression.TimeZone = oracleLpTimeZoneExpression;
			ctx.SetActiveObject(11, oracleLpTimeZoneExpression);
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[2], 0, -1, ctx);
			ctx.SetActiveObject(11, oracleLpDatetimeExpression);
			return oracleLpDatetimeExpression;
		}

		// Token: 0x06001BDB RID: 7131 RVA: 0x001117B4 File Offset: 0x0010F9B4
		public static object Process_DatetimeExpression_DBTIMEZONE_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpTimeZoneExpression oracleLpTimeZoneExpression = (OracleLpTimeZoneExpression)ctx.GetActiveObject(11);
			oracleLpTimeZoneExpression.TZExpressionType = OracleLpTimeZoneExpressionType.DBTIMEZONE;
			return oracleLpTimeZoneExpression;
		}

		// Token: 0x06001BDC RID: 7132 RVA: 0x001117D8 File Offset: 0x0010F9D8
		public static object Process_DatetimeExpression_Expr_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpTimeZoneExpression oracleLpTimeZoneExpression = (OracleLpTimeZoneExpression)ctx.GetActiveObject(11);
			oracleLpTimeZoneExpression.TZExpressionType = OracleLpTimeZoneExpressionType.EXPRESSION;
			oracleLpTimeZoneExpression.TZExpression = (OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx) as OracleLpExpression);
			return oracleLpTimeZoneExpression;
		}

		// Token: 0x06001BDD RID: 7133 RVA: 0x0011181C File Offset: 0x0010FA1C
		public static object Process_DatetimeExpression_StringLiteral_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpTimeZoneExpression oracleLpTimeZoneExpression = (OracleLpTimeZoneExpression)ctx.GetActiveObject(11);
			oracleLpTimeZoneExpression.TZExpressionType = OracleLpTimeZoneExpressionType.STRING_LITERAL;
			oracleLpTimeZoneExpression.TZLiteral = ctx.Tokens[ctx.CurrentParseNode.From].m_vContent;
			return oracleLpTimeZoneExpression;
		}

		// Token: 0x06001BDE RID: 7134 RVA: 0x00111860 File Offset: 0x0010FA60
		public static object Process_FunctionExpression_Function_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpFunctionExpression oracleLpFunctionExpression = new OracleLpFunctionExpression(null);
			List<LexerToken> tokens = ctx.Tokens;
			ParseNode currentParseNode = ctx.CurrentParseNode;
			int vBegin = tokens[currentParseNode.From].m_vBegin;
			int vEnd = tokens[currentParseNode.To].m_vEnd;
			oracleLpFunctionExpression.Text = new OracleLpTextFragment(ctx.Script, vBegin, vEnd - vBegin);
			return oracleLpFunctionExpression;
		}

		// Token: 0x06001BDF RID: 7135 RVA: 0x001118C0 File Offset: 0x0010FAC0
		public static object Process_FunctionExpression_FunctionCall_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpFunctionExpression oracleLpFunctionExpression = new OracleLpFunctionExpression(null);
			List<LexerToken> tokens = ctx.Tokens;
			ParseNode currentParseNode = ctx.CurrentParseNode;
			int vBegin = tokens[currentParseNode.From].m_vBegin;
			int vEnd = tokens[currentParseNode.To].m_vEnd;
			oracleLpFunctionExpression.Text = new OracleLpTextFragment(ctx.Script, vBegin, vEnd - vBegin);
			return oracleLpFunctionExpression;
		}

		// Token: 0x06001BE0 RID: 7136 RVA: 0x00111920 File Offset: 0x0010FB20
		public static object Process_IntervalExpression_IntervalDayToSecond_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpIntervalExpression oracleLpIntervalExpression = new OracleLpIntervalExpression(null);
			oracleLpIntervalExpression.IntervalExpessionType = OracleLpIntervalExpressionType.DAY_TO_SECOND;
			List<LexerToken> tokens = ctx.Tokens;
			ParseNode currentParseNode = ctx.CurrentParseNode;
			int vBegin = tokens[currentParseNode.From].m_vBegin;
			int vEnd = tokens[currentParseNode.To - 1].m_vEnd;
			oracleLpIntervalExpression.Text = new OracleLpTextFragment(ctx.Script, vBegin, vEnd - vBegin);
			OracleLpExpression ao = (OracleLpExpression)ctx.GetActiveObject(11);
			ctx.SetActiveObject(11, oracleLpIntervalExpression);
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			ctx.SetActiveObject(11, ao);
			return oracleLpIntervalExpression;
		}

		// Token: 0x06001BE1 RID: 7137 RVA: 0x001119C0 File Offset: 0x0010FBC0
		public static object Process_IntervalExpression_IntervalYearToMonth_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpIntervalExpression oracleLpIntervalExpression = new OracleLpIntervalExpression(null);
			oracleLpIntervalExpression.IntervalExpessionType = OracleLpIntervalExpressionType.YEAR_TO_MONTH;
			List<LexerToken> tokens = ctx.Tokens;
			ParseNode currentParseNode = ctx.CurrentParseNode;
			int vBegin = tokens[currentParseNode.From].m_vBegin;
			int vEnd = tokens[currentParseNode.To - 1].m_vEnd;
			oracleLpIntervalExpression.Text = new OracleLpTextFragment(ctx.Script, vBegin, vEnd - vBegin);
			OracleLpExpression ao = (OracleLpExpression)ctx.GetActiveObject(11);
			ctx.SetActiveObject(11, oracleLpIntervalExpression);
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			ctx.SetActiveObject(11, ao);
			return oracleLpIntervalExpression;
		}

		// Token: 0x06001BE2 RID: 7138 RVA: 0x00111A60 File Offset: 0x0010FC60
		public static object Process_IntervalExpression_LEFT_PARENTHESIS_Expr_DASH_Expr_RIGHT_PARENTHESIS_IntervalExpression_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("(");
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			stringBuilder.Append(OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx));
			stringBuilder.Append("-");
			stringBuilder.Append(OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[3], 0, -1, ctx));
			ctx.RuleProcessorTable = ruleProcessorTable;
			stringBuilder.Append(")");
			stringBuilder.Append(OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[5], 0, -1, ctx));
			return stringBuilder.ToString();
		}

		// Token: 0x06001BE3 RID: 7139 RVA: 0x00111AFC File Offset: 0x0010FCFC
		public static object Process_IntervalExpression_DAY_TO_SECOND_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return "DAY TO SECOND";
		}

		// Token: 0x06001BE4 RID: 7140 RVA: 0x00111B04 File Offset: 0x0010FD04
		public static object Process_IntervalExpression_YEAR_TO_MONTH_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return "YEAR TO MONTH";
		}

		// Token: 0x06001BE5 RID: 7141 RVA: 0x00111B0C File Offset: 0x0010FD0C
		public static object Process_IntervalExpression_DAY_TO_SECOND_IntervalExpression_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DAY TO SECOND ");
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			stringBuilder.Append(OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[3], 0, -1, ctx));
			return stringBuilder.ToString();
		}

		// Token: 0x06001BE6 RID: 7142 RVA: 0x00111B54 File Offset: 0x0010FD54
		public static object Process_IntervalExpression_DAY_IntervalExpression_TO_SECOND_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DAY ");
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			stringBuilder.Append(OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx));
			stringBuilder.Append(" TO SECOND");
			return stringBuilder.ToString();
		}

		// Token: 0x06001BE7 RID: 7143 RVA: 0x00111BA8 File Offset: 0x0010FDA8
		public static object Process_IntervalExpression_YEAR_IntervalExpression_TO_MONTH_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("YEAR ");
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			stringBuilder.Append(OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx));
			stringBuilder.Append(" TO MONTH");
			return stringBuilder.ToString();
		}

		// Token: 0x06001BE8 RID: 7144 RVA: 0x00111BFC File Offset: 0x0010FDFC
		public static object Process_IntervalExpression_DAY_IntervalExpression_TO_SECOND_IntervalExpression_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DAY ");
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			stringBuilder.Append(OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx));
			stringBuilder.Append(" TO SECOND");
			stringBuilder.Append(OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[4], 0, -1, ctx));
			return stringBuilder.ToString();
		}

		// Token: 0x06001BE9 RID: 7145 RVA: 0x00111C68 File Offset: 0x0010FE68
		public static object Process_IntervalExpression_LEFT_PARENTHESIS_Digits_RIGHT_PARENTHESIS_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			stringBuilder.Append("(");
			stringBuilder.Append(ctx.Tokens[list[1].From].m_vContent);
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x06001BEA RID: 7146 RVA: 0x00111CC8 File Offset: 0x0010FEC8
		public static object Process_IntervalDayToSecond_INTERVAL_StringLiteral_IntervalDayToSecond_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			stringBuilder.Append("INTERVAL ");
			stringBuilder.Append(ctx.Tokens[list[1].From].m_vContent);
			stringBuilder.Append(" ");
			stringBuilder.Append(OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[2], 0, -1, ctx));
			return stringBuilder.ToString();
		}

		// Token: 0x06001BEB RID: 7147 RVA: 0x00111D40 File Offset: 0x0010FF40
		public static object Process_IntervalDayToSecond_INTERVAL_StringLiteral_IntervalDayToSecond_TO_IntervalDayToSecond_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			stringBuilder.Append("INTERVAL ");
			stringBuilder.Append(ctx.Tokens[list[1].From].m_vContent);
			stringBuilder.Append(" ");
			stringBuilder.Append(OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[2], 0, -1, ctx));
			stringBuilder.Append(" TO ");
			stringBuilder.Append(OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[4], 0, -1, ctx));
			return stringBuilder.ToString();
		}

		// Token: 0x06001BEC RID: 7148 RVA: 0x00111DD8 File Offset: 0x0010FFD8
		public static object Process_IntervalDayToSecond_LEFT_PARENTHESIS_Digits_RIGHT_PARENTHESIS_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			stringBuilder.Append("(");
			stringBuilder.Append(ctx.Tokens[list[1].From].m_vContent);
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x06001BED RID: 7149 RVA: 0x00111E38 File Offset: 0x00110038
		public static object Process_IntervalDayToSecond_LEFT_PARENTHESIS_Digits_COMMA_Digits_RIGHT_PARENTHESIS_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			stringBuilder.Append("(");
			stringBuilder.Append(ctx.Tokens[list[1].From].m_vContent);
			stringBuilder.Append(",");
			stringBuilder.Append(ctx.Tokens[list[3].From].m_vContent);
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x06001BEE RID: 7150 RVA: 0x00111EC8 File Offset: 0x001100C8
		public static object Process_IntervalDayToSecond_GetIdentifier_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return ctx.Tokens[ctx.CurrentParseNode.From].m_vContent;
		}

		// Token: 0x06001BEF RID: 7151 RVA: 0x00111EE8 File Offset: 0x001100E8
		public static object Process_IntervalDayToSecond_SECOND_IntervalDayToSecond_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			stringBuilder.Append("SECOND ");
			stringBuilder.Append(OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx));
			return stringBuilder.ToString();
		}

		// Token: 0x06001BF0 RID: 7152 RVA: 0x00111F30 File Offset: 0x00110130
		public static object Process_IntervalDayToSecond_IntervalDayToSecond_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001BF1 RID: 7153 RVA: 0x00111F48 File Offset: 0x00110148
		public static object Process_IntervalDayToSecond_IntervalDayToSecond_IntervalDayToSecond_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			stringBuilder.Append(OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx));
			stringBuilder.Append(" ");
			stringBuilder.Append(OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx));
			return stringBuilder.ToString();
		}

		// Token: 0x06001BF2 RID: 7154 RVA: 0x00111FA8 File Offset: 0x001101A8
		public static object Process_IntervalYearToMonth_INTERVAL_StringLiteral_IntervalYearToMonth_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			stringBuilder.Append("INTERVAL ");
			stringBuilder.Append(ctx.Tokens[list[1].From].m_vContent);
			stringBuilder.Append(" ");
			stringBuilder.Append(OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[2], 0, -1, ctx));
			return stringBuilder.ToString();
		}

		// Token: 0x06001BF3 RID: 7155 RVA: 0x00112020 File Offset: 0x00110220
		public static object Process_IntervalYearToMonth_INTERVAL_StringLiteral_IntervalYearToMonth_IntervalYearToMonth_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			stringBuilder.Append("INTERVAL ");
			stringBuilder.Append(ctx.Tokens[list[1].From].m_vContent);
			stringBuilder.Append(" ");
			stringBuilder.Append(OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[2], 0, -1, ctx));
			stringBuilder.Append(" ");
			stringBuilder.Append(OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[3], 0, -1, ctx));
			return stringBuilder.ToString();
		}

		// Token: 0x06001BF4 RID: 7156 RVA: 0x001120B8 File Offset: 0x001102B8
		public static object Process_IntervalYearToMonth_INTERVAL_StringLiteral_IntervalYearToMonth_TO_IntervalYearToMonth_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			stringBuilder.Append("INTERVAL ");
			stringBuilder.Append(ctx.Tokens[list[1].From].m_vContent);
			stringBuilder.Append(" ");
			stringBuilder.Append(OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[2], 0, -1, ctx));
			stringBuilder.Append(" TO ");
			stringBuilder.Append(OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[4], 0, -1, ctx));
			return stringBuilder.ToString();
		}

		// Token: 0x06001BF5 RID: 7157 RVA: 0x00112150 File Offset: 0x00110350
		public static object Process_IntervalYearToMonth_INTERVAL_StringLiteral_IntervalYearToMonth_IntervalYearToMonth_TO_IntervalYearToMonth_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			stringBuilder.Append("INTERVAL ");
			stringBuilder.Append(ctx.Tokens[list[1].From].m_vContent);
			stringBuilder.Append(" ");
			stringBuilder.Append(OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[2], 0, -1, ctx));
			stringBuilder.Append(" ");
			stringBuilder.Append(OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[3], 0, -1, ctx));
			stringBuilder.Append(" TO ");
			stringBuilder.Append(OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[5], 0, -1, ctx));
			return stringBuilder.ToString();
		}

		// Token: 0x06001BF6 RID: 7158 RVA: 0x0011220C File Offset: 0x0011040C
		public static object Process_IntervalYearToMonth_GetIdentifier_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return ctx.Tokens[ctx.CurrentParseNode.From].m_vContent;
		}

		// Token: 0x06001BF7 RID: 7159 RVA: 0x0011222C File Offset: 0x0011042C
		public static object Process_IntervalYearToMonth_LEFT_PARENTHESIS_Digits_RIGHT_PARENTHESIS_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			stringBuilder.Append("(");
			stringBuilder.Append(ctx.Tokens[list[1].From].m_vContent);
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x04001D60 RID: 7520
		public static OracleMbEarleyRuleMultiProcessorAddItem[] s_vRuleProcessorItems = new OracleMbEarleyRuleMultiProcessorAddItem[]
		{
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "expr",
				m_vRHSSymbols = new string[]
				{
					"expr"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_Expr_Expr_StartWithRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllRulesStartingWith
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "expr",
				m_vRHSSymbols = new string[]
				{
					"attribute"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_Expr_Attribute_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "expr",
				m_vRHSSymbols = new string[]
				{
					"bind_var"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_Expr_BindVar_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "expr",
				m_vRHSSymbols = new string[]
				{
					"case_expression"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_Expr_CaseExpression_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "expr",
				m_vRHSSymbols = new string[]
				{
					"compound_expression"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_Expr_CompoundExpression_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "expr",
				m_vRHSSymbols = new string[]
				{
					"cursor_expression"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_Expr_CursorExpression_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "expr",
				m_vRHSSymbols = new string[]
				{
					"datetime_expression"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_Expr_DateTimeExpression_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "expr",
				m_vRHSSymbols = new string[]
				{
					"function_expression"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_Expr_FunctionExpression_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "expr",
				m_vRHSSymbols = new string[]
				{
					"interval_expression"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_Expr_IntervalExpression_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "expr",
				m_vRHSSymbols = new string[]
				{
					"model_expression"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_Expr_ModelExpression_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "expr",
				m_vRHSSymbols = new string[]
				{
					"multiset_expression"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_Expr_MultisetExpression_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "expr",
				m_vRHSSymbols = new string[]
				{
					"object_access_expression"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_Expr_ObjectAccessExpression_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "expr",
				m_vRHSSymbols = new string[]
				{
					"scalar_subquery_expression"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_Expr_ScalarSubqueryExpression_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "expr",
				m_vRHSSymbols = new string[]
				{
					"simple_expression"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_Expr_SimpleExpression_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "expr",
				m_vRHSSymbols = new string[]
				{
					"type_constructor_expression"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_Expr_TypeConstructorExpression_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "compound_expression",
				m_vRHSSymbols = new string[]
				{
					"compound_expression",
					"expr"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_CompoundExpression_Expr_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "compound_expression",
				m_vRHSSymbols = new string[]
				{
					"'('",
					"expr",
					"')'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_CompoundExpression_LEFT_PARENTHESIS_Expr_RIGHT_PARENTHESIS_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "compound_expression",
				m_vRHSSymbols = new string[]
				{
					"expr",
					"compound_expression",
					"expr"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_CompoundExpression_Expr_CompoundExpression_Expr_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "compound_expression",
				m_vRHSSymbols = new string[]
				{
					"'*'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_CompoundExpression_GetIdentifier_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "compound_expression",
				m_vRHSSymbols = new string[]
				{
					"'+'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_CompoundExpression_GetIdentifier_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "compound_expression",
				m_vRHSSymbols = new string[]
				{
					"'-'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_CompoundExpression_GetIdentifier_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "compound_expression",
				m_vRHSSymbols = new string[]
				{
					"'/'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_CompoundExpression_GetIdentifier_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "compound_expression",
				m_vRHSSymbols = new string[]
				{
					"'|'",
					"'|'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_CompoundExpression_CONCATENATE_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "compound_expression",
				m_vRHSSymbols = new string[]
				{
					"'PRIOR'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_CompoundExpression_GetIdentifier_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "datetime_expression",
				m_vRHSSymbols = new string[]
				{
					"expr",
					"'AT'",
					"datetime_expression"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_DatetimeExpression_Expr_AT_DatetimeExpression_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "datetime_expression",
				m_vRHSSymbols = new string[]
				{
					"'LOCAL'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_DatetimeExpression_LOCAL_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "datetime_expression",
				m_vRHSSymbols = new string[]
				{
					"'TIME'",
					"'ZONE'",
					"datetime_expression"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_DatetimeExpression_TIME_ZONE_DatetimeExpression_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "datetime_expression",
				m_vRHSSymbols = new string[]
				{
					"'DBTIMEZONE'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_DatetimeExpression_DBTIMEZONE_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "datetime_expression",
				m_vRHSSymbols = new string[]
				{
					"expr"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_DatetimeExpression_Expr_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "datetime_expression",
				m_vRHSSymbols = new string[]
				{
					"string_literal"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_DatetimeExpression_StringLiteral_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "function_expression",
				m_vRHSSymbols = new string[]
				{
					"function"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_FunctionExpression_Function_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "function_expression",
				m_vRHSSymbols = new string[]
				{
					"function_call"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_FunctionExpression_FunctionCall_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "interval_expression",
				m_vRHSSymbols = new string[]
				{
					"interval_day_to_second"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_IntervalExpression_IntervalDayToSecond_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "interval_expression",
				m_vRHSSymbols = new string[]
				{
					"interval_year_to_month"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_IntervalExpression_IntervalYearToMonth_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "interval_expression",
				m_vRHSSymbols = new string[]
				{
					"'('",
					"expr",
					"'-'",
					"expr",
					"')'",
					"interval_expression"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_IntervalExpression_LEFT_PARENTHESIS_Expr_DASH_Expr_RIGHT_PARENTHESIS_IntervalExpression_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "interval_expression",
				m_vRHSSymbols = new string[]
				{
					"'DAY'",
					"'TO'",
					"'SECOND'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_IntervalExpression_DAY_TO_SECOND_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "interval_expression",
				m_vRHSSymbols = new string[]
				{
					"'YEAR'",
					"'TO'",
					"'MONTH'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_IntervalExpression_YEAR_TO_MONTH_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "interval_expression",
				m_vRHSSymbols = new string[]
				{
					"'DAY'",
					"'TO'",
					"'SECOND'",
					"interval_expression"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_IntervalExpression_DAY_TO_SECOND_IntervalExpression_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "interval_expression",
				m_vRHSSymbols = new string[]
				{
					"'DAY'",
					"interval_expression",
					"'TO'",
					"'SECOND'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_IntervalExpression_DAY_IntervalExpression_TO_SECOND_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "interval_expression",
				m_vRHSSymbols = new string[]
				{
					"'YEAR'",
					"interval_expression",
					"'TO'",
					"'MONTH'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_IntervalExpression_YEAR_IntervalExpression_TO_MONTH_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "interval_expression",
				m_vRHSSymbols = new string[]
				{
					"'DAY'",
					"interval_expression",
					"'TO'",
					"'SECOND'",
					"interval_expression"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_IntervalExpression_DAY_IntervalExpression_TO_SECOND_IntervalExpression_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "interval_expression",
				m_vRHSSymbols = new string[]
				{
					"'('",
					"digits",
					"')'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_IntervalExpression_LEFT_PARENTHESIS_Digits_RIGHT_PARENTHESIS_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "interval_day_to_second",
				m_vRHSSymbols = new string[]
				{
					"'INTERVAL'",
					"string_literal",
					"interval_day_to_second"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_IntervalDayToSecond_INTERVAL_StringLiteral_IntervalDayToSecond_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "interval_day_to_second",
				m_vRHSSymbols = new string[]
				{
					"'INTERVAL'",
					"string_literal",
					"interval_day_to_second",
					"'TO'",
					"interval_day_to_second"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_IntervalDayToSecond_INTERVAL_StringLiteral_IntervalDayToSecond_TO_IntervalDayToSecond_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "interval_day_to_second",
				m_vRHSSymbols = new string[]
				{
					"'('",
					"digits",
					"')'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_IntervalDayToSecond_LEFT_PARENTHESIS_Digits_RIGHT_PARENTHESIS_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "interval_day_to_second",
				m_vRHSSymbols = new string[]
				{
					"'('",
					"digits",
					"','",
					"digits",
					"')'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_IntervalDayToSecond_LEFT_PARENTHESIS_Digits_COMMA_Digits_RIGHT_PARENTHESIS_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "interval_day_to_second",
				m_vRHSSymbols = new string[]
				{
					"'DAY'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_IntervalDayToSecond_GetIdentifier_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "interval_day_to_second",
				m_vRHSSymbols = new string[]
				{
					"'HOUR'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_IntervalDayToSecond_GetIdentifier_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "interval_day_to_second",
				m_vRHSSymbols = new string[]
				{
					"'MINUTE'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_IntervalDayToSecond_GetIdentifier_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "interval_day_to_second",
				m_vRHSSymbols = new string[]
				{
					"'SECOND'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_IntervalDayToSecond_GetIdentifier_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "interval_day_to_second",
				m_vRHSSymbols = new string[]
				{
					"'SECOND'",
					"interval_day_to_second"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_IntervalDayToSecond_SECOND_IntervalDayToSecond_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "interval_day_to_second",
				m_vRHSSymbols = new string[]
				{
					"interval_day_to_second"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_IntervalDayToSecond_IntervalDayToSecond_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "interval_day_to_second",
				m_vRHSSymbols = new string[]
				{
					"interval_day_to_second",
					"interval_day_to_second"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_IntervalDayToSecond_IntervalDayToSecond_IntervalDayToSecond_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "interval_year_to_month",
				m_vRHSSymbols = new string[]
				{
					"'INTERVAL'",
					"string_literal",
					"interval_year_to_month"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_IntervalYearToMonth_INTERVAL_StringLiteral_IntervalYearToMonth_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "interval_year_to_month",
				m_vRHSSymbols = new string[]
				{
					"'INTERVAL'",
					"string_literal",
					"interval_year_to_month",
					"interval_year_to_month"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_IntervalYearToMonth_INTERVAL_StringLiteral_IntervalYearToMonth_IntervalYearToMonth_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "interval_year_to_month",
				m_vRHSSymbols = new string[]
				{
					"'INTERVAL'",
					"string_literal",
					"interval_year_to_month",
					"'TO'",
					"interval_year_to_month"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_IntervalYearToMonth_INTERVAL_StringLiteral_IntervalYearToMonth_TO_IntervalYearToMonth_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "interval_year_to_month",
				m_vRHSSymbols = new string[]
				{
					"'INTERVAL'",
					"string_literal",
					"interval_year_to_month",
					"interval_year_to_month",
					"'TO'",
					"interval_year_to_month"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_IntervalYearToMonth_INTERVAL_StringLiteral_IntervalYearToMonth_IntervalYearToMonth_TO_IntervalYearToMonth_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "interval_year_to_month",
				m_vRHSSymbols = new string[]
				{
					"'MONTH'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_IntervalYearToMonth_GetIdentifier_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "interval_year_to_month",
				m_vRHSSymbols = new string[]
				{
					"'YEAR'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_IntervalYearToMonth_GetIdentifier_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "interval_year_to_month",
				m_vRHSSymbols = new string[]
				{
					"'('",
					"digits",
					"')'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprRuleMultiProcessors.Process_IntervalYearToMonth_LEFT_PARENTHESIS_Digits_RIGHT_PARENTHESIS_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			}
		};
	}
}
