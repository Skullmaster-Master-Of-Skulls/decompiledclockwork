using System;
using System.Collections;
using System.Drawing;

namespace Spire.Xls.Core
{
	// Token: 0x0200001F RID: 31
	public interface IBorders : IEnumerable, IExcelApplication
	{
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000256 RID: 598
		// (set) Token: 0x06000257 RID: 599
		ExcelColors KnownColor { get; set; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000258 RID: 600
		// (set) Token: 0x06000259 RID: 601
		Color Color { get; set; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600025A RID: 602
		int Count { get; }

		// Token: 0x170000F4 RID: 244
		IBorder this[BordersLineType Index]
		{
			get;
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600025C RID: 604
		// (set) Token: 0x0600025D RID: 605
		LineStyleType LineStyle { get; set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600025E RID: 606
		// (set) Token: 0x0600025F RID: 607
		LineStyleType Value { get; set; }
	}
}
