using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002E6 RID: 742
	[__DynamicallyInvokable]
	public enum OperationalStatus
	{
		// Token: 0x04001A67 RID: 6759
		[__DynamicallyInvokable]
		Up = 1,
		// Token: 0x04001A68 RID: 6760
		[__DynamicallyInvokable]
		Down,
		// Token: 0x04001A69 RID: 6761
		[__DynamicallyInvokable]
		Testing,
		// Token: 0x04001A6A RID: 6762
		[__DynamicallyInvokable]
		Unknown,
		// Token: 0x04001A6B RID: 6763
		[__DynamicallyInvokable]
		Dormant,
		// Token: 0x04001A6C RID: 6764
		[__DynamicallyInvokable]
		NotPresent,
		// Token: 0x04001A6D RID: 6765
		[__DynamicallyInvokable]
		LowerLayerDown
	}
}
