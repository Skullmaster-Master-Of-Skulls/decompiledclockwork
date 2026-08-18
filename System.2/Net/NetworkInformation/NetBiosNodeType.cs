using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002E5 RID: 741
	[__DynamicallyInvokable]
	public enum NetBiosNodeType
	{
		// Token: 0x04001A61 RID: 6753
		[__DynamicallyInvokable]
		Unknown,
		// Token: 0x04001A62 RID: 6754
		[__DynamicallyInvokable]
		Broadcast,
		// Token: 0x04001A63 RID: 6755
		[__DynamicallyInvokable]
		Peer2Peer,
		// Token: 0x04001A64 RID: 6756
		[__DynamicallyInvokable]
		Mixed = 4,
		// Token: 0x04001A65 RID: 6757
		[__DynamicallyInvokable]
		Hybrid = 8
	}
}
