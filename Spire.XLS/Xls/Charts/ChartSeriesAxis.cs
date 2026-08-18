using System;
using System.Collections.Generic;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Charts;

namespace Spire.Xls.Charts
{
	// Token: 0x02000186 RID: 390
	public class ChartSeriesAxis : XlsChartSeriesAxis
	{
		// Token: 0x0600130C RID: 4876 RVA: 0x000BA970 File Offset: 0x000B9970
		internal ChartSeriesAxis(spr\u2158 A_0, object A_1, AxisType A_2) : base(A_0, A_1, A_2)
		{
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x000BA988 File Offset: 0x000B9988
		internal ChartSeriesAxis(spr\u2158 A_0, object A_1, AxisType A_2, bool A_3) : base(A_0, A_1, A_2, A_3)
		{
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x000BA9A0 File Offset: 0x000B99A0
		internal ChartSeriesAxis(spr\u2158 A_0, object A_1, IList<BiffRecordRaw> A_2, ref int A_3, bool A_4) : base(A_0, A_1, A_2, ref A_3, A_4)
		{
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x000BA9BC File Offset: 0x000B99BC
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

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06001310 RID: 4880 RVA: 0x000BAA00 File Offset: 0x000B9A00
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

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06001311 RID: 4881 RVA: 0x000BAA48 File Offset: 0x000B9A48
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

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06001312 RID: 4882 RVA: 0x000BAA90 File Offset: 0x000B9A90
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

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06001313 RID: 4883 RVA: 0x000BAAD8 File Offset: 0x000B9AD8
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
