using System;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.PivotGrid.Core.Filtering
{
	// Token: 0x020006E1 RID: 1761
	[DataContract]
	public sealed class ValueGroupFilter : SingleGroupFilter, IValueGroupFilter, IConditionFactory
	{
		// Token: 0x17001486 RID: 5254
		// (get) Token: 0x06003ED6 RID: 16086 RVA: 0x000C8168 File Offset: 0x000C6368
		// (set) Token: 0x06003ED7 RID: 16087 RVA: 0x000C8170 File Offset: 0x000C6370
		Condition IValueGroupFilter.Condition
		{
			get
			{
				return this.Condition;
			}
			set
			{
				this.Condition = (value as LocalCondition);
			}
		}

		// Token: 0x17001487 RID: 5255
		// (get) Token: 0x06003ED8 RID: 16088 RVA: 0x000C817E File Offset: 0x000C637E
		// (set) Token: 0x06003ED9 RID: 16089 RVA: 0x000C8186 File Offset: 0x000C6386
		int IValueGroupFilter.AggregateIndex
		{
			get
			{
				return this.AggregateIndex;
			}
			set
			{
				this.AggregateIndex = value;
			}
		}

		// Token: 0x06003EDA RID: 16090 RVA: 0x000C818F File Offset: 0x000C638F
		Condition IConditionFactory.CreateCondition(Type conditionType)
		{
			if (conditionType == typeof(IComparisonCondition))
			{
				return new ComparisonCondition();
			}
			if (conditionType == typeof(IIntervalCondition))
			{
				return new IntervalCondition();
			}
			return null;
		}

		// Token: 0x17001488 RID: 5256
		// (get) Token: 0x06003EDB RID: 16091 RVA: 0x000C81C2 File Offset: 0x000C63C2
		// (set) Token: 0x06003EDC RID: 16092 RVA: 0x000C81CA File Offset: 0x000C63CA
		[DataMember]
		public LocalCondition Condition
		{
			get
			{
				return this.condition;
			}
			set
			{
				if (this.condition != value)
				{
					this.condition = value;
					base.OnPropertyChanged("Condition");
				}
			}
		}

		// Token: 0x17001489 RID: 5257
		// (get) Token: 0x06003EDD RID: 16093 RVA: 0x000C81E7 File Offset: 0x000C63E7
		// (set) Token: 0x06003EDE RID: 16094 RVA: 0x000C81EF File Offset: 0x000C63EF
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

		// Token: 0x06003EDF RID: 16095 RVA: 0x000C820C File Offset: 0x000C640C
		protected internal override bool Filter(IGroup group, IAggregateResultProvider results, PivotAxis axis)
		{
			return this.Condition == null || this.Condition.PassesFilter(ValueGroupFilter.GetValue(group, results, axis, this.AggregateIndex));
		}

		// Token: 0x06003EE0 RID: 16096 RVA: 0x000C8231 File Offset: 0x000C6431
		protected override Cloneable CreateInstanceCore()
		{
			return new ValueGroupFilter();
		}

		// Token: 0x06003EE1 RID: 16097 RVA: 0x000C8238 File Offset: 0x000C6438
		protected override void CloneCore(Cloneable source)
		{
			ValueGroupFilter valueGroupFilter = source as ValueGroupFilter;
			if (valueGroupFilter != null)
			{
				this.Condition = ((valueGroupFilter.Condition == null) ? null : (valueGroupFilter.Condition.Clone() as LocalCondition));
				this.AggregateIndex = valueGroupFilter.AggregateIndex;
			}
		}

		// Token: 0x06003EE2 RID: 16098 RVA: 0x000C827C File Offset: 0x000C647C
		private static object GetValue(IGroup group, IAggregateResultProvider results, PivotAxis axis, int aggregateIndex)
		{
			Coordinate groups = (axis == PivotAxis.Rows) ? new Coordinate(group, results.Root.ColumnGroup) : new Coordinate(results.Root.RowGroup, group);
			AggregateValue aggregateResult = results.GetAggregateResult(aggregateIndex, groups);
			return (aggregateResult == null) ? null : aggregateResult.GetValue();
		}

		// Token: 0x06003EE3 RID: 16099 RVA: 0x000C82D0 File Offset: 0x000C64D0
		internal override bool TrackDescriptions(IDescriptionIndexMap map)
		{
			bool flag = base.TrackDescriptions(map);
			AggregateMapResult aggregateMapResult = DescriptionIndexMapExtensions.MapAggregate(map, this.AggregateIndex);
			this.AggregateIndex = aggregateMapResult.Index;
			return flag && aggregateMapResult.Success;
		}

		// Token: 0x040010B1 RID: 4273
		private LocalCondition condition;

		// Token: 0x040010B2 RID: 4274
		private int aggregateIndex;
	}
}
