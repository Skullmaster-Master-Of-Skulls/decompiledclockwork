using System;

namespace Oracle.SqlAndPlsqlParser
{
	// Token: 0x02000270 RID: 624
	internal enum Token
	{
		// Token: 0x04001B31 RID: 6961
		UNKNOWN,
		// Token: 0x04001B32 RID: 6962
		COMMENT,
		// Token: 0x04001B33 RID: 6963
		LINE_COMMENT,
		// Token: 0x04001B34 RID: 6964
		QUOTED_STRING,
		// Token: 0x04001B35 RID: 6965
		DQUOTED_STRING,
		// Token: 0x04001B36 RID: 6966
		WS,
		// Token: 0x04001B37 RID: 6967
		DIGITS,
		// Token: 0x04001B38 RID: 6968
		OPERATION,
		// Token: 0x04001B39 RID: 6969
		IDENTIFIER,
		// Token: 0x04001B3A RID: 6970
		AUXILIARY,
		// Token: 0x04001B3B RID: 6971
		PRE
	}
}
