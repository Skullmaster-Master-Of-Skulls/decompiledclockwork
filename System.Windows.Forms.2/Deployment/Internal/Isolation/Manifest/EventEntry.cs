using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000CC RID: 204
	[StructLayout(LayoutKind.Sequential)]
	internal class EventEntry
	{
		// Token: 0x0400034D RID: 845
		public uint EventID;

		// Token: 0x0400034E RID: 846
		public uint Level;

		// Token: 0x0400034F RID: 847
		public uint Version;

		// Token: 0x04000350 RID: 848
		public Guid Guid;

		// Token: 0x04000351 RID: 849
		[MarshalAs(UnmanagedType.LPWStr)]
		public string SubTypeName;

		// Token: 0x04000352 RID: 850
		public uint SubTypeValue;

		// Token: 0x04000353 RID: 851
		[MarshalAs(UnmanagedType.LPWStr)]
		public string DisplayName;

		// Token: 0x04000354 RID: 852
		public uint EventNameMicrodomIndex;
	}
}
