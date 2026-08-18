using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core.Aggregates;

namespace Telerik.Web.UI.PivotGrid.Core.Totals
{
	// Token: 0x02000C73 RID: 3187
	[DataContract]
	public sealed class RankTotals : SiblingTotalsFormat
	{
		// Token: 0x170026CE RID: 9934
		// (get) Token: 0x060077DB RID: 30683 RVA: 0x001BB7A3 File Offset: 0x001B99A3
		// (set) Token: 0x060077DC RID: 30684 RVA: 0x001BB7AB File Offset: 0x001B99AB
		[DataMember]
		public SortOrder SortOrder
		{
			get
			{
				return this.sortOrder;
			}
			set
			{
				if (this.sortOrder != value)
				{
					this.sortOrder = value;
					base.NotifySettingsChanged(new SettingsChangedEventArgs());
					base.OnPropertyChanged("SortOrder");
				}
			}
		}

		// Token: 0x170026CF RID: 9935
		// (get) Token: 0x060077DD RID: 30685 RVA: 0x001BB7D3 File Offset: 0x001B99D3
		// (set) Token: 0x060077DE RID: 30686 RVA: 0x001BB7DB File Offset: 0x001B99DB
		internal TotalComparer Comparer { get; set; }

		// Token: 0x060077DF RID: 30687 RVA: 0x001BB7E4 File Offset: 0x001B99E4
		public override string GetStringFormat(Type dataType, string stringFormat)
		{
			return "G";
		}

		// Token: 0x060077E0 RID: 30688 RVA: 0x001BB7FC File Offset: 0x001B99FC
		internal override void FormatTotals(IReadOnlyList<TotalValue> totals, IAggregateResultProvider results)
		{
			IComparer<TotalValue> comparer = this.GetComparer();
			List<TotalValue> list = (from f in totals
			where f.Value != null
			select f).ToList<TotalValue>();
			list.Sort(comparer);
			int num = 0;
			TotalValue x = null;
			foreach (TotalValue totalValue in list)
			{
				if (comparer.Compare(x, totalValue) != 0)
				{
					num++;
				}
				x = totalValue;
				totalValue.FormattedValue = new ConstantValueAggregate(num);
			}
		}

		// Token: 0x060077E1 RID: 30689 RVA: 0x001BB8A4 File Offset: 0x001B9AA4
		protected override void CloneCore(Cloneable source)
		{
			RankTotals rankTotals = source as RankTotals;
			if (rankTotals != null)
			{
				this.SortOrder = rankTotals.SortOrder;
				if (rankTotals.Comparer != null)
				{
					this.Comparer = (rankTotals.Comparer.Clone() as TotalComparer);
				}
			}
			base.CloneCore(source);
		}

		// Token: 0x060077E2 RID: 30690 RVA: 0x001BB8EC File Offset: 0x001B9AEC
		protected override Cloneable CreateInstanceCore()
		{
			return new RankTotals();
		}

		// Token: 0x060077E3 RID: 30691 RVA: 0x001BB8F3 File Offset: 0x001B9AF3
		internal override RunningTotalSubGroupVariation SubVariation()
		{
			return RunningTotalSubGroupVariation.GroupDescriptionAndName;
		}

		// Token: 0x060077E4 RID: 30692 RVA: 0x001BB8F8 File Offset: 0x001B9AF8
		private IComparer<TotalValue> GetComparer()
		{
			IComparer<TotalValue> comparer;
			if (this.Comparer == null)
			{
				comparer = new DoubleAggregateValueComparer();
			}
			else
			{
				comparer = this.Comparer;
			}
			if (this.SortOrder == SortOrder.Descending)
			{
				comparer = new DescendingSort<TotalValue>(comparer);
			}
			return comparer;
		}

		// Token: 0x040020C8 RID: 8392
		private SortOrder sortOrder;
	}
}
