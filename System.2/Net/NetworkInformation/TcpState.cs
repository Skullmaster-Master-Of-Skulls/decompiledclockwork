using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000306 RID: 774
	[__DynamicallyInvokable]
	public enum TcpState
	{
		// Token: 0x04001AF7 RID: 6903
		[__DynamicallyInvokable]
		Unknown,
		// Token: 0x04001AF8 RID: 6904
		[__DynamicallyInvokable]
		Closed,
		// Token: 0x04001AF9 RID: 6905
		[__DynamicallyInvokable]
		Listen,
		// Token: 0x04001AFA RID: 6906
		[__DynamicallyInvokable]
		SynSent,
		// Token: 0x04001AFB RID: 6907
		[__DynamicallyInvokable]
		SynReceived,
		// Token: 0x04001AFC RID: 6908
		[__DynamicallyInvokable]
		Established,
		// Token: 0x04001AFD RID: 6909
		[__DynamicallyInvokable]
		FinWait1,
		// Token: 0x04001AFE RID: 6910
		[__DynamicallyInvokable]
		FinWait2,
		// Token: 0x04001AFF RID: 6911
		[__DynamicallyInvokable]
		CloseWait,
		// Token: 0x04001B00 RID: 6912
		[__DynamicallyInvokable]
		Closing,
		// Token: 0x04001B01 RID: 6913
		[__DynamicallyInvokable]
		LastAck,
		// Token: 0x04001B02 RID: 6914
		[__DynamicallyInvokable]
		TimeWait,
		// Token: 0x04001B03 RID: 6915
		[__DynamicallyInvokable]
		DeleteTcb
	}
}
