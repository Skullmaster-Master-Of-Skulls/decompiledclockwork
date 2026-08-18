using System;
using System.Collections;

namespace System.Windows.Forms
{
	// Token: 0x0200026B RID: 619
	public class GridItemCollection : ICollection, IEnumerable
	{
		// Token: 0x060027A8 RID: 10152 RVA: 0x000B8FB5 File Offset: 0x000B71B5
		internal GridItemCollection(GridItem[] entries)
		{
			if (entries == null)
			{
				this.entries = new GridItem[0];
				return;
			}
			this.entries = entries;
		}

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x060027A9 RID: 10153 RVA: 0x000B8FD4 File Offset: 0x000B71D4
		public int Count
		{
			get
			{
				return this.entries.Length;
			}
		}

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x060027AA RID: 10154 RVA: 0x00006C59 File Offset: 0x00004E59
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x060027AB RID: 10155 RVA: 0x00011A20 File Offset: 0x0000FC20
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000933 RID: 2355
		public GridItem this[int index]
		{
			get
			{
				return this.entries[index];
			}
		}

		// Token: 0x17000934 RID: 2356
		public GridItem this[string label]
		{
			get
			{
				foreach (GridItem gridItem in this.entries)
				{
					if (gridItem.Label == label)
					{
						return gridItem;
					}
				}
				return null;
			}
		}

		// Token: 0x060027AE RID: 10158 RVA: 0x000B901F File Offset: 0x000B721F
		void ICollection.CopyTo(Array dest, int index)
		{
			if (this.entries.Length != 0)
			{
				Array.Copy(this.entries, 0, dest, index, this.entries.Length);
			}
		}

		// Token: 0x060027AF RID: 10159 RVA: 0x000B9040 File Offset: 0x000B7240
		public IEnumerator GetEnumerator()
		{
			return this.entries.GetEnumerator();
		}

		// Token: 0x0400105A RID: 4186
		public static GridItemCollection Empty = new GridItemCollection(new GridItem[0]);

		// Token: 0x0400105B RID: 4187
		internal GridItem[] entries;
	}
}
