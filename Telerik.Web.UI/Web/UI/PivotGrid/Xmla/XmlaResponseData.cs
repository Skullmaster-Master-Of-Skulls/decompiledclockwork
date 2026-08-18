using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Telerik.Web.UI.PivotGrid.Core.Olap;

namespace Telerik.Web.UI.PivotGrid.Xmla
{
	// Token: 0x02000D80 RID: 3456
	internal class XmlaResponseData : IOlapResponseData
	{
		// Token: 0x060080DD RID: 32989 RVA: 0x001D77FC File Offset: 0x001D59FC
		public XmlaResponseData(IOlapPivotConfiguration configuration, string responseString)
		{
			XmlaMultidimensionalResponseInfo xmlaMultidimensionalResponseInfo = new XmlaMultidimensionalResponseInfo(responseString);
			this.RowAxisTuples = (from rte in xmlaMultidimensionalResponseInfo.RowTupleElements
			select XmlaTupleInfo.FromXElement(rte)).OfType<IOlapTuple>().ToList<IOlapTuple>();
			this.ColumnAxisTuples = (from cte in xmlaMultidimensionalResponseInfo.ColumnTupleElements
			select XmlaTupleInfo.FromXElement(cte)).OfType<IOlapTuple>().ToList<IOlapTuple>();
			List<IOlapCell> olapCells = (from dce in xmlaMultidimensionalResponseInfo.DataCellElements
			select XmlaCellInfo.FromXElement(dce)).OfType<IOlapCell>().ToList<IOlapCell>();
			this.Cells = new OlapCellsDictionary(olapCells);
			this.Configuration = configuration;
		}

		// Token: 0x170028E3 RID: 10467
		// (get) Token: 0x060080DE RID: 32990 RVA: 0x001D78CC File Offset: 0x001D5ACC
		// (set) Token: 0x060080DF RID: 32991 RVA: 0x001D78D4 File Offset: 0x001D5AD4
		public IList<IOlapTuple> RowAxisTuples { get; private set; }

		// Token: 0x170028E4 RID: 10468
		// (get) Token: 0x060080E0 RID: 32992 RVA: 0x001D78DD File Offset: 0x001D5ADD
		// (set) Token: 0x060080E1 RID: 32993 RVA: 0x001D78E5 File Offset: 0x001D5AE5
		public IList<IOlapTuple> ColumnAxisTuples { get; private set; }

		// Token: 0x170028E5 RID: 10469
		// (get) Token: 0x060080E2 RID: 32994 RVA: 0x001D78EE File Offset: 0x001D5AEE
		// (set) Token: 0x060080E3 RID: 32995 RVA: 0x001D78F6 File Offset: 0x001D5AF6
		public OlapCellsDictionary Cells { get; private set; }

		// Token: 0x170028E6 RID: 10470
		// (get) Token: 0x060080E4 RID: 32996 RVA: 0x001D78FF File Offset: 0x001D5AFF
		// (set) Token: 0x060080E5 RID: 32997 RVA: 0x001D7907 File Offset: 0x001D5B07
		public IOlapPivotConfiguration Configuration { get; private set; }
	}
}
