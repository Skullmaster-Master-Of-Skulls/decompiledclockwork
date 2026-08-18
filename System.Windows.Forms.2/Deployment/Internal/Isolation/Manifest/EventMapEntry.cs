using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000CF RID: 207
	[StructLayout(LayoutKind.Sequential)]
	internal class EventMapEntry
	{
		// Token: 0x0400035D RID: 861
		[MarshalAs(UnmanagedType.LPWStr)]
		public string MapName;

		// Token: 0x0400035E RID: 862
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Name;

		// Token: 0x0400035F RID: 863
		public uint Value;

		// Token: 0x04000360 RID: 864
		public bool IsValueMap;
	}
}
