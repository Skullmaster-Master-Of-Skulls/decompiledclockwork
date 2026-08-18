using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200061E RID: 1566
	public enum NetBiosNodeType
	{
		// Token: 0x04002DEC RID: 11756
		Unknown,
		// Token: 0x04002DED RID: 11757
		Broadcast,
		// Token: 0x04002DEE RID: 11758
		Peer2Peer,
		// Token: 0x04002DEF RID: 11759
		Mixed = 4,
		// Token: 0x04002DF0 RID: 11760
		Hybrid = 8
	}
}
