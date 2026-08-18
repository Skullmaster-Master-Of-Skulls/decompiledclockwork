using System;
using Spire.Xls.Core.Spreadsheet.Sorting;

namespace Spire.Xls.Core
{
	// Token: 0x02000178 RID: 376
	public interface IDataSort
	{
		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x060011F3 RID: 4595
		// (set) Token: 0x060011F4 RID: 4596
		bool IsCaseSensitive { get; set; }

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x060011F5 RID: 4597
		// (set) Token: 0x060011F6 RID: 4598
		bool IsIncludeTitle { get; set; }

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x060011F7 RID: 4599
		// (set) Token: 0x060011F8 RID: 4600
		SortOrientationType Orientation { get; set; }

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x060011F9 RID: 4601
		// (set) Token: 0x060011FA RID: 4602
		SortColumns SortColumns { get; set; }

		// Token: 0x060011FB RID: 4603
		void Sort(CellRange range);
	}
}
