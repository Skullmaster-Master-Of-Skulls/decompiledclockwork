using System;

namespace System.Web.UI.Design
{
	// Token: 0x02000057 RID: 87
	public interface IProjectItem
	{
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002BD RID: 701
		string AppRelativeUrl { get; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002BE RID: 702
		string Name { get; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002BF RID: 703
		IProjectItem Parent { get; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002C0 RID: 704
		string PhysicalPath { get; }
	}
}
