using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200054F RID: 1359
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class DataListItemCollection : ICollection, IEnumerable
	{
		// Token: 0x0600431C RID: 17180 RVA: 0x0011553B File Offset: 0x0011453B
		public DataListItemCollection(ArrayList items)
		{
			this.items = items;
		}

		// Token: 0x17001053 RID: 4179
		// (get) Token: 0x0600431D RID: 17181 RVA: 0x0011554A File Offset: 0x0011454A
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x17001054 RID: 4180
		// (get) Token: 0x0600431E RID: 17182 RVA: 0x00115557 File Offset: 0x00114557
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001055 RID: 4181
		// (get) Token: 0x0600431F RID: 17183 RVA: 0x0011555A File Offset: 0x0011455A
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001056 RID: 4182
		// (get) Token: 0x06004320 RID: 17184 RVA: 0x0011555D File Offset: 0x0011455D
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17001057 RID: 4183
		public DataListItem this[int index]
		{
			get
			{
				return (DataListItem)this.items[index];
			}
		}

		// Token: 0x06004322 RID: 17186 RVA: 0x00115574 File Offset: 0x00114574
		public void CopyTo(Array array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x06004323 RID: 17187 RVA: 0x001155A4 File Offset: 0x001145A4
		public IEnumerator GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x04002947 RID: 10567
		private ArrayList items;
	}
}
