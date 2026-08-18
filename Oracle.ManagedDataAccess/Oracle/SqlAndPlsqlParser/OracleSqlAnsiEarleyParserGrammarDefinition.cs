using System;

namespace Oracle.SqlAndPlsqlParser
{
	// Token: 0x02000279 RID: 633
	internal class OracleSqlAnsiEarleyParserGrammarDefinition : OracleSqlEarleyParserGrammarDefinition
	{
		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x060018F2 RID: 6386 RVA: 0x001078B0 File Offset: 0x00105AB0
		public new static OracleSqlAnsiEarleyParserGrammarDefinition Instance
		{
			get
			{
				lock (OracleSqlAnsiEarleyParserGrammarDefinition.m_vObjectLock)
				{
					if (OracleSqlAnsiEarleyParserGrammarDefinition.s_vInstance == null)
					{
						OracleSqlAnsiEarleyParserGrammarDefinition.s_vInstance = new OracleSqlAnsiEarleyParserGrammarDefinition(OracleSqlEarleyParserGrammarDefinition.GetRulesSet("Oracle.ManagedDataAccess.src.SqlParser.Resources.SQLPLSQL_ANSI.zip"));
					}
				}
				return OracleSqlAnsiEarleyParserGrammarDefinition.s_vInstance;
			}
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x0010790C File Offset: 0x00105B0C
		protected OracleSqlAnsiEarleyParserGrammarDefinition(Set<RuleTuple> rules) : base(rules)
		{
		}

		// Token: 0x04001B64 RID: 7012
		private static readonly object m_vObjectLock = new object();

		// Token: 0x04001B65 RID: 7013
		private static OracleSqlAnsiEarleyParserGrammarDefinition s_vInstance = null;
	}
}
