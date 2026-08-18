using System;
using System.Collections.Generic;

namespace System.Collections.ObjectModel
{
	// Token: 0x02000005 RID: 5
	internal sealed class ListWrapperCollection<T> : Collection<T>
	{
		// Token: 0x06000029 RID: 41 RVA: 0x00002980 File Offset: 0x00000B80
		internal ListWrapperCollection() : this(new List<T>())
		{
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000298D File Offset: 0x00000B8D
		internal ListWrapperCollection(List<T> list) : base(list)
		{
			this._items = list;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000299D File Offset: 0x00000B9D
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
