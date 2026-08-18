using System;
using System.Collections.Generic;
using Oracle.SqlAndPlsqlParser.LocalParsing;
using OracleInternal.Common;

namespace Oracle.SqlAndPlsqlParser.RuleProcessors.ODPCommands
{
	// Token: 0x0200030D RID: 781
	internal static class OracleMbEarleyLabeledBlockStmtRuleMultiProcessors
	{
		// Token: 0x06001C2E RID: 7214 RVA: 0x00114BF4 File Offset: 0x00112DF4
		public static object Process_LabeledBlockStmt_BlockStmt_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpParserContext oracleLpParserContext = (OracleLpParserContext)ctx;
			OracleLpStatement oracleLpStatement = new OracleLpBlockStatement(oracleLpParserContext.CurrentStatementText, (IOracleMetadata)oracleLpParserContext.GetActiveObject(0));
			oracleLpParserContext.CurrentStatement = oracleLpStatement;
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("block_stmt");
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return oracleLpStatement;
		}

		// Token: 0x06001C2F RID: 7215 RVA: 0x00114C60 File Offset: 0x00112E60
		public static object Process_LabeledBlockStmt_LabelListOpt_BlockStmt_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpParserContext oracleLpParserContext = (OracleLpParserContext)ctx;
			OracleLpStatement oracleLpStatement = new OracleLpBlockStatement(oracleLpParserContext.CurrentStatementText, (IOracleMetadata)oracleLpParserContext.GetActiveObject(0));
			oracleLpParserContext.CurrentStatement = oracleLpStatement;
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("label_list_opt");
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx);
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("block_stmt");
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return oracleLpStatement;
		}

		// Token: 0x04001D62 RID: 7522
		public static OracleMbEarleyRuleMultiProcessorAddItem[] s_vRuleProcessorItems = new OracleMbEarleyRuleMultiProcessorAddItem[]
		{
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "labeled_block_stmt",
				m_vRHSSymbols = new string[]
				{
					"block_stmt"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyLabeledBlockStmtRuleMultiProcessors.Process_LabeledBlockStmt_BlockStmt_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "labeled_block_stmt",
				m_vRHSSymbols = new string[]
				{
					"label_list_opt",
					"block_stmt"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyLabeledBlockStmtRuleMultiProcessors.Process_LabeledBlockStmt_LabelListOpt_BlockStmt_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			}
		};
	}
}
