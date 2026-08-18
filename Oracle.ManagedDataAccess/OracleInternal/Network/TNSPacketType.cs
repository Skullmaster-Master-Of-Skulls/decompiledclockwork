using System;

namespace OracleInternal.Network
{
	// Token: 0x02000167 RID: 359
	internal enum TNSPacketType
	{
		// Token: 0x04000F96 RID: 3990
		CONNECT = 1,
		// Token: 0x04000F97 RID: 3991
		ACCEPT,
		// Token: 0x04000F98 RID: 3992
		ACK,
		// Token: 0x04000F99 RID: 3993
		REFUSE,
		// Token: 0x04000F9A RID: 3994
		REDIRECT,
		// Token: 0x04000F9B RID: 3995
		DATA,
		// Token: 0x04000F9C RID: 3996
		NULL,
		// Token: 0x04000F9D RID: 3997
		ABORT = 9,
		// Token: 0x04000F9E RID: 3998
		RESEND = 11,
		// Token: 0x04000F9F RID: 3999
		MARKER,
		// Token: 0x04000FA0 RID: 4000
		ATTN,
		// Token: 0x04000FA1 RID: 4001
		CTRL,
		// Token: 0x04000FA2 RID: 4002
		HIGHEST = 19
	}
}
