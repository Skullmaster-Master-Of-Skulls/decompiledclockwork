using System;
using System.Collections.Generic;

namespace System.Collections.ObjectModel
{
	// Token: 0x02000004 RID: 4
	internal sealed class ListWrapperCollection<T> : Collection<T>
	{
		// Token: 0x06000024 RID: 36 RVA: 0x000025E0 File Offset: 0x000007E0
		internal ListWrapperCollection() : this(new List<T>())
		{
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000025ED File Offset: 0x000007ED
		internal ListWrapperCollection(List<T> list) : base(list)
		{
			this._items = list;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000025FD File Offset: 0x000007FD
		internal List<T> ItemsList
		{
			get
			{
				return this._items;
			}
		}

		// Token: 0x04000003 RID: 3
		private readonly List<T> _items;
	}
}
