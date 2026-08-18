using System;
using Spire.Xls.Charts;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Shapes;

namespace Spire.Xls
{
	// Token: 0x0200005B RID: 91
	public class Chart : XlsChartShape
	{
		// Token: 0x060008A2 RID: 2210 RVA: 0x0005972C File Offset: 0x0005872C
		internal Chart(spr\u2158 A_0, object A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x00059744 File Offset: 0x00058744
		internal Chart(spr\u2158 A_0, object A_1, sprὙ A_2, ExcelParseOptions A_3) : base(A_0, A_1, A_2, A_3)
		{
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x060008A4 RID: 2212 RVA: 0x0005975C File Offset: 0x0005875C
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

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x060008A5 RID: 2213 RVA: 0x000597A4 File Offset: 0x000587A4
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

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x060008A6 RID: 2214 RVA: 0x000597EC File Offset: 0x000587EC
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

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x00059834 File Offset: 0x00058834
		// (set) Token: 0x060008A8 RID: 2216 RVA: 0x0005987C File Offset: 0x0005887C
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

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x060008A9 RID: 2217 RVA: 0x000598C0 File Offset: 0x000588C0
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

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x060008AA RID: 2218 RVA: 0x00059908 File Offset: 0x00058908
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

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x060008AB RID: 2219 RVA: 0x00059950 File Offset: 0x00058950
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

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x060008AC RID: 2220 RVA: 0x00059998 File Offset: 0x00058998
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

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x000599E0 File Offset: 0x000589E0
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

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x060008AE RID: 2222 RVA: 0x00059A28 File Offset: 0x00058A28
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

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x00059A70 File Offset: 0x00058A70
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

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x060008B0 RID: 2224 RVA: 0x00059AB8 File Offset: 0x00058AB8
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

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x060008B1 RID: 2225 RVA: 0x00059B00 File Offset: 0x00058B00
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

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x060008B2 RID: 2226 RVA: 0x00059B48 File Offset: 0x00058B48
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

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x060008B3 RID: 2227 RVA: 0x00059B90 File Offset: 0x00058B90
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

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x060008B4 RID: 2228 RVA: 0x00059BDC File Offset: 0x00058BDC
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
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x00059C24 File Offset: 0x00058C24
		public new Worksheet Worksheet
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
				return (Worksheet)base.Worksheet;
			}
		}
	}
}
