using System;

namespace Oracle.SqlAndPlsqlParser
{
	// Token: 0x0200026C RID: 620
	[Flags]
	internal enum ParserGrammarSymbolFlags
	{
		// Token: 0x04001B1B RID: 6939
		IS_TERMINAL = 1,
		// Token: 0x04001B1C RID: 6940
		IS_ON_LEFT_HAND_SIDE = 2,
		// Token: 0x04001B1D RID: 6941
		IS_ON_RIGHT_HAND_SIDE = 4,
		// Token: 0x04001B1E RID: 6942
		IS_KEYWORD = 16,
		// Token: 0x04001B1F RID: 6943
		IS_RESERVED_WORD = 32,
		// Token: 0x04001B20 RID: 6944
		IS_SQL_SPECIAL_WORD = 256,
		// Token: 0x04001B21 RID: 6945
		IS_PLSQL_SPECIAL_WORD = 512
	}
}
