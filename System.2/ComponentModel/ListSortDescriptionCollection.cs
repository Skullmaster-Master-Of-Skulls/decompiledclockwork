using System;
using System.Collections;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000589 RID: 1417
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class ListSortDescriptionCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x06003445 RID: 13381 RVA: 0x000E4D90 File Offset: 0x000E2F90
		public ListSortDescriptionCollection()
		{
		}

		// Token: 0x06003446 RID: 13382 RVA: 0x000E4DA4 File Offset: 0x000E2FA4
		public ListSortDescriptionCollection(ListSortDescription[] sorts)
		{
			if (sorts != null)
			{
				for (int i = 0; i < sorts.Length; i++)
				{
					this.sorts.Add(sorts[i]);
				}
			}
		}

		// Token: 0x17000CC7 RID: 3271
		public ListSortDescription this[int index]
		{
			get
			{
				return (ListSortDescription)this.sorts[index];
			}
			set
			{
				throw new InvalidOperationException(SR.GetString("CantModifyListSortDescriptionCollection"));
			}
		}

		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x06003449 RID: 13385 RVA: 0x000E4E06 File Offset: 0x000E3006
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000CC9 RID: 3273
		// (get) Token: 0x0600344A RID: 13386 RVA: 0x000E4E09 File Offset: 0x000E3009
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000CCA RID: 3274
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				throw new InvalidOperationException(SR.GetString("CantModifyListSortDescriptionCollection"));
			}
		}

		// Token: 0x0600344D RID: 13389 RVA: 0x000E4E26 File Offset: 0x000E3026
		int IList.Add(object value)
		{
			throw new InvalidOperationException(SR.GetString("CantModifyListSortDescriptionCollection"));
		}

		// Token: 0x0600344E RID: 13390 RVA: 0x000E4E37 File Offset: 0x000E3037
		void IList.Clear()
		{
			throw new InvalidOperationException(SR.GetString("CantModifyListSortDescriptionCollection"));
		}

		// Token: 0x0600344F RID: 13391 RVA: 0x000E4E48 File Offset: 0x000E3048
		public bool Contains(object value)
		{
			return ((IList)this.sorts).Contains(value);
		}

		// Token: 0x06003450 RID: 13392 RVA: 0x000E4E56 File Offset: 0x000E3056
		public int IndexOf(object value)
		{
			return ((IList)this.sorts).IndexOf(value);
		}

		// Token: 0x06003451 RID: 13393 RVA: 0x000E4E64 File Offset: 0x000E3064
		void IList.Insert(int index, object value)
		{
			throw new InvalidOperationException(SR.GetString("CantModifyListSortDescriptionCollection"));
		}

		// Token: 0x06003452 RID: 13394 RVA: 0x000E4E75 File Offset: 0x000E3075
		void IList.Remove(object value)
		{
			throw new InvalidOperationException(SR.GetString("CantModifyListSortDescriptionCollection"));
		}

		// Token: 0x06003453 RID: 13395 RVA: 0x000E4E86 File Offset: 0x000E3086
		void IList.RemoveAt(int index)
		{
			throw new InvalidOperationException(SR.GetString("CantModifyListSortDescriptionCollection"));
		}

		// Token: 0x17000CCB RID: 3275
		// (get) Token: 0x06003454 RID: 13396 RVA: 0x000E4E97 File Offset: 0x000E3097
		public int Count
		{
			get
			{
				return this.sorts.Count;
			}
		}

		// Token: 0x17000CCC RID: 3276
		// (get) Token: 0x06003455 RID: 13397 RVA: 0x000E4EA4 File Offset: 0x000E30A4
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x06003456 RID: 13398 RVA: 0x000E4EA7 File Offset: 0x000E30A7
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06003457 RID: 13399 RVA: 0x000E4EAA File Offset: 0x000E30AA
		public void CopyTo(Array array, int index)
		{
			this.sorts.CopyTo(array, index);
		}

		// Token: 0x06003458 RID: 13400 RVA: 0x000E4EB9 File Offset: 0x000E30B9
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.sorts.GetEnumerator();
		}

		// Token: 0x040029EB RID: 10731
		private ArrayList sorts = new ArrayList();
	}
}
