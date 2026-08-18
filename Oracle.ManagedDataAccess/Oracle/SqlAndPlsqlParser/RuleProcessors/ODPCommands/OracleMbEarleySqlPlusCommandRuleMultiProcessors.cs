using System;
using System.Collections.Generic;
using Oracle.SqlAndPlsqlParser.LocalParsing;
using OracleInternal.Common;

namespace Oracle.SqlAndPlsqlParser.RuleProcessors.ODPCommands
{
	// Token: 0x020001FD RID: 509
	internal static class OracleMbEarleySqlPlusCommandRuleMultiProcessors
	{
		// Token: 0x060012E9 RID: 4841 RVA: 0x000CAFF4 File Offset: 0x000C91F4
		public static object Process_SqlPlusCommand_SqlPlusCommandNN_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
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

		// Token: 0x060012EA RID: 4842 RVA: 0x000CB044 File Offset: 0x000C9244
		public static object Process_SqlPlusCommandNN_EXECUTE_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpParserContext oracleLpParserContext = (OracleLpParserContext)ctx;
			List<LexerToken> tokens = ctx.Tokens;
			ParseNode currentParseNode = ctx.CurrentParseNode;
			int vBegin = tokens[currentParseNode.From].m_vBegin;
			int vEnd = tokens[currentParseNode.To - 1].m_vEnd;
			OracleLpTextFragment currentStatementText = new OracleLpTextFragment(ctx.Script, vBegin, vEnd - vBegin);
			oracleLpParserContext.CurrentStatementText = currentStatementText;
			OracleLpStatement oracleLpStatement = new OracleLpExecuteStatement(oracleLpParserContext.CurrentStatementText, (IOracleMetadata)oracleLpParserContext.GetActiveObject(0));
			oracleLpParserContext.CurrentStatement = oracleLpStatement;
			oracleLpParserContext.HandleBindVariables = true;
			return oracleLpStatement;
		}

		// Token: 0x04001448 RID: 5192
		public static OracleMbEarleyRuleMultiProcessorAddItem[] s_vRuleProcessorItems = new OracleMbEarleyRuleMultiProcessorAddItem[]
		{
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "sqlplus_command",
				m_vRHSSymbols = new string[]
				{
					"sqlplus_command#"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySqlPlusCommandRuleMultiProcessors.Process_SqlPlusCommand_SqlPlusCommandNN_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllRulesStartingWith
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "sqlplus_command#",
				m_vRHSSymbols = new string[]
				{
					"EXECUTE"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleySqlPlusCommandRuleMultiProcessors.Process_SqlPlusCommandNN_EXECUTE_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			}
		};
	}
}
