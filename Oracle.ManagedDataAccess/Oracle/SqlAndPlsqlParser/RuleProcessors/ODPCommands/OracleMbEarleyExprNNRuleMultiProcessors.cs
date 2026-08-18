using System;
using System.Collections.Generic;
using System.Text;
using Oracle.SqlAndPlsqlParser.LocalParsing;

namespace Oracle.SqlAndPlsqlParser.RuleProcessors.ODPCommands
{
	// Token: 0x0200030C RID: 780
	internal static class OracleMbEarleyExprNNRuleMultiProcessors
	{
		// Token: 0x06001BF9 RID: 7161 RVA: 0x001139B8 File Offset: 0x00111BB8
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

		// Token: 0x06001BFA RID: 7162 RVA: 0x00113A08 File Offset: 0x00111C08
		public static object Process_Expr_Attribute_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001BFB RID: 7163 RVA: 0x00113A2C File Offset: 0x00111C2C
		public static object Process_Expr_BindVar_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpBindVarExpression oracleLpBindVarExpression = new OracleLpBindVarExpression(null);
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("bind_var");
			oracleLpBindVarExpression.BindParameter = (OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx) as OracleLpBindParameter);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return oracleLpBindVarExpression;
		}

		// Token: 0x06001BFC RID: 7164 RVA: 0x00113A80 File Offset: 0x00111C80
		public static object Process_Expr_CaseExpression_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			OracleLpExpression oracleLpExpression = new OracleLpExpression(null);
			oracleLpExpression.ExpressionType = OracleLpExpressionType.CASE_EXPRESSION;
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			return oracleLpExpression;
		}

		// Token: 0x06001BFD RID: 7165 RVA: 0x00113ABC File Offset: 0x00111CBC
		public static object Process_Expr_CompoundExpression_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001BFE RID: 7166 RVA: 0x00113AE0 File Offset: 0x00111CE0
		public static object Process_Expr_CursorExpression_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("cursor_expression");
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001BFF RID: 7167 RVA: 0x00113B24 File Offset: 0x00111D24
		public static object Process_Expr_DateTimeExpression_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001C00 RID: 7168 RVA: 0x00113B48 File Offset: 0x00111D48
		public static object Process_Expr_FunctionExpression_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001C01 RID: 7169 RVA: 0x00113B6C File Offset: 0x00111D6C
		public static object Process_Expr_IntervalExpression_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001C02 RID: 7170 RVA: 0x00113B90 File Offset: 0x00111D90
		public static object Process_Expr_ModelExpression_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("model_expression");
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001C03 RID: 7171 RVA: 0x00113BD4 File Offset: 0x00111DD4
		public static object Process_Expr_MultisetExpression_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("multiset_expression");
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001C04 RID: 7172 RVA: 0x00113C18 File Offset: 0x00111E18
		public static object Process_Expr_ObjectAccessExpression_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("object_access_expression");
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001C05 RID: 7173 RVA: 0x00113C5C File Offset: 0x00111E5C
		public static object Process_Expr_ScalarSubqueryExpression_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("scalar_subquery_expression");
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001C06 RID: 7174 RVA: 0x00113CA0 File Offset: 0x00111EA0
		public static object Process_Expr_SimpleExpression_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("simple_expression");
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001C07 RID: 7175 RVA: 0x00113CE8 File Offset: 0x00111EE8
		public static object Process_Expr_TypeConstructorExpression_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("type_constructor_expression");
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001C08 RID: 7176 RVA: 0x00113D2C File Offset: 0x00111F2C
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

		// Token: 0x06001C09 RID: 7177 RVA: 0x00113DCC File Offset: 0x00111FCC
		public static object Process_CompoundExpression_LEFT_PARENTHESIS_Expr_RIGHT_PARENTHESIS_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			return new OracleLpCompoundEvaluateExpression(null)
			{
				Operand = (OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx) as OracleLpExpression)
			};
		}

		// Token: 0x06001C0A RID: 7178 RVA: 0x00113E08 File Offset: 0x00112008
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

		// Token: 0x06001C0B RID: 7179 RVA: 0x00113EE8 File Offset: 0x001120E8
		public static object Process_CompoundExpression_GetIdentifier_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return ctx.Tokens[ctx.CurrentParseNode.From].m_vContent;
		}

		// Token: 0x06001C0C RID: 7180 RVA: 0x00113F08 File Offset: 0x00112108
		public static object Process_CompoundExpression_CONCATENATE_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return "||";
		}

		// Token: 0x06001C0D RID: 7181 RVA: 0x00113F10 File Offset: 0x00112110
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

		// Token: 0x06001C0E RID: 7182 RVA: 0x00113FCC File Offset: 0x001121CC
		public static object Process_DatetimeExpression_LOCAL_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpDatetimeExpression oracleLpDatetimeExpression = (OracleLpDatetimeExpression)ctx.GetActiveObject(11);
			oracleLpDatetimeExpression.DatetimeExpessionType = OracleLpDatetimeExpressionType.LOCAL;
			return oracleLpDatetimeExpression;
		}

		// Token: 0x06001C0F RID: 7183 RVA: 0x00113FF0 File Offset: 0x001121F0
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

		// Token: 0x06001C10 RID: 7184 RVA: 0x00114050 File Offset: 0x00112250
		public static object Process_DatetimeExpression_DBTIMEZONE_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpTimeZoneExpression oracleLpTimeZoneExpression = (OracleLpTimeZoneExpression)ctx.GetActiveObject(11);
			oracleLpTimeZoneExpression.TZExpressionType = OracleLpTimeZoneExpressionType.DBTIMEZONE;
			return oracleLpTimeZoneExpression;
		}

		// Token: 0x06001C11 RID: 7185 RVA: 0x00114074 File Offset: 0x00112274
		public static object Process_DatetimeExpression_Expr_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpTimeZoneExpression oracleLpTimeZoneExpression = (OracleLpTimeZoneExpression)ctx.GetActiveObject(11);
			oracleLpTimeZoneExpression.TZExpressionType = OracleLpTimeZoneExpressionType.EXPRESSION;
			oracleLpTimeZoneExpression.TZExpression = (OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx) as OracleLpExpression);
			return oracleLpTimeZoneExpression;
		}

		// Token: 0x06001C12 RID: 7186 RVA: 0x001140B8 File Offset: 0x001122B8
		public static object Process_DatetimeExpression_StringLiteral_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpTimeZoneExpression oracleLpTimeZoneExpression = (OracleLpTimeZoneExpression)ctx.GetActiveObject(11);
			oracleLpTimeZoneExpression.TZExpressionType = OracleLpTimeZoneExpressionType.STRING_LITERAL;
			oracleLpTimeZoneExpression.TZLiteral = ctx.Tokens[ctx.CurrentParseNode.From].m_vContent;
			return oracleLpTimeZoneExpression;
		}

		// Token: 0x06001C13 RID: 7187 RVA: 0x001140FC File Offset: 0x001122FC
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

		// Token: 0x06001C14 RID: 7188 RVA: 0x0011415C File Offset: 0x0011235C
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

		// Token: 0x06001C15 RID: 7189 RVA: 0x001141BC File Offset: 0x001123BC
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

		// Token: 0x06001C16 RID: 7190 RVA: 0x0011425C File Offset: 0x0011245C
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

		// Token: 0x06001C17 RID: 7191 RVA: 0x001142FC File Offset: 0x001124FC
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

		// Token: 0x06001C18 RID: 7192 RVA: 0x00114398 File Offset: 0x00112598
		public static object Process_IntervalExpression_DAY_TO_SECOND_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return "DAY TO SECOND";
		}

		// Token: 0x06001C19 RID: 7193 RVA: 0x001143A0 File Offset: 0x001125A0
		public static object Process_IntervalExpression_YEAR_TO_MONTH_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return "YEAR TO MONTH";
		}

		// Token: 0x06001C1A RID: 7194 RVA: 0x001143A8 File Offset: 0x001125A8
		public static object Process_IntervalExpression_DAY_TO_SECOND_IntervalExpression_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DAY TO SECOND ");
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			stringBuilder.Append(OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[3], 0, -1, ctx));
			return stringBuilder.ToString();
		}

		// Token: 0x06001C1B RID: 7195 RVA: 0x001143F0 File Offset: 0x001125F0
		public static object Process_IntervalExpression_DAY_IntervalExpression_TO_SECOND_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DAY ");
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			stringBuilder.Append(OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx));
			stringBuilder.Append(" TO SECOND");
			return stringBuilder.ToString();
		}

		// Token: 0x06001C1C RID: 7196 RVA: 0x00114444 File Offset: 0x00112644
		public static object Process_IntervalExpression_YEAR_IntervalExpression_TO_MONTH_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("YEAR ");
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			stringBuilder.Append(OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx));
			stringBuilder.Append(" TO MONTH");
			return stringBuilder.ToString();
		}

		// Token: 0x06001C1D RID: 7197 RVA: 0x00114498 File Offset: 0x00112698
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

		// Token: 0x06001C1E RID: 7198 RVA: 0x00114504 File Offset: 0x00112704
		public static object Process_IntervalExpression_LEFT_PARENTHESIS_Digits_RIGHT_PARENTHESIS_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			stringBuilder.Append("(");
			stringBuilder.Append(ctx.Tokens[list[1].From].m_vContent);
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x06001C1F RID: 7199 RVA: 0x00114564 File Offset: 0x00112764
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

		// Token: 0x06001C20 RID: 7200 RVA: 0x001145DC File Offset: 0x001127DC
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

		// Token: 0x06001C21 RID: 7201 RVA: 0x00114674 File Offset: 0x00112874
		public static object Process_IntervalDayToSecond_LEFT_PARENTHESIS_Digits_RIGHT_PARENTHESIS_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			stringBuilder.Append("(");
			stringBuilder.Append(ctx.Tokens[list[1].From].m_vContent);
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x06001C22 RID: 7202 RVA: 0x001146D4 File Offset: 0x001128D4
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

		// Token: 0x06001C23 RID: 7203 RVA: 0x00114764 File Offset: 0x00112964
		public static object Process_IntervalDayToSecond_GetIdentifier_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return ctx.Tokens[ctx.CurrentParseNode.From].m_vContent;
		}

		// Token: 0x06001C24 RID: 7204 RVA: 0x00114784 File Offset: 0x00112984
		public static object Process_IntervalDayToSecond_SECOND_IntervalDayToSecond_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			stringBuilder.Append("SECOND ");
			stringBuilder.Append(OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx));
			return stringBuilder.ToString();
		}

		// Token: 0x06001C25 RID: 7205 RVA: 0x001147CC File Offset: 0x001129CC
		public static object Process_IntervalDayToSecond_IntervalDayToSecond_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001C26 RID: 7206 RVA: 0x001147E4 File Offset: 0x001129E4
		public static object Process_IntervalDayToSecond_IntervalDayToSecond_IntervalDayToSecond_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			stringBuilder.Append(OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx));
			stringBuilder.Append(" ");
			stringBuilder.Append(OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx));
			return stringBuilder.ToString();
		}

		// Token: 0x06001C27 RID: 7207 RVA: 0x00114844 File Offset: 0x00112A44
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

		// Token: 0x06001C28 RID: 7208 RVA: 0x001148BC File Offset: 0x00112ABC
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

		// Token: 0x06001C29 RID: 7209 RVA: 0x00114954 File Offset: 0x00112B54
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

		// Token: 0x06001C2A RID: 7210 RVA: 0x001149EC File Offset: 0x00112BEC
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

		// Token: 0x06001C2B RID: 7211 RVA: 0x00114AA8 File Offset: 0x00112CA8
		public static object Process_IntervalYearToMonth_GetIdentifier_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return ctx.Tokens[ctx.CurrentParseNode.From].m_vContent;
		}

		// Token: 0x06001C2C RID: 7212 RVA: 0x00114AC8 File Offset: 0x00112CC8
		public static object Process_IntervalYearToMonth_LEFT_PARENTHESIS_Digits_RIGHT_PARENTHESIS_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			stringBuilder.Append("(");
			stringBuilder.Append(ctx.Tokens[list[1].From].m_vContent);
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x04001D61 RID: 7521
		public static OracleMbEarleyRuleMultiProcessorAddItem[] s_vRuleProcessorItems = new OracleMbEarleyRuleMultiProcessorAddItem[]
		{
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "expr",
				m_vRHSSymbols = new string[]
				{
					"expr#"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_Expr_Expr_StartWithRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllRulesStartingWith
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "expr",
				m_vRHSSymbols = new string[]
				{
					"attribute"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_Expr_Attribute_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "expr#",
				m_vRHSSymbols = new string[]
				{
					"bind_var"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_Expr_BindVar_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "expr#",
				m_vRHSSymbols = new string[]
				{
					"case_expression"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_Expr_CaseExpression_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "expr#",
				m_vRHSSymbols = new string[]
				{
					"compound_expression"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_Expr_CompoundExpression_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "expr#",
				m_vRHSSymbols = new string[]
				{
					"cursor_expression"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_Expr_CursorExpression_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "expr#",
				m_vRHSSymbols = new string[]
				{
					"datetime_expression"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_Expr_DateTimeExpression_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "expr#",
				m_vRHSSymbols = new string[]
				{
					"function_expression"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_Expr_FunctionExpression_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "expr#",
				m_vRHSSymbols = new string[]
				{
					"interval_expression"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_Expr_IntervalExpression_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "expr#",
				m_vRHSSymbols = new string[]
				{
					"model_expression"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_Expr_ModelExpression_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "expr#",
				m_vRHSSymbols = new string[]
				{
					"multiset_expression"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_Expr_MultisetExpression_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "expr#",
				m_vRHSSymbols = new string[]
				{
					"object_access_expression"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_Expr_ObjectAccessExpression_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "expr#",
				m_vRHSSymbols = new string[]
				{
					"scalar_subquery_expression"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_Expr_ScalarSubqueryExpression_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "expr#",
				m_vRHSSymbols = new string[]
				{
					"simple_expression"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_Expr_SimpleExpression_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "expr#",
				m_vRHSSymbols = new string[]
				{
					"type_constructor_expression"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_Expr_TypeConstructorExpression_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_CompoundExpression_Expr_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_CompoundExpression_LEFT_PARENTHESIS_Expr_RIGHT_PARENTHESIS_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_CompoundExpression_Expr_CompoundExpression_Expr_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "compound_expression",
				m_vRHSSymbols = new string[]
				{
					"'*'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_CompoundExpression_GetIdentifier_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "compound_expression",
				m_vRHSSymbols = new string[]
				{
					"'+'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_CompoundExpression_GetIdentifier_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "compound_expression",
				m_vRHSSymbols = new string[]
				{
					"'-'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_CompoundExpression_GetIdentifier_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "compound_expression",
				m_vRHSSymbols = new string[]
				{
					"'/'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_CompoundExpression_GetIdentifier_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_CompoundExpression_CONCATENATE_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "compound_expression",
				m_vRHSSymbols = new string[]
				{
					"'PRIOR'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_CompoundExpression_GetIdentifier_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_DatetimeExpression_Expr_AT_DatetimeExpression_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "datetime_expression",
				m_vRHSSymbols = new string[]
				{
					"'LOCAL'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_DatetimeExpression_LOCAL_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_DatetimeExpression_TIME_ZONE_DatetimeExpression_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "datetime_expression",
				m_vRHSSymbols = new string[]
				{
					"'DBTIMEZONE'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_DatetimeExpression_DBTIMEZONE_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "datetime_expression",
				m_vRHSSymbols = new string[]
				{
					"expr"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_DatetimeExpression_Expr_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "datetime_expression",
				m_vRHSSymbols = new string[]
				{
					"string_literal"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_DatetimeExpression_StringLiteral_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "function_expression",
				m_vRHSSymbols = new string[]
				{
					"function"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_FunctionExpression_Function_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "function_expression",
				m_vRHSSymbols = new string[]
				{
					"function_call"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_FunctionExpression_FunctionCall_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "interval_expression",
				m_vRHSSymbols = new string[]
				{
					"interval_day_to_second"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_IntervalExpression_IntervalDayToSecond_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "interval_expression",
				m_vRHSSymbols = new string[]
				{
					"interval_year_to_month"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_IntervalExpression_IntervalYearToMonth_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_IntervalExpression_LEFT_PARENTHESIS_Expr_DASH_Expr_RIGHT_PARENTHESIS_IntervalExpression_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_IntervalExpression_DAY_TO_SECOND_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_IntervalExpression_YEAR_TO_MONTH_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_IntervalExpression_DAY_TO_SECOND_IntervalExpression_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_IntervalExpression_DAY_IntervalExpression_TO_SECOND_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_IntervalExpression_YEAR_IntervalExpression_TO_MONTH_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_IntervalExpression_DAY_IntervalExpression_TO_SECOND_IntervalExpression_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_IntervalExpression_LEFT_PARENTHESIS_Digits_RIGHT_PARENTHESIS_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_IntervalDayToSecond_INTERVAL_StringLiteral_IntervalDayToSecond_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_IntervalDayToSecond_INTERVAL_StringLiteral_IntervalDayToSecond_TO_IntervalDayToSecond_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_IntervalDayToSecond_LEFT_PARENTHESIS_Digits_RIGHT_PARENTHESIS_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_IntervalDayToSecond_LEFT_PARENTHESIS_Digits_COMMA_Digits_RIGHT_PARENTHESIS_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "interval_day_to_second",
				m_vRHSSymbols = new string[]
				{
					"'DAY'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_IntervalDayToSecond_GetIdentifier_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "interval_day_to_second",
				m_vRHSSymbols = new string[]
				{
					"'HOUR'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_IntervalDayToSecond_GetIdentifier_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "interval_day_to_second",
				m_vRHSSymbols = new string[]
				{
					"'MINUTE'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_IntervalDayToSecond_GetIdentifier_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "interval_day_to_second",
				m_vRHSSymbols = new string[]
				{
					"'SECOND'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_IntervalDayToSecond_GetIdentifier_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_IntervalDayToSecond_SECOND_IntervalDayToSecond_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "interval_day_to_second",
				m_vRHSSymbols = new string[]
				{
					"interval_day_to_second"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_IntervalDayToSecond_IntervalDayToSecond_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_IntervalDayToSecond_IntervalDayToSecond_IntervalDayToSecond_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_IntervalYearToMonth_INTERVAL_StringLiteral_IntervalYearToMonth_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_IntervalYearToMonth_INTERVAL_StringLiteral_IntervalYearToMonth_IntervalYearToMonth_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_IntervalYearToMonth_INTERVAL_StringLiteral_IntervalYearToMonth_TO_IntervalYearToMonth_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_IntervalYearToMonth_INTERVAL_StringLiteral_IntervalYearToMonth_IntervalYearToMonth_TO_IntervalYearToMonth_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "interval_year_to_month",
				m_vRHSSymbols = new string[]
				{
					"'MONTH'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_IntervalYearToMonth_GetIdentifier_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "interval_year_to_month",
				m_vRHSSymbols = new string[]
				{
					"'YEAR'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_IntervalYearToMonth_GetIdentifier_Rule),
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
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyExprNNRuleMultiProcessors.Process_IntervalYearToMonth_LEFT_PARENTHESIS_Digits_RIGHT_PARENTHESIS_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			}
		};
	}
}
