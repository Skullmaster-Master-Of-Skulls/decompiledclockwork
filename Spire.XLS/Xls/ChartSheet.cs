using System;
using System.Collections;
using System.Collections.Generic;
using Spire.Xls.Charts;
using Spire.Xls.Collections;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Charts;
using Spire.Xls.Core.Spreadsheet.Security;

namespace Spire.Xls
{
	// Token: 0x02000062 RID: 98
	public class ChartSheet : XlsChart
	{
		// Token: 0x060009E4 RID: 2532 RVA: 0x00060E44 File Offset: 0x0005FE44
		internal ChartSheet(spr\u2158 A_0, object A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x00060E5C File Offset: 0x0005FE5C
		internal ChartSheet(spr\u2158 A_0, object A_1, IList A_2, ref int A_3, ExcelParseOptions A_4) : base(A_0, A_1, A_2, ref A_3, A_4)
		{
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x00060E76 File Offset: 0x0005FE76
		internal ChartSheet(spr\u2158 A_0, object A_1, sprἛ A_2, ExcelParseOptions A_3, bool A_4, Dictionary<int, int> A_5, IDecryptor A_6) : base(A_0, A_1, A_2, A_3, A_4, A_5, A_6)
		{
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x060009E7 RID: 2535 RVA: 0x00060E8C File Offset: 0x0005FE8C
		public new ChartSeries Series
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return base.Series as ChartSeries;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x00060ED4 File Offset: 0x0005FED4
		public new ChartTextArea ChartTitleArea
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return (ChartTextArea)base.ChartTitleArea;
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x060009E9 RID: 2537 RVA: 0x00060F1C File Offset: 0x0005FF1C
		public new ChartArea ChartArea
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return (ChartArea)base.ChartArea;
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x060009EA RID: 2538 RVA: 0x00060F64 File Offset: 0x0005FF64
		// (set) Token: 0x060009EB RID: 2539 RVA: 0x00060FAC File Offset: 0x0005FFAC
		public new CellRange DataRange
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return (CellRange)base.DataRange;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				base.DataRange = value;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x060009EC RID: 2540 RVA: 0x00060FF0 File Offset: 0x0005FFF0
		public new ChartDataTable DataTable
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return (ChartDataTable)base.DataTable;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x060009ED RID: 2541 RVA: 0x00061038 File Offset: 0x00060038
		public new ChartWallOrFloor Floor
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return (ChartWallOrFloor)base.Floor;
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x060009EE RID: 2542 RVA: 0x00061080 File Offset: 0x00060080
		public new ChartLegend Legend
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return (ChartLegend)base.Legend;
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x000610C8 File Offset: 0x000600C8
		public new ChartPageSetup PageSetup
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return (ChartPageSetup)base.PageSetup;
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x060009F0 RID: 2544 RVA: 0x00061110 File Offset: 0x00060110
		public new ChartPlotArea PlotArea
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return (ChartPlotArea)base.PlotArea;
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x060009F1 RID: 2545 RVA: 0x00061158 File Offset: 0x00060158
		public new ChartCategoryAxis PrimaryCategoryAxis
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return (ChartCategoryAxis)base.PrimaryCategoryAxis;
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x060009F2 RID: 2546 RVA: 0x000611A0 File Offset: 0x000601A0
		public new ChartValueAxis PrimaryValueAxis
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return (ChartValueAxis)base.PrimaryValueAxis;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x060009F3 RID: 2547 RVA: 0x000611E8 File Offset: 0x000601E8
		public new ChartSeriesAxis PrimarySerieAxis
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return (ChartSeriesAxis)base.PrimarySerieAxis;
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x060009F4 RID: 2548 RVA: 0x00061230 File Offset: 0x00060230
		public new ChartCategoryAxis SecondaryCategoryAxis
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return (ChartCategoryAxis)base.SecondaryCategoryAxis;
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x060009F5 RID: 2549 RVA: 0x00061278 File Offset: 0x00060278
		public new ChartValueAxis SecondaryValueAxis
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return (ChartValueAxis)base.SecondaryValueAxis;
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x060009F6 RID: 2550 RVA: 0x000612C0 File Offset: 0x000602C0
		public new Workbook Workbook
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return ((XlsWorkbook)base.Workbook).InnerWorkBook;
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x060009F7 RID: 2551 RVA: 0x0006130C File Offset: 0x0006030C
		// (set) Token: 0x060009F8 RID: 2552 RVA: 0x00061354 File Offset: 0x00060354
		public new ChartWallOrFloor Walls
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return (ChartWallOrFloor)base.Walls;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				base.Walls = value;
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x060009F9 RID: 2553 RVA: 0x00061398 File Offset: 0x00060398
		public new CommentsCollection Comments
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return base.Comments as CommentsCollection;
			}
		}
	}
}
