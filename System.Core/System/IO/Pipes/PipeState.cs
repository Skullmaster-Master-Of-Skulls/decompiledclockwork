using System;

namespace System.IO.Pipes
{
	// Token: 0x020000B5 RID: 181
	[Serializable]
	internal enum PipeState
	{
		// Token: 0x04000564 RID: 1380
		WaitingToConnect,
		// Token: 0x04000565 RID: 1381
		Connected,
		// Token: 0x04000566 RID: 1382
		Broken,
		// Token: 0x04000567 RID: 1383
		Disconnected,
		// Token: 0x04000568 RID: 1384
		Closed
	}
}
