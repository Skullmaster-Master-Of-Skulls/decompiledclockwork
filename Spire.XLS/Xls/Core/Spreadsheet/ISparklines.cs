using System;
using System.Collections.Generic;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x0200015D RID: 349
	public interface ISparklines : IList<ISparkline>
	{
		// Token: 0x06000F92 RID: 3986
		Sparkline Add();

		// Token: 0x06000F93 RID: 3987
		void Add(CellRange dataRange, CellRange referenceRange);

		// Token: 0x06000F94 RID: 3988
		void RefreshRanges(CellRange dataRange, CellRange referenceRange);

		// Token: 0x06000F95 RID: 3989
		void Add(CellRange dataRange, CellRange referenceRange, bool isVertical);

		// Token: 0x06000F96 RID: 3990
		void RefreshRanges(CellRange dataRange, CellRange referenceRange, bool isVertical);

		// Token: 0x06000F97 RID: 3991
		void Clear(Sparkline sparkline);
	}
}
