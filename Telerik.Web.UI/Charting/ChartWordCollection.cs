using System;
using System.Collections;

namespace Telerik.Charting
{
	// Token: 0x0200171D RID: 5917
	internal class ChartWordCollection : CollectionBase
	{
		// Token: 0x17004605 RID: 17925
		// (get) Token: 0x0600E5E5 RID: 58853 RVA: 0x00330DCE File Offset: 0x0032EFCE
		// (set) Token: 0x0600E5E6 RID: 58854 RVA: 0x00330DD6 File Offset: 0x0032EFD6
		internal ChartString Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				this.parent = value;
			}
		}

		// Token: 0x17004606 RID: 17926
		internal ChartWord this[int index]
		{
			get
			{
				return (ChartWord)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x17004607 RID: 17927
		// (get) Token: 0x0600E5E9 RID: 58857 RVA: 0x00330E01 File Offset: 0x0032F001
		internal ChartWord Last
		{
			get
			{
				if (base.Count > 0)
				{
					return this[base.Count - 1];
				}
				return null;
			}
		}

		// Token: 0x0600E5EA RID: 58858 RVA: 0x00330E1C File Offset: 0x0032F01C
		internal ChartWordCollection(ChartString parent)
		{
			this.parent = parent;
		}

		// Token: 0x0600E5EB RID: 58859 RVA: 0x00330E2B File Offset: 0x0032F02B
		internal int Add(ChartWord word)
		{
			word.Parent = this;
			base.List.Add(word);
			return base.Count - 1;
		}

		// Token: 0x0600E5EC RID: 58860 RVA: 0x00330E4C File Offset: 0x0032F04C
		internal ChartWord RemoveLast()
		{
			ChartWord last = this.Last;
			base.List.Remove(last);
			return last;
		}

		// Token: 0x0600E5ED RID: 58861 RVA: 0x00330E6D File Offset: 0x0032F06D
		internal void InsertAsFirst(ChartWord str)
		{
			base.List.Insert(0, str);
		}

		// Token: 0x0600E5EE RID: 58862 RVA: 0x00330E7C File Offset: 0x0032F07C
		internal ChartWordCollection Clone()
		{
			ChartWordCollection chartWordCollection = new ChartWordCollection(null);
			foreach (object obj in this)
			{
				ChartWord chartWord = (ChartWord)obj;
				chartWordCollection.Add(chartWord.Clone());
			}
			return chartWordCollection;
		}

		// Token: 0x04004221 RID: 16929
		private ChartString parent;
	}
}
