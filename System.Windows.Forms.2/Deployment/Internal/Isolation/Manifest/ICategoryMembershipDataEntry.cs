using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x02000098 RID: 152
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("DA0C3B27-6B6B-4b80-A8F8-6CE14F4BC0A4")]
	[ComImport]
	internal interface ICategoryMembershipDataEntry
	{
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000268 RID: 616
		CategoryMembershipDataEntry AllData { [SecurityCritical] get; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000269 RID: 617
		uint index { [SecurityCritical] get; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600026A RID: 618
		string Xml { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600026B RID: 619
		string Description { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }
	}
}
