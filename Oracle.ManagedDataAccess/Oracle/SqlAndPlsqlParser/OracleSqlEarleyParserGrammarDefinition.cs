using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Reflection;

namespace Oracle.SqlAndPlsqlParser
{
	// Token: 0x02000278 RID: 632
	internal class OracleSqlEarleyParserGrammarDefinition : EarleyParserGrammarDefinition
	{
		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x060018E9 RID: 6377 RVA: 0x00107020 File Offset: 0x00105220
		public static OracleSqlEarleyParserGrammarDefinition Instance
		{
			get
			{
				lock (OracleSqlEarleyParserGrammarDefinition.m_vObjectLock)
				{
					if (OracleSqlEarleyParserGrammarDefinition.s_vInstance == null)
					{
						OracleSqlEarleyParserGrammarDefinition.s_vInstance = new OracleSqlEarleyParserGrammarDefinition(OracleSqlEarleyParserGrammarDefinition.GetRulesSet("Oracle.ManagedDataAccess.src.SqlParser.Resources.SQLPLSQL.zip"));
					}
				}
				return OracleSqlEarleyParserGrammarDefinition.s_vInstance;
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x060018EA RID: 6378 RVA: 0x0010707C File Offset: 0x0010527C
		public override OracleMbEarleyRulesPriorityDescriptor[] RulesPriorityDescriptors
		{
			get
			{
				return OracleSqlEarleyParserGrammarDefinition.s_cRulesPriorityDescriptors;
			}
		}

		// Token: 0x060018EB RID: 6379 RVA: 0x00107084 File Offset: 0x00105284
		protected OracleSqlEarleyParserGrammarDefinition(Set<RuleTuple> rules) : base(rules)
		{
			this.m_vIdentifierSymbolIndex = this.m_vSymbolIndexes["identifier"];
			this.m_vStringLiteralSymbolIndex = this.m_vSymbolIndexes["string_literal"];
			this.m_vDigitsSymbolIndex = this.m_vSymbolIndexes["digits"];
		}

		// Token: 0x060018EC RID: 6380 RVA: 0x001070DC File Offset: 0x001052DC
		protected override void InitializeSpecialWords()
		{
			ParserGrammarReservedWordsAndKeywords parserGrammarReservedWordsAndKeywords = OracleSqlGrammarReservedWordsAndKeywords.s_vInstance;
			foreach (string key in parserGrammarReservedWordsAndKeywords.ReservedWords)
			{
				int num = this.m_vQuotedSymbolIndexes[key];
				if (num >= 0)
				{
					this.m_vSymbolsFlags[num] |= 288;
				}
			}
			foreach (string key2 in parserGrammarReservedWordsAndKeywords.Keywords)
			{
				int num = this.m_vQuotedSymbolIndexes[key2];
				if (num >= 0)
				{
					this.m_vSymbolsFlags[num] |= 272;
				}
			}
			parserGrammarReservedWordsAndKeywords = OraclePlsqlGrammarReservedWordsAndKeywords.s_vInstance;
			foreach (string key3 in parserGrammarReservedWordsAndKeywords.ReservedWords)
			{
				int num = this.m_vQuotedSymbolIndexes[key3];
				if (num >= 0)
				{
					this.m_vSymbolsFlags[num] |= 544;
				}
			}
			foreach (string key4 in parserGrammarReservedWordsAndKeywords.Keywords)
			{
				int num = this.m_vQuotedSymbolIndexes[key4];
				if (num >= 0)
				{
					this.m_vSymbolsFlags[num] |= 528;
				}
			}
		}

		// Token: 0x060018ED RID: 6381 RVA: 0x00107238 File Offset: 0x00105438
		public override bool LookaheadOK(int lookAheadSymbol, int symbolIndex, bool symbolCouldBeIdentifier)
		{
			if (lookAheadSymbol < 0 || symbolIndex < 0)
			{
				return true;
			}
			if ((this.m_vSymbolsFlags[lookAheadSymbol] & 1) == 0)
			{
				return this.CanBePrediction(lookAheadSymbol, symbolIndex, symbolCouldBeIdentifier);
			}
			return lookAheadSymbol == symbolIndex || (symbolCouldBeIdentifier && lookAheadSymbol == this.m_vIdentifierSymbolIndex);
		}

		// Token: 0x060018EE RID: 6382 RVA: 0x00107270 File Offset: 0x00105470
		public override bool CanBePrediction(int symbol, int firstSymbol, bool firstSymbolCouldBeIdentifier)
		{
			HashSet<int> hashSet = this.m_vHasPathToFirstSymbolTable[symbol];
			return hashSet.Contains(firstSymbol) || (firstSymbolCouldBeIdentifier && hashSet.Contains(this.m_vIdentifierSymbolIndex));
		}

		// Token: 0x060018EF RID: 6383 RVA: 0x001072A4 File Offset: 0x001054A4
		public override void GetTokensInfo(LexerToken token, out int symbolIndex, out bool symbolCouldBeIdentifier)
		{
			symbolCouldBeIdentifier = false;
			switch (token.m_vType)
			{
			case Token.QUOTED_STRING:
				symbolIndex = this.m_vStringLiteralSymbolIndex;
				return;
			case Token.DQUOTED_STRING:
				symbolIndex = this.m_vIdentifierSymbolIndex;
				return;
			case Token.DIGITS:
				symbolIndex = this.m_vDigitsSymbolIndex;
				return;
			case Token.OPERATION:
				symbolIndex = this.m_vQuotedSymbolIndexes[token.m_vContent];
				return;
			case Token.IDENTIFIER:
				symbolIndex = this.m_vQuotedSymbolIndexes[token.m_vContent];
				if (symbolIndex < 0)
				{
					symbolIndex = this.m_vIdentifierSymbolIndex;
					return;
				}
				if ((this.m_vSymbolsFlags[symbolIndex] & 32) == 0)
				{
					symbolCouldBeIdentifier = true;
					return;
				}
				return;
			}
			symbolIndex = -1;
		}

		// Token: 0x060018F0 RID: 6384 RVA: 0x00107344 File Offset: 0x00105544
		protected static Set<RuleTuple> GetRulesSet(string resource)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			Set<RuleTuple> result;
			using (Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(resource))
			{
				using (GZipStream gzipStream = new GZipStream(manifestResourceStream, CompressionMode.Decompress))
				{
					MemoryStream memoryStream = new MemoryStream();
					gzipStream.CopyTo(memoryStream);
					memoryStream.Position = 0L;
					InputStream inputStream = new InputStream(memoryStream);
					result = ((inputStream != null) ? RuleTuple.ReadUnifiedRules(inputStream) : null);
				}
			}
			return result;
		}

		// Token: 0x04001B5E RID: 7006
		internal int m_vIdentifierSymbolIndex;

		// Token: 0x04001B5F RID: 7007
		protected int m_vStringLiteralSymbolIndex;

		// Token: 0x04001B60 RID: 7008
		protected int m_vDigitsSymbolIndex;

		// Token: 0x04001B61 RID: 7009
		private static readonly object m_vObjectLock = new object();

		// Token: 0x04001B62 RID: 7010
		private static OracleSqlEarleyParserGrammarDefinition s_vInstance = null;

		// Token: 0x04001B63 RID: 7011
		private static OracleMbEarleyRulesPriorityDescriptor[] s_cRulesPriorityDescriptors = new OracleMbEarleyRulesPriorityDescriptor[]
		{
			new OracleMbEarleyRulesPriorityDescriptor
			{
				m_vHeadSymbol = "expr",
				m_vFirstRHSSymbols = new string[]
				{
					"type_constructor_expression",
					"object_access_expression",
					"function_expression",
					"simple_expression"
				}
			},
			new OracleMbEarleyRulesPriorityDescriptor
			{
				m_vHeadSymbol = "col_properties",
				m_vFirstRHSSymbols = new string[]
				{
					"supplemental_logging_props",
					"virtual_column_definition",
					"column_definition",
					"out_of_line_ref_constraint",
					"out_of_line_constraint"
				}
			},
			new OracleMbEarleyRulesPriorityDescriptor
			{
				m_vHeadSymbol = "unconstrained_type_wo_datetime",
				m_vFirstRHSSymbols = new string[]
				{
					"link_expanded_n",
					"'SYS_REFCURSOR'"
				}
			},
			new OracleMbEarleyRulesPriorityDescriptor
			{
				m_vHeadSymbol = "simple_expression",
				m_vFirstRHSSymbols = new string[]
				{
					"column",
					"identifier",
					"'CONNECT_BY_ISLEAF'",
					"'CONNECT_BY_ISCYCLE'",
					"'ROWNUM'",
					"'ROWID'",
					"'CONNECT_BY_ROOT'",
					"'NULL'"
				}
			},
			new OracleMbEarleyRulesPriorityDescriptor
			{
				m_vHeadSymbol = "function_expression",
				m_vFirstRHSSymbols = new string[]
				{
					"function_call",
					"count",
					"function"
				}
			},
			new OracleMbEarleyRulesPriorityDescriptor
			{
				m_vHeadSymbol = "function",
				m_vFirstRHSSymbols = new string[]
				{
					"object_reference_function",
					"user_defined_function",
					"single_row_function",
					"analytic_function",
					"aggregate_function"
				}
			},
			new OracleMbEarleyRulesPriorityDescriptor
			{
				m_vHeadSymbol = "comparison_condition",
				m_vFirstRHSSymbols = new string[]
				{
					"simple_comparison_condition",
					"group_comparison_condition",
					"between_condition"
				}
			},
			new OracleMbEarleyRulesPriorityDescriptor
			{
				m_vHeadSymbol = "analytic_function",
				m_vFirstRHSSymbols = new string[]
				{
					"a_f",
					"max",
					"min",
					"sum",
					"lead",
					"lag",
					"listagg",
					"first_last_value",
					"nth_value",
					"count"
				}
			},
			new OracleMbEarleyRulesPriorityDescriptor
			{
				m_vHeadSymbol = "windowing_clause[31,73)",
				m_vFirstRHSSymbols = new string[]
				{
					"expr",
					"'UNBOUNDED'"
				}
			},
			new OracleMbEarleyRulesPriorityDescriptor
			{
				m_vHeadSymbol = "windowing_clause[81,123)",
				m_vFirstRHSSymbols = new string[]
				{
					"expr",
					"'UNBOUNDED'"
				}
			},
			new OracleMbEarleyRulesPriorityDescriptor
			{
				m_vHeadSymbol = "select_term",
				m_vFirstRHSSymbols = new string[]
				{
					"aliased_expr",
					"expr"
				}
			},
			new OracleMbEarleyRulesPriorityDescriptor
			{
				m_vHeadSymbol = "ty_def",
				m_vFirstRHSSymbols = new string[]
				{
					"tbl_ty_def",
					"array_ty_def"
				}
			},
			new OracleMbEarleyRulesPriorityDescriptor
			{
				m_vHeadSymbol = "datetime_expression[1923,1939)",
				m_vFirstRHSSymbols = new string[]
				{
					"expr",
					"string_literal",
					"'DBTIMEZONE'"
				}
			},
			new OracleMbEarleyRulesPriorityDescriptor
			{
				m_vHeadSymbol = "excptn_choice",
				m_vFirstRHSSymbols = new string[]
				{
					"dotted_name",
					"'OTHERS'"
				}
			}
		};
	}
}
