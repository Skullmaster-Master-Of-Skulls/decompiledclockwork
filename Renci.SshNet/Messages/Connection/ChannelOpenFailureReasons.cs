using System;

namespace Renci.SshNet.Messages.Connection
{
	// Token: 0x020000A2 RID: 162
	internal enum ChannelOpenFailureReasons : uint
	{
		// Token: 0x0400030F RID: 783
		AdministativelyProhibited = 1U,
		// Token: 0x04000310 RID: 784
		ConnectFailed,
		// Token: 0x04000311 RID: 785
		UnknownChannelType,
		// Token: 0x04000312 RID: 786
		ResourceShortage
	}
}
