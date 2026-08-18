using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.AdomdClient;
using Telerik.Web.UI.PivotGrid.Core.Olap;

namespace Telerik.Web.UI.PivotGrid.DataProviders.Adomd
{
	// Token: 0x02000D5A RID: 3418
	internal class AdomdResponseData : IOlapResponseData
	{
		// Token: 0x06007F93 RID: 32659 RVA: 0x001D26DC File Offset: 0x001D08DC
		public AdomdResponseData(IOlapPivotConfiguration configuration, CellSet responseCellSet)
		{
			this.RowAxisTuples = new List<IOlapTuple>();
			this.ColumnAxisTuples = new List<IOlapTuple>();
			this.Cells = new OlapCellsDictionary();
			if (responseCellSet.Axes.Count > 0)
			{
				Axis axis = responseCellSet.Axes[0];
				foreach (Tuple tupleElement in axis.Set.Tuples)
				{
					this.ColumnAxisTuples.Add(AdomdTupleInfo.FromAdomdTuple(tupleElement));
				}
			}
			if (responseCellSet.Axes.Count > 1)
			{
				Axis axis2 = responseCellSet.Axes[1];
				foreach (Tuple tupleElement2 in axis2.Set.Tuples)
				{
					this.RowAxisTuples.Add(AdomdTupleInfo.FromAdomdTuple(tupleElement2));
				}
			}
			foreach (Cell cellElement in responseCellSet.Cells)
			{
				this.Cells.AddCell(AdomdCellInfo.FromAdomdCell(cellElement));
			}
			this.Configuration = configuration;
		}

		// Token: 0x17002899 RID: 10393
		// (get) Token: 0x06007F94 RID: 32660 RVA: 0x001D27EC File Offset: 0x001D09EC
		// (set) Token: 0x06007F95 RID: 32661 RVA: 0x001D27F4 File Offset: 0x001D09F4
		public IList<IOlapTuple> RowAxisTuples { get; private set; }

		// Token: 0x1700289A RID: 10394
		// (get) Token: 0x06007F96 RID: 32662 RVA: 0x001D27FD File Offset: 0x001D09FD
		// (set) Token: 0x06007F97 RID: 32663 RVA: 0x001D2805 File Offset: 0x001D0A05
		public IList<IOlapTuple> ColumnAxisTuples { get; private set; }

		// Token: 0x1700289B RID: 10395
		// (get) Token: 0x06007F98 RID: 32664 RVA: 0x001D280E File Offset: 0x001D0A0E
		// (set) Token: 0x06007F99 RID: 32665 RVA: 0x001D2816 File Offset: 0x001D0A16
		public OlapCellsDictionary Cells { get; private set; }

		// Token: 0x1700289C RID: 10396
		// (get) Token: 0x06007F9A RID: 32666 RVA: 0x001D281F File Offset: 0x001D0A1F
		// (set) Token: 0x06007F9B RID: 32667 RVA: 0x001D2827 File Offset: 0x001D0A27
		public IOlapPivotConfiguration Configuration { get; private set; }
	}
}
