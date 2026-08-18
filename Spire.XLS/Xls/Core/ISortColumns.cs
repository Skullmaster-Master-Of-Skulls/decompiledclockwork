using System;
using System.Collections;
using Spire.Xls.Core.Spreadsheet.Sorting;

namespace Spire.Xls.Core
{
	// Token: 0x02000176 RID: 374
	public interface ISortColumns : IEnumerable
	{
		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x060011D4 RID: 4564
		int Count { get; }

		// Token: 0x060011D5 RID: 4565
		SortColumn Add(int key, SortComparsionType sortComparsionType, OrderBy orderBy);

		// Token: 0x060011D6 RID: 4566
		SortColumn Add(int key, OrderBy orderBy);

		// Token: 0x060011D7 RID: 4567
		void Remove(int key);

		// Token: 0x060011D8 RID: 4568
		void Remove(SortColumn sortField);

		// Token: 0x17000649 RID: 1609
		SortColumn this[int index]
		{
			get;
		}
	}
}
