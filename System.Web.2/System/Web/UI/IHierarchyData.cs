using System;

namespace System.Web.UI
{
	// Token: 0x020002A8 RID: 680
	public interface IHierarchyData
	{
		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06001F9C RID: 8092
		bool HasChildren { get; }

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x06001F9D RID: 8093
		string Path { get; }

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x06001F9E RID: 8094
		object Item { get; }

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x06001F9F RID: 8095
		string Type { get; }

		// Token: 0x06001FA0 RID: 8096
		IHierarchicalEnumerable GetChildren();

		// Token: 0x06001FA1 RID: 8097
		IHierarchyData GetParent();
	}
}
