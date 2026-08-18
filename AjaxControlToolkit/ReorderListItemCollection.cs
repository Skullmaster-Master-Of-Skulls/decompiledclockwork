using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;

namespace AjaxControlToolkit
{
	// Token: 0x0200017B RID: 379
	public class ReorderListItemCollection : IList<ReorderListItem>, ICollection<ReorderListItem>, IEnumerable<ReorderListItem>, IEnumerable
	{
		// Token: 0x06000A86 RID: 2694 RVA: 0x0001B877 File Offset: 0x00019A77
		public ReorderListItemCollection(ReorderList parent)
		{
			this._parent = parent;
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000A87 RID: 2695 RVA: 0x0001B886 File Offset: 0x00019A86
		private ControlCollection ChildList
		{
			get
			{
				return this._parent.ChildList.Controls;
			}
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x0001B898 File Offset: 0x00019A98
		public int IndexOf(ReorderListItem item)
		{
			return this.ChildList.IndexOf(item);
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x0001B8A6 File Offset: 0x00019AA6
		public void Insert(int index, ReorderListItem item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x0001B8AD File Offset: 0x00019AAD
		public void RemoveAt(int index)
		{
			this.ChildList.RemoveAt(index);
		}

		// Token: 0x170003F7 RID: 1015
		public ReorderListItem this[int index]
		{
			get
			{
				return (ReorderListItem)this.ChildList[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0001B8D5 File Offset: 0x00019AD5
		public void Add(ReorderListItem item)
		{
			this.ChildList.Add(item);
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0001B8E3 File Offset: 0x00019AE3
		public void Clear()
		{
			this.ChildList.Clear();
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0001B8F0 File Offset: 0x00019AF0
		public bool Contains(ReorderListItem item)
		{
			return this.ChildList.Contains(item);
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0001B8FE File Offset: 0x00019AFE
		public void CopyTo(ReorderListItem[] array, int arrayIndex)
		{
			this.ChildList.CopyTo(array, arrayIndex);
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000A91 RID: 2705 RVA: 0x0001B90D File Offset: 0x00019B0D
		public int Count
		{
			get
			{
				return this.ChildList.Count;
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000A92 RID: 2706 RVA: 0x0001B91A File Offset: 0x00019B1A
		public bool IsReadOnly
		{
			get
			{
				return this.ChildList.IsReadOnly;
			}
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x0001B927 File Offset: 0x00019B27
		public bool Remove(ReorderListItem item)
		{
			this.ChildList.Remove(item);
			return true;
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x0001B936 File Offset: 0x00019B36
		public IEnumerator<ReorderListItem> GetEnumerator()
		{
			return new ReorderListItemCollection.ReorderListItemEnumerator(this.ChildList.GetEnumerator());
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x0001B948 File Offset: 0x00019B48
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.ChildList.GetEnumerator();
		}

		// Token: 0x04000405 RID: 1029
		private ReorderList _parent;

		// Token: 0x0200017C RID: 380
		private class ReorderListItemEnumerator : IEnumerator<ReorderListItem>, IDisposable, IEnumerator
		{
			// Token: 0x06000A96 RID: 2710 RVA: 0x0001B955 File Offset: 0x00019B55
			public ReorderListItemEnumerator(IEnumerator baseEnum)
			{
				this._controlEnum = baseEnum;
			}

			// Token: 0x170003FA RID: 1018
			// (get) Token: 0x06000A97 RID: 2711 RVA: 0x0001B964 File Offset: 0x00019B64
			public ReorderListItem Current
			{
				get
				{
					return (ReorderListItem)this._controlEnum.Current;
				}
			}

			// Token: 0x06000A98 RID: 2712 RVA: 0x0001B976 File Offset: 0x00019B76
			public void Dispose()
			{
				this._controlEnum = null;
				GC.SuppressFinalize(this);
			}

			// Token: 0x170003FB RID: 1019
			// (get) Token: 0x06000A99 RID: 2713 RVA: 0x0001B985 File Offset: 0x00019B85
			object IEnumerator.Current
			{
				get
				{
					return (ReorderListItem)this._controlEnum.Current;
				}
			}

			// Token: 0x06000A9A RID: 2714 RVA: 0x0001B997 File Offset: 0x00019B97
			public bool MoveNext()
			{
				return this._controlEnum.MoveNext();
			}

			// Token: 0x06000A9B RID: 2715 RVA: 0x0001B9A4 File Offset: 0x00019BA4
			public void Reset()
			{
				this._controlEnum.Reset();
			}

			// Token: 0x04000406 RID: 1030
			private IEnumerator _controlEnum;
		}
	}
}
