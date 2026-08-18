using System;

namespace System.Data.SqlClient
{
	// Token: 0x0200021A RID: 538
	internal sealed class SqlEnvChange
	{
		// Token: 0x04001431 RID: 5169
		internal byte type;

		// Token: 0x04001432 RID: 5170
		internal byte oldLength;

		// Token: 0x04001433 RID: 5171
		internal int newLength;

		// Token: 0x04001434 RID: 5172
		internal int length;

		// Token: 0x04001435 RID: 5173
		internal string newValue;

		// Token: 0x04001436 RID: 5174
		internal string oldValue;

		// Token: 0x04001437 RID: 5175
		internal byte[] newBinValue;

		// Token: 0x04001438 RID: 5176
		internal byte[] oldBinValue;

		// Token: 0x04001439 RID: 5177
		internal long newLongValue;

		// Token: 0x0400143A RID: 5178
		internal long oldLongValue;

		// Token: 0x0400143B RID: 5179
		internal SqlCollation newCollation;

		// Token: 0x0400143C RID: 5180
		internal SqlCollation oldCollation;

		// Token: 0x0400143D RID: 5181
		internal RoutingInfo newRoutingInfo;
	}
}
