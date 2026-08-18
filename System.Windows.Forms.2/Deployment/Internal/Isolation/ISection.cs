using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000019 RID: 25
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("285a8862-c84a-11d7-850f-005cd062464f")]
	[ComImport]
	internal interface ISection
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000B4 RID: 180
		object _NewEnum { [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000B5 RID: 181
		uint Count { get; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000B6 RID: 182
		uint SectionID { get; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000B7 RID: 183
		string SectionName { [return: MarshalAs(UnmanagedType.LPWStr)] get; }
	}
}
