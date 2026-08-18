using System;
using System.Collections.Generic;
using Oracle.SqlAndPlsqlParser.LocalParsing;

namespace Oracle.SqlAndPlsqlParser.RuleProcessors.ODPCommands
{
	// Token: 0x02000305 RID: 773
	internal static class OracleMbEarleyBindVarRuleMultiProcessors
	{
		// Token: 0x06001B88 RID: 7048 RVA: 0x0010DB74 File Offset: 0x0010BD74
		public static object Process_BindVar_QUESTIONMARK_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpParserContext oracleLpParserContext = (OracleLpParserContext)ctx;
			OracleLpBindParameter oracleLpBindParameter = null;
			if (!oracleLpParserContext.CurrentStatementBindVarParseNodes.TryGetValue(ctx.CurrentParseNode, out oracleLpBindParameter))
			{
				List<LexerToken> tokens = ctx.Tokens;
				ParseNode currentParseNode = ctx.CurrentParseNode;
				int vBegin = tokens[currentParseNode.From].m_vBegin;
				OracleLpRelativeTextFragment relativeTextFragment = oracleLpParserContext.CurrentStatementText.GetRelativeTextFragment(vBegin, 1);
				OracleLpStatement currentStatement = oracleLpParserContext.CurrentStatement;
				oracleLpBindParameter = new OracleLpBindParameter(currentStatement, relativeTextFragment, ++oracleLpParserContext.CurrentStatementBindVarCount, OracleLpBindParameterType.Positional);
				oracleLpBindParameter.ParentClause = oracleLpParserContext.CurrentStatementClause;
				currentStatement.AddParameter(oracleLpBindParameter);
				oracleLpParserContext.CurrentStatementBindVarParseNodes[ctx.CurrentParseNode] = oracleLpBindParameter;
			}
			return oracleLpBindParameter;
		}

		// Token: 0x06001B89 RID: 7049 RVA: 0x0010DC20 File Offset: 0x0010BE20
		public static object Process_BindVar_COLON_Digits_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpParserContext oracleLpParserContext = (OracleLpParserContext)ctx;
			OracleLpBindParameter oracleLpBindParameter = null;
			if (!oracleLpParserContext.CurrentStatementBindVarParseNodes.TryGetValue(ctx.CurrentParseNode, out oracleLpBindParameter))
			{
				List<ParseNode> list = ctx.CurrentParseNode.Children();
				List<LexerToken> tokens = ctx.Tokens;
				ParseNode parseNode = list[1];
				int vBegin = tokens[parseNode.From].m_vBegin;
				int vEnd = tokens[parseNode.To - 1].m_vEnd;
				OracleLpRelativeTextFragment relativeTextFragment = oracleLpParserContext.CurrentStatementText.GetRelativeTextFragment(vBegin, vEnd - vBegin);
				OracleLpStatement currentStatement = oracleLpParserContext.CurrentStatement;
				oracleLpBindParameter = new OracleLpBindParameter(currentStatement, relativeTextFragment, ++oracleLpParserContext.CurrentStatementBindVarCount, OracleLpBindParameterType.NamedOrPositional);
				oracleLpBindParameter.ParentClause = oracleLpParserContext.CurrentStatementClause;
				currentStatement.AddParameter(oracleLpBindParameter);
				oracleLpParserContext.CurrentStatementBindVarParseNodes[ctx.CurrentParseNode] = oracleLpBindParameter;
			}
			return oracleLpBindParameter;
		}

		// Token: 0x06001B8A RID: 7050 RVA: 0x0010DCF8 File Offset: 0x0010BEF8
		public static object Process_BindVar_COLON_Identifier_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpParserContext oracleLpParserContext = (OracleLpParserContext)ctx;
			OracleLpBindParameter oracleLpBindParameter = null;
			if (!oracleLpParserContext.CurrentStatementBindVarParseNodes.TryGetValue(ctx.CurrentParseNode, out oracleLpBindParameter))
			{
				List<ParseNode> list = ctx.CurrentParseNode.Children();
				List<LexerToken> tokens = ctx.Tokens;
				ParseNode parseNode = list[1];
				int vBegin = tokens[parseNode.From].m_vBegin;
				int vEnd = tokens[parseNode.To - 1].m_vEnd;
				OracleLpRelativeTextFragment relativeTextFragment = oracleLpParserContext.CurrentStatementText.GetRelativeTextFragment(vBegin, vEnd - vBegin);
				OracleLpStatement currentStatement = oracleLpParserContext.CurrentStatement;
				oracleLpBindParameter = new OracleLpBindParameter(currentStatement, relativeTextFragment, ++oracleLpParserContext.CurrentStatementBindVarCount, OracleLpBindParameterType.NamedOrPositional);
				oracleLpBindParameter.ParentClause = oracleLpParserContext.CurrentStatementClause;
				currentStatement.AddParameter(oracleLpBindParameter);
				oracleLpParserContext.CurrentStatementBindVarParseNodes[ctx.CurrentParseNode] = oracleLpBindParameter;
			}
			return oracleLpBindParameter;
		}

		// Token: 0x06001B8B RID: 7051 RVA: 0x0010DDD0 File Offset: 0x0010BFD0
		public static object Process_BindVar_COLON_Identifier_DOT_Identifier_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpParserContext oracleLpParserContext = (OracleLpParserContext)ctx;
			OracleLpBindParameter oracleLpBindParameter = null;
			if (!oracleLpParserContext.CurrentStatementBindVarParseNodes.TryGetValue(ctx.CurrentParseNode, out oracleLpBindParameter))
			{
				List<ParseNode> list = ctx.CurrentParseNode.Children();
				List<LexerToken> tokens = ctx.Tokens;
				int vBegin = tokens[list[1].From].m_vBegin;
				int vEnd = tokens[list[3].To - 1].m_vEnd;
				OracleLpRelativeTextFragment relativeTextFragment = oracleLpParserContext.CurrentStatementText.GetRelativeTextFragment(vBegin, vEnd - vBegin);
				OracleLpStatement currentStatement = oracleLpParserContext.CurrentStatement;
				oracleLpBindParameter = new OracleLpBindParameter(currentStatement, relativeTextFragment, ++oracleLpParserContext.CurrentStatementBindVarCount, OracleLpBindParameterType.NamedOrPositional);
				oracleLpBindParameter.ParentClause = oracleLpParserContext.CurrentStatementClause;
				currentStatement.AddParameter(oracleLpBindParameter);
				oracleLpParserContext.CurrentStatementBindVarParseNodes[ctx.CurrentParseNode] = oracleLpBindParameter;
			}
			return oracleLpBindParameter;
		}

		// Token: 0x04001D5A RID: 7514
		public static OracleMbEarleyRuleMultiProcessorAddItem[] s_vRuleProcessorItems = new OracleMbEarleyRuleMultiProcessorAddItem[]
		{
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "bind_var",
				m_vRHSSymbols = new string[]
				{
					"'?'"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyBindVarRuleMultiProcessors.Process_BindVar_QUESTIONMARK_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "bind_var",
				m_vRHSSymbols = new string[]
				{
					"':'",
					"digits"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyBindVarRuleMultiProcessors.Process_BindVar_COLON_Digits_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "bind_var",
				m_vRHSSymbols = new string[]
				{
					"':'",
					"identifier"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyBindVarRuleMultiProcessors.Process_BindVar_COLON_Identifier_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "data_item",
				m_vRHSSymbols = new string[]
				{
					"':'",
					"identifier"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyBindVarRuleMultiProcessors.Process_BindVar_COLON_Identifier_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "bind_var",
				m_vRHSSymbols = new string[]
				{
					"':'",
					"identifier",
					"'.'",
					"identifier"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyBindVarRuleMultiProcessors.Process_BindVar_COLON_Identifier_DOT_Identifier_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			}
		};
	}
}
