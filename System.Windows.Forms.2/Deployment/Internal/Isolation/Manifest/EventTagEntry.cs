using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000D2 RID: 210
	[StructLayout(LayoutKind.Sequential)]
	internal class EventTagEntry
	{
		// Token: 0x04000365 RID: 869
		[MarshalAs(UnmanagedType.LPWStr)]
		public string TagData;

		// Token: 0x04000366 RID: 870
		public uint EventID;
	}
}
