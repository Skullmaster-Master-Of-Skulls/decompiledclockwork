using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000E1 RID: 225
	[StructLayout(LayoutKind.Sequential)]
	internal class CounterSetEntry
	{
		// Token: 0x04000397 RID: 919
		public Guid CounterSetGuid;

		// Token: 0x04000398 RID: 920
		public Guid ProviderGuid;

		// Token: 0x04000399 RID: 921
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Name;

		// Token: 0x0400039A RID: 922
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Description;

		// Token: 0x0400039B RID: 923
		public bool InstanceType;
	}
}
