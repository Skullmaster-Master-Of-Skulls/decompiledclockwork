using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x02000096 RID: 150
	[StructLayout(LayoutKind.Sequential)]
	internal class CategoryMembershipDataEntry
	{
		// Token: 0x04000295 RID: 661
		public uint index;

		// Token: 0x04000296 RID: 662
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Xml;

		// Token: 0x04000297 RID: 663
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Description;
	}
}
