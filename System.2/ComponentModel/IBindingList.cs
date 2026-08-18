using System;
using System.Collections;

namespace System.ComponentModel
{
	// Token: 0x02000558 RID: 1368
	public interface IBindingList : IList, ICollection, IEnumerable
	{
		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x06003367 RID: 13159
		bool AllowNew { get; }

		// Token: 0x06003368 RID: 13160
		object AddNew();

		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x06003369 RID: 13161
		bool AllowEdit { get; }

		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x0600336A RID: 13162
		bool AllowRemove { get; }

		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x0600336B RID: 13163
		bool SupportsChangeNotification { get; }

		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x0600336C RID: 13164
		bool SupportsSearching { get; }

		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x0600336D RID: 13165
		bool SupportsSorting { get; }

		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x0600336E RID: 13166
		bool IsSorted { get; }

		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x0600336F RID: 13167
		PropertyDescriptor SortProperty { get; }

		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x06003370 RID: 13168
		ListSortDirection SortDirection { get; }

		// Token: 0x1400004B RID: 75
		// (add) Token: 0x06003371 RID: 13169
		// (remove) Token: 0x06003372 RID: 13170
		event ListChangedEventHandler ListChanged;

		// Token: 0x06003373 RID: 13171
		void AddIndex(PropertyDescriptor property);

		// Token: 0x06003374 RID: 13172
		void ApplySort(PropertyDescriptor property, ListSortDirection direction);

		// Token: 0x06003375 RID: 13173
		int Find(PropertyDescriptor property, object key);

		// Token: 0x06003376 RID: 13174
		void RemoveIndex(PropertyDescriptor property);

		// Token: 0x06003377 RID: 13175
		void RemoveSort();
	}
}
