using System;
using System.Collections.Generic;
using System.Linq;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000D17 RID: 3351
	internal class OlapTupleProcessorInput
	{
		// Token: 0x06007CFC RID: 31996 RVA: 0x001CAEFC File Offset: 0x001C90FC
		public OlapTupleProcessorInput()
		{
			this.AggregateDescriptions = new List<OlapAggregateDescription>();
			this.GroupDescriptions = new List<OlapGroupDescription>();
			this.Tuples = new List<IOlapTuple>();
		}

		// Token: 0x170027DD RID: 10205
		// (get) Token: 0x06007CFD RID: 31997 RVA: 0x001CAF25 File Offset: 0x001C9125
		// (set) Token: 0x06007CFE RID: 31998 RVA: 0x001CAF2D File Offset: 0x001C912D
		public IList<OlapAggregateDescription> AggregateDescriptions { get; set; }

		// Token: 0x170027DE RID: 10206
		// (get) Token: 0x06007CFF RID: 31999 RVA: 0x001CAF36 File Offset: 0x001C9136
		// (set) Token: 0x06007D00 RID: 32000 RVA: 0x001CAF3E File Offset: 0x001C913E
		public IList<OlapGroupDescription> GroupDescriptions { get; set; }

		// Token: 0x170027DF RID: 10207
		// (get) Token: 0x06007D01 RID: 32001 RVA: 0x001CAF47 File Offset: 0x001C9147
		// (set) Token: 0x06007D02 RID: 32002 RVA: 0x001CAF4F File Offset: 0x001C914F
		public IList<IOlapTuple> Tuples { get; set; }

		// Token: 0x06007D03 RID: 32003 RVA: 0x001CAF58 File Offset: 0x001C9158
		public static OlapTupleProcessorInput ColumnInfoFromData(IOlapResponseData data)
		{
			return new OlapTupleProcessorInput
			{
				AggregateDescriptions = data.Configuration.PivotAggregateDescriptions.ToList<OlapAggregateDescription>(),
				GroupDescriptions = data.Configuration.PivotColumnGroupDescriptions.ToList<OlapGroupDescription>(),
				Tuples = data.ColumnAxisTuples
			};
		}

		// Token: 0x06007D04 RID: 32004 RVA: 0x001CAFA4 File Offset: 0x001C91A4
		public static OlapTupleProcessorInput RowInfoFromData(IOlapResponseData data)
		{
			return new OlapTupleProcessorInput
			{
				AggregateDescriptions = data.Configuration.PivotAggregateDescriptions.ToList<OlapAggregateDescription>(),
				GroupDescriptions = data.Configuration.PivotRowGroupDescriptions.ToList<OlapGroupDescription>(),
				Tuples = data.RowAxisTuples
			};
		}
	}
}
