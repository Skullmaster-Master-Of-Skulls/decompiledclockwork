using System;
using System.Collections.Generic;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Charts;

namespace Spire.Xls.Charts
{
	// Token: 0x020001AC RID: 428
	public class ChartCategoryAxis : XlsChartCategoryAxis
	{
		// Token: 0x06001704 RID: 5892 RVA: 0x000DE954 File Offset: 0x000DD954
		internal ChartCategoryAxis(spr\u2158 A_0, object A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06001705 RID: 5893 RVA: 0x000DE96C File Offset: 0x000DD96C
		internal ChartCategoryAxis(spr\u2158 A_0, object A_1, AxisType A_2) : base(A_0, A_1, A_2)
		{
		}

		// Token: 0x06001706 RID: 5894 RVA: 0x000DE984 File Offset: 0x000DD984
		internal ChartCategoryAxis(spr\u2158 A_0, object A_1, AxisType A_2, bool A_3) : base(A_0, A_1, A_2, A_3)
		{
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x000DE99C File Offset: 0x000DD99C
		internal ChartCategoryAxis(spr\u2158 A_0, object A_1, IList<BiffRecordRaw> A_2, ref int A_3) : base(A_0, A_1, A_2, ref A_3)
		{
		}

		// Token: 0x06001708 RID: 5896 RVA: 0x000DE9B4 File Offset: 0x000DD9B4
		internal ChartCategoryAxis(spr\u2158 A_0, object A_1, IList<BiffRecordRaw> A_2, ref int A_3, bool A_4) : base(A_0, A_1, A_2, ref A_3, A_4)
		{
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x06001709 RID: 5897 RVA: 0x000DE9D0 File Offset: 0x000DD9D0
		// (set) Token: 0x0600170A RID: 5898 RVA: 0x000DEA18 File Offset: 0x000DDA18
		public new CellRange CategoryLabels
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
				return (CellRange)base.CategoryLabels;
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
				base.CategoryLabels = value;
			}
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x0600170B RID: 5899 RVA: 0x000DEA5C File Offset: 0x000DDA5C
		public new ExcelFont Font
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
				return new ExcelFont(base.Font);
			}
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x0600170C RID: 5900 RVA: 0x000DEAA4 File Offset: 0x000DDAA4
		public new ChartTextArea TitleArea
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
				return base.TitleArea as ChartTextArea;
			}
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x0600170D RID: 5901 RVA: 0x000DEAEC File Offset: 0x000DDAEC
		public new ChartGridLine MajorGridLines
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
				return base.MajorGridLines as ChartGridLine;
			}
		}

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x0600170E RID: 5902 RVA: 0x000DEB34 File Offset: 0x000DDB34
		public new ChartGridLine MinorGridLines
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
				return base.MinorGridLines as ChartGridLine;
			}
		}
	}
}
