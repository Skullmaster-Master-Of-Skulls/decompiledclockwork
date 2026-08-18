using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x02000099 RID: 153
	[StructLayout(LayoutKind.Sequential)]
	internal class SubcategoryMembershipEntry
	{
		// Token: 0x0400029B RID: 667
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Subcategory;

		// Token: 0x0400029C RID: 668
		public ISection CategoryMembershipData;
	}
}
