using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace Telerik.Web.UI.PivotGrid.Core.Engine
{
	// Token: 0x020006AA RID: 1706
	internal class PivotResultsProcessingState
	{
		// Token: 0x06003D78 RID: 15736 RVA: 0x000C5AB8 File Offset: 0x000C3CB8
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification = "Won't fix.")]
		public PivotResultsProcessingState()
		{
			this.MaxDegreeOfParallelism = Environment.ProcessorCount;
			this.FormatTotals = new Dictionary<Coordinate, AggregateValue[]>();
			this.UniqueGroupKeys = new List<List<HashSet<object>>>();
			this.CancellationTokenSource = new CancellationTokenSource();
			this.RowGroupDescriptions = new ReadOnlyList<GroupDescription, GroupDescription>(new List<GroupDescription>());
			this.ColumnGroupDescriptions = new ReadOnlyList<GroupDescription, GroupDescription>(new List<GroupDescription>());
			this.AggregateDescriptions = new ReadOnlyList<IAggregateDescription, IAggregateDescription>(new List<IAggregateDescription>());
			this.FilterDescriptions = new ReadOnlyList<FilterDescription, FilterDescription>(new List<FilterDescription>());
		}

		// Token: 0x17001425 RID: 5157
		// (get) Token: 0x06003D79 RID: 15737 RVA: 0x000C5B37 File Offset: 0x000C3D37
		// (set) Token: 0x06003D7A RID: 15738 RVA: 0x000C5B3F File Offset: 0x000C3D3F
		public virtual CancellationTokenSource CancellationTokenSource { get; set; }

		// Token: 0x17001426 RID: 5158
		// (get) Token: 0x06003D7B RID: 15739 RVA: 0x000C5B48 File Offset: 0x000C3D48
		public CancellationToken CancellationToken
		{
			get
			{
				return this.CancellationTokenSource.Token;
			}
		}

		// Token: 0x17001427 RID: 5159
		// (get) Token: 0x06003D7C RID: 15740 RVA: 0x000C5B55 File Offset: 0x000C3D55
		// (set) Token: 0x06003D7D RID: 15741 RVA: 0x000C5B5D File Offset: 0x000C3D5D
		public IReadOnlyList<GroupDescription> RowGroupDescriptions { get; set; }

		// Token: 0x17001428 RID: 5160
		// (get) Token: 0x06003D7E RID: 15742 RVA: 0x000C5B66 File Offset: 0x000C3D66
		// (set) Token: 0x06003D7F RID: 15743 RVA: 0x000C5B6E File Offset: 0x000C3D6E
		public IReadOnlyList<GroupDescription> ColumnGroupDescriptions { get; set; }

		// Token: 0x17001429 RID: 5161
		// (get) Token: 0x06003D80 RID: 15744 RVA: 0x000C5B77 File Offset: 0x000C3D77
		// (set) Token: 0x06003D81 RID: 15745 RVA: 0x000C5B7F File Offset: 0x000C3D7F
		public IReadOnlyList<IAggregateDescription> AggregateDescriptions { get; set; }

		// Token: 0x1700142A RID: 5162
		// (get) Token: 0x06003D82 RID: 15746 RVA: 0x000C5B88 File Offset: 0x000C3D88
		// (set) Token: 0x06003D83 RID: 15747 RVA: 0x000C5B90 File Offset: 0x000C3D90
		public IReadOnlyList<FilterDescription> FilterDescriptions { get; set; }

		// Token: 0x1700142B RID: 5163
		// (get) Token: 0x06003D84 RID: 15748 RVA: 0x000C5B99 File Offset: 0x000C3D99
		// (set) Token: 0x06003D85 RID: 15749 RVA: 0x000C5BA1 File Offset: 0x000C3DA1
		public int MaxDegreeOfParallelism { get; set; }

		// Token: 0x1700142C RID: 5164
		// (get) Token: 0x06003D86 RID: 15750 RVA: 0x000C5BAA File Offset: 0x000C3DAA
		// (set) Token: 0x06003D87 RID: 15751 RVA: 0x000C5BB2 File Offset: 0x000C3DB2
		public virtual IAggregateResultProvider AggregatesProvider { get; set; }

		// Token: 0x1700142D RID: 5165
		// (get) Token: 0x06003D88 RID: 15752 RVA: 0x000C5BBB File Offset: 0x000C3DBB
		// (set) Token: 0x06003D89 RID: 15753 RVA: 0x000C5BC3 File Offset: 0x000C3DC3
		public Dictionary<Coordinate, AggregateValue[]> FormatTotals { get; set; }

		// Token: 0x1700142E RID: 5166
		// (get) Token: 0x06003D8A RID: 15754 RVA: 0x000C5BCC File Offset: 0x000C3DCC
		// (set) Token: 0x06003D8B RID: 15755 RVA: 0x000C5BD4 File Offset: 0x000C3DD4
		public List<List<HashSet<object>>> UniqueGroupKeys { get; set; }
	}
}
