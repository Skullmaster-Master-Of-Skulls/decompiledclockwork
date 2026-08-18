using System;

namespace Oracle.SqlAndPlsqlParser
{
	// Token: 0x0200027A RID: 634
	internal class OracleSqlGrammarReservedWordsAndKeywords : ParserGrammarReservedWordsAndKeywords
	{
		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x060018F5 RID: 6389 RVA: 0x0010792C File Offset: 0x00105B2C
		public override string[] ReservedWords
		{
			get
			{
				return OracleSqlGrammarReservedWordsAndKeywords.s_vReservedWords;
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x060018F6 RID: 6390 RVA: 0x00107934 File Offset: 0x00105B34
		public override string[] Keywords
		{
			get
			{
				return OracleSqlGrammarReservedWordsAndKeywords.s_vKeywords;
			}
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x00107968 File Offset: 0x00105B68
		protected OracleSqlGrammarReservedWordsAndKeywords()
		{
		}

		// Token: 0x04001B66 RID: 7014
		public static OracleSqlGrammarReservedWordsAndKeywords s_vInstance = new OracleSqlGrammarReservedWordsAndKeywords();

		// Token: 0x04001B67 RID: 7015
		private static string[] s_vReservedWords = ParserGrammarReservedWordsAndKeywords.ReadCompressedDataFromManifest("Oracle.ManagedDataAccess.src.SqlParser.Resources.SqlReservedWords.zip");

		// Token: 0x04001B68 RID: 7016
		private static string[] s_vKeywords = ParserGrammarReservedWordsAndKeywords.ReadCompressedDataFromManifest("Oracle.ManagedDataAccess.src.SqlParser.Resources.SqlKeywords.zip");
	}
}
