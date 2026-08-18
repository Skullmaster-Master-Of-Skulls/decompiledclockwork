using System;

namespace System.Reflection.Metadata
{
	// Token: 0x020000A9 RID: 169
	[Flags]
	public enum SignatureAttributes : byte
	{
		// Token: 0x04000437 RID: 1079
		None = 0,
		// Token: 0x04000438 RID: 1080
		Generic = 16,
		// Token: 0x04000439 RID: 1081
		Instance = 32,
		// Token: 0x0400043A RID: 1082
		ExplicitThis = 64
	}
}
