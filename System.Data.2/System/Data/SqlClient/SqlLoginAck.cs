using System;

namespace System.Data.SqlClient
{
	// Token: 0x0200021C RID: 540
	internal sealed class SqlLoginAck
	{
		// Token: 0x04001450 RID: 5200
		internal string programName;

		// Token: 0x04001451 RID: 5201
		internal byte majorVersion;

		// Token: 0x04001452 RID: 5202
		internal byte minorVersion;

		// Token: 0x04001453 RID: 5203
		internal short buildNum;

		// Token: 0x04001454 RID: 5204
		internal bool isVersion8;

		// Token: 0x04001455 RID: 5205
		internal uint tdsVersion;
	}
}
