using System;
using Spire.Xls.Core.Interfaces;

namespace Spire.Xls.Core
{
	// Token: 0x02000357 RID: 855
	public interface IPageSetup : IPageSetupBase
	{
		// Token: 0x17000CC9 RID: 3273
		// (get) Token: 0x06003431 RID: 13361
		// (set) Token: 0x06003432 RID: 13362
		int FitToPagesTall { get; set; }

		// Token: 0x17000CCA RID: 3274
		// (get) Token: 0x06003433 RID: 13363
		// (set) Token: 0x06003434 RID: 13364
		int FitToPagesWide { get; set; }

		// Token: 0x17000CCB RID: 3275
		// (get) Token: 0x06003435 RID: 13365
		// (set) Token: 0x06003436 RID: 13366
		bool IsPrintGridlines { get; set; }

		// Token: 0x17000CCC RID: 3276
		// (get) Token: 0x06003437 RID: 13367
		// (set) Token: 0x06003438 RID: 13368
		bool IsPrintHeadings { get; set; }

		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x06003439 RID: 13369
		// (set) Token: 0x0600343A RID: 13370
		string PrintArea { get; set; }

		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x0600343B RID: 13371
		// (set) Token: 0x0600343C RID: 13372
		string PrintTitleColumns { get; set; }

		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x0600343D RID: 13373
		// (set) Token: 0x0600343E RID: 13374
		string PrintTitleRows { get; set; }

		// Token: 0x17000CD0 RID: 3280
		// (get) Token: 0x0600343F RID: 13375
		// (set) Token: 0x06003440 RID: 13376
		bool IsSummaryRowBelow { get; set; }

		// Token: 0x17000CD1 RID: 3281
		// (get) Token: 0x06003441 RID: 13377
		// (set) Token: 0x06003442 RID: 13378
		bool IsSummaryColumnRight { get; set; }

		// Token: 0x17000CD2 RID: 3282
		// (get) Token: 0x06003443 RID: 13379
		// (set) Token: 0x06003444 RID: 13380
		bool IsFitToPage { get; set; }
	}
}
