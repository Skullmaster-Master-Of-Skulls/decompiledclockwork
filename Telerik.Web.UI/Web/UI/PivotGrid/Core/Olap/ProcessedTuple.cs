using System;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000D19 RID: 3353
	internal class ProcessedTuple : IComparable<ProcessedTuple>
	{
		// Token: 0x06007D0B RID: 32011 RVA: 0x001CB076 File Offset: 0x001C9276
		public ProcessedTuple()
		{
			this.AggregateIndex = -1;
		}

		// Token: 0x170027E2 RID: 10210
		// (get) Token: 0x06007D0C RID: 32012 RVA: 0x001CB085 File Offset: 0x001C9285
		// (set) Token: 0x06007D0D RID: 32013 RVA: 0x001CB08D File Offset: 0x001C928D
		public int SourceTupleIndex { get; set; }

		// Token: 0x170027E3 RID: 10211
		// (get) Token: 0x06007D0E RID: 32014 RVA: 0x001CB096 File Offset: 0x001C9296
		// (set) Token: 0x06007D0F RID: 32015 RVA: 0x001CB09E File Offset: 0x001C929E
		public Group Group { get; set; }

		// Token: 0x170027E4 RID: 10212
		// (get) Token: 0x06007D10 RID: 32016 RVA: 0x001CB0A7 File Offset: 0x001C92A7
		public bool HasAggregate
		{
			get
			{
				return this.AggregateIndex >= 0;
			}
		}

		// Token: 0x170027E5 RID: 10213
		// (get) Token: 0x06007D11 RID: 32017 RVA: 0x001CB0B5 File Offset: 0x001C92B5
		// (set) Token: 0x06007D12 RID: 32018 RVA: 0x001CB0BD File Offset: 0x001C92BD
		public int AggregateIndex { get; set; }

		// Token: 0x06007D13 RID: 32019 RVA: 0x001CB0C8 File Offset: 0x001C92C8
		public int CompareTo(ProcessedTuple other)
		{
			return this.SourceTupleIndex.CompareTo(other.SourceTupleIndex);
		}
	}
}
