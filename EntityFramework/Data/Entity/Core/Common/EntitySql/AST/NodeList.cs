using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000214 RID: 532
	internal sealed class NodeList<T> : Node, IEnumerable<!0>, IEnumerable where T : Node
	{
		// Token: 0x06001354 RID: 4948 RVA: 0x00050348 File Offset: 0x0004E548
		internal NodeList()
		{
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x0005035B File Offset: 0x0004E55B
		internal NodeList(T item)
		{
			this._list.Add(item);
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x0005037A File Offset: 0x0004E57A
		internal NodeList<T> Add(T item)
		{
			this._list.Add(item);
			return this;
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06001357 RID: 4951 RVA: 0x00050389 File Offset: 0x0004E589
		internal int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		// Token: 0x170001F0 RID: 496
		internal T this[int index]
		{
			get
			{
				return this._list[index];
			}
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x000503A4 File Offset: 0x0004E5A4
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return this._list.GetEnumerator();
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x000503B6 File Offset: 0x0004E5B6
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._list.GetEnumerator();
		}

		// Token: 0x040005A6 RID: 1446
		private readonly List<T> _list = new List<T>();
	}
}
