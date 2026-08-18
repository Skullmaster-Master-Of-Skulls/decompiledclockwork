using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004B6 RID: 1206
	public sealed class RepeaterItemCollection : ICollection, IEnumerable
	{
		// Token: 0x06003C55 RID: 15445 RVA: 0x000C3956 File Offset: 0x000C1B56
		public RepeaterItemCollection(ArrayList items)
		{
			this.items = items;
		}

		// Token: 0x170011A0 RID: 4512
		// (get) Token: 0x06003C56 RID: 15446 RVA: 0x000C3965 File Offset: 0x000C1B65
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x170011A1 RID: 4513
		// (get) Token: 0x06003C57 RID: 15447 RVA: 0x00007722 File Offset: 0x00005922
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170011A2 RID: 4514
		// (get) Token: 0x06003C58 RID: 15448 RVA: 0x00007722 File Offset: 0x00005922
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170011A3 RID: 4515
		// (get) Token: 0x06003C59 RID: 15449 RVA: 0x00004335 File Offset: 0x00002535
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170011A4 RID: 4516
		public RepeaterItem this[int index]
		{
			get
			{
				return (RepeaterItem)this.items[index];
			}
		}

		// Token: 0x06003C5B RID: 15451 RVA: 0x000C3988 File Offset: 0x000C1B88
		public void CopyTo(Array array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x06003C5C RID: 15452 RVA: 0x000C39B8 File Offset: 0x000C1BB8
		public IEnumerator GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x04002378 RID: 9080
		private ArrayList items;
	}
}
