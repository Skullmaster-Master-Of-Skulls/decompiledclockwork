using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000326 RID: 806
	internal sealed class SqlLogin
	{
		// Token: 0x04001BAA RID: 7082
		internal int timeout;

		// Token: 0x04001BAB RID: 7083
		internal bool userInstance;

		// Token: 0x04001BAC RID: 7084
		internal string hostName = "";

		// Token: 0x04001BAD RID: 7085
		internal string userName = "";

		// Token: 0x04001BAE RID: 7086
		internal string password = "";

		// Token: 0x04001BAF RID: 7087
		internal string applicationName = "";

		// Token: 0x04001BB0 RID: 7088
		internal string serverName = "";

		// Token: 0x04001BB1 RID: 7089
		internal string language = "";

		// Token: 0x04001BB2 RID: 7090
		internal string database = "";

		// Token: 0x04001BB3 RID: 7091
		internal string attachDBFilename = "";

		// Token: 0x04001BB4 RID: 7092
		internal string newPassword = "";

		// Token: 0x04001BB5 RID: 7093
		internal bool useReplication;

		// Token: 0x04001BB6 RID: 7094
		internal bool useSSPI;

		// Token: 0x04001BB7 RID: 7095
		internal int packetSize = 8000;

		// Token: 0x04001BB8 RID: 7096
		internal bool readOnlyIntent;
	}
}
