using System;
using System.Collections;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x0200035D RID: 861
	public sealed class HtmlTableCellCollection : ICollection, IEnumerable
	{
		// Token: 0x060027D0 RID: 10192 RVA: 0x00081368 File Offset: 0x0007F568
		internal HtmlTableCellCollection(HtmlTableRow owner)
		{
			this.owner = owner;
		}

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x060027D1 RID: 10193 RVA: 0x00081377 File Offset: 0x0007F577
		public int Count
		{
			get
			{
				if (this.owner.HasControls())
				{
					return this.owner.Controls.Count;
				}
				return 0;
			}
		}

		// Token: 0x17000B04 RID: 2820
		public HtmlTableCell this[int index]
		{
			get
			{
				return (HtmlTableCell)this.owner.Controls[index];
			}
		}

		// Token: 0x060027D3 RID: 10195 RVA: 0x000813B0 File Offset: 0x0007F5B0
		public void Add(HtmlTableCell cell)
		{
			this.Insert(-1, cell);
		}

		// Token: 0x060027D4 RID: 10196 RVA: 0x000813BA File Offset: 0x0007F5BA
		public void Insert(int index, HtmlTableCell cell)
		{
			this.owner.Controls.AddAt(index, cell);
		}

		// Token: 0x060027D5 RID: 10197 RVA: 0x000813CE File Offset: 0x0007F5CE
		public void Clear()
		{
			if (this.owner.HasControls())
			{
				this.owner.Controls.Clear();
			}
		}

		// Token: 0x060027D6 RID: 10198 RVA: 0x000813ED File Offset: 0x0007F5ED
		public IEnumerator GetEnumerator()
		{
			return this.owner.Controls.GetEnumerator();
		}

		// Token: 0x060027D7 RID: 10199 RVA: 0x00081400 File Offset: 0x0007F600
		public void CopyTo(Array array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x060027D8 RID: 10200 RVA: 0x00004335 File Offset: 0x00002535
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x060027D9 RID: 10201 RVA: 0x00007722 File Offset: 0x00005922
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x060027DA RID: 10202 RVA: 0x00007722 File Offset: 0x00005922
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060027DB RID: 10203 RVA: 0x00081430 File Offset: 0x0007F630
		public void Remove(HtmlTableCell cell)
		{
			this.owner.Controls.Remove(cell);
		}

		// Token: 0x060027DC RID: 10204 RVA: 0x00081443 File Offset: 0x0007F643
		public void RemoveAt(int index)
		{
			this.owner.Controls.RemoveAt(index);
		}

		// Token: 0x04001DDE RID: 7646
		private HtmlTableRow owner;
	}
}
