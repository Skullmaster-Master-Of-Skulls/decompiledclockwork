using System;

namespace System.ServiceModel.Security
{
	// Token: 0x020002AA RID: 682
	[Flags]
	internal enum ReceiveSecurityHeaderBindingModes
	{
		// Token: 0x04001B0D RID: 6925
		Unknown = 0,
		// Token: 0x04001B0E RID: 6926
		Primary = 1,
		// Token: 0x04001B0F RID: 6927
		Endorsing = 2,
		// Token: 0x04001B10 RID: 6928
		Signed = 4,
		// Token: 0x04001B11 RID: 6929
		SignedEndorsing = 8,
		// Token: 0x04001B12 RID: 6930
		Basic = 16
	}
}
