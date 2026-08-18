using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002E0 RID: 736
	[Flags]
	public enum NetworkInformationAccess
	{
		// Token: 0x04001A56 RID: 6742
		None = 0,
		// Token: 0x04001A57 RID: 6743
		Read = 1,
		// Token: 0x04001A58 RID: 6744
		Ping = 4
	}
}
