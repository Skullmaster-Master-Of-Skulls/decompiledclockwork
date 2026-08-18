using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000325 RID: 805
	internal sealed class SqlEnvChange
	{
		// Token: 0x04001B9D RID: 7069
		internal byte type;

		// Token: 0x04001B9E RID: 7070
		internal byte oldLength;

		// Token: 0x04001B9F RID: 7071
		internal int newLength;

		// Token: 0x04001BA0 RID: 7072
		internal int length;

		// Token: 0x04001BA1 RID: 7073
		internal string newValue;

		// Token: 0x04001BA2 RID: 7074
		internal string oldValue;

		// Token: 0x04001BA3 RID: 7075
		internal byte[] newBinValue;

		// Token: 0x04001BA4 RID: 7076
		internal byte[] oldBinValue;

		// Token: 0x04001BA5 RID: 7077
		internal long newLongValue;

		// Token: 0x04001BA6 RID: 7078
		internal long oldLongValue;

		// Token: 0x04001BA7 RID: 7079
		internal SqlCollation newCollation;

		// Token: 0x04001BA8 RID: 7080
		internal SqlCollation oldCollation;

		// Token: 0x04001BA9 RID: 7081
		internal RoutingInfo newRoutingInfo;
	}
}
