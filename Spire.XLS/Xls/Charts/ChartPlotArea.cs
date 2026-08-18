using System;
using System.Collections.Generic;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Charts;

namespace Spire.Xls.Charts
{
	// Token: 0x0200017C RID: 380
	public class ChartPlotArea : XlsChartPlotArea
	{
		// Token: 0x0600121A RID: 4634 RVA: 0x000B0620 File Offset: 0x000AF620
		internal ChartPlotArea(spr\u2158 A_0, object A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x000B0638 File Offset: 0x000AF638
		internal ChartPlotArea(spr\u2158 A_0, object A_1, ExcelChartType A_2) : base(A_0, A_1, A_2)
		{
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x000B0650 File Offset: 0x000AF650
		internal ChartPlotArea(spr\u2158 A_0, object A_1, IList<BiffRecordRaw> A_2, ref int A_3) : base(A_0, A_1, A_2, ref A_3)
		{
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x0600121D RID: 4637 RVA: 0x000B0668 File Offset: 0x000AF668
		public new ChartBorder Border
		{
			get
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_7B;
					case 1:
						this.m_border = new ChartBorder((spr\u2158)base.ReservedHandle, this);
						num = 0;
						continue;
					}
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_7D;
					}
					if (false)
					{
					}
					if (this.m_border != null)
					{
						break;
					}
					num = 1;
				}
				IL_7B:
				IL_7D:
				return (ChartBorder)this.m_border;
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x0600121E RID: 4638 RVA: 0x000B0700 File Offset: 0x000AF700
		public new ChartInterior Interior
		{
			get
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						this.ᜂ = new ChartInterior((spr\u2158)base.ReservedHandle, this);
						num = 2;
						continue;
					case 2:
						goto IL_7B;
					}
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_7D;
					}
					if (false)
					{
					}
					if (this.ᜂ != null)
					{
						break;
					}
					num = 1;
				}
				IL_7B:
				IL_7D:
				return (ChartInterior)this.ᜂ;
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x0600121F RID: 4639 RVA: 0x000B0798 File Offset: 0x000AF798
		public new Workbook Workbook
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
				return base.Workbook.InnerWorkBook;
			}
		}
	}
}
