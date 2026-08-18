using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Data.Common.EntitySql.AST
{
	// Token: 0x02000361 RID: 865
	internal sealed class NodeList<T> : Node, IEnumerable<!0>, IEnumerable where T : Node
	{
		// Token: 0x060031F2 RID: 12786 RVA: 0x000C49FC File Offset: 0x000C2BFC
		internal NodeList()
		{
		}

		// Token: 0x060031F3 RID: 12787 RVA: 0x000C4A0F File Offset: 0x000C2C0F
		internal NodeList(T item)
		{
			this._list.Add(item);
		}

		// Token: 0x060031F4 RID: 12788 RVA: 0x000C4A2E File Offset: 0x000C2C2E
		internal NodeList<T> Add(T item)
		{
			this._list.Add(item);
			return this;
		}

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x060031F5 RID: 12789 RVA: 0x000C4A3D File Offset: 0x000C2C3D
		internal int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		// Token: 0x170009A8 RID: 2472
		internal T this[int index]
		{
			get
			{
				return this._list[index];
			}
		}

		// Token: 0x060031F7 RID: 12791 RVA: 0x000C4A58 File Offset: 0x000C2C58
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return this._list.GetEnumerator();
		}

		// Token: 0x060031F8 RID: 12792 RVA: 0x000C4A58 File Offset: 0x000C2C58
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._list.GetEnumerator();
		}

		// Token: 0x040015BF RID: 5567
		private readonly List<T> _list = new List<T>();
	}
}
