using System;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Collections
{
	// Token: 0x02000032 RID: 50
	public class WorksheetChartsCollection : XlsWorksheetChartsCollection
	{
		// Token: 0x0600039E RID: 926 RVA: 0x00020C7C File Offset: 0x0001FC7C
		internal WorksheetChartsCollection(spr\u2158 A_0, object A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00020C94 File Offset: 0x0001FC94
		public new Chart Add()
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
			return (Chart)base.Add();
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00020CDC File Offset: 0x0001FCDC
		public Chart Add(ExcelChartType chartType)
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
			Chart chart = (Chart)base.Add();
			chart.ChartType = chartType;
			return chart;
		}

		// Token: 0x1700013C RID: 316
		public Chart this[int index]
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
				return (Chart)base[index];
			}
		}
	}
}
