using System;
using System.Collections;

namespace System.ComponentModel
{
	// Token: 0x02000559 RID: 1369
	public interface IBindingListView : IBindingList, IList, ICollection, IEnumerable
	{
		// Token: 0x06003378 RID: 13176
		void ApplySort(ListSortDescriptionCollection sorts);

		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x06003379 RID: 13177
		// (set) Token: 0x0600337A RID: 13178
		string Filter { get; set; }

		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x0600337B RID: 13179
		ListSortDescriptionCollection SortDescriptions { get; }

		// Token: 0x0600337C RID: 13180
		void RemoveFilter();

		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x0600337D RID: 13181
		bool SupportsAdvancedSorting { get; }

		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x0600337E RID: 13182
		bool SupportsFiltering { get; }
	}
}
