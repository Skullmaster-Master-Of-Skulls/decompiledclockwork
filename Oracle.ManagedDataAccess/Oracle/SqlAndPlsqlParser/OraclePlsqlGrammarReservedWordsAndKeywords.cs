using System;

namespace Oracle.SqlAndPlsqlParser
{
	// Token: 0x02000276 RID: 630
	internal class OraclePlsqlGrammarReservedWordsAndKeywords : ParserGrammarReservedWordsAndKeywords
	{
		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x060018E3 RID: 6371 RVA: 0x00106F34 File Offset: 0x00105134
		public override string[] ReservedWords
		{
			get
			{
				return OraclePlsqlGrammarReservedWordsAndKeywords.s_vReservedWords;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x060018E4 RID: 6372 RVA: 0x00106F3C File Offset: 0x0010513C
		public override string[] Keywords
		{
			get
			{
				return OraclePlsqlGrammarReservedWordsAndKeywords.s_vKeywords;
			}
		}

		// Token: 0x04001B5B RID: 7003
		public static OraclePlsqlGrammarReservedWordsAndKeywords s_vInstance = new OraclePlsqlGrammarReservedWordsAndKeywords();

		// Token: 0x04001B5C RID: 7004
		private static string[] s_vReservedWords = ParserGrammarReservedWordsAndKeywords.ReadCompressedDataFromManifest("Oracle.ManagedDataAccess.src.SqlParser.Resources.PlsqlReservedWords.zip");

		// Token: 0x04001B5D RID: 7005
		public static string[] s_vKeywords = ParserGrammarReservedWordsAndKeywords.ReadCompressedDataFromManifest("Oracle.ManagedDataAccess.src.SqlParser.Resources.PlsqlKeywords.zip");
	}
}
