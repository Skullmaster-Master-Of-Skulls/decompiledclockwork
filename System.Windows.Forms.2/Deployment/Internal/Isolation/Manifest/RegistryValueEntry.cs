using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000D5 RID: 213
	[StructLayout(LayoutKind.Sequential)]
	internal class RegistryValueEntry
	{
		// Token: 0x04000369 RID: 873
		public uint Flags;

		// Token: 0x0400036A RID: 874
		public uint OperationHint;

		// Token: 0x0400036B RID: 875
		public uint Type;

		// Token: 0x0400036C RID: 876
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Value;

		// Token: 0x0400036D RID: 877
		[MarshalAs(UnmanagedType.LPWStr)]
		public string BuildFilter;
	}
}
