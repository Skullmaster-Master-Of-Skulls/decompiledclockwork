using System;
using System.Globalization;
using Microsoft.AnalysisServices.AdomdClient;
using Telerik.Web.UI.PivotGrid.Core.Olap;

namespace Telerik.Web.UI.PivotGrid.DataProviders.Adomd
{
	// Token: 0x02000D5F RID: 3423
	internal class AdomdCellInfo : IOlapCell
	{
		// Token: 0x1700289E RID: 10398
		// (get) Token: 0x06007FAC RID: 32684 RVA: 0x001D2CBE File Offset: 0x001D0EBE
		// (set) Token: 0x06007FAD RID: 32685 RVA: 0x001D2CC6 File Offset: 0x001D0EC6
		public string FormattedValue { get; private set; }

		// Token: 0x1700289F RID: 10399
		// (get) Token: 0x06007FAE RID: 32686 RVA: 0x001D2CCF File Offset: 0x001D0ECF
		// (set) Token: 0x06007FAF RID: 32687 RVA: 0x001D2CD7 File Offset: 0x001D0ED7
		public object Value { get; private set; }

		// Token: 0x170028A0 RID: 10400
		// (get) Token: 0x06007FB0 RID: 32688 RVA: 0x001D2CE0 File Offset: 0x001D0EE0
		// (set) Token: 0x06007FB1 RID: 32689 RVA: 0x001D2CE8 File Offset: 0x001D0EE8
		public int Ordinal { get; private set; }

		// Token: 0x06007FB2 RID: 32690 RVA: 0x001D2CF4 File Offset: 0x001D0EF4
		public static AdomdCellInfo FromAdomdCell(Cell cellElement)
		{
			return new AdomdCellInfo
			{
				Ordinal = (int)cellElement.CellProperties["CellOrdinal"].Value,
				FormattedValue = cellElement.FormattedValue,
				Value = cellElement.Value
			};
		}

		// Token: 0x06007FB3 RID: 32691 RVA: 0x001D2D40 File Offset: 0x001D0F40
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Ordinal: {0} | Value: {1}", new object[]
			{
				this.Ordinal,
				this.Value
			});
		}
	}
}
