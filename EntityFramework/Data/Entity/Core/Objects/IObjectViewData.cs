using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x0200059C RID: 1436
	internal interface IObjectViewData<T>
	{
		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06003850 RID: 14416
		IList<T> List { get; }

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06003851 RID: 14417
		bool AllowNew { get; }

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x06003852 RID: 14418
		bool AllowEdit { get; }

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x06003853 RID: 14419
		bool AllowRemove { get; }

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06003854 RID: 14420
		bool FiresEventOnAdd { get; }

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x06003855 RID: 14421
		bool FiresEventOnRemove { get; }

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06003856 RID: 14422
		bool FiresEventOnClear { get; }

		// Token: 0x06003857 RID: 14423
		void EnsureCanAddNew();

		// Token: 0x06003858 RID: 14424
		int Add(T item, bool isAddNew);

		// Token: 0x06003859 RID: 14425
		void CommitItemAt(int index);

		// Token: 0x0600385A RID: 14426
		void Clear();

		// Token: 0x0600385B RID: 14427
		bool Remove(T item, bool isCancelNew);

		// Token: 0x0600385C RID: 14428
		ListChangedEventArgs OnCollectionChanged(object sender, CollectionChangeEventArgs e, ObjectViewListener listener);
	}
}
