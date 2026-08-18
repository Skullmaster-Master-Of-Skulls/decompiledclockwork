using System;

namespace System.Net.Mail
{
	// Token: 0x0200025D RID: 605
	internal enum ServerState
	{
		// Token: 0x0400177E RID: 6014
		Starting = 1,
		// Token: 0x0400177F RID: 6015
		Started,
		// Token: 0x04001780 RID: 6016
		Stopping,
		// Token: 0x04001781 RID: 6017
		Stopped,
		// Token: 0x04001782 RID: 6018
		Pausing,
		// Token: 0x04001783 RID: 6019
		Paused,
		// Token: 0x04001784 RID: 6020
		Continuing
	}
}
