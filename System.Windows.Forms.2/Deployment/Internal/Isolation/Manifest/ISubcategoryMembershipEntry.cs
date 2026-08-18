using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x0200009B RID: 155
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("5A7A54D7-5AD5-418e-AB7A-CF823A8D48D0")]
	[ComImport]
	internal interface ISubcategoryMembershipEntry
	{
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600026D RID: 621
		SubcategoryMembershipEntry AllData { [SecurityCritical] get; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600026E RID: 622
		string Subcategory { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600026F RID: 623
		ISection CategoryMembershipData { [SecurityCritical] get; }
	}
}
