using System;
using System.Collections.Generic;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Charts;

namespace Spire.Xls.Charts
{
	// Token: 0x0200018C RID: 396
	public class ChartValueAxis : XlsChartValueAxis
	{
		// Token: 0x06001388 RID: 5000 RVA: 0x000BDB30 File Offset: 0x000BCB30
		internal ChartValueAxis(spr\u2158 A_0, object A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x000BDB48 File Offset: 0x000BCB48
		internal ChartValueAxis(spr\u2158 A_0, object A_1, AxisType A_2) : base(A_0, A_1, A_2)
		{
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x000BDB60 File Offset: 0x000BCB60
		internal ChartValueAxis(spr\u2158 A_0, object A_1, AxisType A_2, bool A_3) : base(A_0, A_1, A_2, A_3)
		{
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x000BDB78 File Offset: 0x000BCB78
		internal ChartValueAxis(spr\u2158 A_0, object A_1, IList<BiffRecordRaw> A_2, ref int A_3, bool A_4) : base(A_0, A_1, A_2, ref A_3, A_4)
		{
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x000BDB94 File Offset: 0x000BCB94
		public void SetTitle(ChartTextArea text)
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
			base.SetTitle(text);
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x0600138D RID: 5005 RVA: 0x000BDBD8 File Offset: 0x000BCBD8
		public new ExcelFont Font
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
				return new ExcelFont(base.Font);
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x0600138E RID: 5006 RVA: 0x000BDC20 File Offset: 0x000BCC20
		public new ChartGridLine MajorGridLines
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
				return (ChartGridLine)base.MajorGridLines;
			}
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x0600138F RID: 5007 RVA: 0x000BDC68 File Offset: 0x000BCC68
		public new ChartGridLine MinorGridLines
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
				return (ChartGridLine)base.MinorGridLines;
			}
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06001390 RID: 5008 RVA: 0x000BDCB0 File Offset: 0x000BCCB0
		public new ChartTextArea TitleArea
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
				return (ChartTextArea)base.TitleArea;
			}
		}
	}
}
