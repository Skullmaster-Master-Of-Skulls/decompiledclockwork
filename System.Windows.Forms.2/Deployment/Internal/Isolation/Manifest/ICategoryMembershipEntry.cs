using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x0200009E RID: 158
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("97FDCA77-B6F2-4718-A1EB-29D0AECE9C03")]
	[ComImport]
	internal interface ICategoryMembershipEntry
	{
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000271 RID: 625
		CategoryMembershipEntry AllData { [SecurityCritical] get; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000272 RID: 626
		IDefinitionIdentity Identity { [SecurityCritical] get; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000273 RID: 627
		ISection SubcategoryMembership { [SecurityCritical] get; }
	}
}
