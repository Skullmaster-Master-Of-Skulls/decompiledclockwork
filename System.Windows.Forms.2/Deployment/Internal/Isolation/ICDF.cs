using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200001E RID: 30
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("285a8860-c84a-11d7-850f-005cd062464f")]
	[ComImport]
	internal interface ICDF
	{
		// Token: 0x060000C1 RID: 193
		ISection GetRootSection(uint SectionId);

		// Token: 0x060000C2 RID: 194
		ISectionEntry GetRootSectionEntry(uint SectionId);

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000C3 RID: 195
		object _NewEnum { [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000C4 RID: 196
		uint Count { get; }

		// Token: 0x060000C5 RID: 197
		object GetItem(uint SectionId);
	}
}
