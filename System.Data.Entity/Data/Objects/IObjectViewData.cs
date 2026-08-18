using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace System.Data.Objects
{
	// Token: 0x02000157 RID: 343
	internal interface IObjectViewData<T>
	{
		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06001968 RID: 6504
		IList<T> List { get; }

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06001969 RID: 6505
		bool AllowNew { get; }

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x0600196A RID: 6506
		bool AllowEdit { get; }

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x0600196B RID: 6507
		bool AllowRemove { get; }

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x0600196C RID: 6508
		bool FiresEventOnAdd { get; }

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x0600196D RID: 6509
		bool FiresEventOnRemove { get; }

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x0600196E RID: 6510
		bool FiresEventOnClear { get; }

		// Token: 0x0600196F RID: 6511
		void EnsureCanAddNew();

		// Token: 0x06001970 RID: 6512
		int Add(T item, bool isAddNew);

		// Token: 0x06001971 RID: 6513
		void CommitItemAt(int index);

		// Token: 0x06001972 RID: 6514
		void Clear();

		// Token: 0x06001973 RID: 6515
		bool Remove(T item, bool isCancelNew);

		// Token: 0x06001974 RID: 6516
		ListChangedEventArgs OnCollectionChanged(object sender, CollectionChangeEventArgs e, ObjectViewListener listener);
	}
}
