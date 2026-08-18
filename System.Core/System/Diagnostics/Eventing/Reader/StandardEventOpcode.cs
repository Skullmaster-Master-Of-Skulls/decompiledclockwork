using System;

namespace System.Diagnostics.Eventing.Reader
{
	// Token: 0x020002D0 RID: 720
	public enum StandardEventOpcode
	{
		// Token: 0x04000CBF RID: 3263
		Info,
		// Token: 0x04000CC0 RID: 3264
		Start,
		// Token: 0x04000CC1 RID: 3265
		Stop,
		// Token: 0x04000CC2 RID: 3266
		DataCollectionStart,
		// Token: 0x04000CC3 RID: 3267
		DataCollectionStop,
		// Token: 0x04000CC4 RID: 3268
		Extension,
		// Token: 0x04000CC5 RID: 3269
		Reply,
		// Token: 0x04000CC6 RID: 3270
		Resume,
		// Token: 0x04000CC7 RID: 3271
		Suspend,
		// Token: 0x04000CC8 RID: 3272
		Send,
		// Token: 0x04000CC9 RID: 3273
		Receive = 240
	}
}
