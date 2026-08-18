using System;

namespace System.Data.SqlClient
{
	// Token: 0x0200032C RID: 812
	internal sealed class _SqlRPC
	{
		// Token: 0x04001BEA RID: 7146
		internal string rpcName;

		// Token: 0x04001BEB RID: 7147
		internal string databaseName;

		// Token: 0x04001BEC RID: 7148
		internal ushort ProcID;

		// Token: 0x04001BED RID: 7149
		internal ushort options;

		// Token: 0x04001BEE RID: 7150
		internal SqlParameter[] parameters;

		// Token: 0x04001BEF RID: 7151
		internal byte[] paramoptions;

		// Token: 0x04001BF0 RID: 7152
		internal int? recordsAffected;

		// Token: 0x04001BF1 RID: 7153
		internal int cumulativeRecordsAffected;

		// Token: 0x04001BF2 RID: 7154
		internal int errorsIndexStart;

		// Token: 0x04001BF3 RID: 7155
		internal int errorsIndexEnd;

		// Token: 0x04001BF4 RID: 7156
		internal SqlErrorCollection errors;

		// Token: 0x04001BF5 RID: 7157
		internal int warningsIndexStart;

		// Token: 0x04001BF6 RID: 7158
		internal int warningsIndexEnd;

		// Token: 0x04001BF7 RID: 7159
		internal SqlErrorCollection warnings;
	}
}
