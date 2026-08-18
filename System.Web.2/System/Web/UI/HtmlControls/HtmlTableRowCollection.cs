using System;
using System.Collections;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x0200035F RID: 863
	public sealed class HtmlTableRowCollection : ICollection, IEnumerable
	{
		// Token: 0x060027F0 RID: 10224 RVA: 0x0008158A File Offset: 0x0007F78A
		internal HtmlTableRowCollection(HtmlTable owner)
		{
			this.owner = owner;
		}

		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x060027F1 RID: 10225 RVA: 0x00081599 File Offset: 0x0007F799
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

		// Token: 0x17000B11 RID: 2833
		public HtmlTableRow this[int index]
		{
			get
			{
				return (HtmlTableRow)this.owner.Controls[index];
			}
		}

		// Token: 0x060027F3 RID: 10227 RVA: 0x000815D2 File Offset: 0x0007F7D2
		public void Add(HtmlTableRow row)
		{
			this.Insert(-1, row);
		}

		// Token: 0x060027F4 RID: 10228 RVA: 0x000815DC File Offset: 0x0007F7DC
		public void Insert(int index, HtmlTableRow row)
		{
			this.owner.Controls.AddAt(index, row);
		}

		// Token: 0x060027F5 RID: 10229 RVA: 0x000815F0 File Offset: 0x0007F7F0
		public void Clear()
		{
			if (this.owner.HasControls())
			{
				this.owner.Controls.Clear();
			}
		}

		// Token: 0x060027F6 RID: 10230 RVA: 0x00081610 File Offset: 0x0007F810
		public void CopyTo(Array array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x060027F7 RID: 10231 RVA: 0x00004335 File Offset: 0x00002535
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x060027F8 RID: 10232 RVA: 0x00007722 File Offset: 0x00005922
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x060027F9 RID: 10233 RVA: 0x00007722 File Offset: 0x00005922
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060027FA RID: 10234 RVA: 0x00081640 File Offset: 0x0007F840
		public IEnumerator GetEnumerator()
		{
			return this.owner.Controls.GetEnumerator();
		}

		// Token: 0x060027FB RID: 10235 RVA: 0x00081652 File Offset: 0x0007F852
		public void Remove(HtmlTableRow row)
		{
			this.owner.Controls.Remove(row);
		}

		// Token: 0x060027FC RID: 10236 RVA: 0x00081665 File Offset: 0x0007F865
		public void RemoveAt(int index)
		{
			this.owner.Controls.RemoveAt(index);
		}

		// Token: 0x04001DE0 RID: 7648
		private HtmlTable owner;
	}
}
