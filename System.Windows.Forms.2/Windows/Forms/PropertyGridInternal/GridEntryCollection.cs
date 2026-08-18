using System;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x02000506 RID: 1286
	internal class GridEntryCollection : GridItemCollection
	{
		// Token: 0x06005487 RID: 21639 RVA: 0x00161CD8 File Offset: 0x0015FED8
		public GridEntryCollection(GridEntry owner, GridEntry[] entries) : base(entries)
		{
			this.owner = owner;
		}

		// Token: 0x06005488 RID: 21640 RVA: 0x00161CF8 File Offset: 0x0015FEF8
		public void AddRange(GridEntry[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			GridItem[] entries;
			if (this.entries != null)
			{
				GridEntry[] array = new GridEntry[this.entries.Length + value.Length];
				this.entries.CopyTo(array, 0);
				value.CopyTo(array, this.entries.Length);
				entries = array;
				this.entries = entries;
				return;
			}
			entries = (GridEntry[])value.Clone();
			this.entries = entries;
		}

		// Token: 0x06005489 RID: 21641 RVA: 0x00161D68 File Offset: 0x0015FF68
		public void Clear()
		{
			GridItem[] entries = new GridEntry[0];
			this.entries = entries;
		}

		// Token: 0x0600548A RID: 21642 RVA: 0x00161D83 File Offset: 0x0015FF83
		public void CopyTo(Array dest, int index)
		{
			this.entries.CopyTo(dest, index);
		}

		// Token: 0x0600548B RID: 21643 RVA: 0x00161D92 File Offset: 0x0015FF92
		internal GridEntry GetEntry(int index)
		{
			return (GridEntry)this.entries[index];
		}

		// Token: 0x0600548C RID: 21644 RVA: 0x00161DA1 File Offset: 0x0015FFA1
		internal int GetEntry(GridEntry child)
		{
			return Array.IndexOf<GridItem>(this.entries, child);
		}

		// Token: 0x0600548D RID: 21645 RVA: 0x00161DAF File Offset: 0x0015FFAF
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600548E RID: 21646 RVA: 0x00161DC0 File Offset: 0x0015FFC0
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.owner != null && this.entries != null)
			{
				for (int i = 0; i < this.entries.Length; i++)
				{
					if (this.entries[i] != null)
					{
						((GridEntry)this.entries[i]).Dispose();
						this.entries[i] = null;
					}
				}
				GridItem[] entries = new GridEntry[0];
				this.entries = entries;
			}
		}

		// Token: 0x0600548F RID: 21647 RVA: 0x00161E28 File Offset: 0x00160028
		~GridEntryCollection()
		{
			this.Dispose(false);
		}

		// Token: 0x0400370D RID: 14093
		private GridEntry owner;
	}
}
