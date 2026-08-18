using System;

namespace System.Net.Mail
{
	// Token: 0x0200068C RID: 1676
	internal enum ServerState
	{
		// Token: 0x04002FD3 RID: 12243
		Starting = 1,
		// Token: 0x04002FD4 RID: 12244
		Started,
		// Token: 0x04002FD5 RID: 12245
		Stopping,
		// Token: 0x04002FD6 RID: 12246
		Stopped,
		// Token: 0x04002FD7 RID: 12247
		Pausing,
		// Token: 0x04002FD8 RID: 12248
		Paused,
		// Token: 0x04002FD9 RID: 12249
		Continuing
	}
}
