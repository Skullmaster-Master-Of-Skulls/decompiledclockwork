using System;
using System.Collections.Generic;

namespace System.Collections.ObjectModel
{
	// Token: 0x02000004 RID: 4
	internal sealed class ListWrapperCollection<T> : Collection<T>
	{
		// Token: 0x06000015 RID: 21 RVA: 0x000025F4 File Offset: 0x000007F4
		internal ListWrapperCollection() : this(new List<T>())
		{
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002601 File Offset: 0x00000801
		internal ListWrapperCollection(List<T> list) : base(list)
		{
			this._items = list;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002611 File Offset: 0x00000811
		internal List<T> ItemsList
		{
			get
			{
				return this._items;
			}
		}

		// Token: 0x04000002 RID: 2
		private readonly List<T> _items;
	}
}
