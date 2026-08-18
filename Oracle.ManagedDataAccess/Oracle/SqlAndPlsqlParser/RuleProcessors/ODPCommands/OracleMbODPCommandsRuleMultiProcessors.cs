using System;
using System.Collections.Generic;
using Oracle.SqlAndPlsqlParser.LocalParsing;
using OracleInternal.Common;

namespace Oracle.SqlAndPlsqlParser.RuleProcessors.ODPCommands
{
	// Token: 0x02000316 RID: 790
	internal static class OracleMbODPCommandsRuleMultiProcessors
	{
		// Token: 0x06001D00 RID: 7424 RVA: 0x0011DA68 File Offset: 0x0011BC68
		public static object Process_CompilationUnit_PragmaListOpt_StartWithRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001D01 RID: 7425 RVA: 0x0011DA90 File Offset: 0x0011BC90
		public static object Process_CompilationUnit_CompoundTrgBody_EndWithRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001D02 RID: 7426 RVA: 0x0011DAB8 File Offset: 0x0011BCB8
		public static object Process_CompilationUnit_LibraryUnit_EndWithRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001D03 RID: 7427 RVA: 0x0011DAE0 File Offset: 0x0011BCE0
		public static object Process_CompilationUnit_PkgBody_EndWithRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001D04 RID: 7428 RVA: 0x0011DB08 File Offset: 0x0011BD08
		public static object Process_Unprocessed_AllRules(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpStatement oracleLpStatement = null;
			OracleLpParserContext oracleLpParserContext = (OracleLpParserContext)ctx;
			if (oracleLpParserContext.CurrentStatement == null)
			{
				List<LexerToken> tokens = ctx.Tokens;
				ParseNode currentParseNode = ctx.CurrentParseNode;
				int vBegin = tokens[currentParseNode.From].m_vBegin;
				int vEnd = tokens[currentParseNode.To - 1].m_vEnd;
				OracleLpTextFragment oracleLpTextFragment = new OracleLpTextFragment(ctx.Script, vBegin, vEnd - vBegin);
				oracleLpParserContext.CurrentStatementText = oracleLpTextFragment;
				oracleLpStatement = new OracleLpStatement(oracleLpTextFragment, (IOracleMetadata)oracleLpParserContext.GetActiveObject(0));
				oracleLpParserContext.CurrentStatement = oracleLpStatement;
			}
			oracleLpParserContext.CurrentStatement = null;
			return oracleLpStatement;
		}

		// Token: 0x06001D05 RID: 7429 RVA: 0x0011DB9C File Offset: 0x0011BD9C
		public static object Process_SqlStatements_SqlStatement_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001D06 RID: 7430 RVA: 0x0011DBC4 File Offset: 0x0011BDC4
		public static object Process_SqlStatements_SqlStatements_SqlStatement_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			object obj = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx);
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx);
		}

		// Token: 0x06001D07 RID: 7431 RVA: 0x0011DC00 File Offset: 0x0011BE00
		public static object Process_SqlStatement_Create_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<LexerToken> tokens = ctx.Tokens;
			ParseNode currentParseNode = ctx.CurrentParseNode;
			int vBegin = tokens[currentParseNode.From].m_vBegin;
			int vEnd = tokens[currentParseNode.To - 1].m_vEnd;
			OracleLpParserContext oracleLpParserContext = (OracleLpParserContext)ctx;
			OracleLpTextFragment oracleLpTextFragment = new OracleLpTextFragment(ctx.Script, vBegin, vEnd - vBegin);
			oracleLpParserContext.CurrentStatementText = oracleLpTextFragment;
			OracleLpStatement currentStatement = new OracleLpCreateStatement(oracleLpTextFragment, (IOracleMetadata)oracleLpParserContext.GetActiveObject(0));
			oracleLpParserContext.CurrentStatement = currentStatement;
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("create");
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001D08 RID: 7432 RVA: 0x0011DCC0 File Offset: 0x0011BEC0
		public static object Process_SqlStatement_LibraryUnit_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
		}

		// Token: 0x06001D09 RID: 7433 RVA: 0x0011DCE8 File Offset: 0x0011BEE8
		public static object Process_SqlStatement_SqlPlusCommand_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("sqlplus_command");
			object result;
			if (ctx.CurrentRule.IsUnary)
			{
				result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			}
			else
			{
				result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode.Children()[ruleMatchPosition], 0, -1, ctx);
			}
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001D0A RID: 7434 RVA: 0x0011DD58 File Offset: 0x0011BF58
		public static object Process_SqlStatement_ExplainPlan_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpParserContext oracleLpParserContext = (OracleLpParserContext)ctx;
			List<LexerToken> tokens = ctx.Tokens;
			ParseNode currentParseNode = ctx.CurrentParseNode;
			int vBegin = tokens[currentParseNode.From].m_vBegin;
			int vEnd = tokens[currentParseNode.To - 1].m_vEnd;
			OracleLpTextFragment currentStatementText = new OracleLpTextFragment(ctx.Script, vBegin, vEnd - vBegin);
			oracleLpParserContext.CurrentStatementText = currentStatementText;
			OracleLpStatement oracleLpStatement = new OracleLpExplainPlanStatement(oracleLpParserContext.CurrentStatementText, (IOracleMetadata)oracleLpParserContext.GetActiveObject(0));
			oracleLpParserContext.CurrentStatement = oracleLpStatement;
			oracleLpParserContext.HandleBindVariables = true;
			OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			return oracleLpStatement;
		}

		// Token: 0x06001D0B RID: 7435 RVA: 0x0011DDFC File Offset: 0x0011BFFC
		public static object Process_LibraryUnit_AdtDefinition_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("adt_definition");
			object result;
			if (ctx.CurrentRule.IsUnary)
			{
				result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			}
			else
			{
				result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode.Children()[ruleMatchPosition], 0, -1, ctx);
			}
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001D0C RID: 7436 RVA: 0x0011DE6C File Offset: 0x0011C06C
		public static object Process_LibraryUnit_ForeignLibrarySpec_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("foreign_library_spec");
			object result;
			if (ctx.CurrentRule.IsUnary)
			{
				result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			}
			else
			{
				result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode.Children()[ruleMatchPosition], 0, -1, ctx);
			}
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001D0D RID: 7437 RVA: 0x0011DEDC File Offset: 0x0011C0DC
		public static object Process_LibraryUnit_LabeledBlockStmt_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<LexerToken> tokens = ctx.Tokens;
			ParseNode currentParseNode = ctx.CurrentParseNode;
			int vBegin = tokens[currentParseNode.From].m_vBegin;
			int vEnd = tokens[currentParseNode.To - 1].m_vEnd;
			((OracleLpParserContext)ctx).CurrentStatementText = new OracleLpTextFragment(ctx.Script, vBegin, vEnd - vBegin);
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("labeled_block_stmt");
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001D0E RID: 7438 RVA: 0x0011DF74 File Offset: 0x0011C174
		public static object Process_LibraryUnit_AssemblySpec_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("assembly_spec");
			object result;
			if (ctx.CurrentRule.IsUnary)
			{
				result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			}
			else
			{
				result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode.Children()[ruleMatchPosition], 0, -1, ctx);
			}
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001D0F RID: 7439 RVA: 0x0011DFE4 File Offset: 0x0011C1E4
		public static object Process_LibraryUnit_OperatorDefinition_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("operator_definition");
			object result;
			if (ctx.CurrentRule.IsUnary)
			{
				result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			}
			else
			{
				result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode.Children()[ruleMatchPosition], 0, -1, ctx);
			}
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001D10 RID: 7440 RVA: 0x0011E054 File Offset: 0x0011C254
		public static object Process_LibraryUnit_SubprgBody_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("subprg_body");
			object result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001D11 RID: 7441 RVA: 0x0011E09C File Offset: 0x0011C29C
		public static object Process_LibraryUnit_TableTypeDefinition_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("table_type_definition");
			object result;
			if (ctx.CurrentRule.IsUnary)
			{
				result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			}
			else
			{
				result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode.Children()[ruleMatchPosition], 0, -1, ctx);
			}
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001D12 RID: 7442 RVA: 0x0011E10C File Offset: 0x0011C30C
		public static object Process_LibraryUnit_UnlabeledBlockStmt_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("unlabeled_nonblock_stmt");
			object result;
			if (ctx.CurrentRule.IsUnary)
			{
				result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			}
			else
			{
				result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode.Children()[ruleMatchPosition], 0, -1, ctx);
			}
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001D13 RID: 7443 RVA: 0x0011E17C File Offset: 0x0011C37C
		public static object Process_LibraryUnit_CallStatement_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpParserContext oracleLpParserContext = (OracleLpParserContext)ctx;
			List<LexerToken> tokens = ctx.Tokens;
			ParseNode currentParseNode = ctx.CurrentParseNode;
			int vBegin = tokens[currentParseNode.From].m_vBegin;
			int vEnd = tokens[currentParseNode.To - 1].m_vEnd;
			OracleLpTextFragment currentStatementText = new OracleLpTextFragment(ctx.Script, vBegin, vEnd - vBegin);
			oracleLpParserContext.CurrentStatementText = currentStatementText;
			OracleLpStatement oracleLpStatement = new OracleLpCallStatement(oracleLpParserContext.CurrentStatementText, (IOracleMetadata)oracleLpParserContext.GetActiveObject(0));
			oracleLpParserContext.CurrentStatement = oracleLpStatement;
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("call_statement");
			if (ctx.CurrentRule.IsUnary)
			{
				OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			}
			else
			{
				OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode.Children()[ruleMatchPosition], 0, -1, ctx);
			}
			ctx.RuleProcessorTable = ruleProcessorTable;
			return oracleLpStatement;
		}

		// Token: 0x06001D14 RID: 7444 RVA: 0x0011E264 File Offset: 0x0011C464
		public static object Process_LibraryUnit_PkgSpec_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("pkg_spec");
			object result;
			if (ctx.CurrentRule.IsUnary)
			{
				result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			}
			else
			{
				result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode.Children()[ruleMatchPosition], 0, -1, ctx);
			}
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001D15 RID: 7445 RVA: 0x0011E2D4 File Offset: 0x0011C4D4
		public static object Process_LibraryUnit_SubprgI_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("subprg_i");
			object result;
			if (ctx.CurrentRule.IsUnary)
			{
				result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			}
			else
			{
				result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode.Children()[ruleMatchPosition], 0, -1, ctx);
			}
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001D16 RID: 7446 RVA: 0x0011E344 File Offset: 0x0011C544
		public static object Process_LibraryUnit_LabelListOpt_PartialRule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("label_list_opt");
			object result;
			if (ctx.CurrentRule.IsUnary)
			{
				result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, ctx.CurrentRuleIndex + 1, -1, ctx);
			}
			else
			{
				result = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode.Children()[ruleMatchPosition], 0, -1, ctx);
			}
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x04001D6B RID: 7531
		public static OracleMbEarleyRuleMultiProcessorAddItem[] s_vRuleProcessorItems = new OracleMbEarleyRuleMultiProcessorAddItem[]
		{
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "compilation_unit",
				m_vRHSSymbols = new string[]
				{
					"pragma_list_opt"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbODPCommandsRuleMultiProcessors.Process_CompilationUnit_PragmaListOpt_StartWithRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllRulesStartingWith
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "compilation_unit",
				m_vRHSSymbols = new string[]
				{
					"compound_trg_body"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbODPCommandsRuleMultiProcessors.Process_CompilationUnit_CompoundTrgBody_EndWithRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllRulesEndingWith
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "compilation_unit",
				m_vRHSSymbols = new string[]
				{
					"library_unit"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbODPCommandsRuleMultiProcessors.Process_CompilationUnit_LibraryUnit_EndWithRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllRulesEndingWith
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "compilation_unit",
				m_vRHSSymbols = new string[]
				{
					"pkg_body"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbODPCommandsRuleMultiProcessors.Process_CompilationUnit_PkgBody_EndWithRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllRulesEndingWith
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "compilation_unit",
				m_vRHSSymbols = new string[0],
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbODPCommandsRuleMultiProcessors.Process_Unprocessed_AllRules),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "sql_statements",
				m_vRHSSymbols = new string[]
				{
					"sql_statement"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbODPCommandsRuleMultiProcessors.Process_SqlStatements_SqlStatement_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "sql_statements",
				m_vRHSSymbols = new string[]
				{
					"sql_statements",
					"sql_statement"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbODPCommandsRuleMultiProcessors.Process_SqlStatements_SqlStatements_SqlStatement_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "sql_statement",
				m_vRHSSymbols = new string[]
				{
					"create"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbODPCommandsRuleMultiProcessors.Process_SqlStatement_Create_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "sql_statement",
				m_vRHSSymbols = new string[]
				{
					"library_unit"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbODPCommandsRuleMultiProcessors.Process_SqlStatement_LibraryUnit_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "sql_statement",
				m_vRHSSymbols = new string[]
				{
					"explain_plan"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbODPCommandsRuleMultiProcessors.Process_SqlStatement_ExplainPlan_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "sql_statement",
				m_vRHSSymbols = new string[]
				{
					"sqlplus_command"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbODPCommandsRuleMultiProcessors.Process_SqlStatement_SqlPlusCommand_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "sql_statement",
				m_vRHSSymbols = new string[0],
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbODPCommandsRuleMultiProcessors.Process_Unprocessed_AllRules),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "library_unit",
				m_vRHSSymbols = new string[]
				{
					"adt_definition"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbODPCommandsRuleMultiProcessors.Process_LibraryUnit_AdtDefinition_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "library_unit",
				m_vRHSSymbols = new string[]
				{
					"assembly_spec"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbODPCommandsRuleMultiProcessors.Process_LibraryUnit_AssemblySpec_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "library_unit",
				m_vRHSSymbols = new string[]
				{
					"foreign_library_spec"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbODPCommandsRuleMultiProcessors.Process_LibraryUnit_ForeignLibrarySpec_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "library_unit",
				m_vRHSSymbols = new string[]
				{
					"labeled_block_stmt"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbODPCommandsRuleMultiProcessors.Process_LibraryUnit_LabeledBlockStmt_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "library_unit",
				m_vRHSSymbols = new string[]
				{
					"operator_definition"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbODPCommandsRuleMultiProcessors.Process_LibraryUnit_OperatorDefinition_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "library_unit",
				m_vRHSSymbols = new string[]
				{
					"subprg_body"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbODPCommandsRuleMultiProcessors.Process_LibraryUnit_SubprgBody_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "library_unit",
				m_vRHSSymbols = new string[]
				{
					"table_type_definition"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbODPCommandsRuleMultiProcessors.Process_LibraryUnit_TableTypeDefinition_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "library_unit",
				m_vRHSSymbols = new string[]
				{
					"unlabeled_nonblock_stmt"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbODPCommandsRuleMultiProcessors.Process_LibraryUnit_UnlabeledBlockStmt_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "library_unit",
				m_vRHSSymbols = new string[]
				{
					"call_statement"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbODPCommandsRuleMultiProcessors.Process_LibraryUnit_CallStatement_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "library_unit",
				m_vRHSSymbols = new string[]
				{
					"pkg_spec"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbODPCommandsRuleMultiProcessors.Process_LibraryUnit_PkgSpec_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "library_unit",
				m_vRHSSymbols = new string[]
				{
					"subprg_i"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbODPCommandsRuleMultiProcessors.Process_LibraryUnit_SubprgI_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "library_unit",
				m_vRHSSymbols = new string[]
				{
					"label_list_opt"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbODPCommandsRuleMultiProcessors.Process_LibraryUnit_LabelListOpt_PartialRule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.AllPartialMatchingRules
			}
		};
	}
}
