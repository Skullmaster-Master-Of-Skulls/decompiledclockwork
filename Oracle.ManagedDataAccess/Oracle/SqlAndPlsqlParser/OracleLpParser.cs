using System;
using System.Collections.Generic;
using System.Text;
using Oracle.SqlAndPlsqlParser.LocalParsing;
using Oracle.SqlAndPlsqlParser.RuleProcessors;
using OracleInternal.Common;

namespace Oracle.SqlAndPlsqlParser
{
	// Token: 0x02000261 RID: 609
	public class OracleLpParser
	{
		// Token: 0x06001882 RID: 6274 RVA: 0x00102FE0 File Offset: 0x001011E0
		internal OracleLpParser(OracleMbEarleySqlPlsqlRuleProcessorTableDictionary rulesProcessorTableDictionary)
		{
			this.m_vRulesProcessorTableDictionary = rulesProcessorTableDictionary;
			this.m_vParser = new OracleSqlEarley(this.m_vRulesProcessorTableDictionary.Grammar);
		}

		// Token: 0x06001883 RID: 6275 RVA: 0x00103008 File Offset: 0x00101208
		public OracleLpParser(int type = 0) : this((type == 0) ? OracleMbEarleySqlPlsqlAnsiRuleProcessorTableDictionary.Instance : OracleMbEarleySqlPlsqlRuleProcessorTableDictionary.Instance)
		{
		}

		// Token: 0x06001884 RID: 6276 RVA: 0x00103020 File Offset: 0x00101220
		public string TestParse(string s)
		{
			ParseNode parseNode = null;
			List<LexerToken> list = null;
			StringBuilder stringBuilder = null;
			try
			{
				using (new PerformanceTimer("Lexing"))
				{
					list = LexerToken.Parse(s);
				}
				using (new PerformanceTimer("Parsing"))
				{
					parseNode = this.m_vParser.Parse(s, list);
				}
				if (parseNode != null)
				{
					return parseNode.ToString(this.m_vParser.EarleyGrammar);
				}
				stringBuilder = new StringBuilder("Parse failed\n");
			}
			catch (ParserException ex)
			{
				stringBuilder = new StringBuilder(ex.Message);
			}
			int num = 0;
			stringBuilder.Append("\nTokenizer output:\n =========================\n");
			if (list != null)
			{
				foreach (LexerToken lexerToken in list)
				{
					stringBuilder.Append(lexerToken.ToString());
					stringBuilder.Append('\n');
					if (num++ == 200)
					{
						stringBuilder.Append(string.Format("... {0} more tokens not printed\n", list.Count - 200));
						break;
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001885 RID: 6277 RVA: 0x00103178 File Offset: 0x00101378
		public IEnumerable<OracleLpStatement> ParseStatements(IOracleMetadata md, string s)
		{
			ParseNode parseNode = null;
			List<LexerToken> list = null;
			using (new PerformanceTimer("Lexing"))
			{
				list = LexerToken.Parse(s);
			}
			using (new PerformanceTimer("Parsing"))
			{
				parseNode = this.m_vParser.Parse(s, list);
			}
			IEnumerable<OracleLpStatement> result;
			using (new PerformanceTimer("Statement information retrieval"))
			{
				if (parseNode != null)
				{
					OracleLpParserContext oracleLpParserContext = new OracleLpParserContext(this.m_vParser, this.m_vRulesProcessorTableDictionary);
					oracleLpParserContext.SetActiveObject(0, md);
					oracleLpParserContext.Script = s;
					oracleLpParserContext.Tokens = list;
					oracleLpParserContext.RootParseNode = parseNode;
					oracleLpParserContext.CurrentParseNode = parseNode;
					oracleLpParserContext.PropertiesBag.DefaultSchemaName = "";
					OracleMbEarleyRuleMultiProcessor.ProcessNodeRules(parseNode, oracleLpParserContext.CurrentRuleIndex, -1, oracleLpParserContext);
					IEnumerable<OracleLpStatement> statements = oracleLpParserContext.Statements;
					oracleLpParserContext.Clear();
					result = statements;
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x04001AE3 RID: 6883
		private OracleSqlEarley m_vParser;

		// Token: 0x04001AE4 RID: 6884
		private OracleMbEarleySqlPlsqlRuleProcessorTableDictionary m_vRulesProcessorTableDictionary;
	}
}
