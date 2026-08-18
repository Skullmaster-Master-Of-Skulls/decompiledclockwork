using System;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000CD6 RID: 3286
	[DataContract]
	public sealed class GrandTotalComparer : GroupComparer, IDescriptionsReferencing
	{
		// Token: 0x1700275E RID: 10078
		// (get) Token: 0x06007AC6 RID: 31430 RVA: 0x001C2B0C File Offset: 0x001C0D0C
		// (set) Token: 0x06007AC7 RID: 31431 RVA: 0x001C2B14 File Offset: 0x001C0D14
		[DataMember]
		public int AggregateIndex
		{
			get
			{
				return this.aggregateIndex;
			}
			set
			{
				if (this.aggregateIndex != value)
				{
					this.aggregateIndex = value;
					base.OnPropertyChanged("AggregateIndex");
				}
			}
		}

		// Token: 0x06007AC8 RID: 31432 RVA: 0x001C2B34 File Offset: 0x001C0D34
		public override int CompareGroups(IAggregateResultProvider results, IGroup left, IGroup right, PivotAxis axis)
		{
			Coordinate groups;
			Coordinate groups2;
			if (axis == PivotAxis.Rows)
			{
				groups = new Coordinate(left, results.Root.ColumnGroup);
				groups2 = new Coordinate(right, results.Root.ColumnGroup);
			}
			else
			{
				groups = new Coordinate(results.Root.RowGroup, left);
				groups2 = new Coordinate(results.Root.RowGroup, right);
			}
			AggregateValue aggregateResult = results.GetAggregateResult(this.AggregateIndex, groups);
			AggregateValue aggregateResult2 = results.GetAggregateResult(this.AggregateIndex, groups2);
			return GrandTotalComparer.Comparer.Compare((aggregateResult == null) ? null : aggregateResult.GetValue(), (aggregateResult2 == null) ? null : aggregateResult2.GetValue());
		}

		// Token: 0x06007AC9 RID: 31433 RVA: 0x001C2BE1 File Offset: 0x001C0DE1
		protected override Cloneable CreateInstanceCore()
		{
			return new GrandTotalComparer();
		}

		// Token: 0x06007ACA RID: 31434 RVA: 0x001C2BE8 File Offset: 0x001C0DE8
		protected override void CloneCore(Cloneable source)
		{
			GrandTotalComparer grandTotalComparer = source as GrandTotalComparer;
			if (grandTotalComparer != null)
			{
				this.AggregateIndex = grandTotalComparer.AggregateIndex;
			}
		}

		// Token: 0x06007ACB RID: 31435 RVA: 0x001C2C0C File Offset: 0x001C0E0C
		internal bool TrackDescriptions(IDescriptionIndexMap map)
		{
			AggregateMapResult aggregateMapResult = DescriptionIndexMapExtensions.MapAggregate(map, this.AggregateIndex);
			this.AggregateIndex = aggregateMapResult.Index;
			return aggregateMapResult.Success;
		}

		// Token: 0x06007ACC RID: 31436 RVA: 0x001C2C3A File Offset: 0x001C0E3A
		bool IDescriptionsReferencing.TrackDescriptions(IDescriptionIndexMap map)
		{
			return this.TrackDescriptions(map);
		}

		// Token: 0x0400219E RID: 8606
		private int aggregateIndex;

		// Token: 0x0400219F RID: 8607
		private static readonly DefaultComparer Comparer = new DefaultComparer();
	}
}
