using System;
using System.Collections.Generic;
using Oracle.SqlAndPlsqlParser.LocalParsing;

namespace Oracle.SqlAndPlsqlParser.RuleProcessors.ODPCommands
{
	// Token: 0x020001FA RID: 506
	internal static class OracleMbEarleyDbLinkRuleMultiProcessors
	{
		// Token: 0x06001251 RID: 4689 RVA: 0x000C5184 File Offset: 0x000C3384
		public static object Process_Dblink_DblinkAlts_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			return OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx);
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x000C51B0 File Offset: 0x000C33B0
		public static object Process_Dblink_Identifier_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			return new OracleLpDbLink
			{
				Database = new OracleLpName(ctx.Tokens[ctx.CurrentParseNode.From].m_vContent)
			};
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x000C51EC File Offset: 0x000C33EC
		public static object Process_Dblink_Identifier_Dblink_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpDbLink oracleLpDbLink = new OracleLpDbLink();
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			oracleLpDbLink.Database = new OracleLpName(ctx.Tokens[list[0].From].m_vContent);
			string text = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx) as string;
			oracleLpDbLink.Domain = new OracleLpName(text.Substring(1));
			return oracleLpDbLink;
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x000C525C File Offset: 0x000C345C
		public static object Process_Dblink_Identifier_AT_Identifier_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpDbLink oracleLpDbLink = new OracleLpDbLink();
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			oracleLpDbLink.Database = new OracleLpName(ctx.Tokens[list[0].From].m_vContent);
			oracleLpDbLink.ConnectionQualifier = new OracleLpName(ctx.Tokens[list[2].From].m_vContent);
			return oracleLpDbLink;
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x000C52CC File Offset: 0x000C34CC
		public static object Process_Dblink_Identifier_Dblink_AT_Identifier_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			OracleLpDbLink oracleLpDbLink = new OracleLpDbLink();
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			oracleLpDbLink.Database = new OracleLpName(ctx.Tokens[list[0].From].m_vContent);
			string text = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[1], 0, -1, ctx) as string;
			oracleLpDbLink.Domain = new OracleLpName(text.Substring(1));
			oracleLpDbLink.ConnectionQualifier = new OracleLpName(ctx.Tokens[list[3].From].m_vContent);
			return oracleLpDbLink;
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x000C5364 File Offset: 0x000C3564
		public static object Process_Dblink_DOT_Identifier_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			return "." + ctx.Tokens[list[1].From].m_vContent;
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x000C53A4 File Offset: 0x000C35A4
		public static object Process_Dblink_Dblink_DOT_Identifier_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			string str = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx) as string;
			return str + "." + ctx.Tokens[list[2].From].m_vContent;
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x000C53FC File Offset: 0x000C35FC
		public static object Process_DblinkAlts_DottedName_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			object result = null;
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("dotted_name");
			string[] array = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(ctx.CurrentParseNode, 0, -1, ctx) as string[];
			if (array.Length > 2)
			{
				result = string.Concat(new string[]
				{
					array[0],
					".",
					array[1],
					".",
					array[2]
				});
			}
			else if (array.Length > 1)
			{
				result = array[0] + "." + array[1];
			}
			else if (array.Length > 0)
			{
				result = array[0];
			}
			ctx.RuleProcessorTable = ruleProcessorTable;
			return result;
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x000C549C File Offset: 0x000C369C
		public static object Process_DblinkAlts_AT_Identifier_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			return "@" + ctx.Tokens[list[1].From].m_vContent;
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x000C54DC File Offset: 0x000C36DC
		public static object Process_DblinkAlts_DottedName_AT_Identifier_Rule(OracleMbEarleyParserMultiContext ctx, int ruleMatchPosition)
		{
			string str = string.Empty;
			List<ParseNode> list = ctx.CurrentParseNode.Children();
			OracleMbEarleyRuleMultiProcessorTable ruleProcessorTable = ctx.RuleProcessorTable;
			ctx.RuleProcessorTable = ctx.GetRuleProcessorTable("dotted_name");
			string[] array = OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(list[0], 0, -1, ctx) as string[];
			if (array.Length > 2)
			{
				str = string.Concat(new string[]
				{
					array[0],
					".",
					array[1],
					".",
					array[2]
				});
			}
			else if (array.Length > 1)
			{
				str = array[0] + "." + array[1];
			}
			else if (array.Length > 0)
			{
				str = array[0];
			}
			ctx.RuleProcessorTable = ruleProcessorTable;
			return str + "@" + ctx.Tokens[list[2].From].m_vContent;
		}

		// Token: 0x04001445 RID: 5189
		public static OracleMbEarleyRuleMultiProcessorAddItem[] s_vRuleProcessorItems = new OracleMbEarleyRuleMultiProcessorAddItem[]
		{
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "dblink",
				m_vRHSSymbols = new string[]
				{
					"identifier"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyDbLinkRuleMultiProcessors.Process_Dblink_Identifier_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "dblink",
				m_vRHSSymbols = new string[]
				{
					"identifier",
					"dblink"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyDbLinkRuleMultiProcessors.Process_Dblink_Identifier_Dblink_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "dblink",
				m_vRHSSymbols = new string[]
				{
					"identifier",
					"'@'",
					"identifier"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyDbLinkRuleMultiProcessors.Process_Dblink_Identifier_AT_Identifier_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "dblink",
				m_vRHSSymbols = new string[]
				{
					"identifier",
					"dblink",
					"'@'",
					"identifier"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyDbLinkRuleMultiProcessors.Process_Dblink_Identifier_Dblink_AT_Identifier_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "dblink",
				m_vRHSSymbols = new string[]
				{
					"'.'",
					"identifier"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyDbLinkRuleMultiProcessors.Process_Dblink_DOT_Identifier_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			},
			new OracleMbEarleyRuleMultiProcessorAddItem
			{
				m_vHeadSymbol = "dblink",
				m_vRHSSymbols = new string[]
				{
					"dblink",
					"'.'",
					"identifier"
				},
				m_vRuleProcessor = new OracleMbEarleyRuleMultiProcessorDelegate(OracleMbEarleyDbLinkRuleMultiProcessors.Process_Dblink_Dblink_DOT_Identifier_Rule),
				m_vAddType = OracleMbEarleyRuleMultiProcessorAddType.SpecificRule
			}
		};
	}
}
