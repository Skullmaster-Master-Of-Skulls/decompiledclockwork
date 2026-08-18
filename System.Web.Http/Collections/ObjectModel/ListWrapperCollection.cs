using System;
using System.Collections.Generic;

namespace System.Collections.ObjectModel
{
	// Token: 0x02000007 RID: 7
	internal sealed class ListWrapperCollection<T> : Collection<T>
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00002A49 File Offset: 0x00000C49
		internal ListWrapperCollection() : this(new List<T>())
		{
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002A56 File Offset: 0x00000C56
		internal ListWrapperCollection(List<T> list) : base(list)
		{
			this._items = list;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002A66 File Offset: 0x00000C66
		internal List<T> ItemsList
		{
			get
			{
				return this._items;
			}
		}

		// Token: 0x04000004 RID: 4
		private readonly List<T> _items;
	}
}
