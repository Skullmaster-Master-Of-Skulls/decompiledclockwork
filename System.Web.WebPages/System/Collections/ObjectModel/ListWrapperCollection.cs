using System;
using System.Collections.Generic;

namespace System.Collections.ObjectModel
{
	// Token: 0x02000006 RID: 6
	internal sealed class ListWrapperCollection<T> : Collection<T>
	{
		// Token: 0x06000023 RID: 35 RVA: 0x00002646 File Offset: 0x00000846
		internal ListWrapperCollection() : this(new List<T>())
		{
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002653 File Offset: 0x00000853
		internal ListWrapperCollection(List<T> list) : base(list)
		{
			this._items = list;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002663 File Offset: 0x00000863
		internal List<T> ItemsList
		{
			get
			{
				return this._items;
			}
		}

		// Token: 0x04000006 RID: 6
		private readonly List<T> _items;
	}
}
