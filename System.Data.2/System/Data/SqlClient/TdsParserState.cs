using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000213 RID: 531
	internal enum TdsParserState
	{
		// Token: 0x0400140B RID: 5131
		Closed,
		// Token: 0x0400140C RID: 5132
		OpenNotLoggedIn,
		// Token: 0x0400140D RID: 5133
		OpenLoggedIn,
		// Token: 0x0400140E RID: 5134
		Broken
	}
}
