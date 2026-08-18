using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000CF8 RID: 3320
	internal class OlapCellsDictionary
	{
		// Token: 0x06007BE6 RID: 31718 RVA: 0x001C7B80 File Offset: 0x001C5D80
		public OlapCellsDictionary(IEnumerable<IOlapCell> olapCells) : this()
		{
			foreach (IOlapCell cell in olapCells)
			{
				this.AddCell(cell);
			}
		}

		// Token: 0x06007BE7 RID: 31719 RVA: 0x001C7BD0 File Offset: 0x001C5DD0
		public OlapCellsDictionary()
		{
			this.dictionary = new Dictionary<int, IOlapCell>();
		}

		// Token: 0x1700279B RID: 10139
		// (get) Token: 0x06007BE8 RID: 31720 RVA: 0x001C7BE3 File Offset: 0x001C5DE3
		public int CellCount
		{
			get
			{
				return this.dictionary.Count;
			}
		}

		// Token: 0x06007BE9 RID: 31721 RVA: 0x001C7BF0 File Offset: 0x001C5DF0
		public void AddCell(IOlapCell cell)
		{
			if (cell == null)
			{
				return;
			}
			this.dictionary.Add(cell.Ordinal, cell);
		}

		// Token: 0x06007BEA RID: 31722 RVA: 0x001C7C08 File Offset: 0x001C5E08
		public IOlapCell GetCellByOrdinal(int ordinal)
		{
			IOlapCell result = null;
			this.dictionary.TryGetValue(ordinal, out result);
			return result;
		}

		// Token: 0x040021FE RID: 8702
		private readonly Dictionary<int, IOlapCell> dictionary;
	}
}
